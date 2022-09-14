param (
    $SourceDirectory = "$PSScriptRoot/../",
    $PackageLocation = "$SourceDirectory/nupkg"
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

dotnet pack $SourceDirectory/PascalABC.NET.Compiler --output $PackageLocation -p:VersionSuffix=dev
if (!$?) { throw "dotnet pack exit code: $LASTEXITCODE" }

dotnet pack $SourceDirectory/PascalABC.NET.SDK --output $PackageLocation -p:VersionSuffix=dev
if (!$?) { throw "dotnet pack exit code: $LASTEXITCODE" }
