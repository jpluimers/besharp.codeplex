using System;
using System.IO;
using System.Text;

namespace LineCount
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (string arg in args)
            {
                try
                {
                    string fileName = arg;
                    int bufferSize = BeSharp.Numerics.UnitPrefixes.MiB;
                    Int64 lineCount = 0;
                    // simpler, but does not have a buffersize:
                    //using (StreamReader reader = File.OpenText(arg))
                    using (Stream stream = File.OpenRead(fileName))
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8, true, bufferSize))
                        {
                            while (null != reader.ReadLine())
                            {
                                lineCount++;
                            }
                        }
                    }
                    Console.WriteLine("{0} lines in '{1}'", lineCount, fileName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
