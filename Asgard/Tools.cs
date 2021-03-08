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
    public partial class Tools : Form, IForm
    {
        private int Id { set; get; }
        private string Token { set; get; }
        public Tools()
        {
            InitializeComponent();
        }

        private void OpenForm<MyForm>(params object[] args) where MyForm : Form, new()
        {
            Form form = Parent.Controls.OfType<MyForm>().FirstOrDefault();
            if (form == null)
            {
                form = new MyForm();
                ((IForm)form).InitializeParameters(args);
                Parent.BringToFront();
                form.TopLevel = false;
                form.Dock = DockStyle.Fill;
                Parent.Controls.Add(form);
                Parent.Tag = form;
                form.BringToFront();
                form.Show();
            }
            else
            {
                form.BringToFront();
            }
        }

        public void InitializeParameters(params object[] parameters)
        {
            if (parameters.Length == 2)
            {
                Id = (int)parameters[0];
                Token = parameters[1].ToString();
            }
        }

        private void IconButtonCombos_Click(object sender, EventArgs e)
        {
            OpenForm<Combos>(Id, Token);
        }

        private void IconButtonRealCC_Click(object sender, EventArgs e)
        {
            OpenForm<Combos>(Id, Token);
        }
    }
}
