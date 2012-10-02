program SplitListOfIntegersIntoSublists;

{$APPTYPE CONSOLE}

{$R *.res}

uses
  System.SysUtils,
  System.Generics.Collections;

type
  TIntegerList = TList<Integer>;
  TIntegerListList = TList<TIntegerList>;
  TMain = class(TObject)
  private
    class function AddNewIntegerList(IntegerListList: TIntegerListList): TIntegerList;
    class procedure AddValues(AllIntegers: TIntegerList);
    class procedure Fill_NewListForEachValueChange(const AllIntegers: TIntegerList; const IntegerListList: TIntegerListList);
    class procedure Fill_NewListForDistinctValues(const AllIntegers: TIntegerList; const IntegerListList: TIntegerListList);
    class procedure Free(const IntegerListList: TIntegerListList);
    class procedure Print(const IntegerList: TIntegerList); overload;
    class procedure Print(const IntegerListList: TIntegerListList); overload;
  public
    class procedure Run;
  end;

class function TMain.AddNewIntegerList(IntegerListList: TIntegerListList): TIntegerList;
begin
  Result := TIntegerList.Create;
  IntegerListList.Add(Result);
end;

class procedure TMain.AddValues(AllIntegers: TIntegerList);
begin
// 1, 1, 2, 2, 2, 2, 3, 4, 4, 4
  AllIntegers.Add(1);
  AllIntegers.Add(1);
  AllIntegers.Add(2);
  AllIntegers.Add(2);
  AllIntegers.Add(2);
  AllIntegers.Add(2);
  AllIntegers.Add(3);
  AllIntegers.Add(4);
  AllIntegers.Add(4);
  AllIntegers.Add(4);
end;

class procedure TMain.Fill_NewListForEachValueChange(const AllIntegers: TIntegerList; const IntegerListList: TIntegerListList);
var
  IntegerList: TIntegerList;
  Value: Integer;
begin
  IntegerList := nil;
  for Value in AllIntegers do
  begin
    if (IntegerList = nil) or (Value <> IntegerList.First) then
      IntegerList := AddNewIntegerList(IntegerListList);
    IntegerList.Add(Value);
  end;
end;

class procedure TMain.Fill_NewListForDistinctValues(const AllIntegers: TIntegerList; const IntegerListList:
    TIntegerListList);
type
  TIntegerListDictionary = TDictionary<Integer, TIntegerList>;
var
  IntegerListDictionary: TIntegerListDictionary;
  IntegerList: TIntegerList;
  Value: Integer;
begin
  IntegerListDictionary := TIntegerListDictionary.Create();
  for Value in AllIntegers do
  begin
    if IntegerListDictionary.ContainsKey(Value) then
      IntegerList := IntegerListDictionary[Value]
    else
    begin
      IntegerList := AddNewIntegerList(IntegerListList);
      IntegerListDictionary.Add(Value, IntegerList);
    end;
    IntegerList.Add(Value);
  end;
end;

class procedure TMain.Free(const IntegerListList: TIntegerListList);
var
  IntegerList: TIntegerList;
begin
  for IntegerList in IntegerListList do
    IntegerList.Free;
  IntegerListList.Free;
end;

class procedure TMain.Print(const IntegerList: TIntegerList);
var
  Value: Integer;
  First: Boolean;
begin
  First := True;
  for Value in IntegerList do
  begin
    if not First then
      Write(', ');
    Write(Value);
    First := False;
  end;
  Writeln;
end;

class procedure TMain.Print(const IntegerListList: TIntegerListList);
var
  IntegerList: TIntegerList;
begin
  for IntegerList in IntegerListList do
    Print(IntegerList);
  Writeln;
end;

class procedure TMain.Run;
var
  AllIntegers: TIntegerList;
  IntegerListList: TIntegerListList;
begin
  AllIntegers := TIntegerList.Create();
  try
    AddValues(AllIntegers);
    Print(AllIntegers);

    IntegerListList := TIntegerListList.Create();
    try
      Fill_NewListForEachValueChange(AllIntegers, IntegerListList);
      Print(IntegerListList);
    finally
      Free(IntegerListList);
    end;

    AddValues(AllIntegers);
    Print(AllIntegers);

    IntegerListList := TIntegerListList.Create();
    try
      Fill_NewListForEachValueChange(AllIntegers, IntegerListList);
      Print(IntegerListList);
    finally
      Free(IntegerListList);
    end;

    Print(AllIntegers);

    IntegerListList := TIntegerListList.Create();
    try
      Fill_NewListForDistinctValues(AllIntegers, IntegerListList);
      Print(IntegerListList);
    finally
      Free(IntegerListList);
    end;
  finally
    AllIntegers.Free;
  end;
end;

begin
  try
    TMain.Run();
  except
    on E: Exception do
      Writeln(E.ClassName, ': ', E.Message);
  end;
end.
