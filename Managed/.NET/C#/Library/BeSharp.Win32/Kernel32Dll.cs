using System;
using System.Runtime.InteropServices;

namespace BeSharp.Win32
{
    internal class Kernel32Dll
    {
        private const string kernel32_dll = "kernel32.dll";

        [DllImport(kernel32_dll)]
        public static extern bool FreeLibrary(IntPtr hModule);

        /// <summary>
        /// Do not call FreeLibrary on a handle obtained from GetModuleHandle
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms683199(v=vs.85).aspx
        /// </summary>
        /// <param name="lpModuleName"></param>
        /// <returns></returns>
        [DllImport(kernel32_dll, CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport(kernel32_dll, CharSet = CharSet.Auto)]
        public static extern IntPtr LoadLibrary(string lpModuleName);
    }
}
