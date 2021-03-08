using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CefSharp;
using CefSharp.WinForms;

namespace Asgard
{
    public partial class Bifrost : Form, IForm
    {
        private int Id { set; get; }
        private string Token { set; get; }
        public Bifrost()
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

        public void InitializeParameters(params object[] parameters)
        {
            if (parameters.Length == 2)
            {
                Id = (int)parameters[0];
                Token = parameters[1].ToString();
            }
        }
        private void IconButtonShop1_Click(object sender, EventArgs e)
        {
            string url = "http://fe-acc18.ru";
            string name = "Fe-Shop.";
            Color color = Color.DarkRed;
            OpenForm<Browser>(Id, Token, name, url, color);
        }
        private void IconButtonShop2_Click(object sender, EventArgs e)
        {
            string url = "https://ccvvstore.bz";
            string name = "CcvvStore.";
            Color color = Color.FromArgb(255, 127, 0);
            OpenForm<Shop2>(Id, Token, name, url, color);
        }

        private void IconButtonShop3_Click(object sender, EventArgs e)
        {
            string url = "https://anxiety-blast-space.net";
            string name = "Joker's Stash.";
            Color color = Color.FromArgb(227, 184, 20);
            OpenForm<Shop3>(Id, Token, name, url, color);
        }

        private void IconButtonShop4_Click(object sender, EventArgs e)
        {
            string url = "https://unicvv.ru";
            string name = "Unicvv.";
            Color color = Color.FromArgb(46, 179, 31);
            OpenForm<Shop4>(Id, Token, name, url, color);
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            string url = "https://cardhouse.africa/";
            string name = "Card House.";
            Color color = Color.Navy;
            OpenForm<Shop5>(Id, Token, name, url, color);
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            string url = "https://toxyzen.ru/";
            string name = "Toxyzen.";
            Color color = Color.Indigo;
            OpenForm<Shop6>(Id, Token, name, url, color);
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            string url = "http://amauta.cc/";
            string name = "Amauta.";
            Color color = Color.FromArgb(148, 0, 211);
            OpenForm<Shop7>(Id, Token, name, url, color);
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            string url = "https://boveda7k.net/";
            string name = "Boveda 7k.";
            Color color = Color.Black;
            OpenForm<Shop8>(Id, Token, name, url, color);
        }
    }
}
