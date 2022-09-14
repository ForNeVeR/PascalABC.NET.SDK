param (
    $SourceDirectory = "$PSScriptRoot/../",
    $PackageStore = "$SourceDirectory/nupkg",
    $PackagesLocation = "$SourceDirectory/packages",
    [switch] $Clean
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

if ($Clean -and (Test-Path $PackagesLocation)) {
    Remove-Item -Recurse $PackagesLocation
}

dotnet pack $SourceDirectory/PascalABC.NET.Compiler --output $PackageStore -p:VersionSuffix=dev
if (!$?) { throw "dotnet pack exit code: $LASTEXITCODE" }

dotnet pack $SourceDirectory/PascalABC.NET.SDK --output $PackageStore -p:VersionSuffix=dev
if (!$?) { throw "dotnet pack exit code: $LASTEXITCODE" }
