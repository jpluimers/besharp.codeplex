unit Base64;

interface

function Base64Decode(const Text: string): string;
function Base64Encode(const Text: string): string;

implementation

uses
  IdCoderMIME;

function Base64Decode(const Text: string): string;
var
  Decoder : TIdDecoderMime;
begin
  Decoder := TIdDecoderMime.Create(nil);
  try
    Result := Decoder.DecodeString(Text);
  finally
    Decoder.Free();
  end;
end;

function Base64Encode(const Text: string): string;
var
  Encoder : TIdEncoderMime;
begin
  Encoder := TIdEncoderMime.Create(nil);
  try
    Result := Encoder.EncodeString(Text);
  finally
    Encoder.Free();
  end;
end;

end.
