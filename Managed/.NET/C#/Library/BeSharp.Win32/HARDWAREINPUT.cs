﻿using System;
using System.Runtime.InteropServices;

using DWORD = System.UInt32;
using WORD = System.UInt16;

namespace BeSharp.Win32
{
    /// <summary>
    /// Contains information about a simulated message generated by an input device other than a keyboard or mouse.
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms646269.aspx
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct HARDWAREINPUT
    {
        internal DWORD uMsg;
        internal WORD wParamL;
        internal WORD wParamH;
    }
}
