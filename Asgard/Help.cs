using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asgard
{
    public partial class Help : Form, IForm
    {
        private int Id { set; get; }
        private string Token { set; get; }
        public Help()
        {
            InitializeComponent();
        }

        public void InitializeParameters(params object[] parameters)
        {
            if (parameters.Length == 2)
            {
                Id = (int)parameters[0];
                Token = parameters[1].ToString();
            }
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
                form.Tag = true;
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

        private void IconButtonRegister_Click(object sender, EventArgs e)
        {
            Process.Start("https://t.me/soporteasgardchecker");
        }

        private void IconButtonOdin_Click(object sender, EventArgs e)
        {
            string url = "https://www.youtube.com/embed/7SLl5x35wdo";
            string name = "Como Registrarse En ASGARD CHECKER.";
            Color color = Color.DarkRed;
            OpenForm<Video1>(Id, Token, name, url, color);
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            string url = "https://www.youtube.com/embed/0uzmycvaBDU";
            string name = "Como Usar Æsir ( Sección de Gates ) en ASGARD CHECKER.";
            Color color = Color.DarkRed;
            OpenForm<Video1>(Id, Token, name, url, color);
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            string url = "https://www.youtube.com/embed/pxsUQnQlT8U";
            string name = "Como usar BIFROST (Seccion VIRTUAL CARDERS) en ASGARD CHECKER.";
            Color color = Color.DarkRed;
            OpenForm<Video1>(Id, Token, name, url, color);
        }
    }
}