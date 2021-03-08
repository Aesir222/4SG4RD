using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
//using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Asgard
{
    public partial class Password : Form
    {
        //private string User { set; get; }
        //private string Password { set; get; }

        private string Token { set; get; }
        private string Message { set; get; }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hwnd, int wmsg, int wparam, int lparam);

        public Password()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
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



        private void Password_Paint(object sender, PaintEventArgs e)
        {
            Rectangle borderTextBoxUsernameOrEmail = new Rectangle(TextBoxUsernameOrEmail.Location.X, TextBoxUsernameOrEmail.Location.Y, TextBoxUsernameOrEmail.ClientSize.Width, TextBoxUsernameOrEmail.ClientSize.Height);
            borderTextBoxUsernameOrEmail.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxUsernameOrEmail, Color.White, ButtonBorderStyle.Solid);
        }

        private void Password_Load(object sender, EventArgs e)
        {
            PictureBoxLoading.Visible = false;
            IconButtonGenerateNewPassword.Enabled = false;
            IconButtonGenerateNewPassword.IconColor = Color.Black;
        }

        public async Task ConsoleProgress(string info, string status = null)
        {
            try
            {
                LabelInfo.ForeColor = Color.White;
                int delay = 10;
                if (status == "Success")
                {
                    LabelInfo.ForeColor = Color.FromArgb(17, 97, 238);
                    delay = 3000;
                }
                else if (status == "Fail")
                {
                    LabelInfo.ForeColor = Color.Red;
                    delay = 5000;
                }

                LabelInfo.Text = info;
                await Task.Delay(delay);
            }
            catch (Exception) { }
        }

        private void IconButtonBack_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.Show();

        }

        private void TextBoxUsernameOrEmail_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.TextLength > 5)
            {
                IconButtonGenerateNewPassword.Enabled = true;
                IconButtonGenerateNewPassword.IconColor = Color.White;
            }
            else
            {
                IconButtonGenerateNewPassword.Enabled = false;
                IconButtonGenerateNewPassword.IconColor = Color.Black;
            }
        }

        private async void IconButtonGenerateNewPassword_Click(object sender, EventArgs e)
        {
            string usernameOrEmail = TextBoxUsernameOrEmail.Text;

            if (usernameOrEmail.Length > 5)
            {
                PictureBoxLoading.Visible = true;
                ConsoleProgress("Comprobando.");

                if (await Asatru.GenerateNewPassword(usernameOrEmail)) {
                    ConsoleProgress("Nueva contraseña, envida a su correo.", "Success");
                    TextBoxUsernameOrEmail.Text = string.Empty;
                }
                PictureBoxLoading.Visible = false;
            }
        }

    }
}
