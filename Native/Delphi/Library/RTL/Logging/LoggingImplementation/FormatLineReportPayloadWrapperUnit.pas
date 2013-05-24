{ Copyright (c) 2007-2013 Jeroen Wiert Pluimers for BeSharp.net and better office benelux.
Full BSD License is available at http://besharp.codeplex.com/license and http://bo.codeplex.com/license }

unit FormatLineReportPayloadWrapperUnit;

interface

uses
  BaseReportPayloadWrapperUnit;

type
  ///	<summary>
  ///	  Allows descendants to format the ToString() of the Payload in the
  ///	  FormatLine method.
  ///	</summary>
  TFormatLineReportPayloadWrapper = class(TBaseReportPayloadWrapper)
  strict protected
    function FormatLine(const Line: string): string; virtual;
    function ToString: string; override;
  end;

implementation

function TFormatLineReportPayloadWrapper.FormatLine(const Line: string): string;
begin
  Result := Line;
end;

function TFormatLineReportPayloadWrapper.ToString: string;
var
  Line: string;
begin
  Line := ReportPayload.ToString();
  Result := FormatLine(Line);
end;

end.

