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
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.ReplayGain
{
#pragma warning disable CA1060
    static partial class LibEbur128
#pragma warning restore CA1060
    {
#if LINUX
        const string _ebur128Library = "libebur128.so.1";
#else
        const string _ebur128Library = "libebur128";
#endif

        [LibraryImport(_ebur128Library, EntryPoint = "ebur128_init")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial StateHandle Init(uint channels, CULong samplerate, Modes modes);

        [LibraryImport(_ebur128Library, EntryPoint = "ebur128_get_version")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void GetVersion(out int major, out int minor, out int patch);

        [LibraryImport(_ebur128Library, EntryPoint = "ebur128_add_frames_float")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial Ebur128Error AddFramesFloat(StateHandle handle, in float source, UIntPtr frames);

        [LibraryImport(_ebur128Library, EntryPoint = "ebur128_sample_peak")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial Ebur128Error SamplePeak(StateHandle handle, uint channel, out double result);

        [LibraryImport(_ebur128Library, EntryPoint = "ebur128_true_peak")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial Ebur128Error TruePeak(StateHandle handle, uint channel, out double result);

        [LibraryImport(_ebur128Library, EntryPoint = "ebur128_loudness_global")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial Ebur128Error LoudnessGlobal(StateHandle handle, out double result);

        [LibraryImport(_ebur128Library, EntryPoint = "ebur128_loudness_global_multiple")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial Ebur128Error LoudnessGlobalMultiple(
            [In] IntPtr[] handles, UIntPtr count, out double result);

        [LibraryImport(_ebur128Library, EntryPoint = "ebur128_destroy")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        internal static partial void Destroy(ref IntPtr handle);
    }
}