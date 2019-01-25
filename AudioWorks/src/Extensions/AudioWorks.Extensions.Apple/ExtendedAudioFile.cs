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

using System.IO;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace AudioWorks.Extensions.Apple
{
    sealed class ExtendedAudioFile : AudioFile
    {
        [NotNull] readonly ExtendedAudioFileHandle _handle;

        public ExtendedAudioFile(
            AudioStreamBasicDescription description,
            AudioFileType fileType,
            [NotNull] Stream stream)
            : base(description, fileType, stream)
        {
            SafeNativeMethods.ExtAudioFileWrapAudioFile(Handle, true, out _handle);
        }

        internal void SetProperty<T>(ExtendedAudioFilePropertyId id, T value) where T : unmanaged
        {
            var unmanagedValueSize = Marshal.SizeOf(typeof(T));
            var unmanagedValue = Marshal.AllocHGlobal(unmanagedValueSize);
            try
            {
                Marshal.StructureToPtr(value, unmanagedValue, false);
                SafeNativeMethods.ExtAudioFileSetProperty(_handle, id, (uint) unmanagedValueSize, unmanagedValue);
            }
            finally
            {
                Marshal.FreeHGlobal(unmanagedValue);
            }
        }

        internal T GetProperty<T>(ExtendedAudioFilePropertyId id) where T : unmanaged
        {
            var unmanagedValueSize = (uint) Marshal.SizeOf(typeof(T));
            var unmanagedValue = Marshal.AllocHGlobal((int) unmanagedValueSize);
            try
            {
                SafeNativeMethods.ExtAudioFileGetProperty(_handle, id, ref unmanagedValueSize, unmanagedValue);
                return Marshal.PtrToStructure<T>(unmanagedValue);
            }
            finally
            {
                Marshal.FreeHGlobal(unmanagedValue);
            }
        }

        internal ExtendedAudioFileStatus Write(AudioBufferList data, uint frames)
        {
            return SafeNativeMethods.ExtAudioFileWrite(_handle, frames, ref data);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _handle.Dispose();
            base.Dispose(disposing);
        }
    }
}