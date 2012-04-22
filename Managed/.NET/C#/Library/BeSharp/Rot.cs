using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeSharp
{
    public class Rot
    {
        protected static string rot(string value, Func<char, char> rotFunc)
        {
            StringBuilder result = new StringBuilder(value.Length);
            foreach (char item in value)
            {
                char rot = rotFunc(item);
                result.Append(rot);
            }
            return result.ToString();
        }

        public static char Rot5(char value)
        {
            switch (value)
            {
                case '0':
                    return '5';
                case '1':
                    return '6';
                case '2':
                    return '7';
                case '3':
                    return '8';
                case '4':
                    return '9';
                case '5':
                    return '0';
                case '6':
                    return '1';
                case '7':
                    return '2';
                case '8':
                    return '3';
                case '9':
                    return '4';
                default:
                    return value;
            }
        }

        public static string Rot5(string value)
        {
            string result = rot(value, Rot5);
            return result;
        }

        public const int Rot13Displacement = 13;
        /// <summary>
        /// http://rosettacode.org/wiki/Rot-13#C.23
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static char Rot13(char value)
        {
            if (
                ((value >= 'A') && (value <= 'M'))
                ||
                ((value >= 'a') && (value <= 'M'))
                )
            {
                char result = (char)(value + Rot13Displacement);
                return result;
            }

            if (
                ((value >= 'N') && (value <= 'Z'))
                ||
                ((value >= 'n') && (value <= 'z'))
                )
            {
                char result = (char)(value - Rot13Displacement);
                return result;
            }

            return value;
        }

        public static string Rot13(string value)
        {
            string result = rot(value, Rot13);
            return result;
        }

    }
}
