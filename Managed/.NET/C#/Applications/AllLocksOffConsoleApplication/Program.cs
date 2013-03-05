using System;
using BeSharp.Win32;

namespace AllLocksOffConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            ToggleVirtualKeyCode[] toggleVirtualKeyCodes = new ToggleVirtualKeyCode[] { ToggleVirtualKeyCode.Capital, ToggleVirtualKeyCode.Insert, ToggleVirtualKeyCode.NumLock, ToggleVirtualKeyCode.Scroll };
            foreach (ToggleVirtualKeyCode toggleVirtualKeyCode in toggleVirtualKeyCodes)
            {
                Keyboard.UnlockToggleKey(toggleVirtualKeyCode);
            }
        }
    }
}
