unit HResultUnit;

interface

uses
  System.SysUtils;

type
  THResult = class(TObject)
  public
    function Format(const Format: string; const Args: array of const): string; overload;
    function Format(const Format: string; const Args: array of const; const AFormatSettings: TFormatSettings): string; overload;
    function FormatBuf(Buffer: PWideChar; BufLen: Cardinal; const Format; FmtLen: Cardinal; const Args: array of const): Cardinal; overload;
    function FormatBuf(Buffer: PWideChar; BufLen: Cardinal; const Format; FmtLen: Cardinal; const Args: array of const; const AFormatSettings: TFormatSettings):
        Cardinal; overload;
    function FormatBuf(var Buffer; BufLen: Cardinal; const Format; FmtLen: Cardinal; const Args: array of const): Cardinal; overload;
    function FormatBuf(var Buffer; BufLen: Cardinal; const Format; FmtLen: Cardinal; const Args: array of const; const AFormatSettings: TFormatSettings): Cardinal;
        overload;
    function FormatBuf(var Buffer: UnicodeString; BufLen: Cardinal; const Format; FmtLen: Cardinal; const Args: array of const): Cardinal; overload;
    function FormatBuf(var Buffer: UnicodeString; BufLen: Cardinal; const Format; FmtLen: Cardinal; const Args: array of const; const AFormatSettings:
        TFormatSettings): Cardinal; overload;
    function FormatCurr(const Format: string; Value: Currency): string; overload; inline;
    function FormatCurr(const Format: string; Value: Currency; const AFormatSettings: TFormatSettings): string; overload;
    function FormatDateTime(const Format: string; DateTime: TDateTime): string; overload; inline;
    function FormatDateTime(const Format: string; DateTime: TDateTime; const AFormatSettings: TFormatSettings): string; overload;
    function FormatFloat(const Format: string; Value: Extended): string; overload; inline;
    function FormatFloat(const Format: string; Value: Extended; const AFormatSettings: TFormatSettings): string; overload;
  end;

implementation

function THResult.Format(const Format: string; const Args: array of const): string;
begin
  Result := System.SysUtils.Format(Format, Args);
end;

function THResult.Format(const Format: string; const Args: array of const; const AFormatSettings: TFormatSettings): string;
begin
  Result := System.SysUtils.Format(Format, Args, AFormatSettings);
end;

function THResult.FormatBuf(Buffer: PWideChar; BufLen: Cardinal; const Format; FmtLen: Cardinal; const Args: array of const): Cardinal;
begin
  Result := System.SysUtils.FormatBuf(Buffer, BufLen, Format, FmtLen, Args);
end;

function THResult.FormatBuf(Buffer: PWideChar; BufLen: Cardinal; const Format; FmtLen: Cardinal; const Args: array of const; const AFormatSettings:
    TFormatSettings): Cardinal;
begin
  Result := System.SysUtils.FormatBuf(Buffer, BufLen, Format, FmtLen, Args, AFormatSettings);
end;

function THResult.FormatBuf(var Buffer; BufLen: Cardinal; const Format; FmtLen: Cardinal; const Args: array of const): Cardinal;
begin
  Result := System.SysUtils.FormatBuf(Buffer, BufLen, Format, FmtLen, Args);
end;

function THResult.FormatBuf(var Buffer; BufLen: Cardinal; const Format; FmtLen: Cardinal; const Args: array of const; const AFormatSettings:
    TFormatSettings): Cardinal;
begin
  Result := System.SysUtils.FormatBuf(Buffer, BufLen, Format, FmtLen, Args, AFormatSettings);
end;

function THResult.FormatBuf(var Buffer: UnicodeString; BufLen: Cardinal; const Format; FmtLen: Cardinal; const Args: array of const): Cardinal;
begin
  Result := FormatBuf(Buffer, BufLen, Format, FmtLen, Args, FormatSettings);
end;

function THResult.FormatBuf(var Buffer: UnicodeString; BufLen: Cardinal; const Format; FmtLen: Cardinal; const Args: array of const; const
    AFormatSettings: TFormatSettings): Cardinal;
begin
  Result := System.SysUtils.FormatBuf(Buffer, BufLen, Format, FmtLen, Args, AFormatSettings);
end;

function THResult.FormatCurr(const Format: string; Value: Currency): string;
begin
  Result := System.SysUtils.FormatCurr(Format, Value);
end;

function THResult.FormatCurr(const Format: string; Value: Currency; const AFormatSettings: TFormatSettings): string;
begin
  Result := System.SysUtils.FormatCurr(Format, Value, AFormatSettings);
end;

function THResult.FormatDateTime(const Format: string; DateTime: TDateTime): string;
begin
  Result := System.SysUtils.FormatDateTime(Format, DateTime);
end;

function THResult.FormatDateTime(const Format: string; DateTime: TDateTime; const AFormatSettings: TFormatSettings): string;
begin
  Result := System.SysUtils.FormatDateTime(Format, DateTime, AFormatSettings);
end;

function THResult.FormatFloat(const Format: string; Value: Extended): string;
begin
  Result := System.SysUtils.FormatFloat(Format, Value);
end;

function THResult.FormatFloat(const Format: string; Value: Extended; const AFormatSettings: TFormatSettings): string;
begin
  Result := System.SysUtils.FormatFloat(Format, Value, AFormatSettings);
end;

end.
