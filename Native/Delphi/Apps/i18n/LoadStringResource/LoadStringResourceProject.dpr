program LoadStringResourceProject;

uses
  Vcl.Forms,
  LoadStringResourceMainFormUnit in 'LoadStringResourceMainFormUnit.pas' {LoadStringResourceMainForm},
  StringResourcesUnit in '..\..\..\Library\RTL\WIN\i18n\StringResourcesUnit.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.MainFormOnTaskbar := True;
  Application.CreateForm(TLoadStringResourceMainForm, LoadStringResourceMainForm);
  Application.Run;
end.
