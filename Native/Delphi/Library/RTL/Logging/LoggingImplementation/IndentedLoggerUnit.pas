{ Copyright (c) 2007-2013 Jeroen Wiert Pluimers for BeSharp.net and better office benelux.
Full BSD License is available at http://besharp.codeplex.com/license and http://bo.codeplex.com/license }

unit IndentedLoggerUnit;

interface

uses
  System.SysUtils,
  LoggerInterfaceUnit,
  EnabledLoggerUnit,
  StringListWrapperInterfaceUnit,
  ReportPayloadInterfaceUnit;

type
  ///	<summary>
  ///	  Current indentation both shown the indentation as a number and as a
  ///	  number of spaces before each line logged.
  ///	</summary>
  TIndentedLogger = class(TEnabledLogger, IIndentedLogger)
  strict private
    FTraceLevel: Integer;
    //TODO ##jpl: make these properties
    DoTraceOnlyEnabled: Boolean; { trace all except disabled }
    TraceLevels: IStringListWrapper; { list of String='-1/0' } //TODO ##jpl: replace with TQueue<Boolean>
    DoTrace: IStringListWrapper; { sorted list of String=ClassName/Object=TClass }
    DoNotTrace: IStringListWrapper; { sorted list of String=ClassName/Object=TClass }
  private
    function ClassMethod(const Instance: TObject; const MethodName: string): string;
    function FindInstanceInClassList(const Instance: TObject; const ClassList: IStringListWrapper): Boolean;
    function SafeClassName(const Instance: TObject): string;
  strict protected
    procedure EnterTraceLevel; virtual;
    procedure Initialize; override;
    function IsADoNotTraceInstance(Instance: TObject): Boolean; virtual;
    function IsADoTraceInstance(Instance: TObject): Boolean; virtual;
    procedure LeaveTraceLevel; virtual;
    procedure Report(const FormatMask: string; const Arguments: array of const); overload; virtual;
    procedure Report(const FormatMask: string; const Arguments: array of const; const FormatSettings: TFormatSettings); overload; virtual;
    procedure SetTraceLevel(const Value: Integer); virtual;
    property TraceLevel: Integer read FTraceLevel write SetTraceLevel;
  protected
    function CanTraceInstance(Instance: TObject): Boolean;
    procedure Log(const FormatMask: string; const Arguments: array of const); overload; virtual;
    procedure Log(const FormatMask: string; const Arguments: array of const; const FormatSettings: TFormatSettings); overload; virtual;
    procedure Enter(const MethodName: string); overload; virtual;
    procedure Enter(const Instance: TObject; const MethodName: string); overload; virtual;
    procedure Enter(const Instance: TObject; const Mask: string; const Args: array of const); overload; virtual;
    procedure Leave(const MethodName: string); overload; virtual;
    procedure Leave(const Instance: TObject; const MethodName: string); overload; virtual;
    procedure Leave(const Instance: TObject; const Mask: string; const Args: array of const); overload; virtual;
    procedure Report(const Payload: IReportPayload); overload; override;
    procedure Report(const Line: string); overload; override;
  end;

implementation

uses
  StringListWrapperUnit,
  System.StrUtils,
  StringReportPayloadUnit,
  TimeStampedReportPayloadWrapperUnit,
  SafeFormatUnit,
  GlobalLoggerSettingsUnit;

function TIndentedLogger.ClassMethod(const Instance: TObject; const MethodName: string): string;
begin
  Result := SafeFormat('%s[%p].%s', [SafeClassName(Instance), Pointer(Instance), MethodName]);
end;

function TIndentedLogger.FindInstanceInClassList(const Instance: TObject; const ClassList: IStringListWrapper): Boolean;
var
  Index: Integer;
  CurrentClassObject: TObject;
  CurrentClass: TClass;
begin
  Result := False;
  for Index := 0 to ClassList.Count - 1 do
  begin
    CurrentClassObject := ClassList.Objects[Index];
    CurrentClass := TClass(CurrentClassObject);
    if (Instance is CurrentClass) then
    begin
      Result := True;
      Break;
    end;
  end;
end;

function TIndentedLogger.SafeClassName(const Instance: TObject): string;
begin
  if Instance = nil then
    Result := '(NIL)'
  else try
    Result := Instance.ClassName;
  except
    on E: Exception do
      Result := 'Exception: '+E.Message;
  end;
end;

procedure TIndentedLogger.Log(const FormatMask: string; const Arguments: array of const);
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  Report(FormatMask, Arguments);
end;

procedure TIndentedLogger.Log(const FormatMask: string; const Arguments: array of const; const FormatSettings: TFormatSettings);
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  Report(FormatMask, Arguments, FormatSettings);
end;

procedure TIndentedLogger.Enter(const MethodName: string);
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  Log('>' + MethodName);
  EnterTraceLevel;
end;

procedure TIndentedLogger.Enter(const Instance: TObject; const MethodName: string);
var
  NewTraceEnabled: Boolean;
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  NewTraceEnabled := CanTraceInstance(Instance);
  if NewTraceEnabled then
    Enter(ClassMethod(Instance, MethodName))
  else
    EnterTraceLevel;
  Enabled := NewTraceEnabled;
end;

procedure TIndentedLogger.Enter(const Instance: TObject; const Mask: string; const Args: array of const);
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  Enter(Instance, SafeFormat(Mask, Args));
end;

procedure TIndentedLogger.Leave(const Instance: TObject; const MethodName: string);
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  if CanTraceInstance(Instance) then
    Leave(ClassMethod(Instance, MethodName))
  else
    LeaveTraceLevel;
end;

procedure TIndentedLogger.Leave(const Instance: TObject; const Mask: string; const Args: array of const);
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  Leave(Instance, SafeFormat(Mask, Args));
end;

procedure TIndentedLogger.Leave(const MethodName: string);
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  LeaveTraceLevel();
  Log('<' + MethodName);
end;

function TIndentedLogger.IsADoTraceInstance(Instance: TObject): Boolean;
begin
  Result := FindInstanceInClassList(Instance, DoTrace);
end;

function TIndentedLogger.IsADoNotTraceInstance(Instance: TObject): Boolean;
begin
  Result := FindInstanceInClassList(Instance, DoNotTrace);
end;

function TIndentedLogger.CanTraceInstance(Instance: TObject): Boolean;
begin
  if DoTraceOnlyEnabled then
    Result := IsADoTraceInstance(Instance)
  else
    Result := not IsADoNotTraceInstance(Instance);
end;

procedure TIndentedLogger.EnterTraceLevel;
var
  TraceLevelEnabledString: string;
begin
  TraceLevel := TraceLevel + 1;
  if Assigned(TraceLevels) then
  begin
    TraceLevelEnabledString := BoolToStr(Enabled);
    TraceLevels.Add(TraceLevelEnabledString);
  end;
end;

procedure TIndentedLogger.Initialize;
begin
  inherited Initialize;
  DoTraceOnlyEnabled := False;
  TraceLevels := TStringListWrapper.Create();
  DoTrace := TStringListWrapper.Create();
  DoNotTrace := TStringListWrapper.Create();
end;

procedure TIndentedLogger.LeaveTraceLevel;
var
  TraceLevelEnabledString: string;
  TraceLevelsCount: Integer;
begin
  if Assigned(TraceLevels) then
  begin
    TraceLevelsCount := TraceLevels.Count;
    if TraceLevelsCount > 0 then
    begin
      TraceLevelEnabledString := TraceLevels[TraceLevelsCount-1];
      Enabled := StrToBool(TraceLevelEnabledString);
      TraceLevels.Delete(TraceLevelsCount-1);
    end;
  end;
  TraceLevel := TraceLevel - 1;
end;

procedure TIndentedLogger.Report(const FormatMask: string; const Arguments: array of const);
var
  Line: string;
begin
  Line := SafeFormat(FormatMask, Arguments);
  Report(Line);
end;

procedure TIndentedLogger.Report(const FormatMask: string; const Arguments: array of const; const FormatSettings: TFormatSettings);
var
  Line: string;
begin
  Line := SafeFormat(FormatMask, Arguments, FormatSettings);
  Report(Line);
end;

procedure TIndentedLogger.Report(const Payload: IReportPayload);
var
  IndentedPayloadPayload: IReportPayload;
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  IndentedPayloadPayload := TTimeStampedReportPayloadWrapper.Create(TraceLevel, Payload);
  inherited Report(IndentedPayloadPayload);
end;

procedure TIndentedLogger.Report(const Line: string);
var
  Payload: IReportPayload;
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  Payload := TStringReportPayload.Create(TraceLevel, Line, Now);
  Report(Payload);
end;

procedure TIndentedLogger.SetTraceLevel(const Value: Integer);
begin
  FTraceLevel := Value;
end;

end.
