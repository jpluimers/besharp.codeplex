using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RegExCRLF
{
    class Program
    {
        private static void RunRegEx(string CRLF)
        {
            Console.WriteLine("CRLF: '{0}'", CRLF);
            string MatchInput = Environment.NewLine;
            const string ReplaceInput = "Foo Bar";
            bool IsMatch;
            string Replacement;
            IsMatch = Regex.Match(MatchInput, CRLF).Success;
            Console.WriteLine("{0} for matching Environment.NewLine with {1}", IsMatch, CRLF);
            Replacement = Regex.Replace(ReplaceInput, " ", CRLF);
            Console.WriteLine("Replacing spaces in '{0}' with '{1}'", ReplaceInput, CRLF);
            Console.WriteLine("Results in '{0}'", Replacement);
        }

        static void Main(string[] args)
        {
            const string CStyleCRLF = "\r\n";
  
            try
            {
                Console.WriteLine("C#: CStyleCRLF expands to '{0}'", CStyleCRLF);
                RunRegEx(CStyleCRLF);
                StringBuilder CRLFBuilder = new StringBuilder();
                CRLFBuilder.Append(@"\");
                CRLFBuilder.Append("r");
                CRLFBuilder.Append(@"\");
                CRLFBuilder.Append("n");
                RunRegEx(CRLFBuilder.ToString());
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
CRLF: '
'
True for matching Environment.NewLine with

Replacing spaces in 'Foo Bar' with '
'
Results in 'Foo
Bar'
CRLF: '\r\n'
True for matching Environment.NewLine with \r\n
Replacing spaces in 'Foo Bar' with '\r\n'
Results in 'Foo\r\nBar'

*/