---
external help file: AudioWorks.Commands.dll-Help.xml
Module Name: AudioWorks.Commands
online version: https://github.com/jherby2k/AudioWorks/blob/master/AudioWorks/src/AudioWorks.Commands/docs/Get-AudioCoverArt.md
schema: 2.0.0
---

# Get-AudioCoverArt

## SYNOPSIS
Gets an object that represents an audio file or album's cover art.

## SYNTAX

### ByPath (Default)
```
Get-AudioCoverArt [-Path] <String> [<CommonParameters>]
```

### ByLiteralPath
```
Get-AudioCoverArt [-LiteralPath] <String> [<CommonParameters>]
```

### ByFileInfo
```
Get-AudioCoverArt [-FileInfo] <FileInfo> [<CommonParameters>]
```

## DESCRIPTION
The Get-AudioCoverArt cmdlet gets objects that represent cover art. Cover art can be applied to and extracted from audio files when supported by the audio format.

## EXAMPLES

### Example 1: Retrieve cover art from an image file
```powershell
PS C:\> $cover = Get-AudioCoverArt Folder.png
```

Gets a cover art object from a file named Folder.png, located in the current directory.

### Example 2: Retrieve cover art from an image file, then apply it to an audio file as metadata
```powershell
PS C:\> Get-AudioFile test.flac | Set-AudioMetadata -CoverArt (Get-AudioCoverArt Folder.png) -PassThru | Save-AudioMetadata
```

Sets the cover art in test.flac to the image retrieved from Folder.png, then saves the changes to disk.

## PARAMETERS

### -Path
Specifies the path to an item.
This cmdlet gets the item at the specified location.
Wildcards are permitted.
This parameter is required, but the parameter name ("Path") is optional.

Use a dot (.) to specify the current location.
Use the wildcard character (*) to specify all the items in the current location.

```yaml
Type: String
Parameter Sets: ByPath
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -LiteralPath
Specifies a path to the item.
Unlike the Path parameter, the value of LiteralPath is used exactly as it is typed.
No characters are interpreted as wildcards.
If the path includes escape characters, enclose it in single quotation marks.
Single quotation marks tell Windows PowerShell not to interpret any characters as escape sequences.

```yaml
Type: String
Parameter Sets: ByLiteralPath
Aliases: PSPath

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FileInfo
Specifies the file information.

```yaml
Type: FileInfo
Parameter Sets: ByFileInfo
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

### System.String
Specifies the path to an item.
This cmdlet gets the item at the specified location.
Wildcards are permitted.
This parameter is required, but the parameter name ("Path") is optional.

Use a dot (.) to specify the current location.
Use the wildcard character (*) to specify all the items in the current location.

### System.IO.FileInfo
Specifies the file information.

## OUTPUTS

### AudioWorks.Common.ICoverArt
## NOTES

## RELATED LINKS
