<h1 align="center"><img src="https://github.com/jherby2k/AudioWorks/blob/master/Logo.png" width="567" /></h1>

A cross-platform, multi-format audio conversion and tagging suite written in C# and featuring a PowerShell front-end. Codecs currently supported are MP3, MP4 AAC, FLAC, Apple Lossless (ALAC) and Ogg Vorbis.

This project is intended to supplant [PowerShell Audio](https://github.com/jherby2k/PowerShellAudio) by targetting both Windows PowerShell and PowerShell Core (Windows, Linux and Mac OSX).

Release is tentatively planned for the third quarter of 2018.

### Build Status (Master Branch)

Currently working and relatively stable on Windows and Ubuntu.

Platform | Status
-------- | ------
Windows  | [![Build status](https://ci.appveyor.com/api/projects/status/k7yiy48qkoa5701t/branch/master?svg=true)](https://ci.appveyor.com/project/jherby2k/audioworks-n8ay6/branch/master) [![codecov](https://codecov.io/gh/jherby2k/audioworks/branch/master/graph/badge.svg)](https://codecov.io/gh/jherby2k/audioworks)
Linux    | [![Build status](https://ci.appveyor.com/api/projects/status/8kh6urve97ibwubv/branch/master?svg=true)](https://ci.appveyor.com/project/jherby2k/audioworks-n6p0s/branch/master) [![codecov](https://codecov.io/gh/jherby2k/audioworks/branch/master/graph/badge.svg)](https://codecov.io/gh/jherby2k/audioworks)
MacOS    | Not available

### System Requirements

AudioWorks currently needs to be built from source, although installers are coming soon!

* [PowerShell Core 6.1+](https://github.com/PowerShell/PowerShell/releases) or Windows PowerShell 5.1+
* [.NET Framework 4.7.1+](https://support.microsoft.com/en-us/help/4054530/microsoft-net-framework-4-7-2-offline-installer-for-windows) (Windows only, if using Windows PowerShell)
* Windows 7+, OSX 10.13, Ubuntu (18.04 or 16.04), Fedora 28 (other OS versions and distributions may work, but are currently untested).
* [iTunes](https://www.apple.com/itunes) (optional, for AAC and ALAC support on Windows only. Requires the classic installer, not the Windows Store version. Technically only the Apple Application support packages are required).
* libebur128-1 (via apt-get on Ubuntu 16.04 and 18.04)
* libmp3lame0 (via apt-get on Ubuntu 16.04)
* libebur128 (via dnf on Fedora 28)

### Installation

The AudioWorks PowerShell module can be installed via PowerShellGet:
1. (Windows PowerShell only) [Update PowerShellGet](https://docs.microsoft.com/en-us/powershell/gallery/installing-psget) to a version that supports prerelease modules.
1. Check your PowerShell execution policy using `Get-ExecutionPolicy`. If it is Restricted, you will need to set your PowerShell execution policy to allow signed scripts using something like `Set-ExecutionPolicy -ExecutionPolicy AllSigned` from an administrator command prompt. RemoteSigned and Unrestricted will also work, but are less secure.
1. Register the AudioWorks repository with `Register-PSRepository -Name AudioWorks -SourceLocation 'https://www.myget.org/F/audioworks/api/v2'`.
1. Install the module with `Install-Module -Name AudioWorks.Commands -Repository AudioWorks -AllowPrerelease -Scope CurrentUser`.

Keep the module up to date with `Update-Module -Name AudioWorks.Commands -AllowPrerelease`.

### Additional Requirements for Building / Testing

* [.NET Core SDK 2.1.300+](https://dotnet.github.io/)
* Windows 7+ or Ubuntu 16.04 (currently does not compile on Ubuntu 18.04)
* [Visual Studio 2017 15.7+](https://visualstudio.microsoft.com/downloads) (optional - Windows only)
* [Visual Studio Code](https://code.visualstudio.com/) (optional - all platforms)
* [ReSharper 2018.2+](https://www.jetbrains.com/resharper/eap) (optional - Windows only)

### Special Thanks

This project wouldn't be possible without the work of these other fine projects and organizations:

* [Hydrogen Audio Forums](https://hydrogenaud.io/), a wonderful, scientifically-minded community of digital audio enthusiasts.
* [The LAME Project](http://lame.sourceforge.net/), maintainers of the high-quality MP3 encoder.
* [The Xiph.Org Foundation](https://xiph.org/), maintainers of Ogg Vorbis and FLAC (the Free Lossless Audio Codec).
* [libebur128](https://github.com/jiixyj/libebur128), a library implementing the EBU R128 loudness standard.
* [QAAC](https://sites.google.com/site/qaacpage/), a command-line front-end for Apple's AAC and Apple Lossless encoders.
