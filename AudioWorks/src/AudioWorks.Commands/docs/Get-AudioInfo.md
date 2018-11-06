---
external help file: AudioWorks.Commands.dll-Help.xml
Module Name: AudioWorks.Commands
online version: https://github.com/jherby2k/AudioWorks/blob/master/AudioWorks/src/AudioWorks.Commands/docs/Get-AudioInfo.md
schema: 2.0.0
---

# Get-AudioInfo

## SYNOPSIS
Gets information about an audio file.

## SYNTAX

```
Get-AudioInfo [-AudioFile] <IAudioFile> [<CommonParameters>]
```

## DESCRIPTION
The Get-AudioInfo cmdlet gets information about audio files. This consists of immutable information that can't be changed without re-encoding the file.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-AudioFile test.flac | Get-AudioInfo

Format        : FLAC
Channels      : 2
BitsPerSample : 24
SampleRate    : 96 kHz
BitRate       : 4608 kbps
FrameCount    : 16142465
PlayLength    : 00:02:48
```

Gets information about the audio stored in test.flac.

## PARAMETERS

### -AudioFile
Specifies the audio file.

```yaml
Type: IAudioFile
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### AudioWorks.Common.IAudioFile
Specifies the audio file.

## OUTPUTS

### AudioWorks.Common.AudioInfo
## NOTES

## RELATED LINKS
[AudioWorks Wiki](https://github.com/jherby2k/AudioWorks/wiki)
