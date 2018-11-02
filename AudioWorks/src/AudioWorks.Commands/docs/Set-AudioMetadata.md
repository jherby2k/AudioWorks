---
external help file: AudioWorks.Commands.dll-Help.xml
Module Name: AudioWorks.Commands
online version:
schema: 2.0.0
---

# Set-AudioMetadata

## SYNOPSIS
Sets one or more metadata fields on an audio file.

## SYNTAX

```
Set-AudioMetadata [-AudioFile] <ITaggedAudioFile> [-Title <String>] [-Artist <String>] [-Album <String>]
 [-AlbumArtist <String>] [-Composer <String>] [-Genre <String>] [-Comment <String>] [-Day <String>]
 [-Month <String>] [-Year <String>] [-TrackNumber <String>] [-TrackCount <String>] [-CoverArt <ICoverArt>]
 [-PassThru] [<CommonParameters>]
```

## DESCRIPTION
The Set-AudioMetadata cmdlet changes the value of one or more metadata fields. These changes are not persisted to disk unless followed by a call to Save-AudioMetadata.

## EXAMPLES

### Example 1: Update an audio file's metadata
```powershell
PS C:\> Get-AudioFile test.flac | Set-AudioMetadata -Title "Testing 1-2-3" -PassThru | Save-AudioMetadata
```

Sets the test.flac file's Title to "Testing 1-2-3", then saves the changes to disk.

### Example 2: Update an audio file's cover art
```powershell
PS C:\> Get-AudioFile test.flac | Set-AudioMetadata -CoverArt (Get-AudioCoverArt Folder.png) -PassThru | Save-AudioMetadata
```

Sets the cover art in test.flac to the image retrieved from Folder.png, then saves the changes to disk.

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
Sets the title.

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

### -Artist
Sets the artist.

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

### -Album
Sets the album.

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

### -AlbumArtist
Sets the album artist.

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

### -Composer
Sets the composer.

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

### -Genre
Sets the genre.

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

### -Comment
Sets the comment.

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

### -Day
Sets the day of the month.

Should be a number between 1 and 31.

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

### -Month
Sets the month.

Should be a number between 1 and 12.

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

### -Year
Sets the year.

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

### -TrackNumber
Sets the track number.

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

### -TrackCount
Sets the track number.

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

### -CoverArt
Sets the cover art.

```yaml
Type: ICoverArt
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
