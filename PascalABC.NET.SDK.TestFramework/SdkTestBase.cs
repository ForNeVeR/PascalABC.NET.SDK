using System.Reflection;
using Medallion.Shell;
using Xunit;

namespace PascalABC.NET.SDK.TestFramework;

public abstract class SdkTestBase : IDisposable
{
    private readonly string _temporaryPath = Path.GetTempFileName();

    protected SdkTestBase()
    {
        File.Delete(_temporaryPath);

        var assemblyPath = Assembly.GetExecutingAssembly().Location;
        var sourcePath = Path.Combine(Path.GetDirectoryName(assemblyPath)!, "TestData");
        CopyDirectoryRecursive(sourcePath, _temporaryPath);

        var sdkPath = Path.GetFullPath(
            Path.Combine(Path.GetDirectoryName(assemblyPath)!, "../../../../PascalABC.NET.SDK"));
        CopyDirectoryRecursive(sdkPath, _temporaryPath);
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

    public void Dispose()
    {
        // Directory.Delete(TemporaryPath, recursive: true);
    }

    protected string GetTestDataPath(string relativePath) => Path.Combine(_temporaryPath, relativePath);

    private async Task<CommandResult> BuildProject(string projectFilePath)
    {
        var compilerPath = await Artifacts.GetPascalAbcNetDirectory();
        var command = Command.Run(
            "dotnet",
            new[] { "build", "--verbosity:normal", projectFilePath },
            opts => opts.EnvironmentVariable(
                "PATH",
                Environment.GetEnvironmentVariable("PATH") + Path.PathSeparator + compilerPath));
        return await command.Task;
    }

    protected async Task TestFailure(string projectFileRelativePath, string errorMessage)
    {
        var projectFilePath = GetTestDataPath(projectFileRelativePath);
        if (!File.Exists(projectFilePath)) throw new Exception($"File \"{projectFilePath}\" doesn't exists.");
        var result = await BuildProject(projectFilePath);
        Assert.False(result.Success, "Build should be failed. Stdout: " + result.StandardOutput);

        Assert.Contains(errorMessage, result.StandardOutput);
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
}
