{ Copyright (c) 2007-2013 Jeroen Wiert Pluimers for BeSharp.net and better office benelux.
Full BSD License is available at http://besharp.codeplex.com/license and http://bo.codeplex.com/license }

unit LoggerUnit;

interface

uses
  IndexLoggerUnit,
  LoggerInterfaceUnit,
  System.SysUtils;

type
  TLogger = class abstract(TIndexLogger, ILogger)
  protected
    procedure Flush; virtual;
    procedure Log(); overload; virtual;
    procedure Log(const E: Exception); overload; virtual;
    procedure LogMulti(const Description: string; const Items: array of string); overload; virtual;
  end;

implementation

uses
  GlobalLoggerSettingsUnit,
  SafeFormatUnit;

procedure TLogger.Flush;
begin
  // allow descendants to override.
end;

procedure TLogger.Log;
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  Log('');
end;

procedure TLogger.Log(const E: Exception);
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  Log('Exception "%s", at %p: "%s"', [E.ClassName, ExceptAddr, E.Message]);
end;

procedure TLogger.LogMulti(const Description: string; const Items: array of string);
var
  Item: string;
  Index: Integer;
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  Index := 0;
  for Item in Items do
  begin
    Index := Index + 1;
    Log(SafeFormat('%s[%d]', [Description, Index]), Item);
  end;
end;

end.
