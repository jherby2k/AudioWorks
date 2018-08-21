param(
    [string] $ProjectDir,
    [string] $Configuration)

$outputDir = "$($ProjectDir)bin\$Configuration\AudioWorks.Commands"

"Clearing $outputDir..."

if (Test-Path $outputDir) { Remove-Item -Path $outputDir -Recurse -ErrorAction Stop }

"Publishing AudioWorks.Commands module to $outputDir."

dotnet publish --no-build -c $Configuration -o "$outputDir\netstandard2.0" -f netstandard2.0
dotnet publish --no-build -c $Configuration -o "$outputDir\netcoreapp2.1" -f netcoreapp2.1
Move-Item -Path "$outputDir\netstandard2.0\*" -Destination $outputDir -Include "*.psd1", "*.ps1xml", "*.dll-Help.xml"
Remove-Item -Path "$outputDir\*\*" -Recurse -Include "*.psd1", "*.ps1xml", "*.xml", "*.pdb", "*.deps.json"