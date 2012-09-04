unit VariantRecordUnit;

interface

{ First a few basic types}

const
  TGuidStringSize = 38;

type
  TChar = AnsiChar; { single byte character, as it interfaces with DOS and CoolGen programs through C interface }
  TChar2    = array[0..   1] of TChar;
  TChar8    = array[0..   7] of TChar;
  TChar10   = array[0..   9] of TChar;
  TChar20   = array[0..  19] of TChar;
  T1Char33 = array[1..33] of TChar; { 1-based because the DOS CAS sources expect this }
  TGuidChar   = array[0..TGuidStringSize-1] of TChar;
  TMessageId = array[0..23] of Byte;

{ now the record types }

type
  TVariantData = record
  case Boolean of
    False: (
      ProgramName: TChar10;
      InterChangeFormat: TChar10;
      FunctionCode: TChar2;
      ReturnCode: TChar2;
      ErrorCode: TChar2;
      Zero: TChar2);
    True: (Contents: T1Char33);
  end; { total: 33 bytes }

  TId = packed record
    NetBiosName: TChar20; { historically, as DOS app defined it wrongly }
    TimeStamp: TChar8; { HHMMSShh because a DOS directory name can be no longer than 8 characters }
  end; { total: 28 bytes }

  TVariantKey = packed record
  case Integer of
    0: ( // SNA
      ConversationId: TId; { 28 bytes }
      GuidChars: TGuidChar); { 38 bytes }
    2: ( // MQ
      ConversationIdFiller: TId;
      MessageId: TMessageID); // 24 bytes
  end; { total: 66 bytes }

  TPacket = packed record
    EntryType : Byte;
    ReturnKey : TVariantKey;
    DataType  : Byte;
    Data      : TVariantData;
{$if CompilerVersion >= 17}
  public
    procedure InitializeData;
{$ifend CompilerVersion >= 17}
  end;

const
  MarkerChar = #$AA;
  NullChar = #$00;
  SpaceChar = #$20;

implementation

{$if CompilerVersion >= 17}
procedure TPacket.InitializeData;
begin
  FillChar(Data.Contents, SizeOf(Data.Contents), SpaceChar);
  FillChar(Data.Zero, SizeOf(Data.Zero), NullChar);
end;
{$ifend CompilerVersion >= 17}

end.
