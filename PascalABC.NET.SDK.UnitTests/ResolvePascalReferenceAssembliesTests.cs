using Microsoft.Build.Utilities;
using Xunit;

namespace PascalABC.NET.SDK.UnitTests;

public class ResolvePascalReferenceAssembliesTests
{
    [Fact]
    public void ResolveShouldWorkCorrectly()
    {
        var task = new ResolvePascalReferenceAssemblies
        {
            ReferencePaths = new[]
            {
                new TaskItem("/tmp/ref/System.Memory.dll")
            },
            RuntimeCopyLocalItems = new[]
            {
                CreateReferenceItem("System.Memory", "4.5.1", "/tmp/lib/System.Memory.dll")
            },
            ResolvedCompileFileDefinitions = new[]
            {
                CreateReferenceItem("System.Memory", "4.5.1", "/tmp/ref/System.Memory.dll")
            }
        };

        Assert.True(task.Execute());

        var results = task.ResolvedReferences.Select(item => item.ItemSpec);
        Assert.Equal(new[] { "/tmp/lib/System.Memory.dll" }, results);
    }

    private TaskItem CreateReferenceItem(string nuGetPackageId, string nuGetPackageVersion, string itemSpec)
    {
        var item = new TaskItem(itemSpec);
        item.SetMetadata("NuGetPackageId", nuGetPackageId);
        item.SetMetadata("NuGetPackageVersion", nuGetPackageVersion);
        return item;
    }
}
