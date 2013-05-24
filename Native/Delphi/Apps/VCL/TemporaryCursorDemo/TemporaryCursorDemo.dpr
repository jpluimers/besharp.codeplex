program TemporaryCursorDemo;

uses
  Vcl.Forms,
  TemporaryCursorMainFormUnit in 'TemporaryCursorMainFormUnit.pas' {TemporaryCursorMainForm},
  TemporaryCursorUnit in '..\..\..\Library\VCL\TemporaryCursorUnit.pas',
  AnonymousMethodMementoUnit in '..\..\..\Library\RTL\AnonymousMethodMementoUnit.pas',
  MementoUnit in '..\..\..\Library\RTL\MementoUnit.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.MainFormOnTaskbar := True;
  Application.CreateForm(TTemporaryCursorMainForm, TemporaryCursorMainForm);
  Application.Run;
end.
