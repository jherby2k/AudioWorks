---
external help file: AudioWorks.Commands.dll-Help.xml
Module Name: AudioWorks.Commands
online version: https://github.com/jherby2k/AudioWorks/blob/master/AudioWorks/src/AudioWorks.Commands/docs/Save-AudioMetadata.md
schema: 2.0.0
---

# Save-AudioMetadata

## SYNOPSIS
Saves an audio file's metadata to disk.

## SYNTAX

```
Save-AudioMetadata [-AudioFile] <ITaggedAudioFile> [-Format <String>] [-PassThru] [-WhatIf] [-Confirm]
 [<CommonParameters>]
```

## DESCRIPTION
The Save-AudioMetadata cmdlet persists changes to an audio file's metadata.

## EXAMPLES

### Example 1: Clear an audio file's metadata, then save the changes
```powershell
PS C:\> Get-AudioFile test.flac | Clear-AudioMetadata -PassThru | Save-AudioMetadata
```

Clears all metadata fields from test.flac, then saves the changes to disk using default settings.

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

### -Format
Specifies the metadata format to use.

This is normally selected automatically based on the file extension.
Explicitly specifying the format enables any dynamic parameters that are format-specific.

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

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs.
The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

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
