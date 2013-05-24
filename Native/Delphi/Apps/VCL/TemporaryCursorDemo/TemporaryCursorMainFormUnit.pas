unit TemporaryCursorMainFormUnit;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.StdCtrls;

type
  TTemporaryCursorMainForm = class(TForm)
    TemporaryCursorClassicButton: TButton;
    TemporaryCursorMementoButton: TButton;
    procedure TemporaryCursorClassicButtonClick(Sender: TObject);
    procedure TemporaryCursorMementoButtonClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  TemporaryCursorMainForm: TTemporaryCursorMainForm;

implementation

uses
  TemporaryCursorUnit, AnonymousMethodMementoUnit;

{$R *.dfm}

procedure TTemporaryCursorMainForm.TemporaryCursorClassicButtonClick(Sender: TObject);
var
  Button: TButton;
  SavedCursor: TCursor;
  SavedEnabled: Boolean;
begin
  Button := Sender as TButton;
  SavedEnabled := Button.Enabled;
  try
    Button.Enabled := False;
    SavedCursor := Screen.Cursor;
    try
      Screen.Cursor := crHourGlass;
      Sleep(3000);
    finally
      Screen.Cursor := SavedCursor;
    end;
  finally
    Button.Enabled := SavedEnabled;
  end;
end;

procedure TTemporaryCursorMainForm.TemporaryCursorMementoButtonClick(Sender: TObject);
var
  Button: TButton;
  SavedEnabled: Boolean;
begin
  TTemporaryCursor.SetTemporaryCursor();
  Button := Sender as TButton;
  SavedEnabled := Button.Enabled;
  TAnonymousMethodMemento.CreateMemento(procedure begin Button.Enabled := SavedEnabled; end);
  Button.Enabled := False;
  Sleep(3000); // sleep 3 seconds with the button disabled crHourGlass cursor
  // Delphi will automatically restore the cursor
end;

end.
