using JetBrains.Annotations;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace AudioWorks.Extensions.ReplayGain
{
    [SuppressUnmanagedCodeSecurity]
    static class SafeNativeMethods
    {
        const string _winEbur128Library = "ebur128.dll";
        const string _linuxEbur128Library = "libebur128.so.0";

        static SafeNativeMethods()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Select an architecture-appropriate directory by prefixing the PATH variable:
                var newPath = new StringBuilder(
                    Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath));
                newPath.Append(Path.DirectorySeparatorChar);
                newPath.Append(Environment.Is64BitProcess ? "x64" : "x86");
                newPath.Append(Path.PathSeparator);
                newPath.Append(Environment.GetEnvironmentVariable("PATH"));

                Environment.SetEnvironmentVariable("PATH", newPath.ToString());
            }
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        internal static StateHandle Initialize(
            uint channels,
            uint samplerate,
            Modes modes)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WinInit(channels, samplerate, modes);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return LinuxInit(channels, samplerate, modes);

            throw new NotImplementedException();
        }

        internal static void AddFramesFloat(
            [NotNull] StateHandle handle,
            float[] source,
            uint count)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinAddFramesFloat(handle, source, new UIntPtr(count));
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxAddFramesFloat(handle, source, new UIntPtr(count));
                return;
            }

            throw new NotImplementedException();
        }

        internal static void TruePeak(
            [NotNull] StateHandle handle,
            uint channel,
            out double result)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinTruePeak(handle, channel, out result);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxTruePeak(handle, channel, out result);
                return;
            }

            throw new NotImplementedException();
        }

        internal static void SamplePeak(
            [NotNull] StateHandle handle,
            uint channel,
            out double result)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinSamplePeak(handle, channel, out result);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxSamplePeak(handle, channel, out result);
                return;
            }

            throw new NotImplementedException();
        }

        internal static void LoudnessGlobal(
            [NotNull] StateHandle handle,
            out double result)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinLoudnessGlobal(handle, out result);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxLoudnessGlobal(handle, out result);
                return;
            }

            throw new NotImplementedException();
        }

        internal static void LoudnessGlobalMultiple(
            [NotNull] StateHandle[] handles,
            out double result)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinLoudnessGlobalMultiple(
                    handles.Select(handle => handle.DangerousGetHandle()).ToArray(),
                    new UIntPtr((uint) handles.Length), out result);
                return;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxLoudnessGlobalMultiple(
                    handles.Select(handle => handle.DangerousGetHandle()).ToArray(),
                    new UIntPtr((uint) handles.Length), out result);
                return;
            }

            throw new NotImplementedException();
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        internal static void Destroy(
            ref IntPtr handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                WinDestroy(ref handle);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                LinuxDestroy(ref handle);
        }

        [DllImport(_winEbur128Library, EntryPoint = "ebur128_init",
            CallingConvention = CallingConvention.Cdecl)]
        static extern StateHandle WinInit(uint channels, uint samplerate, Modes modes);

        [DllImport(_linuxEbur128Library, EntryPoint = "ebur128_init",
            CallingConvention = CallingConvention.Cdecl)]
        static extern StateHandle LinuxInit(uint channels, uint samplerate, Modes modes);

        [DllImport(_winEbur128Library, EntryPoint = "ebur128_add_frames_float",
            CallingConvention = CallingConvention.Cdecl)]
        static extern Ebur128Error WinAddFramesFloat([NotNull] StateHandle handle, float[] source, UIntPtr count);

        [DllImport(_linuxEbur128Library, EntryPoint = "ebur128_add_frames_float",
            CallingConvention = CallingConvention.Cdecl)]
        static extern Ebur128Error LinuxAddFramesFloat([NotNull] StateHandle handle, float[] source, UIntPtr count);

        [DllImport(_winEbur128Library, EntryPoint = "ebur128_true_peak",
            CallingConvention = CallingConvention.Cdecl)]
        static extern Ebur128Error WinTruePeak([NotNull] StateHandle handle, uint channel, out double result);

        [DllImport(_linuxEbur128Library, EntryPoint = "ebur128_true_peak",
            CallingConvention = CallingConvention.Cdecl)]
        static extern Ebur128Error LinuxTruePeak([NotNull] StateHandle handle, uint channel, out double result);

        [DllImport(_winEbur128Library, EntryPoint = "ebur128_sample_peak",
            CallingConvention = CallingConvention.Cdecl)]
        static extern Ebur128Error WinSamplePeak([NotNull] StateHandle handle, uint channel, out double result);

        [DllImport(_linuxEbur128Library, EntryPoint = "ebur128_sample_peak",
            CallingConvention = CallingConvention.Cdecl)]
        static extern Ebur128Error LinuxSamplePeak([NotNull] StateHandle handle, uint channel, out double result);

        [DllImport(_winEbur128Library, EntryPoint = "ebur128_loudness_global",
            CallingConvention = CallingConvention.Cdecl)]
        static extern Ebur128Error WinLoudnessGlobal([NotNull] StateHandle handle, out double result);

        [DllImport(_linuxEbur128Library, EntryPoint = "ebur128_loudness_global",
            CallingConvention = CallingConvention.Cdecl)]
        static extern Ebur128Error LinuxLoudnessGlobal([NotNull] StateHandle handle, out double result);

        [DllImport(_winEbur128Library, EntryPoint = "ebur128_loudness_global_multiple",
            CallingConvention = CallingConvention.Cdecl)]
        static extern Ebur128Error WinLoudnessGlobalMultiple(
            [NotNull] IntPtr[] handles,
            UIntPtr count,
            out double result);

        [DllImport(_linuxEbur128Library, EntryPoint = "ebur128_loudness_global_multiple",
            CallingConvention = CallingConvention.Cdecl)]
        static extern Ebur128Error LinuxLoudnessGlobalMultiple(
            [NotNull] IntPtr[] handles,
            UIntPtr count,
            out double result);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_winEbur128Library, EntryPoint = "ebur128_destroy",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void WinDestroy(ref IntPtr handle);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(_linuxEbur128Library, EntryPoint = "ebur128_destroy",
            CallingConvention = CallingConvention.Cdecl)]
        static extern void LinuxDestroy(ref IntPtr handle);
    }
}