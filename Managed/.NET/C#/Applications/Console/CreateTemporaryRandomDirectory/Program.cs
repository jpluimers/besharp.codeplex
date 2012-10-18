using System;
using BeSharp.IO;

namespace CreateTemporaryRandomDirectory
{
    class Program
    {
        static void Main(string[] args)
        {
            string temporaryDirectory = DirectoryHelper.GetTemporaryDirectory();
            Console.WriteLine("Temporary directory {0}", temporaryDirectory);
        }
    }
}
