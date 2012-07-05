using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace CharIsInSetConsoleApplication
{
    class Program
    {
        /*
         * call with arguments like this:
         *     ftp://besharp.com;C:\Windows\system32\cmd.exe 
         */
        static void Main(string[] args)
        {
            List<char> separatorsUsed = getSeparatorsUsed(args);

            Console.WriteLine("Separators used in args:");
            // regular foreach is more readable, but you can use LINQ here too: 
            // http://stackoverflow.com/questions/823532/apply-function-to-all-elements-of-collection-through-linq/823563#823563
            separatorsUsed.ForEach(item => 
                Console.WriteLine(item)
            );
            Console.Write("Press <Enter>");
            Console.ReadLine();
        }

        private static List<char> getSeparatorsUsed(string[] args)
        {
            char[] pathSeparators = { Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar, Path.PathSeparator };
            List<char> separatorsUsed = new List<char>();
            foreach (string arg in args)
            {
                foreach (char item in arg)
                {
                    switch (item)
                    {
                        case '/':
                        case '\\':
                        case ';':
                            addSeparator(separatorsUsed, item);
                            break;
                    }

                    //switch (item)
                    //{
                    //    case Path.AltDirectorySeparatorChar: // Error: A constant value is expected
                    //    case Path.DirectorySeparatorChar:
                    //    case Path.PathSeparator:
                    //        addSeparator(separatorsUsed, item);
                    //        break;
                    //}

                    if ((item == Path.AltDirectorySeparatorChar) || (item == Path.DirectorySeparatorChar) || (item == Path.PathSeparator))
                        addSeparator(separatorsUsed, item);

                    // LINQ: http://stackoverflow.com/questions/1818611/how-to-check-if-a-char-in-a-char-array/1818635#1818635
                    if (pathSeparators.Contains(item))
                        addSeparator(separatorsUsed, item);
                }
            }
            return separatorsUsed;
        }

        private static void addSeparator(List<Char> separatorsUsed, char item)
        {
            if (!separatorsUsed.Contains(item))
                separatorsUsed.Add(item);
        }
    }
}
