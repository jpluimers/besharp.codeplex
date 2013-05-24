{ Copyright (c) 2007-Copyright (c) 2007-2013 Jeroen Wiert Pluimers Jeroen Wiert Pluimers for BeSharp.net and better office benelux.
Full BSD License is available at http://besharp.codeplex.com/license and http://bo.codeplex.com/license }

unit StringsReporterUnit;

interface

uses
  System.Classes,
  SimpleAbstractReporterUnit;

type
  ///	<summary>
  ///	  <para>
  ///	    <see cref="AbstractReporterUnit|TSimpleAbstractReporter" />
  ///	    implementation that can report to a
  ///	    <see cref="System.Classes|TStrings" /> instance.
  ///	  </para>
  ///	</summary>
  ///	<remarks>
  ///	  <para>
  ///	    All <see cref="Report" /> calls are appended to this
  ///	    <see cref="System.Classes|TStrings" /> instance.
  ///	  </para>
  ///	  <para>
  ///	    You have to pass the initial <see cref="System.Classes|TStrings" />
  ///	    instance using the Create constructor.
  ///	  </para>
  ///	</remarks>
  TStringsReporter = class(TSimpleAbstractReporter)
  strict private
    FStrings: TStrings;
  public
    constructor Create(const Strings: TStrings);
    destructor Destroy; override;
    procedure Report(const Line: string); override;
  end;

implementation

constructor TStringsReporter.Create(const Strings: TStrings);
begin
  inherited Create();
  FStrings := Strings;
end;

destructor TStringsReporter.Destroy;
begin
  inherited Destroy();
  FStrings := nil;
end;

procedure TStringsReporter.Report(const Line: string);
var
  Strings: TStrings;
begin
  Strings := FStrings;
  if Assigned(Strings) then
    Strings.Append(Line);
end;

end.
