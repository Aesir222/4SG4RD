using FontAwesome.Sharp;

using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asgard
{
    public partial class Clans : Form, IForm, IClans
    {
        public new IChecker Owner { get; set; }
        public new IHome OHome { get; set; }
        private int Id { set; get; }
        private string Token { set; get; }
        private dynamic Voucher { set; get; }

        public Clans()
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
        //    IconButtonOdinOff.Hide();
        //    OpenForm<Odin>(Id, Token, this);
        //}

        //private void IconButtonFrigg_Click(object sender, EventArgs e)
        //{
        //    IconButton iconbutton = (IconButton)sender;
        //    iconbutton.IconChar = IconChar.Cogs;
        //    iconbutton.Text = "VOLVER A FRIGG";
        //    IconButtonFriggOff.Hide();
        //    OpenForm<Frigg>(Id, Token, this);
        //}


        //public void HideIconActive(string god)
        //{

        //    switch (god)
        //    {
        //        case "odin":
        //            IconButtonAesir.IconChar = IconChar.PrayingHands;
        //            IconButtonAesir.Text = "INVOCAR A ODIN";
        //            IconButtonOdinOff.Show();
        //            break;
        //        case "frigg":
        //            IconButtonFrigg.IconChar = IconChar.PrayingHands;
        //            IconButtonFrigg.Text = "INVOCAR A FRIGG";
        //            IconButtonFriggOff.Show();
        //            break;
        //        case "thor":
        //            IconButtonThor.IconChar = IconChar.PrayingHands;
        //            IconButtonThor.Text = "INVOCAR A THOR";
        //            IconButtonThorOff.Show();
        //            break;
        //        case "balder":
        //            IconButtonJotun.IconChar = IconChar.PrayingHands;
        //            IconButtonJotun.Text = "INVOCAR A BALDER";
        //            IconButtonBalderOff.Show();
        //            break;
        //        default:
        //            break;
        //    }
        //}

        private void Clans_Load(object sender, EventArgs e)
        {
            //Block();
            //TimerSync.Start();
            //IconButtonJotun.Enabled = false;
            //IconButtonJotun.IconColor = Color.Black;
            //PanelBlockGateClose.Hide();
         //   PictureBoxJotun.Hide();
            PanelBlockGateClose.Hide();
            PanelConfirm.Hide();
            PanelVoucher.Hide();
            Task.Run(() => Block());
            //Task.Run(() => LoadPlanVIP());
            // TimerSync.Start();
        }

        //public async void LoadPlanVIP()
        //{
        //    try
        //    {
        //        PictureBoxLoadJotun.Show();
        //        bool planVIP = await Asatru.GetPlanVIP(Id, Token);
        //        if (planVIP)
        //        {
        //            IconButtonJotun.Enabled = true;
        //            IconButtonJotun.IconColor =Color.White;
        //            IconButtonVIP.Hide();
        //            PictureBoxJotun.Show();
        //            PictureBoxJotunDisable.Hide();
        //        }
        //        else
        //        {
        //            IconButtonJotun.Enabled = false;
        //            IconButtonJotun.IconColor = Color.Black;
        //            PictureBoxJotun.Show();
        //            PictureBoxJotunDisable.Hide();
        //            //IconButtonJotun.BackColor = Color.Silver;
        //        }
        //        PictureBoxLoadJotun.Hide();
        //    }
        //    catch (Exception) { }
        //}


        private async Task Block()
        {
            try
            {
                PanelBlockGate.Show();
                dynamic planDetails = await Asatru.GetPlanDetails(Id, Token);
                if (planDetails == null)
                {
                    PanelBlockGateClose.Show();
                }
                else
                {
                    //PanelBlockGateClose.Hide();
                    PanelBlockGate.Hide();
                }

            }
            catch (Exception)
            {

                throw;
            }
            //bool odin = await Asatru.BlockClans(Id, 1, Token);
            //if (odin)
            //{
            //    IconButtonAesir.Enabled = false;
            //    IconButtonAesir.IconColor = Color.Black;
            //    IconButtonAesir.Text = "POCAS RUNAS";
            //}
            //else
            //{
            //    IconButtonAesir.Enabled = true;
            //    IconButtonAesir.IconColor = Color.White;
            //    IconButtonAesir.Text = "INVOCAR A ODIN";
            //}

            //bool frigg = await Asatru.BlockClans(Id, 2, Token);
            //if (frigg)
            //{
            //    IconButtonJotun.Enabled = false;
            //    IconButtonJotun.IconColor = Color.Black;
            //    IconButtonJotun.Text = "POCAS RUNAS";
            //}
            //else
            //{
            //    IconButtonJotun.Enabled = true;
            //    IconButtonJotun.IconColor = Color.White;
            //    IconButtonJotun.Text = "INVOCAR A FRIGG";

            //}
        }

        private void IconButtonAesir_Click(object sender, EventArgs e)
        {
            
            OpenForm<Aesir>(Id, Token, Owner);
        }

        private async void IconButtonJotun_Click(object sender, EventArgs e)
        {
            //PictureBoxLoadJotun.Show();
            bool planVIP = await Asatru.GetPlanVIP(Id, Token);
            if (planVIP)
            {
                OpenForm<Jotun>(Id, Token, Owner);
            }
            else
            {
                PanelConfirm.Show();
            }
        }

        private void IconButtonVIP_Click(object sender, EventArgs e)
        {
            try
            {
                PanelConfirm.Show();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async void IconButtonConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                PanelConfirm.Hide();
                Voucher = await Asatru.RequestPLan(Token, Id, 5);
                if (Voucher != null)
                {
                    LabelVoucher.Text = Voucher;
                    PanelVoucher.Show();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void IconButtonBlockGateClose_Click(object sender, EventArgs e)
        {
            string voucher = "Paga a traves de Bitcoin " + LabelPlanNValue.Text + "\n\r" +
                "ASGARD BITCOIN WALLET: " + LabelWallet.Text + "\n\r" +
                "Envia CAPTURA del pago junto a este Voucher al chat de ventas." + "\n\r" +
                "VOUCHER: " + Voucher;
            Clipboard.SetText(voucher);
        }

        private void IconButtonCloseVoucher_Click(object sender, EventArgs e)
        {
            Voucher = null;
            LabelVoucher.Text = "-";
            PanelVoucher.Hide();
        }

        private void TimerSync_Tick(object sender, EventArgs e)
        {
            //Task.Run(() => LoadPlanVIP());
        }

        private void IconButtonConfirmClose_Click(object sender, EventArgs e)
        {
            PanelConfirm.Hide();
        }

        private void IconButtonCancel_Click(object sender, EventArgs e)
        {
            PanelConfirm.Hide();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            Owner.ClickOnRefillBalance();
            this.Close();
        }

        private void PanelBlockGateClose_Paint(object sender, PaintEventArgs e)
        {

        }

        //public void ClickOnRefillBalanceClans()
        //{
        //    Owner.ClickOnRefillBalanceChecker();
        //    this.Close();
        //}
    }
}
