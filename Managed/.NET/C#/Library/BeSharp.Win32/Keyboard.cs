using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace BeSharp.Win32
{
    public class Keyboard
    {
        public static void PressReleaseToggleKey(ToggleVirtualKeyCode value)
        {
            VirtualKeyCode virtualKeyCode = (VirtualKeyCode)value; // we can do this safely: VirtualKeyCode is a superset of ToggleVirtualKeyCode
            pressReleaseKey(virtualKeyCode);
        }

        internal static void pressReleaseKey(VirtualKeyCode value)
        {
            INPUT pressInput = new INPUT();
            pressInput.type = INPUT.Type.INPUT_KEYBOARD;
            pressInput.u.ki.wVk = value;

            INPUT releaseInput = pressInput;
            releaseInput.u.ki.dwFlags = KEYBDINPUT.DWFlags.KEYEVENTF_KEYUP;

            INPUT[] pressReleaseInputs = new INPUT[] { pressInput, releaseInput };

            uint length = (uint)pressReleaseInputs.Length;
            uint result = User32Dll.SendInput(length, pressReleaseInputs, INPUT.Size);

            if (length != result)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        public static void UnlockToggleKey(ToggleVirtualKeyCode value)
        {
            if (IsToggleKeyLocked(value))
                PressReleaseToggleKey(value);
        }

        public static void LockToggleKey(ToggleVirtualKeyCode value)
        {
            if (!IsToggleKeyLocked(value))
                PressReleaseToggleKey(value);
        }

        /// <summary>
        /// From System.Windows.Forms.Control.IsKeyLocked, with ToggleVirtualKeyCode subset of VirtualKeyCode 
        /// </summary>
        /// <param name="value">VirtualKeyCode that can have a toggle.</param>
        /// <returns>If toggle key was locked.</returns>
        public static bool IsToggleKeyLocked(ToggleVirtualKeyCode value)
        {
            // Detecting this with GetAsyncKeyState fails, though The Old New Thing indicates it should: http://blogs.msdn.com/b/oldnewthing/archive/2004/11/30/272262.aspx 
            //short asyncKeyState = User32Dll.GetAsyncKeyState((int)value);
            short keyState = User32Dll.GetKeyState((int)value);
            if ((value != ToggleVirtualKeyCode.Insert) && (value != ToggleVirtualKeyCode.Capital))
            {
                return ((keyState & 0x8001) != 0); // Insert, Captial have both bits set
            }
            return ((keyState & 1) != 0); // Scroll, NumLock have only lower bit set
        }
    }
}