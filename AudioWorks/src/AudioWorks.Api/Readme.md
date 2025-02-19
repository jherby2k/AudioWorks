## About

This is the public API for AudioWorks, a cross-platform, multi-format audio conversion and tagging suite.

You should also consume one or more "Extensions" packages (see Related Packages below) for full functionality.

More documentation is available on [the GitHub repository](https://github.com/jherby2k/AudioWorks).

## How to Use

```csharp
var flacFiles = new DirectoryInfo($"path to an album")
    .GetFiles("*.flac")
    .Select(file => new TaggedAudioFile(file.FullName));

# Export the files to Opus format
var opusEncoder = new AudioFileEncoder("Opus");
await opusEncoder.EncodeAsync(flacFiles);
```

## Key Features

* Supports MP3, MP4 AAC, FLAC, Apple Lossless (ALAC), Opus, Ogg Vorbis and Wave formats
* High performance architecture
* Runs on Windows, Linux and MacOS

## Main Types

The main types provided by this library are:

* `TaggedAudioFile`
* `AudioFileEncoder`
* `AudioFileAnalyzer`
* `CoverArtExtractor`

## Related Packages

* AAC (encoding) and Apple Lossless (encoding and decoding): [AudioWorks.Extensions.Apple](https://www.nuget.org/packages/AudioWorks.Extensions.Apple/)
* FLAC encoding, decoding and metadata: [AudioWorks.Extensions.Flac](https://www.nuget.org/packages/AudioWorks.Extensions.Flac/)
* ID3 metadata: [AudioWorks.Extensions.Id3](https://www.nuget.org/packages/AudioWorks.Extensions.Id3/)
* MP3 encoding: [AudioWorks.Extensions.Lame](https://www.nuget.org/packages/AudioWorks.Extensions.Lame/)
* MP3 reading: [AudioWorks.Extensions.Mp3](https://www.nuget.org/packages/AudioWorks.Extensions.Mp3/)
* iTunes-style MP4 metadata: [AudioWorks.Extensions.Mp4](https://www.nuget.org/packages/AudioWorks.Extensions.Mp4/)
* Opus encoding and metadata: [AudioWorks.Extensions.Opus](https://www.nuget.org/packages/AudioWorks.Extensions.Opus/)
* ReplayGain and EBU R 128 analysis: [AudioWorks.Extensions.ReplayGain](https://www.nuget.org/packages/AudioWorks.Extensions.ReplayGain/)
* Ogg Vorbis encoding and metadata: [AudioWorks.Extensions.Vorbis](https://www.nuget.org/packages/AudioWorks.Extensions.Vorbis/)
* Wave audio encoding and decoding: [AudioWorks.Extensions.Wave](https://www.nuget.org/packages/AudioWorks.Extensions.Wave/)

## Feedback

AudioWorks is released as open source under the [GNU Affero General Public License](https://github.com/jherby2k/AudioWorks/blob/main/LICENSE). Bug reports and contributions are welcome at [the GitHub repository](https://github.com/jherby2k/AudioWorks).