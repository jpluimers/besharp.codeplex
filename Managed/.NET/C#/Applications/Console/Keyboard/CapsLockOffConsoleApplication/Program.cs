using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapsLockOffConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            BeSharp.Win32.Keyboard.UnlockToggleKey(BeSharp.Win32.ToggleVirtualKeyCode.Capital);
        }
    }
}
