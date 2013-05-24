unit AnonymousMethodMementoUnit;

interface

uses
  System.SysUtils;

type
  IAnonymousMethodMemento = interface(IInterface)
  ['{29690E1E-24C8-43A5-8FDF-5F21BB32CEC2}']
  end;

  TAnonymousMethodMemento = class(TInterfacedObject, IAnonymousMethodMemento)
  strict private
    FFinallyProc: TProc;
  public
    constructor Create(const AFinallyProc: TProc);
    destructor Destroy; override;
    procedure Restore(const AFinallyProc: TProc); virtual;
    class function CreateMemento(const AFinallyProc: TProc): IAnonymousMethodMemento;
  end;

implementation

{ TAnonymousMethodMemento }
constructor TAnonymousMethodMemento.Create(const AFinallyProc: TProc);
begin
  inherited Create();
  FFinallyProc := AFinallyProc;
end;

destructor TAnonymousMethodMemento.Destroy;
begin
  Restore(FFinallyProc);
  inherited Destroy();
end;

class function TAnonymousMethodMemento.CreateMemento(const AFinallyProc: TProc): IAnonymousMethodMemento;
begin
  Result := TAnonymousMethodMemento.Create(AFinallyProc);
end;

procedure TAnonymousMethodMemento.Restore(const AFinallyProc: TProc);
begin
  AFinallyProc();
end;

end.
