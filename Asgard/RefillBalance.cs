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
    public partial class RefillBalance : Form, IForm
    {
        private int UserId { set; get; }
        private string Token { set; get; }
        private dynamic Voucher { set; get; }

        public RefillBalance()
        {
            InitializeComponent();
        }

        /*
         * TODO: traer info de los planes
         * TODO: Definir nueva tabla para las promos(definir alcance y funcionalidades)
         */
        public void InitializeParameters(params object[] parameters)
        {
            if (parameters.Length == 2)
            {
                UserId = (int)parameters[0];
                Token = parameters[1].ToString();
            }
        }

        private async void IconButtonNorori_Click(object sender, EventArgs e)
        {
            Voucher = await Asatru.RequestPLan(Token, UserId, 1);
            if (Voucher != null)
            {
                LabelPlanNValue.Text = LabelNordri.Text + " " + LabelNordriCost.Text;
                LabelVoucher.Text = Voucher;
                PanelBlockVoucher.Show();
            }
        }

        private async void IconButtonSuori_Click(object sender, EventArgs e)
        {
            Voucher = await Asatru.RequestPLan(Token, UserId, 2);
            if (Voucher != null)
            {
                LabelPlanNValue.Text = LabelSudri.Text + " " + LabelSudriCost.Text;
                LabelVoucher.Text = Voucher;
                PanelBlockVoucher.Show();
            }
        }

        private async void IconButtonAustri_Click(object sender, EventArgs e)
        {
            Voucher = await Asatru.RequestPLan(Token, UserId, 3);
            if (Voucher != null)
            {
                LabelPlanNValue.Text = LabelAustri.Text + " " + LabelAustriCost.Text;
                LabelVoucher.Text = Voucher;
                PanelBlockVoucher.Show();
            }
        }

        private async void IconButtonVestri_Click(object sender, EventArgs e)
        {
            Voucher = await Asatru.RequestPLan(Token, UserId, 4);
            if (Voucher != null)
            {
                LabelPlanNValue.Text = LabelVestri.Text + " " + LabelVestriCost.Text;
                LabelVoucher.Text = Voucher;
                PanelBlockVoucher.Show();
            }
        }

        private void RefillBalance_Load(object sender, EventArgs e)
        {
            PanelBlockVoucher.Hide();
        }

        private void IconButtonCloseVoucher_Click(object sender, EventArgs e)
        {
            Voucher = null;
            LabelVoucher.Text = "-";
            PanelBlockVoucher.Hide();
        }

        private void IconButtonBlockGateClose_Click(object sender, EventArgs e)
        {
            string voucher = "Paga a traves de Bitcoin " + LabelPlanNValue.Text + "\n\r" +
                "ASGARD BITCOIN WALLET: " + LabelWallet.Text + "\n\r" +
                "Envia CAPTURA del pago junto a este Voucher al chat de ventas." + "\n\r" +
                "VOUCHER: " + Voucher;
            Clipboard.SetText(voucher);
        }

        private void LabelVoucher_Click(object sender, EventArgs e)
        {

        }

        private void PanelVoucher_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void PanelBlockVoucher_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
