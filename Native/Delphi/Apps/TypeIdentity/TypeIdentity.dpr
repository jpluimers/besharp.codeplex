program TypeIdentity;

{$APPTYPE CONSOLE}

{$R *.res}

uses
  SysUtils;

type
  Range20 = 0..19;
  Range48 = 0..47;
  Range264 = 0..263;
  SingleByteChar = AnsiChar; // Single-Byte Character; WebSphereMQ does not support Unicode
  MQCHAR = SingleByteChar;
  MQCHAR20 = array[Range20] of MQCHAR;
  MQCHAR48 = array[Range48] of MQCHAR;
  MQCHAR264 = array[Range264] of MQCHAR;

  TMQConnectionName = type MQCHAR264;
  TMQChannelName = type MQCHAR20;
  TMQQueueManagerName = type MQCHAR48;
  TMQQueueName = type MQCHAR48;

const
  SYSTEM_DEFAULT_MODEL_QUEUE = 'SYSTEM.DEFAULT.MODEL.QUEUE'; // Default model queue.

  SYSTEM_DEFAULT_MODEL_QUEUE_Name: TMQQueueName = ('S','Y','S','T','E','M','.','D','E','F','A','U','L','T','.','M','O','D','E','L','.','Q','U','E','U','E',
    ' ',' ', // http://publib.boulder.ibm.com/infocenter/wmqv7/v7r0/index.jsp?topic=%2Fcom.ibm.mq.csqzaj.doc%2Fsc10300_.htm
    ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',
    ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ');

function PadRight(const Value: AnsiString; const ResultLength: Integer): AnsiString;
var
  Index: Integer;
begin
  Result := Value;
  SetLength(Result, ResultLength);
  for Index := Length(Value)+1 to ResultLength do
    Result[Index] := ' ';
end;

function ToMQQueueName(const Value: AnsiString): TMQQueueName;
var
  PaddedValue: AnsiString;
  ResultSize: Integer;
begin
  ResultSize := SizeOf(Result);
  PaddedValue := PadRight(Value, ResultSize);
  Move(PaddedValue[1], Result, ResultSize);
end;

var
  S: AnsiString;
  QueueName: TMQQueueName;
  QueueManagerName: TMQQueueManagerName;
  QueueName48: MQCHAR48;

begin
  try
    S := SYSTEM_DEFAULT_MODEL_QUEUE;
    S := SYSTEM_DEFAULT_MODEL_QUEUE_Name;

    QueueName := SYSTEM_DEFAULT_MODEL_QUEUE;
    QueueName := SYSTEM_DEFAULT_MODEL_QUEUE_Name;

    QueueManagerName := SYSTEM_DEFAULT_MODEL_QUEUE;
    QueueManagerName := SYSTEM_DEFAULT_MODEL_QUEUE_Name; // 68

    QueueName48 := SYSTEM_DEFAULT_MODEL_QUEUE;
    QueueName48 := SYSTEM_DEFAULT_MODEL_QUEUE_Name; // 71

    QueueManagerName := QueueName;   // 73
    QueueName := QueueManagerName;   // 74

    QueueName48 := QueueManagerName; // 76
    QueueManagerName := QueueName48; // 77

    S := QueueName;
    S := QueueManagerName;
    S := QueueName48;

    QueueName := ToMQQueueName(S);

    QueueName := S;         // 85
    QueueManagerName := S;  // 86
    QueueName48 := S;       // 87
  except
    on E: Exception do
      Writeln(E.ClassName, ': ', E.Message);
  end;
end.

[DCC Error] TypeIdentity.dpr(68): E2010 Incompatible types: 'TMQQueueManagerName' and 'TMQQueueName'
[DCC Error] TypeIdentity.dpr(71): E2010 Incompatible types: 'MQCHAR48' and 'TMQQueueName'
[DCC Error] TypeIdentity.dpr(73): E2010 Incompatible types: 'TMQQueueManagerName' and 'TMQQueueName'
[DCC Error] TypeIdentity.dpr(74): E2010 Incompatible types: 'TMQQueueName' and 'TMQQueueManagerName'
[DCC Error] TypeIdentity.dpr(76): E2010 Incompatible types: 'MQCHAR48' and 'TMQQueueManagerName'
[DCC Error] TypeIdentity.dpr(77): E2010 Incompatible types: 'TMQQueueManagerName' and 'MQCHAR48'
[DCC Error] TypeIdentity.dpr(85): E2010 Incompatible types: 'TMQQueueName' and 'AnsiString'
[DCC Error] TypeIdentity.dpr(86): E2010 Incompatible types: 'TMQQueueManagerName' and 'AnsiString'
[DCC Error] TypeIdentity.dpr(87): E2010 Incompatible types: 'MQCHAR48' and 'AnsiString'
