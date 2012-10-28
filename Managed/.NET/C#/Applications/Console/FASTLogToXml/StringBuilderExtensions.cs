using System;
using System.Text;
using System.Runtime;
using System.Security;

namespace FASTLogToXml
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendFormatLine(this StringBuilder self, string format, params object[] args)
        {
            return self.AppendFormat(format, args).AppendLine();
        }

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static StringBuilder AppendFormatLine(this StringBuilder self, string format, object arg0)
        {
            return self.AppendFormat(format, arg0).AppendLine();
        }

        [SecuritySafeCritical]
        public static StringBuilder AppendFormatLine(this StringBuilder self, IFormatProvider provider, string format, params object[] args)
        {
            return self.AppendFormat(provider, format, args).AppendLine();
        }

        public static StringBuilder AppendFormatLine(this StringBuilder self, string format, object arg0, object arg1)
        {
            return self.AppendFormat(format, arg0, arg1).AppendLine();
        }

        public static StringBuilder AppendFormatLine(this StringBuilder self, string format, object arg0, object arg1, object arg2)
        {
            return self.AppendFormat(format, arg0, arg1, arg2).AppendLine();
        }
    }
}
