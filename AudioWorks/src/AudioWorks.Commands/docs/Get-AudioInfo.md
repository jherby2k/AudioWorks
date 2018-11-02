---
external help file: AudioWorks.Commands.dll-Help.xml
Module Name: AudioWorks.Commands
online version:
schema: 2.0.0
---

# Get-AudioInfo

## SYNOPSIS
Gets information about an audio file.

## SYNTAX

```
Get-AudioInfo [-AudioFile] <IAudioFile> [<CommonParameters>]
```

## DESCRIPTION
The Get-AudioInfo cmdlet gets information about audio files.
This consists of immutable information that can't be changed without re-encoding the file.

## EXAMPLES

### Example 1
```powershell
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -AudioFile
Specifies the audio file.

```yaml
Type: IAudioFile
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

### AudioWorks.Common.IAudioFile
Specifies the audio file.

## OUTPUTS

### AudioWorks.Common.AudioInfo
## NOTES

## RELATED LINKS
