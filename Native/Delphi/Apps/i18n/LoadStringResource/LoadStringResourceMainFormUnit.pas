unit LoadStringResourceMainFormUnit;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.StdCtrls, Vcl.ExtCtrls;

type
  TLoadStringResourceMainForm = class(TForm)
    Panel1: TPanel;
    LogMemo: TMemo;
    DemoButton: TButton;
    procedure DemoButtonClick(Sender: TObject);
  strict protected
    procedure Log(const Line: string); overload; virtual;
    procedure Log(Strings: TStringBuilder; i: Word; NewFolder: string); overload; virtual;
  end;

var
  LoadStringResourceMainForm: TLoadStringResourceMainForm;

implementation

uses
  StringResourcesUnit;

{$R *.dfm}

procedure TLoadStringResourceMainForm.DemoButtonClick(Sender: TObject);
const
  ShellNewFolderId = 30396; // 61966; // 4096; //
var
  Shell32DllHandle: HMODULE;
  FindResult: string;
  LoadResult: string;
  ResourceId: Word;
  Strings: TStringBuilder;
begin
  Shell32DllHandle := LoadLibraryW('shell32.dll');

  // UINT new_folder_id= FindResourceStringId(shell_handle, L"New Folder", 0x409); // look for US English "New Folder" resource id.

  FindResult := TStringResources.FindStringResourceEx(Shell32DllHandle, ShellNewFolderId, TStringResources.LCID_0409_English);
  Log(FindResult);

  Strings := TStringBuilder.Create();
  try
    for ResourceId := Low(ResourceId) to High(ResourceId) do
    begin
      FindResult := TStringResources.FindStringResourceEx(Shell32DllHandle, ResourceId, TStringResources.LCID_0409_English);
      if Trim(FindResult) <> '' then
      begin
        Log(Strings, ResourceId, FindResult);
        LoadResult := TStringResources.LoadString(Shell32DllHandle, ResourceId);

        {$ifdef sanity_check_IDs}
        if not SameStr(LoadResult, FindResult) then
        begin
          Strings.AppendLine('----- not equal -----');
          Log(Strings, i, FindResult);
        end;
        {$endif sanity_check_IDs}

        LoadResult := TStringResources.FindStringResourceEx(Shell32DllHandle, ResourceId, TStringResources.LCID_0413_Dutch);
        Log(Strings, ResourceId, LoadResult);
      end;
    end;
    LogMemo.Lines.Add(Strings.ToString());
  finally
    Strings.Free;
  end;

  LogMemo.SelectAll();
end;

procedure TLoadStringResourceMainForm.Log(const Line: string);
begin
  OutputDebugString(PChar(Line));
  LogMemo.Lines.Add(Line);
end;

procedure TLoadStringResourceMainForm.Log(Strings: TStringBuilder; i: Word; NewFolder: string);
var
  Tabbed: string;
begin
  Tabbed := IntToStr(i) + ''#9'' + NewFolder;
  OutputDebugString(PChar(Tabbed));
  Strings.AppendLine(Tabbed);
end;

end.
