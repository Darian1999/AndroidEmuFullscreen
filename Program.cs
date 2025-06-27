using System;
using System.Diagnostics;
using AndroidEmuFullscreen.FullscreenPatch;
using AndroidEmuFullscreen.util;

namespace AndroidEmuFullscreen
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Searching for QEMU process...");
            // The process name for Android Emulator's QEMU is usually "qemu-system-x86_64"
            Process[] qemuProcesses = Process.GetProcessesByName("qemu-system-x86_64");

            if (qemuProcesses.Length > 0)
            {
                // Use the first QEMU process found
                int processId = qemuProcesses[0].Id;
                Console.WriteLine($"Found QEMU process with ID: {processId}");
                ProcessUtils.AttachToProcessById(processId);

                if (ProcessUtils.ProcessHandle != IntPtr.Zero)
                {
                    Console.WriteLine("Successfully attached to QEMU process.");
                    ToFullscreen toFullscreen = new ToFullscreen();
                    toFullscreen.MakeProcessFullscreen();
                }
            }
            else
            {
                Console.WriteLine("QEMU process not found. Please ensure the Android Emulator is running.");
            }
        }
    }
}