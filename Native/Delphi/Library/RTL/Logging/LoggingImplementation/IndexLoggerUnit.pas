unit IndexLoggerUnit;

interface

uses
  DescriptionLoggerUnit,
  LoggerInterfaceUnit;

type
  TIndexLogger = class(TDescriptionLogger, IIndexLogger)
  protected
    procedure Log(const Description: string; const Index: Integer; const Item: Boolean); overload; virtual;
    procedure Log(const Description: string; const Index: Integer; const Item: Integer); overload; virtual;
    procedure Log(const Description: string; const Index: Integer; const Item: Pointer); overload; virtual;
    procedure Log(const Description: string; const Index: Integer; const Item: string); overload; virtual;
    procedure Log(const Description: string; const Index: Integer; const Item: ShortStringBase); overload;
    procedure Log(const Description: string; const Index: Integer; const DescriptionSuffix: string; const Item: Boolean); overload; virtual;
    procedure Log(const Description: string; const Index: Integer; const DescriptionSuffix: string; const Item: Integer); overload; virtual;
    procedure Log(const Description: string; const Index: Integer; const DescriptionSuffix: string; const Item: Pointer); overload; virtual;
    procedure Log(const Description: string; const Index: Integer; const DescriptionSuffix, Item: string); overload; virtual;
    procedure Log(const Description: string; const Index: Integer; const DescriptionSuffix: string; const Item: ShortStringBase); overload;
  end;

implementation

uses
  System.SysUtils,
  GlobalLoggerSettingsUnit;

procedure TIndexLogger.Log(const Description: string; const Index: Integer; const DescriptionSuffix: string; const Item: ShortStringBase);
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  Log(Description, Index, DescriptionSuffix, string(Item));
end;

procedure TIndexLogger.Log(const Description: string; const Index: Integer; const Item: ShortStringBase);
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  Log(Description, Index, string(Item));
end;

procedure TIndexLogger.Log(const Description: string; const Index: Integer; const Item: Boolean);
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  Log(Description, Index, BoolToStr(Item));
end;

procedure TIndexLogger.Log(const Description: string; const Index: Integer; const Item: Integer);
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  Log(Description, Index, IntToStr(Item));
end;

procedure TIndexLogger.Log(const Description: string; const Index: Integer; const Item: Pointer);
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  Log(Description, Index, PointerToString(Item));
end;

procedure TIndexLogger.Log(const Description: string; const Index: Integer; const Item: string);
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  Log('%s[%d]:%s', [Description, Index, Item]);
end;

procedure TIndexLogger.Log(const Description: string; const Index: Integer; const DescriptionSuffix: string; const Item: Boolean);
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  Log(Description, Index, DescriptionSuffix, BoolToStr(Item));
end;

procedure TIndexLogger.Log(const Description: string; const Index: Integer; const DescriptionSuffix: string; const Item: Integer);
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  Log(Description, Index, DescriptionSuffix, IntToStr(Item));
end;

procedure TIndexLogger.Log(const Description: string; const Index: Integer; const DescriptionSuffix: string; const Item: Pointer);
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  Log(Description, Index, DescriptionSuffix, PointerToString(Item));
end;

procedure TIndexLogger.Log(const Description: string; const Index: Integer; const DescriptionSuffix, Item: string);
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  Log('%s[%d].%s:%s', [Description, Index, DescriptionSuffix, Item]);
end;

end.
