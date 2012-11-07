using System;
using System.Runtime.InteropServices;
using System.Text;

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
    }
}
