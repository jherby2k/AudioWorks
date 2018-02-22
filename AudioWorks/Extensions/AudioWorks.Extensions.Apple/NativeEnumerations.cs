using System;

namespace AudioWorks.Extensions.Apple
{
    enum AudioFileType : uint
    {
        None,
        M4A = 0x6d346166 // 'm4af'
    }

    enum AudioFilePropertyId
    {
        None,
        DataFormat = 0x64666d74,           // 'dfmt'
        MagicCookieData = 0x6d676963,      // 'mgic'
        PacketSizeUpperBound = 0x706b7562  // 'pkub'
    }

    [Flags]
    enum AudioFormatFlags : uint
    {
        Alac16BitSourceData = 0b0000_0001,
        Alac20BitSourceData = 0b0000_0010,
        Alac24BitSourceData = 0b0000_0011,
        Alac32BitSourceData = 0b0000_0100,
        PcmIsSignedInteger = 0b0000_0100,
        PcmIsAlignedHigh = 0b0001_0000
    }

    enum AudioFormat : uint
    {
        None,
        AppleLossless = 0x616c6163,    // "alac"
        LinearPcm = 0x6c70636d         // "lpcm"
    }

    enum AudioConverterPropertyId : uint
    {
        None,
        DecompressionMagicCookie = 0x646d6763 // 'dmgc'
    }

    enum AudioFileStatus
    {
        Ok = 0,
        UnspecifiedError = 0x7768743f,               // 'wht?'
        UnsupportedFileTypeError = 0x7479703f,       // 'typ?'
        UnsupportedDataFormatError = 0x666d743f,     // 'fmt?'
        UnsupportedPropertyError = 0x7074793f,       // 'pty?'
        BadPropertySizeError = 0x2173697a,           // '!siz'
        PermissionsError = 0x70726d3f,               // 'prm?'
        NotOptimizedError = 0x6f70746d,              // 'optm'
        InvalidChunkError = 0x63686b3f,              // 'chk?'
        DoesNotAllow64BitDataSizeError = 0x6f66663f, // 'off?'
        InvalidPacketOffsetError = 0x70636b3f,       // 'pck?'
        InvalidFileError = 0x6474613f,               // 'dta?'
        OperationNotSupportedError = 0x6f703f3f,     // 'op??'
        NotOpenError = -38,
        EndOfFileError = -39,
        PositionError = -40,
        FileNotFoundError = -43
    }

    enum ExtendedAudioFilePropertyId
    {
        AudioConverter = 0x61636e76, // 'acnv'
        ClientDataFormat = 0x63666d74, // 'cfmt'
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
        FormatNotSupported = 0x666d743f,              // 'fmt?'
        OperationNotSupported = 0x6f703f3f,           // 'op??'
        PropertyNotSupported = 0x70726f70,            // 'prop'
        InvalidInputSize = 0x696e737a,                // 'insz'
        InvalidOutputSize = 0x6f74737a,               // 'otsz'
        UnspecifiedError = 0x77686174,                // 'what'
        BadPropertySizeError = 0x2173697a,            // '!siz'
        RequiresPacketDescriptionsError = 0x21706b64, // '!pkd'
        InputSampleRateOutOfRange = 0x21697372,       // '!isr'
        OutputSampleRateOutOfRange = 0x216f7372       // '!osr'
    }
}
