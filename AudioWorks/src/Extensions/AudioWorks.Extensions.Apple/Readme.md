## About

Provides AAC (encoding) and Apple Lossless (encoding and decoding) functionality for AudioWorks via Apple.

You should normally consume [AudioWorks.Api](https://www.nuget.org/packages/AudioWorks.Api/) in addition to this package.

More documentation is available on [the GitHub repository](https://github.com/jherby2k/AudioWorks).

## External Dependencies

On Windows, this extension requires the Apple Core Audio library.
   * Go to [the iTunes download page](https://www.apple.com/itunes), ignore the recommendation to visit the Windows Store, scroll down to *Looking for other versions* and download iTunes64Setup.exe.
   * Either install iTunes, or just extract the required libraries via [these instructions](https://github.com/nu774/makeportable).

This extension is not available on Linux.

## Main Types

The main types provided by this library are:

* `AlacAudioDecoder`
* `AlacAudioEncoder`
* `AacAudioEncoder`

## Related Packages

* The AudioWorks API: [AudioWorks.Api](https://www.nuget.org/packages/AudioWorks.Api/)
* iTunes-style MP4 metadata: [AudioWorks.Extensions.Mp4](https://www.nuget.org/packages/AudioWorks.Extensions.Mp4/)
* ReplayGain and EBU R 128 analysis: [AudioWorks.Extensions.ReplayGain](https://www.nuget.org/packages/AudioWorks.Extensions.ReplayGain/)

## Feedback

AudioWorks is released as open source under the [GNU Affero General Public License](https://github.com/jherby2k/AudioWorks/blob/main/LICENSE). Bug reports and contributions are welcome at [the GitHub repository](https://github.com/jherby2k/AudioWorks).