{ Copyright (c) 2007-2013 Jeroen Wiert Pluimers for BeSharp.net and better office benelux.
Full BSD License is available at http://besharp.codeplex.com/license and http://bo.codeplex.com/license }

unit ReportPayloadInterfaceUnit;

interface

type
  ///	<summary>
  ///	  <para>
  ///	    Anything implementing the IReportPayload interface can be a Report
  ///	    payload for the Logging infrastructure.
  ///	  </para>
  ///	  <para>
  ///	    The easiest way to do this is descend from
  ///	    <see cref="System|TInterfacedObject" />. But in practice anything
  ///	    that descends from <see cref="System|TObject" /> (which has
  ///	    <see cref="System|TObject.ToString" />) and implements the
  ///	    <see cref="System|IInterface" /> infrastructure is a suitable
  ///	    starting point.
  ///	  </para>
  ///	</summary>
  IReportPayload = interface
    function GetIndentation: Integer; stdcall;
    function GetTimeStamp: TDateTime; stdcall;
    function ToString: string;
    property Indentation: Integer read GetIndentation;
    property TimeStamp: TDateTime read GetTimeStamp;
  end;

implementation

end.
