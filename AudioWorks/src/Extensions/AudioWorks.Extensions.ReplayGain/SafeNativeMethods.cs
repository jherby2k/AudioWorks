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
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace AudioWorks.Extensions.ReplayGain
{
    [SuppressUnmanagedCodeSecurity]
    static class SafeNativeMethods
    {
#if LINUX
        const string _ebur128Library = "libebur128.so.1";
#else
        const string _ebur128Library = "libebur128";
#endif

        [DllImport(_ebur128Library, EntryPoint = "ebur128_init",
            CallingConvention = CallingConvention.Cdecl)]
#if WINDOWS
        internal static extern StateHandle Init(uint channels, uint sampleRate, Modes modes);
#else
        internal static extern StateHandle Init(uint channels, ulong samplerate, Modes modes);
#endif


        [DllImport(_ebur128Library, EntryPoint = "ebur128_get_version",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern void GetVersion(
            out int major,
            out int minor,
            out int patch);

        [DllImport(_ebur128Library, EntryPoint = "ebur128_add_frames_float",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern Ebur128Error AddFramesFloat(
            StateHandle handle,
            in float source,
            UIntPtr frames);

        [DllImport(_ebur128Library, EntryPoint = "ebur128_sample_peak",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern Ebur128Error SamplePeak(
            StateHandle handle,
            uint channel,
            out double result);

        [DllImport(_ebur128Library, EntryPoint = "ebur128_true_peak",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern Ebur128Error TruePeak(
            StateHandle handle,
            uint channel,
            out double result);

        [DllImport(_ebur128Library, EntryPoint = "ebur128_loudness_global",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern Ebur128Error LoudnessGlobal(
            StateHandle handle,
            out double result);

        [DllImport(_ebur128Library, EntryPoint = "ebur128_loudness_global_multiple",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern Ebur128Error LoudnessGlobalMultiple(
            IntPtr[] handles,
            UIntPtr count,
            out double result);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_ebur128Library, EntryPoint = "ebur128_destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Destroy(ref IntPtr handle);
    }
}