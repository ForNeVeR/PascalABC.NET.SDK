PascalABC.NET.SDK Maintainership
================================

Publishing the Compiler Package
-------------------------------

1. Download the compiler artifact manually, check the actual version.
2. Update the `VersionPrefix` and the `CompilerSha256Hash` in the `PascalABC.NET.Compiler/PascalABC.NET.Compiler.proj`.
3. Check if [the license][license] for the compiler was updated; if so, then update the `LicenseCommitHash` and the metadata for every `LicenseArtifact` item in the `PascalABC.NET.Compiler/PascalABC.NET.Compiler.proj`.
4. Update the `_PascalABCNETCompilerPackageVersionPrefix` in the `PascalABC.NET.SDK/Sdk/Sdk.props`.
5. Create a PR with these changes to the `main` branch.
6. Ensure the CI works and merge the PR.
7. Trigger the [**Pack the Compiler** workflow][actions.compiler] on GitHub Actions. It will upload the package to nuget.org.

[actions.compiler]: https://github.com/ForNeVeR/PascalABC.NET.SDK/actions/workflows/compiler.yml
[license]: https://github.com/pascalabcnet/pascalabcnet/tree/HEAD/doc
