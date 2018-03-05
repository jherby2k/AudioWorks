param(
    [string] $Configuration,
    [string] $ExtensionProjectRoot,
    [string] $ExtensionOutputRoot)

"Copying all extensions under $ExtensionProjectRoot to $ExtensionOutputRoot\Extensions built using the $Configuration configuration."

$outputDir = New-Item -Name Extensions -Path $ExtensionOutputRoot -ItemType Directory -Force
foreach ($projectDir in Get-ChildItem $ExtensionProjectRoot -Directory)
{
    $projectOutputDir = $projectDir | Get-ChildItem -Filter bin | Get-ChildItem -Filter $Configuration | Get-ChildItem -Filter "netstandard2.0"
    $extensionName = $projectDir.Name -replace "AudioWorks.Extensions.", ""
    Copy-Item -Path $projectOutputDir.FullName -Destination "$outputDir\$extensionName" -Recurse
}