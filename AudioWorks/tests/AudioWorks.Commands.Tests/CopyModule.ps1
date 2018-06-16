param(
    [string] $Configuration,
	[string] $Framework,
    [string] $ModuleProjectRoot,
    [string] $OutputRoot)

"Copying module under $ModuleProjectRoot to $OutputRoot built using the $Configuration configuration."

$projectOutputDir = $ModuleProjectRoot | Get-ChildItem -Filter bin | Get-ChildItem -Filter $Configuration | Get-ChildItem -Filter $Framework
Copy-Item -Path "$($projectOutputDir.FullName)\*.*" -Destination $OutputRoot