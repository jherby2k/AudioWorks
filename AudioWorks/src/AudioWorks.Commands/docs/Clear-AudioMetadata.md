---
external help file: AudioWorks.Commands.dll-Help.xml
Module Name: AudioWorks.Commands
online version:
schema: 2.0.0
---

# Clear-AudioMetadata

## SYNOPSIS
Clears one or more metadata fields on an audio file.

## SYNTAX

```
Clear-AudioMetadata [-AudioFile] <ITaggedAudioFile> [-Title] [-Artist] [-Album] [-AlbumArtist] [-Composer]
 [-Genre] [-Comment] [-Day] [-Month] [-Year] [-TrackNumber] [-TrackCount] [-Loudness] [-PassThru]
 [<CommonParameters>]
```

## DESCRIPTION
The Clear-AudioMetadata cmdlet clears one or more metadata fields. Note that these changes are not persisted to disk unless followed by a call to Save-AudioMetadata. If no metadata fields are specified, all fields are cleared.

## EXAMPLES

### Example 1: Remove all metadata from an existing file
```powershell
PS C:\> Get-AudioFile test.flac | Clear-AudioMetadata -PassThru | Save-AudioMetadata
```

Clears all metadata fields from test.flac, then saves the changes to disk.

### Example 2: Remove a metadata field from an existing file
```powershell
PS C:\> Get-AudioFile test.flac | Clear-AudioMetadata -Loudness -PassThru | Save-AudioMetadata
```

Clears loudness (REPLAYGAIN_*) fields from test.flac, then saves the changes to disk.

### Example 3: Export to a new file, with no metadata preserved
```powershell
PS C:\> Get-AudioFile test.flac | Clear-AudioMetadata -PassThru | Export-AudioFile LameMP3 .
```

Exports test.flac as test.mp3 (in the current directory), without any metadata (no ID3 tag will be written).

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

### -Title
Clears the title.

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

### -Artist
Clears the artist.

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

### -Album
Clears the album.

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

### -AlbumArtist
Clears the album artist.

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

### -Composer
Clears the composer.

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

### -Genre
Clears the genre.

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

### -Comment
Clears the comment.

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

### -Day
Clears the day of the month.

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

### -Month
Clears the month.

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

### -Year
Clears the year.

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

### -TrackNumber
Clears the track number.

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

### -TrackCount
Clears the track count.

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

### -Loudness
Clears the track peak, album peak, track gain and album gain.

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
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### AudioWorks.Common.ITaggedAudioFile
Specifies the audio file.

## OUTPUTS

### AudioWorks.Common.ITaggedAudioFile
## NOTES

## RELATED LINKS
