using System;
using System.ComponentModel;

namespace BeSharp.IO.Ports
{
    /// <summary>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/aa365247#file_and_directory_names
    /// Do not use the following reserved device names for the name of a file:
    /// CON, PRN, AUX, NUL, COM1, COM2, COM3, COM4, COM5, COM6, COM7, COM8, COM9, LPT1, LPT2, LPT3, LPT4, LPT5, LPT6, LPT7, LPT8, and LPT9. Also avoid these names followed immediately by an extension; for example, NUL.txt is not recommended. For more information, see Namespaces.
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/aa365247#namespaces
    /// http://support.microsoft.com/kb/74496 (AUX, PRN, and other DOS device names)
    /// </summary>
    public static class ReservedDeviceNames
    {
        public const string NULL = NUL;
        [Description("System real-time clock")]
        public const string CLOCK = "CLOCK$";

        [Description("Keyboard and display")]
        public const string CON = "CON";
        [Description("System list device, usually a parallel port")]
        public const string PRN = "PRN";
        [Description("Auxiliary device, usually a serial port")]
        public const string AUX = "AUX";
        [Description("Bit-bucket device")]
        public const string NUL = "NUL";

        [Description("First serial communications port")]
        public const string COM1 = "COM1";
        [Description("Second serial communications port")]
        public const string COM2 = "COM2";
        [Description("Third serial communications port")]
        public const string COM3 = "COM3";
        [Description("Fourth serial communications port")]
        public const string COM4 = "COM4";
        public const string COM5 = "COM5";
        public const string COM6 = "COM6";
        public const string COM7 = "COM7";
        public const string COM8 = "COM8";
        public const string COM9 = "COM9";

        [Description("First parallel printer port")]
        public const string LPT1 = "LPT1";
        [Description("Second parallel printer port")]
        public const string LPT2 = "LPT2";
        [Description("Third parallel printer port")]
        public const string LPT3 = "LPT3";
        public const string LPT4 = "LPT4";
        public const string LPT5 = "LPT5";
        public const string LPT6 = "LPT6";
        public const string LPT7 = "LPT7";
        public const string LPT8 = "LPT8";
        public const string LPT9 = "LPT9";

        public readonly static string[] AllDeviceNames = { CLOCK, CON, PRN, AUX, NUL, COM1, COM2, COM3, COM4, COM5, COM6, COM7, COM8, COM9, LPT1, LPT2, LPT3, LPT4, LPT5, LPT6, LPT7, LPT8, LPT9 };
        public readonly static string[] ParallelDeviceNames = { PRN, LPT1, LPT2, LPT3, LPT4, LPT5, LPT6, LPT7, LPT8, LPT9 };
        public readonly static string[] LPTDeviceNames = { LPT1, LPT2, LPT3, LPT4, LPT5, LPT6, LPT7, LPT8, LPT9 };
        public readonly static string[] SerialDeviceNames = { AUX, COM1, COM2, COM3, COM4, COM5, COM6, COM7, COM8, COM9 };
        public readonly static string[] COMDeviceNames = { COM1, COM2, COM3, COM4, COM5, COM6, COM7, COM8, COM9 };

        public readonly static string[] AllDosDeviceNames = { CLOCK, CON, PRN, AUX, NUL, COM1, COM2, COM3, LPT1, LPT2, LPT3 };
        public readonly static string[] ParallelDosDeviceNames = { PRN, LPT1, LPT2, LPT3 };
        public readonly static string[] LPTDosDeviceNames = { LPT1, LPT2, LPT3 };
        public readonly static string[] SerialDosDeviceNames = { AUX, COM1, COM2, COM3, COM4 };
        public readonly static string[] COMDosDeviceNames = { COM1, COM2, COM3, COM4 };
    }
}
