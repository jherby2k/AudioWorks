﻿<# Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT AN
WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. #>

param(
    [Parameter(Mandatory)]
    [ValidateSet("Debug", "Release")]
    [string] $Configuration,
    [Parameter(Mandatory)]
    [string] $ModuleProjectRoot,
    [Parameter(Mandatory)]
    [string] $OutputRoot)

$moduleName = "AudioWorks.Commands"

Write-Host "Copying module from $ModuleProjectRoot to $OutputRoot."

$outputDir = Join-Path $OutputRoot -ChildPath $moduleName
if (Test-Path $outputDir) { Remove-Item -Path $outputDir -Recurse -ErrorAction Stop }

$projectOutputDir = Join-Path -Path $ModuleProjectRoot -ChildPath bin | Join-Path -ChildPath $Configuration | Join-Path -ChildPath $moduleName
Copy-Item -Path $projectOutputDir -Destination $OutputRoot -Recurse

Write-Host "Copying debugging symbols from $ModuleProjectRoot to $OutputRoot."
Join-Path -Path $ModuleProjectRoot -ChildPath bin | Join-Path -ChildPath $Configuration | Get-ChildItem -Filter *.pdb -Recurse | % { Copy-Item $_.FullName -Destination $(Join-Path $outputDir -ChildPath $_.Directory.Name) }
