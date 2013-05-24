{ Copyright (c) 2007-2013 Jeroen Wiert Pluimers for BeSharp.net and better office benelux.
Full BSD License is available at http://besharp.codeplex.com/license and http://bo.codeplex.com/license }

unit TimeStampedReportPayloadWrapperUnit;

interface

uses
  FormatLineReportPayloadWrapperUnit;

type
  ///	<summary>
  ///	  Adds ISO 8601 formatted time stamp and IndentationString before logging
  ///	  each ReportPayload.ToString() in the log.
  ///	</summary>
  TTimeStampedReportPayloadWrapper = class(TFormatLineReportPayloadWrapper)
  strict protected
    function FormatLine(const Line: string): string; override;
  end;

implementation

uses
  System.SysUtils,
  SafeFormatUnit;

function TTimeStampedReportPayloadWrapper.FormatLine(const Line: string): string;
var
  NowDateTimeString: string;
begin
  // TODO ##jpl ISO 8601 compliant date-time formatting
  // TODO ##jpl: adjust for UTC
  // http://stackoverflow.com/questions/3572128/delphi-equivalent-of-nets-datetime-tostrings-datetime-sortable
  NowDateTimeString := FormatDateTime('yyyy"-"mm"-"dd"T"hh":"mm":"ss.zzz', ReportPayload.TimeStamp);
  Result := SafeFormat('%s %-*d %s%s', [NowDateTimeString, Indentation+1, Indentation, IndentationString, Line]);
end;

end.

