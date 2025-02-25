<h1 align="center"><img src="https://github.com/jherby2k/AudioWorks/raw/main/Logo.png" width="567" /></h1>

A cross-platform, multi-format audio conversion and tagging suite for .NET and PowerShell. Formats currently supported are MP3, MP4 AAC, FLAC, Apple Lossless (ALAC), Opus, Ogg Vorbis and Wave.

Main Branch | Dev branch
-- | --
![Build Status](https://github.com/jherby2k/AudioWorks/actions/workflows/build-and-test.yml/badge.svg?branch=main) | ![Build Status](https://github.com/jherby2k/AudioWorks/actions/workflows/build-and-test.yml/badge.svg?branch=dev)

## How to Use

### Encode some files
PowerShell:
```pwsh
$flacFiles = Get-AudioFile '<path to an album>\*.flac'

# Export the files to Opus format, organized into directories and named according to metadata
$files | Export-AudioFile "Opus" -Path "C:\Output\{Album} by {Artist}" -Name "{TrackNumber} - {Title}" -SignalType Speech -BitRate 32 -Force
```

C#:
```csharp
var flacFiles = new DirectoryInfo("<path to an album>")
    .GetFiles("*.flac")
    .Select(file => new TaggedAudioFile(file.FullName));

// Export the files to Opus format, organized into directories and named according to metadata
var opusEncoder = new AudioFileEncoder("Opus")
{
    EncodedDirectoryName = @"C:\Output\{Album} by {Artist}",
    EncodedFileName = "{TrackNumber} - {Title}",
    Overwrite = true,
    Settings = new()
    {
        ["SignalType"] = "Speech",
        ["BitRate"] = 32
    }
};
await opusEncoder.EncodeAsync(flacFiles);
```

### Set metadata (tags)
PowerShell:
```pwsh
$flacFiles = Get-AudioFile '<path to an album>\*.flac'

# Add ReplayGain tags, analyzing the files as a single album
$flacFiles | Measure-AudioFile -Analyzer ReplayGain

# Set the artist as well, then persist the new tags to disk
$flacFiles | Set-AudioMetadata -Artist "Iron Butterfly"
$flacFiles | Save-AudioMetadata
```

C#:
```csharp
var flacFiles = new DirectoryInfo("<path to an album>")
    .GetFiles("*.flac")
    .Select(file => new TaggedAudioFile(file.FullName));

// Add ReplayGain tags, analyzing the files as a single album
var replayGainAnalyzer = new AudioFileAnalyzer("ReplayGain");
await replayGainAnalyzer.AnalyzeAsync(flacFiles);

// Set the artist as well, then persist the new tags to disk
foreach (var flacFile in flacFiles)
{
    flacFile.Metadata.Artist = "Iron Butterfly";
    flacFile.SaveMetadata();
}
```

## System Requirements
AudioWorks runs on Windows, MacOS, and Linux.
You will need [PowerShell 7.4+](https://aka.ms/powershell). "Windows PowerShell" (aka PowerShell 5.1) which is included with Windows, is no longer supported.
 
### On Windows:
* For AAC and Apple Lossless (ALAC) support, [you need Apple's Core Audio library](https://github.com/jherby2k/AudioWorks/wiki/Apple-Core-Audio-dependency)

### On Ubuntu 24.04 (other distros should also work, but are untested):
* Install all dependencies with `apt install libflac12t64 libmp3lame0 libvorbisenc2 libopusenc0 libebur128-1`

A standard installation should only be missing libopusenc and libebur128.

## Installation
The AudioWorks PowerShell module can found on [the PowerShell Gallery](https://www.powershellgallery.com/packages/AudioWorks.Commands).

Keep the module up to date with `Update-Module -Name AudioWorks.Commands`.

The .NET API is available via [NuGet](https://www.nuget.org/packages/AudioWorks.Api).

## Special Thanks
This project wouldn't be possible without the work of these other fine projects and organizations:
* [Hydrogen Audio Forums](https://hydrogenaud.io/), a scientifically-minded community of digital audio enthusiasts.
* [The LAME Project](http://lame.sourceforge.net/), maintainers of the high-quality MP3 encoder.
* [The Xiph.Org Foundation](https://xiph.org/), maintainers of Opus, Ogg Vorbis and FLAC (the Free Lossless Audio Codec).
* [libebur128](https://github.com/jiixyj/libebur128), a library implementing the EBU R.128 loudness standard.
* [QAAC](https://sites.google.com/site/qaacpage/), a command-line front-end for Apple's AAC and Apple Lossless encoders.
