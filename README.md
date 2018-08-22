AudioWorks
==========

A cross-platform, multi-format audio conversion and tagging suite written in C# and featuring a PowerShell front-end. Codecs currently supported are MP3, MP4 AAC, FLAC, Apple Lossless (ALAC) and Ogg Vorbis.

This project is intended to supplant [PowerShell Audio](https://github.com/jherby2k/PowerShellAudio) by targetting both Windows PowerShell and PowerShell Core (Windows, Linux and Mac OSX).

Release is tentatively planned for the third quarter of 2018.

### Build Status (Master Branch)

Currently working and relatively stable on Windows and Ubuntu.

Platform | Status
-------- | ------
Windows  | [![Build status](https://ci.appveyor.com/api/projects/status/k7yiy48qkoa5701t/branch/master?svg=true)](https://ci.appveyor.com/project/jherby2k/audioworks-n8ay6/branch/master)
Linux    | [![Build status](https://ci.appveyor.com/api/projects/status/8kh6urve97ibwubv/branch/master?svg=true)](https://ci.appveyor.com/project/jherby2k/audioworks-n6p0s/branch/master)
MacOS    | Coming soon!

### System Requirements

AudioWorks currently needs to be built from source, although installers are coming soon!

* [PowerShell Core 6.1+](https://github.com/PowerShell/PowerShell/releases) (currently in preview) or Windows PowerShell 5.1+
* [.NET Framework 4.7.1+](https://support.microsoft.com/en-us/help/4054530/microsoft-net-framework-4-7-2-offline-installer-for-windows) (Windows only, if using Windows PowerShell)
* Windows 7+, Ubuntu 16.04 or 18.04 (other Linux distros may work, but are currently untested).
* [iTunes](https://www.apple.com/itunes) (optional, for AAC and ALAC support on Windows only. Requires the classic installer, not the Windows Store version. Technically only the Apple Application support packages are required).
* libebur128-1 (via apt-get on Ubuntu 16.04 and 18.04)
* libmp3lame0 (via apt-get on Ubuntu 16.04)

### Installation

The AudioWorks PowerShell module can be installed via PowerShellGet:
1. (Windows PowerShell only) [Update PowerShellGet](https://docs.microsoft.com/en-us/powershell/gallery/installing-psget) to a version that supports prerelease modules.
1. Register the AudioWorks repository with `Register-PSRepository -Name AudioWorks -SourceLocation 'https://www.myget.org/F/audioworks/api/v2/package'`.
1. Install the module with `Install-Module -Name AudioWorks -Repository AudioWorks -AllowPrerelease -Scope CurrentUser`.

Keep the module up to date with `Update-Module -Name AudioWorks.Commands -AllowPrerelease`.

### Additional Requirements for Building / Testing

* [.NET Core SDK 2.1.300+](https://dotnet.github.io/)
* Windows 7+ or Ubuntu 16.04 (currently does not compile on Ubuntu 18.04)
* [Visual Studio 2017 15.7+](https://visualstudio.microsoft.com/downloads) (optional - Windows only)
* [Visual Studio Code](https://code.visualstudio.com/) (optional - all platforms)
* [ReSharper 2018.2+](https://www.jetbrains.com/resharper/eap) (optional - Windows only)
