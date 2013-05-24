{ Copyright (c) 2007-2013 Jeroen Wiert Pluimers for BeSharp.net and better office benelux.
Full BSD License is available at http://besharp.codeplex.com/license and http://bo.codeplex.com/license }

unit AbstractReporterUnit;

interface

uses
  ReporterInterfaceUnit,
  ReportPayloadInterfaceUnit;

type
  ///	<summary>
  ///	  <para>
  ///	    Abstract implementation of the
  ///	    <see cref="ReporterInterfaceUnit|IReport" /> interface descending
  ///	    from <see cref="System|TInterfacedObject" />.
  ///	  </para>
  ///	  <para>
  ///	    It serves as a base of concrete descendents that only need to
  ///	    override the <see cref="Report" /> method.
  ///	  </para>
  ///	</summary>
  ///	<remarks>
  ///	  <para>
  ///	    <see cref="TReporter" /> introduces the abstract <see cref="Report" />
  ///	     method forcing you to override these in descendants.
  ///	  </para>
  ///	  <para>
  ///	    Since the signature is already correct, this allows you to use code
  ///	    completion for quickly setting up a descendant.
  ///	  </para>
  ///	</remarks>
  TAbstractReporter = class abstract(TInterfacedObject, IReporter)
  public
    procedure Report(const Payload: IReportPayload); overload; virtual; abstract;
  end;

implementation

end.

