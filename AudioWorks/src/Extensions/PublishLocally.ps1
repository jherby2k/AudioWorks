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
if ($PSEdition -eq 'Desktop') {
    $IsWindows = $true
}

if ($IsWindows -and -not (Test-Path $env:TEMP\nuget.exe)) {
    Invoke-WebRequest -Uri https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -OutFile $env:TEMP\nuget.exe
}
if (-not $IsWindows -and -not (Test-Path /usr/local/bin/nuget)) {
    Invoke-WebRequest -Uri https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -OutFile /usr/local/bin/nuget
}

$localFeedDir = "$([System.Environment]::GetFolderPath(28))\AudioWorks\LocalFeed"
if (-not (Test-Path $localFeedDir)) {
    New-Item -Path $localFeedDir -ItemType Directory | Out-Null
}

Get-ChildItem -Path $localFeedDir -Filter "*$ProjectName*" -Directory | Remove-Item  -Recurse
Get-ChildItem -Path "$([System.Environment]::GetFolderPath(28))\AudioWorks\Extensions" -Recurse -Directory -Filter "*$ProjectName*" | Remove-Item -Recurse

if ($IsWindows) {
    Get-ChildItem -Path "$PSScriptRoot\$ProjectName\bin\$Configuration" -Filter *.nupkg | Select-Object -ExpandProperty FullName | % { &"$env:TEMP\nuget" add $_ -Source $localFeedDir -Expand -NonInteractive }
}
else {
    Get-ChildItem -Path "$PSScriptRoot/$ProjectName/bin/$Configuration" -Filter *.nupkg | Select-Object -ExpandProperty FullName | % { mono /user/local/bin/nuget add $_ -Source $localFeedDir -Expand -NonInteractive }
}