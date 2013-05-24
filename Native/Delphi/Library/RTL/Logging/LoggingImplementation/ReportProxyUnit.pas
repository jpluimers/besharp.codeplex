{ Copyright (c) 2007-2013 Jeroen Wiert Pluimers for BeSharp.net and better office benelux.
Full BSD License is available at http://besharp.codeplex.com/license and http://bo.codeplex.com/license }

unit ReportProxyUnit;

interface

uses
  ReportProxyInterfaceUnit,
  ReportPayloadInterfaceUnit,
  ReportEventUnit;

type
  ///	<summary>
  ///	  For creating instances of TReportProxy descendants dynamically.
  ///	</summary>
  TReportProxyClass = class of TReportProxy;

  ///	<summary>
  ///	  Basic implementation of IReportProxy
  ///	</summary>
  ///	<remarks>
  ///	  <para>
  ///	    Any call to the <see cref="Report" /> method will be forwarded to the
  ///	    <see cref="OnReport"/> event (if that event is assigned).
  ///	  </para>
  ///	  <para>
  ///	    The <see cref="Create"/> method is overloaded to you can pass an
  ///     <see cref="OnReportEvent"/> method of type <see cref="ReportEventUnit|TReportEvent"/>
  ///	    at creation time.
  ///	  </para>
  ///	  <para>
  ///	    The <see cref="SetOnReport"/> method is virtual so you can override it in
  ///	    descendants.
  ///	  </para>
  ///	</remarks>
  TReportProxy = class(TInterfacedObject, IReportProxy)
  strict private
    FOnReport: TReportEvent;
  strict protected
    procedure Initialize; virtual;
    function GetOnReport: TReportEvent; virtual; stdcall;
    procedure Report(const Payload: IReportPayload); overload; virtual;
    procedure SetOnReport(const Value: TReportEvent); virtual; stdcall;
    property OnReport: TReportEvent read GetOnReport write SetOnReport;
  public
    constructor Create(const OnReportEvent: TReportEvent); overload;
    constructor Create; overload;
  end;

implementation

constructor TReportProxy.Create(const OnReportEvent: TReportEvent);
begin
  inherited Create();
  FOnReport := OnReportEvent;
  Initialize();
end;

constructor TReportProxy.Create;
begin
  inherited Create();
  Initialize();
end;

function TReportProxy.GetOnReport: TReportEvent;
begin
  Result := FOnReport;
end;

procedure TReportProxy.Initialize;
begin
  // placeholder for descending classes
end;

procedure TReportProxy.Report(const Payload: IReportPayload);
var
  OnLog: TReportEvent;
begin
  OnLog := Self.OnReport;
  if Assigned(OnLog) then
    OnLog(Payload);
end;

procedure TReportProxy.SetOnReport(const Value: TReportEvent);
begin
  FOnReport := Value;
end;

end.

