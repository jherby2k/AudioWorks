---
external help file: AudioWorks.Commands.dll-Help.xml
Module Name: AudioWorks.Commands
online version: https://github.com/jherby2k/AudioWorks/blob/master/AudioWorks/src/AudioWorks.Commands/docs/Rename-AudioFile.md
schema: 2.0.0
---

# Rename-AudioFile

## SYNOPSIS
Renames an audio file.

## SYNTAX

```
Rename-AudioFile [-Name] <String> [-AudioFile] <ITaggedAudioFile> [-Replace] [-PassThru] [<CommonParameters>]
```

## DESCRIPTION
The Rename-AudioFile cmdlet renames audio files.

## EXAMPLES

### Example 1: Rename an audio file based on metadata
```powershell
PS C:\> Get-AudioFile test.flac | Rename-AudioFile "{TrackNumber} - {Title}"
```

Renames test.flac with a file name composed of the track number and title.

## PARAMETERS

### -Name
Specifies the new file name.

The file extension will be selected automatically and should not be included.

This parameter can reference metadata fields using {} braces. For example, to specify a name consisting of the track number, a hyphen, then the title, you would use "{TrackNumber} - {Title}".

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
