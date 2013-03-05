using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeSharp.Win32
{
    public enum ToggleVirtualKeyCode: ushort // WORD because of SendInput
    {
        Capital = VirtualKeyCode.VK_CAPITAL,
        Insert = VirtualKeyCode.VK_INSERT,
        NumLock = VirtualKeyCode.VK_NUMLOCK,
        Scroll = VirtualKeyCode.VK_SCROLL,
    }
}
