program Base64DUnitGUIProject;

uses
  Forms,
  TestFramework,
  GUITestRunner,
  Base64TestCaseUnit in '..\src\Base64TestCaseUnit.pas',
  Base64 in '..\src\Base64.pas',
  Base64BaseCalculatorUnit in '..\src\Base64BaseCalculatorUnit.pas';

{$R *.res}

begin
  TGUITestRunner.runRegisteredTests;
end.
