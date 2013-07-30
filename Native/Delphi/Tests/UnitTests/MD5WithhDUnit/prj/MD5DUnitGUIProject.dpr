program MD5DUnitGUIProject;

uses
  Forms,
  TestFramework,
  GUITestRunner,
  MD5TestCaseUnit in '..\src\MD5TestCaseUnit.pas',
  MD5 in '..\src\MD5.pas',
  MD5BaseCalculatorUnit in '..\src\MD5BaseCalculatorUnit.pas',
  MessageDigest_5 in '..\..\..\..\Library\RTL\MessageDigest_5.pas';

{$R *.res}

begin
  TGUITestRunner.runRegisteredTests;
end.
