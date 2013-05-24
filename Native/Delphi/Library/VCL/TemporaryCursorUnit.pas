unit TemporaryCursorUnit;

interface

uses
  Vcl.Controls;

// .NET/C# Equivalent: http://wiert.me/2012/01/26/netc-using-idisposable-to-restore-temporary-settrings-example-temporarycursor-class/

type
  ITemporaryCursor = interface(IInterface)
    ['{495ADE0F-EFBE-4A0E-BF37-F1ACCACCE03D}']
  end;

  TTemporaryCursor = class(TInterfacedObject, ITemporaryCursor)
  strict private
    FCursor: TCursor;
  public
    constructor Create(const ACursor: TCursor);
    destructor Destroy; override;
    class function SetTemporaryCursor(const ACursor: TCursor = crHourGlass): ITemporaryCursor;
  end;

implementation

uses
  Vcl.Forms;

{ TTemporaryCursor }
constructor TTemporaryCursor.Create(const ACursor: TCursor);
begin
  inherited Create();
  FCursor := Screen.Cursor;
  Screen.Cursor := ACursor;
end;

destructor TTemporaryCursor.Destroy;
begin
  if Assigned(Screen) then
    Screen.Cursor := FCursor;
  inherited Destroy();
end;

class function TTemporaryCursor.SetTemporaryCursor(const ACursor: TCursor = crHourGlass): ITemporaryCursor;
begin
  Result := TTemporaryCursor.Create(ACursor);
end;

end.

uses
   autoCursor, Vcl.Forms;

procedure TForm1.Button1Click(Sender: TObject);
var
  Obj: TSomeObject;
begin
  __SetCursor(crHourGlass);

  Obj:= TSomeObject.Create;
  try
    // do something
  finally
    Obj.Free;
  end;
end;

and Delphi's reference counted interface mechanism takes care of restoring the cursor.



using System.Windows.Forms;
 
namespace bo.Windows.Forms
{
    public class TemporaryCursor : IDisposable
    {
        private Control targetControl;
        private Cursor savedCursor;
        private Cursor temporaryCursor;
        private bool disposed = false;
 
        public TemporaryCursor(Control targetControl, Cursor temporaryCursor)
        {
            if (null == targetControl)
                throw new ArgumentNullException("targetControl");
            if (null == temporaryCursor)
                throw new ArgumentNullException("temporaryCursor");
            this.targetControl = targetControl;
            this.temporaryCursor = temporaryCursor;
            savedCursor = targetControl.Cursor;
            targetControl.Cursor = temporaryCursor;
            targetControl.HandleDestroyed += new EventHandler(targetControl_HandleDestroyed);
        }
 
        void targetControl_HandleDestroyed(object sender, EventArgs e)
        {
            if (null != targetControl)
                if (!targetControl.RecreatingHandle)
                    targetControl = null;
        }
 
        // public so you can call it on the class instance as well as through IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
 
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (null != targetControl)
                {
                    targetControl.HandleDestroyed -= new EventHandler(targetControl_HandleDestroyed);
                    if (temporaryCursor == targetControl.Cursor)
                        targetControl.Cursor = savedCursor;
                    targetControl = null;
                }
                disposed = true;
            }
        }
 
        // Finalizer
        ~TemporaryCursor()
        {
            Dispose(false);
        }
    }
}
