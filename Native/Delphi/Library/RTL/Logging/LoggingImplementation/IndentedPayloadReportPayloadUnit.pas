{ Copyright (c) 2007-2013 Jeroen Wiert Pluimers for BeSharp.net and better office benelux.
Full BSD License is available at http://besharp.codeplex.com/license and http://bo.codeplex.com/license }

unit IndentedPayloadReportPayloadUnit;

interface

uses
  BaseReportPayloadWrapperUnit;

type
  TIndentedReportPayloadWrapper = class(TBaseReportPayloadWrapper)
  strict protected
    function GetIndentationString: string; override;
  end;

implementation

function TIndentedReportPayloadWrapper.GetIndentationString: string;
begin
  Result := inherited GetIndentationString() + GetIndentationString(ReportPayload.Indentation);
end;

end.
