{ Copyright (c) 2007-2013 Jeroen Wiert Pluimers for BeSharp.net and better office benelux.
Full BSD License is available at http://besharp.codeplex.com/license and http://bo.codeplex.com/license }

unit ReportEventUnit;

interface

uses
  ReportPayloadInterfaceUnit;

type
  ///	<summary>
  ///	  Event method type for reporting a payload of type
  ///	  <see cref="ReportPayloadInterfaceUnit|IReportPayload" />.
  ///	</summary>
  TReportEvent = procedure(const Payload: IReportPayload) of object;

implementation

end.
