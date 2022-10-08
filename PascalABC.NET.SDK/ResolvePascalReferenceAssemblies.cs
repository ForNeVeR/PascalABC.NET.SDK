using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace PascalABC.NET.SDK;

public class ResolvePascalReferenceAssemblies : Task
{
    [Required, PublicAPI] public ITaskItem[] ReferencePaths { get; set; }
    [Required, PublicAPI] public ITaskItem[] RuntimeCopyLocalItems { get; set; }
    [Required, PublicAPI] public ITaskItem[] ResolvedCompileFileDefinitions { get; set; }

    [Output] public ITaskItem[] ResolvedReferences { get; private set; }

    public override bool Execute()
    {
        // Debugger.Launch();
        var runtimeLibs = RuntimeCopyLocalItems.ToDictionary(GetNuGetKey, item => item.ItemSpec);
        var compileTimeLibs = ResolvedCompileFileDefinitions.ToDictionary(item => Path.GetFullPath(item.ItemSpec), GetNuGetKey);

        ResolvedReferences = ReferencePaths.Select(item =>
        {
            var itemSpec = item.ItemSpec;
            if (compileTimeLibs.TryGetValue(Path.GetFullPath(itemSpec), out var nuGetKey)
                && runtimeLibs.TryGetValue(nuGetKey, out var runtimeItemSpec))
            {
                itemSpec = runtimeItemSpec;
            }

            return (ITaskItem)new TaskItem(itemSpec);
        }).ToArray();

        return true;
    }

    private static (string, string, string) GetNuGetKey(ITaskItem item)
    {
        var nuGetPackageId = item.GetMetadata("NuGetPackageId");
        var nuGetPackageVersion = item.GetMetadata("NuGetPackageVersion");
        var assemblyName = Path.GetFileName(item.ItemSpec);
        return (nuGetPackageId, nuGetPackageVersion, assemblyName);
    }
}
