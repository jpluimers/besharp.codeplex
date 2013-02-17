using System;

namespace AssemblyHelperConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string nameValues = AssemblyHelperNameValues.NameValues;
            if (!string.IsNullOrWhiteSpace(nameValues))
                Console.WriteLine(nameValues);
        }
    }
}
