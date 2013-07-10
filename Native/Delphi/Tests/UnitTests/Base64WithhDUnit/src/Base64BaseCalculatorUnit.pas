unit Base64BaseCalculatorUnit;

interface

uses
  Classes;

//##jpl: note that for Unicode you should perform some kind of Normalization (http://unicode.org/reports/tr15/)
// java SE6 includes a few of those (http://java.sun.com/developer/technicalArticles/javase/i18n_enhance/#normalization)
// MessageDigest_5 chooses to encode through UTF8, I'm not sure that is always a good thing to do.

type
  TBase64BaseCalculator = class(TObject)
  strict protected
    function CreateReadOnlyFileStream(const Filename: string): TFileStream; virtual;
  public
    procedure CalculateFile(const Filename:string; var Base64: string); overload; virtual;
    procedure CalculateString(const Buffer:string; var Base64: string); overload; virtual;
    procedure InverseCalculateFile(const Filename:string; var Base64: string); overload; virtual;
    procedure InverseCalculateString(const Buffer:string; var Base64: string); overload; virtual;
  end;

  TBase64Calculator = class(TBase64BaseCalculator)
  public
    procedure CalculateString(const Buffer:string; var Base64: string); overload; override;
    procedure InverseCalculateString(const Buffer:string; var Base64: string); overload; override;
  end;

  //1 Utility class to write out a bunch of files with various encodings of the Buffer
  /// This allows you to test what values the unix/Linux Base64 tool generates on the
  /// binary content.
  ///
  /// Note: Base64 runs on binary, not on strings, so the encoding of your string does
  /// matter!
  TBase64AndWriterCalculator = class(TBase64Calculator)
    procedure CalculateString(const Buffer:string; var Base64: string); overload; override;
  end;

  TIdHashMessageDigest5Calculator = class(TBase64BaseCalculator)
  public
    procedure CalculateFile(const Filename:string; var Base64: string); overload; override;
    procedure CalculateString(const Buffer:string; var Base64: string); overload; override;
  end;

const
  ChunkSize = 8192; // for streaming of files

implementation

uses
  SysUtils,
  Base64,
  MessageDigest_5,
  Variants,
  Types,
  IdHashMessageDigest;

procedure TBase64BaseCalculator.CalculateFile(const Filename:string; var Base64: string);
begin
  Base64 := NullAsStringValue;
end;

procedure TBase64BaseCalculator.CalculateString(const Buffer:string; var Base64: string);
begin
  Base64 := NullAsStringValue;
end;

function TBase64BaseCalculator.CreateReadOnlyFileStream(const Filename: string): TFileStream;
begin
  Result := TFileStream.Create(Filename, fmOpenRead or fmShareDenyWrite);
end;

procedure TBase64BaseCalculator.InverseCalculateFile(const Filename:string; var Base64: string);
begin
  Base64 := NullAsStringValue;
end;

procedure TBase64BaseCalculator.InverseCalculateString(const Buffer:string; var Base64: string);
begin
  Base64 := NullAsStringValue;
end;

procedure TBase64Calculator.CalculateString(const Buffer:string; var Base64: string);
begin
  Base64 := string(Base64Encode(Buffer));
end;

procedure TBase64Calculator.InverseCalculateString(const Buffer:string; var Base64: string);
begin
  Base64 := string(Base64Decode(Buffer));
end;

procedure TBase64AndWriterCalculator.CalculateString(const Buffer:string; var Base64: string);
var
  Strings: TStrings;
  procedure SaveStrings(const Encoding: TEncoding; const EncodingName: string);
  begin
    Strings.SaveToFile(Format('%s.%s.%s.text', [Buffer, Base64, EncodingName]), Encoding);
  end;
begin
  inherited CalculateString(Buffer, Base64);
  Strings := TStringList.Create();
  try
    Strings.Text := Buffer;
    SaveStrings(TEncoding.ASCII, 'ASCII');
    SaveStrings(TEncoding.BigEndianUnicode, 'BigEndianUnicode');
    SaveStrings(TEncoding.Default, 'Default');
    SaveStrings(TEncoding.Unicode, 'Unicode');
    SaveStrings(TEncoding.UTF7, 'UTF7');
    SaveStrings(TEncoding.UTF8, 'UTF8');
  finally
    Strings.Free;
  end;
end;

procedure TIdHashMessageDigest5Calculator.CalculateFile(const Filename:string; var Base64: string);
var
  IdHashMessageDigest5 : TIdHashMessageDigest5;
  FileStream : TFileStream;
begin
  IdHashMessageDigest5 := TIdHashMessageDigest5.Create;
  FileStream := CreateReadOnlyFileStream(Filename);
  try
    Base64 := IdHashMessageDigest5.HashStreamAsHex(FileStream);
  finally
    FileStream.Free;
    IdHashMessageDigest5.Free;
  end;
end;

procedure TIdHashMessageDigest5Calculator.CalculateString(const Buffer:string; var Base64: string);
var
  IdHashMessageDigest5 : TIdHashMessageDigest5;
begin
  IdHashMessageDigest5 := TIdHashMessageDigest5.Create;
  try
    Base64 := IdHashMessageDigest5.HashStringAsHex(Buffer);
  finally
    IdHashMessageDigest5.Free;
  end;
end;

end.
