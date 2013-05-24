{ Copyright (c) 2007-2013 Jeroen Wiert Pluimers for BeSharp.net and better office benelux.
Full BSD License is available at http://besharp.codeplex.com/license and http://bo.codeplex.com/license }

unit SafeFormatUnit;

interface

uses
  System.SysUtils;

function SafeFormat(const Format: string; const Args: array of const): string; overload;
function SafeFormat(const Format: string; const Args: array of const; const FormatSettings: TFormatSettings): string; overload;

implementation

function SafeFormat(const Format: string; const Args: array of const): string;
begin
  try
    Result := System.SysUtils.Format(Format, Args);
  except
    on E: Exception do
      Result := E.Message;
  end;
end;

function SafeFormat(const Format: string; const Args: array of const; const FormatSettings: TFormatSettings): string;
begin
  try
    Result := System.SysUtils.Format(Format, Args, FormatSettings);
  except
    on E: Exception do
      Result := E.Message;
  end;
end;

end.
