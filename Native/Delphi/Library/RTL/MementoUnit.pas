unit MementoUnit;

interface

uses
  TemporaryCursorUnit;

type
  IMemento = interface(IInterface)
  ['{529987B4-0C7C-4103-BCE7-EB9651E88E58}']
  end;

  TMemento = class(TInterfacedObject, IMemento)
  strict private
    FObject: TObject;
  public
    constructor Create(const AObject: TObject);
    destructor Destroy; override;
    procedure Restore(const AObject: TObject); virtual; abstract;
    class function CreateMemento(const AObject: TObject): IMemento;
  end;

implementation

{ TMemento }
constructor TMemento.Create(const AObject: TObject);
begin
  inherited Create();
  FObject := AObject;
end;

destructor TMemento.Destroy;
begin
  Restore(FObject);
  inherited Destroy();
end;

class function TMemento.CreateMemento(const AObject: TObject): IMemento;
begin
  Result := TMemento.Create(AObject);
end;

end.
