<h1 align="center"><img src="https://github.com/jherby2k/AudioWorks/raw/main/Logo.png" width="567" /></h1>

A cross-platform, multi-format audio conversion and tagging suite for .NET and PowerShell. Formats currently supported are MP3, MP4 AAC, FLAC, Apple Lossless (ALAC), Opus, Ogg Vorbis and Wave.

Main Branch | Dev branch
-- | --
![Build Status](https://github.com/jherby2k/AudioWorks/actions/workflows/build-and-test.yml/badge.svg?branch=main) | ![Build Status](https://github.com/jherby2k/AudioWorks/actions/workflows/build-and-test.yml/badge.svg?branch=dev)

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
