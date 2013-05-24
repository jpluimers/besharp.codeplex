{ Copyright (c) 2007-2013 Jeroen Wiert Pluimers for BeSharp.net and better office benelux.
Full BSD License is available at http://besharp.codeplex.com/license and http://bo.codeplex.com/license }

unit StringListWrapperInterfaceUnit;

interface

uses
  System.Classes;

type
  // TODO ##jwp separate into a IStringsWrapper and a IStringListWrapper.
  IStringListWrapper = interface(IInterface)
    function Add(const S: string): Integer;
    function AddObject(const S: string; const AObject: TObject): Integer;
    procedure AddStrings(const Strings: TStrings); overload;
    procedure AddStringListWrapper(const StringListWrapper: IStringListWrapper); overload;
    procedure Append(const S: string);
    procedure BeginUpdate;
    procedure Clear;
    procedure Delete(Index: Integer);
    procedure EndUpdate;
    function Equals(const Strings: TStrings): Boolean;
    procedure Exchange(const Index1, Index2: Integer);
    function Get(Index: Integer): string;
    function GetCapacity: Integer;
    function GetCommaText: string;
    function GetCount: Integer;
    function GetDelimitedText: string;
    function GetDelimiter: Char;
    function GetEnumerator: TStringsEnumerator;
    function GetLineBreak: string;
    function GetName(Index: Integer): string;
    function GetNameValueSeparator: Char;
    function GetObject(Index: Integer): TObject;
    function GetQuoteChar: Char;
    function GetStrictDelimiter: Boolean;
    function GetTextStr: string;
    function GetValue(const Name: string): string;
    function GetValueFromIndex(Index: Integer): string;
    function IndexOf(const S: string): Integer;
    function IndexOfName(const Name: string): Integer;
    function IndexOfObject(const AObject: TObject): Integer;
    procedure Insert(const Index: Integer; const S: string);
    procedure InsertObject(const Index: Integer; const S: string; const AObject: TObject);
    procedure LoadFromFile(const FileName: string);
    procedure LoadFromStream(const Stream: TStream);
    procedure Move(const CurIndex, NewIndex: Integer);
    procedure Put(Index: Integer; const S: string);
    procedure PutObject(Index: Integer; const AObject: TObject);
    procedure SaveToFile(const FileName: string);
    procedure SaveToStream(const Stream: TStream);
    procedure SetCapacity(const NewCapacity: Integer);
    procedure SetCommaText(const Value: string);
    procedure SetDelimitedText(const Value: string);
    procedure SetDelimiter(const Value: Char);
    procedure SetLineBreak(const Value: string);
    procedure SetNameValueSeparator(const Value: Char);
    procedure SetQuoteChar(const Value: Char);
    procedure SetStrictDelimiter(const Value: Boolean);
    procedure SetText(Text: PChar);
    procedure SetTextStr(const Value: string);
    procedure SetValue(const Name, Value: string);
    procedure SetValueFromIndex(Index: Integer; const Value: string);
    property Capacity: Integer read GetCapacity write SetCapacity;
    property CommaText: string read GetCommaText write SetCommaText;
    property Count: Integer read GetCount;
    property Delimiter: Char read GetDelimiter write SetDelimiter;
    property DelimitedText: string read GetDelimitedText write SetDelimitedText;
    property LineBreak: string read GetLineBreak write SetLineBreak;
    property Names[Index: Integer]: string read GetName;
    property Objects[Index: Integer]: TObject read GetObject write PutObject;
    property QuoteChar: Char read GetQuoteChar write SetQuoteChar;
    property Values[const Name: string]: string read GetValue write SetValue;
    property ValueFromIndex[Index: Integer]: string read GetValueFromIndex write SetValueFromIndex;
    property NameValueSeparator: Char read GetNameValueSeparator write SetNameValueSeparator;
    property StrictDelimiter: Boolean read GetStrictDelimiter write SetStrictDelimiter;
    property Strings[Index: Integer]: string read Get write Put; default;
    property Text: string read GetTextStr write SetTextStr;
  end;

implementation

end.
