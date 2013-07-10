program Base64DUnitGUIProject;

uses
  Forms,
  TestFramework,
  GUITestRunner,
  Base64TestCaseUnit in '..\src\Base64TestCaseUnit.pas',
  Base64 in '..\src\Base64.pas',
  Base64BaseCalculatorUnit in '..\src\Base64BaseCalculatorUnit.pas',
  IdCoderMIME in 'C:\Program Files (x86)\Embarcadero\RAD Studio\9.0\source\Indy10\Protocols\IdCoderMIME.pas';

{$R *.res}

begin
  TGUITestRunner.runRegisteredTests;
end.
