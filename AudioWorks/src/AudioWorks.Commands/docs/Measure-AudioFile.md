---
external help file: AudioWorks.Commands.dll-Help.xml
Module Name: AudioWorks.Commands
online version: https://github.com/jherby2k/AudioWorks/blob/master/AudioWorks/src/AudioWorks.Commands/docs/Measure-AudioFile.md
schema: 2.0.0
---

# Measure-AudioFile

## SYNOPSIS
Analyzes an audio file.

## SYNTAX

```
Measure-AudioFile [-Analyzer] <String> [-AudioFile] <ITaggedAudioFile> [-MaxDegreeOfParallelism <Int32>]
 [-PassThru] [<CommonParameters>]
```

## DESCRIPTION
The Measure-AudioFile cmdlet performs analysis on an audio file, then stores these measurements as metadata.

## EXAMPLES

### Example 1: Analyze a single audio file
```powershell
PS C:\> Get-AudioFile test.flac | Measure-AudioFile ReplayGain -PassTrue | Save-AudioMetadata
```

Performs ReplayGain analysis on test.flac, then saves the updated metadata to disk. Track and Album values will be the same.

### Example 2: Analyze a group of audio files together
```powershell
PS C:\> Get-AudioFile *.flac | Measure-AudioFile ReplayGain -PassThru | Save-AudioMetadata
```

Performs ReplayGain analysis on all the flac files in the current directory, then saves the updated metadata to disk. For calculating album values, the files are treated as a one album.

## PARAMETERS

### -Analyzer
Specifies the type of analysis to perform.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AudioFile
Specifies the audio file.

```yaml
Type: ITaggedAudioFile
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -PassThru
Returns an object representing the item with which you are working.
By default, this cmdlet does not generate any output.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -MaxDegreeOfParallelism
Sets the maximum degree of parallelism.

Defaults to the logical processor count.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### AudioWorks.Common.ITaggedAudioFile
Specifies the audio file.

## OUTPUTS

### AudioWorks.Common.ITaggedAudioFile
## NOTES

## RELATED LINKS

[AudioWorks Wiki](https://github.com/jherby2k/AudioWorks/wiki)
