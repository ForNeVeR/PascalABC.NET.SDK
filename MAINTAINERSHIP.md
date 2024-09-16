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
7. Check if the `fvnever.pascalabc.net.sdk.compiler.github` NuGet key is still valid (see the **Rotate NuGet Publishing Keys** section if it isn't).
8. Trigger the [**Pack the Compiler** workflow][actions.compiler] on GitHub Actions. It will upload the package to nuget.org.

[actions.compiler]: https://github.com/ForNeVeR/PascalABC.NET.SDK/actions/workflows/compiler.yml
[license]: https://github.com/pascalabcnet/pascalabcnet/tree/HEAD/doc

Publishing the SDK Package
--------------------------
1. Update the copyright year in the `LICENSE.md`, if required.
2. Choose a new version according to [Semantic Versioning][semver].
3. Update the package version in the following places:
    - `PascalABC.NET.SDK/PascalABC.NET.SDK.csproj` (the `VersionPrefix` element)
    - `README.md` (the example section, look for `Sdk=`)
    - `PascalABC.NET.SDK.Demo/PascalABC.NET.SDK.Demo.pasproj` (line 1)
4. Make sure there's a properly formed version entry in the `CHANGELOG.md` (often it can be created by renaming the **Unreleased** section).
5. Merge the changes to the `main` branch via a pull request.
6. Check if the `fvnever.pascalabc.net.sdk.github` NuGet key is still valid (see the **Rotate NuGet Publishing Keys** section if it isn't).
7. Push a tag named `v<VERSION>` to GitHub.

Rotate NuGet Publishing Keys
----------------------------
CI relies on NuGet API key being added to the secrets. From time to time, this key requires maintenance: it will become obsolete and will have to be updated.

To update the key:

1. Sign in onto nuget.org.
2. Go to the [API keys][nuget.api-keys] section.
3. Update the existing or create new keys with the following parameters:
    - `fvnever.pascalabc.net.sdk.compiler.github` with permission to **Push only new package versions** and only allowed to publish the package **FVNever.PascalABC.NET.Compiler**,
    - `fvnever.pascalabc.net.sdk.github` with permission to **Push only new package versions** and only allowed to publish the package **FVNever.PascalABC.NET.SDK**.


    (If this is the first publication of a new package, upload temporary short-living keys with permission to add new packages, and rotate them afterward.)
1. Paste the generated keys to the variables, correspondingly, `COMPILER_NUGET_TOKEN` and `SDK_NUGET_TOKEN` on the [action secrets][github.secrets] section of GitHub settings.

[github.secrets]: https://github.com/ForNeVeR/PascalABC.NET.SDK/settings/secrets/actions
[nuget.api-keys]: https://www.nuget.org/account/apikeys
[semver]: https://semver.org/spec/v2.0.0.html
