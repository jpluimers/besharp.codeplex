using System;
using System.Runtime.InteropServices;

using DWORD = System.UInt32;

namespace BeSharp.Win32
{
    /// <summary>
    /// Used by SendInput to store information for synthesizing input events such as keystrokes, mouse movement, and mouse clicks.
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms646270.aspx
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct INPUT
    {
        internal Type type; // DWORD
        internal INPUT_UNION u;

        internal enum Type : uint // DWORD
        {
            /// <summary>
            /// The event is a mouse event. Use the mi structure of the union.
            /// </summary>
            INPUT_MOUSE = 0,
            /// <summary>
            /// The event is a keyboard event. Use the ki structure of the union.
            /// </summary>
            INPUT_KEYBOARD = 1,
            /// <summary>
            /// The event is a hardware event. Use the hi structure of the union.
            /// </summary>
            INPUT_HARDWARE = 2,
        }

        public static readonly int Size = Marshal.SizeOf(typeof(INPUT));
    }

    /// <summary>
    /// This generates the anonymous union
    /// http://blogs.msdn.com/b/oldnewthing/archive/2009/08/13/9867383.aspx
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    internal struct INPUT_UNION
    {
        [FieldOffset(0)]
        internal MOUSEINPUT mi;
        [FieldOffset(0)]
        internal KEYBDINPUT ki;
        [FieldOffset(0)]
        internal HARDWAREINPUT hi;
    };
}
