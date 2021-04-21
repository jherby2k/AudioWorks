<h1 align="center"><img src="https://github.com/jherby2k/AudioWorks/raw/main/Logo.png" width="567" /></h1>

A cross-platform, multi-format audio conversion and tagging suite for .NET and PowerShell. Formats currently supported are MP3, MP4 AAC, FLAC, Apple Lossless (ALAC), Opus, Ogg Vorbis and Wave.

A full user interface (Windows only) [is in development](https://github.com/jherby2k/AudioWorks.UI).

This project supplants [PowerShell Audio](https://github.com/jherby2k/PowerShellAudio) by targetting PowerShell cross-platform (Windows, Linux and MacOS).

Platform | Build Status (master) | Build Status (dev) | Deployment Status
-- | -- | -- | --
Windows | [![Build Status](https://dev.azure.com/jherby2k/AudioWorks/_apis/build/status/AudioWorks%20for%20Windows?branchName=main)](https://dev.azure.com/jherby2k/AudioWorks/_build?definitionId=2&_a=summary&repositoryFilter=1&branchFilter=3) [![Code Coverage](https://img.shields.io/azure-devops/coverage/jherby2k/AudioWorks/2/main)](https://dev.azure.com/jherby2k/AudioWorks/_build?definitionId=2&_a=summary&repositoryFilter=1&branchFilter=3) | [![Build Status](https://dev.azure.com/jherby2k/AudioWorks/_apis/build/status/AudioWorks%20for%20Windows?branchName=dev)](https://dev.azure.com/jherby2k/AudioWorks/_build?definitionId=2&_a=summary&repositoryFilter=1&branchFilter=4) [![Code Coverage](https://img.shields.io/azure-devops/coverage/jherby2k/AudioWorks/2/dev)](https://dev.azure.com/jherby2k/AudioWorks/_build?definitionId=2&_a=summary&repositoryFilter=1&branchFilter=4) | ![Deployment Status](https://vsrm.dev.azure.com/jherby2k/_apis/public/Release/badge/ce2541e1-667c-4be1-a926-7d44ff89db07/2/2)
Linux | [![Build Status](https://dev.azure.com/jherby2k/AudioWorks/_apis/build/status/AudioWorks%20for%20Linux?branchName=main)](https://dev.azure.com/jherby2k/AudioWorks/_build?definitionId=3&_a=summary&repositoryFilter=1&branchFilter=3) [![Code Coverage](https://img.shields.io/azure-devops/coverage/jherby2k/AudioWorks/3/main)](https://dev.azure.com/jherby2k/AudioWorks/_build?definitionId=3&_a=summary&repositoryFilter=1&branchFilter=3) | [![Build Status](https://dev.azure.com/jherby2k/AudioWorks/_apis/build/status/AudioWorks%20for%20Linux?branchName=dev)](https://dev.azure.com/jherby2k/AudioWorks/_build?definitionId=3&_a=summary&repositoryFilter=1&branchFilter=4) [![Code Coverage](https://img.shields.io/azure-devops/coverage/jherby2k/AudioWorks/3/dev)](https://dev.azure.com/jherby2k/AudioWorks/_build?definitionId=3&_a=summary&repositoryFilter=1&branchFilter=4) | ![Deployment Status](https://vsrm.dev.azure.com/jherby2k/_apis/public/Release/badge/ce2541e1-667c-4be1-a926-7d44ff89db07/4/4)
MacOS | [![Build Status](https://dev.azure.com/jherby2k/AudioWorks/_apis/build/status/AudioWorks%20for%20MacOS?branchName=main)](https://dev.azure.com/jherby2k/AudioWorks/_build?definitionId=4&_a=summary&repositoryFilter=1&branchFilter=3) [![Code Coverage](https://img.shields.io/azure-devops/coverage/jherby2k/AudioWorks/4/main)](https://dev.azure.com/jherby2k/AudioWorks/_build?definitionId=4&_a=summary&repositoryFilter=1&branchFilter=3) | [![Build Status](https://dev.azure.com/jherby2k/AudioWorks/_apis/build/status/AudioWorks%20for%20MacOS?branchName=dev)](https://dev.azure.com/jherby2k/AudioWorks/_build?definitionId=4&_a=summary&repositoryFilter=1&branchFilter=4) [![Code Coverage](https://img.shields.io/azure-devops/coverage/jherby2k/AudioWorks/4/dev)](https://dev.azure.com/jherby2k/AudioWorks/_build?definitionId=4&_a=summary&repositoryFilter=1&branchFilter=4) | ![Deployment Status](https://vsrm.dev.azure.com/jherby2k/_apis/public/Release/badge/ce2541e1-667c-4be1-a926-7d44ff89db07/3/3)

### System Requirements
AudioWorks runs on Windows 8.1+, MacOS 10.13+, and Ubuntu 16.04+. Other 64-bit Linux distributions may work, but may require additional dependencies and are currently untested. Currently, most extensions only support the Intel (x86) architecture. Apple M1 support will follow once .NET supports it.

#### On Windows:
* [PowerShell 7+](https://aka.ms/powershell) (recommended) and/or Windows PowerShell 5.1
* [.NET Framework 4.6.2+](https://dotnet.microsoft.com/download/dotnet-framework/net462) (if using Windows PowerShell)
* [iTunes](https://www.apple.com/itunes) (optional, for AAC and ALAC support. Requires the classic installer, not the Windows Store version)
#### On Ubuntu 20.04 or 18.04:
* Via `apt-get install`:
  * libebur128-1
#### On Ubuntu 16.04:
* Via `apt-get install`:
  * libmp3lame0
  * libebur128-1

### Installation
The AudioWorks PowerShell module can found on [the PowerShell Gallery](https://www.powershellgallery.com/packages/AudioWorks.Commands).

Keep the module up to date with `Update-Module -Name AudioWorks.Commands`.

The .NET API is available via [NuGet](https://www.nuget.org/packages/AudioWorks.Api).

### Additional Requirements for Building / Testing
* [.NET 5.0 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
* [.NET Core 2.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/2.1)
* [PowerShell 7+](https://aka.ms/powershell) (recommended) and/or Windows PowerShell 5.1.
* Windows 8.1+, MacOS 10.13+, Ubuntu 16.04, 18.04 or 20.04.
* [NuGet client tools](https://docs.microsoft.com/en-us/nuget/install-nuget-client-tools#nugetexe-cli) (for building extensions on MacOS and Linux)
* [Visual Studio 2019 16.8+](https://visualstudio.microsoft.com/downloads) (optional - Windows only)
* [Visual Studio 2019 for Mac 8.8+](https://visualstudio.microsoft.com/downloads) (optional - Mac only)
* [Visual Studio Code](https://code.visualstudio.com/) (optional - all platforms)
* [ReSharper 2020.3+](https://www.jetbrains.com/resharper) (optional - Windows only)

### Special Thanks
This project wouldn't be possible without the work of these other fine projects and organizations:
* [Hydrogen Audio Forums](https://hydrogenaud.io/), a wonderful, scientifically-minded community of digital audio enthusiasts.
* [The LAME Project](http://lame.sourceforge.net/), maintainers of the high-quality MP3 encoder.
* [The Xiph.Org Foundation](https://xiph.org/), maintainers of Ogg Vorbis and FLAC (the Free Lossless Audio Codec).
* [libebur128](https://github.com/jiixyj/libebur128), a library implementing the EBU R.128 loudness standard.
* [QAAC](https://sites.google.com/site/qaacpage/), a command-line front-end for Apple's AAC and Apple Lossless encoders.
