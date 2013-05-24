unit TestSysUtilsFormatUnit;

interface

uses
  TestFramework, System.SysUtils;

type
  TestSysUtilsFormat = class(TTestCase)
  strict private
    DoublePi: Double;
    CurrencyPi: Currency;
    FloatFormat: string;
    NumericFormat: string;
    Expected_Format_F: string;
    Expected_Format_N: string;
  public
    procedure SetUp; override;
    procedure TearDown; override;
  published
    procedure Test_Format_F_Double;
    procedure Test_Format_F_Currency;
    procedure Test_Format_N_Double;
    procedure Test_Format_N_Currency;
  end;

implementation

procedure TestSysUtilsFormat.Test_Format_F_Double;
var
  ReturnValue: string;
begin
  ReturnValue := System.SysUtils.Format(FloatFormat, [DoublePi]);
  Self.CheckEqualsString(Expected_Format_F, ReturnValue); // actual '3.1'
end;

procedure TestSysUtilsFormat.Test_Format_F_Currency;
var
  ReturnValue: string;
begin
  ReturnValue := System.SysUtils.Format(FloatFormat, [CurrencyPi]);
  Self.CheckEqualsString(Expected_Format_F, ReturnValue); // actual '3.142'
end;

procedure TestSysUtilsFormat.Test_Format_N_Double;
var
  ReturnValue: string;
begin
  ReturnValue := System.SysUtils.Format(NumericFormat, [DoublePi]);
  Self.CheckEqualsString(Expected_Format_N, ReturnValue); // actual '   3'
end;

procedure TestSysUtilsFormat.Test_Format_N_Currency;
var
  ReturnValue: string;
begin
  ReturnValue := System.SysUtils.Format(NumericFormat, [CurrencyPi]);
  Self.CheckEqualsString(Expected_Format_N, ReturnValue); // actual '3.142'
end;

procedure TestSysUtilsFormat.SetUp;
begin
  DoublePi := 3.1415;
  CurrencyPi := 3.1415;
  FloatFormat := '%.1f';
  Expected_Format_F := '3.1';
  NumericFormat := '%4.0n';
  Expected_Format_N := '   3';
end;

procedure TestSysUtilsFormat.TearDown;
begin
end;

initialization
  RegisterTest(TestSysUtilsFormat.Suite);
end.
