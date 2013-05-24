{ Copyright (c) 2007-2013 Jeroen Wiert Pluimers for BeSharp.net and better office benelux.
Full BSD License is available at http://besharp.codeplex.com/license and http://bo.codeplex.com/license }

unit ConsoleReporterUnit;

interface

uses
  SimpleAbstractReporterUnit;

type
  ///	<summary>
  ///	  <para>
  ///	    <see cref="SimpleAbstractReporterUnit|TSimpleAbstractReporter" />
  ///	    implementation that can report to the console through
  ///	    <see cref="System|WriteLn" />.
  ///	  </para>
  ///	</summary>
  ///	<remarks>
  ///	  <para>
  ///	    Should work on any platform that implements
  ///	    <see cref="System|WriteLn" />.
  ///	  </para>
  ///	</remarks>
  TConsoleReporter = class(TSimpleAbstractReporter)
  public
    procedure Report(const Line: string); override;
  end;

implementation

procedure TConsoleReporter.Report(const Line: string);
begin
  WriteLn(Line);
end;

end.

