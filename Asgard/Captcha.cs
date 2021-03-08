using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Asgard
{
    public partial class Captcha : Form
    {
        private string Image { set; get; }


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hwnd, int wmsg, int wparam, int lparam);

        public Captcha(string image)
        {
            Image = image;
            InitializeComponent();
        }

        #region "Form Behaviors"
        private void IconButtonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void IconButtonMinus_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion

        #region "Drag Form"
        private void PanelHeader_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        #endregion

        private void IconButtonAccept_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Captcha_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            //LabelTitle.Text = Title;
            //LabelText.Text = Message;

        }
    }
}
