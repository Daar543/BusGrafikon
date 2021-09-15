using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafikon_Busy
{
    public static class ControlExtensions
    {
        internal static void DisableAll(this IReadOnlyList<CheckBox> checkboxes)
        {
            foreach (var x in checkboxes)
            {
                x.Enabled = false;
                x.Checked = false;
            }
        }
        internal static void DisableAll(this IReadOnlyList<Button> buttons)
        {
            foreach (var x in buttons)
            {
                x.Enabled = false;
                
            }
        }
        internal static void EnableAll(this IReadOnlyList<CheckBox> controls)
        {
            foreach (var x in controls)
            {
                x.Enabled = true;
            }
        }
        internal static void EnableAll(this IReadOnlyList<Button> controls)
        {
            foreach (var x in controls)
            {
                x.Enabled = true;
            }
        }
    }
}
