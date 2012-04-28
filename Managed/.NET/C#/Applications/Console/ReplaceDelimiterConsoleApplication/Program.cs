using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ReplaceDelimiterConsoleApplication
{
    class Program
    {
        static void Logic(string sourcedelimiter, string targetdelimiter, string sourcefile, string targetfile)
        {
            int codePage = 1252;
            Encoding encoding = Encoding.GetEncoding(codePage);
            string[] sourceLines = File.ReadAllLines(sourcefile, encoding);
            List<string> targetLines = new List<string>();
            foreach (string sourceLine in sourceLines)
            {
                string targetLine = sourceLine.Replace(sourcedelimiter, targetdelimiter);
                targetLines.Add(targetLine);
            }
            File.WriteAllLines(targetfile, targetLines, encoding);
        }

        static void Main(string[] args)
        {
            if (args.Length < 4)
            {
                Console.WriteLine("Use 4 arguments: sourcedelimiter targetdelimiter sourcefile targetfile");
                Console.WriteLine("this will replace all occurances of sourcedelimiter in sourcefile with targetdelimiter in targetfile");
            }
            else
            {
                Logic(args[0], args[1], args[2], args[3]);
            }
        }
    }
}
