using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeSharp
{
    public static class StringExtensions
    {
        public static bool IsAllDigits(this string value)
        {
            foreach (char item in value)
            {
                if (!Char.IsDigit(item))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Repeats a string count times
        /// Thanks DrJokepu http://stackoverflow.com/users/8954/drjokepu
        /// http://stackoverflow.com/questions/532892/can-i-multiply-a-string-in-c
        /// </summary>
        /// <param name="source">string to repeat</param>
        /// <param name="count">number of times to repeat source</param>
        /// <returns></returns>
        public static string Repeat(this string source, int count)
        {
            StringBuilder stringBuilder = new StringBuilder(count * source.Length);
            for (int occurrence = 0; occurrence < count; occurrence++)
            {
                stringBuilder.Append(source);
            }

            return stringBuilder.ToString();
        }

        // http://extensionmethod.net/Details.aspx?ID=513
        // probably can be faster (see http://www.codeproject.com/Articles/10890/Fastest-C-Case-Insenstive-String-Replace) but this suffices for now
        public static string Replace(this string originalString, string oldValue, string newValue, StringComparison comparisonType)
        {
            string result = originalString;
            int startIndex = 0;

            while (true)
            {
                startIndex = result.IndexOf(oldValue, startIndex, comparisonType);
                if (startIndex < 0)
                    break;

                result = string.Concat(result.Substring(0, startIndex), newValue, result.Substring(startIndex + oldValue.Length));

                startIndex += newValue.Length;
            }

            return result;
        }

        /// <summary>
        /// Thanks Jeremy Ruten http://stackoverflow.com/users/813/jeremy-ruten
        /// http://stackoverflow.com/a/223854/29290
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsHex(this string value)
        {
            foreach (char current_char in value)
            {
                bool is_hex_char = (current_char >= '0' && current_char <= '9') ||
                                   (current_char >= 'a' && current_char <= 'f') ||
                                   (current_char >= 'A' && current_char <= 'F');

                if (!is_hex_char)
                {
                    return false;
                }
            }

            return true;
        }

        public static string ReverseString(this string value)
        {
            char[] array = value.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }

#if NET10_35
        // not needed in .NET 4 and up
        public static bool IsNullOrWhiteSpace(this string value)
        {
            if (null != value)
            {
                foreach (Char character in value)
                {
                    if (!Char.IsWhiteSpace(character))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
#endif

        // note that With needs a couple of overloads to match the System.String.Format overloads

        public static string With(this string format, object arg0)
        {
            return string.Format(format, arg0);
        }

        public static string With(this string format, object arg0, object arg1)
        {
            return string.Format(format, arg0, arg1);
        }

        public static string Format(this string format, IFormatProvider provider, params object[] args)
        {
            return string.Format(provider, format, args);
        }

        public static string With(this string format, object arg0, object arg1, object arg2)
        {
            return string.Format(format, arg0, arg1, arg2);
        }

        public static string With(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
    }
}
