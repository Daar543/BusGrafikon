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
        static void DisableAll(this IReadOnlyList<CheckBox> controls)
        {
            foreach (CheckBox c in controls)
            {
                c.Enabled = false;
            }
        }
        static void DisableAll(this IReadOnlyList<Button> controls)
        {
            foreach (Button b in controls)
            {
                b.Enabled = false;
            }
        }
    }
}
