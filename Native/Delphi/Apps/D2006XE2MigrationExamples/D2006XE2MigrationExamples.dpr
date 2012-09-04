program D2006XE2MigrationExamples;

{$APPTYPE CONSOLE}

uses
  SysUtils,
  VariantRecordUnit in 'VariantRecordUnit.pas',
  PacketWrapperUnit in 'PacketWrapperUnit.pas',
  Classes;

procedure Test(const Context: string; const PacketWrapper: TPacketWrapper; const Method: TThreadMethod);
var
  Character: TChar;
begin
  Writeln(Context);
  Method();
  for Character in PacketWrapper.Packet.Data.Contents do
  begin
    Writeln(Character, Byte(Character));
  end;
end;

var
  PacketWrapper: TPacketWrapper;
begin
  PacketWrapper := TPacketWrapper.Create();
  try
    Test('InitializeFPacketData1', PacketWrapper, PacketWrapper.InitializeFPacketData1);
    Test('InitializeFPacketData2', PacketWrapper, PacketWrapper.InitializeFPacketData2);
    Test('InitializeFPacketData3', PacketWrapper, PacketWrapper.InitializeFPacketData3);
    Test('InitializePacketData1', PacketWrapper, PacketWrapper.InitializePacketData1);
    Test('InitializePacketData2', PacketWrapper, PacketWrapper.InitializePacketData2);
    Test('InitializePacketData3', PacketWrapper, PacketWrapper.InitializePacketData3);
  finally
    PacketWrapper.Free;
  end;
end.
