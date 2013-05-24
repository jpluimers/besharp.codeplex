{ Copyright (c) 2007-2013 Jeroen Wiert Pluimers for BeSharp.net and better office benelux.
Full BSD License is available at http://besharp.codeplex.com/license and http://bo.codeplex.com/license }

unit BaseReportPayloadWrapperUnit;

interface

uses
  BaseReportPayloadUnit,
  ReportPayloadInterfaceUnit;

type
  ///	<summary>
  ///	  Wraps a <see cref="ReportPayloadInterfaceUnit|IReportPayload" /> so we
  ///	  can add additional indentation (for implementing
  ///	  <see cref="IndentedLoggerUnit|TIndentedLogger" />).
  ///	</summary>
  TBaseReportPayloadWrapper = class(TBaseReportPayload)
  strict private
    FReportPayload: IReportPayload;
  strict protected
    function GetReportPayload: IReportPayload; virtual;
  public
    // TODO ##jpl: check if we can merge the AIndentation into the AReportPayload
    constructor Create(const AIndentation: Integer; const AReportPayload: IReportPayload);
    property ReportPayload: IReportPayload read GetReportPayload;
  end;

implementation

constructor TBaseReportPayloadWrapper.Create(const AIndentation: Integer; const AReportPayload: IReportPayload);
begin
  inherited Create(AIndentation, AReportPayload.TimeStamp);
  FReportPayload := AReportPayload;
end;

function TBaseReportPayloadWrapper.GetReportPayload: IReportPayload;
begin
  Result := FReportPayload;
end;

end.
