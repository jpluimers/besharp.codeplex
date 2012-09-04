unit PacketWrapperUnit;

interface

uses
  VariantRecordUnit;

type

  TPacketBase = class(TObject)
  strict private
    FPacket: TPacket;
  strict protected
    procedure InitializeFPacket; virtual;
  public
    procedure InitializeFPacketData1; virtual;
    procedure InitializeFPacketData2; virtual;
    procedure InitializeFPacketData3; virtual;
    property Packet: TPacket read FPacket write FPacket;
  end;

  TPacketWrapper = class(TPacketBase)
  strict private
    procedure InitializePacket;
  public
    constructor Create;
    procedure InitializePacketData1; virtual;
    procedure InitializePacketData2; virtual;
    procedure InitializePacketData3; virtual;
  end;

implementation

procedure TPacketBase.InitializeFPacket;
begin
  FillChar(FPacket, SizeOf(FPacket), MarkerChar);
end;

procedure TPacketBase.InitializeFPacketData1;
begin
  InitializeFPacket();
  with FPacket do
    FillChar(Data.Contents, SizeOf(Data.Contents), SpaceChar);
  with FPacket do
    FillChar(Data.Zero, SizeOf(Data.Zero), NullChar);
end;

procedure TPacketBase.InitializeFPacketData2;
begin
  InitializeFPacket();
  with FPacket.Data do
    FillChar(Contents, SizeOf(Contents), SpaceChar);
  with FPacket.Data do
    FillChar(Zero, SizeOf(Zero), NullChar);
end;

procedure TPacketBase.InitializeFPacketData3;
begin
  InitializeFPacket();
  FillChar(FPacket.Data.Contents, SizeOf(FPacket.Data.Contents), SpaceChar);
  FillChar(FPacket.Data.Zero, SizeOf(FPacket.Data.Zero), NullChar);
end;


constructor TPacketWrapper.Create;
begin
  inherited;
  InitializeFPacket();
end;

procedure TPacketWrapper.InitializePacket;
begin
//[Pascal Error] PacketWrapperUnit.pas(74): E2197 Constant object cannot be passed as var parameter
//  FillChar(Packet, $AA, SizeOf(Packet));
  InitializeFPacket();
end;

procedure TPacketWrapper.InitializePacketData1;
begin
  InitializePacket();
{$if CompilerVersion >= 20}
  Packet.InitializeData(); // fix the E2197 tightening introduced in Delphi 2009
{$else}
  with Packet do
    FillChar(Data.Contents, SizeOf(Data.Contents), SpaceChar);
  with Packet do
    FillChar(Data.Zero, SizeOf(Data.Zero), NullChar);
{$ifend CompilerVersion >= 20}
end;

procedure TPacketWrapper.InitializePacketData2;
begin
  InitializePacket();
{$if CompilerVersion >= 20}
  Packet.InitializeData(); // fix the E2197 tightening introduced in Delphi 2009
{$else}
  with Packet.Data do
    FillChar(Contents, SizeOf(Contents), SpaceChar);
  with Packet.Data do
    FillChar(Zero, SizeOf(Zero), NullChar);
{$ifend CompilerVersion >= 20}
end;

procedure TPacketWrapper.InitializePacketData3;
begin
  InitializePacket();
{$if CompilerVersion >= 17}
  Packet.InitializeData();
{$else}
//[Pascal Error] PacketWrapperUnit.pas(104): E2197 Constant object cannot be passed as var parameter
//[Pascal Error] PacketWrapperUnit.pas(105): E2197 Constant object cannot be passed as var parameter
  FillChar(Packet.Data.Contents, SizeOf(Packet.Data.Contents), SpaceChar);
  FillChar(Packet.Data.Zero, SizeOf(Packet.Data.Zero), NullChar);
{$ifend CompilerVersion >= 17}
end;

end.
