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
                RunRegEx("\x0D\x0A");
                RunRegEx("\x000D\x000A");
                RunRegEx(@"\x000D\x000A");
                RunRegEx(@"\u000D\u000A");
                RunRegEx(@"\x0D\x0A");
                RunRegEx(@"\u0D\u0A");
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
CRLF: '
'
True for matching Environment.NewLine with

Replacing spaces in 'Foo Bar' with '
'
Results in 'Foo
Bar'
CRLF: '
'
True for matching Environment.NewLine with

Replacing spaces in 'Foo Bar' with '
'
Results in 'Foo
Bar'
CRLF: '\x000D\x000A'
False for matching Environment.NewLine with \x000D\x000A
Replacing spaces in 'Foo Bar' with '\x000D\x000A'
Results in 'Foo\x000D\x000ABar'
CRLF: '\u000D\u000A'
True for matching Environment.NewLine with \u000D\u000A
Replacing spaces in 'Foo Bar' with '\u000D\u000A'
Results in 'Foo\u000D\u000ABar'
CRLF: '\x0D\x0A'
True for matching Environment.NewLine with \x0D\x0A
Replacing spaces in 'Foo Bar' with '\x0D\x0A'
Results in 'Foo\x0D\x0ABar'
CRLF: '\u0D\u0A'
System.ArgumentException: parsing "\u0D\u0A" - Insufficient hexadecimal digits.

*/