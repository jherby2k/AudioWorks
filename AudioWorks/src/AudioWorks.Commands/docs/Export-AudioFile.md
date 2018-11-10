---
external help file: AudioWorks.Commands.dll-Help.xml
Module Name: AudioWorks.Commands
online version: https://github.com/jherby2k/AudioWorks/blob/master/AudioWorks/src/AudioWorks.Commands/docs/Export-AudioFile.md
schema: 2.0.0
---

# Export-AudioFile

## SYNOPSIS
Exports an audio file.

## SYNTAX

```
Export-AudioFile [-Encoder] <String> [-Path] <String> [-AudioFile] <ITaggedAudioFile> [-Name <String>]
 [-Replace] [-MaxDegreeOfParallelism <Int32>] [<CommonParameters>]
```

## DESCRIPTION
The Export-AudioFile cmdlet creates a new audio file using the specified encoder.

## EXAMPLES

### Example 1: Export an audio file to a new audio format in the same directory
```powershell
PS C:\> Get-AudioFile test.flac | Export-AudioFile LameMP3 .
```

Exports test.flac as test.mp3 using default settings, in the current directory.

### Example 2: Re-encode an audio file in-place
```powershell
PS C:\> Get-AudioFile test.flac | Export-AudioFile FLAC . -Replace
```

Re-encodes test.flac in-place, using default settings.

### Example 3: Export all audio files in the current directory, laid out according to each audio file's metadata
```powershell
PS C:\> Get-AudioFile *.flac | Export-AudioFile LameMP3 "{Artist}\{Album}" -Name "{TrackNumber} - {Title}"
```

Exports all .flac files in the current directory, under an artist then album directory structure, with a file name composed of the track number and title.

## PARAMETERS

### -Encoder
Specifies the encoder to use.

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

### -Path
Specifies the output directory path.

This parameter can reference metadata fields using {} braces. For example, to specify an Artist then Album heirarchy relative to the current directory, you would use "{Artist}\{Album}".

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AudioFile
Specifies the source audio file.

```yaml
Type: ITaggedAudioFile
Parameter Sets: (All)
Aliases:

Required: True
Position: 2
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Name
Specifies the output file name.

The file extension will be selected automatically and should not be included. If this parameter is omitted, the name will be the same as the source audio file.

This parameter can reference metadata fields using {} braces. For example, to specify a name consisting of the track number, a hyphen, then the title, you would use "{TrackNumber} - {Title}".

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Replace
Indicates that existing files should be replaced.

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
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### AudioWorks.Common.ITaggedAudioFile
Specifies the source audio file.

## OUTPUTS

### AudioWorks.Common.ITaggedAudioFile
## NOTES

## RELATED LINKS

[AudioWorks Wiki](https://github.com/jherby2k/AudioWorks/wiki)
