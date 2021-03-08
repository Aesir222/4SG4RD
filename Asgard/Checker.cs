using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using FontAwesome.Sharp;
//using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.IO.Compression;
//using System.Threading;

namespace Asgard
{

    public partial class Checker : Form
    {
        private IconButton currentButton;
        private IconButton currentHoverButton;
        private Panel leftBorderButton;
        private Panel leftBorderHoverButton;
        private Form currentForm;
        private IconButton lastButton;
        private Color lastColor;
        private bool leave;
        private int Id { set; get; }
        private string Token { set; get; }
        // private static readonly string[] blackList = new[] { "cmd" };//Black List Applications
        private bool State = true;
        private bool Stop = false;
        clsResize _form_resize;

        public Checker(int id, string token)
        {

            InitializeComponent();
            ////MessageBox.Show("1");
            Control.CheckForIllegalCrossThreadCalls = false;
            Token = token;
            Id = id;
            leftBorderButton = new Panel();
            leftBorderHoverButton = new Panel();
            leftBorderButton.Size = new Size(8, 40);
            leftBorderHoverButton.Size = new Size(8, 40);
            PanelMenu.Controls.Add(leftBorderButton);
            PanelMenu.Controls.Add(leftBorderHoverButton);
            //this.DoubleBuffered = true;
            //this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            //_form_resize = new clsResize(this);
            //this.Load += _Load;
            //this.Resize += _Resize;
        }

        #region "Form Behaviors"
        private void IconButtonClose_Click(object sender, EventArgs e)
        {
            Yggdrasil.KillCEF();
            Application.Exit();
        }

        private void IconButtonMinus_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion

        #region "Drag Form"
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void PanelHeader_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion

        #region "Animate Menu"

        private struct RGBColors
        {
            public static Color color1 = Color.DarkRed;
            public static Color color2 = Color.FromArgb(255, 127, 0);
            public static Color color3 = Color.FromArgb(227, 184, 20);
            //public static Color color3 = Color.FromArgb(242, 116, 5);
            public static Color color4 = Color.FromArgb(46, 179, 31);
            public static Color color5 = Color.FromArgb(0, 0, 255);
            public static Color color6 = Color.Indigo;
            public static Color color7 = Color.FromArgb(148, 0, 211);
        }

        private void ActivateButton(object senderButton, Color color)
        {
            if (senderButton != null)
            {
                InactivateButton();
                currentButton = (IconButton)senderButton;
                currentButton.BackColor = Color.White;
                currentButton.ForeColor = color;
                currentButton.TextAlign = ContentAlignment.MiddleCenter;
                currentButton.IconColor = color;
                currentButton.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentButton.ImageAlign = ContentAlignment.MiddleRight;
                currentButton.IconColor = color;
                currentButton.Font = new Font(currentButton.Font.Name, currentButton.Font.Size, FontStyle.Bold);

                leftBorderButton.BackColor = color;
                leftBorderButton.Location = new Point(0, currentButton.Location.Y);
                leftBorderButton.Visible = true;
                leftBorderButton.BringToFront();
            }
        }

        private void InactivateButton()
        {
            if (currentButton != null)
            {
                currentButton.BackColor = Color.FromArgb(30, 38, 70);
                currentButton.ForeColor = Color.White;
                currentButton.IconColor = Color.White;
                currentButton.TextAlign = ContentAlignment.MiddleLeft;
                currentButton.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentButton.ImageAlign = ContentAlignment.MiddleLeft;
                currentButton.IconSize = 24;
                currentButton.Font = new Font(currentButton.Font.Name, currentButton.Font.Size, FontStyle.Regular);
            }
        }

        private void OpenForm<MyForm>(params object[] args) where MyForm : Form, new()
        {
            Form form = PanelContent.Controls.OfType<MyForm>().FirstOrDefault();

            if (form == null)
            {
                currentForm = new MyForm();
                ((IForm)currentForm).InitializeParameters(args);
                PanelContent.BringToFront();
                currentForm.TopLevel = false;
                currentForm.Dock = DockStyle.Fill;
                PanelContent.Controls.Add(currentForm);
                PanelContent.Tag = currentForm;
                currentForm.BringToFront();
                currentForm.Show();
            }
            else
            {
                form.BringToFront();
            }
        }


        public async void LoadUserDetails()
        {
            try
            {
                PictureBoxLoadMenu.Show();
                dynamic userDetails = await Asatru.GetUserDetails(Id, Token);
                if (userDetails != null)
                {
                    LabelGetUsername.Text = userDetails.username.Value;
                }
                PictureBoxLoadMenu.Hide();
            }
            catch (Exception) { }
        }

        public async void LoadPlanDetails()
        {
            try
            {
                PictureBoxLoadMenu.Show();
                dynamic planDetails = await Asatru.GetPlanDetails(Id, Token);
                if (planDetails != null)
                {
                    IconButtonClans.Enabled = true;
                    IconButtonClans.IconColor = Color.White;
                    LabelGetRunes.Text = planDetails.total_runes.Value;
                }
                else
                {
                    IconButtonClans.Enabled = false;
                    IconButtonClans.IconColor = Color.Black;
                    LabelGetRunes.Text = "-";
                }
                PictureBoxLoadMenu.Hide();
            }
            catch (Exception) { }
        }

        private void IconButtonHome_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenForm<Home>(Id, Token);
        }

        private void IconButtonAesir_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color2);
            OpenForm<Clans>(Id, Token);
        }

        private void IconButtonBifrost_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);
            OpenForm<Bifrost>(Id, Token);
        }

        private void IconButtonRefillBalance_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
            OpenForm<RefillBalance>(Id, Token);
        }

        private void IconButtonTools_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color5);
            OpenForm<Tools>(Id, Token);
        }

        private void IconButtonOptions_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            OpenForm<Settings>(Id, Token);
        }

        private void IconButtonHelp_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color7);
            OpenForm<Help>(Id, Token);
        }
        #endregion

        private void Checker_Load(object sender, EventArgs e)
        {
            Updater();
            PictureBoxLoadMenu.Hide();
            //TimerMonitorAplications.Start();
            IconButtonClans.Enabled = false;
            IconButtonClans.IconColor = Color.Black;
            Task.Run(() => LoadUserDetails());
            Task.Run(() => LoadPlanDetails());
            ActivateButton(IconButtonHome, RGBColors.color1);
            OpenForm<Home>(Id, Token);
            TimerSync.Start();
        }

        private void IconPictureBoxLogo_Click(object sender, EventArgs e)
        {
            ActivateButton(IconButtonHome, RGBColors.color1);
            OpenForm<Home>(Id, Token);
        }

        private void Reset()
        {
            InactivateButton();
            leftBorderButton.Visible = false;
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
                        await Asatru.SetDisUser(Id, p.ProcessName);
                        Hermod hermod = new Hermod("BAN.", $"{p.ProcessName} es un aplicativo que consideramos sospechoso y puede afectar el funcionamiento de 4SG4RD, Pro esta razon tu usuario ha sido BANEADO");
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

        private void TimerSync_Tick(object sender, EventArgs e)
        {
            Task.Run(() => LoadUserDetails());
            Task.Run(() => LoadPlanDetails());
        }

        private void IconButtonTelegram_Click(object sender, EventArgs e)
        {
            Process.Start("https://t.me/asgardchecker");
        }

        private async void Updater()
        {
            WebClient webClient = new WebClient();
            try
            {
                await webClient.DownloadFileTaskAsync("http://4sg4rd.club/update/loader.zip", @"loader.zip");
                File.Delete(@".\Loader.exe");
                string zipPath = @".\loader.zip";
                string extractPath = @".\";
                ZipFile.ExtractToDirectory(zipPath, extractPath);
                File.Delete(@".\loader.zip");
            }
            catch (Exception)
            {
                File.Delete(@".\loader.zip");
            }
        }

    }
}
