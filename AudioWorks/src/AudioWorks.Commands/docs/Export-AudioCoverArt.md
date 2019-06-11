---
external help file: AudioWorks.Commands.dll-Help.xml
Module Name: AudioWorks.Commands
online version: https://github.com/jherby2k/AudioWorks/blob/master/AudioWorks/src/AudioWorks.Commands/docs/Export-AudioCoverArt.md
schema: 2.0.0
---

# Export-AudioCoverArt

## SYNOPSIS
Exports cover art from an audio file.

## SYNTAX

```
Export-AudioCoverArt [-Path] <String> [-AudioFile] <ITaggedAudioFile> [-Name <String>] [-Replace]
 [<CommonParameters>]
```

## DESCRIPTION
The Export-AudioCoverArt cmdlet extracts the cover art from an audio file, and saves it to disk.

## EXAMPLES

### Example 1: Extract cover art from an audio file to an image file
```powershell
PS C:\> Get-AudioFile test.flac | Export-AudioCoverArt .
```

Extracts the cover art stored in test.flac as test.png or test.jpg, in the current directory.

### Example 2: Extract cover art from all audio files in the current directory, laid out according to each audio file's metadata
```powershell
PS C:\> Get-AudioFile *.flac | Export-AudioCoverArt "{Artist}\{Album}" -Name "{Artist} - {Album}"
```

Extracts the cover art stored in each .flac file, saving the result under an artist then album directory structure, with a file name composed of the artist and album title.

## PARAMETERS

### -Path
Specifies the output directory path.

This parameter can reference metadata fields using {} braces. For example, to specify an Artist then Album heirarchy relative to the current directory, you would use "{Artist}\{Album}".

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
Specifies the source audio file.

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### AudioWorks.Common.ITaggedAudioFile
Specifies the source audio file.

## OUTPUTS

### System.IO.FileInfo
## NOTES

## RELATED LINKS

[AudioWorks Wiki](https://github.com/jherby2k/AudioWorks/wiki)
