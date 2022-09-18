using PascalABC.NET.SDK.TestFramework;
using Xunit;

namespace PascalABC.NET.SDK.Tests;

public class SimpleCompilationTests : SdkTestBase
{
    [Fact] public Task Test1() => TestSuccess("Test1.pasproj", "bin/Debug/net472/Test1.exe");
    [Fact] public Task Test2() => TestFailure("Test2.pasproj", "Only one main compile item is supported. Currently, there are multiple: Hello1.pas, Hello2.pas.");
}
