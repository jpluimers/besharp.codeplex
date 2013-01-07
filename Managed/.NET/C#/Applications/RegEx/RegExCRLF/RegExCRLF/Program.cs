using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RegExCRLF
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CStyleCRLF = "\r\n";
            string MatchInput = Environment.NewLine;
            const string ReplaceInput = "Foo Bar";
            bool IsMatch;
            string Replacement;

            try
            {
                Console.WriteLine("C#: CStyleCRLF expands to '{0}'", CStyleCRLF);
                IsMatch = Regex.Match(MatchInput, CStyleCRLF).Success;
                Console.WriteLine("{0} for matching Environment.NewLine with {1}", IsMatch, CStyleCRLF);
                Replacement = Regex.Replace(ReplaceInput, " ", CStyleCRLF);
                Console.WriteLine("Replacing spaces in '{0}' with '{1}'", ReplaceInput, CStyleCRLF);
                Console.WriteLine("Results in '{0}'", Replacement);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
/*
Output:

C#: CStyleCRLF expands to '
'
True for matching Environment.NewLine with

Replacing spaces in 'Foo Bar' with '
'
Results in 'Foo
Bar' */