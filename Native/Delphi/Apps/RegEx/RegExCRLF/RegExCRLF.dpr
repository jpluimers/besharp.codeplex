program RegExCRLF;

{$APPTYPE CONSOLE}

{$R *.res}

uses
  System.SysUtils,
  System.RegularExpressions;

const
  CStyleCRLF = '\r\n';
  MatchInput = sLineBreak;
  ReplaceInput = 'Foo Bar';
var
  IsMatch: Boolean;
  Replacement: string;

begin
  try
    IsMatch := TRegEx.Match(MatchInput, CStyleCRLF).Success;
    Writeln(IsMatch, ' for matching sLineBreak with ', CStyleCRLF);
    Replacement := TRegEx.Replace(ReplaceInput, ' ', CStyleCRLF);
    Writeln('Replacing spaces in "', ReplaceInput, '" with "', CStyleCRLF,'"');
    Writeln('Results in "', Replacement,'"');
  except
    on E: Exception do
      Writeln(E.ClassName, ': ', E.Message);
  end;
end.

(*
Output:

TRUE for matching sLineBreak with \r\n
Replacing spaces in "Foo Bar" with "\r\n"
Results in "Foo\r\nBar"

*)
