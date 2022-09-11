using PascalABC.NET.SDK.TestFramework;
using Xunit;

namespace PascalABC.NET.SDK.Tests;

public class SimpleCompilationTests : SdkTestBase
{
    [Fact] public Task Test1() => TestSuccess("Test1.pasproj", "bin/Hello1.exe");
    [Fact] public Task Test2() => TestSuccess("Test2.pasproj", "bin/Hello1.exe", "bin/Hello2.exe");
    [Fact] public async Task BlockedOutputItemTest()
    {
        var path = GetTestDataPath("Hello1.exe");

        await File.WriteAllBytesAsync(path, Array.Empty<byte>());
        await TestFailure(
            "Test1.pasproj",
            "already exist on disk but would be removed after compilation. Please clean up manually.");
    }
}
