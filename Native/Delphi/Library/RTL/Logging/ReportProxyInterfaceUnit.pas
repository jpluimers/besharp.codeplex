{ Copyright (c) 2007-2013 Jeroen Wiert Pluimers for BeSharp.net and better office benelux.
Full BSD License is available at http://besharp.codeplex.com/license and http://bo.codeplex.com/license }

unit ReportProxyInterfaceUnit;

interface

uses
  ReportPayloadInterfaceUnit,
  ReporterInterfaceUnit,
  ReportEventUnit;

type
  ///	<summary>
  ///	  Proxy for a <see cref="ReportEventUnit|TReportEvent" /> instance.
  ///	</summary>
  ///	<remarks>
  ///	  <para>
  ///	    This makes it easier to hook up event methods to your reporting
  ///	    infrastructure.
  ///	  </para>
  ///	  <para>
  ///	    Implementations should implement the <see cref="Report" /> method so
  ///	    that any calls are at least forwarded to the <see cref="OnReport" />
  ///	    event (if that event is assigned).
  ///	  </para>
  ///	</remarks>
  IReportProxy = interface(IReporter)
  ['{DAE6E497-D6C1-434F-970D-02C0C67351EE}']
    function GetOnReport: TReportEvent; stdcall;
    procedure SetOnReport(const Value: TReportEvent); stdcall;
    property OnReport: TReportEvent read GetOnReport write SetOnReport;
  end;

implementation

end.

