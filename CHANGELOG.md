﻿Changelog
=========

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/), and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased] (2.0.0)
### Changed
- The `pabcnetcclear.exe` binary is now used for compilation, since it's more suited for command-line invocation.
- Multiple `<MainCompile>` items are no longer supported in one project.
- The default output assembly path is now in line with MSBuild SDK standards (`bin/<Configuration>/<TargetFramework>/<ProjectName>.exe`).

### Added
- New `DebugMode` property is supported and set by default for the `Debug` configuration. Before that, the debug mode was always enabled.
- [Common MSBuild properties](https://learn.microsoft.com/en-us/visualstudio/msbuild/common-msbuild-project-properties?view=vs-2022) are now supported.
- Assembly references support (`<Reference>`, `<PackageReference>`, `<ProjectReference>`).

## [1.0.0] - 2022-09-17
This is the initial release of the SDK. It allows invoking the compiler to build simple PascalABC.NET programs.

[1.0.0]: https://github.com/ForNeVeR/PascalABC.NET.SDK/releases/tag/v1.0.0
[Unreleased]: https://github.com/ForNeVeR/PascalABC.NET.SDK/compare/v1.0.0...HEAD