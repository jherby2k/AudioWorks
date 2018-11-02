---
external help file: AudioWorks.Commands.dll-Help.xml
Module Name: AudioWorks.Commands
online version:
schema: 2.0.0
---

# Get-AudioMetadata

## SYNOPSIS
Gets metadata describing an audio file.

## SYNTAX

```
Get-AudioMetadata [-AudioFile] <ITaggedAudioFile> [<CommonParameters>]
```

## DESCRIPTION
The Get-AudioMetadata cmdlet gets information about audio files. This consists of fields that can be modified and optionally persisted to disk using other cmdlets.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-AudioFile test.flac | Get-AudioMetadata

Title       : Test Track
Artist      : Some Artist
Album       : Greatest Hits
AlbumArtist :
Composer    :
Genre       : Rock
Comment     :
Day         :
Month       :
Year        : 2018
TrackNumber : 01
TrackCount  : 10
TrackPeak   : 0.956024
AlbumPeak   : 1.000000
TrackGain   : -2.94 dB
AlbumGain   : -7.65 dB
```

Gets The metadata stored in test.flac.

## PARAMETERS

### -AudioFile
Specifies the audio file.

```yaml
Type: ITaggedAudioFile
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

### AudioWorks.Common.ITaggedAudioFile
Specifies the audio file.

## OUTPUTS

### AudioWorks.Common.AudioMetadata
## NOTES

## RELATED LINKS
