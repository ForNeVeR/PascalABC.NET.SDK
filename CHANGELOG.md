Changelog
=========

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/), and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.0.1] - 2022-10-08
### Fixed
- Actual assemblies instead of reference ones are now passed to the compiler from the NuGet packages

## [2.0.0] - 2022-09-25
### Changed
- The `pabcnetcclear.exe` binary is now used for compilation, since it's more suited for command-line invocation.
- Multiple `<MainCompile>` items are no longer supported in one project.
- The default output assembly path is now in line with MSBuild SDK standards (`bin/<Configuration>/<TargetFramework>/<ProjectName>.exe`).
- A version 3.8.3.3178-preview.1 of the compiler is now used by default. It was published from a fork of the official repository, with better support for NuGet.

### Added
- New `DebugMode` property is supported and set by default for the `Debug` configuration. Before that, the debug mode was always enabled.
- [Common MSBuild properties](https://learn.microsoft.com/en-us/visualstudio/msbuild/common-msbuild-project-properties?view=vs-2022) are now supported.
- Assembly references support (`<Reference>`, `<PackageReference>`, `<ProjectReference>`).

## [1.0.0] - 2022-09-17
This is the initial release of the SDK. It allows invoking the compiler to build simple PascalABC.NET programs.

[1.0.0]: https://github.com/ForNeVeR/PascalABC.NET.SDK/releases/tag/v1.0.0
[2.0.0]: https://github.com/ForNeVeR/PascalABC.NET.SDK/compare/v1.0.0...v2.0.0
[2.0.1]: https://github.com/ForNeVeR/PascalABC.NET.SDK/compare/v2.0.0...v2.0.1
[Unreleased]: https://github.com/ForNeVeR/PascalABC.NET.SDK/compare/v2.0.1...HEAD
