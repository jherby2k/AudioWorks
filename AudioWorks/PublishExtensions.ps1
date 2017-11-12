param(
    [string] $Configuration,
    [string] $ExtensionProjectRoot,
    [string] $ExtensionOutputRoot)

"Publishing all extensions under $ExtensionProjectRoot to $ExtensionOutputRoot using the $Configuration configuration."

$outputDir = New-Item $ExtensionOutputRoot -ItemType Directory -Force
foreach ($projectDir in Get-ChildItem $ExtensionProjectRoot -Directory)
{
    $extensionName = $projectDir.Name -replace "AudioWorks.Extensions.", ""
    $projectOutputDir = $outputDir.CreateSubdirectory($extensionName)
    Start-Process "dotnet" -ArgumentList "publish -v quiet -c $Configuration -o ""$($projectOutputDir.FullName)""" -WorkingDirectory $projectDir.FullName -Wait -WindowStyle Hidden
}