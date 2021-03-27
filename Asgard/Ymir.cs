using Bogus;

using CefSharp;
using CefSharp.OffScreen;

using FontAwesome.Sharp;
using CircularProgressBar;

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
    public partial class Ymir : Form, IForm
    {
        public new IJotun Owner { get; set; }
        //public new IHome Home { set; get; }
        //public new IChecker Checker { set; get; }

        private string Quatity { set; get; }
        private int UserId { set; get; }
        private string Token { set; get; }
        private int GateId { set; get; }

        List<List<string>> CreditCards { set; get; }
        List<List<string>> Items { set; get; }
        List<List<string>> Info { set; get; }


        private static ChromiumWebBrowser browser;
        private static ChromiumWebBrowser browser2;
        private static ChromiumWebBrowser browser3;

        private const string web = "www.manscaped.com";
        private string initialUrl = "https://" + web + "/signup";

        private const string web2 = "www.gmailnator.com";
        private string initialUrl2 = "https://" + web2 + "/";


        private string initialUrl3 = "https://" + web + "/";

        private string PriceItem { set; get; }
        private string NameItem { set; get; }
        private Faker Faker { set; get; }

        private bool forsetiError = false;

        private CancellationTokenSource tokenCancel;
        private CancellationTokenSource tokenCancel2;

        SoundPlayer Valhalla;

        const string SpecialCharacters = @"!%'()*+-./:;<=>[\]_";

        //private string Size;
        private double OriginalCreditCardsCount;
        private bool Stop = false;

        private string Email;
        private string Password;
        private string Address;

        public Ymir()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            Faker = new Faker();
        }
        private void Ymir_Load(object sender, EventArgs e)
        {
            ControlsInit();
        }

        public void InitializeParameters(params object[] parameters)
        {
            if (parameters.Length == 3)
            {
                UserId = (int)parameters[0];
                Token = parameters[1].ToString();
                Owner = (Jotun)parameters[2];
            }
        }

        #region General
        #region Controls Events
        private void IconButtonBack_Click(object sender, EventArgs e)
        {
            Form form = Parent.Controls.OfType<Jotun>().FirstOrDefault();
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
            Owner.HideIconActive("ymir");
            PanelConfirm.Hide();
            if (browser != null)
            {
                await browser.Kill(web);
                await browser2.Kill(web2);
                await KillGod();
            }
            this.Close();
        }
        #endregion

        #region Methods
        private async void ControlsInit()
        {
            Yggdrasil.God = "ymir";
            GateId = 5;

            //LOAD
            //PanelLoad.Hide();
            //BLOCK
            PanelBlockGate.Hide();
            //CLOSE
            PanelConfirm.Hide();

            //SETTINGS
            //Item
            TextBoxItemLink.ContextMenuStrip = new ContextMenuStrip();
            TextBoxItemLink.Placeholder("https://www.manscaped.com/...");
            LabelItemSize.Hide();
            ComboBoxItemSize.Hide();
            TextBoxItemQuantity.MaxLength = 2;
            //IconButtonItemLoad.Enabled = false;
            //IconButtonItemLoad.IconColor = Color.Black;
            PictureBoxItemLoadInfo.Hide();
            //            TextBoxItems.Text = @"https://www.manscaped.com/products/the-performance-package|L
            //https://www.manscaped.com/products/the-performance-package|L";
            //            TextBoxInfo.Text = @"CRISTIAN ANDRES|GOMEZ OSORIO|7705 NW 46th ST|#COCRI10404|Doral|Florida|33166|+1 (305) 717-6595
            //CRISTIAN ANDRES|GOMEZ OSORIO|1695 Market St|#COCRI10404|San Francisco|California|94103|(415) 872-9368";
            //            TextBoxCC.Text = @"5152412101820944|05|2023|040
            //5152412101828202|05|2023|860";

            //Info


            //CONSOLE
            CircularProgressBarGeneral.Minimum = 0;
            CircularProgressBarGeneral.Maximum = 100;
            CircularProgressBarGeneral.Value = 0;

            CircularProgressBarDetail.Value = 0;

            CircularProgressBarMain.Minimum = 0;
            CircularProgressBarMain.Maximum = 100;
            CircularProgressBarMain.Value = 0;

            //JOTUNHEIM
            IconButtonStop.Enabled = false;
            IconButtonStop.IconColor = Color.Black;

            await ConsoleProgressGeneral("AURGELMIR ENTRE LOS GIGANTES.", 0);

            //SOUND
            Valhalla = new SoundPlayer(@"Valhalla.wav");

            await InvokeGod(true);

            PanelMain.Hide();

        }
        #endregion
        #endregion

        #region Checker
        #region Panel Generator
        #region Panel Events
        private void PanelSettings_Paint(object sender, PaintEventArgs e)
        {
            Rectangle borderTextBoxItemLink = new Rectangle(TextBoxItemLink.Location.X, TextBoxItemLink.Location.Y, TextBoxItemLink.ClientSize.Width, TextBoxItemLink.ClientSize.Height);
            borderTextBoxItemLink.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxItemLink, Color.White, ButtonBorderStyle.Solid);

            Rectangle borderTextBoxQuantity = new Rectangle(TextBoxItemQuantity.Location.X, TextBoxItemQuantity.Location.Y, TextBoxItemQuantity.ClientSize.Width, TextBoxItemQuantity.ClientSize.Height);
            borderTextBoxQuantity.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxQuantity, Color.White, ButtonBorderStyle.Solid);

            Rectangle borderTextBoxItems = new Rectangle(TextBoxItems.Location.X, TextBoxItems.Location.Y, TextBoxItems.ClientSize.Width + 17, TextBoxItems.ClientSize.Height + 17);
            borderTextBoxItems.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxItems, Color.White, ButtonBorderStyle.Solid);

            Rectangle borderTextBoxInfoName = new Rectangle(TextBoxInfoName.Location.X, TextBoxInfoName.Location.Y, TextBoxInfoName.ClientSize.Width, TextBoxInfoName.ClientSize.Height);
            borderTextBoxInfoName.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxInfoName, Color.White, ButtonBorderStyle.Solid);

            Rectangle borderTextBoxInfoLastName = new Rectangle(TextBoxInfoLastName.Location.X, TextBoxInfoLastName.Location.Y, TextBoxInfoLastName.ClientSize.Width, TextBoxInfoLastName.ClientSize.Height);
            borderTextBoxInfoLastName.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxInfoLastName, Color.White, ButtonBorderStyle.Solid);

            Rectangle borderTextBoxInfoAddress1 = new Rectangle(TextBoxInfoAddress1.Location.X, TextBoxInfoAddress1.Location.Y, TextBoxInfoAddress1.ClientSize.Width, TextBoxInfoAddress1.ClientSize.Height);
            borderTextBoxInfoAddress1.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxInfoAddress1, Color.White, ButtonBorderStyle.Solid);

            Rectangle borderTextBoxInfoAddress2 = new Rectangle(TextBoxInfoAddress2.Location.X, TextBoxInfoAddress2.Location.Y, TextBoxInfoAddress2.ClientSize.Width, TextBoxInfoAddress2.ClientSize.Height);
            borderTextBoxInfoAddress2.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxInfoAddress2, Color.White, ButtonBorderStyle.Solid);

            Rectangle borderTextBoxInfoCity = new Rectangle(TextBoxInfoCity.Location.X, TextBoxInfoCity.Location.Y, TextBoxInfoCity.ClientSize.Width, TextBoxInfoCity.ClientSize.Height);
            borderTextBoxInfoCity.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxInfoCity, Color.White, ButtonBorderStyle.Solid);

            Rectangle borderTextBoxInfoZip = new Rectangle(TextBoxInfoZip.Location.X, TextBoxInfoZip.Location.Y, TextBoxInfoZip.ClientSize.Width, TextBoxInfoZip.ClientSize.Height);
            borderTextBoxInfoZip.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxInfoZip, Color.White, ButtonBorderStyle.Solid);

            //Rectangle borderTextBoxInfoEmail = new Rectangle(TextBoxInfoEmail.Location.X, TextBoxInfoEmail.Location.Y, TextBoxInfoEmail.ClientSize.Width, TextBoxInfoEmail.ClientSize.Height);
            //borderTextBoxInfoEmail.Inflate(1, 1);
            //ControlPaint.DrawBorder(e.Graphics, borderTextBoxInfoEmail, Color.White, ButtonBorderStyle.Solid);

            Rectangle borderTextBoxInfoPhone = new Rectangle(TextBoxInfoPhone.Location.X, TextBoxInfoPhone.Location.Y, TextBoxInfoPhone.ClientSize.Width, TextBoxInfoPhone.ClientSize.Height);
            borderTextBoxInfoPhone.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxInfoPhone, Color.White, ButtonBorderStyle.Solid);

            Rectangle borderTextBoxInfo = new Rectangle(TextBoxInfo.Location.X, TextBoxInfo.Location.Y, TextBoxInfo.ClientSize.Width + 17, TextBoxInfo.ClientSize.Height + 17);
            borderTextBoxInfo.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxInfo, Color.White, ButtonBorderStyle.Solid);

            Rectangle borderTextBoxCC = new Rectangle(TextBoxCC.Location.X, TextBoxCC.Location.Y, TextBoxCC.ClientSize.Width + 17, TextBoxCC.ClientSize.Height + 17);
            borderTextBoxCC.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxCC, Color.White, ButtonBorderStyle.Solid);
        }
        #endregion
        #region Controls Events
        #region TextBoxBin Events
        private void TextBoxLinkProduct_Enter(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Placeholder("https://www.manscaped.com/...");
        }

        private void TextBoxLinkProduct_Leave(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Placeholder("https://www.manscaped.com/...");
            //Bin = textBox.Text;
            //CompleteBin(textBox);
        }
        private void TextBoxLinkProduct_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Control)
                {
                    e.SuppressKeyPress = false;
                }
                else if (e.KeyCode == Keys.Back)
                {
                    e.SuppressKeyPress = false;
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    e.SuppressKeyPress = false;
                }
                else if (e.KeyCode == Keys.Shift)
                {
                    e.SuppressKeyPress = false;
                }
                else if (e.KeyCode == Keys.Right)
                {
                    e.SuppressKeyPress = false;
                }
                else if (e.KeyCode == Keys.Left)
                {
                    e.SuppressKeyPress = false;
                }
                else if (e.KeyCode == Keys.Home)
                {
                    e.SuppressKeyPress = false;
                }
                else if (e.KeyCode == Keys.End)
                {
                    e.SuppressKeyPress = false;
                }
                else
                {
                    e.SuppressKeyPress = true;
                }
            }
            catch (Exception) { }
        }
        private void TextBoxLinkProduct_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Control)
                {
                    e.SuppressKeyPress = false;
                }
                else if (e.KeyCode == Keys.Back)
                {
                    e.SuppressKeyPress = false;
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    e.SuppressKeyPress = false;
                }
                else if (e.KeyCode == Keys.Shift)
                {
                    e.SuppressKeyPress = false;
                }
                else if (e.KeyCode == Keys.Right)
                {
                    e.SuppressKeyPress = false;
                }
                else if (e.KeyCode == Keys.Left)
                {
                    e.SuppressKeyPress = false;
                }
                else if (e.KeyCode == Keys.Home)
                {
                    e.SuppressKeyPress = false;
                }
                else if (e.KeyCode == Keys.End)
                {
                    e.SuppressKeyPress = false;
                }
                else
                {
                    e.SuppressKeyPress = true;
                }

            }
            catch (Exception) { }
        }

        //private void TextBoxBin_TextChanged(object sender, EventArgs e)
        //{
        //    TextBox textBox = (TextBox)sender;
        //    Bin = textBox.Text;
        //    if (ModifierKeys.HasFlag(Keys.Control))
        //    {
        //        int textLenght = textBox.TextLength;
        //        for (int i = 0; i < textLenght; i++)
        //        {
        //            if (i < 6)
        //            {
        //                if (Regex.IsMatch(textBox.Text.Substring(i, 1), "[^0-9]"))
        //                {
        //                    textBox.Clear();
        //                    break;
        //                }
        //            }
        //            else if (Regex.IsMatch(textBox.Text.Substring(i, 1), @"[^x-xX-X0-9]"))
        //            {
        //                textBox.Clear();
        //                break;
        //            }
        //        }
        //        CompleteBin(textBox);
        //        Bin = textBox.Text;
        //        Task.Run(() => InfoBinList(Bin));
        //    }
        //}

        private void TextBoxLinkProduct_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string text = textBox.Text;
            if (text == "https://www.manscaped.com/...")
            {
                text = string.Empty;
            }

            //if (string.IsNullOrEmpty(text))
            //{
            //    //IconButtonGenerar.IconColor = Color.Black;
            //    //IconButtonGenerar.Enabled = false;
            //}
            //else
            //{
            //    if (text.Length < 6)
            //    {
            //        //IconButtonGenerar.IconColor = Color.Black;
            //        //IconButtonGenerar.Enabled = false;
            //    }
            //    else
            //    {
            //        if (!ValidateBin(text))
            //        {
            //            //IconButtonGenerar.IconColor = Color.Black;
            //            //IconButtonGenerar.Enabled = false;
            //        }
            //        else
            //        {
            //            //IconButtonGenerar.IconColor = Color.White;
            //            //IconButtonGenerar.Enabled = true;
            //            //IconButtonClear.IconColor = Color.White;
            //            //IconButtonClear.Enabled = true;

            //        }
            //    }
            //}
        }
        #endregion
        #region MaskedBoxDate Events
        private void MaskedTextBoxDate_Enter(object sender, EventArgs e)
        {
            MaskedTextBox maskedTextBox = (MaskedTextBox)sender;
            maskedTextBox.Placeholder("dd/aa");
            //MaskedTextBoxDate.Mask = "##/##";
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
                //MaskedTextBoxDate.Mask = "";
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
            //textBox.Placeholder(cvvDigits);
        }
        private void TextBoxCvv_Leave(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            //if (textBox.TextLength < cvvDigits.Length)
            //{
            //    textBox.Clear();
            //}
            //textBox.Placeholder(cvvDigits);
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
                    //IconButtonVerify.IconColor = Color.Black;
                    //IconButtonVerify.Enabled = false;
                    //IconButtonStop.IconColor = Color.Black;
                    //IconButtonStop.Enabled = false;
                    //IconButtonClear.IconColor = Color.Black;
                    //IconButtonClear.Enabled = false;
                    //IconButtonValkyrie.IconColor = Color.Black;
                    //IconButtonValkyrie.Enabled = false;
                    //IconButtonGenerarStop.Show();
                    //IconButtonGenerarClick = true;
                    await ConsoleProgressGeneral("Iniciando Motor del Generador.", 0);
                    await GenerateCreditCards();
                    await ConsoleProgressGeneral("Material de pago generado correctamente.", 100, "Success");
                    await ConsoleProgressGeneral("AURGELMIR ENTRE LOS GIGANTES..", 0);
                    //IconButtonGenerarStop.Hide();
                    iconButton.IconColor = Color.White;
                    iconButton.Enabled = true;
                    //IconButtonVerify.IconColor = Color.White;
                    //IconButtonVerify.Enabled = true;
                    //IconButtonClear.IconColor = Color.White;
                    //IconButtonClear.Enabled = true;
                    //IconButtonValkyrie.IconColor = Color.White;
                    //IconButtonValkyrie.Enabled = true;
                    //IconButtonGenerarClick = false;
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
                //IconButtonGenerarStopClick = true;
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
                        //cvvDigits = "####";
                        //TextBoxCvv.MaxLength = 4;
                    }
                    else
                    {
                        //cvvDigits = "###";
                        //TextBoxCvv.MaxLength = 3;
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
                    //cvvDigits = "###";
                    //TextBoxCvv.MaxLength = 3;
                }

                textBox.Select(textBox.TextLength, 0);
                //TextBoxCvv.ForeColor = Color.Silver;
                //TextBoxCvv.Text = cvvDigits;
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
            //if (ShowPanelValkyrie)
            //{
            //    //IconButtonGenerar.IconColor = IconButtonGenerarIconColor;
            //    //IconButtonGenerar.Enabled = IconButtonGenerarEnabled;
            //    PanelValkyrie.Hide();
            //    //PanelValkyrieLive.Hide();
            //    //PanelValkyrieDie.Hide();
            //    ShowPanelValkyrie = false;
            //    await ConsoleProgressGeneral("AURGELMIR ENTRE LOS GIGANTES..", 0);
            //    await ConsoleProgressDetail("AURGELMIR ENTRE LOS GIGANTES..", 0);
            //}
            //else
            //{
            //    //IconButtonGenerarIconColor = IconButtonGenerar.IconColor;
            //    //IconButtonGenerarEnabled = IconButtonGenerar.Enabled;

            //    if (IconButtonGenerarEnabled)
            //    {
            //        //IconButtonGenerar.IconColor = Color.Black;
            //        //IconButtonGenerar.Enabled = false;
            //    }
            //    PanelValkyrie.Show();
            //    //PanelValkyrieLive.Show();
            //    //PanelValkyrieDie.Show();
            //    ShowPanelValkyrie = true;
            //    await ConsoleProgressGeneral("VALKIRIAS (PROXYS)", 0);
            //    await ConsoleProgressDetail("VALKIRIAS (PROXYS)", 0);
            //}
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

                        //pictureBox1.Show();
                        //pictureBox2.Show();
                        //pictureBox3.Show();
                        //pictureBox4.Show();
                        //pictureBox5.Show();
                        //pictureBox6.Show();


                        dynamic binList = await Asatru.InfoBin(bin);
                        if (binList != null)
                        {
                            //pictureBox1.Hide();
                            //pictureBox2.Hide();
                            //pictureBox3.Hide();
                            //pictureBox4.Hide();
                            //pictureBox5.Hide();
                            //pictureBox6.Hide();
                            //LabelGetBin.Text = bin;
                            //LabelGetSchema.Text = binList.scheme ?? "-";
                            //LabelGetType.Text = binList.type ?? "-";
                            //LabelGetBrand.Text = binList.brand ?? "-";
                            //LabelGetBank.Text = binList.bank.name ?? "-";
                            //LabelGetCountry.Text = binList.country.name ?? "-";
                        }
                        else
                        {
                            //pictureBox1.Hide();
                            //pictureBox2.Hide();
                            //pictureBox3.Hide();
                            //pictureBox4.Hide();
                            //pictureBox5.Hide();
                            //pictureBox6.Hide();
                            //LabelGetBin.Text = bin;
                            //LabelGetSchema.Text = "-";
                            //LabelGetType.Text = "-";
                            //LabelGetBrand.Text = "-";
                            //LabelGetBank.Text = "-";
                            //LabelGetCountry.Text = "-";
                        }
                    }
                    else
                    {
                        //pictureBox1.Hide();
                        //pictureBox2.Hide();
                        //pictureBox3.Hide();
                        //pictureBox4.Hide();
                        //pictureBox5.Hide();
                        //pictureBox6.Hide();
                        //LabelGetBin.Text = bin;
                        //LabelGetSchema.Text = "-";
                        //LabelGetType.Text = "-";
                        //LabelGetBrand.Text = "-";
                        //LabelGetBank.Text = "-";
                        //LabelGetCountry.Text = "-";
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
            Rectangle borderTextBoxCC = new Rectangle(TextBoxItems.Location.X, TextBoxItems.Location.Y, TextBoxItems.ClientSize.Width + 17, TextBoxItems.ClientSize.Height);
            borderTextBoxCC.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxCC, Color.White, ButtonBorderStyle.Solid);
        }
        #endregion
        #region Controls Events
        private async void TextBoxCC_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            try
            {
                LabelCountCC.Text = textBox.Lines.Count().ToString();
                string creditCards = textBox.Text.Trim();
            }
            catch (Exception) { }
        }


        private async Task<bool> UpCreditCards()
        {
            string creditCards = TextBoxCC.Text.Trim();
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
                //IconButtonGenerarIconColor = IconButtonGenerar.IconColor;
                //IconButtonGenerarEnabled = IconButtonGenerar.Enabled;
                IconButton iconButton = (IconButton)sender;
                iconButton.IconColor = Color.Black;
                iconButton.Enabled = false;
                //IconButtonGenerar.IconColor = Color.Black;
                //IconButtonGenerar.Enabled = false;
                //IconButtonClear.IconColor = Color.Black;
                //IconButtonClear.Enabled = false;
                //IconButtonValkyrie.IconColor = Color.Black;
                //IconButtonValkyrie.Enabled = false;
                //IconButtonStop.IconColor = Color.White;
                //IconButtonStop.Enabled = true;
                //running = true;

                //await InvokeYmir();
                await ConsoleProgressGeneral("Ymir finalizo Verificación.", 100, "Success");
                await ConsoleProgressDetail("Ymir finalizo Verificación.", 100, "Success");
                await ConsoleProgressGeneral("AURGELMIR ENTRE LOS GIGANTES..", 0);
                await ConsoleProgressDetail("AURGELMIR ENTRE LOS GIGANTES..", 0);
                //IconButtonClear.IconColor = Color.White;
                //IconButtonClear.Enabled = true;
                //IconButtonValkyrie.IconColor = Color.White;
                //IconButtonValkyrie.Enabled = true;
            }
            catch (Exception) { }
        }
        #endregion
        #endregion
        #region Panel Valhalla
        #region Panel Events
        private void PanelValhalla_Paint(object sender, PaintEventArgs e)
        {
            Rectangle borderTextBoxValhalla = new Rectangle(TextBoxValhalla.Location.X, TextBoxValhalla.Location.Y, TextBoxValhalla.ClientSize.Width + 17, TextBoxValhalla.ClientSize.Height + 17);
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
                await browser.Kill(web);
                await browser2.Kill(web2);

                await ConsoleProgressGeneral("Ymir esta siendo Detenido.", 0, "Success");
                await ConsoleProgressDetail("Ymir esta siendo Detenido.", 0, "Success");
                await ConsoleProgressGeneral("AURGELMIR ENTRE LOS GIGANTES..", 0);
                await ConsoleProgressDetail("AURGELMIR ENTRE LOS GIGANTES..", 0);

                IconButtonStart.Enabled = true;
                IconButtonStart.IconColor = Color.White;
                iconButton4.Enabled = true;
                iconButton4.IconColor = Color.White;
                Stop = true;

            }
            catch (Exception) { }
        }
        #endregion
        #endregion
        #region Panel Hellheim
        #region Panel Events
        private void PanelHellheim_Paint(object sender, PaintEventArgs e)
        {
            //Rectangle borderTextBoxHelheim = new Rectangle(TextBoxHelheim.Location.X, TextBoxHelheim.Location.Y, TextBoxHelheim.ClientSize.Width + 17, TextBoxHelheim.ClientSize.Height);
            //borderTextBoxHelheim.Inflate(1, 1); // border thickness
            //ControlPaint.DrawBorder(e.Graphics, borderTextBoxHelheim, Color.White, ButtonBorderStyle.Solid);
        }
        #endregion
        #region Contols Events
        private void IconButtonClear_Click(object sender, EventArgs e)
        {
            IconButton iconButton = (IconButton)sender;
            iconButton.IconColor = Color.Black;
            iconButton.Enabled = false;
            TextBoxItemLink.Clear();
            TextBoxItemLink.Placeholder("######xxxxxxxxxx");
            //MaskedTextBoxDate.Clear();
            //MaskedTextBoxDate.Placeholder("dd/aa");
            //TextBoxCvv.Clear();
            //TextBoxCvv.Placeholder(cvvDigits);
            TextBoxItemQuantity.Clear();
            TextBoxItemQuantity.Text = "10";
            TextBoxItems.Text = string.Empty;
            TextBoxValhalla.Text = string.Empty;
            //TextBoxHelheim.Text = string.Empty;
            //IconButtonGenerar.IconColor = Color.Black;
            //IconButtonGenerar.Enabled = false;
            //IconButtonVerify.IconColor = Color.Black;
            //IconButtonVerify.Enabled = false;
            //LabelGetBin.Text = "-";
            //LabelGetSchema.Text = "-";
            //LabelGetType.Text = "-";
            //LabelGetBrand.Text = "-";
            //LabelGetBank.Text = "-";
            //LabelGetCountry.Text = "-";
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
                     string bin = TextBoxItemLink.Text.Replace("x", string.Empty).Replace("X", string.Empty);
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
                             Quatity = TextBoxItemQuantity.Text;
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
                                 //if (IconButtonGenerarStopClick)
                                 //{
                                 //    IconButtonGenerarStopClick = false;
                                 //    await ConsoleProgressGeneral("¡Detenido!", 0, "Success");
                                 //    break;
                                 //}
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
                     TextBoxItems.Text = listCreditCard;
                 }
             }, token);
        }

        private static RangeAndLength[] AllCards()
        {
            //return BuildRangeAndLengthList(rangeAmex, 15).
            //    Union(BuildRangeAndLengthList(rangeCUP, 16)).
            //    Union(BuildRangeAndLengthList(rangeDCCB, 14)).
            //    Union(BuildRangeAndLengthList(rangeDCI, 14)).
            //    Union(BuildRangeAndLengthList(rangeDCUSC, 16)).
            //    Union(BuildRangeAndLengthList(rangeDC, 14)).
            //    Union(BuildRangeAndLengthList(rangeInsP, 16)).
            //    Union(BuildRangeAndLengthList(rangeIntP, 16)).
            //    Union(BuildRangeAndLengthList(rangeJCB, 16)).
            //    Union(BuildRangeAndLengthList(rangeM, 16)).
            //    Union(BuildRangeAndLengthList(rangeMC, 16)).
            //    Union(BuildRangeAndLengthList(rangeUATP, 15)).
            //    Union(BuildRangeAndLengthList(rangeV, 16)).
            //    ToArray();
            return null;
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
            //if (MaskedTextBoxDate.Text != "dd/aa")
            //{
            //    string[] split = MaskedTextBoxDate.Text.Split('/');
            //    date.Add(split[0]);
            //    date.Add("20" + split[1]);
            //}
            //else
            //{
            //    date.Add(new Random().Next(1, 12).ToString("00"));
            //    date.Add("20" + new Random().Next(20, 29).ToString());
            //}
            return date;
        }
        private string GetCvv()
        {
            string cvv = "";
            //if (TextBoxCvv.Text == cvvDigits)
            //{
            //    if (TextBoxCvv.MaxLength == 4)
            //    {
            //        cvv = new Random().Next(1, 9999).ToString("0000");
            //    }
            //    else
            //    {
            //        cvv = new Random().Next(1, 999).ToString("000");
            //    }
            //}
            //else
            //{
            //    cvv = TextBoxCvv.Text;
            //}
            return cvv;
        }

        #endregion
        #endregion

        #region Valkyrie
        #region Panel Valkyrie
        #region Panel Events
        private void PanelValkyrie_Paint(object sender, PaintEventArgs e)
        {
            //Rectangle borderTextBoxValkyrie = new Rectangle(TextBoxValkyrie.Location.X, TextBoxValkyrie.Location.Y, TextBoxValkyrie.ClientSize.Width + 17, TextBoxValkyrie.ClientSize.Height);
            //borderTextBoxValkyrie.Inflate(1, 1); // border thickness
            //ControlPaint.DrawBorder(e.Graphics, borderTextBoxValkyrie, Color.White, ButtonBorderStyle.Solid);
        }
        #endregion
        #region Contols Events
        private void TextBoxValkyrie_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            try
            {
                //    LabelCountValkyrie.Text = textBox.Lines.Count().ToString();
                //    string valkyries = textBox.Text.Trim();

                //    if (ModifierKeys.HasFlag(Keys.Control))
                //    {
                //        if (valkyries.Contains("\r\n"))
                //        {
                //            string[] listValkyries = valkyries.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                //            int listValkyriesLength = listValkyries.Length;
                //            Valkyries = new List<List<string>>();
                //            for (int i = 0; i < listValkyriesLength; i++)
                //            {
                //                string[] listValkyrie = listValkyries[i].Split('|');
                //                Valkyries.Add(new List<string>());
                //                Valkyries[i].Add(listValkyrie[0]);
                //                Valkyries[i].Add(listValkyrie[1]);
                //                Valkyries[i].Add(listValkyrie[2]);
                //            }

                //            if (!IconButtonValkyrieVerifyClick)
                //            {
                //                IconButtonValkyrieVerify.IconColor = Color.White;
                //                IconButtonValkyrieVerify.Enabled = true;
                //                //IconButtonValkyrieClear.IconColor = Color.White;
                //                //IconButtonValkyrieClear.Enabled = true;
                //            }
                //        }
                //        else if (valkyries.Contains("|"))
                //        {
                //            Valkyries = new List<List<string>>();
                //            string[] listValkyrie = valkyries.Split('|');
                //            Valkyries.Add(new List<string>());
                //            Valkyries[0].Add(listValkyrie[0]);
                //            Valkyries[0].Add(listValkyrie[1]);
                //            Valkyries[0].Add(listValkyrie[2]);
                //            if (!IconButtonValkyrieVerifyClick)
                //            {
                //                IconButtonValkyrieVerify.IconColor = Color.White;
                //                IconButtonValkyrieVerify.Enabled = true;
                //                //IconButtonValkyrieClear.IconColor = Color.White;
                //                //IconButtonValkyrieClear.Enabled = true;
                //            }
                //        }
                //        else
                //        {
                //            textBox.Text = string.Empty;
                //        }
                //    }
                //    else
                //    {
                //        if (valkyries == string.Empty)
                //        {
                //            textBox.Text = string.Empty;
                //            //IconButtonVerify.IconColor = Color.Black;
                //            //IconButtonVerify.Enabled = false;
                //            //if (TextBoxValkyrieLive.Text == string.Empty && TextBoxValkyrieDie.Text == string.Empty)
                //            //{
                //            //    IconButtonClear.IconColor = Color.Black;
                //            //    IconButtonClear.Enabled = false;
                //            //}
                //        }
                //    }
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
            //IconButtonValkyrieStart.IconColor = Color.Black;
            //IconButtonValkyrieStart.Enabled = false;
            //IconButtonValkyrieClear.IconColor = Color.Black;
            //IconButtonValkyrieClear.Enabled = false;
            //IconButtonValkyrie.IconColor = Color.Black;
            //IconButtonValkyrie.Enabled = false;
            //IconButtonValkyrieVerifyClick = true;
            //IconButtonValkyrieStop.Enabled = true;
            //IconButtonValkyrieStop.IconColor = Color.White;
            //IconButtonValkyrieStop.Show();
            await InvokeValkyrie();
            await ConsoleProgressGeneral("Finalizo llamado a Valquirias.", 100, "Success");
            await ConsoleProgressDetail("Finalizo llamado a Valquirias.", 100, "Success");
            await ConsoleProgressGeneral("VALQUIRIAS (PROXYS).", 0);
            await ConsoleProgressDetail("VALQUIRIAS (PROXYS).", 0);
            //IconButtonValkyrieClear.IconColor = Color.White;
            //IconButtonValkyrieClear.Enabled = true;
            //if (!string.IsNullOrEmpty(TextBoxValkyrieLive.Text))
            {
                //if (TextBoxValkyrieLive.Lines.Count() > 0)
                //{
                //    IconButtonValkyrieStart.IconColor = Color.White;
                //    IconButtonValkyrieStart.Enabled = true;
                //    IconButtonValkyrie.IconColor = Color.White;
                //    IconButtonValkyrie.Enabled = true;
                //}
            }
            //IconButtonValkyrieVerifyClick = false;
            //IconButtonValkyrieStop.Hide();
        }
        #endregion
        #endregion
        #region Panel Valkyrie Live
        #region Panel events
        private void PanelValkyrieLive_Paint(object sender, PaintEventArgs e)
        {
            //Rectangle borderTextBoxValkyrieLive = new Rectangle(TextBoxValkyrieLive.Location.X, TextBoxValkyrieLive.Location.Y, TextBoxValkyrieLive.ClientSize.Width + 17, TextBoxValkyrieLive.ClientSize.Height);
            //borderTextBoxValkyrieLive.Inflate(1, 1); // border thickness
            //ControlPaint.DrawBorder(e.Graphics, borderTextBoxValkyrieLive, Color.White, ButtonBorderStyle.Solid);
        }
        #endregion
        #region Controls Events
        private void TextBoxValkyrieLive_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            //LabelCountValkyrieLive.Text = textBox.Lines.Count().ToString();
            //Valkyrie.Play();
        }

        private void IconButtonValkyrieStart_Click(object sender, EventArgs e)
        {
            try
            {
                IconButton iconButton = (IconButton)sender;
                iconButton.IconColor = Color.Black;
                iconButton.Enabled = false;
                //string valkyries = TextBoxValkyrieLive.Text.Trim();
                //if (valkyries.Contains("\r\n"))
                //{
                //    string[] listValkyries = valkyries.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                //    int listValkyriesLength = listValkyries.Length;
                //    LiveValkyries = new List<List<string>>();
                //    for (int i = 0; i < listValkyriesLength; i++)
                //    {
                //        string[] listValkyrie = listValkyries[i].Split('|');
                //        LiveValkyries.Add(new List<string>());
                //        LiveValkyries[i].Add(listValkyrie[0] + "://" + listValkyrie[1] + ":" + listValkyrie[2]);
                //    }
                //}
                //else if (valkyries.Contains("|"))
                //{
                //    LiveValkyries = new List<List<string>>();
                //    string[] listValkyrie = valkyries.Split('|');
                //    LiveValkyries.Add(new List<string>());
                //    LiveValkyries[0].Add(listValkyrie[0] + "://" + listValkyrie[1] + ":" + listValkyrie[2]);
                //}

            }
            catch (Exception) { }
            //IconButtonGenerar.IconColor = IconButtonGenerarIconColor;
            //IconButtonGenerar.Enabled = IconButtonGenerarEnabled;
            //PanelValkyrie.Hide();
            //PanelValkyrieLive.Hide();
            //PanelValkyrieDie.Hide();
            //ShowPanelValkyrie = false;
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

                //IconButtonValkyrieClear.IconColor = Color.White;
                //IconButtonValkyrieClear.Enabled = true;
                //IconButtonValkyrie.IconColor = Color.White;
                //IconButtonValkyrie.Enabled = true;
                //if (Valkyries.Count > 0)
                //{
                //    IconButtonValkyrieVerify.IconColor = Color.White;
                //    IconButtonValkyrieVerify.Enabled = true;
                //}

                //if (!string.IsNullOrEmpty(TextBoxValkyrieLive.Text))
                //{
                //    if (TextBoxValkyrieLive.Lines.Count() > 0)
                //    {
                //        IconButtonValkyrieStart.IconColor = Color.White;
                //        IconButtonValkyrieStart.Enabled = true;
                //        IconButtonValkyrie.IconColor = Color.White;
                //        IconButtonValkyrie.Enabled = true;
                //    }
                //}
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
            //Rectangle borderTextBoxValkyrieDie = new Rectangle(TextBoxValkyrieDie.Location.X, TextBoxValkyrieDie.Location.Y, TextBoxValkyrieDie.ClientSize.Width + 17, TextBoxValkyrieDie.ClientSize.Height);
            //borderTextBoxValkyrieDie.Inflate(1, 1); // border thickness
            //ControlPaint.DrawBorder(e.Graphics, borderTextBoxValkyrieDie, Color.White, ButtonBorderStyle.Solid);
        }
        #endregion
        #region Controls Events
        private void TextBoxValkyrieDie_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            //LabelCountValkyrieDie.Text = textBox.Lines.Count().ToString();
        }
        private void IconButtonValkyrieClear_Click(object sender, EventArgs e)
        {
            IconButton iconButton = (IconButton)sender;

            iconButton.IconColor = Color.Black;
            iconButton.Enabled = false;
            //IconButtonValkyrieVerify.IconColor = Color.Black;
            //IconButtonValkyrieVerify.Enabled = false;
            //IconButtonValkyrieStart.IconColor = Color.Black;
            //IconButtonValkyrieStart.Enabled = false;

            //TextBoxValkyrie.Text = string.Empty;
            //TextBoxValkyrieLive.Text = string.Empty;
            //TextBoxValkyrieDie.Text = string.Empty;

            //if (Valkyries != null && Valkyries.Count > 0)
            //{
            //    Valkyries.Clear();
            //}

            //if (LiveValkyries != null && LiveValkyries.Count > 0)
            //{
            //    LiveValkyries.Clear();
            //}
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
                //int valkyriesCount = Valkyries.Count;
                await ConsoleProgressGeneral("Ymir hace llamado a Valquirias.", 0);
                int x = 0;
                //for (int i = 0; i < valkyriesCount;)
                //{
                //    //string proxy = Valkyries[i][0] + "://" + Valkyries[i][1] + ":" + Valkyries[i][2];
                //    bool ymirVerifyValkyrie = await YmirVerifyValkyrie(proxy);
                //    if (ymirVerifyValkyrie)
                //    {
                //        //if (TextBoxValkyrieLive.Text == string.Empty)
                //        //{
                //        //    listValkyrieLive += string.Join("|", Valkyries[i].ToArray());
                //        //}
                //        //else
                //        //{
                //        //    listValkyrieLive = TextBoxValkyrieLive.Text;
                //        //    listValkyrieLive += "\r\n" + string.Join("|", Valkyries[i].ToArray());
                //        //}
                //        await ReduceListValkyrie();
                //        //TextBoxValkyrieLive.Text = listValkyrieLive;
                //        double progress = Convert.ToDouble((x + 1) * 100 / valkyriesCount);
                //        await ConsoleProgressGeneral("Ymir llamando a Valquirias.", (int)Math.Round(progress));
                //    }
                //    else
                //    {
                //        //if (TextBoxValkyrieDie.Text == string.Empty)
                //        //{
                //        //    listValkyrieDie += string.Join("|", Valkyries[i].ToArray());
                //        //}
                //        //else
                //        //{
                //        //    listValkyrieDie = TextBoxValkyrieDie.Text;
                //        //    listValkyrieDie += "\r\n" + string.Join("|", Valkyries[i].ToArray());
                //        //}
                //        //await ReduceListValkyrie();
                //        //TextBoxValkyrieDie.Text = listValkyrieDie;
                //        //double progress = Convert.ToDouble((x + 1) * 100 / valkyriesCount);
                //        //await ConsoleProgressGeneral("Ymir llamando a Valquirias.", (int)Math.Round(progress));
                //    }
                //    await Task.Delay(50);
                //    x++;

                //}
            }
            catch (Exception) { }
        }

        private async Task<bool> YmirVerifyValkyrie(string proxy)
        {
            bool ymir = false;
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
                            bool startYmirValkyrie = await StartYmirValkyrie();
                            if (startYmirValkyrie)
                            {
                                await ConsoleProgressDetail("Valquiria. " + proxy, 100, "Success");
                                ymir = true;
                            }
                            else
                            {
                                await ConsoleProgressDetail("Valquiria. " + proxy, 100, "Fail");
                                ymir = false;
                            }
                        }
                        else
                        {
                            await YmirVerifyValkyrie(proxy);
                        }
                    }
                    else
                    {
                        ymir = false;
                    }
                }, token);
            }
            catch (Exception) { }
            return ymir;
        }

        private async Task<bool> StartYmirValkyrie()
        {
            try
            {
                await ConsoleProgressDetail("Estado de Valquiria.", 80);
                if (await browser.ElementVisible("#dropSortBy", "ExceptionStartYmirValkyrie"))
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
                //Valkyries.RemoveAt(0);
                //string listValkyries = string.Empty;
                //int ValkyriesCount = Valkyries.Count;
                //for (int i = 0; i < ValkyriesCount; i++)
                //{
                //    if (Valkyries.Count > 0)
                //    {
                //        if (i < Valkyries.Count - 1)
                //        {
                //            listValkyries += string.Join("|", Valkyries[i].ToArray()) + "\r\n";
                //        }
                //        else
                //        {
                //            listValkyries += string.Join("|", Valkyries[i].ToArray());
                //        }
                //    }
                //    else
                //    {
                //        listValkyries = string.Empty;
                //    }
                //    await Task.Delay(50);
                //}
                //TextBoxValkyrie.Text = listValkyries;
            }

            catch (Exception) { }
        }
        #endregion
        #endregion

        #region Web Browser
        #region Load Browser
        private async Task InvokeYmir(bool persistentLoading = false)
        {
            bool ymir = false;
            try
            {
                await ConsoleProgressGeneral("Invocando a Ymir.", 0);

                browser = new ChromiumWebBrowser();
                browser2 = new ChromiumWebBrowser();
                await Task.Delay(500);

                tokenCancel = new CancellationTokenSource();
                CancellationToken token = tokenCancel.Token;

                ymir = await Task<bool>.Run(async () =>
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

            //await browser.Kill(web);
            //if (!persistentLoading)
            //{
            await browser.Kill(web);
            await browser2.Kill(web2);
            await tokenCancel.Kill();
            //}

            if (ymir)
            {
                await ConsoleProgressGeneral("Ymir finalizo los procesos correctamente.", 100, "Success");
                //IconButtonStart.Enabled = true;
                //IconButtonStart.IconColor = Color.White;
                //iconButton4.IconColor = Color.White;
                //iconButton4.Enabled = true;
                //IconButtonStop.Enabled = false;
                //IconButtonStop.IconColor = Color.Black;
                //IconButtonValkyrie.IconColor = Color.White;
                //IconButtonValkyrie.Enabled = true;
            }
            else
            {
                if (CreditCards.Count > 0 && Items.Count > 0)
                {
                    if (!Stop)
                    {
                        await ConsoleProgressGeneral("Ymir requiere un sacrificio.", 0, "Fail");
                        await ConsoleProgressDetail("", 0);
                        await ConsoleProgressGeneral("Ofreciendo un sacrificio a Ymir.", 0, "Success");
                        await InvokeYmir();
                    }
                    else
                    {
                        await Task.Delay(2000);
                        await ConsoleProgressDetail("", 0);
                        await ConsoleProgressGeneral("AURGELMIR ENTRE LOS GIGANTES.", 0);
                        Stop = false;
                    }
                }
                else
                {
                    await ConsoleProgressGeneral("Ymir encontro un elfo oscuro y se detuvo.", 0, "Fail");
                    //IconButtonStart.Enabled = true;
                    //IconButtonStart.IconColor = Color.White;
                    //iconButton4.IconColor = Color.White;
                    //iconButton4.Enabled = true;
                    //IconButtonStop.Enabled = false;
                    //IconButtonStop.IconColor = Color.Black;
                }
            }
            await Task.Delay(2000);
            await ConsoleProgressDetail("", 0);
            await ConsoleProgressGeneral("AURGELMIR ENTRE LOS GIGANTES.", 0);
            IconButtonStart.Enabled = true;
            IconButtonStart.IconColor = Color.White;
            iconButton4.IconColor = Color.White;
            iconButton4.Enabled = true;
            IconButtonStop.Enabled = false;
            IconButtonStop.IconColor = Color.Black;

        }

        private async Task<bool> LoadBrowser()
        {
            bool loadBrowser = false;
            try
            {
                await ConsoleProgressGeneral("Invocando a Ymir..", 2);
                Task<bool> task1 = browser.LoadPage(initialUrl);
                Task<bool> task2 = browser2.LoadPage(initialUrl2);

                await Task.WhenAll(task1, task2);
                bool task1Result = task1.Result; // or await task1
                bool task2Result = task2.Result; // or await task2
                loadBrowser = task1Result && task2Result; // or await task1
            }
            catch (Exception) { }

            await Task.Delay(500);
            if (loadBrowser)
            {
                return await LoadPage();
            }

            await ConsoleProgressGeneral("Invocando a Ymir.. ¡Fallo!", 2, "Fail");
            return false;

        }

        private async Task<bool> LoadPage()
        {
            try
            {
                await ConsoleProgressGeneral("Invocando a Ymir...", 3);
                //return true;
                if (await EmailGenerator())
                {
                    return await StartYmir();
                }
            }
            catch (Exception) { throw; }

            await ConsoleProgressGeneral("Invocando a Ymir... ¡Fallo!", 0, "Fail");
            return false;

        }
        #endregion

        private async Task<bool> EmailGenerator()
        {
            try
            {
                await browser2.Screenshot("1.EmailGenerator");
                await ConsoleProgressGeneral("Reequilibrando coeficientes automovilísticos..", 5);
                await browser2.DisableAlerts();
            }
            catch (Exception) { }

            bool buttonGo = await browser2.ElementVisible("#button_go", "1.ExceptionEmailGenerator");
            if (buttonGo)
            {
                return await Go();
            }
            await ConsoleProgressGeneral("Reequilibrando coeficientes automovilísticos. ¡Fallo!", 5, "Fail");
            return false;
        }

        private async Task<bool> Go()
        {
            string email = string.Empty;
            try
            {
                await browser2.Screenshot("2.Go");
                await ConsoleProgressGeneral("Creando red social.", 10);
                await browser2.DisableAlerts();
                email = (string)await browser2.ExecuteScript("document.querySelector('#email_address').value;");
                await browser2.ExecuteScript("document.querySelector('#button_go').click();");
                //await browser.Scroll(500);
            }
            catch (Exception) { }

            bool divEmail = await browser2.ElementInnerTextEquals("#email_address", email, "2.ExceptionGo");
            if (divEmail)
            {
                Email = email;
                return true;
            }
            await ConsoleProgressGeneral("Creando red social. ¡Fallo!", 10, "Fail");
            return false;
        }

        #region  Gate Step By Step
        private async Task<bool> StartYmir()
        {
            try
            {
                await browser.Screenshot("1.StartYmir");
                await ConsoleProgressGeneral("Simulando tercera dimensión.", 15);
                await browser.DisableAlerts();
            }
            catch (Exception) { }

            bool inputCreateAccount = await browser.ElementVisible("input[name=email]", "1.ExceptionStartYmir");
            if (inputCreateAccount)
            {
                return await CreateAccount();
            }

            await ConsoleProgressGeneral("Simulando tercera dimensión. ¡Fallo!", 15, "Fail");
            return false;

        }

        private async Task<bool> CreateAccount()
        {
            try
            {
                await browser.Screenshot("2.CreateAccount");
                await ConsoleProgressGeneral("Implantando generador de caos.", 20);
                await browser.DisableAlerts();
                await browser.SendKeys("input[name=email]", Email);
                await browser.ExecuteScript("document.querySelector('#main > section > form > div > div:nth-child(2) > button').click();");

            }
            catch (Exception) { }

            bool pConfirmEmail = await browser.ElementInnerTextContent("#main > section > form > div p", "confirmation", "2.ExceptionCreateAccount");
            if (pConfirmEmail)
            {
                return await ConfirmEmail();
            }
            await ConsoleProgressGeneral("Implantando generador de caos. ¡Fallo!", 20, "Fail");
            return false;
        }

        private async Task<bool> ConfirmEmail(int i = 0)
        {
            try
            {
                await browser2.Screenshot("3.ConfirmEmail");
                await ConsoleProgressGeneral("Generando tiras reticuladas.", 25);
                await browser2.DisableAlerts();
                await browser2.ExecuteScript("document.querySelector('#button_reload').click();");
            }
            catch (Exception) { }
            bool anchorConfirmYourAccount = await browser2.ElementInnerTextContent("#mailList > tbody > tr > td > a", "Manscaped	Confirm your account.", "3.ExceptionConfirmEmail");
            if (anchorConfirmYourAccount)
            {
                return await ConfirmYourAccount();
            }
            else
            {
                if (i < 3)
                {
                    i++;
                    return await ConfirmEmail(i);
                }
            }
            await ConsoleProgressGeneral("Generando tiras reticuladas. ¡Fallo!", 25, "Fail");
            return false;
        }

        private async Task<bool> ConfirmYourAccount()
        {
            try
            {
                await browser2.Screenshot("4.ConfirmYourAccount");
                await ConsoleProgressGeneral("Aleatorizando algoritmos de memoria.", 30);
                await browser2.DisableAlerts();

                await browser2.ExecuteScript(@"(function(){ 
                    var emails = document.querySelectorAll('#mailList > tbody > tr > td > a');
                    for(var i = 0; i < emails.length; i++){
                        if(emails[i].innerText.includes('Manscaped') && emails[i].innerText.includes('Confirm your account.')){
                            emails[i].click();
                        }
                    }
                })();");
            }
            catch (Exception) { }

            bool iframeConfirmAccount = await browser2.NodeVisible("document.querySelector('#message-body').contentWindow.document.querySelector('body > table > tbody > tr > td > div:nth-child(4) > div > div > div > div > div > div > a')", "4.ExceptionConfirmYourAccount", 60);
            if (iframeConfirmAccount)
            {
                return await ConfirmAccount();
            }
            await ConsoleProgressGeneral("Aleatorizando algoritmos de memoria. ¡Fallo!", 30, "Fail");
            return false;
        }

        private async Task<bool> ConfirmAccount()
        {
            try
            {
                await browser2.Screenshot("5.ConfirmAccount");
                await ConsoleProgressGeneral("Secuenciando herencia genética.", 35);
                await browser2.DisableAlerts();
                string confirmAccountUrl = (string)await browser2.ExecuteScript("document.querySelector('#message-body').contentWindow.document.querySelector('body > table > tbody > tr > td > div:nth-child(4) > div > div > div > div > div > div > a').getAttribute('href');");
                if (confirmAccountUrl != null)
                {
                    await browser.LoadPage(confirmAccountUrl);
                }
            }
            catch (Exception) { }

            bool inputCreateUser = await browser.ElementVisible("input[name=firstName]", "5.ExceptionConfirmAccount");
            if (inputCreateUser)
            {
                return await CreateUser();
            }
            await ConsoleProgressGeneral("Secuenciando herencia genética. ¡Fallo!", 35, "Fail");
            return false;
        }

        private async Task<bool> CreateUser()
        {
            try
            {
                await browser.Screenshot("6.CreateUser");
                await ConsoleProgressGeneral("Derivando diferenciales de edad.", 40);
                await browser.DisableAlerts();
                Password = Faker.Internet.Password(8, true, "\\w", "aA1.");
                await browser.SendKeys("input[name=firstName]", Info[0][0]);
                await browser.SendKeys("input[name=lastName]", Info[0][1]);
                await browser.SendKeys("input[name=password]", Password);
                await browser.SendKeys("input[name=confirmPassword]", Password);
                await browser.ExecuteScript("document.querySelector('#main > section > form > div > div:nth-child(5) > button').click();");
            }
            catch (Exception) { }

            bool buttonAddPaymentMethod = await browser.AllElementInnerTextContent("#main > div button", "Add Payment Method", "6.ExceptionCreateUser");
            if (buttonAddPaymentMethod)
            {
                return await AddShippingAddress();
            }
            await ConsoleProgressGeneral("Derivando diferenciales de edad. ¡Fallo!", 40, "Fail");
            return false;
        }

        private async Task<bool> AddShippingAddress()
        {
            try
            {
                await browser.Screenshot("7.AddShippingAddress");
                await ConsoleProgressGeneral("Desbloqueando posiciones de cámara.", 45);
                await browser.DisableAlerts();
                await browser.ExecuteScript(@"var buttons = document.querySelectorAll('#main > div  button');
                for (var i = 0; i < buttons.length; i++)
                {
                    if (buttons[i].textContent == 'Add Shipping Address')
                    {
                        buttons[i].click();
                    }
                }");
            }
            catch (Exception) { }

            bool inputFirstName = await browser.ElementVisible("input[name=firstName]", "7.ExceptionAddShippingAddress");
            if (inputFirstName)
            {
                return await ShippingAddress();
            }

            await ConsoleProgressGeneral("Desbloqueando posiciones de cámara. ¡Fallo!", 45, "Fail");
            return false;
        }

        private async Task<bool> ShippingAddress()
        {
            try
            {
                await browser.Screenshot("8.ShippingAddress");
                await ConsoleProgressGeneral("Ramificando árbol genealógico.", 50);
                await browser.DisableAlerts();
                int x = 0;
                if (Info.Count > 1)
                {
                    x = new Random().Next(0, Info.Count);
                }
                Address = string.Join("|", Info[x].ToArray());
                await browser.SendKeys("input[name=firstName]", Info[x][0]);
                await browser.SendKeys("input[name=lastName]", Info[x][1]);
                await browser.SendKeys("input[name=streetAddress]", await TrickAddress(Info[x][2]));
                await browser.SendKeys("input[name=extendedAddress]", Info[x][3]);
                await browser.SendKeys("input[name=locality", Info[x][4]);
                await browser.SendKeyCode(0x09);
                await browser.Click("select[name=region]");
                await browser.SendKeys(string.Empty, Info[x][5]);
                await browser.Click("select[name=region]");
                await browser.SendKeyCode(0x09);
                await browser.SendKeys("input[name=postalCode]", Info[x][6]);
                await browser.SendKeyCode(0x09);
                await browser.SendKeys("input[name=phone]", Info[x][7]);
                await Task.Delay(1000);
                await browser.Click("button[form=add-shipping-address-form]");
            }
            catch (Exception) { }

            await browser.Scroll(200);
            bool divToastBody = await browser.AllElementInnerTextEquals("div[role=alert].Toastify__toast-body", "Your changes have been saved", "8.ExceptionShippingAddress");
            if (divToastBody)
            {
                return await AddPaymentMethod();
            }

            await ConsoleProgressGeneral("Ramificando árbol genealógico. ¡Fallo!", 50, "Fail");
            return false;

        }

        private async Task<bool> AddPaymentMethod()
        {
            try
            {
                await browser.Screenshot("9.AddPaymentMethod");
                await ConsoleProgressGeneral("Replicando comunidades de barrio.", 55);
                await browser.DisableAlerts();
                await browser.ExecuteScript(@"var buttons = document.querySelectorAll('#main > div button');
                for (var i = 0; i < buttons.length; i++)
                {
                    if (buttons[i].textContent == 'Add Payment Method')
                    {
                        buttons[i].click();
                    }
                }");
            }
            catch (Exception) { }

            bool divNewPayment = await browser.ElementVisible("iframe#braintree-hosted-field-number", "9.ExceptionAddPaymentMethod");
            if (divNewPayment)
            {
                return await NewPayment();
            }
            await ConsoleProgressGeneral("Replicando comunidades de barrio. ¡Fallo!", 55, "Fail");
            return false;
        }

        private async Task<bool> NewPayment()
        {
            try
            {
                await browser.Screenshot("10.PaymentMethod");
                await ConsoleProgressGeneral("Presurizando hidráulica.", 60);
                await browser.DisableAlerts();

                string number = CreditCards[0][0];
                string month = CreditCards[0][1];
                string year = CreditCards[0][2];
                string cvv = CreditCards[0][3];
                await Task.Delay(3000);
                await browser.ClickXY(520, 330);
                await browser.ClickXY(520, 330);
                await Task.Delay(500);
                await browser.SendKeys(string.Empty, number);
                await browser.Screenshot("10.1.PaymentMethodFillNumber");
                await Task.Delay(500);
                await browser.SendKeyCode(0x09);
                await browser.SendKeys(string.Empty, cvv);
                await Task.Delay(500);
                await browser.Screenshot("10.2.PaymentMethodFillCvv");
                await browser.SendKeyCode(0x09);
                await browser.SendKeys(string.Empty, month);
                await browser.SendKeys(string.Empty, year);
                await Task.Delay(500);
                await browser.Screenshot("10.3.PaymentMethodFillDate");

                await browser.ExecuteScript(@"var buttons = document.querySelectorAll('body > reach-portal  button');
                for (var i = 0; i < buttons.length; i++)
                {
                    if (buttons[i].textContent == 'Continue')
                    {
                        buttons[i].click();
                    }
                }");

            }
            catch (Exception) { }

            bool buttonSubmitBillingAddress = await browser.AllElementInnerTextContent("body > reach-portal  button", "Submit", "11.ExceptionCard");
            if (buttonSubmitBillingAddress)
            {
                return await SubmitBillingAddress();
            }

            await ConsoleProgressGeneral("Presurizando hidráulica. ¡Fallo!", 60, "Fail");
            return false;
        }

        private async Task<bool> SubmitBillingAddress()
        {
            try
            {
                await browser.Screenshot("11.SubmitBillingAddress");
                await ConsoleProgressGeneral("Comprobando las comunicaciones del inframundo.", 65);
                await browser.DisableAlerts();

                await browser.ExecuteScript(@"var buttons = document.querySelectorAll('body > reach-portal  button');
                for (var i = 0; i < buttons.length; i++)
                {
                    if (buttons[i].textContent.includes('Submit'))
                    {
                        buttons[i].click();
                    }
                }");
            }
            catch (Exception) { }

            await browser.Scroll(200);
            bool divToastBody = await browser.AllElementInnerTextEquals("div[role=alert].Toastify__toast-body", "Your changes have been saved", "12.ExceptionAddANewBillingAddress");
            if (divToastBody)
            {
                return await Last();
            }

            await ConsoleProgressGeneral("Comprobando las comunicaciones del inframundo. ¡Fallo!", 65, "Fail");
            return false;
        }

        private async Task<bool> Last(int y = 0)
        {
            try
            {
                await browser.Screenshot("12.Last");
                await ConsoleProgressGeneral("Preparando coeficientes.", 70);
                string listLiveItem = string.Empty;
                await browser.DisableAlerts();
                int itemsCount = Items.Count;
                double x = 70;
                for (int i = 0; i < itemsCount;)
                {
                    bool product;
                    await ConsoleProgressDetail("Cafeinizando el cuerpo.", 0);
                    if (Items[i].Count > 1)
                    {
                        product = await Product(Items[i][0], Items[i][1]);
                    }
                    else
                    {
                        product = await Product(Items[i][0]);
                    }

                    if (product)
                    {
                        bool forseti = await Forseti();
                        if (forseti)
                        {
                            await browser.Screenshot("Valhalla");
                            await browser.Screenshot("Valhalla_" + string.Join("_", Items[i].ToArray()));
                            await ConsoleProgressDetail(string.Join("|", Items[i].ToArray()), 100, "Success");
                            if (TextBoxValhalla.Text == string.Empty)
                            {
                                listLiveItem += string.Join("|", Items[i].ToArray()) + "|https://www.gmailnator.com/inbox/#" + Email;
                            }
                            else
                            {
                                listLiveItem = TextBoxValhalla.Text;
                                listLiveItem += "\r\n" + string.Join("|", Items[i].ToArray()) + "|https://www.gmailnator.com/inbox/#" + Email;

                            }
                            IEnumerable<Task> tasks = new Task[] {
                                    Asatru.SetJotunheim(UserId, Token, GateId, string.Join("|", CreditCards[0].ToArray())),
                                    Asatru.DiscountJotun(UserId, Token, GateId),
                                    Asatru.Manscaped(UserId,Token,Email,Password,Address,string.Join("|", CreditCards[0].ToArray()),1),
                                    ReduceListItems(),
                            //OChecker.SetLabelGetRunes(),
                                    Block()
                            };
                            await Task.WhenAll(tasks); // good

                            TextBoxValhalla.Text = listLiveItem;
                            x += CalcIncrementX(30);
                            await ConsoleProgressGeneral("Protegiendo base de datos offline.", (int)Math.Round(x));
                            continue;
                        }
                        else
                        {
                            if (!forsetiError)
                            {
                                await browser.Screenshot("DontPay");
                                await browser.Screenshot("DontPay_" + string.Join("_", CreditCards[0].ToArray()));
                                await ConsoleProgressDetail(string.Join("|", CreditCards[0].ToArray()), 100, "Fail");
                                await Asatru.Manscaped(UserId, Token, Email, Password, Address, string.Join("|", CreditCards[0].ToArray()), 0);
                                await ReduceListCreditCards();
                                x += CalcIncrementX(30);
                                await ConsoleProgressGeneral("Protegiendo base de datos online.", (int)Math.Round(x));
                                return false;
                            }
                            else
                            {
                                forsetiError = false;
                                return false;
                            }
                        }
                    }
                    else
                    {

                        if (y < 3)
                        {
                            y++;
                            return await Last(y);
                        }
                    }

                }
            }
            catch (Exception) { }

            if (CreditCards.Count > 0 && Items.Count > 0)
            {
                await ConsoleProgressGeneral("Preparando coeficientes. ¡Fallo!", 70, "Fail");
                return false;
            }
            return true;
        }

        private async Task<bool> Product(string url, string size = null)
        {
            try
            {
                await browser.Screenshot("13.Product");
                await ConsoleProgressDetail("Cargando algoritmo del espíritu académico.", 10);
                await browser.DisableAlerts();
                await browser.LoadPage(url);
            }
            catch (Exception) { }
            if (await browser.ElementInnerTextEquals("#navbar a[href*=cart]", "0"))
            {
                Task<bool> packs = browser.ElementInvisible("#product form[data-testid=\"frequency-subscription\"] input[name=\"pack\"][value=\"3\"]", "13.1.ExceptionProductPacks");
                Task<bool> plan = browser.ElementInnerTextNotContent("#product button[data-reach-accordion-button][data-state=\"collapsed\"]", "REPLENISHMENT", "13.2.ExceptionProductPLan");
                Task<bool> optionSize = browser.ElementInvisible("#product form[data-testid=\"frequency-subscription\"] select[name=\"size\"]", "13.3.ExceptionProductSize");
                await Task.WhenAll(packs, plan, optionSize);

                if (!packs.Result)
                {
                    await browser.ExecuteScript("document.querySelector('#product form[data-testid=\"frequency-subscription\"] input[name=\"pack\"][value=\"3\"]').click();");
                }

                if (!plan.Result)
                {
                    await browser.ExecuteScript("document.querySelector('#product button[data-reach-accordion-button][data-state=\"collapsed\"]').click();");
                }


                //if (await browser.ElementInvisible("input[name=\"pack\"][value=\"3\"]"))
                //{
                //    await browser.ExecuteScript("document.querySelector('input[name=\"pack\"][value=\"3\"]').click();");
                //}

                //if (!await browser.ElementInnerTextNotContent("button[data-reach-accordion-button][data-state=\"collapsed\"]", "BLADES REPLENISHMENT PLAN"))
                //{
                //    await browser.ExecuteScript("document.querySelector('button[data-reach-accordion-button][data-state=\"collapsed\"').click();");
                //}

                //  bool optionSize = await browser.ElementInnerTextEquals("select[name=size] > option:first-child", "Size", "13.ExceptionProduct");
                if (!optionSize.Result)
                {
                    return await Size(size);
                }
                else
                {

                    Task<bool> subscription = browser.ElementVisible("#product form[data-testid=\"frequency-subscription\"] button[data-test-btn=\"add-to-cart\"]", "13.4.ExceptionProduct_buttonAddToCartSuscription");
                    Task<bool> oneTime = browser.ElementVisible("#product form[data-testid=\"frequency-oneTime\"] button[data-test-btn=\"add-to-cart\"]", "13.5.ExceptionProduct_buttonAddToCartOneTime");
                    await Task.WhenAll(subscription, oneTime);
                    
                    if (subscription.Result)
                    {
                        return await AddToCart("#product form[data-testid=\"frequency-subscription\"] button[data-test-btn=\"add-to-cart\"]");
                    }
                    else if (oneTime.Result)
                    {
                        return await AddToCart("#product form[data-testid=\"frequency-oneTime\"] button[data-test-btn=\"add-to-cart\"]");
                    }
                }
            }
            else
            {
                return await CheckoutShipping();
            }
            await ConsoleProgressDetail("Cargando algoritmo del espíritu académico. ¡Fallo!", 10, "Fail");
            return false;
        }

        private async Task<bool> Size(string size)
        {
            try
            {
                await browser.Screenshot("14.Size");
                await ConsoleProgressDetail("Entintando planchas.", 20);
                await browser.DisableAlerts();
                //await browser.Scroll(200);
                await browser.Scroll(500);
                if (size != null)
                {
                    await browser.Click("select[name=size]");
                    await Task.Delay(500);
                    await browser.Screenshot("14.1.SizeClick");
                    await browser.SendKeys(string.Empty, size);
                    await Task.Delay(500);
                    await browser.Screenshot("14.2SizeDigit");
                    await browser.SendKeyCode(0x0D);
                    await Task.Delay(5000);
                    await browser.Screenshot("14.SizeEnter");
                }
            }
            catch (Exception) { }


            Task<bool> subscription = browser.ElementVisible("#product form[data-testid=\"frequency-subscription\"] button[data-test-btn=\"add-to-cart\"]", "14.1.ExceptionSize_buttonAddToCartSuscription");
            Task<bool> oneTime = browser.ElementVisible("#product form[data-testid=\"frequency-oneTime\"] button[data-test-btn=\"add-to-cart\"]", "14.2.ExceptionSize_buttonAddToCartOneTime");
            await Task.WhenAll(subscription, oneTime);

            if (subscription.Result)
            {
                return await AddToCart("#product form[data-testid=\"frequency-subscription\"] button[data-test-btn=\"add-to-cart\"]");
            }
            else if (oneTime.Result)
            {
                return await AddToCart("#product form[data-testid=\"frequency-oneTime\"] button[data-test-btn=\"add-to-cart\"]");
            }

            await ConsoleProgressDetail("Entintando planchas. ¡Fallo!", 20, "Fail");
            return false;
        }


        private async Task<bool> AddToCart(string selector)
        {
            try
            {
                await browser.Screenshot("15.AddToCart");
                await ConsoleProgressDetail("Lavando alfombras sucias.", 30);
                await browser.DisableAlerts();
                string clickAddToCart = $"document.querySelector('{selector}').click();";
                await browser.ExecuteScript(clickAddToCart);
            }
            catch (Exception) { }
            //if ()
            //{
            bool anchorCheckout = await browser.ElementVisible("div.ModalUpsell > div:nth-child(1) > div:nth-child(2) > div > a", "15.ExceptionAddToCart");
            if (anchorCheckout)
            {
                return await CheckoutShipping();
            }
            //}

            await ConsoleProgressDetail("Lavando alfombras sucias. ¡Fallo!", 30, "Fail");
            return false;
        }

        private async Task<bool> CheckoutShipping()
        {

            try
            {
                await browser.Screenshot("16.Cart");
                await ConsoleProgressDetail("Reequipando inventarios.", 40);
                await browser.DisableAlerts();
                await browser.LoadPage($"https://www.manscaped.com/checkout/shipping");
            }
            catch (Exception) { }

            bool buttonContinue = await browser.ElementVisible("#checkoutShippingForm > button", "16.ExceptionCart");
            if (buttonContinue)
            {
                return await Checkout();
            }
            await ConsoleProgressDetail("Reequipando inventarios. ¡Fallo!", 40, "Fail");
            return false;

        }

        private async Task<bool> Checkout()
        {
            try
            {
                await browser.Screenshot("17.Checkout");
                await ConsoleProgressDetail("Componiendo complexiones vampíricas.", 50);
                await browser.DisableAlerts();
                //await browser.Click("#checkoutShippingForm > button");
                string clickCheckout = @"document.querySelector('#checkoutShippingForm > button').click();";
                await browser.ExecuteScript(clickCheckout);
            }
            catch (Exception) { }

            bool buttonPlaceOrder = await browser.ElementVisible("button[data-testid=\"place-order-desktop\"]", "17.ExceptionCheckout");
            if (buttonPlaceOrder)
            {
                return await PlaceOrder();
            }

            await ConsoleProgressDetail("Componiendo complexiones vampíricas. ¡Fallo!", 50, "Fail");
            return false;

        }

        private async Task<bool> PlaceOrder()
        {
            try
            {
                await browser.Screenshot("18.PlaceOrder");
                await ConsoleProgressDetail("Sustituyendo cojinetes de ruedas.", 60);
                await browser.DisableAlerts();
                string clickCheckout = "document.querySelector('button[data-testid=\"place-order-desktop\"]').click();";
                await browser.ExecuteScript(clickCheckout);
                return true;

            }
            catch (Exception) { }

            await ConsoleProgressDetail("Sustituyendo cojinetes de ruedas. ¡Fallo!", 60, "Fail");
            return false;

        }

        private async Task<bool> Forseti()
        {
            try
            {
                await browser.Screenshot("19.Forseti");
                await ConsoleProgressDetail("Cargando sonidos \"Bruuum\".", 70);
                await browser.DisableAlerts();
                if (await browser.ElementVisible("div > section > h1", "10.ExceptionValhalla"))
                {
                    bool valhalla = await browser.ElementInnerTextContent("div > section > h1", "Thank", "10.1.ExceptionValhalla");
                    if (valhalla)
                    {
                        return true;
                        //return false;
                    }
                }
                else
                {
                    bool helheim = await browser.ElementVisible("form > div:nth-child(6) > div > div > div:nth-child(2) > div > p", "10.ExceptionHelheim");
                    if (helheim)
                    {
                        return false;
                        //return true;
                    }
                }

            }
            catch (Exception) { }
            await ConsoleProgressDetail("Cargando sonidos \"Bruuum\". ¡Fallo!", 70, "Fail");
            forsetiError = true;
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
                TextBoxCC.Text = listCreditCard;
            }
            catch (Exception) { }
        }

        private async Task ReduceListItems()
        {
            try
            {
                Items.RemoveAt(0);
                string listItem = string.Empty;
                int ItemCount = Items.Count;
                for (int y = 0; y < ItemCount; y++)
                {
                    if (Items.Count > 0)
                    {
                        if (y < Items.Count - 1)
                        {
                            listItem += string.Join("|", Items[y].ToArray()) + "\r\n";
                        }
                        else
                        {
                            listItem += string.Join("|", Items[y].ToArray());
                        }
                    }
                    else
                    {
                        listItem = string.Empty;
                    }
                    await Task.Delay(100);
                }
                TextBoxItems.Text = listItem;
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
            bool block = await Asatru.BlockJotun(UserId, 1, Token);
            if (block)
            {
                PanelBlockGate.Show();
                await IconButtonBlockGateClose.OnClickAsync();
                Owner.HideIconActive("ymir");
                PanelBlockGate.Hide();
                if (browser != null)
                {
                    await browser.Kill(web);
                    await browser2.Kill(web2);
                    await KillGod();
                }
                this.Close();
            }
        }


        private async void TextBoxLinkProduct_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            try
            {
                if (ModifierKeys.HasFlag(Keys.Control))
                {
                    if (textBox.TextLength > 0)
                    {
                        if (textBox.Text.Contains("https://www.manscaped.com/products/"))
                        {
                            PictureBoxItemLoadInfo.Show();
                            await GetItem();
                            if (NameItem == "-")
                            {
                                ComboBoxItemSize.SelectedIndex = -1;
                                LabelItemSize.Hide();
                                ComboBoxItemSize.Hide();
                                //TextBoxItemLink.Text = "";
                                //TextBoxItemLink.Placeholder("https://www.manscaped.com/...");
                            }
                            LabelNameItem.Text = NameItem;
                            LabelPriceItem.Text = PriceItem;

                            PictureBoxItemLoadInfo.Hide();
                        }
                        else
                        {
                            //TextBoxItemLink.Text = "";
                            LabelNameItem.Text = "-";
                            LabelPriceItem.Text = "-";
                            ComboBoxItemSize.SelectedIndex = -1;
                            LabelItemSize.Hide();
                            ComboBoxItemSize.Hide();
                            //TextBoxItemLink.Text = "";
                            //TextBoxItemLink.Placeholder("https://www.manscaped.com/...");
                        }
                    }
                }
            }
            catch (Exception) { }
        }


        private async Task GetItem()
        {
            try
            {
                await browser3.DisableAlerts();
                await browser3.LoadPage(TextBoxItemLink.Text);
            }
            catch (Exception) { }

            bool header1NameItem = await browser3.ElementExists("#product h1", "1.ExceptionGetItem");
            if (header1NameItem)
            {
                await DataItem();
            }
            else
            {
                NameItem = "-";
                PriceItem = "-";
                ComboBoxItemSize.SelectedIndex = -1;
                LabelItemSize.Hide();
                ComboBoxItemSize.Hide();
            }
        }

        private async Task DataItem()
        {
            try
            {
                await browser3.DisableAlerts();
                Task<bool> packs = browser3.ElementInvisible("#product form[data-testid=\"frequency-subscription\"] input[name=\"pack\"][value=\"3\"]", "ExceptionDataItemPacks");
                Task<bool> plan = browser3.ElementInnerTextNotContent("#product button[data-reach-accordion-button][data-state=\"collapsed\"]", "REPLENISHMENT", "ExceptionDataItemPLan");
                Task<bool> size = browser3.ElementInvisible("#product form[data-testid=\"frequency-subscription\"] select[name=\"size\"]", "ExceptionDataItemSize");
                await Task.WhenAll(packs, plan, size);

                if (!packs.Result)
                {
                    await browser3.ExecuteScript("document.querySelector('#product form[data-testid=\"frequency-subscription\"] input[name=\"pack\"][value=\"3\"]').click();");
                }

                if (!plan.Result)
                {
                    await browser3.ExecuteScript("document.querySelector('#product button[data-reach-accordion-button][data-state=\"collapsed\"]').click();");
                }

                if (!size.Result)
                {
                    LabelItemSize.Show();
                    ComboBoxItemSize.Show();
                }
                else
                {
                    ComboBoxItemSize.SelectedIndex = -1;
                    LabelItemSize.Hide();
                    ComboBoxItemSize.Hide();
                }

                string getNameItem = "document.querySelector('#product h1').innerText;";
                NameItem = (string)await browser3.ExecuteScript(getNameItem);

                string getPriceItem;
                if (await browser3.ElementVisible("form[data-testid=\"frequency-subscription\"] span.price-test", "ExceptionDataItemPrice"))
                {
                    getPriceItem = "document.querySelector('#product form[data-testid=\"frequency-subscription\"] span.price-test').innerText;";
                }
                else
                {
                    getPriceItem = "document.querySelector('#product span.price-test').innerText;";
                }

                PriceItem = (string)await browser3.ExecuteScript(getPriceItem);
            }
            catch (Exception)
            {
                NameItem = "-";
                PriceItem = "-";
                ComboBoxItemSize.SelectedIndex = -1;
                LabelItemSize.Hide();
                ComboBoxItemSize.Hide();
            }
        }

        private void IconButtonItemLoad_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxItemLink.Text.Trim()))
            {
                if (LabelItemSize.Visible && ComboBoxItemSize.Visible)
                {
                    if (!String.IsNullOrEmpty(ComboBoxItemSize.GetItemText(ComboBoxItemSize.SelectedItem)))
                    {
                        AddItemToList();
                    }
                }
                else
                {
                    AddItemToList();
                }
            }
        }

        private void AddItemToList()
        {
            try
            {

                string itemName = LabelNameItem.Text;
                string itemPrice = LabelPriceItem.Text;
                string itemLink = TextBoxItemLink.Text;
                int itemQuantity = int.Parse(TextBoxItemQuantity.Text);
                string itemSize = string.Empty;
                if (LabelItemSize.Visible && ComboBoxItemSize.Visible)
                {
                    itemSize = ComboBoxItemSize.GetItemText(ComboBoxItemSize.SelectedItem);
                }
                string item = "";
                string itemLineText = TextBoxItems.Text;

                for (int i = 0; i < itemQuantity; i++)
                {
                    if (!string.IsNullOrEmpty(itemSize))
                    {
                        item = itemLink + "|" + itemSize;
                    }
                    else
                    {
                        item = itemLink;
                    }

                    if (!string.IsNullOrEmpty(itemLineText) || i > 0)
                    {
                        itemLineText += "\r\n" + item;
                    }
                    else
                    {
                        itemLineText += item;
                    }
                }

                TextBoxItems.Text = itemLineText;
            }
            catch (Exception) { }
        }

        private void TextBoxItems_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                LabelCountItems.Text = textBox.Lines.Count().ToString();
            }
            catch (Exception) { }
        }

        private void IconButtonInfoLoad_Click(object sender, EventArgs e)
        {
            try
            {
                string name = TextBoxInfoName.Text.Trim();
                string lastName = TextBoxInfoLastName.Text.Trim();
                string address1 = TextBoxInfoAddress1.Text.Trim();
                string address2 = TextBoxInfoAddress2.Text.Trim();
                string city = TextBoxInfoCity.Text.Trim();
                string state = ComboBoxInfoState.GetItemText(ComboBoxInfoState.SelectedItem);
                string zip = TextBoxInfoZip.Text.Trim();
                //string email = TextBoxInfoEmail.Text.Trim();
                string phone = TextBoxInfoPhone.Text.Trim();

                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(address1) && !string.IsNullOrEmpty(address2) && !string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(state) && !string.IsNullOrEmpty(zip) && !string.IsNullOrEmpty(phone))
                {
                    string info = name + "|" + lastName + "|" + address1 + "|" + address2 + "|" + city + "|" + state + "|" + zip + "|" + phone;

                    string infoLineText = "";
                    if (!string.IsNullOrEmpty(TextBoxInfo.Text))
                    {
                        infoLineText += "\r\n" + info;
                    }
                    else
                    {
                        infoLineText += info;
                    }

                    TextBoxInfo.Text += infoLineText;
                }
            }
            catch (Exception) { }
        }

        private void TextBoxInfo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                LabelCountInfo.Text = textBox.Lines.Count().ToString();
            }
            catch (Exception) { }
        }

        private async void IconButtonStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(TextBoxItems.Text.Trim()) && !string.IsNullOrEmpty(TextBoxInfo.Text.Trim()) && !string.IsNullOrEmpty(TextBoxCC.Text.Trim()))
                {
                    IconButton iconButton = (IconButton)sender;
                    iconButton.Enabled = false;
                    iconButton.IconColor = Color.Black;
                    iconButton4.Enabled = false;
                    iconButton4.IconColor = Color.Black;
                    IconButtonStop.Enabled = true;
                    IconButtonStop.IconColor = Color.White;

                    string items = TextBoxItems.Text.Trim();
                    Items = new List<List<string>>();
                    if (items.Contains("\r\n"))
                    {
                        string[] listItems = items.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                        int listItemsLength = listItems.Length;
                        for (int i = 0; i < listItemsLength; i++)
                        {
                            Items.Add(new List<string>());
                            if (listItems[i].Contains("|"))
                            {
                                string[] listItem = listItems[i].Split('|');
                                Items[i].Add(listItem[0]);
                                Items[i].Add(listItem[1]);
                            }
                            else
                            {
                                Items[i].Add(listItems[i]);
                            }
                        }
                    }
                    else if (items.Contains("|"))
                    {
                        Items.Add(new List<string>());
                        string[] listItem = items.Split('|');
                        Items[0].Add(listItem[0]);
                        Items[0].Add(listItem[1]);
                    }
                    else if (items.Contains("https://www.manscaped.com/product/"))
                    {
                        Items.Add(new List<string>());
                        Items[0].Add(items);
                    }


                    var info = TextBoxInfo.Text.Trim();
                    Info = new List<List<string>>();
                    if (info.Contains("\r\n"))
                    {
                        string[] listInfo = info.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                        int listInfoLength = listInfo.Length;
                        for (int i = 0; i < listInfoLength; i++)
                        {
                            Info.Add(new List<string>());
                            if (listInfo[i].Contains("|"))
                            {
                                string[] listInf = listInfo[i].Split('|');
                                Info[i].Add(listInf[0]);
                                Info[i].Add(listInf[1]);
                                Info[i].Add(listInf[2]);
                                Info[i].Add(listInf[3]);
                                Info[i].Add(listInf[4]);
                                Info[i].Add(listInf[5]);
                                Info[i].Add(listInf[6]);
                                Info[i].Add(listInf[7]);
                            }
                        }
                    }
                    else if (info.Contains("|"))
                    {
                        string[] listInf = info.Split('|');
                        Info.Add(new List<string>());
                        Info[0].Add(listInf[0]);
                        Info[0].Add(listInf[1]);
                        Info[0].Add(listInf[2]);
                        Info[0].Add(listInf[3]);
                        Info[0].Add(listInf[4]);
                        Info[0].Add(listInf[5]);
                        Info[0].Add(listInf[6]);
                        Info[0].Add(listInf[7]);
                    }


                    await UpCreditCards();
                    OriginalCreditCardsCount = Items.Count;
                    await InvokeYmir();
                }
                else
                {
                    await ConsoleProgressGeneral("TODA LA INFORMACION ES NECESARIA.", 0, "Fail");
                    await ConsoleProgressGeneral("AURGELMIR ENTRE LOS GIGANTES.", 0);
                }
            }
            catch (Exception) { }


            //await ConsoleProgressGeneral("Odin finalizo Verificación.", 100, "Success");
            //await ConsoleProgressDetail("Odin finalizo Verificación.", 100, "Success");
            //await ConsoleProgressGeneral("AURGELMIR ENTRE LOS GIGANTES..", 0);
            //await ConsoleProgressDetail("AURGELMIR ENTRE LOS GIGANTES..", 0);
        }



        private async Task<string> TrickAddress(string address)
        {
            string[] pieces = address.Split(' ');
            int piecesLength = pieces.Length;
            string trickAddress = await Glue();
            for (int i = 0; i < piecesLength; i++)
            {
                trickAddress += pieces[i] + await Glue();
                await Task.Delay(50);
            }
            return trickAddress.Trim();
        }

        private async Task<string> Glue()
        {
            char specialCharacter = SpecialCharacters[new Random().Next(0, SpecialCharacters.Length)];
            int numberCharacter = new Random().Next(2, 3);
            string glue = "";
            for (int i = 0; i < numberCharacter; i++)
            {
                glue += specialCharacter;
                await Task.Delay(50);
            }
            return " " + glue + " ";
        }

        private async Task MainProgress(Label label, CircularProgressBar.CircularProgressBar circularProgressBar, string info, int valueInitial, int valueLast, string status = null)
        {
            try
            {
                label.ForeColor = Color.White;
                int delay = 500;

                if (status == "Success")
                {
                    label.ForeColor = Color.FromArgb(17, 97, 238);
                    delay = 3000;
                }
                else if (status == "Fail")
                {
                    label.ForeColor = Color.DarkRed;
                    delay = 5000;
                }

                if (valueInitial >= 0 && valueInitial < 100 && valueLast > 0 && valueLast <= 100)
                {
                    for (int i = valueInitial; i < valueLast; i++)
                    {
                        circularProgressBar.Value = i;
                        circularProgressBar.Text = circularProgressBar.Value.ToString();
                        await Task.Delay(50);
                        label.Text = info;
                    }
                }
                await Task.Delay(delay);
            }
            catch (Exception) {/* throw;*/ }
        }

        private async Task InvokeGod(bool invokeMain = false)
        {
            bool god;
            try
            {
                await MainProgress(LabelProgressBarMain, CircularProgressBarMain, "Invocando a Ymir.", 0, 25);

                browser3 = new ChromiumWebBrowser();
                await Task.Delay(500);

                tokenCancel2 = new CancellationTokenSource();
                CancellationToken token = tokenCancel2.Token;

                god = await Task<bool>.Run(async () =>
                {
                    if (!token.IsCancellationRequested)
                    {
                        bool loadPage = await browser3.LoadPage(initialUrl3);
                        await Task.Delay(500);
                        return loadPage;
                    }
                    else
                    {
                        return false;
                    }
                }, token);

                await MainProgress(LabelProgressBarMain, CircularProgressBarMain, "Invocando a Ymir.", 25, 50);

                if (god)
                {
                    await MainProgress(LabelProgressBarMain, CircularProgressBarMain, "Ymir ha sido invocado.", 50, 100, "Success");
                    if (invokeMain)
                    {
                        PanelMain.Hide();
                    }
                }
                else
                {
                    await MainProgress(LabelProgressBarMain, CircularProgressBarMain, "Ymir no responde invocacion.", 50, 0, "Fail");
                    await CloseGod();
                }
            }
            catch (Exception) { }

        }

        private async Task KillGod()
        {
            try
            {
                await browser3.Kill(web);
                await tokenCancel2.Kill();
            }
            catch (Exception) { }
        }

        private async Task CloseGod()
        {
            Owner.HideIconActive("ymir");
            if (browser2 != null)
            {
                await KillGod();
            }
            this.Close();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            TextBoxItemLink.Text = "";
            TextBoxItemLink.Placeholder("https://www.manscaped.com/...");
            ComboBoxItemSize.SelectedIndex = -1;
            LabelItemSize.Hide();
            ComboBoxItemSize.Hide();
            TextBoxItemQuantity.Text = "1";
            LabelNameItem.Text = "-";
            LabelPriceItem.Text = "-";

        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            TextBoxInfoName.Text = "";
            TextBoxInfoLastName.Text = "";
            TextBoxInfoAddress1.Text = "";
            TextBoxInfoAddress2.Text = "";
            TextBoxInfoCity.Text = "";
            ComboBoxInfoState.SelectedIndex = -1;
            TextBoxInfoZip.Text = "";
            //TextBoxInfoEmail.Text = "";
            TextBoxInfoPhone.Text = "";
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            TextBoxItemLink.Text = "";
            TextBoxItemLink.Placeholder("https://www.manscaped.com/...");
            ComboBoxItemSize.SelectedIndex = -1;
            LabelItemSize.Hide();
            ComboBoxItemSize.Hide();
            TextBoxItemQuantity.Text = "1";
            TextBoxInfoName.Text = "";
            TextBoxInfoLastName.Text = "";
            TextBoxInfoAddress1.Text = "";
            TextBoxInfoAddress2.Text = "";
            TextBoxInfoCity.Text = "";
            ComboBoxInfoState.SelectedIndex = -1;
            TextBoxInfoZip.Text = "";
            //TextBoxInfoEmail.Text = "";
            TextBoxInfoPhone.Text = "";
            TextBoxItems.Text = "";
            TextBoxInfo.Text = "";
            TextBoxCC.Text = "";
            TextBoxValhalla.Text = "";
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

        private void IconButtonConfirm_Click(object sender, EventArgs e)
        {

        }


        //private async Task<bool> BillingAddress()
        //{
        //    try
        //    {
        //        await browser.Screenshot("13.BillingAddress");
        //        await ConsoleProgressGeneral("Interrelacionando distribución regular de caos.", 75);
        //        await browser.DisableAlerts();

        //        await browser.SendKeys("input[name=firstName]", Info[0][0]);
        //        await browser.SendKeys("input[name=lastName]", Info[0][1]);
        //        await browser.SendKeys("input[name=streetAddress]", await TrickAddress(Info[0][2]));
        //        await browser.SendKeys("input[name=extendedAddress]", Info[0][3]);
        //        await browser.SendKeys("input[name=locality", Info[0][4]);
        //        await browser.SendKeyCode(0x09);
        //        await browser.Click("select[name=region]");
        //        await browser.SendKeys(string.Empty, Info[0][5]);
        //        await browser.Click("select[name=region]");
        //        await browser.SendKeyCode(0x09);
        //        await browser.SendKeys("input[name=postalCode]", Info[0][6]);
        //        await browser.SendKeyCode(0x09);
        //        await browser.SendKeys("input[name=phone]", Info[0][8]);
        //        await Task.Delay(500);
        //        await browser.Click("button[form=add-billing-address-form]");//

        //    }
        //    catch (Exception) { }
        //    await browser.Scroll(200);
        //    bool divToastBody = await browser.AllElementInnerTextEquals("div[role=alert].Toastify__toast-body", "Your changes have been saved", "13.ExceptionBillingAddress", 60);
        //    if (divToastBody)
        //    {
        //        //Task.Delay(5000);
        //        //// await browser.Screenshot("6._");
        //        return await Products();
        //    }

        //    await ConsoleProgressGeneral("Interrelacionando distribución regular de caos. ¡Fallo!", 75, "Fail");
        //    return false;

        //}

        //private async Task<bool> Last_()
        //{
        //    try
        //    {
        //        await browser.Screenshot("12.Last");
        //        await ConsoleProgressGeneral("Difundiendo rumores.", 70);
        //        string listLiveItem = string.Empty;
        //        if (Items.Count > 0)
        //        {
        //            int ItemsCount = Items.Count;
        //            double x = 70;
        //            for (int i = 0; i < ItemsCount;)
        //            {

        //                //await Products();
        //                //final


        //                //}
        //                //x++;
        //            }
        //        }
        //    }
        //    catch (Exception) { }

        //    if (CreditCards.Count > 0)
        //    {
        //        await ConsoleProgressGeneral("Difundiendo rumores. ¡Fallo!", 90, "Fail");
        //        return false;
        //    }
        //    return true;
        //}
    }
}
