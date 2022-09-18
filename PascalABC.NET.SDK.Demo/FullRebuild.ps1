# This script will fully rebuild the SDK and the demo project.
param (
    $ProjectDirectory = $PSScriptRoot,
    $SolutionDirectory = "$ProjectDirectory/.."
)

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest

if ((Test-Path $ProjectDirectory/bin) -or (Test-Path $ProjectDirectory/obj)) {
    Remove-Item -Recurse $ProjectDirectory/bin, $ProjectDirectory/obj
}

if (Test-Path $SolutionDirectory/packages) {
    Remove-Item -Recurse $SolutionDirectory/packages
}

../scripts/build-packages.ps1
dotnet build -bl $ProjectDirectory/PascalABC.NET.SDK.Demo.pasproj
