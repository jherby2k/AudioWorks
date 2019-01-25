/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using JetBrains.Annotations;
// ReSharper disable CommentTypo

namespace AudioWorks.Extensions.Apple
{
    enum AudioFileType : uint
    {
        M4A = 0x6D346166 // 'm4af'
    }

    enum AudioFilePropertyId
    {
        DataFormat = 0x64666D74, // 'dfmt'
        MagicCookieData = 0x6D676963, // 'mgic'
        PacketSizeUpperBound = 0x706B7562 // 'pkub'
    }

    [Flags]
    enum AudioFormatFlags : uint
    {
        Alac16BitSourceData = 0b00000001,
        Alac20BitSourceData = 0b00000010,
        Alac24BitSourceData = 0b00000011,
        Alac32BitSourceData = 0b00000100,
        PcmIsFloat = 0b00000001,
        PcmIsSignedInteger = 0b00000100,
        PcmIsAlignedHigh = 0b00010000
    }

    enum AudioFormat : uint
    {
        AacLowComplexity = 0x61616320, // "aac "
        AppleLossless = 0x616C6163, // "alac"
        LinearPcm = 0x6C70636D // "lpcm"
    }

    enum AudioConverterPropertyId : uint
    {
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
        [UsedImplicitly] UnspecifiedError = 0x7768743F, // 'wht?'
        [UsedImplicitly] UnsupportedFileTypeError = 0x7479703F, // 'typ?'
        [UsedImplicitly] UnsupportedDataFormatError = 0x666D743F, // 'fmt?'
        [UsedImplicitly] UnsupportedPropertyError = 0x7074793F, // 'pty?'
        [UsedImplicitly] BadPropertySizeError = 0x2173697A, // '!siz'
        [UsedImplicitly] PermissionsError = 0x70726D3F, // 'prm?'
        [UsedImplicitly] NotOptimizedError = 0x6F70746D, // 'optm'
        [UsedImplicitly] InvalidChunkError = 0x63686B3F, // 'chk?'
        [UsedImplicitly] DoesNotAllow64BitDataSizeError = 0x6F66663F, // 'off?'
        [UsedImplicitly] InvalidPacketOffsetError = 0x70636B3F, // 'pck?'
        [UsedImplicitly] InvalidFileError = 0x6474613F, // 'dta?'
        [UsedImplicitly] OperationNotSupportedError = 0x6F703F3F, // 'op??'
        [UsedImplicitly] NotOpenError = -38,
        EndOfFileError = -39,
        [UsedImplicitly] PositionError = -40,
        [UsedImplicitly] FileNotFoundError = -43
    }

    enum ExtendedAudioFilePropertyId
    {
        AudioConverter = 0x61636E76, // 'acnv'
        ClientDataFormat = 0x63666D74, // 'cfmt'
        ConverterConfig = 0x61636366 // 'accf'
    }

    enum ExtendedAudioFileStatus
    {
        Ok = 0,
        [UsedImplicitly] InvalidProperty = -66561,
        [UsedImplicitly] InvalidPropertySize = -66562,
        [UsedImplicitly] NonPcmClientFormat = -66563,
        [UsedImplicitly] InvalidChannelMap = -66564,
        [UsedImplicitly] InvalidOperationOrder = -665665,
        [UsedImplicitly] InvalidDataFormat = -66566,
        [UsedImplicitly] MaxPacketSizeUnknown = -66567,
        [UsedImplicitly] InvalidSeek = -66568,
        [UsedImplicitly] AsyncWriteTooLarge = -66569,
        [UsedImplicitly] AsyncWriteBufferOverflow = -66570
    }

    enum AudioConverterStatus
    {
        Ok = 0,
        [UsedImplicitly] FormatNotSupported = 0x666D743F, // 'fmt?'
        [UsedImplicitly] OperationNotSupported = 0x6F703F3F, // 'op??'
        [UsedImplicitly] PropertyNotSupported = 0x70726F70, // 'prop'
        [UsedImplicitly] InvalidInputSize = 0x696E737A, // 'insz'
        [UsedImplicitly] InvalidOutputSize = 0x6F74737A, // 'otsz'
        [UsedImplicitly] UnspecifiedError = 0x77686174, // 'what'
        [UsedImplicitly] BadPropertySizeError = 0x2173697A, // '!siz'
        [UsedImplicitly] RequiresPacketDescriptionsError = 0x21706B64, // '!pkd'
        [UsedImplicitly] InputSampleRateOutOfRange = 0x21697372, // '!isr'
        [UsedImplicitly] OutputSampleRateOutOfRange = 0x216F7372 // '!osr'
    }
}
