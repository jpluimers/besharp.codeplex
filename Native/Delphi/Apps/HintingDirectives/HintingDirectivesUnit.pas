unit HintingDirectivesUnit;

//[DCC Warning] HintingDirectivesConsoleProject.dpr(9): W1006 Unit 'HintingDirectivesUnit' is deprecated
//unit HintingDirectivesUnit deprecated;

//[DCC Warning] HintingDirectivesConsoleProject.dpr(9): W1007 Unit 'HintingDirectivesUnit' is experimental
//unit HintingDirectivesUnit experimental;

//[DCC Warning] HintingDirectivesConsoleProject.dpr(9): W1004 Unit 'HintingDirectivesUnit' is specific to a library
//unit HintingDirectivesUnit library;

//[DCC Warning] HintingDirectivesConsoleProject.dpr(9): W1005 Unit 'HintingDirectivesUnit' is specific to a platform
//unit HintingDirectivesUnit platform;

//[DCC Warning] HintingDirectivesConsoleProject.dpr(9): W1006 Unit 'HintingDirectivesUnit' is deprecated
//unit HintingDirectivesUnit deprecated 'use a different one';

interface

var
  I: Integer deprecated 'do not use global variables';
  J: Integer deprecated;
  K: Integer deprecated platform library experimental;

type
  THinted = class
  end deprecated platform library experimental;

// W1000 Symbol 'THinted' is deprecated
// W1001 Symbol 'THinted' is specific to a library
// W1002 Symbol 'THinted' is specific to a platform
// W1003 Symbol 'THinted' is experimental
// W1000 Symbol 'THinted' is deprecated
  THintedClass = class of THinted;

  TDefault = class(TObject)
  strict private
    FMember: Integer;
  public
    Field: Integer;
    FieldDepracated: Integer deprecated;
    FieldExperimental: Integer experimental;
    FieldLibrary: Integer library;
    FieldPlatform: Integer platform;
    FieldAll: Integer deprecated platform library experimental;
    function Func: Integer; virtual; abstract;
    function FuncDeprecated: Integer; virtual; deprecated; abstract;
    function FuncExperimental: Integer; virtual; experimental; abstract;
    function FuncLibrary: Integer; virtual; library; abstract;
    function FuncPlatform: Integer; virtual; platform; abstract;
    procedure Proc; virtual; abstract;
    procedure ProcDeprecated virtual; deprecated; abstract;
    procedure ProcExperimental; virtual; experimental; abstract;
    procedure ProcLibrary; virtual; library; abstract;
    procedure ProcPlatform; virtual; platform; abstract;
    property Member: Integer read FMember write FMember;
  strict protected
    procedure ProcDeprecatedComment; virtual; deprecated 'use some other Proc in stead'; abstract;
  strict private
    // E2169 Field definition not allowed after Procs or properties
    // procedure ProcAbstractDeprecated; virtual;  abstract; deprecated;

    // E2029 ';' expected but string constant found
    // procedure ProcExperimentalComment; virtual; experimental 'Experimental Proc'; abstract;

    // E2029 ';' expected but string constant found
    // procedure ProcLibraryComment; virtual; library 'Library Proc'; abstract;

    // E2029 ';' expected but string constant found
    // procedure ProcPlatformComment; virtual; platform 'Platform Proc'; abstract;

    // E2169 Field definition not allowed after Procs or properties
    // property MemberDeprecated: Integer read FMember write FMember; deprecated;
  end;

// W1001 Symbol 'THinted' is specific to a library
// W1002 Symbol 'THinted' is specific to a platform
// W1003 Symbol 'THinted' is experimental
implementation

procedure UseDefault;
var
  Default: TDefault;
begin
// W1000 Symbol 'I' is deprecated: 'do not use global variables'
  I := 1;
// W1000 Symbol 'J' is deprecated
  J := 2;
// W1000 Symbol 'K' is deprecated
// W1001 Symbol 'K' is specific to a library
// W1002 Symbol 'K' is specific to a platform
// W1003 Symbol 'K' is experimental
  K := 3;
// W1000 Symbol 'FuncDeprecated' is deprecated
// W1003 Symbol 'FuncExperimental' is experimental
// W1001 Symbol 'FuncLibrary' is specific to a library
// W1002 Symbol 'FuncPlatform' is specific to a platform

// W1000 Symbol 'ProcDeprecated' is deprecated
// W1003 Symbol 'ProcExperimental' is experimental
// W1001 Symbol 'ProcLibrary' is specific to a library
// W1002 Symbol 'ProcPlatform' is specific to a platform

// W1000 Symbol 'ProcDeprecatedComment' is deprecated: 'use some other Proc in stead'
  Default := TDefault.Create();
  try
    Default.Field := 1;
// W1000 Symbol 'FieldDepracated' is deprecated
    Default.FieldDepracated := 1;
// W1003 Symbol 'FieldExperimental' is experimental
    Default.FieldExperimental := 1;
// W1001 Symbol 'FieldLibrary' is specific to a library
    Default.FieldLibrary := 1;
// W1002 Symbol 'FieldPlatform' is specific to a platform
    Default.FieldPlatform := 1;
  finally
    Default.Free;
  end;
end;

end.
