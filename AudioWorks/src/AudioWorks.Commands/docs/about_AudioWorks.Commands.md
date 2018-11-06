# AudioWorks.Commands
## about_AudioWorks.Commands

# SHORT DESCRIPTION
A cross-platform, multi-format audio conversion and tagging module.

# LONG DESCRIPTION
The AudioWorks PowerShell module. AudioWorks is a cross-platform, multi-format audio conversion and tagging suite. For documentation not specific to PowerShell, visit the AudioWorks wiki.

# EXAMPLES
Most cmdlets operate on objects of type `AudioWorks.ITaggedAudioFile`, which are obtained using the `Get-AudioFile` cmdlet.

```powershell
$af = Get-AudioFile test.flac
```

## Encoding
AudioWorks can export "lossless" (Wave, FLAC, ALAC) audio files to other audio formats using the `Export-AudioFile` cmdlet.

```powershell
$af | Export-AudioFile LameMP3 .
```

## Tag Editing
AudioWorks provides several metadata "tag" editing cmdlets as well.

```powershell
$af = Get-AudioFile test.flac
$af | Clear-AudioMetadata
$af | Set-AudioMetadata -Title "Testing 1-2-3"
$af | Save-AudioMetadata
```

## Analysis
AudioWorks also provides EBU R.128-based ReplayGain analysis through the `Measure-AudioFile` cmdlet.

```powershell
$af | Measure-AudioFile ReplayGain
$af | Save-AudioMetadata
```

# SEE ALSO
https://github.com/jherby2k/AudioWorks/wiki
