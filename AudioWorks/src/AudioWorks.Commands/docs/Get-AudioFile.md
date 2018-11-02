---
external help file: AudioWorks.Commands.dll-Help.xml
Module Name: AudioWorks.Commands
online version:
schema: 2.0.0
---

# Get-AudioFile

## SYNOPSIS
Gets an object that represents an audio file.

## SYNTAX

### ByPath (Default)
```
Get-AudioFile [-Path] <String> [<CommonParameters>]
```

### ByLiteralPath
```
Get-AudioFile [-LiteralPath] <String> [<CommonParameters>]
```

### ByFileInfo
```
Get-AudioFile [-FileInfo] <FileInfo> [<CommonParameters>]
```

## DESCRIPTION
The Get-AudioFile cmdlet gets objects that represent audio files. Audio files expose metadata, and can be manipulated in various ways by the other AudioWorks cmdlets.

## EXAMPLES

### Example 1: Get all audio files in the current directory with a specific extension
```powershell
PS C:\> $audioFiles = Get-AudioFile *.flac
```

Gets all the flac files in the current directory.

### Example 2: Get all audio files in a directory tree, recursively
```powershell
PS C:\> Get-ChildItem Music -Recurse -Filter *.flac | Get-AudioFile
```

Gets all the flac files in the 'Music' directory recursively.

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

### AudioWorks.Common.ITaggedAudioFile
## NOTES

## RELATED LINKS
