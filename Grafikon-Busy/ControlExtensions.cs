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
        public static void DisableAll(this IReadOnlyList<CheckBox> checkboxes)
        {
            foreach (var x in checkboxes)
            {
                x.Enabled = false;
                x.Checked = false;
            }
        }
        public static void DisableAll(this IReadOnlyList<Button> buttons)
        {
            foreach (var x in buttons)
            {
                x.Enabled = false;
                
            }
        }
        public static void EnableAll(this IReadOnlyList<CheckBox> controls)
        {
            foreach (var x in controls)
            {
                x.Enabled = true;
            }
        }
        public static void EnableAll(this IReadOnlyList<Button> controls)
        {
            foreach (var x in controls)
            {
                x.Enabled = true;
            }
        }
    }
}
