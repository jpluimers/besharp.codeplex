using System;
using BeSharp;

namespace StringToAsciiConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string unicode = "áìôüç";
            string ascii = unicode.ToAscii();
            Console.WriteLine("Unicode\t{0}", unicode);
            Console.WriteLine("ASCII\t{0}", ascii);
        }
    }
}
