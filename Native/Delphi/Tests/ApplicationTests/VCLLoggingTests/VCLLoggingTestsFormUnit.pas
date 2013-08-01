{ Copyright (c) 2007-2012 Jeroen Wiert Pluimers for BeSharp.net and better office benelux.
Full BSD License is available at http://besharp.codeplex.com/license and http://bo.codeplex.com/license }

unit VCLLoggingTestsFormUnit;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.StdCtrls, Vcl.ExtCtrls, LoggerInterfaceUnit,
  ReportPayloadInterfaceUnit, ReportProxyUnit, ReportProxyInterfaceUnit;

type
  TVCLLoggingTestsForm = class(TForm)
    LogMemo: TMemo;
    RunButton: TButton;
    TopPanel: TPanel;
    procedure RunButtonClick(Sender: TObject);
  strict private
    FEnabledLogger: IReportProxy;
    procedure Report(const Payload: IReportPayload);
    procedure RunDescriptionLogger;
    procedure RunEnabledLogger;
    procedure RunIndexedLogger;
    procedure RunIndentedLogger;
    procedure RunLogger;
    procedure RunInnerIndentedLogger;
  strict protected
    function CreateLogger<TLoggerInterface: IReportProxy; TLoggerClass: TReportProxy, IReportProxy>(const IID: TGUID): TLoggerInterface;
    function GetDescriptionLogger: IDescriptionLogger; virtual;
    function GetEnabledLogger: IEnabledLogger; virtual;
    function GetIndentedLogger: IIndentedLogger; virtual;
    function GetIndexLogger: IIndexLogger; virtual;
    function GetLogger: ILogger; virtual;
    function GetOrCreateLogger<TLoggerInterface: IReportProxy; TLoggerClass: TReportProxy, IReportProxy>(const IID: TGUID): TLoggerInterface;
  public
    destructor Destroy; override;
    property DescriptionLogger: IDescriptionLogger read GetDescriptionLogger;
    property EnabledLogger: IEnabledLogger read GetEnabledLogger;
    property IndentedLogger: IIndentedLogger read GetIndentedLogger;
    property IndexLogger: IIndexLogger read GetIndexLogger;
    property Logger: ILogger read GetLogger;
  end;

var
  VCLLoggingTestsForm: TVCLLoggingTestsForm;

implementation

uses
  IndentedLoggerUnit,
  DescriptionLoggerUnit,
  LoggerUnit,
  GlobalLoggerSettingsUnit,
  EnabledLoggerUnit,
  IndexLoggerUnit;

{$R *.dfm}

destructor TVCLLoggingTestsForm.Destroy;
begin
  inherited;
  FEnabledLogger := nil;
end;

function TVCLLoggingTestsForm.CreateLogger<TLoggerInterface, TLoggerClass>(const IID: TGUID): TLoggerInterface;
var
  ReportProxyClass: TReportProxyClass;
  ReportProxy: TReportProxy;
// IID2: TGUID;
begin
  ReportProxyClass := TLoggerClass;
  // IID2 := TLoggerInterface; // doesn't work as you cannot specify that TLoggerInterface has a GUID, and there is no 'interface of' language construct.
  // it is impossible to assure that TLoggerInterface matches IID, as there is no 'interface of' language construct.
  if Supports(ReportProxyClass, IID) then
  begin
    ReportProxy := ReportProxyClass.Create(Report); // cannot do this with generics
    if ReportProxy.GetInterface(IID, Result) then
    begin
      FEnabledLogger := Result;
      Exit;
    end;
  end;
  raise ENotSupportedException.CreateFmt('Class %s does not support interface with GUID %s', [ReportProxyClass.ClassName, GuidToString(IID)]);
//    FEnabledLogger :=  TTeeLogger.Create([
//      TDbWinLogger.Create(),
//      TStringsLogger.Create(LogMemo.Lines),
//      TStringsLogger.Create(LogRadioGroup.Items)
//    ]);
end;

function TVCLLoggingTestsForm.GetDescriptionLogger: IDescriptionLogger;
begin
  Result := GetOrCreateLogger<IDescriptionLogger, TDescriptionLogger>(IDescriptionLogger);
end;

function TVCLLoggingTestsForm.GetEnabledLogger: IEnabledLogger;
begin
  Result := GetOrCreateLogger<IEnabledLogger, TEnabledLogger>(IEnabledLogger);
end;

function TVCLLoggingTestsForm.GetIndentedLogger: IIndentedLogger;
begin
  Result := GetOrCreateLogger<IIndentedLogger, TIndentedLogger>(IIndentedLogger);
end;

function TVCLLoggingTestsForm.GetIndexLogger: IIndexLogger;
begin
  Result := GetOrCreateLogger<IIndexLogger, TIndexLogger>(IIndexLogger);
end;

function TVCLLoggingTestsForm.GetLogger: ILogger;
begin
  Result := GetOrCreateLogger<ILogger, TLogger>(ILogger);
end;

function TVCLLoggingTestsForm.GetOrCreateLogger<TLoggerInterface, TLoggerClass>(const IID: TGUID): TLoggerInterface;
begin
  if Assigned(FEnabledLogger) and Supports(FEnabledLogger, IID, Result) then
    Exit;
  Result := CreateLogger<TLoggerInterface, TLoggerClass>(IID);
end;

procedure TVCLLoggingTestsForm.Report(const Payload: IReportPayload);
var
  Line: string;
begin
  Line := Payload.ToString();
  LogMemo.Lines.Add(Line);
end;

procedure TVCLLoggingTestsForm.RunButtonClick(Sender: TObject);
begin
  RunEnabledLogger();
  RunIndentedLogger();
  RunDescriptionLogger();
  RunIndexedLogger();
  RunLogger();
end;

procedure TVCLLoggingTestsForm.RunDescriptionLogger;
var
  B: Boolean;
  String14: ShortString;
begin
  String14 := 'a short string';
  B := True;

  DescriptionLogger.Log('Boolean', not B);
  DescriptionLogger.Log('Integer', MaxInt);
  DescriptionLogger.Log('Pointer', @Self);
  DescriptionLogger.Log('string', ClassName);
  DescriptionLogger.Log('PTypeInfo', TypeInfo(Boolean), Ord(B));
  DescriptionLogger.Log('PTypeInfo', Self.ClassType.ClassInfo, 'prefix');
  DescriptionLogger.Log('ShortStringBase', String14);
end;

procedure TVCLLoggingTestsForm.RunEnabledLogger;
var
  Enabled: Boolean;
  GlobalEnabled: Boolean;
begin
  for Enabled := False to True do
  begin
    for GlobalEnabled := False to True do
    begin
      TGlobalLoggerSettings.GlobalEnabled := GlobalEnabled;
      EnabledLogger.Enabled := Enabled;
      EnabledLogger.Log(Format('GlobalEnabled=%s, Enabled=%s', [BoolToStr(GlobalEnabled), BoolToStr(Enabled)]));
    end;
  end;
end;

procedure TVCLLoggingTestsForm.RunIndexedLogger;
var
  B: Boolean;
  String14: ShortString;
  Index: Integer;
begin
  for Index := 0 to 1 do
  begin
    String14 := 'a short string';
    B := True;

    IndexLogger.Log('Boolean', Index, not B);
    IndexLogger.Log('Integer', Index, MaxInt);
    IndexLogger.Log('Pointer', Index, @Self);
    IndexLogger.Log('string', Index, ClassName);
    IndexLogger.Log('ShortStringBase', Index, String14);

    IndexLogger.Log('Boolean', Index, 'suffix', not B);
    IndexLogger.Log('Integer', Index, 'suffix', MaxInt);
    IndexLogger.Log('Pointer', Index, 'suffix', @Self);
    IndexLogger.Log('string', Index, 'suffix', ClassName);
    IndexLogger.Log('ShortStringBase', Index, 'suffix', String14);
  end;
end;

procedure TVCLLoggingTestsForm.RunIndentedLogger;
const
  Outer = 0;
  Inner = 2;
  MethodName = 'RunIndentedLogger';
  InnerMethodName = 'RunInnerIndentedLogger';
var
  CallLevel: Integer;
begin
  IndentedLogger.Enter(Self, MethodName);
  IndentedLogger.Enter(Self, '%s will call %s', [MethodName, InnerMethodName]);
  RunInnerIndentedLogger;
  IndentedLogger.Leave(Self, '%s will call %s', [MethodName, InnerMethodName]);
  IndentedLogger.Leave(Self, MethodName);

  for CallLevel := Outer to Inner do
  begin
    IndentedLogger.Enter(Format('%s%d', [InnerMethodName, CallLevel]));
    RunInnerIndentedLogger;
  end;
  for CallLevel := Inner downto Outer do
  begin
    IndentedLogger.Leave(Format('%s%d', [MethodName, CallLevel]));
    EnabledLogger.Log(Format('Left %s%d', [MethodName, CallLevel]));
  end;
end;

procedure TVCLLoggingTestsForm.RunLogger;
var
  E: Exception;
begin
  Logger.Flush();
  Logger.Log();
  E := ENotSupportedException.Create('not supported exception');
  try
    Logger.Log(E);
  finally
    E.Free();
  end;
  Logger.LogMulti('Description', ['One', 'Two', 'Three']);
end;

procedure TVCLLoggingTestsForm.RunInnerIndentedLogger;
var
  FormatSettings: TFormatSettings;
begin
  RunEnabledLogger();
  IndentedLogger.Log('FormatMask; %d "%s"', [1, 'foo']);
  FormatSettings := TFormatSettings.Create;
  FormatSettings.DecimalSeparator := ';';
  FormatSettings.ThousandSeparator := ':';
  IndentedLogger.Log('FormatMask; %g "%s"', [1234567.89, 'foo'], FormatSettings);
//  EnabledLogger.Log(Sender.ClassName, (Sender as TComponent).Name);
//  EnabledLogger.Log('Foo Bar', 1, 'Foo', 'Bar');
end;

end.
