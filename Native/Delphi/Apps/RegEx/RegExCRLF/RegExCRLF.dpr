program RegExCRLF;

{$APPTYPE CONSOLE}

{$R *.res}

uses
  System.SysUtils,
  System.RegularExpressions;

procedure RunRegEx(const CRLF: string);
const
  MatchInput = sLineBreak;
  ReplaceInput = 'Foo Bar';
var
  IsMatch: Boolean;
  Replacement: string;
begin
  Writeln('CRLF: "', CRLF, '"');
  IsMatch := TRegEx.Match(MatchInput, CRLF).Success;
  Writeln(IsMatch, ' for matching sLineBreak with ', CRLF);
  Replacement := TRegEx.Replace(ReplaceInput, ' ', CRLF);
  Writeln('Replacing spaces in "', ReplaceInput, '" with "', CRLF, '"');
  Writeln('Results in "', Replacement, '"');
end;

const
  CStyleCRLF = '\r\n';
var
  CStyleCRLFBuilder: TStringBuilder;

begin
  try
    Writeln('Delphi: CStyleCRLF expands to "', CStyleCRLF, '"');
    RunRegEx(CStyleCRLF);
    CStyleCRLFBuilder := TStringBuilder.Create();
    try
      CStyleCRLFBuilder.Append('\');
      CStyleCRLFBuilder.Append('r');
      CStyleCRLFBuilder.Append('\');
      CStyleCRLFBuilder.Append('n');
      RunRegEx(CStyleCRLFBuilder.ToString());
    finally
      CStyleCRLFBuilder.Free;
    end;
    RunRegEx(#13#10);
    RunRegEx(^M^J);
    RunRegEx('\x0D\x0A');
    RunRegEx('\x000D\x000A');
    RunRegEx('\u0D\u0A');
    RunRegEx('\u000D\u000A');
  except
    on E: Exception do
      Writeln(E.ClassName, ': ', E.Message);
  end;
end.

(*
Output:

Delphi: CStyleCRLF expands to "\r\n"
CRLF: "\r\n"
TRUE for matching sLineBreak with \r\n
Replacing spaces in "Foo Bar" with "\r\n"
Results in "Foo\r\nBar"
CRLF: "\r\n"
TRUE for matching sLineBreak with \r\n
Replacing spaces in "Foo Bar" with "\r\n"
Results in "Foo\r\nBar"
CRLF: "
"
TRUE for matching sLineBreak with

Replacing spaces in "Foo Bar" with "
"
Results in "Foo
Bar"
CRLF: "
"
TRUE for matching sLineBreak with

Replacing spaces in "Foo Bar" with "
"
Results in "Foo
Bar"
CRLF: "\x0D\x0A"
TRUE for matching sLineBreak with \x0D\x0A
Replacing spaces in "Foo Bar" with "\x0D\x0A"
Results in "Foo\x0D\x0ABar"
CRLF: "\x000D\x000A"
FALSE for matching sLineBreak with \x000D\x000A
Replacing spaces in "Foo Bar" with "\x000D\x000A"
Results in "Foo\x000D\x000ABar"
CRLF: "\u0D\u0A"
ERegularExpressionError: Error in regular expression at offset 1: PCRE does not
support \L, \l, \N, \U, or \u

*)
