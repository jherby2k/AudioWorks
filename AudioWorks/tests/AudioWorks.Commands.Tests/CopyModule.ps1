param(
    [string] $Configuration,
    [string] $ModuleProjectRoot,
    [string] $OutputRoot)

"Copying module under $ModuleProjectRoot to $OutputRoot built using the $Configuration configuration."

$projectOutputDir = $ModuleProjectRoot | Get-ChildItem -Filter bin | Get-ChildItem -Filter $Configuration | Get-ChildItem -Filter "netstandard2.0"
Copy-Item -Path "$($projectOutputDir.FullName)\*.*" -Destination $OutputRoot