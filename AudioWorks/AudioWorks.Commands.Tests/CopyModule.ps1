param(
    [string] $Configuration,
    [string] $ModuleProjectRoot,
    [string] $ExtensionProjectRoot,
    [string] $OutputRoot)

"Copying module under $ModuleProjectRoot to $OutputRoot built using the $Configuration configuration."

$projectOutputDir = $ModuleProjectRoot | Get-ChildItem -Filter bin | Get-ChildItem -Filter $Configuration | Get-ChildItem -Filter "netstandard2.0"
Copy-Item -Path "$($projectOutputDir.FullName)\*.*" -Destination $OutputRoot

"Copying all extensions under $ExtensionProjectRoot to $OutputRoot\Module\Extensions built using the $Configuration configuration."

$outputDir = New-Item -Name Extensions -Path $OutputRoot -ItemType Directory -Force
$excludeFiles = Get-Item -Path $OutputRoot | Get-ChildItem -File

foreach ($projectDir in Get-ChildItem $ExtensionProjectRoot -Directory)
{
    $projectOutputDir = $projectDir | Get-ChildItem -Filter bin | Get-ChildItem -Filter $Configuration | Get-ChildItem -Filter "netstandard2.0"
    $extensionName = $projectDir.Name -replace "AudioWorks.Extensions.", ""
    Remove-Item -Path "$outputDir\$extensionName" -Recurse | Out-Null
    Copy-Item -Path $projectOutputDir.FullName -Destination "$outputDir\$extensionName" -Recurse -Exclude $excludeFiles
}