{ Copyright (c) 2007-2013 Jeroen Wiert Pluimers for BeSharp.net and better office benelux.
Full BSD License is available at http://besharp.codeplex.com/license and http://bo.codeplex.com/license }

unit SimpleAbstractReporterUnit;

interface

uses
  AbstractReporterUnit,
  ReportPayloadInterfaceUnit;

type
  ///	<summary>
  ///	  <see cref="AbstractReporterUnit|TAbstractReporter" /> implementation
  ///	  that only understands the ToString of the IReportPayload.
  ///	</summary>
  ///	<remarks>
  ///	  <para>
  ///	    Introduces an abstract <see cref="Report" /> method that reports a
  ///	    Line of type <see cref="System|string" />.
  ///	  </para>
  ///	  <para>
  ///	    Implements the
  ///	    <see cref="AbstractReporterUnit|TAbstractReporter.Report" /> method
  ///	    to call the new <see cref="Report" /> method.
  ///	  </para>
  ///	</remarks>
  TSimpleAbstractReporter = class(TAbstractReporter)
  public
    procedure Report(const Line: string); overload; virtual; abstract;
    procedure Report(const Payload: IReportPayload); override;
  end;

implementation

procedure TSimpleAbstractReporter.Report(const Payload: IReportPayload);
var
  Line: string;
begin
  Line := Payload.ToString();
  Report(Line);
end;

end.
