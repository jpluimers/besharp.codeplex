{ Copyright (c) 2007-2013 Jeroen Wiert Pluimers for BeSharp.net and better office benelux.
Full BSD License is available at http://besharp.codeplex.com/license and http://bo.codeplex.com/license }

unit StringReportPayloadUnit;

interface

uses
  BaseReportPayloadUnit;

type
  ///	<summary>
  ///	  IReportPayload for logging a single string.
  ///	</summary>
  TStringReportPayload = class(TBaseReportPayload)
  strict private
    FValue: string;
  strict protected
    function GetValue: string; virtual;
  public
    constructor Create(const AIndentation: Integer; const AValue: string; const ATimeStamp: TDateTime);
    function ToString: string; override;
    property Value: string read GetValue;
  end;

implementation

uses
  System.StrUtils;

constructor TStringReportPayload.Create(const AIndentation: Integer; const AValue: string; const ATimeStamp: TDateTime);
begin
  inherited Create(AIndentation, ATimeStamp);
  FValue := AValue;
end;

function TStringReportPayload.GetValue: string;
begin
  Result := FValue;
end;

function TStringReportPayload.ToString: string;
var
  Indent: string;
begin
  Indent := DupeString('  ', Indentation);
  Result := Indent + Value;
end;

end.
