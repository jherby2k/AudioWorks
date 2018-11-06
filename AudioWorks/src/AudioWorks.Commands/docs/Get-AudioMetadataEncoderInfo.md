---
external help file: AudioWorks.Commands.dll-Help.xml
Module Name: AudioWorks.Commands
online version: https://github.com/jherby2k/AudioWorks/blob/master/AudioWorks/src/AudioWorks.Commands/docs/Get-AudioMetadataEncoderInfo.md
schema: 2.0.0
---

# Get-AudioMetadataEncoderInfo

## SYNOPSIS
Gets information about the available metadata encoders.

## SYNTAX

```
Get-AudioMetadataEncoderInfo [<CommonParameters>]
```

## DESCRIPTION
The Get-AudioMetadataEncoderInfo cmdlet gets objects that describe the metadata encoders currently loaded and available for use with the Save-AudioMetadata cmdlet.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-AudioMetadataEncoderInfo

Extension Format Description
--------- ------ -----------
.flac     FLAC   FLAC
.mp3      ID3    ID3 version 2.x
.m4a      iTunes iTunes-compatible MPEG-4
.ogg      Vorbis Vorbis Comments
```

Gets the available audio metadata encoders.

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

### AudioWorks.Api.AudioMetadataEncoderInfo
## NOTES

## RELATED LINKS
[AudioWorks Wiki](https://github.com/jherby2k/AudioWorks/wiki)
