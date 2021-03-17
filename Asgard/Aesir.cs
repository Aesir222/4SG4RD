using FontAwesome.Sharp;

using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asgard
{
    public partial class Aesir : Form, IForm, IAesir
    {
        public new IChecker Owner { set; get; }
        public new IHome OHome { set; get; }
        private int Id { set; get; }
        private string Token { set; get; }

        public Aesir()
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

        private async void IconButtonOdin_Click(object sender, EventArgs e)
        {
            bool odin = await Asatru.BlockAesir(Id, 1, Token);
            if (!odin)
            {
                IconButton iconbutton = (IconButton)sender;
                iconbutton.IconChar = IconChar.Cogs;
                iconbutton.Text = "VOLVER A ODIN";
                IconButtonOdinOff.Hide();
                OpenForm<Odin>(Id, Token, this, Owner);
            }
            else
            {
                PanelBlockGate.Show();
            }
        }
        private async void IconButtonFrigg_Click(object sender, EventArgs e)
        {
            bool frigg = await Asatru.BlockAesir(Id, 2, Token);
            if (!frigg)
            {
                IconButton iconbutton = (IconButton)sender;
                iconbutton.IconChar = IconChar.Cogs;
                iconbutton.Text = "VOLVER A FRIGG";
                IconButtonFriggOff.Hide();
                OpenForm<Frigg>(Id, Token, this, Owner);
            }
            else
            {
                PanelBlockGate.Show();
            }
        }

        private async void IconButtonThor_Click(object sender, EventArgs e)
        {
            bool thor = await Asatru.BlockAesir(Id, 3, Token);
            if (!thor)
            {
                IconButton iconbutton = (IconButton)sender;
                iconbutton.Text = "VOLVER A THOR";
                iconbutton.IconChar = IconChar.Cogs;
                IconButtonThorOff.Hide();
                OpenForm<Thor>(Id, Token, this, Owner);
            }
            else
            {
                PanelBlockGate.Show();
            }
        }

        private async void IconButtonBalder_Click(object sender, EventArgs e)
        {
            bool balder = await Asatru.BlockAesir(Id, 4, Token);
            if (!balder)
            {
                IconButton iconbutton = (IconButton)sender;
                iconbutton.IconChar = IconChar.Cogs;
                iconbutton.Text = "VOLVER A BALDER";
                IconButtonBalderOff.Hide();
                OpenForm<Balder>(Id, Token, this, Owner);
            }
            else
            {
                PanelBlockGate.Show();
            }
        }

        private void IconButtonHeimdal_Click(object sender, EventArgs e)
        {
            //OpenForm<Heimdal>(Id, Token);
        }


        public void HideIconActive(string god)
        {

            switch (god)
            {
                case "odin":
                    IconButtonOdin.IconChar = IconChar.PrayingHands;
                    IconButtonOdin.Text = "INVOCAR A ODIN";
                    IconButtonOdinOff.Show();
                    break;
                case "frigg":
                    IconButtonFrigg.IconChar = IconChar.PrayingHands;
                    IconButtonFrigg.Text = "INVOCAR A FRIGG";
                    IconButtonFriggOff.Show();
                    break;
                case "thor":
                    IconButtonThor.IconChar = IconChar.PrayingHands;
                    IconButtonThor.Text = "INVOCAR A THOR";
                    IconButtonThorOff.Show();
                    break;
                case "balder":
                    IconButtonBalder.IconChar = IconChar.PrayingHands;
                    IconButtonBalder.Text = "INVOCAR A BALDER";
                    IconButtonBalderOff.Show();
                    break;
                default:
                    break;
            }
        }

        private void Aesir_Load(object sender, EventArgs e)
        {
            PanelBlockGate.Hide();
            // Task.Run(() => Block());
        }

        private async Task Block()
        {
            bool odin = await Asatru.BlockAesir(Id, 1, Token);
            if (odin)
            {
                IconButtonOdin.Enabled = false;
                IconButtonOdin.IconColor = Color.Black;
                IconButtonOdin.Text = "POCAS RUNAS";
            }
            else
            {
                IconButtonOdin.Enabled = true;
                IconButtonOdin.IconColor = Color.White;
                IconButtonOdin.Text = "INVOCAR A ODIN";
            }

            bool frigg = await Asatru.BlockAesir(Id, 2, Token);
            if (frigg)
            {
                IconButtonFrigg.Enabled = false;
                IconButtonFrigg.IconColor = Color.Black;
                IconButtonFrigg.Text = "POCAS RUNAS";
            }
            else
            {
                IconButtonFrigg.Enabled = true;
                IconButtonFrigg.IconColor = Color.White;
                IconButtonFrigg.Text = "INVOCAR A FRIGG";

            }
            bool thor = await Asatru.BlockAesir(Id, 3, Token);
            if (thor)
            {
                IconButtonThor.Enabled = false;
                IconButtonThor.IconColor = Color.Black;
                IconButtonThor.Text = "POCAS RUNAS";
            }
            else
            {
                IconButtonThor.Enabled = true;
                IconButtonThor.IconColor = Color.Black;
                IconButtonThor.Text = "INVOCAR A THOR";
            }

            bool balder = await Asatru.BlockAesir(Id, 4, Token);
            if (balder)
            {
                IconButtonBalder.Enabled = false;
                IconButtonBalder.IconColor = Color.Black;
                IconButtonBalder.Text = "POCAS RUNAS";
            }
            else
            {
                IconButtonBalder.Enabled = true;
                IconButtonBalder.IconColor = Color.Black;
                IconButtonBalder.Text = "INVOCAR A BALDER";
            }
        }

        private void TimerSync_Tick(object sender, EventArgs e)
        {
            Block();
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

        private void iconButton1_Click(object sender, EventArgs e)
        {
            Owner.ClickOnRefillBalance();
            this.Close();
        }
    }
}
