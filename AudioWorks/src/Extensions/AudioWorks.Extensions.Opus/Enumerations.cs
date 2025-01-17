﻿/* Copyright © 2019 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

namespace AudioWorks.Extensions.Opus
{
    enum EncoderControlRequest
    {
        SetSignal = 4000,
        SetBitRate = 4002,
        GetBitRate = 4003,
        SetVbr = 4006,
        SetVbrConstraint = 4020,
        SetLsbDepth = 4036,
        SetSerialNumber = 14006,
        SetHeaderGain = 14010
    }

    enum SignalType
    {
        Speech = 2048,
        Music = 2049
    }
}
