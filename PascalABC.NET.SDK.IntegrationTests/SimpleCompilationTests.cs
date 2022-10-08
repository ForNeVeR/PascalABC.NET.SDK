using PascalABC.NET.SDK.TestFramework;
using Xunit;

namespace PascalABC.NET.SDK.IntegrationTests;

public class SimpleCompilationTests : SdkTestBase
{
    [Fact] public Task Test1() => TestSuccess("Test1.pasproj", "bin/Debug/net472/Test1.exe");
    [Fact] public Task Test2() => TestFailure("Test2.pasproj", "Only one main compile item is supported. Currently, there are multiple: Hello1.pas, Hello2.pas.");
    [Fact] public Task TestMultiProjectSolution() => TestSuccess(
        "MultiProjectSolution/MultiProjectSolution.sln",
        "MultiProjectSolution/PascalProject/bin/Debug/net472/PascalProject.exe");
    [Fact] public Task TestSystemMemory() => TestSuccess("TestSystemMemory.pasproj", "bin/Debug/net472/TestSystemMemory.exe");
}
