using System;
using System.Runtime.InteropServices;

using DWORD = System.UInt32;
using WORD = System.UInt16;
using ULONG_PTR = System.IntPtr;

namespace BeSharp.Win32
{
    /// <summary>
    /// Contains information about a simulated keyboard event.
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms646271.aspx
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct KEYBDINPUT
    {
        internal VirtualKeyCode wVk; // WORD
        internal WScan wScan; // WORD 
        internal DWFlags dwFlags; // DWORD
        internal DWORD time;
        internal ULONG_PTR dwExtraInfo;

        /// <summary>
        /// Physical keyboard scancodes
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms646267.aspx#scan_code
        /// http://msdn.microsoft.com/en-us/library/aa299374
        /// https://en.wikipedia.org/wiki/Scancode
        /// http://www.win.tue.nl/~aeb/linux/kbd/scancodes-10.html#ss10.6
        /// http://www.pinvoke.net/default.aspx/Structures/KEYBDINPUT.html
        /// </summary>
        internal enum WScan : ushort // WORD
        {
            // TODO ##jwp find a good table that has close to 100% coverage.
            // http://www.win.tue.nl/~aeb/linux/kbd/scancodes-10.html#ss10.6
        }

        [Flags]
        internal enum DWFlags : uint
        {
            KEYEVENTF_EXTENDEDKEY = 0x0001,
            KEYEVENTF_KEYUP = 0x0002,
            KEYEVENTF_SCANCODE = 0x0008,
            KEYEVENTF_UNICODE = 0x0004,
        }
    }
}
