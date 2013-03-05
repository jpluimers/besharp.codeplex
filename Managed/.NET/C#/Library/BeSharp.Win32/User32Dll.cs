using System;
using System.Runtime.InteropServices;
using System.Text;

using UINT = System.UInt32;
using INT = System.Int32;

namespace BeSharp.Win32
{
    internal class User32Dll
    {
        internal const string user32_dll = "user32.dll";

        [DllImport(user32_dll, CharSet = CharSet.Auto)]
        public static extern int LoadString(IntPtr hInstance, uint uID, StringBuilder lpBuffer, int nBufferMax);

        public static string LoadString(string libraryName, uint Ident, string DefaultText)
        {
            IntPtr libraryHandle = Kernel32Dll.GetModuleHandle(libraryName);
            if (libraryHandle != IntPtr.Zero)
            {
                StringBuilder sb = new StringBuilder(1024);
                int size = LoadString(libraryHandle, Ident, sb, 1024);
                if (size > 0)
                    return sb.ToString();
                else
                    return DefaultText;
            }
            else
            {
                return DefaultText;
            }
        }

        /// <summary>
        /// Synchronously retrieves the status of the specified virtual key. The status specifies whether the key is up, down, or toggled (on, off—alternating each time the key is pressed).
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms646301
        /// http://blogs.msdn.com/b/oldnewthing/archive/2004/11/30/272262.aspx
        /// </summary>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        [DllImport(user32_dll, CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern short GetKeyState(int keyCode);

        /// <summary>
        /// Determines whether a key is up or down at the time the function is called.
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms646293
        /// </summary>
        /// <param name="keyCode">The virtual-key code. http://msdn.microsoft.com/en-us/library/windows/desktop/dd375731 </param>
        /// <returns>If the most significant bit is set, the key is down.</returns>
        [DllImport(user32_dll, CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern short GetAsyncKeyState(int keyCode);

        /// <summary>
        /// Synthesizes keystrokes, mouse motions, and button clicks.
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms646310.aspx
        /// </summary>
        [DllImport(user32_dll, SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern uint SendInput(
            UINT nInputs,
            [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs,
            INT cbSize);
    }
}
