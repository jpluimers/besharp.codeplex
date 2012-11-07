using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BeSharp.Windows.Forms
{

    public static class UITools
    {
#if DEBUG
        public static bool Catch = false;
#else
        public static bool Catch = true;
#endif

        public delegate void Action();

        #region Run...

        public static void RunWithWaitCursor(Action action)
        {
            Cursor savedCursor = Cursor.Current;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                action();
            }
            finally
            {
                Cursor.Current = savedCursor;
            }
        }

        public static void RunWithWaitCursor(this Control control, Action action)
        {
            Cursor savedCursor = control.Cursor;
            try
            {
                control.Cursor = Cursors.WaitCursor;
                action();
            }
            finally
            {
                control.Cursor = savedCursor;
            }
        }

        public static void RunWithWaitCursor(string formName, Action action)
        {
            RunCatchedWithContext(string.Format("when opening form {0}", formName),
                delegate()
                {
                    RunWithWaitCursor(action);
                }
            );
        }

        public static void RunCatchedWithContext(string exceptionContext, Action action)
        {
            if (!Catch)
            {
                action();
                return;
            }

            try
            {
                action();
            }
            catch (Exception ex)
            {
                ShowMessage(string.Format("An error occurred in context {0}.\n\nExeption:\n{1}", exceptionContext, ex));
            }
        }

        public static void ShowWithWaitCursor<T>(string formName) where T : Form, new()
        {
            UITools.RunWithWaitCursor(formName, delegate()
            {
                T form = new T();
                form.Show();
            }
            );
        }

        #endregion

        public static void SetItemCheckedForAllItems(CheckedListBox checkedListbox, bool itemChecked)
        {
            for (int index = 0; index < checkedListbox.Items.Count; index++)
            {
                checkedListbox.SetItemChecked(index, itemChecked);
            }
        }

        #region Clear

        public static void Clear(Control[] controls)
        {
            foreach (Control control in controls)
            {
                Clear(control);
            }
        }

        public static void Clear(Control control)
        {
            DateTimePicker dateTimePicker = control as DateTimePicker;
            if (null != dateTimePicker)
            {
                Clear(dateTimePicker);
                return;
            }

            CheckBox checkBox = control as CheckBox;
            if (null != checkBox)
            {
                checkBox.Checked = true;
                return;
            }

            control.Text = string.Empty;
        }

        public static void Clear(DateTimePicker control)
        {
            // Checked = true/false because otherwise you cannot change the Value
            if (control.Checked == true)
            {
                control.Value = DateTime.Today;
                control.Checked = false;
            }
        }

        #endregion

        #region Validate...

        private static void runTrimmedText(Control control, Action<string> action)
        {
            string text = control.Text.Trim();
            if (text.Length > 0)
            {
                action(text);
            }
        }

        public static void ValidateUIValueInt32(TextBox control)
        {
            runTrimmedText(control, delegate(string text)
            {
                Convert.ToInt32(text);
            }
            );
        }

        public static void ValidateUIValueDouble(TextBox control)
        {
            runTrimmedText(control, delegate(string text)
            {
                Convert.ToDouble(text);
            }
            );
        }

        #endregion

        public static DateTime GetUIValue(DateTimePicker control)
        {
            if (control.Checked)
            {
                return control.Value;
            }
            return new DateTime();
        }

        public static void ShowMessage(string message)
        {
            MessageBox.Show(message); // TODO ##jpl: make a default application caption available
        }

    }
}