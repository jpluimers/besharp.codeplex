{ Copyright (c) 2007-2013 Jeroen Wiert Pluimers for BeSharp.net and better office benelux.
Full BSD License is available at http://besharp.codeplex.com/license and http://bo.codeplex.com/license }

unit EnabledLoggerUnit;

interface

uses
  LoggerInterfaceUnit,
  ReportProxyUnit,
  ReportPayloadInterfaceUnit;

type
  ///	<summary>
  ///	  Implements <see cref="LoggerInterfaceUnit|IEnabledLogger" />.
  ///	</summary>
  TEnabledLogger = class(TReportProxy, IEnabledLogger)
  strict private
    FEnabled: Boolean;
  strict protected
    procedure Initialize; override;
  protected
    function GetEnabled: Boolean; virtual;
    procedure Log(const Line: string); overload; virtual;
    procedure Log(const Payload: IReportPayload); overload; virtual;
    procedure Report(const Payload: IReportPayload); overload; override;
    procedure Report(const Line: string); overload; virtual;
    procedure SetEnabled(const Value: Boolean); virtual;
    property Enabled: Boolean read GetEnabled write SetEnabled;
  end;

implementation

uses
  StringReportPayloadUnit,
  System.SysUtils,
  GlobalLoggerSettingsUnit;

function TEnabledLogger.GetEnabled: Boolean;
begin
  Result := FEnabled;
end;

procedure TEnabledLogger.Initialize;
begin
  Enabled := True;
end;

procedure TEnabledLogger.Log(const Line: string);
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  Report(Line);
end;

procedure TEnabledLogger.Log(const Payload: IReportPayload);
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  Report(Payload);
end;

procedure TEnabledLogger.Report(const Payload: IReportPayload);
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  if Enabled then
    inherited Report(Payload);
end;

procedure TEnabledLogger.Report(const Line: string);
var
  Payload: IReportPayload;
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  Payload := TStringReportPayload.Create(0, Line, Now);
  Report(Payload);
end;

procedure TEnabledLogger.SetEnabled(const Value: Boolean);
begin
  FEnabled := Value;
end;

end.
