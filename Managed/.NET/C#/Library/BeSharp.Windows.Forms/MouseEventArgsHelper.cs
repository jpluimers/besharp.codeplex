using System.Windows.Forms;

namespace BeSharp.Windows.Forms
{
    // .NET 2.0 or less: use a helper
    public class MouseEventArgsHelper
    {
        public static bool IsButtonPressed(MouseButtons button, MouseEventArgs e)
        {
            bool result = ((e.Button & button) == button);
            return result;
        }

        public static bool IsAnyOftheseButtonsPressed(MouseButtons button, MouseEventArgs e)
        {
            bool result = ((e.Button & button) != 0);
            return result;
        }
    }

    // .NET 3.0 or better: use extension methods
    public static class MouseEventArgsExtensions
    {
        public static bool IsButtonPressed(this MouseEventArgs e, MouseButtons button)
        {
            bool result = MouseEventArgsHelper.IsButtonPressed(button, e);
            return result;
        }

        public static bool IsAnyOftheseButtonsPressed(this MouseEventArgs e, MouseButtons button)
        {
            bool result = MouseEventArgsHelper.IsAnyOftheseButtonsPressed(button, e);
            return result;
        }
    }
}