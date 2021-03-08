using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asgard
{
    public partial class Result : Form
    {
        public Result()
        {
            InitializeComponent();
        }

        private void Generator_Load(object sender, EventArgs e)
        {

        }

        private void LabelGroupBoxGenerator_Click(object sender, EventArgs e)
        {

        }

        private void Result_Paint(object sender, PaintEventArgs e)
        {
            Rectangle borderTextBoxLive = new Rectangle(TextBoxLive.Location.X, TextBoxLive.Location.Y, TextBoxLive.ClientSize.Width + 17, TextBoxLive.ClientSize.Height);
            borderTextBoxLive.Inflate(1, 1); // border thickness
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxLive, Color.White, ButtonBorderStyle.Solid);

            Rectangle borderTextBoxDie = new Rectangle(TextBoxDie.Location.X, TextBoxDie.Location.Y, TextBoxDie.ClientSize.Width + 17, TextBoxDie.ClientSize.Height);
            borderTextBoxDie.Inflate(1, 1); // border thickness
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxDie, Color.White, ButtonBorderStyle.Solid);
        }
    }
}
