using System;
using System.Runtime.InteropServices;

using DWORD = System.UInt32; // http://stackoverflow.com/questions/745425/in-c-how-to-declare-dword-as-an-uint32
using LONG = System.Int32; // http://www.codeproject.com/Articles/9714/Win32-API-C-to-NET
using ULONG_PTR = System.IntPtr; // http://stackoverflow.com/questions/1939889/c-sharp-ulong-ptr-equivelent

/// always wrap your PInvoke data types through an intermediate layer. Do NOT expose them to the outside world
/// http://stackoverflow.com/questions/13285459/modify-struct-layout-from-p-invoke
/// be very carefull with BOOL: http://stackoverflow.com/questions/1602487/what-am-i-doing-wrong-with-this-use-of-structlayout-layoutkind-explicit-when
/// calling conventions http://stackoverflow.com/questions/654734/c-sharp-p-invoke-structure-problem
namespace BeSharp.Win32
{
    /// <summary>
    /// Contains information about a simulated mouse event.
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms646273.aspx
    /// http://blogs.msdn.com/b/oldnewthing/archive/2009/08/13/9867383.aspx
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct MOUSEINPUT
    {
        internal LONG dx;
        internal LONG dy;
        internal MouseData mouseData; // DWORD
        internal DWFlags dwFlags; // DWORD
        internal DWORD time;
        internal ULONG_PTR dwExtraInfo; // http://stackoverflow.com/questions/3448267/pinvokestackimbalance-invoking-unmanaged-code-from-hookcallback/3448357#3448357

        [Flags]
        internal enum MouseData : uint // cannot be DWORD nor System.UInt32
        {
            Nothing = 0x0000,
            XBUTTON1 = 0x0001,
            XBUTTON2 = 0x0002,
        }

        [Flags]
        internal enum DWFlags : uint // TODO ##jpl: why can't this be DWORD?
        {
            MOUSEEVENTF_ABSOLUTE = 0x8000,
            MOUSEEVENTF_HWHEEL = 0x01000,
            MOUSEEVENTF_MOVE = 0x0001,
            MOUSEEVENTF_MOVE_NOCOALESCE = 0x2000,
            MOUSEEVENTF_LEFTDOWN = 0x0002,
            MOUSEEVENTF_LEFTUP = 0x0004,
            MOUSEEVENTF_RIGHTDOWN = 0x0008, 
            MOUSEEVENTF_RIGHTUP = 0x0010,
            MOUSEEVENTF_MIDDLEDOWN = 0x0020,
            MOUSEEVENTF_MIDDLEUP = 0x0040,
            MOUSEEVENTF_VIRTUALDESK = 0x4000,
            MOUSEEVENTF_WHEEL = 0x0800,
            MOUSEEVENTF_XDOWN = 0x0080,
            MOUSEEVENTF_XUP = 0x0100,
        }
    }
}