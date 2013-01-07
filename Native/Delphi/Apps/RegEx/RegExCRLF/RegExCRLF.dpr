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

*)
