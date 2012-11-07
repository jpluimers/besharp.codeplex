using System;
using System.Windows.Forms;
using BeSharp.Generic;

namespace BeSharp.Windows.Forms
{
    /// <summary>
    /// Note this works for only one control at a time, so if the control has child controls on top, you won't see the effect
    /// </summary>
    public class TemporaryCursor : Disposable, IDisposable
    {
        private Control targetControl;
        private readonly Cursor savedCursor;
        private readonly Cursor temporaryCursor;

        public TemporaryCursor(Control targetControl, Cursor temporaryCursor)
        {
            if (null == targetControl)
                throw new ArgumentNullException(Reflector.GetName(new { targetControl }));
            if (null == temporaryCursor)
                throw new ArgumentNullException(Reflector.GetName(new { temporaryCursor }));
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

        protected override void InternalDispose(bool disposingManagedResources)
        {
            if (null != targetControl)
            {
                targetControl.HandleDestroyed -= new EventHandler(targetControl_HandleDestroyed);
                if (temporaryCursor == targetControl.Cursor) // is it still the value we assigned?
                    targetControl.Cursor = savedCursor;
                targetControl = null;
            }
        }
    }
}