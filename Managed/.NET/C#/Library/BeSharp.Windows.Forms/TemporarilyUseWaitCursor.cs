using System;
using System.Windows.Forms;
using BeSharp.Generic;

namespace BeSharp.Windows.Forms
{
    /// <summary>
    /// Note this works for only one control including all child controls, as it uses the Control.UseWaitCursor property which also sets all childs.
    /// The Control.Cursor property first looks at the UseWaitCursor state, which overrides any manually assigned Control.Cursor.
    /// </summary>
    public class TemporarilyUseWaitCursor : Disposable, IDisposable
    {
        private Control targetControl;
        private readonly bool savedUseWaitCursorValue;
        private readonly bool useWaitCursorValue;

        public TemporarilyUseWaitCursor(Control targetControl, bool useWaitCursorValue = true)
        {
            if (null == targetControl)
                throw new ArgumentNullException(Reflector.GetName(new { targetControl }));
            this.targetControl = targetControl;
            this.useWaitCursorValue = useWaitCursorValue;
            savedUseWaitCursorValue = targetControl.UseWaitCursor;
            targetControl.UseWaitCursor = useWaitCursorValue;
            targetControl.HandleDestroyed += new EventHandler(targetControl_HandleDestroyed);
        }

        void targetControl_HandleDestroyed(object sender, EventArgs e)
        {
            if (null != targetControl)
                if (!targetControl.RecreatingHandle)
                    targetControl = null; // the handle is gone, so no use setting a cursur handle
        }

        protected override void InternalDispose(bool disposingManagedResources)
        {
            if (null != targetControl)
            {
                targetControl.HandleDestroyed -= new EventHandler(targetControl_HandleDestroyed);
                if (useWaitCursorValue == targetControl.UseWaitCursor) // is it still the value we assigned?
                    targetControl.UseWaitCursor = savedUseWaitCursorValue;
                targetControl = null;
            }
        }
    }
}
