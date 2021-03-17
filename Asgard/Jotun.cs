using FontAwesome.Sharp;

using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asgard
{
    public partial class Jotun : Form, IForm, IJotun
    {
        public new IChecker Owner { set; get; }
        private int Id { set; get; }
        private string Token { set; get; }

        public Jotun()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
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
            if (parameters.Length == 3)
            {
                Id = (int)parameters[0];
                Token = parameters[1].ToString();
                Owner = (Checker)parameters[2];
            }
        }

        //private void IconButtonOdin_Click(object sender, EventArgs e)
        //{
        //    IconButton iconbutton = (IconButton)sender;
        //    iconbutton.IconChar = IconChar.Cogs;
        //    iconbutton.Text = "VOLVER A ODIN";
        //    IconButtonYmirOff.Hide();
        //    OpenForm<Ymir>(Id, Token, this);
        //}


        public void HideIconActive(string god)
        {

            switch (god)
            {
                case "ymir":
                    IconButtonYmir.IconChar = IconChar.PrayingHands;
                    IconButtonYmir.Text = "INVOCAR A YMIR";
                    IconButtonYmirOff.Show();
                    break;
                default:
                    break;
            }
        }

        private void Jotun_Load(object sender, EventArgs e)
        {
            PanelBlockGate.Hide();
            // Block();
            //TimerSync.Start();
        }

        private async Task Block()
        {
            bool ymir = await Asatru.BlockJotun(Id, 5, Token);
            if (ymir)
            {
                IconButtonYmir.Enabled = false;
                IconButtonYmir.IconColor = Color.Black;
                IconButtonYmir.Text = "POCAS RUNAS";
            }
            else
            {
                IconButtonYmir.Enabled = true;
                IconButtonYmir.IconColor = Color.White;
                IconButtonYmir.Text = "INVOCAR A YMIR";
            }
        }

        private void TimerSync_Tick(object sender, EventArgs e)
        {
            //Block();
        }

        private void IconButtonBack_Click(object sender, EventArgs e)
        {
            Form form = Parent.Controls.OfType<Clans>().FirstOrDefault();
            if (form == null)
            {
                Parent.Parent.Show();
                Parent.Parent.BringToFront();
                form.TopLevel = false;
                form.Dock = DockStyle.Fill;
                Parent.Parent.Controls.Add(form);
                Parent.Parent.Tag = form;
                form.BringToFront();
                form.Show();
            }
            else
            {
                form.BringToFront();
            }
        }

        private async void IconButtonYmir_Click(object sender, EventArgs e)
        {
            bool ymir = await Asatru.BlockJotun(Id, 5, Token);
            if (!ymir)
            {
                IconButton iconbutton = (IconButton)sender;
                iconbutton.IconChar = IconChar.Cogs;
                iconbutton.Text = "VOLVER A YMIR";
                IconButtonYmirOff.Hide();
                OpenForm<Ymir>(Id, Token, this);
            }
            else
            {
                PanelBlockGate.Show();
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            Owner.ClickOnRefillBalance();
            this.Close();
        }
    }
}
