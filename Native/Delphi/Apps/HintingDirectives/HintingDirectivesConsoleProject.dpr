program HintingDirectivesConsoleProject;

{$APPTYPE CONSOLE}

{$R *.res}

uses
  System.SysUtils,
  HintingDirectivesUnit in 'HintingDirectivesUnit.pas';

begin
  try
    { TODO -oUser -cConsole Main : Insert code here }
  except
    on E: Exception do
      Writeln(E.ClassName, ': ', E.Message);
  end;
end.
