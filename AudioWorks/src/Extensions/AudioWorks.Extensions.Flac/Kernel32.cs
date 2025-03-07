﻿/* Copyright © 2025 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Flac
{
    [SuppressMessage("Design", "CA1060:Move pinvokes to native methods class",
        Justification = "Following latest native interop best practices")]
    static partial class Kernel32
    {
        const string _kernelLibrary = "kernel32";

        [LibraryImport(_kernelLibrary, EntryPoint = "LoadLibraryW")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static partial nint LoadLibrary([MarshalAs(UnmanagedType.LPWStr)] string dllToLoad);

        [LibraryImport(_kernelLibrary)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool FreeLibrary(nint module);

        [LibraryImport(_kernelLibrary)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static partial nint GetProcAddress(nint module, [MarshalAs(UnmanagedType.LPStr)] string name);
    }
}
