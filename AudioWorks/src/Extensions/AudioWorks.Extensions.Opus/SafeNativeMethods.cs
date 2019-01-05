/* Copyright © 2019 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace AudioWorks.Extensions.Opus
{
    [SuppressUnmanagedCodeSecurity]
    static class SafeNativeMethods
    {
#if LINUX
        const string _oggLibrary = "libogg.so.0";
        const string _opusLibrary = "libopus.so.0";
#else
        //const string _oggLibrary = "libogg";
        const string _opusLibrary = "libopus";
#endif

        [DllImport(_opusLibrary, EntryPoint = "opus_get_version_string",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr OpusGetVersion();
    }
}
