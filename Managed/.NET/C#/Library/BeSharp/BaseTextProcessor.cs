using System;
using System.IO;

namespace BeSharp
{
    public class BaseTextProcessor
    {

        public static Stream GetInputStream(string inputFileName)
        {
            Stream input;
            if (string.IsNullOrWhiteSpace(inputFileName))
                input = Console.OpenStandardInput();
            else
                input = new FileStream(inputFileName, FileMode.Open, FileAccess.Read);
            return input;
        }

        public static Stream GetOutputStream(string outputFileName)
        {
            return GetOutputStream1(outputFileName, FileMode.Create);
            //return GetOutputStream1(outputFileName, FileMode.OpenOrCreate); // not so good for output
        }

        private static Stream GetOutputStream1(string outputFileName, FileMode fileMode)
        {
            Stream output;
            if (string.IsNullOrWhiteSpace(outputFileName))
                output = Console.OpenStandardOutput();
            else
                output = new FileStream(outputFileName, fileMode, FileAccess.Write);
            return output;
        }

    }
}