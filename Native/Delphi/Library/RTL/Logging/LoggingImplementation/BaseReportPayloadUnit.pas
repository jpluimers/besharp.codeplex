{ Copyright (c) 2007-2013 Jeroen Wiert Pluimers for BeSharp.net and better office benelux.
Full BSD License is available at http://besharp.codeplex.com/license and http://bo.codeplex.com/license }

unit BaseReportPayloadUnit;

interface

uses
  ReportPayloadInterfaceUnit;

type
  ///	<summary>
  ///	  <see cref="System|TInterfacedObject" /> implementing
  ///	  <see cref="ReportPayloadInterfaceUnit|IReportPayload" />.
  ///	</summary>
  ///	<remarks>
  ///	  The <see cref="Create" /> constructor takes all the properties of
  ///	  <see cref="ReportPayloadInterfaceUnit|IReportPayload" /> as parameters:
  ///	  <c>AIndentation</c> and <c>ATimeStamp</c>.
  ///	</remarks>
  TBaseReportPayload = class(TInterfacedObject, IReportPayload)
  strict private
    FIndentation: Integer;
    FTimeStamp: TDateTime;
  strict protected
    function GetIndentation: Integer; virtual; stdcall;
    function GetIndentationString: string; overload; virtual;
    function GetIndentationString(const Indentation: Integer): string; overload; virtual;
    function GetTimeStamp: TDateTime; virtual; stdcall;
  public
    constructor Create(const AIndentation: Integer; const ATimeStamp: TDateTime);
    property Indentation: Integer read GetIndentation;
    property IndentationString: string read GetIndentationString;
    property TimeStamp: TDateTime read GetTimeStamp;
  end;

implementation

uses
  System.StrUtils;

constructor TBaseReportPayload.Create(const AIndentation: Integer; const ATimeStamp: TDateTime);
begin
  inherited Create;
  FIndentation := AIndentation;
  FTimeStamp := ATimeStamp;
end;

function TBaseReportPayload.GetIndentation: Integer;
begin
  Result := FIndentation;
end;

function TBaseReportPayload.GetIndentationString: string;
begin
  Result := GetIndentationString(Indentation);
end;

function TBaseReportPayload.GetIndentationString(const Indentation: Integer): string;
begin
  Result := DupeString('  ', Indentation);
end;

function TBaseReportPayload.GetTimeStamp: TDateTime;
begin
  Result := FTimeStamp;
end;

end.

