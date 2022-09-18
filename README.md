PascalABC.NET SDK [![Status Enfer][status-enfer]][andivionian-status-classifier]
=================

This is an MSBuild SDK for [PascalABC.NET][pascalabc.net] programming language.

NuGet
-----

- `FVNever.PascalABC.NET.Compiler`: [![NuGet Package][nuget.compiler.badge]][nuget.compiler.package]
- `FVNever.PascalABC.NET.SDK`: [![NuGet Package][nuget.sdk.badge]][nuget.sdk.package]

Usage
-----

### Prerequisites

1. For now, only Windows is supported as the target platform.
2. Download [.NET 6 SDK][dotnet.sdk] or later.

### Quick Start

Prepare the following project file (`.pasproj` extension is recommended):

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

where `Hello.pas` is your main Pascal module.

After that, `dotnet build` should build the PascalABC.NET project; see the output in the `bin` directory.

See the `PascalABC.NET.SDK.Demo` project for details.

### Items

Every `<MainCompile>` item produces a separate assembly.

Other (non-main) `.pas` files may be added as `<Compile>` items, which are only used to track incremental compilation dependencies.

### Properties

You can change the following properties in your project file to customize the SDK behavior.

- `OutDir`: the directory for output assemblies, `$(MSBuildProjectDirectory)\bin\` by default
- `PascalABCNETCompilerPackageName`: the name of the compiler package that will be downloaded from NuGet, `FVNever.PascalABC.NET.Compiler` by default
- `PascalABCNETCompilerPackageVersion`: the version of the compiler package to use; may be updated if a new version of the SDK is published
- `SkipPascalABCNETCompilerInstallation`: set to `true` to skip the compiler installation by the SDK (if you want to get the compiler yourself via other means)

- `PascalAbcCompilerCommand`: command to run the compiler; just the path to the packaged compiler executable by default

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
[docs.license]: LICENSE.md
[docs.maintainership]: MAINTAINERSHIP.md
[dotnet.changelog]: CHANGELOG.md
[dotnet.sdk]: https://dotnet.microsoft.com/en-us/download
[nuget.compiler.badge]: https://img.shields.io/nuget/v/FVNever.PascalABC.NET.Compiler
[nuget.compiler.package]: https://www.nuget.org/packages/FVNever.PascalABC.NET.Compiler/
[nuget.sdk.badge]: https://img.shields.io/nuget/v/FVNever.PascalABC.NET.SDK
[nuget.sdk.package]: https://www.nuget.org/packages/FVNever.PascalABC.NET.SDK/
[pascalabc.net.downloads]: http://pascalabc.net/en/download
[pascalabc.net]: http://pascalabc.net/en/
[powershell]: https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell?view=powershell-7.2
[status-enfer]: https://img.shields.io/badge/status-enfer-orange.svg
