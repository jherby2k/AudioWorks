<h1 align="center"><img src="https://github.com/jherby2k/AudioWorks/raw/main/Logo.png" width="567" /></h1>

A cross-platform, multi-format audio conversion and tagging suite for .NET and PowerShell. Formats currently supported are MP3, MP4 AAC, FLAC, Apple Lossless (ALAC), Opus, Ogg Vorbis and Wave.

Platform | Build Status (main) | Build Status (dev) | Deployment Status
-- | -- | -- | --
Windows | [![Build Status](https://dev.azure.com/jherby2k/AudioWorks/_apis/build/status/AudioWorks%20for%20Windows?branchName=main)](https://dev.azure.com/jherby2k/AudioWorks/_build?definitionId=2&_a=summary&repositoryFilter=1&branchFilter=3) [![Code Coverage](https://img.shields.io/azure-devops/coverage/jherby2k/AudioWorks/2/main)](https://dev.azure.com/jherby2k/AudioWorks/_build?definitionId=2&_a=summary&repositoryFilter=1&branchFilter=3) | [![Build Status](https://dev.azure.com/jherby2k/AudioWorks/_apis/build/status/AudioWorks%20for%20Windows?branchName=dev)](https://dev.azure.com/jherby2k/AudioWorks/_build?definitionId=2&_a=summary&repositoryFilter=1&branchFilter=4) [![Code Coverage](https://img.shields.io/azure-devops/coverage/jherby2k/AudioWorks/2/dev)](https://dev.azure.com/jherby2k/AudioWorks/_build?definitionId=2&_a=summary&repositoryFilter=1&branchFilter=4) | ![Deployment Status](https://vsrm.dev.azure.com/jherby2k/_apis/public/Release/badge/ce2541e1-667c-4be1-a926-7d44ff89db07/2/2)
Linux | [![Build Status](https://dev.azure.com/jherby2k/AudioWorks/_apis/build/status/AudioWorks%20for%20Linux?branchName=main)](https://dev.azure.com/jherby2k/AudioWorks/_build?definitionId=3&_a=summary&repositoryFilter=1&branchFilter=3) [![Code Coverage](https://img.shields.io/azure-devops/coverage/jherby2k/AudioWorks/3/main)](https://dev.azure.com/jherby2k/AudioWorks/_build?definitionId=3&_a=summary&repositoryFilter=1&branchFilter=3) | [![Build Status](https://dev.azure.com/jherby2k/AudioWorks/_apis/build/status/AudioWorks%20for%20Linux?branchName=dev)](https://dev.azure.com/jherby2k/AudioWorks/_build?definitionId=3&_a=summary&repositoryFilter=1&branchFilter=4) [![Code Coverage](https://img.shields.io/azure-devops/coverage/jherby2k/AudioWorks/3/dev)](https://dev.azure.com/jherby2k/AudioWorks/_build?definitionId=3&_a=summary&repositoryFilter=1&branchFilter=4) | ![Deployment Status](https://vsrm.dev.azure.com/jherby2k/_apis/public/Release/badge/ce2541e1-667c-4be1-a926-7d44ff89db07/4/4)
MacOS | [![Build Status](https://dev.azure.com/jherby2k/AudioWorks/_apis/build/status/AudioWorks%20for%20MacOS?branchName=main)](https://dev.azure.com/jherby2k/AudioWorks/_build?definitionId=4&_a=summary&repositoryFilter=1&branchFilter=3) [![Code Coverage](https://img.shields.io/azure-devops/coverage/jherby2k/AudioWorks/4/main)](https://dev.azure.com/jherby2k/AudioWorks/_build?definitionId=4&_a=summary&repositoryFilter=1&branchFilter=3) | [![Build Status](https://dev.azure.com/jherby2k/AudioWorks/_apis/build/status/AudioWorks%20for%20MacOS?branchName=dev)](https://dev.azure.com/jherby2k/AudioWorks/_build?definitionId=4&_a=summary&repositoryFilter=1&branchFilter=4) [![Code Coverage](https://img.shields.io/azure-devops/coverage/jherby2k/AudioWorks/4/dev)](https://dev.azure.com/jherby2k/AudioWorks/_build?definitionId=4&_a=summary&repositoryFilter=1&branchFilter=4) | ![Deployment Status](https://vsrm.dev.azure.com/jherby2k/_apis/public/Release/badge/ce2541e1-667c-4be1-a926-7d44ff89db07/3/3)

### System Requirements
AudioWorks runs on Windows, MacOS, and Linux.
 You will need [PowerShell 7.4+](https://aka.ms/powershell). "Windows PowerShell" (aka PowerShell 5.1) which is included with Windows, is no longer supported.
 
#### On Windows:
* [iTunes](https://www.apple.com/itunes) (optional, for AAC and Apple Lossless (ALAC) support. Technically only CoreAudioTools.dll is required)

#### On Ubuntu Linux (other distros untested):
* libebur128  (`apt install libebur128-1`)

### Installation
The AudioWorks PowerShell module can found on [the PowerShell Gallery](https://www.powershellgallery.com/packages/AudioWorks.Commands).

Keep the module up to date with `Update-Module -Name AudioWorks.Commands`.

The .NET API is available via [NuGet](https://www.nuget.org/packages/AudioWorks.Api).

### Additional Requirements for Building / Testing
* [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* [Visual Studio 2022](https://visualstudio.microsoft.com/downloads) (optional - Windows or Mac)
* [Visual Studio Code](https://code.visualstudio.com/) (optional - all platforms)
* [ReSharper 2024+](https://www.jetbrains.com/resharper) (optional - Windows only)

### Special Thanks
This project wouldn't be possible without the work of these other fine projects and organizations:
* [Hydrogen Audio Forums](https://hydrogenaud.io/), a wonderful, scientifically-minded community of digital audio enthusiasts.
* [The LAME Project](http://lame.sourceforge.net/), maintainers of the high-quality MP3 encoder.
* [The Xiph.Org Foundation](https://xiph.org/), maintainers of Ogg Vorbis and FLAC (the Free Lossless Audio Codec).
* [libebur128](https://github.com/jiixyj/libebur128), a library implementing the EBU R.128 loudness standard.
* [QAAC](https://sites.google.com/site/qaacpage/), a command-line front-end for Apple's AAC and Apple Lossless encoders.
