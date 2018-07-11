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