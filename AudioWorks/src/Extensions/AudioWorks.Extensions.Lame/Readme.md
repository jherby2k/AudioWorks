## About

Provides MP3 encoding functionality for AudioWorks via LAME.

You should normally consume [AudioWorks.Api](https://www.nuget.org/packages/AudioWorks.Api/) in addition to this package.

More documentation is available on [the GitHub repository](https://github.com/jherby2k/AudioWorks).

## External Dependencies

On Linux, you will need libmp3lame and libebur128. For Ubuntu 24.04:

```bash
sudo apt-get install libmp3lame0 libebur128-1
```

## Main Types

The main types provided by this library are:

* `LameAudioEncoder`

## Related Packages

* The AudioWorks API: [AudioWorks.Api](https://www.nuget.org/packages/AudioWorks.Api/)
* ID3 metadata: [AudioWorks.Extensions.Id3](https://www.nuget.org/packages/AudioWorks.Extensions.Id3/)
* MP3 reading: [AudioWorks.Extensions.Mp3](https://www.nuget.org/packages/AudioWorks.Extensions.Mp3/)
* ReplayGain and EBU R 128 analysis: [AudioWorks.Extensions.ReplayGain](https://www.nuget.org/packages/AudioWorks.Extensions.ReplayGain/)

## Feedback

AudioWorks is released as open source under the [GNU Affero General Public License](https://github.com/jherby2k/AudioWorks/blob/main/LICENSE). Bug reports and contributions are welcome at [the GitHub repository](https://github.com/jherby2k/AudioWorks).