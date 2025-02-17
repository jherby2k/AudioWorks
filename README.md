<h1 align="center"><img src="https://github.com/jherby2k/AudioWorks/raw/main/Logo.png" width="567" /></h1>

A cross-platform, multi-format audio conversion and tagging suite for .NET and PowerShell. Formats currently supported are MP3, MP4 AAC, FLAC, Apple Lossless (ALAC), Opus, Ogg Vorbis and Wave.

Main Branch | Dev branch
-- | --
![Build Status](https://github.com/jherby2k/AudioWorks/actions/workflows/build-and-test.yml/badge.svg?branch=main) | ![Build Status](https://github.com/jherby2k/AudioWorks/actions/workflows/build-and-test.yml/badge.svg?branch=dev)

### System Requirements
AudioWorks runs on Windows, MacOS, and Linux.
You will need [PowerShell 7.4+](https://aka.ms/powershell). "Windows PowerShell" (aka PowerShell 5.1) which is included with Windows, is no longer supported.
 
#### On Windows:
* For AAC and Apple Lossless (ALAC) support, you need Apple's Core Audio library:
   * Go to [the iTunes download page](https://www.apple.com/itunes), ignore the recommendation to visit the Windows Store, scroll down to *Looking for other versions* and download iTunes64Setup.exe.
   * Either install iTunes, or just extract the required libraries via [these instructions](https://github.com/nu774/makeportable).

#### On Ubuntu 24.04 (other distros should also work, but are untested):
* Install all dependencies with `apt install libflac12t64 libmp3lame0 libopus0 libvorbisenc2 libopusenc0 libebur128-1`

A standard installation should only be missing libopusenc and libebur128.

### Installation
The AudioWorks PowerShell module can found on [the PowerShell Gallery](https://www.powershellgallery.com/packages/AudioWorks.Commands).

Keep the module up to date with `Update-Module -Name AudioWorks.Commands`.

The .NET API is available via [NuGet](https://www.nuget.org/packages/AudioWorks.Api).

### Additional Requirements for Building / Testing
* [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or higher

### Special Thanks
This project wouldn't be possible without the work of these other fine projects and organizations:
* [Hydrogen Audio Forums](https://hydrogenaud.io/), a scientifically-minded community of digital audio enthusiasts.
* [The LAME Project](http://lame.sourceforge.net/), maintainers of the high-quality MP3 encoder.
* [The Xiph.Org Foundation](https://xiph.org/), maintainers of Opus, Ogg Vorbis and FLAC (the Free Lossless Audio Codec).
* [libebur128](https://github.com/jiixyj/libebur128), a library implementing the EBU R.128 loudness standard.
* [QAAC](https://sites.google.com/site/qaacpage/), a command-line front-end for Apple's AAC and Apple Lossless encoders.
