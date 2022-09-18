PascalABC.NET SDK [![Status Enfer][status-enfer]][andivionian-status-classifier]
=================

This is an MSBuild SDK for [PascalABC.NET][pascalabc.net] programming language.

NuGet
-----

- `FVNever.PascalABC.NET.Compiler`: [<img valign="middle" src="https://img.shields.io/nuget/v/FVNever.PascalABC.NET.Compiler" alt="NuGet package version badge">][nuget.compiler.package]
- `FVNever.PascalABC.NET.SDK`: [<img valign="middle" src="https://img.shields.io/nuget/v/FVNever.PascalABC.NET.SDK" alt="NuGet package version badge">][nuget.sdk.package]

Usage
-----

### Prerequisites

1. For now, only Windows is supported as the target platform.
2. Download [.NET 6 SDK][dotnet.sdk] or later.

### Quick Start

Prepare the following project file, `MyProject.pasproj`:

```xml
<Project Sdk="FVNever.PascalABC.NET.SDK/1.0.0">

   <PropertyGroup>
       <TargetFramework>net472</TargetFramework>
   </PropertyGroup>

   <ItemGroup>
       <MainCompile Include="Hello.pas" />
   </ItemGroup>

</Project>
```

where `Hello.pas` is your main Pascal module, for example:

```pas
uses References_Generated; // to use SDK-generated assembly references
begin
    writeln('Hello, world!');
end.
```

After that, `dotnet build` should build the PascalABC.NET project; see the output in the `bin` directory.

`dotnet run` may be used to run the project.

See the `PascalABC.NET.SDK.Demo` project for details.

### Items

In a project, there should be exactly one `<MainCompile>` item, which will be the main module passed to the compiler.

Other (non-main) `.pas` files may be added as `<Compile>` items, which are only used to track incremental compilation dependencies.

### References

All the assembly references are resolved during build and written to a generated module `References_Generated` by default. This means that your main module should add `uses References_Generated` to access the referenced assemblies.

### Properties

You can change the following properties in your project file to customize the SDK behavior.

<!-- Sdk.props -->
- `PascalABCNETCompilerPackageName`: the name of the compiler package that will be downloaded from NuGet, `FVNever.PascalABC.NET.Compiler` by default.
- `PascalABCNETCompilerPackageVersion`: the version of the compiler package to use; may be updated if a new version of the SDK is published.
- `SkipPascalABCNETCompilerInstallation`: set to `true` to skip the compiler installation by the SDK (if you want to get the compiler yourself via other means).

<!-- Sdk.targets -->
- `DebugMode`: if `true`, then the compiler will generate debug information and disable the optimizations. Enabled by default for the `Debug` configuration.
- `PascalAbcCompilerCommand`: command to run the compiler; just the path to the packaged compiler executable by default.
- `SkipGenerateReferenceFile`: if you don't want the SDK to generate the reference module.

[Common MSBuild properties][msbuild.common-properties] are supported as well.

Development
-----------

To run the unit testing suite, first publish the development packages using this shell command ([PowerShell][powershell] is required):

```console
$ pwsh ./scripts/build-packages.ps1
```

To execute the tests, run the following shell command:

```console
$ dotnet test PascalABC.NET.SDK.Tests
```

Documentation
-------------

- [Changelog][docs.changelog]
- [License (MIT)][docs.license]
- [Maintainership][docs.maintainership]

[andivionian-status-classifier]: https://github.com/ForNeVeR/andivionian-status-classifier#status-enfer-
[docs.changelog]: CHANGELOG.md
[docs.license]: LICENSE.md
[docs.maintainership]: MAINTAINERSHIP.md
[dotnet.sdk]: https://dotnet.microsoft.com/en-us/download
[msbuild.common-properties]: https://learn.microsoft.com/en-us/visualstudio/msbuild/common-msbuild-project-properties?view=vs-2022
[nuget.compiler.package]: https://www.nuget.org/packages/FVNever.PascalABC.NET.Compiler/
[nuget.sdk.package]: https://www.nuget.org/packages/FVNever.PascalABC.NET.SDK/
[pascalabc.net.downloads]: http://pascalabc.net/en/download
[pascalabc.net]: http://pascalabc.net/en/
[powershell]: https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell?view=powershell-7.2
[status-enfer]: https://img.shields.io/badge/status-enfer-orange.svg
