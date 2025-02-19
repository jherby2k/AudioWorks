## About

This is the public API for AudioWorks, a is cross-platform, multi-format audio conversion and tagging suite.

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

## Feedback

AudioWorks is released as open source under the [GNU Affero General Public License](https://github.com/jherby2k/AudioWorks/blob/main/LICENSE). Bug reports and contributions are welcome at [the GitHub repository](https://github.com/jherby2k/AudioWorks).