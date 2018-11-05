<# Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. #>

param(
    [string] $ProjectDir,
    [string] $Configuration,
    [string] $Framework)

$outputRoot = "$($ProjectDir)bin\$Configuration\AudioWorks.Commands"
$outputDir = "$outputRoot\$Framework"

Write-Host "Clearing $outputDir..."

if (Test-Path $outputDir) { Remove-Item -Path $outputDir -Recurse -ErrorAction Stop }

Write-Host "Publishing $Framework PowerShell module to $outputDir..."

dotnet publish "$ProjectDir" --no-build -c $Configuration -o "$outputDir" -f $Framework
Copy-Item -Path "$outputDir\*" -Destination $outputRoot -Include "*.psd1", "*.ps1xml", "COPYING", "COPYING.LESSER" -ErrorAction Stop
Remove-Item -Path "$outputDir\*" -Recurse -Include "*.psd1", "*.ps1xml", "*.xml", "*.pdb", "*.deps.json", "COPYING", "COPYING.LESSER" -ErrorAction Stop

Write-Host "Generating help file..."

Install-Module -Name platyPS -Scope CurrentUser -Force -ErrorAction Stop
Import-Module platyPS -ErrorAction Stop
New-ExternalHelp -Path "$ProjectDir\docs" -OutputPath "$outputRoot\en-US" -Force -ErrorAction Stop