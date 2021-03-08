using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asgard
{
    internal static class Forms
    {
        public static void Placeholder(this Control control, string placeholder)
        {
            if (control.Text == "")
            {
                control.ForeColor = Color.Silver;
                control.Text = placeholder;
            }
            else if (control.Text == placeholder)
            {
                control.ForeColor = Color.White;
                control.Text = "";
            }
        }
    }
}
