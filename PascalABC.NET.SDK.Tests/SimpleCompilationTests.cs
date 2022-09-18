using PascalABC.NET.SDK.TestFramework;
using Xunit;

namespace PascalABC.NET.SDK.Tests;

public class SimpleCompilationTests : SdkTestBase
{
    [Fact] public Task Test1() => TestSuccess("Test1.pasproj", "bin/Hello1.exe");
    [Fact] public Task Test2() => TestSuccess("Test2.pasproj", "bin/Hello1.exe", "bin/Hello2.exe");
}
