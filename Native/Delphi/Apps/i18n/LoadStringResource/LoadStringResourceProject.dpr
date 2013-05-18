program LoadStringResourceProject;

uses
  Vcl.Forms,
  LoadStringResourceMainFormUnit in 'LoadStringResourceMainFormUnit.pas' {LoadStringResourceMainForm},
  BeSharp.i18n.StringResourcesUnit in '..\..\..\..\BeSharp.i18n\BeSharp.i18n.StringResourcesUnit.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.MainFormOnTaskbar := True;
  Application.CreateForm(TLoadStringResourceMainForm, LoadStringResourceMainForm);
  Application.Run;
end.
