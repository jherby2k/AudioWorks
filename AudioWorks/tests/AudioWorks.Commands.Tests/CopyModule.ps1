<# Copyright © 2018 Jeremy Herbison

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
    [string] $ArtifactRoot,
    [Parameter(Mandatory)]
    [string] $OutputRoot)

$moduleName = "AudioWorks.Commands"
$packageRoot = Join-Path -Path $ArtifactRoot -ChildPath "package" | Join-Path -ChildPath $Configuration.ToLower()
$binDir = Join-Path -Path $ArtifactRoot -ChildPath "bin" | Join-Path -ChildPath $moduleName | Join-Path -ChildPath $Configuration.ToLower()

$outputDir = Join-Path $OutputRoot -ChildPath $moduleName
if (Test-Path $outputDir) { Remove-Item -Path $outputDir -Recurse -ErrorAction Stop }

Write-Host "Copying module from $packageRoot to $outputDir."
Copy-Item -Path $(Join-Path $packageRoot -ChildPath $moduleName) -Destination $outputDir -Recurse

Write-Host "Copying debugging symbols from $binDir to $outputDir."
Get-ChildItem -Path $binDir -Filter *.pdb -Recurse | % { Copy-Item $_.FullName -Destination $(Join-Path $outputDir -ChildPath "net8.0") }
