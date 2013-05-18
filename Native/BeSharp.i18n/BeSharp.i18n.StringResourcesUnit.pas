unit BeSharp.i18n.StringResourcesUnit;

interface

uses
  Winapi.Windows;

type
  TStringResources = class(TObject)
  public
    class function FindResourceStringId(const resource_handle: HMODULE;
      const search_resource_string: string; const langId: UINT): UINT;
    // 1 http://blogs.msdn.com/b/oldnewthing/archive/2004/01/30/65013.aspx
    class function FindStringResourceEx(const hInstance: HMODULE;
      const uId, langId: UINT): string;
    class function LoadString(const hInstance: HMODULE; const uId: UINT)
      : string;
  const
    LCID_0413_Dutch = $413;
    LCID_0409_English = $409;
  end;

implementation

uses
  System.SysUtils;

class function TStringResources.FindResourceStringId(const resource_handle
  : HMODULE; const search_resource_string: string; const langId: UINT): UINT;
var
  resource_id: UINT;
  i: Word;
  resource_string: string;
  compare_string: string;
begin
  resource_id := High(resource_id);
  for i := Low(i) to High(i) do
  begin
    resource_string := FindStringResourceEx(resource_handle, i, langId);
    compare_string := Copy(resource_string, Length(search_resource_string));
    if (resource_string <> '') and (SameStr(resource_string, compare_string))
    then
      resource_id := i;
  end;
  Result := resource_id;
end;

class function TStringResources.FindStringResourceEx(const hInstance: HMODULE;
  const uId, langId: UINT): string;
const
  StringsPerBucket = 16;
var
  BucketWideCharsPointer: LPCWSTR;
  BucketResourceHandle: HRSRC;
  BucketGlobalHandle: HGLOBAL;
  BucketPointer: Pointer;
  i: UINT;
  BucketNumber: Cardinal;
  BucketIntResource: PWideChar;
  IndexInBucket: UINT;
  StringLengthPointer: PWord;
  StringLength: Word;
begin
  // http://www.webdelphi.ru/2013/01/stringtable-i-rabota-s-identifikatorami-yazykov-v-delphi/
  // http://blogs.msdn.com/b/oldnewthing/archive/2004/01/30/65013.aspx
  // The format of string resources

  // 16 string entries per bucket
  // each entry starts with a 2-byte length, then Length 2-byte character pairs (UTF-16) without null termination

  Result := ''; // assume failure
  // Convert the string ID into a bundle number
  BucketNumber := uId div StringsPerBucket + 1;
  BucketIntResource := MAKEINTRESOURCE(BucketNumber);
  BucketResourceHandle := FindResourceEx(hInstance, RT_STRING,
    BucketIntResource, langId);
  if (BucketResourceHandle <> 0) then
  begin
    BucketGlobalHandle := LoadResource(hInstance, BucketResourceHandle);
    if (BucketGlobalHandle <> 0) then
    begin
      BucketPointer := LockResource(BucketGlobalHandle);
      // http://msdn.microsoft.com/en-us/library/windows/desktop/ms648047
      BucketWideCharsPointer := LPCWSTR(BucketPointer);
      if (BucketWideCharsPointer <> nil) then
      begin
        // okay now walk the string table
        IndexInBucket := (uId and (StringsPerBucket - 1));
        for i := 1 to IndexInBucket do // skip n-1 entries
        begin
          StringLengthPointer := PWord(BucketWideCharsPointer);
          StringLength := StringLengthPointer^;
          Inc(BucketWideCharsPointer); // skip the length Word
          Inc(BucketWideCharsPointer, StringLength); // skip the content
        end;
        StringLengthPointer := PWord(BucketWideCharsPointer);
        StringLength := StringLengthPointer^;
        Inc(BucketWideCharsPointer); // skip the length Word
        if StringLength <> 0 then
          Result := Copy(BucketWideCharsPointer, 1, StringLength);
        UnlockResource(BucketGlobalHandle)
        // NOP in Win32, so no need to check result and perform RaiseLastOSError();
      end;
      FreeResource(BucketGlobalHandle);
      // http://msdn.microsoft.com/en-us/library/windows/desktop/ms648044 in Win32 it will return false, so no need to check result and perform RaiseLastOSError();
    end;
  end;
end;

class function TStringResources.LoadString(const hInstance: HMODULE;
  const uId: UINT): string;
// function LoadString(hInstance: HINST; uID: UINT; lpBuffer: PWideChar; nBufferMax: Integer): Integer; stdcall;
var
  Buffer: array [0 .. 1023] of char;
  // reasonable length; might increase for really long resource strings.
  StringLength: Integer;
begin
  StringLength := Winapi.Windows.LoadString(hInstance, uId, Buffer,
    Length(Buffer));
  SetString(Result, Buffer, StringLength);
end;

end.
