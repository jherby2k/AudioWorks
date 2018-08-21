param(
    [string] $ProjectDir,
    [string] $Configuration,
    [string] $Framework)

$outputRoot = "$($ProjectDir)bin\$Configuration\AudioWorks.Commands"
$outputDir = "$outputRoot\$Framework"

"Clearing $outputDir..."

if (Test-Path $outputDir) { Remove-Item -Path $outputDir -Recurse -ErrorAction Stop }

"Publishing $Framework PowerShell module to $outputDir."

dotnet publish --no-build -c $Configuration -o "$outputDir" -f $Framework
Copy-Item -Path "$outputDir\*" -Destination $outputRoot -Include "*.psd1", "*.ps1xml", "*.dll-Help.xml"
Remove-Item -Path "$outputDir\*" -Recurse -Include "*.psd1", "*.ps1xml", "*.xml", "*.pdb", "*.deps.json"