using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeSharp
{
    public class Platform
    {
        public static readonly bool x64;
        public static readonly bool x86;

        static Platform()
        {
            x86 = (IntPtr.Size == 4);
            x86 = (IntPtr.Size == 8);
        }
    }
}
