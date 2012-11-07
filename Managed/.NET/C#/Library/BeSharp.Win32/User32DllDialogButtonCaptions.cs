using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace BeSharp.Win32
{
    public class User32DllDialogButtonCaptions
    {
        internal struct captionStringId
        {
            internal const uint OK = 800;
            internal const uint Cancel = 801;
            internal const uint Abort = 802;
            internal const uint Retry = 803;
            internal const uint Ignore = 804;
            internal const uint Yes = 805;
            internal const uint No = 806;
            internal const uint Close = 807;
            internal const uint Help = 808;
            internal const uint Repeat = 809;
            internal const uint Continue = 810;
        }

        static User32DllDialogButtonCaptions()
        {
            OK = loadString(captionStringId.OK, "OK");
            Cancel = loadString(captionStringId.Cancel, "Cancel");
            Abort = loadString(captionStringId.Abort, "Abort");
            Retry = loadString(captionStringId.Retry, "Retry");
            Ignore = loadString(captionStringId.Ignore, "Ignore");
            Yes = loadString(captionStringId.Yes, "Yes");
            No = loadString(captionStringId.No, "No");
            Close = loadString(captionStringId.Close, "Close");
            Help = loadString(captionStringId.Help, "Help");
            Repeat = loadString(captionStringId.Repeat, "Repeat");
            Continue = loadString(captionStringId.Continue, "Continue");
        }

        private static string loadString(uint id, string defaultValue)
        {
            string result = User32Dll.LoadString(User32Dll.user32_dll, id, defaultValue);
            result = result.Replace("&", string.Empty);
            return result;
        }

        public static string OK { get; private set; }
        public static string Cancel { get; private set; }
        public static string Abort { get; private set; }
        public static string Retry { get; private set; }
        public static string Ignore { get; private set; }
        public static string Yes { get; private set; }
        public static string No { get; private set; }
        public static string Close { get; private set; }
        public static string Help { get; private set; }
        public static string Repeat { get; private set; }
        public static string Continue { get; private set; }
    }
}
