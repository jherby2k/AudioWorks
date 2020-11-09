<# Copyright © 2020 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. #>

param(
    [Parameter(Mandatory)]
    [string] $ProjectName,
    [Parameter(Mandatory)]
    [ValidateSet("Debug", "Release")]
    [string] $Configuration)

# For Windows PowerShell compatibility
if ($PSEdition -eq "Desktop") {
    $IsWindows = $true
}

$nugetPath = Join-Path -Path $([System.IO.Path]::GetTempPath()) -ChildPath nuget.exe
if (-not (Test-Path $nugetPath)) {
    Invoke-WebRequest -Uri https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -OutFile $nugetPath
}

$localAppDataDir = Join-Path -Path $([System.Environment]::GetFolderPath(28)) -ChildPath AudioWorks

$localFeedDir = Join-Path -Path $localAppDataDir -ChildPath LocalFeed
if (-not (Test-Path $localFeedDir)) {
    New-Item -Path $localFeedDir -ItemType Directory | Out-Null
}

Get-ChildItem -Path $localFeedDir -Filter $ProjectName* -Directory | Remove-Item  -Recurse
Get-ChildItem -Path $(Join-Path -Path $localAppDataDir -ChildPath Extensions) -Filter $ProjectName* -Directory -Recurse | Remove-Item -Recurse

foreach ($package in Get-ChildItem -Path $(Join-Path -Path $PSScriptRoot -ChildPath $ProjectName | Join-Path -ChildPath bin | Join-Path -ChildPath $Configuration) -Filter *.nupkg | Select-Object -ExpandProperty FullName) {
    if ($IsWindows) { &$nugetPath add $package -Source $localFeedDir -Expand -NonInteractive }
    else { mono $nugetPath add $package -Source $localFeedDir -Expand -NonInteractive }
}