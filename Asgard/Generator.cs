using System;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;

using CefSharp;
using CefSharp.OffScreen;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Asgard
{
    public partial class Generator : Form
    {
        private string Bin { set; get; }
        private string Month { set; get; }
        private string Year { set; get; }
        private string Cvv { set; get; }
        private string Quatity { set; get; }

        List<List<string>> CreditCards { set; get; }
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

        private string cvvDigits = "###";

        public Generator()
        {
            InitializeComponent();
        }

        private void Generator_Load(object sender, EventArgs e)
        {
            //this.ActiveControl = LabelLogo;
            TextBoxBin.Placeholder("######xxxxxxxxxx");
            MaskedTextBoxDate.Placeholder("dd/aa");
            TextBoxCvv.Placeholder(cvvDigits);
            TextBoxCvv.MaxLength = 3;
        }

        private async void IconButtonGenerar_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                string creditCards = await GenerateCreditCardsAsync();
                TextBoxCC.Text = creditCards.ToString();
                LabelCountCC.Text = TextBoxCC.Lines.Count().ToString();
            }
        }


        private Task<string> GenerateCreditCardsAsync()
        {
            Task<string> listCreditCards = Task<string>.Factory.StartNew(
                () =>
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
                                precision.Add(j, Int32.Parse(match.Value));
                            }
                        }

                        if (precision.Count > 0)
                        {
                            RangeAndLength card = range[precision.OrderByDescending(x => x.Value).First().Key];
                            int cardLenght = card.Length;
                            Quatity = TextBoxQuantity.Text;
                            CreditCards = new List<List<string>>();
                            for (int i = 0; i < Int32.Parse(Quatity); i++)
                            {
                                CreditCards.Add(new List<string>());
                                CreditCards[i].Add(CreateCardNumber(bin, cardLenght));
                                CreditCards[i].Add(GetDate()[0]);
                                CreditCards[i].Add(GetDate()[1]);
                                CreditCards[i].Add(GetCvv());
                                System.Threading.Thread.Sleep(50);

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
                    return listCreditCard;
                });
            return listCreditCards;
        }


        #region Process Bin
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


        private static string[] GenerateRange(string[] rangesList)
        {
            List<string> range = new List<string>();
            foreach (string rangeList in rangesList)
            {
                if (rangeList.Contains("-"))
                {
                    string[] ranges = rangeList.Split('-');
                    int start = Int32.Parse(ranges[0]);
                    int end = Int32.Parse(ranges[1]);

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


        private static IEnumerable<RangeAndLength> BuildRangeAndLengthList(string[] rangeList, int length)
        {
            string[] x = GenerateRange(rangeList);
            return from range in x select new RangeAndLength(range, length);
        }

        /// <summary>
        /// This is an example how BuildPrefixAndLengthList can be used
        /// </summary>
        /// <returns></returns>
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
                        precision.Add(j, Int32.Parse(match.Value));
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
            int checkdigit =
                Convert.ToInt32((Math.Floor((decimal)sum / 10) + 1) * 10 - sum) % 10;

            ccnumber += checkdigit;

            return ccnumber;

        }
        #endregion

        #region Validations
        #region Validations TextBoxBin
        private void TextBoxBin_Enter(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Placeholder("######xxxxxxxxxx");
        }
        private void TextBoxBin_Leave(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Placeholder("######xxxxxxxxxx");
            CompleteBin(textBox);
        }
        private void TextBoxBin_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.TextLength <= 6)
            {
                if (Regex.IsMatch(textBox.Text, @"([^0-9])"))
                {
                    textBox.Text = textBox.Text.Remove(textBox.TextLength - 1);
                    textBox.Select(textBox.TextLength, 0);
                }
            }
            else if (Regex.IsMatch(textBox.Text, @"([^x-xX-X0-9])"))
            {
                textBox.Text = textBox.Text.Remove(textBox.TextLength - 1);
                textBox.Select(textBox.TextLength, 0);
            }
        }
        private void TextBoxBin_TextChanged(object sender, EventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                TextBox textBox = (TextBox)sender;
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
            }
        }
        private void TextBoxBin_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string text = textBox.Text;
            if (text == "######xxxxxxxxxx")
                text = string.Empty;

            if (string.IsNullOrEmpty(text))
            {
                e.Cancel = true;
                textBox.Focus();
                ErrorProvider.SetError(textBox, "¡El Bin es Obligatorio!");
            }
            else
            {
                ErrorProvider.Clear();
                if (text.Length < 6)
                {
                    e.Cancel = true;
                    textBox.Focus();
                    ErrorProvider.SetError(textBox, "¡El Bin debe contener minimo 6 digitos!");
                }
                else
                {
                    ErrorProvider.Clear();
                    if (!ValidateBin(text))
                    {
                        e.Cancel = true;
                        textBox.Focus();
                        ErrorProvider.SetError(textBox, "¡El Bin es Invalido!");
                    }
                    else
                    {
                        e.Cancel = false;
                        ErrorProvider.Clear();
                    }
                }
            }
        }
        #endregion
        #region Validation MaskedBoxDate
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
        #region Validation TxtBoxCvv
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
        #region Validation TextBoxQuantity

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
        private bool ValidateBin(string text)
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
        #endregion


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


        #region Control Forms
        private void Generator_Paint(object sender, PaintEventArgs e)
        {
            Rectangle borderTextBoxBin = new Rectangle(TextBoxBin.Location.X, TextBoxBin.Location.Y, TextBoxBin.ClientSize.Width, TextBoxBin.ClientSize.Height);
            borderTextBoxBin.Inflate(1, 1); // border thickness
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxBin, Color.White, ButtonBorderStyle.Solid);

            Rectangle borderMaskedTextBoxDate = new Rectangle(MaskedTextBoxDate.Location.X, MaskedTextBoxDate.Location.Y, MaskedTextBoxDate.ClientSize.Width, MaskedTextBoxDate.ClientSize.Height);
            borderMaskedTextBoxDate.Inflate(1, 1); // border thickness
            ControlPaint.DrawBorder(e.Graphics, borderMaskedTextBoxDate, Color.White, ButtonBorderStyle.Solid);

            Rectangle borderTextBoxCvv = new Rectangle(TextBoxCvv.Location.X, TextBoxCvv.Location.Y, TextBoxCvv.ClientSize.Width, TextBoxCvv.ClientSize.Height);
            borderTextBoxCvv.Inflate(1, 1); // border thickness
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxCvv, Color.White, ButtonBorderStyle.Solid);

            Rectangle borderTextBoxQuantity = new Rectangle(TextBoxQuantity.Location.X, TextBoxQuantity.Location.Y, TextBoxQuantity.ClientSize.Width, TextBoxQuantity.ClientSize.Height);
            borderTextBoxQuantity.Inflate(1, 1); // border thickness
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxQuantity, Color.White, ButtonBorderStyle.Solid);

            Rectangle borderTextBoxCC = new Rectangle(TextBoxCC.Location.X, TextBoxCC.Location.Y, TextBoxCC.ClientSize.Width + 17, TextBoxCC.ClientSize.Height);
            borderTextBoxCC.Inflate(1, 1); // border thickness
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxCC, Color.White, ButtonBorderStyle.Solid);

        }
        #endregion

        private void TextBoxCC_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            LabelCountCC.Text = textBox.Lines.Count().ToString();
        }
    }
}