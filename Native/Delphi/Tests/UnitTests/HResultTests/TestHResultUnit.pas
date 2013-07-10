unit TestHResultUnit;

interface

uses
  TestFramework, System.SysUtils;

type
  TestHResult = class(TTestCase)
  public
    procedure SetUp; override;
    procedure TearDown; override;
  published
    procedure Test_EOleException;
  end;

implementation

uses
  System.Win.ComObj, Winapi.Windows;

// 1 Fool the optimizer
procedure Touch(var X);
begin

end;

procedure TestHResult.Test_EOleException;
var
  OleException: EOleException;
  IResult: LongInt; // == HResult
  UResult: Cardinal; // == DWord
begin
  // [DCC Warning] TestHResultUnit.pas(35): W1012 Constant expression violates subrange bounds
  IResult := $800A11FD; // Does not fit, but will cast 4-byte Cardinal into a 4-byte Integer with value -2146823683 ($800A11FD)
  UResult := $800A11FD; // Fits, will have value 2148143613 ($800A11FD)
  Touch(IResult);
  Touch(UResult);
  try
    // [DCC Warning] TestHResultUnit.pas(41): W1012 Constant expression violates subrange bounds
    OleException := EOleException.Create('message', $800A11FD, 'source', 'helpfile', -1);
    raise OleException;
  except
    on E: EOleException do
    begin
      // [DCC Warning] TestHResultUnit.pas(48): W1021 Comparison always evaluates to False
      // [DCC Warning] TestHResultUnit.pas(48): W1023 Comparing signed and unsigned types - widened both operands
      if E.ErrorCode = $800A11FD then // Integer can never match a Cardinal larger than High(Integer);
        Exit; // Succeed
      if E.ErrorCode = HResult($800A11FD) then
        Exit; // Succeed
      if Cardinal(E.ErrorCode) = $800A11FD then
        Exit; // Succeed
      // No warning, but both are passed as Int64, so comparison fails
      Self.CheckEquals(E.ErrorCode, $800A11FD, 'E.ErrorCode should equal $800A11FD');
    end; // E: EOleException
  end;
end;

procedure TestHResult.SetUp;
begin
end;

procedure TestHResult.TearDown;
begin
end;

initialization

RegisterTest(TestHResult.Suite);

end.
