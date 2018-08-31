param(
    [string] $Configuration,
    [string] $ModuleProjectRoot,
    [string] $OutputRoot)

"Copying module under $ModuleProjectRoot to $OutputRoot built using the $Configuration configuration."

$outputDir = "$OutputRoot\AudioWorks.Commands"
if (Test-Path $outputDir) { Remove-Item -Path $outputDir -Recurse -ErrorAction Stop }

$projectOutputDir = $ModuleProjectRoot | Get-ChildItem -Filter bin | Get-ChildItem -Filter $Configuration | Get-ChildItem -Filter "AudioWorks.Commands"
Copy-Item -Path $projectOutputDir.FullName -Destination $OutputRoot -Recurse