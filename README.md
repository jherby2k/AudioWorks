<h1 align="center"><img src="https://github.com/jherby2k/AudioWorks/raw/master/Logo.png" width="567" /></h1>

A cross-platform, multi-format audio conversion and tagging suite for .NET and PowerShell. Formats currently supported are MP3, MP4 AAC, FLAC, Apple Lossless (ALAC), Opus, Ogg Vorbis and Wave.

A full user interface (Windows only) is in development.

This project supplants [PowerShell Audio](https://github.com/jherby2k/PowerShellAudio) by targetting both Windows PowerShell and PowerShell Core (Windows, Linux and MacOS).

Platform | Status (Master Branch)
-- | --
Windows | [![Build Status](https://dev.azure.com/jherby2k/AudioWorks/_apis/build/status/AudioWorks%20for%20Windows?branchName=master)](https://dev.azure.com/jherby2k/AudioWorks/_build/latest?definitionId=2?branchName=master) [![Code Coverage](https://img.shields.io/azure-devops/coverage/jherby2k/AudioWorks/2.svg)](https://dev.azure.com/jherby2k/AudioWorks/_build/latest?definitionId=2?branchName=master)
Linux | [![Build Status](https://dev.azure.com/jherby2k/AudioWorks/_apis/build/status/AudioWorks%20for%20Linux?branchName=master)](https://dev.azure.com/jherby2k/AudioWorks/_build/latest?definitionId=3?branchName=master) [![Code Coverage](https://img.shields.io/azure-devops/coverage/jherby2k/AudioWorks/3.svg)](https://dev.azure.com/jherby2k/AudioWorks/_build/latest?definitionId=3?branchName=master)
MacOS | [![Build Status](https://dev.azure.com/jherby2k/AudioWorks/_apis/build/status/AudioWorks%20for%20MacOS?branchName=master)](https://dev.azure.com/jherby2k/AudioWorks/_build/latest?definitionId=4?branchName=master) [![Code Coverage](https://img.shields.io/azure-devops/coverage/jherby2k/AudioWorks/4.svg)](https://dev.azure.com/jherby2k/AudioWorks/_build/latest?definitionId=4?branchName=master)

### System Requirements
AudioWorks runs on Windows 7+, MacOS 10.13+, and Ubuntu (18.04 or 16.04). Other 64-bit Linux distributions may work, but may require additional dependencies and are currently untested.

#### On Windows:
* [PowerShell Core 6.1+](https://aka.ms/powershell) (The PowerShell 7 preview is recommended for maximum performance) and/or Windows PowerShell 5.1
* [.NET Framework 4.7.1+](https://support.microsoft.com/en-us/help/4054530/microsoft-net-framework-4-7-2-offline-installer-for-windows) (if using Windows PowerShell)
* [iTunes](https://www.apple.com/itunes) (optional, for AAC and ALAC support. Requires the classic installer, not the Windows Store version. Technically only the Apple Application support packages are required)
#### On MacOS:
* [PowerShell Core 6.1+](https://aka.ms/powershell) (The PowerShell 7 preview is recommended for maximum performance)
#### On Ubuntu 18.04:
* [PowerShell Core 6.1+](https://aka.ms/powershell) (The PowerShell 7 preview is recommended for maximum performance)
* Via `apt-get install`:
  * libebur128-1
#### On Ubuntu 16.04:
* [PowerShell Core 6.1+](https://aka.ms/powershell) (The PowerShell 7 preview is recommended for maximum performance)
* Via `apt-get install`:
  * libmp3lame0
  * libebur128-1

### Installation
The AudioWorks PowerShell module can found on [the PowerShell Gallery](https://www.powershellgallery.com/packages/AudioWorks.Commands). If you are using Windows PowerShell 5.1, you may need to [update PowerShellGet](https://docs.microsoft.com/en-us/powershell/gallery/installing-psget) to a version that supports pre-release modules.

Keep the module up to date with `Update-Module -Name AudioWorks.Commands -AllowPrerelease`.

The .NET API is available via [NuGet](https://www.nuget.org/packages/AudioWorks.Api).

### Additional Requirements for Building / Testing
* [.NET Core SDK 3.0.100+](https://dotnet.github.io/)
* Windows 7+, MacOS 10.13+, Ubuntu 16.04 or Ubuntu 18.04.
* [Visual Studio 2019 16.3](https://visualstudio.microsoft.com/downloads) (optional - Windows only)
* [Visual Studio 2019 for Mac 8.3+](https://visualstudio.microsoft.com/downloads) (optional - Mac only)
* [Visual Studio Code](https://code.visualstudio.com/) (optional - all platforms)
* [ReSharper 2019.2+](https://www.jetbrains.com/resharper) (optional - Windows only)

### Special Thanks
This project wouldn't be possible without the work of these other fine projects and organizations:
* [Hydrogen Audio Forums](https://hydrogenaud.io/), a wonderful, scientifically-minded community of digital audio enthusiasts.
* [The LAME Project](http://lame.sourceforge.net/), maintainers of the high-quality MP3 encoder.
* [The Xiph.Org Foundation](https://xiph.org/), maintainers of Ogg Vorbis and FLAC (the Free Lossless Audio Codec).
* [libebur128](https://github.com/jiixyj/libebur128), a library implementing the EBU R.128 loudness standard.
* [QAAC](https://sites.google.com/site/qaacpage/), a command-line front-end for Apple's AAC and Apple Lossless encoders.
