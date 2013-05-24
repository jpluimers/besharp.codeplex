{ Copyright (c) 2007-2013 Jeroen Wiert Pluimers for BeSharp.net and better office benelux.
Full BSD License is available at http://besharp.codeplex.com/license and http://bo.codeplex.com/license }

unit GlobalLoggerSettingsUnit;

interface

type
  TGlobalLoggerSettings = class
  strict private
  class var
    FGlobalEnabled: Boolean;{ global trace state -
    if FALSE, then NO trace is performed resulting in minimal CPU time spent }
  strict protected
    class function GetGlobalEnabled: Boolean; static;
    class procedure SetGlobalEnabled(const Value: Boolean); static;
  public
    class property GlobalEnabled: Boolean read GetGlobalEnabled write SetGlobalEnabled;
  end;

implementation

class function TGlobalLoggerSettings.GetGlobalEnabled: Boolean;
begin
  Result := FGlobalEnabled;
end;

class procedure TGlobalLoggerSettings.SetGlobalEnabled(const Value: Boolean);
begin
  FGlobalEnabled := Value;
end;

initialization
  TGlobalLoggerSettings.GlobalEnabled := True;
end.
