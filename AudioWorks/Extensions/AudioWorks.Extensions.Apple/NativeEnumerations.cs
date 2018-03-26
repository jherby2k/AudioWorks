using System;

namespace AudioWorks.Extensions.Apple
{
    enum AudioFileType : uint
    {
        None,
        M4A = 0x6D346166 // 'm4af'
    }

    enum AudioFilePropertyId
    {
        None,
        DataFormat = 0x64666D74,           // 'dfmt'
        MagicCookieData = 0x6D676963,      // 'mgic'
        PacketSizeUpperBound = 0x706B7562  // 'pkub'
    }

    [Flags]
    enum AudioFormatFlags : uint
    {
        Alac16BitSourceData = 0b00000001,
        Alac20BitSourceData = 0b00000010,
        Alac24BitSourceData = 0b00000011,
        Alac32BitSourceData = 0b00000100,
        PcmIsSignedInteger = 0b00000100,
        PcmIsPacked = 0b00001000,
        PcmIsAlignedHigh = 0b00010000
    }

    enum AudioFormat : uint
    {
        None,
        AacLowComplexity = 0x61616320, // "aac "
        AppleLossless = 0x616C6163,    // "alac"
        LinearPcm = 0x6C70636D         // "lpcm"
    }

    enum AudioConverterPropertyId : uint
    {
        None,
        DecompressionMagicCookie = 0x646D6763, // 'dmgc'
        CodecQuality = 0x63647175, // 'cdqu'
        BitRate = 0x62726174, // 'brat'
        BitRateControlMode = 0x61636266, // 'acbf'
        VbrQuality = 0x76627271 // 'vbrq'
    }

    enum BitrateControlMode : uint
    {
        Constant = 0,
        LongTermAverage = 1,
        VariableConstrained = 2,
        Variable = 3
    }

    enum AudioFileStatus
    {
        Ok = 0,
        UnspecifiedError = 0x7768743F,               // 'wht?'
        UnsupportedFileTypeError = 0x7479703F,       // 'typ?'
        UnsupportedDataFormatError = 0x666D743F,     // 'fmt?'
        UnsupportedPropertyError = 0x7074793F,       // 'pty?'
        BadPropertySizeError = 0x2173697A,           // '!siz'
        PermissionsError = 0x70726D3F,               // 'prm?'
        NotOptimizedError = 0x6F70746D,              // 'optm'
        InvalidChunkError = 0x63686B3F,              // 'chk?'
        DoesNotAllow64BitDataSizeError = 0x6F66663F, // 'off?'
        InvalidPacketOffsetError = 0x70636B3F,       // 'pck?'
        InvalidFileError = 0x6474613F,               // 'dta?'
        OperationNotSupportedError = 0x6F703F3F,     // 'op??'
        NotOpenError = -38,
        EndOfFileError = -39,
        PositionError = -40,
        FileNotFoundError = -43
    }

    enum ExtendedAudioFilePropertyId
    {
        AudioConverter = 0x61636E76, // 'acnv'
        ClientDataFormat = 0x63666D74, // 'cfmt'
        ConverterConfig = 0x61636366 // 'accf'
    }

    enum ExtendedAudioFileStatus
    {
        Ok                          = 0,
        InvalidProperty             = -66561,
        InvalidPropertySize         = -66562,
        NonPcmClientFormat          = -66563,
        InvalidChannelMap           = -66564,
        InvalidOperationOrder       = -665665,
        InvalidDataFormat           = -66566,
        MaxPacketSizeUnknown        = -66567,
        InvalidSeek                 = -66568,
        AsyncWriteTooLarge          = -66569,
        AsyncWriteBufferOverflow    = -66570
    }

    enum AudioConverterStatus
    {
        Ok = 0,
        FormatNotSupported = 0x666D743F,              // 'fmt?'
        OperationNotSupported = 0x6F703F3F,           // 'op??'
        PropertyNotSupported = 0x70726F70,            // 'prop'
        InvalidInputSize = 0x696E737A,                // 'insz'
        InvalidOutputSize = 0x6F74737A,               // 'otsz'
        UnspecifiedError = 0x77686174,                // 'what'
        BadPropertySizeError = 0x2173697A,            // '!siz'
        RequiresPacketDescriptionsError = 0x21706B64, // '!pkd'
        InputSampleRateOutOfRange = 0x21697372,       // '!isr'
        OutputSampleRateOutOfRange = 0x216F7372       // '!osr'
    }
}
