using System;
using System.Windows.Forms;

namespace BeSharp.Windows.Forms
{
    public class TemporaryFormCursor : Disposable, IDisposable
    {
        private readonly TemporaryCursor temporaryCursor;

        public TemporaryFormCursor(Control targetControl, Cursor temporaryFormCursor)
        {
            Form parentForm = targetControl.HighestParentForm();
            if (!object.ReferenceEquals(parentForm, null))
                temporaryCursor = new TemporaryCursor(parentForm, temporaryFormCursor);
        }

        protected override void InternalDispose(bool disposingManagedResources)
        {
            temporaryCursor.Free();
        }
    }
}