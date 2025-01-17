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

#if !WINDOWS
using System;
using System.Runtime.InteropServices;

namespace AudioWorks.Extensions.Flac
{
#pragma warning disable CA1060
    static partial class LibDL
#pragma warning restore CA1060
    {
#if LINUX
        const string _dlLibrary = "libdl.so.2";
#else
        const string _dlLibrary = "libdl";
#endif

        [LibraryImport(_dlLibrary, EntryPoint = "dlopen", StringMarshalling = StringMarshalling.Utf8)]
        internal static partial IntPtr Open(string filename, int flags);

        [LibraryImport(_dlLibrary, EntryPoint = "dlclose")]
        internal static partial int Close(IntPtr handle);

        [LibraryImport(_dlLibrary, EntryPoint = "dlsym", StringMarshalling = StringMarshalling.Utf8)]
        internal static partial IntPtr Sym(IntPtr handle, string symbol);
    }
}
#endif