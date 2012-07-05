using System;

namespace CharIntCompatibilityCSharp
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("asterisk");
            char asterisk = '*';
            //writeLineSbyte(asterisk); // no implicit conversion
            //writeLineByte(asterisk); // no implicit conversion
            //writeLineShort(asterisk); // no implicit conversion
            writeLineChar(asterisk);
            writeLineUshort(asterisk); // implicit conversion
            writeLineInt(asterisk);
            writeLineUint(asterisk);
            writeLineLong(asterisk);
            writeLineUlong(asterisk);
            writeLineFloat(asterisk);
            writeLineDouble(asterisk);
            writeLineDecimal(asterisk);
            Console.WriteLine("space");
            byte space = 32;
            writeLineSbyte(space);
            writeLineByte(space);
            writeLineShort(space);
            //writeLineChar(space); // no implicit conversion
            writeLineUshort(space);
            writeLineInt(space);
            writeLineUint(space);
            writeLineLong(space);
            writeLineUlong(space);
            writeLineFloat(space);
            writeLineDouble(space);
            writeLineDecimal(space);
            Console.Write("Press <Enter>");
            Console.ReadLine();
        }

        private static void writeLineByte(byte character)
        {
            Console.WriteLine((int)character);
        }

        private static void writeLineChar(char character)
        {
            Console.WriteLine(character);
        }

        private static void writeLineDecimal(decimal character)
        {
            Console.WriteLine(character);
        }

        private static void writeLineDouble(double character)
        {
            Console.WriteLine(character);
        }

        private static void writeLineFloat(float character)
        {
            Console.WriteLine(character);
        }

        private static void writeLineInt(int character)
        {
            Console.WriteLine(character);
        }

        private static void writeLineLong(long character)
        {
            Console.WriteLine(character);
        }

        private static void writeLineSbyte(byte character)
        {
            Console.WriteLine((int)character);
        }

        private static void writeLineShort(short character)
        {
            Console.WriteLine((int)character);
        }

        private static void writeLineUint(uint character)
        {
            Console.WriteLine(character);
        }

        private static void writeLineUlong(ulong character)
        {
            Console.WriteLine(character);
        }

        private static void writeLineUshort(ushort character)
        {
            Console.WriteLine((int)character);
        }
    }
}