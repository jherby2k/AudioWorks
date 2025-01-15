<# Copyright © 2018 Jeremy Herbison

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
    [string] $ProjectDir,
    [Parameter(Mandatory)]
    [ValidateSet("Debug", "Release")]
    [string] $Configuration,
    [Parameter(Mandatory)]
    [string] $Framework)

$outputRoot = Join-Path -Path $ProjectDir -ChildPath bin | Join-Path -ChildPath $Configuration | Join-Path -ChildPath AudioWorks.Commands
$outputDir = Join-Path -Path $outputRoot -ChildPath $Framework

Write-Host "Clearing $outputDir..."

if (Test-Path $outputDir) { Remove-Item -Path $outputDir -Recurse -ErrorAction Stop }

# Workaround for non-referenced extension content:
$apiProjectDir = Split-Path -Path $ProjectDir -Parent | Join-Path -ChildPath AudioWorks.Api
dotnet publish "$apiProjectDir" --no-build -c $Configuration -o "$outputDir" -f $Framework

Write-Host "Publishing $Framework PowerShell module to $outputDir..."

dotnet publish "$ProjectDir" --no-build -c $Configuration -o "$outputDir" -f $Framework
Copy-Item -Path $(Join-Path -Path $outputDir -ChildPath *) -Destination $outputRoot -Include "*.psd1", "*.psm1", "*.ps1xml", "COPYING" -ErrorAction Stop
Remove-Item -Path $(Join-Path -Path $outputDir -ChildPath *) -Recurse -Include "*.psd1", "*.psm1", "*.ps1xml", "*.xml", "*.pdb", "*.deps.json", "COPYING", "Icon.png" -ErrorAction Stop

Write-Host "Generating help file..."

Install-PackageProvider -Name NuGet -Scope CurrentUser -Force -ErrorAction SilentlyContinue
Install-Module -Name platyPS -Scope CurrentUser -Force -ErrorAction SilentlyContinue
Import-Module platyPS -ErrorAction Stop
New-ExternalHelp -Path $(Join-Path -Path $ProjectDir -ChildPath docs) -OutputPath $(Join-Path -Path $outputRoot -ChildPath en-US) -Force -ErrorAction Stop
