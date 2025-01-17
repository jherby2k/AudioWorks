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

#if WINDOWS
using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Flac
{
#pragma warning disable CA1060
    static partial class Kernel32
#pragma warning restore CA1060
    {
        const string _kernelLibrary = "kernel32";

        [LibraryImport(_kernelLibrary, EntryPoint = "LoadLibraryA", StringMarshalling = StringMarshalling.Utf8)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static partial IntPtr LoadLibrary(string dllToLoad);

        [LibraryImport(_kernelLibrary)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool FreeLibrary(IntPtr module);

        [LibraryImport(_kernelLibrary, StringMarshalling = StringMarshalling.Utf8)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static partial IntPtr GetProcAddress(IntPtr module, string name);
    }
}
#endif