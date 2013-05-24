{ Copyright (c) 2007-2013 Jeroen Wiert Pluimers for BeSharp.net and better office benelux.
Full BSD License is available at http://besharp.codeplex.com/license and http://bo.codeplex.com/license }

unit LeveledLoggerUnit;

interface

uses
  LoggerInterfaceUnit,
  LoggerUnit;

type
  TLeveledLogger = class(TLogger, ILeveledLogger)
  strict private
    FAll: ILeveledLogger;
    FDebug: ILeveledLogger;
    FError: ILeveledLogger;
    FFatal: ILeveledLogger;
    FInfo: ILeveledLogger;
    FTrace: ILeveledLogger;
    FVerbosityLevel: TVerbosityLevel;
    FVerbosityLevelString: string;
    FWarn: ILeveledLogger;
  strict protected
    function FormatLine(const Line: string): string; override;
  protected
    function GetAll: ILeveledLogger; virtual;
    function GetDebug: ILeveledLogger; virtual;
    function GetError: ILeveledLogger; virtual;
    function GetFatal: ILeveledLogger; virtual;
    function GetInfo: ILeveledLogger; virtual;
    function GetTrace: ILeveledLogger; virtual;
    function GetVerbosityLevel: TVerbosityLevel; virtual;
    function GetVerbosityLevelString: string; virtual;
    function GetWarn: ILeveledLogger; virtual;
    procedure SetVerbosityLevel(const Value: TVerbosityLevel); virtual;
  public
    constructor Create;
    class function CreateIfNeeded(var FLogger: ILeveledLogger): ILeveledLogger;
    property All: ILeveledLogger read GetAll;
    property Debug: ILeveledLogger read GetDebug;
    property Error: ILeveledLogger read GetError;
    property Fatal: ILeveledLogger read GetFatal;
    property Info: ILeveledLogger read GetInfo;
    property Trace: ILeveledLogger read GetTrace;
    property VerbosityLevel: TVerbosityLevel read GetVerbosityLevel write SetVerbosityLevel;
    property VerbosityLevelString: string read GetVerbosityLevelString;
    property Warn: ILeveledLogger read GetWarn;
  end;

implementation

uses
  System.SysUtils,
  ReportProxyInterfaceUnit,
  SafeFormatUnit,
  EnumerationTypeInformationUnit;

type
  TReportProxies = array of IReportProxy;

  // show difference between open arrays and array types: http://stackoverflow.com/questions/3780235/delphi-array-of-char-and-tchararray-incompatible-types
  TTeeLogger = class(TLeveledLogger)
  strict private
    FReportProxies: TReportProxies;
  strict protected
    procedure Report(const ReportProxy: IReportProxy; const Line: string); overload; virtual;
    property ReportProxies: TReportProxies read FReportProxies;
  protected
    function BuildReportProxies(const ReportProxyArray: array of IReportProxy): TReportProxies; virtual;
    procedure Report(const Line: string); overload; override;
    procedure SetEnabled(const Value: Boolean); override;
  public
    constructor Create(const ReportProxyArray: array of IReportProxy); overload;
    constructor Create(const ReportProxies: TReportProxies); overload;
  end;

  TIndirectToLeveledLogger = class(TTeeLogger)
  strict protected
    procedure Report(const ReportProxy: IReportProxy; const Line: string); overload; override;
  end;

procedure TIndirectToLeveledLogger.Report(const ReportProxy: IReportProxy; const Line: string);
var
  LeveledLogger: ILeveledLogger;
  FormattedLine: string;
begin
  if Supports(ReportProxy, ILeveledLogger, LeveledLogger) then
  begin
    if LeveledLogger.VerbosityLevel >= Self.VerbosityLevel then
    begin
      FormattedLine := FormatLine(Line);
      // we have to redo formatting here,
      // as we are sending it to the proxy,
      // which will do it's own formatting, not our formatting
      inherited Report(ReportProxy, FormattedLine);
    end;
  end;
end;

constructor TLeveledLogger.Create;
begin
  inherited Create();
  if Self is TIndirectToLeveledLogger then
    Exit;
  // there is no FOff, because that would be of no use
  FFatal := TIndirectToLeveledLogger.Create([Self]);
  FFatal.VerbosityLevel := vlFatal;
  FError := TIndirectToLeveledLogger.Create([Self]);
  FError.VerbosityLevel := vlError;
  FWarn := TIndirectToLeveledLogger.Create([Self]);
  FWarn.VerbosityLevel := vlWarn;
  FInfo := TIndirectToLeveledLogger.Create([Self]);
  FInfo.VerbosityLevel := vlInfo;
  FDebug := TIndirectToLeveledLogger.Create([Self]);
  FDebug.VerbosityLevel := vlDebug;
  FTrace := TIndirectToLeveledLogger.Create([Self]);
  FTrace.VerbosityLevel := vlTrace;
  FAll := TIndirectToLeveledLogger.Create([Self]);
  FAll.VerbosityLevel := vlAll;
  VerbosityLevel := vlWarn;
end;

class function TLeveledLogger.CreateIfNeeded(var FLogger: ILeveledLogger): ILeveledLogger;
begin
  if not Assigned(FLogger) then
    FLogger := TLeveledLogger.Create();
  Result := FLogger;
end;

function TLeveledLogger.FormatLine(const Line: string): string;
var
  Character: Char;
  CleanedLine: string;
  Index: Integer;
begin
  CleanedLine := Line;
  if VerbosityLevel < vlAll then
    for Index := 1 to Length(CleanedLine) do
    begin
      Character := Line[Index];
      if (Character < #32) or (Character > #127) then
        if (Character <> #10) and (Character <> #13) then
          CleanedLine[Index] := '.';
    end;
  Result := SafeFormat('%s: %s', [VerbosityLevelString, CleanedLine]);
  Result := inherited FormatLine(Result);
end;

function TLeveledLogger.GetAll: ILeveledLogger;
begin
  Result := FAll;
end;

function TLeveledLogger.GetDebug: ILeveledLogger;
begin
  Result := FDebug;
end;

function TLeveledLogger.GetError: ILeveledLogger;
begin
  Result := FError;
end;

function TLeveledLogger.GetFatal: ILeveledLogger;
begin
  Result := FFatal;
end;

function TLeveledLogger.GetInfo: ILeveledLogger;
begin
  Result := FInfo;
end;

function TLeveledLogger.GetTrace: ILeveledLogger;
begin
  Result := FTrace;
end;

function TLeveledLogger.GetVerbosityLevel: TVerbosityLevel;
begin
  Result := FVerbosityLevel;
end;

function TLeveledLogger.GetVerbosityLevelString: string;
begin
  Result := FVerbosityLevelString;
end;

function TLeveledLogger.GetWarn: ILeveledLogger;
begin
  Result := FWarn;
end;

procedure TLeveledLogger.SetVerbosityLevel(const Value: TVerbosityLevel);
begin
  FVerbosityLevel := Value;
  Enabled := Value <> vlOff;
  FVerbosityLevelString := EnumerationAsString(Ord(Value), 'vl', TypeInfo(TVerbosityLevel));
end;

end.
