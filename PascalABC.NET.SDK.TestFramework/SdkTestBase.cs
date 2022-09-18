using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;
using Medallion.Shell;
using Xunit;

namespace PascalABC.NET.SDK.TestFramework;

public abstract class SdkTestBase
{
    private readonly string _temporaryPath = Path.GetTempFileName();

    protected SdkTestBase()
    {
        File.Delete(_temporaryPath);

        var assemblyPath = Assembly.GetExecutingAssembly().Location;
        var testDataPath = Path.Combine(Path.GetDirectoryName(assemblyPath)!, "TestData");
        CopyDirectoryRecursive(testDataPath, _temporaryPath);

        var sourceRootPath = Path.Combine(Path.GetDirectoryName(assemblyPath)!, "../../../../");
        var nupkgPath = Path.GetFullPath(Path.Combine(sourceRootPath, "nupkg"));
        EmitNuGetConfig(Path.Combine(_temporaryPath, "NuGet.config"), nupkgPath);
        EmitGlobalJson(Path.Combine(_temporaryPath, "global.json"), GetDevPackageVersion(sourceRootPath));
    }

    private static void CopyDirectoryRecursive(string source, string target)
    {
        Directory.CreateDirectory(target);

        foreach (var subDirPath in Directory.GetDirectories(source))
        {
            var dirName = Path.GetFileName(subDirPath);
            CopyDirectoryRecursive(subDirPath, Path.Combine(target, dirName));
        }

        foreach (var filePath in Directory.GetFiles(source))
        {
            var fileName = Path.GetFileName(filePath);
            File.Copy(filePath, Path.Combine(target, fileName));
        }
    }

    private static void EmitNuGetConfig(string configFilePath, string packageSourcePath)
    {
        File.WriteAllText(configFilePath, $@"<configuration>
    <config>
        <add key=""globalPackagesFolder"" value=""packages"" />
    </config>
    <packageSources>
        <add key=""local"" value=""{packageSourcePath}"" />
    </packageSources>
</configuration>
");
    }

    private static string GetDevPackageVersion(string sourceRoot)
    {
        var sdkProjPath = Path.Combine(sourceRoot, "PascalABC.NET.SDK/PascalABC.NET.SDK.proj");
        var document = XDocument.Load(sdkProjPath);
        var versionElement = document.XPathSelectElement("//VersionPrefix");
        return versionElement!.Value + "-dev";
    }

    private static void EmitGlobalJson(string globalJsonPath, string packageVersion)
    {
        File.WriteAllText(globalJsonPath, $@"{{
    ""msbuild-sdks"": {{
        ""FVNever.PascalABC.NET.SDK"" : ""{packageVersion}""
    }}
}}
");
    }

    private string GetTestDataPath(string relativePath) => Path.Combine(_temporaryPath, relativePath);

    private async Task<CommandResult> BuildProject(string projectFilePath)
    {
        var command = Command.Run("dotnet",  "build", "--verbosity:normal", projectFilePath);
        return await command.Task;
    }

    protected async Task TestSuccess(string projectFileRelativePath, params string[] outputFileRelativePaths)
    {
        foreach (var outputFileRelativePath in outputFileRelativePaths)
        {
            var outputFilePath = GetTestDataPath(outputFileRelativePath);
            if (File.Exists(outputFilePath)) throw new Exception($"File \"{outputFilePath}\" should not exist.");
        }

        var projectFilePath = GetTestDataPath(projectFileRelativePath);
        var result = await BuildProject(projectFilePath);

        Assert.True(result.Success, "Build should succeed. Stdout: " + result.StandardOutput);
        foreach (var outputFileRelativePath in outputFileRelativePaths)
        {
            var outputFilePath = GetTestDataPath(outputFileRelativePath);
            Assert.True(File.Exists(outputFilePath), $"File \"{outputFilePath}\" should exist.");
        }
    }

    protected async Task TestFailure(string projectFileRelativePath, string errorMessage)
    {
        var projectFilePath = GetTestDataPath(projectFileRelativePath);
        if (!File.Exists(projectFilePath)) throw new Exception($"File \"{projectFilePath}\" doesn't exists.");
        var result = await BuildProject(projectFilePath);
        Assert.False(result.Success, "Build should be failed. Stdout: " + result.StandardOutput);

        Assert.Contains(errorMessage, result.StandardOutput);
    }
}
