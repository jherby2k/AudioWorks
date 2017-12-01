using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using JetBrains.Annotations;
#if (WINDOWS)
using System.IO;
using System.Reflection;
using System.Text;
#endif

namespace AudioWorks.Extensions.ReplayGain
{
    [SuppressUnmanagedCodeSecurity]
    static class SafeNativeMethods
    {
        const string _ebur128Library = "libebur128";

#if (WINDOWS)
        static SafeNativeMethods()
        {
            // Select an architecture-appropriate directory by prefixing the PATH variable
            var newPath = new StringBuilder(
                Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath));
            newPath.Append(Path.DirectorySeparatorChar);
            newPath.Append(Environment.Is64BitProcess ? "x64" : "x86");
            newPath.Append(Path.PathSeparator);
            newPath.Append(Environment.GetEnvironmentVariable("PATH"));

            Environment.SetEnvironmentVariable("PATH", newPath.ToString());
        }

        [DllImport(_ebur128Library, EntryPoint = "ebur128_init",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern StateHandle Init(uint channels, uint samplerate, Modes modes);
#else
        [DllImport(_ebur128Library, EntryPoint = "ebur128_init",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern StateHandle Init(uint channels, ulong samplerate, Modes modes);
#endif

        [DllImport(_ebur128Library, EntryPoint = "ebur128_add_frames_float",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern Ebur128Error AddFramesFloat([NotNull] StateHandle handle, float[] source, UIntPtr count);

        [Pure]
        [DllImport(_ebur128Library, EntryPoint = "ebur128_sample_peak",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern Ebur128Error SamplePeak([NotNull] StateHandle handle, uint channel, out double result);

        [Pure]
        [DllImport(_ebur128Library, EntryPoint = "ebur128_true_peak",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern Ebur128Error TruePeak([NotNull] StateHandle handle, uint channel, out double result);

        [Pure]
        [DllImport(_ebur128Library, EntryPoint = "ebur128_loudness_global",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern Ebur128Error LoudnessGlobal([NotNull] StateHandle handle, out double result);

        [Pure]
        [DllImport(_ebur128Library, EntryPoint = "ebur128_loudness_global_multiple",
            CallingConvention = CallingConvention.Cdecl)]
        internal static extern Ebur128Error LoudnessGlobalMultiple(
            [NotNull] IntPtr[] handles,
            UIntPtr count,
            out double result);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_ebur128Library, EntryPoint = "ebur128_destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Destroy(ref IntPtr handle);
    }
}