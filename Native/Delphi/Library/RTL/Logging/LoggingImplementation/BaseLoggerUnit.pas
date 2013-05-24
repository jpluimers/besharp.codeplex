{ Copyright (c) 2007-2013 Jeroen Wiert Pluimers for BeSharp.net and better office benelux.
Full BSD License is available at http://besharp.codeplex.com/license and http://bo.codeplex.com/license }

unit BaseLoggerUnit;

interface

uses
  IndentedLoggerUnit;

type
  TBaseLogger = class(TIndentedLogger)
  protected
    function PointerToString(const Item: Pointer): string;
  end;

implementation

uses
  SafeFormatUnit;

function TBaseLogger.PointerToString(const Item: Pointer): string;
begin
  Result := SafeFormat('$%p', [Item]);
end;

end.
