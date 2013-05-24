{ Copyright (c) 2007-2013 Jeroen Wiert Pluimers for BeSharp.net and better office benelux.
Full BSD License is available at http://besharp.codeplex.com/license and http://bo.codeplex.com/license }

unit DescriptionLoggerUnit;

interface

uses
  BaseLoggerUnit,
  LoggerInterfaceUnit,
  System.TypInfo;

type
  TDescriptionLogger = class(TBaseLogger, IDescriptionLogger)
  protected
    procedure Log(const Description: string; const Item: Boolean); overload; virtual;
    procedure Log(const Description: string; const Item: Integer); overload; virtual;
    procedure Log(const Description: string; const Item: Pointer); overload; virtual;
    procedure Log(const Description: string; const Item: string); overload; virtual;
    procedure Log(const Description: string; const TypeTypeInfo: PTypeInfo; const Value: Integer); overload; virtual;
    procedure Log(const Description: string; const TypeTypeInfo: PTypeInfo; const Prefix: string = ''); overload; virtual;
    procedure Log(const Description: string; const Item: ShortStringBase); overload; virtual;
  end;

implementation

uses
  System.SysUtils,
  EnumerationTypeInformationUnit,
  RecordTypeInformationUnit,
  SetTypeInformationUnit,
  GlobalLoggerSettingsUnit,
  SafeFormatUnit;

procedure TDescriptionLogger.Log(const Description: string; const Item: Boolean);
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  Log(Description, BoolToStr(Item));
end;

procedure TDescriptionLogger.Log(const Description: string; const Item: Integer);
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  Log(Description, IntToStr(Item));
end;

procedure TDescriptionLogger.Log(const Description: string; const Item: Pointer);
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  Log(Description, PointerToString(Item));
end;

procedure TDescriptionLogger.Log(const Description: string; const Item: string);
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  Log('%s:%s', [Description, Item]);
end;

procedure TDescriptionLogger.Log(const Description: string; const TypeTypeInfo: PTypeInfo; const Value: Integer);
var
  Item: string;
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  Item := GetEnumerationNameAndOrdValue(TypeTypeInfo, Value);
  Log(Description, Item);
end;

procedure TDescriptionLogger.Log(const Description: string; const TypeTypeInfo: PTypeInfo; const Prefix: string = '');
var
  TypeTypeData: PTypeData;
  TTypeKindTypeInfo: PTypeInfo;
  TOrdTypeTypeInfo: PTypeInfo;
  TFloatTypeTypeInfo: PTypeInfo;
  TMethodKindTypeInfo: PTypeInfo;
  TIntfFlagsBaseTypeInfo: PTypeInfo;
  IntfFlagsString: string;
  RecordFieldTable: PFieldTable;
  FieldIndex: Integer;
  RecordFieldPrefix: string;
  NewPrefix: string;
  RecordFieldTableField: TFieldInfo;
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  if not Assigned(TypeTypeInfo) then
    Exit;
  Log('%s: TypeInfo for type %s', [Description, TypeTypeInfo.Name]);

  TTypeKindTypeInfo := TypeInfo(TTypeKind);
  NewPrefix := Prefix + '  ';
  Log(NewPrefix + 'TypeInfo.Kind', TTypeKindTypeInfo, Ord(TypeTypeInfo.Kind));

  // uses IntfInfo ;
  // uses TypInfo ;
  TypeTypeData := GetTypeData(TypeTypeInfo);
  case TypeTypeInfo.Kind of
    tkUnknown, tkLString, tkWString, tkVariant: ;
    tkInteger, tkChar, tkEnumeration, tkSet, tkWChar:
      begin
        TOrdTypeTypeInfo := TypeInfo(TOrdType);
        Log(NewPrefix + 'OrdType', TOrdTypeTypeInfo, Ord(TypeTypeData.OrdType));
        case TypeTypeInfo.Kind of
          tkInteger, tkChar, tkEnumeration, tkWChar:
            begin
              Log(NewPrefix + 'MinValue', TypeTypeData.MinValue);
              Log(NewPrefix + 'MaxValue', TypeTypeData.MaxValue);
              case TypeTypeInfo.Kind of
                tkInteger, tkChar, tkWChar: ;
                tkEnumeration:
                  begin
                    if Assigned(TypeTypeData.BaseType) then
                      Log(NewPrefix + 'BaseType', TypeTypeData.BaseType^, NewPrefix);
                    Log(NewPrefix + 'NameList', GetEnumerationCsvNameList(TypeTypeInfo));
                    Log(NewPrefix + 'EnumUnitName', GetEnumUnitName(TypeTypeInfo));
                  end;
              end;
            end;
          tkSet:
            begin
              if Assigned(TypeTypeData.CompType) then
                Log(Prefix + 'CompType', TypeTypeData.CompType^, NewPrefix);
            end;
        end;
      end;
    tkFloat:
      begin
        TFloatTypeTypeInfo := TypeInfo(TFloatType);
        Log(NewPrefix + 'FloatType', TFloatTypeTypeInfo, Ord(TypeTypeData.FloatType));
      end;
    tkString:
      begin
        Log(NewPrefix + 'MaxLength', TypeTypeData.MaxLength);
      end;
    tkClass:
      begin
        Log(NewPrefix + 'ClassType', TypeTypeData.ClassType.ClassName);
        if Assigned(TypeTypeData.ParentInfo) then
          Log(NewPrefix + 'ParentInfo', TypeTypeData.ParentInfo^, NewPrefix);
        Log(NewPrefix + 'PropCount', TypeTypeData.PropCount);
        Log(NewPrefix + 'UnitName', TypeTypeData.UnitName);
        {PropData: TPropData};
      end;
    tkMethod:
      begin
        TMethodKindTypeInfo := typeInfo(TMethodKind);
        Log(NewPrefix + 'MethodKind', TMethodKindTypeInfo, Ord(TypeTypeData.MethodKind));
        Log(NewPrefix + 'ParamCount', ParamCount);
        // ParamList: array[0..1023] of Char
        {ParamList: array[1..ParamCount] of
           record
             Flags: TParamFlags;
             ParamName: ShortString;
             TypeName: ShortString;
           end;
         ResultType: ShortString};
      end;
    tkInterface:
      begin
        if Assigned(TypeTypeData.IntfParent) then
          Log(NewPrefix + 'IntfParent', TypeTypeData.IntfParent^, NewPrefix); { ancestor }
        TIntfFlagsBaseTypeInfo := TypeInfo(TIntfFlagsBase);
        IntfFlagsString := SetToString(TIntfFlagsBaseTypeInfo, TypeTypeData.IntfFlags, True);
        Log(NewPrefix + 'IntfFlags', IntfFlagsString);
        Log(NewPrefix + 'Guid', GUIDToString(TypeTypeData.Guid));
        Log(NewPrefix + 'IntfUnit', TypeTypeData.IntfUnit);
        {PropData: TPropData};
      end;
    tkInt64:
      begin
        Log(NewPrefix + 'MinInt64Value', TypeTypeData.MinInt64Value);
        Log(NewPrefix + 'MaxInt64Value', TypeTypeData.MaxInt64Value);
      end;
    tkDynArray:
      begin
        Log(NewPrefix + 'elSize', TypeTypeData.elSize);
        if Assigned(TypeTypeData.elType) then
          Log(NewPrefix + 'elType', TypeTypeData.elType^, NewPrefix); // nil if type does not require cleanup
        Log(NewPrefix + 'varType', TypeTypeData.varType); // Ole Automation varType equivalent
        if Assigned(TypeTypeData.elType2) then
          Log(NewPrefix + 'elType2', TypeTypeData.elType2^, NewPrefix); // independent of cleanup
        Log(NewPrefix + 'DynUnitName', TypeTypeData.DynUnitName);
      end;
    tkRecord:
      begin //jpl: 20080908
        RecordFieldTable := GetFieldTable(TypeTypeInfo);
        Log(NewPrefix + 'RecordFieldTable.X', RecordFieldTable.X);
        Log(NewPrefix + 'RecordFieldTable.Size', RecordFieldTable.Size);
        Log(NewPrefix + 'RecordFieldTable.Count', RecordFieldTable.Count);
        for FieldIndex := 0 to RecordFieldTable.Count - 1 do
        begin
          RecordFieldTableField := RecordFieldTable.Fields[FieldIndex];
          RecordFieldPrefix := SafeFormat('%s  RecordFieldTable[%d] Offset %8.8x',
            [Prefix, FieldIndex, RecordFieldTableField.Offset]);
          if Assigned(RecordFieldTableField.TypeInfo^) then
            Log(RecordFieldPrefix, RecordFieldTableField.TypeInfo^, NewPrefix);
        end;
      end;
  end;
end;

procedure TDescriptionLogger.Log(const Description: string; const Item: ShortStringBase);
begin
  if not TGlobalLoggerSettings.GlobalEnabled then
    Exit;
  Log(Description, string(Item));
end;

end.
