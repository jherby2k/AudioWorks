#
# Script module for module 'AudioWorks.Commands'
#
Set-StrictMode -Version Latest

# Set up some helper variables to make it easier to work with the module
$PSModule = $ExecutionContext.SessionState.Module
$PSModuleRoot = $PSModule.ModuleBase

# Import the appropriate nested binary module based on the current PowerShell version
$binaryModuleRoot = $PSModuleRoot

if ($PSEdition -eq 'Desktop') {
    $binaryModuleRoot = Join-Path -Path $PSModuleRoot -ChildPath 'net462'
}
elseif ($PSVersionTable.PSVersion.Major -lt 7) {
    $binaryModuleRoot = Join-Path -Path $PSModuleRoot -ChildPath 'netcoreapp2.1'
}
else {
    $binaryModuleRoot = Join-Path -Path $PSModuleRoot -ChildPath 'netcoreapp3.1'
}

$binaryModulePath = Join-Path -Path $binaryModuleRoot -ChildPath 'AudioWorks.Commands.dll'
$binaryModule = Import-Module -Name $binaryModulePath -PassThru

# When the module is unloaded, remove the nested binary module that was loaded with it
$PSModule.OnRemove = {
    Remove-Module -ModuleInfo $binaryModule
}
