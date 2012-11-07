using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BeSharp.Windows.Forms
{
    public static class ControlExtensions
    {
        public static void RunOnUIThread(this Control target, Action action)
        {
            if (target.InvokeRequired)
                target.Invoke(action);
            else
                action();
        }

        public static IEnumerable<T> FindParentsOfType<T>(this Control control) where T : Control
        {
            Control parent = control.Parent;
            while (!object.ReferenceEquals(parent, null))
            {
                T typedControl = parent as T;
                if (!object.ReferenceEquals(typedControl, null))
                {
                    yield return typedControl;
                }
                parent = parent.Parent;
            }
        }

        public static Form HighestParentForm(this Control control) 
        {
            Control parent = control.Parent;
            if (object.ReferenceEquals(parent, null))
                return null;

            IEnumerable<Form> parentForms = control.FindParentsOfType<Form>();
            Form result = parentForms.Last();
            return result;
        }
    }
}