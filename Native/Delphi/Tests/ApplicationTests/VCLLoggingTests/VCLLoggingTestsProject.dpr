program VCLLoggingTestsProject;

uses
  Vcl.Forms,
  VCLLoggingTestsFormUnit in 'VCLLoggingTestsFormUnit.pas' {VCLLoggingTestsForm},
  LoggerInterfaceUnit in '..\..\..\Library\RTL\Logging\LoggerInterfaceUnit.pas',
  ReportProxyInterfaceUnit in '..\..\..\Library\RTL\Logging\ReportProxyInterfaceUnit.pas',
  ReportPayloadInterfaceUnit in '..\..\..\Library\RTL\Logging\ReportPayloadInterfaceUnit.pas',
  ReporterInterfaceUnit in '..\..\..\Library\RTL\Logging\ReporterInterfaceUnit.pas',
  ReportEventUnit in '..\..\..\Library\RTL\Logging\ReportEventUnit.pas',
  IndentedLoggerUnit in '..\..\..\Library\RTL\Logging\LoggingImplementation\IndentedLoggerUnit.pas',
  StringsReporterUnit in '..\..\..\Library\RTL\Logging\LoggingImplementation\StringsReporterUnit.pas',
  EnabledLoggerUnit in '..\..\..\Library\RTL\Logging\LoggingImplementation\EnabledLoggerUnit.pas',
  ReportProxyUnit in '..\..\..\Library\RTL\Logging\LoggingImplementation\ReportProxyUnit.pas',
  StringListWrapperInterfaceUnit in '..\..\..\Library\RTL\StringListWrapperInterfaceUnit.pas',
  GlobalLoggerSettingsUnit in '..\..\..\Library\RTL\Logging\LoggingImplementation\GlobalLoggerSettingsUnit.pas',
  StringListWrapperUnit in '..\..\..\Library\RTL\RTLImplementation\StringListWrapperUnit.pas',
  StringReportPayloadUnit in '..\..\..\Library\RTL\Logging\LoggingImplementation\StringReportPayloadUnit.pas',
  BaseReportPayloadUnit in '..\..\..\Library\RTL\Logging\LoggingImplementation\BaseReportPayloadUnit.pas',
  TimeStampedReportPayloadWrapperUnit in '..\..\..\Library\RTL\Logging\LoggingImplementation\TimeStampedReportPayloadWrapperUnit.pas',
  BaseReportPayloadWrapperUnit in '..\..\..\Library\RTL\Logging\LoggingImplementation\BaseReportPayloadWrapperUnit.pas',
  SimpleAbstractReporterUnit in '..\..\..\Library\RTL\Logging\LoggingImplementation\SimpleAbstractReporterUnit.pas',
  AbstractReporterUnit in '..\..\..\Library\RTL\Logging\LoggingImplementation\AbstractReporterUnit.pas',
  DescriptionLoggerUnit in '..\..\..\Library\RTL\Logging\LoggingImplementation\DescriptionLoggerUnit.pas',
  SetTypeInformationUnit in '..\..\..\Library\RTL\TypeInformation\SetTypeInformationUnit.pas',
  BaseLoggerUnit in '..\..\..\Library\RTL\Logging\LoggingImplementation\BaseLoggerUnit.pas',
  EnumerationTypeInformationUnit in '..\..\..\Library\RTL\TypeInformation\EnumerationTypeInformationUnit.pas',
  RecordTypeInformationUnit in '..\..\..\Library\RTL\TypeInformation\RecordTypeInformationUnit.pas',
  IndexLoggerUnit in '..\..\..\Library\RTL\Logging\LoggingImplementation\IndexLoggerUnit.pas',
  SafeFormatUnit in '..\..\..\Library\RTL\RTLImplementation\SafeFormatUnit.pas',
  LoggerUnit in '..\..\..\Library\RTL\Logging\LoggingImplementation\LoggerUnit.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.MainFormOnTaskbar := True;
  Application.CreateForm(TVCLLoggingTestsForm, VCLLoggingTestsForm);
  Application.Run;
end.
