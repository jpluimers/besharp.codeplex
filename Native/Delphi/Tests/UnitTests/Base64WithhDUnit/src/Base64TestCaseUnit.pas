unit Base64TestCaseUnit;

//##jpl: test cases from https://github.com/couchbase/libcouchbase/blob/master/tests/base64-unit-test.cc
// which are based on http://www.ietf.org/rfc/rfc4648.txt
// and on http://en.wikipedia.org/wiki/Base64

interface

uses
  Classes, SysUtils, TestFrameWork, Base64BaseCalculatorUnit;

type
  //##jpl: you need to add the "constructor" constraint, otherwise you cannot call the Create constructor.
  TBase64BaseTestCase<T: TBase64BaseCalculator, constructor> = class(TTestCase)
  strict protected
    Base64BaseCalculator: TBase64BaseCalculator;
    procedure Calculate(const Value:string; var Base64: string); overload; virtual;
    procedure Test(const ExpectedBase64, Value: string); overload; virtual;
    procedure VerifyTestResult(const ExpectedBase64, Base64, Description: string); virtual;
  protected
    procedure SetUp; override;
    procedure TearDown; override;
  end;

  TBase64FileTestCase<T: TBase64BaseCalculator, constructor> = class(TBase64BaseTestCase<T>)
  strict protected
    procedure Calculate(const Filename:string; var Base64: string); overload; override;
    procedure Test(const RelativeFileName: string); overload; virtual;
  published
    procedure Test_EmptyFileName; overload;
    procedure Test_DamnSmallLinuxBootFloppy; overload;
  end;

  TBase64StringTestCase<T: TBase64BaseCalculator, constructor> = class(TBase64BaseTestCase<T>)
  strict protected
    procedure Calculate(const Buffer:string; var Base64: string); overload; override;
  published
    procedure Test_; overload;
    procedure Test_f; overload;
    procedure Test_fo; overload;
    procedure Test_foo; overload;
    procedure Test_foob; overload;
    procedure Test_fooba; overload;
    procedure Test_foobar; overload;

    procedure Test_Administrator_colon_password;
    procedure Test_at;
    procedure Test_at_at;
    procedure Test_at_at_at;
    procedure Test_at_at_at_at;
    procedure Test_at_at_at_at_UnixNewLine;
    procedure Test_at_at_at_UnixNewLine;
    procedure Test_at_at_UnixNewLine;
    procedure Test_at_UnixNewLine;
    procedure Test_blahblah_colon_bla_at_at_h_;
    procedure Test_blahblah_bla_at_at_h;

  end;

//##jpl: [DCC Error] Base64TestCaseUnit.pas(109): E2506 Method of parameterized type declared in interface section must not use local symbol 'SExpectedHash_d41d8cd98f00b204e9800998ecf8427e_'
const
{  RFC 4648:
   BASE64("") = ""
   BASE64("f") = "Zg=="
   BASE64("fo") = "Zm8="
   BASE64("foo") = "Zm9v"
   BASE64("foob") = "Zm9vYg=="
   BASE64("fooba") = "Zm9vYmE="
   BASE64("foobar") = "Zm9vYmFy"

    /* Examples from http://en.wikipedia.org/wiki/Base64 */
    validate("Man is distinguished, not only by his reason, but by this singular "
             "passion from other animals, which is a lust of the mind, that by a "
             "perseverance of delight in the continued and indefatigable generation"
             " of knowledge, exceeds the short vehemence of any carnal pleasure.",
             "TWFuIGlzIGRpc3Rpbmd1aXNoZWQsIG5vdCBvbmx5IGJ5IGhpcyByZWFzb24sIGJ1dCBieSB0aGlz"
             "IHNpbmd1bGFyIHBhc3Npb24gZnJvbSBvdGhlciBhbmltYWxzLCB3aGljaCBpcyBhIGx1c3Qgb2Yg"
             "dGhlIG1pbmQsIHRoYXQgYnkgYSBwZXJzZXZlcmFuY2Ugb2YgZGVsaWdodCBpbiB0aGUgY29udGlu"
             "dWVkIGFuZCBpbmRlZmF0aWdhYmxlIGdlbmVyYXRpb24gb2Yga25vd2xlZGdlLCBleGNlZWRzIHRo"
             "ZSBzaG9ydCB2ZWhlbWVuY2Ugb2YgYW55IGNhcm5hbCBwbGVhc3VyZS4=");
    validate("pleasure.", "cGxlYXN1cmUu");
    validate("leasure.", "bGVhc3VyZS4=");
    validate("easure.", "ZWFzdXJlLg==");
    validate("asure.", "YXN1cmUu");
    validate("sure.", "c3VyZS4=");

    // Dummy test data. It looks like the "base64" command line
    // utility from gnu coreutils adds the "\n" to the encoded data...
    validate("Administrator:password", "QWRtaW5pc3RyYXRvcjpwYXNzd29yZA==");
    validate("@", "QA==");
    validate("@\n", "QAo=");
    validate("@@", "QEA=");
    validate("@@\n", "QEAK");
    validate("@@@", "QEBA");
    validate("@@@\n", "QEBACg==");
    validate("@@@@", "QEBAQA==");
    validate("@@@@\n", "QEBAQAo=");
    validate("blahblah:bla@@h", "YmxhaGJsYWg6YmxhQEBo");
    validate("blahblah:bla@@h\n", "YmxhaGJsYWg6YmxhQEBoCg==");
}
  S_ = '';
  S_Base64_ = '';

  S_f = 'f';
  S_Base64_f = 'Zg==';
  S_fo = 'fo';
  S_Base64_fo = 'Zm8=';
  S_foo = 'foo';
  S_Base64_foo = 'Zm9v';
  S_foob = 'foob';
  S_Base64_foob = 'Zm9vYg==';
  S_fooba = 'fooba';
  S_Base64_fooba = 'Zm9vYmE=';
  S_foobar = 'foobar';
  S_Base64_foobar = 'Zm9vYmFy';

//    validate("Man is distinguished, not only by his reason, but by this singular "
//             "passion from other animals, which is a lust of the mind, that by a "
//             "perseverance of delight in the continued and indefatigable generation"
//             " of knowledge, exceeds the short vehemence of any carnal pleasure.",
//             "TWFuIGlzIGRpc3Rpbmd1aXNoZWQsIG5vdCBvbmx5IGJ5IGhpcyByZWFzb24sIGJ1dCBieSB0aGlz"
//             "IHNpbmd1bGFyIHBhc3Npb24gZnJvbSBvdGhlciBhbmltYWxzLCB3aGljaCBpcyBhIGx1c3Qgb2Yg"
//             "dGhlIG1pbmQsIHRoYXQgYnkgYSBwZXJzZXZlcmFuY2Ugb2YgZGVsaWdodCBpbiB0aGUgY29udGlu"
//             "dWVkIGFuZCBpbmRlZmF0aWdhYmxlIGdlbmVyYXRpb24gb2Yga25vd2xlZGdlLCBleGNlZWRzIHRo"
//             "ZSBzaG9ydCB2ZWhlbWVuY2Ugb2YgYW55IGNhcm5hbCBwbGVhc3VyZS4=");
  S_pleasure_ = 'pleasure.';
  S_Base64_pleasure_ = 'cGxlYXN1cmUu';
  S_leasure_ = 'leasure.';
  S_Base64_leasure_ = 'bGVhc3VyZS4=';
  S_easure_ = 'easure.';
  S_Base64_easure_ = 'ZWFzdXJlLg==';
  S_asure_ = 'asure.';
  S_Base64_asure_ = 'YXN1cmUu';
  S_sure_ = 'sure.';
  S_Base64_sure_ = 'c3VyZS4=';

  S_Administrator_colon_password = 'Administrator:password';
  S_Base64_Administrator_colon_password = 'QWRtaW5pc3RyYXRvcjpwYXNzd29yZA==';

  UnixNewLine = #10;

  S_at = '@';
  S_Base64_at = 'QA==';
  S_at_UnixNewLine = '@'+UnixNewLine;
  S_Base64_at_UnixNewLine = 'QAo=';
  S_at_at = '@@';
  S_Base64_at_at = 'QEA=';
  S_at_at_UnixNewLine = '@@'+UnixNewLine;
  S_Base64_at_at_UnixNewLine = 'QEAK';
  S_at_at_at = '@@@';
  S_Base64_at_at_at = 'QEBA';
  S_at_at_at_UnixNewLine = '@@@'+UnixNewLine;
  S_Base64_at_at_at_UnixNewLine = 'QEBACg==';
  S_at_at_at_at = '@@@@';
  S_Base64_at_at_at_at = 'QEBAQA==';
  S_at_at_at_at_UnixNewLine = '@@@@'+UnixNewLine;
  S_Base64_at_at_at_at_UnixNewLine = 'QEBAQAo=';
  S_blahblah_bla_at_at_h = 'blahblah:bla@@h';
  S_Base64_blahblah_bla_at_at_h = 'YmxhaGJsYWg6YmxhQEBo';
  S_blahblah_colon_bla_at_at_h_ = 'blahblah:bla@@h'+UnixNewLine;
  S_Base64_blahblah_colon_bla_at_at_h_ = 'YmxhaGJsYWg6YmxhQEBoCg==';

implementation

uses
  Base64, Variants;


procedure TBase64BaseTestCase<T>.SetUp;
begin
  Base64BaseCalculator := T.Create();
end;

procedure TBase64BaseTestCase<T>.TearDown;
begin
  Base64BaseCalculator.Free;
end;

procedure TBase64BaseTestCase<T>.Calculate(const Value:string; var Base64: string);
begin

end;

procedure TBase64BaseTestCase<T>.Test(const ExpectedBase64, Value: string);
var
  Base64: string;
begin
  Calculate(Value, Base64);
  VerifyTestResult(ExpectedBase64, Base64, Value);
end;

procedure TBase64BaseTestCase<T>.VerifyTestResult(const ExpectedBase64, Base64, Description: string);
begin
  Self.CheckEquals(ExpectedBase64, Base64, Format('while calculating Base64 for "%s"', [Description]));
end;


procedure TBase64FileTestCase<T>.Calculate(const Filename:string; var Base64: string);
begin
  Base64BaseCalculator.CalculateFile(Filename, Base64);
end;

procedure TBase64FileTestCase<T>.Test(const RelativeFileName: string);
var
  ExpectedBase64: string;
  BinaryFileName: string;
  Base64FileName: string;
begin
  if RelativeFileName <> NullAsStringValue then
  begin
    // expect these files to exist:
    // ..\..\..\Base64Data\RelativeFileName
    // ..\..\..\Base64Data\RelativeFileName.Base64.txt
    BinaryFileName := Format('..\..\..\Base64Data\%s', [RelativeFileName]);
    Base64FileName := Format('%s.Base64.txt', [BinaryFileName]);
    with TStreamReader.Create(Base64FileName) do
    try
      ExpectedBase64 := LowerCase(ReadToEnd());
      ExpectedBase64 := Copy(ExpectedBase64, 1, 32);
    finally
      Free;
    end;
    Test(ExpectedBase64, BinaryFileName);
  end;
end;

procedure TBase64FileTestCase<T>.Test_EmptyFileName;
begin
  Test(S_);
end;

procedure TBase64FileTestCase<T>.Test_DamnSmallLinuxBootFloppy;
begin
  Test('damn-small-linux.bootfloppy.img');
end;

procedure TBase64StringTestCase<T>.Calculate(const Buffer:string; var Base64: string);
begin
  Base64BaseCalculator.CalculateString(Buffer, Base64);
end;

procedure TBase64StringTestCase<T>.Test_;
begin
  Test(S_Base64_, S_);
end;

procedure TBase64StringTestCase<T>.Test_f;
begin
  Test(S_Base64_f, S_f);
end;

procedure TBase64StringTestCase<T>.Test_fo;
begin
  Test(S_Base64_fo, S_fo);
end;

procedure TBase64StringTestCase<T>.Test_foo;
begin
  Test(S_Base64_foo, S_foo);
end;

procedure TBase64StringTestCase<T>.Test_foob;
begin
  Test(S_Base64_foob, S_foob);
end;

procedure TBase64StringTestCase<T>.Test_fooba;
begin
  Test(S_Base64_fooba, S_fooba);
end;

procedure TBase64StringTestCase<T>.Test_foobar;
begin
  Test(S_Base64_foobar, S_foobar);
end;

procedure TBase64StringTestCase<T>.Test_Administrator_colon_password;
begin
  Test(S_Base64_Administrator_colon_password, S_Administrator_colon_password);
end;

procedure TBase64StringTestCase<T>.Test_at;
begin
  Test(S_Base64_at, S_at);
end;

procedure TBase64StringTestCase<T>.Test_at_UnixNewLine;
begin
  Test(S_Base64_at_UnixNewLine, S_at_UnixNewLine);
end;

procedure TBase64StringTestCase<T>.Test_at_at;
begin
  Test(S_Base64_at_at, S_at_at);
end;

procedure TBase64StringTestCase<T>.Test_at_at_UnixNewLine;
begin
  Test(S_Base64_at_at_UnixNewLine, S_at_at_UnixNewLine);
end;

procedure TBase64StringTestCase<T>.Test_at_at_at;
begin
  Test(S_Base64_at_at_at, S_at_at_at);
end;

procedure TBase64StringTestCase<T>.Test_at_at_at_UnixNewLine;
begin
  Test(S_Base64_at_at_at_UnixNewLine, S_at_at_at_UnixNewLine);
end;

procedure TBase64StringTestCase<T>.Test_at_at_at_at;
begin
  Test(S_Base64_at_at_at_at, S_at_at_at_at);
end;

procedure TBase64StringTestCase<T>.Test_at_at_at_at_UnixNewLine;
begin
  Test(S_Base64_at_at_at_at_UnixNewLine, S_at_at_at_at_UnixNewLine);
end;

procedure TBase64StringTestCase<T>.Test_blahblah_bla_at_at_h;
begin
  Test(S_Base64_blahblah_bla_at_at_h, S_blahblah_bla_at_at_h);
end;

procedure TBase64StringTestCase<T>.Test_blahblah_colon_bla_at_at_h_;
begin
  Test(S_Base64_blahblah_colon_bla_at_at_h_, S_blahblah_colon_bla_at_at_h_);
end;


initialization
  // should fail:
//  RegisterTest('', TBase64StringTestCase<TBase64BaseCalculator>.Suite);
  // should succeed:
  RegisterTest('', TBase64StringTestCase<TBase64Calculator>.Suite);
  RegisterTest('', TBase64StringTestCase<TIdHashMessageDigest5Calculator>.Suite);
//  RegisterTest('', TBase64StringTestCase<TBase64AndWriterCalculator>.Suite);
{$ifdef Base64FileTests}
  RegisterTest('', TBase64FileTestCase<TBase64Calculator>.Suite);
  RegisterTest('', TBase64FileTestCase<TIdHashMessageDigest5Calculator>.Suite);
{$endif Base64FileTests}
end.
