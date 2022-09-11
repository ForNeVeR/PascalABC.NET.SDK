PascalABC.NET SDK [![Status Umbra][status-umbra]][andivionian-status-classifier]
======

This is an MSBuild SDK for [PascalABC.NET][pascalabc.net] programming language.

Usage
-----

### Prerequisites

1. For now, only Windows is supported as the target platform.
2. Download [.NET 6 SDK][dotnet.sdk] or later.
3. Download [PascalABC.NET console compiler][pascalabc.net.downloads] (`PABCNET.ZIP`), extract it somewhere on your disk, and add the directory to the `PATH` environment variable.

### Quick Start

1. Download the SDK sources (i.e. the sources of this project).
2. Prepare the following project file (`.pasproj`):

   ```xml
   <Project xmlns="http://schemas.microsoft.com/developer/msbuild/2003" ToolsVersion="Current">

       <Import Project="$(MSBuildThisFileDirectory)..\PascalABC.NET.SDK\Sdk\Sdk.props" />

       <ItemGroup>
           <MainCompile Include="Hello1.pas" />
           <MainCompile Include="Hello2.pas" />
       </ItemGroup>

       <Import Project="$(MSBuildThisFileDirectory)..\PascalABC.NET.SDK\Sdk\Sdk.targets" />

   </Project>
   ```

   where `$(MSBuildThisFileDirectory)..` is a path to the SDK base directory (the directory this `README.md` file resides).

After that, `dotnet build` should build the PascalABC.NET project.

See the `PascalABC.NET.SDK.Demo` project for details.

### Items

Every `<MainCompile>` item produces a separate assembly.

Other (non-main) `.pas` files may be added as `<Compile>` items, which are only used to track incremental compilation dependencies.

### Properties

- `OutDir`: the directory for output assemblies, `$(MSBuildProjectDirectory)\bin\` by default
- `PascalAbcCompilerAssembly`: path to the compiler assembly, `pabcnetc.exe` by default
- `PascalAbcCompilerCommand`: command to run the compiler, `$(PascalAbcCompilerAssembly)` by default

Development
-----------

To run the unit testing suite, run the following command in your shell:

```console
$ dotnet test PascalABC.NET.SDK.Tests
```

Documentation
-------------

- [License (MIT)][docs.license]

[andivionian-status-classifier]: https://github.com/ForNeVeR/andivionian-status-classifier#status-umbra-
[docs.license]: LICENSE.md
[dotnet.sdk]: https://dotnet.microsoft.com/en-us/download
[pascalabc.net.downloads]: http://pascalabc.net/en/download
[pascalabc.net]: http://pascalabc.net/en/
[status-umbra]: https://img.shields.io/badge/status-umbra-red.svg
