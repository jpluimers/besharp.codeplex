using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeSharp.Win32
{

    /// <summary>
    /// Adapted from 
    /// http://stackoverflow.com/questions/5570368/c-sharp-yes-no-value-of-system/5570582#5570582
    /// http://stackoverflow.com/questions/5570368/c-sharp-yes-no-value-of-system/5577735#5577735
    /// http://dotnet.mvps.org/dotnet/faqs/?id=systemstrings
    /// http://stackoverflow.com/questions/5632920/how-to-generically-format-a-boolean-to-a-yes-no-string/11229372#11229372
    /// http://blogs.msdn.com/b/jonathanswift/archive/2006/10/03/dynamically-calling-an-unmanaged-dll-from-.net-_2800_c_23002900_.aspx
    /// </summary>
    public static class BooleanExtensions
    {
        public static string ToLocalizedYesNoString(this bool value)
        {
            string result = value ? User32DllDialogButtonCaptions.Yes : User32DllDialogButtonCaptions.No;
            return result;
        }
    }
}
