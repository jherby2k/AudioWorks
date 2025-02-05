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
    [string] $Framework,
    [Parameter(Mandatory)]
    [string] $Destination)


Write-Host "Clearing $Destination..."

if (Test-Path $Destination) { Remove-Item -Path $Destination -Recurse -ErrorAction Stop }

Write-Host "Publishing $Framework PowerShell module to $Destination..."

dotnet publish "$ProjectDir" --no-build -c $Configuration -f $Framework -o "$(Join-Path -Path $Destination -ChildPath $Framework)"

Write-Host "Publishing extensions to $Destination..."

foreach ($dir in Get-ChildItem -Path $(Split-Path -Path $ProjectDir -Parent | Join-Path -ChildPath Extensions) -Directory) {
    dotnet publish "$($dir.FullName)" --no-build -c $Configuration -f $Framework -o "$(Join-Path -Path $Destination -ChildPath $Framework)"
}

Copy-Item -Path $(Join-Path -Path $Destination -ChildPath $Framework | Join-Path -ChildPath *) -Destination $Destination -Include "*.psd1", "*.psm1", "*.ps1xml", "COPYING" -ErrorAction Stop
Remove-Item -Path $(Join-Path -Path $Destination -ChildPath $Framework | Join-Path -ChildPath *) -Include "*.psd1", "*.psm1", "*.ps1xml", "*.xml", "*.pdb", "*.deps.json", "COPYING", "Icon.png" -ErrorAction Stop

Write-Host "Generating help file..."

Install-PackageProvider -Name NuGet -Scope CurrentUser -Force -ErrorAction SilentlyContinue
Install-Module -Name platyPS -Scope CurrentUser -Force -ErrorAction SilentlyContinue
Import-Module platyPS -ErrorAction Stop
New-ExternalHelp -Path $(Join-Path -Path $ProjectDir -ChildPath docs) -OutputPath $(Join-Path -Path $Destination -ChildPath en-US) -Force -ErrorAction Stop
