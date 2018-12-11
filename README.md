<h1 align="center"><img src="https://github.com/jherby2k/AudioWorks/raw/master/Logo.png" width="567" /></h1>

A cross-platform, multi-format audio conversion and tagging suite written in C# and featuring a PowerShell front-end. Codecs currently supported are MP3, MP4 AAC, FLAC, Apple Lossless (ALAC) and Ogg Vorbis.

This project supplants [PowerShell Audio](https://github.com/jherby2k/PowerShellAudio) by targetting both Windows PowerShell and PowerShell Core (Windows, Linux and Mac OSX).

Platform | Status (Master Branch)
-- | --
Windows | [![Build Status](https://dev.azure.com/jherby2k/AudioWorks/_apis/build/status/AudioWorks%20for%20Windows?branchName=master)](https://dev.azure.com/jherby2k/AudioWorks/_build/latest?definitionId=2?branchName=master) [![Code Coverage](https://img.shields.io/azure-devops/coverage/jherby2k/AudioWorks/2.svg)](https://dev.azure.com/jherby2k/AudioWorks/_build/latest?definitionId=2?branchName=master)
Linux | [![Build Status](https://dev.azure.com/jherby2k/AudioWorks/_apis/build/status/AudioWorks%20for%20Linux?branchName=master)](https://dev.azure.com/jherby2k/AudioWorks/_build/latest?definitionId=3?branchName=master) [![Code Coverage](https://img.shields.io/azure-devops/coverage/jherby2k/AudioWorks/3.svg)](https://dev.azure.com/jherby2k/AudioWorks/_build/latest?definitionId=3?branchName=master)
MacOS | [![Build Status](https://dev.azure.com/jherby2k/AudioWorks/_apis/build/status/AudioWorks%20for%20MacOS?branchName=master)](https://dev.azure.com/jherby2k/AudioWorks/_build/latest?definitionId=4?branchName=master) [![Code Coverage](https://img.shields.io/azure-devops/coverage/jherby2k/AudioWorks/4.svg)](https://dev.azure.com/jherby2k/AudioWorks/_build/latest?definitionId=4?branchName=master)

### System Requirements
AudioWorks runs on Windows 7+, OSX 10.12+, Ubuntu (18.04 or 16.04) and Fedora 28 (other OS versions and distributions may work, but are currently untested).

#### On Windows:
* [PowerShell Core 6.1+](https://github.com/PowerShell/PowerShell/releases) or Windows PowerShell 5.1
* [.NET Framework 4.7.1+](https://support.microsoft.com/en-us/help/4054530/microsoft-net-framework-4-7-2-offline-installer-for-windows) (if using Windows PowerShell)
* [iTunes](https://www.apple.com/itunes) (optional, for AAC and ALAC support. Requires the classic installer, not the Windows Store version. Technically only the Apple Application support packages are required)
#### On Linux:
* [PowerShell Core 6.1+](https://github.com/PowerShell/PowerShell/releases)
* libebur128-1 (via apt-get on Ubuntu 16.04 and 18.04)
* libmp3lame0 (via apt-get on Ubuntu 16.04)
* libebur128 (via dnf on Fedora 28)
#### On MacOS:
* [PowerShell Core 6.1+](https://github.com/PowerShell/PowerShell/releases)

### Installation
The AudioWorks PowerShell module can found on [the PowerShell Gallery](https://www.powershellgallery.com/packages/AudioWorks.Commands/1.0.0-beta1). If you are using Windows PowerShell 5.1, you may need to [update PowerShellGet](https://docs.microsoft.com/en-us/powershell/gallery/installing-psget) to a version that supports pre-release modules.

Keep the module up to date with `Update-Module -Name AudioWorks.Commands -AllowPrerelease`.

### Additional Requirements for Building / Testing
* [.NET Core SDK 2.1.300+](https://dotnet.github.io/)
* Windows 7+ or Ubuntu 16.04 (currently does not compile on Ubuntu 18.04)
* [Visual Studio 2017 15.7+](https://visualstudio.microsoft.com/downloads) (optional - Windows only)
* [Visual Studio 2017 for Mac 7.7+](https://visualstudio.microsoft.com/downloads) (optional - Mac only)
* [Visual Studio Code](https://code.visualstudio.com/) (optional - all platforms)
* [ReSharper 2018.2+](https://www.jetbrains.com/resharper/eap) (optional - Windows only)

### Special Thanks
This project wouldn't be possible without the work of these other fine projects and organizations:
* [Hydrogen Audio Forums](https://hydrogenaud.io/), a wonderful, scientifically-minded community of digital audio enthusiasts.
* [The LAME Project](http://lame.sourceforge.net/), maintainers of the high-quality MP3 encoder.
* [The Xiph.Org Foundation](https://xiph.org/), maintainers of Ogg Vorbis and FLAC (the Free Lossless Audio Codec).
* [libebur128](https://github.com/jiixyj/libebur128), a library implementing the EBU R.128 loudness standard.
* [QAAC](https://sites.google.com/site/qaacpage/), a command-line front-end for Apple's AAC and Apple Lossless encoders.
