---
external help file: AudioWorks.Commands.dll-Help.xml
Module Name: AudioWorks.Commands
online version: https://github.com/jherby2k/AudioWorks/blob/master/AudioWorks/src/AudioWorks.Commands/docs/Get-AudioEncoderInfo.md
schema: 2.0.0
---

# Get-AudioEncoderInfo

## SYNOPSIS
Gets information about the available audio encoders.

## SYNTAX

```
Get-AudioEncoderInfo [<CommonParameters>]
```

## DESCRIPTION
The Get-AudioEncoderInfo cmdlet gets objects that describe the audio encoders currently loaded and available for use with the Export-AudioFile cmdlet.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-AudioEncoderInfo

Name     Description
----     -----------
AppleAAC Apple MPEG-4 Advanced Audio Codec
ALAC     Apple Lossless Audio Codec
FLAC     Free Lossless Audio Codec
LameMP3  Lame MPEG Audio Layer 3
Vorbis   Ogg Vorbis
Wave     Waveform Audio File Format
```

Gets the available audio encoders.

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

### AudioWorks.Api.AudioEncoderInfo
## NOTES

## RELATED LINKS

[AudioWorks Wiki](https://github.com/jherby2k/AudioWorks/wiki)
