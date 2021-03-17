using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;
using FontAwesome.Sharp;
using System.Net;
using System.IO;
using System.IO.Compression;

namespace Asgard
{
    public partial class Login : Form
    {
        private string UsernameOrEmail { set; get; }
        private string Password { set; get; }
        private int Id = 0;
        private string Token { set; get; }
        private string Message { set; get; }

        //private static readonly string[] blackList = new[] { "cmd" };//Black List Applications

        private bool State = true;
        private bool Stop = false;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hwnd, int wmsg, int wparam, int lparam);

        public Login()
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


        private async void IconButtonSigin_Click(object sender, EventArgs e)
        {
            IconButton iconButton = (IconButton)sender;
            iconButton.Enabled = false;
            iconButton.IconColor = Color.Black;
            PictureBoxLoading.Show();
            try
            {
                if (!string.IsNullOrEmpty(TextBoxUsernameOrEmail.Text.Trim()) && !string.IsNullOrEmpty(TextBoxPassword.Text.Trim()))
                {
                    while (State)
                    {
                        UsernameOrEmail = TextBoxUsernameOrEmail.Text.Trim();
                        Password = TextBoxPassword.Text.Trim().CreateMD5();
                        await ConsoleProgress("Validando datos.");
                        Token = await Asatru.GenerateToken(UsernameOrEmail, Password);
                        if (Token != null)
                        {
                            if (Token == "Usuario inactivo.")
                            {
                                await ConsoleProgress(Token, "Fail");
                                Stop = true;
                                break;

                            }
                            if (Token.Contains("incorrecta"))
                            {
                                await ConsoleProgress(Token, "Fail");
                                Stop = true;
                                break;
                            }

                            int id = int.Parse(new JwtSecurityToken(jwtEncodedString: Token).Claims.First(c => c.Type == "id").Value);
                            Id = id;
                            await ConsoleProgress("Creando sesion.");
                            bool createSession = await Asatru.CreateSession(Id, Token);
                            if (createSession)
                            {
                                while (State)
                                {
                                    Stop = true;
                                    this.Hide();
                                    Checker checker = new Checker(Id, Token);
                                    checker.Show();
                                    break;
                                }

                                //if (!Stop)
                                //{
                                //    await Asatru.SetDisUser(id);
                                //}
                            }
                            else
                            {
                                await ConsoleProgress("Error creando sesion.", "Fail");
                            }
                            Stop = true;
                        }
                        else
                        {
                            await ConsoleProgress(Token, "Fail");
                        }
                        break;
                    }
                }
            }
            catch (Exception) { }
            PictureBoxLoading.Hide();
            iconButton.Enabled = true;
            iconButton.IconColor = Color.White;
            await ConsoleProgress("4SG4RD TEAM - 2020");
        }

        private void Login_Paint(object sender, PaintEventArgs e)
        {
            Rectangle borderTextBoxUser = new Rectangle(TextBoxUsernameOrEmail.Location.X, TextBoxUsernameOrEmail.Location.Y, TextBoxUsernameOrEmail.ClientSize.Width, TextBoxUsernameOrEmail.ClientSize.Height);
            borderTextBoxUser.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxUser, Color.White, ButtonBorderStyle.Solid);

            Rectangle borderTextBoxPassword = new Rectangle(TextBoxPassword.Location.X, TextBoxPassword.Location.Y, TextBoxPassword.ClientSize.Width, TextBoxPassword.ClientSize.Height);
            borderTextBoxPassword.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxPassword, Color.White, ButtonBorderStyle.Solid);
        }


        private void IconButtonRegistry_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            this.Hide();
            register.Show();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            //TimerMonitorAplications.Start();
            PictureBoxLoading.Visible = false;
            IconButtonSigin.Enabled = false;
            IconButtonSigin.IconColor = Color.Black;
            /*
             * TODO: comentar!
             */
            TextBoxUsernameOrEmail.Text = "crango.cero";
            TextBoxPassword.Text = "Eli.Chris1915";
        }

        private void TextBoxUser_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.TextLength > 5 && TextBoxPassword.TextLength > 7)
            {
                IconButtonSigin.Enabled = true;
                IconButtonSigin.IconColor = Color.White;
            }
            else
            {
                IconButtonSigin.Enabled = false;
                IconButtonSigin.IconColor = Color.Black;
            }

        }

        private void TextBoxPassword_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.TextLength > 7 && TextBoxUsernameOrEmail.TextLength > 5)
            {
                IconButtonSigin.Enabled = true;
                IconButtonSigin.IconColor = Color.White;
            }
            else
            {
                IconButtonSigin.Enabled = false;
                IconButtonSigin.IconColor = Color.Black;
            }
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

        private void LinkLabelForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Password password = new Password();
            this.Hide();
            password.Show();
        }

        private async void TimerMonitorAplications_Tick(object sender, EventArgs e)
        {
            Process[] procs = Process.GetProcesses();
            foreach (Process p in procs)
            {
                foreach (string bl in Asatru.blackList)
                {
                    if (p.ProcessName == bl)
                    {
                        Yggdrasil.KillCEF();
                        //TimerMonitorAplications.Stop();
                        State = false;
                        if (Id != 0)
                        {
                            await Asatru.SetDisUser(Id, p.ProcessName);
                        }
                        Hermod hermod = new Hermod("Advertencia.", $"{p.ProcessName} es un aplicativo que consideramos sospechoso y puede afectar el funcionamiento de 4SG4RD.", Color.FromArgb(30, 38, 70), Color.White);
                        this.Hide();
                        hermod.ShowDialog();
                        hermod.BringToFront();
                        Application.Exit();
                        this.Close();
                    }
                }

                if (Stop)
                {
                    //TimerMonitorAplications.Stop();
                    Stop = false;
                }
            }
        }
    }
}
