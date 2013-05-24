{ Copyright (c) 2007-2013 Jeroen Wiert Pluimers for BeSharp.net and better office benelux.
Full BSD License is available at http://besharp.codeplex.com/license and http://bo.codeplex.com/license }

unit ReporterInterfaceUnit;


interface

uses
  ReportPayloadInterfaceUnit;

type
  ///	<summary>
  ///	  Base interface for a conduit between the logging infra structure and actual logging targets.
  ///	</summary>
  ///	<remarks>
  ///	  <para>
  ///	    The <see cref="Report" /> method must match the signature of
  ///	    <see cref="ReportEventUnit|TReportEvent" />.
  ///	  </para>
  ///	  <para>
  ///	    The <see cref="Report" /> method is marked <c>overload</c> because
  ///	    that facilitates introducing <c>Report</c> methods with different
  ///	    signatures in descending interfaces or classes that implement the
  ///	    <see cref="IReporter" /> interface. 
  ///	  </para>
  ///	</remarks>
  IReporter = interface(IInterface)
    procedure Report(const Payload: IReportPayload); overload;
  end;

implementation

end.

