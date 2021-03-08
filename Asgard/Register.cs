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
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;

namespace Asgard
{
    public partial class Register : Form
    {
        private string Username { set; get; }
        private string Email { set; get; }
        private string Password { set; get; }

        private bool validateUser = false;
        private bool validateEmail = false;
        private bool validatePassword = false;
        private bool validateConfirmPassword = false;

        AutoCompleteStringCollection usernames;


        private string Token { set; get; }
        private string Message { set; get; }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hwnd, int wmsg, int wparam, int lparam);

        public Register()
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


        //private async void IconButtonSigin_Click(object sender, EventArgs e)
        //{
        //if (ValidateChildren(ValidationConstraints.Enabled))
        //{
        //    User = TextBoxUser.Text;
        //    Password = TextBoxPassword.Text.CreateMD5();
        //    string token = await Asatru.GenerateToken(User, Password);

        //    try
        //    {
        //        new JwtSecurityToken(jwtEncodedString: token);
        //        this.Hide();
        //        Checker checker = new Checker(token);
        //        checker.Show();
        //    }
        //    catch (Exception)
        //    {
        //        MessageBox.Show(token);
        //    }
        //}
        //}

        private void Register_Paint(object sender, PaintEventArgs e)
        {
            Rectangle borderTextBoxUser = new Rectangle(TextBoxUser.Location.X, TextBoxUser.Location.Y, TextBoxUser.ClientSize.Width, TextBoxUser.ClientSize.Height);
            borderTextBoxUser.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxUser, Color.White, ButtonBorderStyle.Solid);

            Rectangle borderTextBoxEmail = new Rectangle(TextBoxEmail.Location.X, TextBoxEmail.Location.Y, TextBoxEmail.ClientSize.Width, TextBoxEmail.ClientSize.Height);
            borderTextBoxEmail.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxEmail, Color.White, ButtonBorderStyle.Solid);

            Rectangle borderTextBoxPassword = new Rectangle(TextBoxPassword.Location.X, TextBoxPassword.Location.Y, TextBoxPassword.ClientSize.Width, TextBoxPassword.ClientSize.Height);
            borderTextBoxPassword.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxPassword, Color.White, ButtonBorderStyle.Solid);

            Rectangle borderTextBoxConfirmPassword = new Rectangle(TextBoxConfirmPassword.Location.X, TextBoxConfirmPassword.Location.Y, TextBoxConfirmPassword.ClientSize.Width, TextBoxConfirmPassword.ClientSize.Height);
            borderTextBoxConfirmPassword.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxConfirmPassword, Color.White, ButtonBorderStyle.Solid);

            Rectangle borderTextBoxReferred = new Rectangle(TextBoxReferred.Location.X, TextBoxReferred.Location.Y, TextBoxReferred.ClientSize.Width, TextBoxReferred.ClientSize.Height);
            borderTextBoxReferred.Inflate(1, 1);
            ControlPaint.DrawBorder(e.Graphics, borderTextBoxReferred, Color.White, ButtonBorderStyle.Solid);
        }

        private async void Register_Load(object sender, EventArgs e)
        {
            PictureBoxLoading.Visible = false;
            //IconButtonRegister.Enabled = false;
            //IconButtonRegister.IconColor = Color.Black;

            try
            {
                usernames = new AutoCompleteStringCollection();
                dynamic referred = await Asatru.Referred(3);
                if (referred != null)
                {
                    foreach (var r in referred)
                    {
                        usernames.Add(r.username.Value);
                    }
                }
                TextBoxReferred.AutoCompleteCustomSource = usernames;
                TextBoxReferred.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                TextBoxReferred.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            }
            catch (Exception) { }
        }

        private void IconButtonBack_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.Show();
        }

        private void TextBoxUser_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            ValidateData();
        }

        private void TextBoxEmail_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            ValidateData();
            //if (TextBoxUser.TextLength > 5)
            //{
            //    validateUser = true;
            //    if (Asatru.ValidateEmail(textBox.Text))
            //    {
            //        validateEmail = true;
            //        if (TextBoxPassword.TextLength > 7)
            //        {
            //            validatePassword = true;
            //            if (TextBoxConfirmPassword.Text == TextBoxPassword.Text)
            //            {
            //                ConsoleProgress("4SG4RD TEAM - 2020");
            //                validateConfirmPassword = true;
            //            }
            //            else
            //            {
            //                ConsoleProgress("La contraseña no coincide.", "Fail");
            //                validateConfirmPassword = false;
            //            }
            //        }
            //        else
            //        {
            //            ConsoleProgress("La contraseña debe contener minimo 8 caracteres.", "Fail");
            //            validatePassword = false;
            //        }
            //    }
            //    else
            //    {
            //        ConsoleProgress("Debe ingresar un correo valido.", "Fail");
            //        validateEmail = false;
            //    }
            //}
            //else
            //{
            //    ConsoleProgress("El nombre de usuario debe contener minimo 6 caracteres.", "Fail");
            //    validateUser = false;
            //}
            //ActiveButtonRegister();
        }

        private void TextBoxPassword_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            ValidateData();
            //if (TextBoxUser.TextLength > 5)
            //{
            //    validateUser = true;
            //    if (Asatru.ValidateEmail(TextBoxEmail.Text))
            //    {
            //        validateEmail = true;
            //        if (textBox.TextLength > 7)
            //        {
            //            validatePassword = true;
            //            if (TextBoxConfirmPassword.Text == TextBoxPassword.Text)
            //            {
            //                ConsoleProgress("4SG4RD TEAM - 2020");
            //                validateConfirmPassword = true;
            //            }
            //            else
            //            {
            //                ConsoleProgress("La contraseña no coincide.", "Fail");
            //                validateConfirmPassword = false;
            //            }
            //        }
            //        else
            //        {
            //            ConsoleProgress("La contraseña debe contener minimo 8 caracteres.", "Fail");
            //            validatePassword = false;
            //        }
            //    }
            //    else
            //    {
            //        ConsoleProgress("Debe ingresar un correo valido.", "Fail");
            //        validateEmail = false;
            //    }
            //}
            //else
            //{
            //    ConsoleProgress("El nombre de usuario debe contener minimo 6 caracteres.", "Fail");
            //    validateUser = false;
            //}
            //ActiveButtonRegister();
        }

        private void TextBoxConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            ValidateData();
            //if (TextBoxUser.TextLength > 5)
            //{
            //    validateUser = true;
            //    if (Asatru.ValidateEmail(TextBoxEmail.Text))
            //    {
            //        validateEmail = true;
            //        if (TextBoxPassword.TextLength > 7)
            //        {
            //            validatePassword = true;
            //            if (textBox.Text == TextBoxPassword.Text)
            //            {
            //                ConsoleProgress("4SG4RD TEAM - 2020");
            //                validateConfirmPassword = true;
            //            }
            //            else
            //            {
            //                ConsoleProgress("La contraseña no coincide.", "Fail");
            //                validateConfirmPassword = false;
            //            }
            //        }
            //        else
            //        {
            //            ConsoleProgress("La contraseña debe contener minimo 8 caracteres.", "Fail");
            //            validatePassword = false;
            //        }
            //    }
            //    else
            //    {
            //        ConsoleProgress("Debe ingresar un correo valido.", "Fail");
            //        validateEmail = false;
            //    }
            //}
            //else
            //{
            //    ConsoleProgress("El nombre de usuario debe contener minimo 6 caracteres.", "Fail");
            //    validateUser = false;
            //}
            //ActiveButtonRegister();
        }

        private async void ValidateData()
        {
            if (TextBoxUser.TextLength > 5)
            {
                if (!await Asatru.TakenUsername(TextBoxUser.Text))
                {
                    validateUser = true;

                    if (Asatru.ValidateEmail(TextBoxEmail.Text))
                    {
                        if (!await Asatru.TakenEmail(TextBoxEmail.Text))
                        {
                            validateEmail = true;

                            if (TextBoxPassword.TextLength > 7)
                            {
                                validatePassword = true;

                                if (TextBoxConfirmPassword.Text == TextBoxPassword.Text)
                                {
                                    await ConsoleProgress("4SG4RD TEAM - 2020");
                                    validateConfirmPassword = true;
                                }
                                else
                                {
                                    await ConsoleProgress("La contraseña no coincide.", "Fail");
                                    validateConfirmPassword = false;
                                }
                            }
                            else
                            {
                                await ConsoleProgress("La contraseña debe contener minimo 8 caracteres.", "Fail");
                                validatePassword = false;
                            }
                        }
                        else
                        {
                            await ConsoleProgress("Este correo ya se encuentra registrado.", "Fail");
                            validateEmail = false;
                        }
                    }
                    else
                    {
                        await ConsoleProgress("Debe ingresar un correo valido.", "Fail");
                        validateEmail = false;
                    }
                }
                else
                {
                    await ConsoleProgress("El nombre de usuario ya esta registrado.", "Fail");
                    validateUser = false;
                }
            }
            else
            {
                await ConsoleProgress("El nombre de usuario debe contener minimo 6 caracteres.", "Fail");
                validateUser = false;
            }


                //ActiveButtonRegister();
        }

        private async Task ConsoleProgress(string info, string status = null)
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

        private void ActiveButtonRegister()
        {
            //if (validateUser && validateEmail && validatePassword && validateConfirmPassword)
            //{
            //    IconButtonRegister.Enabled = true;
            //    IconButtonRegister.IconColor = Color.White;
            //}
            //else
            //{
            //    IconButtonRegister.Enabled = false;
            //    IconButtonRegister.IconColor = Color.Black;
            //}
        }

        private async void IconButtonRegister_Click(object sender, EventArgs e)
        {
            if (validateUser && validateEmail && validatePassword && validateConfirmPassword)
            {
                PictureBoxLoading.Visible = true;
                await ConsoleProgress("Creando nuevo usuario.");
                IconButtonRegister.Enabled = false;
                IconButtonRegister.IconColor = Color.Black;
                Username = TextBoxUser.Text.Trim();
                Email = TextBoxEmail.Text.Trim();
                string password = TextBoxPassword.Text.Trim();
                Password = password.CreateMD5();
                string referred = TextBoxReferred.Text.Trim();
                //if(TextBoxReferred.Text.Trim())
                bool addUser = await Asatru.AddUser(Username, Email, Password, referred);
                if (addUser)
                {
                    string token = await Asatru.GenerateToken(Email, Password);
                    try
                    {
                        #region htmlEmail
                        string htmlEmail = @"<!doctype html>
<html>
  <head>
    <meta name='viewport' content='width=device-width' />
    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
    <title>4SG4RD CLUB</title>
    <style>
      /* -------------------------------------
          GLOBAL RESETS
      ------------------------------------- */
      
      /*All the styling goes here*/
      
      img {
        border: none;
        -ms-interpolation-mode: bicubic;
        max-width: 100%; 
      }

      body {
        background-color: #f6f6f6;
        font-family: sans-serif;
        -webkit-font-smoothing: antialiased;
        font-size: 14px;
        line-height: 1.4;
        margin: 0;
        padding: 0;
        -ms-text-size-adjust: 100%;
        -webkit-text-size-adjust: 100%; 
      }

      table {
        border-collapse: separate;
        mso-table-lspace: 0pt;
        mso-table-rspace: 0pt;
        width: 100%; }
        table td {
          font-family: sans-serif;
          font-size: 14px;
          vertical-align: top; 
      }

      /* -------------------------------------
          BODY & CONTAINER
      ------------------------------------- */

      .body {
        background-color: #f6f6f6;
        width: 100%; 
      }

      /* Set a max-width, and make it display as block so it will automatically stretch to that width, but will also shrink down on a phone or something */
      .container {
        display: block;
        margin: 0 auto !important;
        /* makes it centered */
        max-width: 580px;
        padding: 10px;
        width: 580px; 
      }

      /* This should also be a block element, so that it will fill 100% of the .container */
      .content {
        box-sizing: border-box;
        display: block;
        margin: 0 auto;
        max-width: 580px;
        padding: 10px; 
      }

      /* -------------------------------------
          HEADER, FOOTER, MAIN
      ------------------------------------- */
      .main {
        background: #ffffff;
        border-radius: 3px;
        width: 100%; 
      }

      .wrapper {
        box-sizing: border-box;
        padding: 20px; 
      }

      .content-block {
        padding-bottom: 10px;
        padding-top: 10px;
      }

      .footer {
        clear: both;
        margin-top: 10px;
        text-align: center;
        width: 100%; 
      }
        .footer td,
        .footer p,
        .footer span,
        .footer a {
          color: #999999;
          font-size: 12px;
          text-align: center; 
      }

      /* -------------------------------------
          TYPOGRAPHY
      ------------------------------------- */
      h1,
      h2,
      h3,
      h4 {
        color: #000000;
        font-family: sans-serif;
        font-weight: 400;
        line-height: 1.4;
        margin: 0;
        margin-bottom: 30px; 
      }

      h1 {
        font-size: 35px;
        font-weight: 300;
        text-align: center;
        text-transform: capitalize; 
      }

      p,
      ul,
      ol {
        font-family: sans-serif;
        font-size: 14px;
        font-weight: normal;
        margin: 0;
        margin-bottom: 15px; 
      }
        p li,
        ul li,
        ol li {
          list-style-position: inside;
          margin-left: 5px; 
      }

      a {
        color: #3498db;
        text-decoration: underline; 
      }

      /* -------------------------------------
          BUTTONS
      ------------------------------------- */
      .btn {
        box-sizing: border-box;
        width: 100%; }
        .btn > tbody > tr > td {
          padding-bottom: 15px; }
        .btn table {
          width: auto; 
      }
        .btn table td {
          background-color: #ffffff;
          border-radius: 5px;
          text-align: center; 
      }
        .btn a {
          background-color: #ffffff;
          border: solid 1px #3498db;
          border-radius: 5px;
          box-sizing: border-box;
          color: #3498db;
          cursor: pointer;
          display: inline-block;
          font-size: 14px;
          font-weight: bold;
          margin: 0;
          padding: 12px 25px;
          text-decoration: none;
          text-transform: capitalize; 
      }

      .btn-primary table td {
        background-color: #3498db; 
      }

      .btn-primary a {
        background-color: #3498db;
        border-color: #3498db;
        color: #ffffff; 
      }

      /* -------------------------------------
          OTHER STYLES THAT MIGHT BE USEFUL
      ------------------------------------- */
      .last {
        margin-bottom: 0; 
      }

      .first {
        margin-top: 0; 
      }

      .align-center {
        text-align: center; 
      }

      .align-right {
        text-align: right; 
      }

      .align-left {
        text-align: left; 
      }

      .clear {
        clear: both; 
      }

      .mt0 {
        margin-top: 0; 
      }

      .mb0 {
        margin-bottom: 0; 
      }

      .preheader {
        color: transparent;
        display: none;
        height: 0;
        max-height: 0;
        max-width: 0;
        opacity: 0;
        overflow: hidden;
        mso-hide: all;
        visibility: hidden;
        width: 0; 
      }

      .powered-by a {
        text-decoration: none; 
      }

      hr {
        border: 0;
        border-bottom: 1px solid #f6f6f6;
        margin: 20px 0; 
      }

      /* -------------------------------------
          RESPONSIVE AND MOBILE FRIENDLY STYLES
      ------------------------------------- */
      @media only screen and (max-width: 620px) {
        table[class=body] h1 {
          font-size: 28px !important;
          margin-bottom: 10px !important; 
        }
        table[class=body] p,
        table[class=body] ul,
        table[class=body] ol,
        table[class=body] td,
        table[class=body] span,
        table[class=body] a {
          font-size: 16px !important; 
        }
        table[class=body] .wrapper,
        table[class=body] .article {
          padding: 10px !important; 
        }
        table[class=body] .content {
          padding: 0 !important; 
        }
        table[class=body] .container {
          padding: 0 !important;
          width: 100% !important; 
        }
        table[class=body] .main {
          border-left-width: 0 !important;
          border-radius: 0 !important;
          border-right-width: 0 !important; 
        }
        table[class=body] .btn table {
          width: 100% !important; 
        }
        table[class=body] .btn a {
          width: 100% !important; 
        }
        table[class=body] .img-responsive {
          height: auto !important;
          max-width: 100% !important;
          width: auto !important; 
        }
      }

      /* -------------------------------------
          PRESERVE THESE STYLES IN THE HEAD
      ------------------------------------- */
      @media all {
        .ExternalClass {
          width: 100%; 
        }
        .ExternalClass,
        .ExternalClass p,
        .ExternalClass span,
        .ExternalClass font,
        .ExternalClass td,
        .ExternalClass div {
          line-height: 100%; 
        }
        .apple-link a {
          color: inherit !important;
          font-family: inherit !important;
          font-size: inherit !important;
          font-weight: inherit !important;
          line-height: inherit !important;
          text-decoration: none !important; 
        }
        #MessageViewBody a {
          color: inherit;
          text-decoration: none;
          font-size: inherit;
          font-family: inherit;
          font-weight: inherit;
          line-height: inherit;
        }
        .btn-primary table td:hover {
          background-color: #34495e !important; 
        }
        .btn-primary a:hover {
          background-color: #34495e !important;
          border-color: #34495e !important; 
        } 
      }

    </style>
  </head>
  <body class=''>
    <span class='preheader'>This is preheader text. Some clients will show this text as a preview.</span>
    <table role='presentation' border='0' cellpadding='0' cellspacing='0' class='body'>
      <tr>
        <td>&nbsp;</td>
        <td class='container'>
          <div class='content'>

            <!-- START CENTERED WHITE CONTAINER -->
            <table role='presentation' class='main'>

              <!-- START MAIN CONTENT AREA -->
              <tr>
                <td class='wrapper'>
                  <table role='presentation' border='0' cellpadding='0' cellspacing='0'>
                    <tr>
                      <td>
                        <p>Hola Asgardiano,</p>
                        <p>Ahora eres bienvenido a 4sg4rd.</p>
                        <table role='presentation' border='0' cellpadding='0' cellspacing='0' class='btn btn-primary'>
                          <tbody>
                            <tr>
                              <td align='left'>
                                <table role='presentation' border='0' cellpadding='0' cellspacing='0'>
                                  <tbody>
                                    <tr>

                                    </tr>
                                  </tbody>
                                </table>
                              </td>
                            </tr>
                          </tbody>
                        </table>
                        <p>" + $"{Username}<br/>{Email}<br/>{password}" + @"</p>
                        <p>Good luck! Hope it works.</p>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>

            <!-- END MAIN CONTENT AREA -->
            </table>
            <!-- END CENTERED WHITE CONTAINER -->

            <!-- START FOOTER -->
            <div class='footer'>
              <table role='presentation' border='0' cellpadding='0' cellspacing='0'>
                <tr>
                  <td class='content-block'>
                    <span class='apple-link'>4SG4RD T34M</span>

                  </td>
                </tr>
                <tr>
                  <td class='content-block powered-by'>
                  </td>
                </tr>
              </table>
            </div>
            <!-- END FOOTER -->

          </div>
        </td>
        <td>&nbsp;</td>
      </tr>
    </table>
  </body>
</html>
";
                        #endregion
                        await ConsoleProgress("Enviando Correo.");
                        await Asatru.SendEmail(Email, Username, "Registro en 4SG4RD CLUB", htmlEmail);
                        //if ()
                        //{

                        await ConsoleProgress("Usuario creado correctamente.", "Success");
                        int id = int.Parse(new JwtSecurityToken(jwtEncodedString: token).Claims.First(c => c.Type == "id").Value);
                        await ConsoleProgress("Creando sesion.");
                        if (await Asatru.CreateSession(id, token))
                        {
                            this.Hide();
                            Checker checker = new Checker(id, token);
                            checker.Show();
                        }
                        else
                        {
                            await ConsoleProgress("Error creando sesion.", "Fail");
                        }
                        PictureBoxLoading.Visible = false;
                        //}
                        //else
                        //{
                        //    await ConsoleProgress("Error enviando Correo.", "Fail");
                        //}
                    }
                    catch (Exception ex)
                    {
                        //throw;
                        await ConsoleProgress(ex.Message, "Fail");
                        IconButtonRegister.Enabled = true;
                        IconButtonRegister.IconColor = Color.White;
                    }
                }
                else
                {
                    await ConsoleProgress("Error creando Usuario", "Fail");
                    PictureBoxLoading.Visible = false;
                    IconButtonRegister.Enabled = true;
                    IconButtonRegister.IconColor = Color.White;
                }
            }
            else
            {
                ValidateData();
            }
        }

        private void TextBoxReferred_TextChanged(object sender, EventArgs e)
        {
            //if (!ModifierKeys.HasFlag(Keys.Back))
            //{
            //TextBox textBox = (TextBox)sender;
            //string text = textBox.Text.Trim();
            //if (textBox.TextLength >= 3)
            //{
            //    try
            //    {
            //        //AutoCompleteStringCollection usernames = new AutoCompleteStringCollection();
            //        if (text != null)
            //        {

            //        }
            //    }
            //    catch (Exception) { }
            //    //  }
            //}
        }

        private void TextBoxUser_Leave(object sender, EventArgs e)
        {
            //Validate();
        }
    }
}
