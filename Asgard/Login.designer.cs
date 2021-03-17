namespace Asgard
{
    partial class Login
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.PanelHeader = new System.Windows.Forms.Panel();
            this.IconButtonIco = new FontAwesome.Sharp.IconButton();
            this.LabelTitleToolBar = new System.Windows.Forms.Label();
            this.IconButtonClose = new FontAwesome.Sharp.IconButton();
            this.PanelFooter = new System.Windows.Forms.Panel();
            this.LabelUser = new System.Windows.Forms.Label();
            this.TextBoxUsernameOrEmail = new System.Windows.Forms.TextBox();
            this.TextBoxPassword = new System.Windows.Forms.TextBox();
            this.LabelPassword = new System.Windows.Forms.Label();
            this.LinkLabelForgotPassword = new System.Windows.Forms.LinkLabel();
            this.LabelLogo = new System.Windows.Forms.Label();
            this.LabelO = new System.Windows.Forms.Label();
            this.LabelInfo = new System.Windows.Forms.Label();
            this.PictureBoxLoading = new System.Windows.Forms.PictureBox();
            this.IconButtonRegistry = new FontAwesome.Sharp.IconButton();
            this.IconButtonSigin = new FontAwesome.Sharp.IconButton();
            this.IconPictureBoxLogo = new FontAwesome.Sharp.IconPictureBox();
            this.PanelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxLoading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconPictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(38)))), ((int)(((byte)(70)))));
            this.PanelHeader.Controls.Add(this.IconButtonIco);
            this.PanelHeader.Controls.Add(this.LabelTitleToolBar);
            this.PanelHeader.Controls.Add(this.IconButtonClose);
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(300, 30);
            this.PanelHeader.TabIndex = 0;
            this.PanelHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelHeader_MouseDown);
            // 
            // IconButtonIco
            // 
            this.IconButtonIco.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IconButtonIco.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconButtonIco.FlatAppearance.BorderSize = 0;
            this.IconButtonIco.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconButtonIco.IconChar = FontAwesome.Sharp.IconChar.GalacticSenate;
            this.IconButtonIco.IconColor = System.Drawing.Color.White;
            this.IconButtonIco.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconButtonIco.IconSize = 24;
            this.IconButtonIco.Location = new System.Drawing.Point(3, 3);
            this.IconButtonIco.Margin = new System.Windows.Forms.Padding(0);
            this.IconButtonIco.Name = "IconButtonIco";
            this.IconButtonIco.Size = new System.Drawing.Size(24, 24);
            this.IconButtonIco.TabIndex = 17;
            this.IconButtonIco.UseVisualStyleBackColor = true;
            // 
            // LabelTitleToolBar
            // 
            this.LabelTitleToolBar.AutoSize = true;
            this.LabelTitleToolBar.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTitleToolBar.ForeColor = System.Drawing.Color.White;
            this.LabelTitleToolBar.Location = new System.Drawing.Point(34, 5);
            this.LabelTitleToolBar.Name = "LabelTitleToolBar";
            this.LabelTitleToolBar.Size = new System.Drawing.Size(66, 21);
            this.LabelTitleToolBar.TabIndex = 0;
            this.LabelTitleToolBar.Text = "Ingreso";
            // 
            // IconButtonClose
            // 
            this.IconButtonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IconButtonClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconButtonClose.FlatAppearance.BorderSize = 0;
            this.IconButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconButtonClose.IconChar = FontAwesome.Sharp.IconChar.Times;
            this.IconButtonClose.IconColor = System.Drawing.Color.White;
            this.IconButtonClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconButtonClose.IconSize = 24;
            this.IconButtonClose.Location = new System.Drawing.Point(273, 3);
            this.IconButtonClose.Margin = new System.Windows.Forms.Padding(0);
            this.IconButtonClose.Name = "IconButtonClose";
            this.IconButtonClose.Size = new System.Drawing.Size(24, 24);
            this.IconButtonClose.TabIndex = 7;
            this.IconButtonClose.UseVisualStyleBackColor = true;
            this.IconButtonClose.Click += new System.EventHandler(this.IconButtonClose_Click);
            // 
            // PanelFooter
            // 
            this.PanelFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(38)))), ((int)(((byte)(70)))));
            this.PanelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelFooter.Location = new System.Drawing.Point(0, 495);
            this.PanelFooter.Name = "PanelFooter";
            this.PanelFooter.Size = new System.Drawing.Size(300, 5);
            this.PanelFooter.TabIndex = 0;
            // 
            // LabelUser
            // 
            this.LabelUser.AutoSize = true;
            this.LabelUser.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelUser.ForeColor = System.Drawing.Color.Silver;
            this.LabelUser.Location = new System.Drawing.Point(25, 196);
            this.LabelUser.Name = "LabelUser";
            this.LabelUser.Size = new System.Drawing.Size(98, 13);
            this.LabelUser.TabIndex = 0;
            this.LabelUser.Text = "Usuario o Correo.";
            // 
            // TextBoxUsernameOrEmail
            // 
            this.TextBoxUsernameOrEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxUsernameOrEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(94)))), ((int)(((byte)(129)))));
            this.TextBoxUsernameOrEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxUsernameOrEmail.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxUsernameOrEmail.ForeColor = System.Drawing.Color.White;
            this.TextBoxUsernameOrEmail.Location = new System.Drawing.Point(25, 213);
            this.TextBoxUsernameOrEmail.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.TextBoxUsernameOrEmail.Name = "TextBoxUsernameOrEmail";
            this.TextBoxUsernameOrEmail.Size = new System.Drawing.Size(250, 22);
            this.TextBoxUsernameOrEmail.TabIndex = 1;
            this.TextBoxUsernameOrEmail.TextChanged += new System.EventHandler(this.TextBoxUser_TextChanged);
            // 
            // TextBoxPassword
            // 
            this.TextBoxPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(94)))), ((int)(((byte)(129)))));
            this.TextBoxPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxPassword.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxPassword.ForeColor = System.Drawing.Color.White;
            this.TextBoxPassword.Location = new System.Drawing.Point(25, 266);
            this.TextBoxPassword.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.TextBoxPassword.Name = "TextBoxPassword";
            this.TextBoxPassword.PasswordChar = '$';
            this.TextBoxPassword.Size = new System.Drawing.Size(250, 22);
            this.TextBoxPassword.TabIndex = 2;
            this.TextBoxPassword.TextChanged += new System.EventHandler(this.TextBoxPassword_TextChanged);
            // 
            // LabelPassword
            // 
            this.LabelPassword.AutoSize = true;
            this.LabelPassword.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPassword.ForeColor = System.Drawing.Color.Silver;
            this.LabelPassword.Location = new System.Drawing.Point(25, 249);
            this.LabelPassword.Name = "LabelPassword";
            this.LabelPassword.Size = new System.Drawing.Size(69, 13);
            this.LabelPassword.TabIndex = 0;
            this.LabelPassword.Text = "Contraseña.";
            // 
            // LinkLabelForgotPassword
            // 
            this.LinkLabelForgotPassword.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(97)))), ((int)(((byte)(238)))));
            this.LinkLabelForgotPassword.AutoSize = true;
            this.LinkLabelForgotPassword.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LinkLabelForgotPassword.LinkColor = System.Drawing.Color.Silver;
            this.LinkLabelForgotPassword.Location = new System.Drawing.Point(145, 292);
            this.LinkLabelForgotPassword.Name = "LinkLabelForgotPassword";
            this.LinkLabelForgotPassword.Size = new System.Drawing.Size(128, 13);
            this.LinkLabelForgotPassword.TabIndex = 3;
            this.LinkLabelForgotPassword.TabStop = true;
            this.LinkLabelForgotPassword.Text = "¿Olvidaste la contraseña?";
            this.LinkLabelForgotPassword.VisitedLinkColor = System.Drawing.Color.Silver;
            this.LinkLabelForgotPassword.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelForgotPassword_LinkClicked);
            // 
            // LabelLogo
            // 
            this.LabelLogo.AutoSize = true;
            this.LabelLogo.BackColor = System.Drawing.Color.Transparent;
            this.LabelLogo.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelLogo.ForeColor = System.Drawing.Color.White;
            this.LabelLogo.Location = new System.Drawing.Point(87, 131);
            this.LabelLogo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelLogo.Name = "LabelLogo";
            this.LabelLogo.Size = new System.Drawing.Size(126, 33);
            this.LabelLogo.TabIndex = 0;
            this.LabelLogo.Text = "4SG4RD";
            // 
            // LabelO
            // 
            this.LabelO.AutoSize = true;
            this.LabelO.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelO.ForeColor = System.Drawing.Color.Silver;
            this.LabelO.Location = new System.Drawing.Point(142, 371);
            this.LabelO.Name = "LabelO";
            this.LabelO.Size = new System.Drawing.Size(16, 13);
            this.LabelO.TabIndex = 0;
            this.LabelO.Text = "Ó";
            // 
            // LabelInfo
            // 
            this.LabelInfo.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelInfo.ForeColor = System.Drawing.Color.White;
            this.LabelInfo.Location = new System.Drawing.Point(25, 478);
            this.LabelInfo.Name = "LabelInfo";
            this.LabelInfo.Size = new System.Drawing.Size(250, 12);
            this.LabelInfo.TabIndex = 0;
            this.LabelInfo.Text = "4SG4RD TEAM - 2021";
            this.LabelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PictureBoxLoading
            // 
            this.PictureBoxLoading.Image = global::Asgard.Properties.Resources.Eclipse_1s_32px1;
            this.PictureBoxLoading.Location = new System.Drawing.Point(134, 439);
            this.PictureBoxLoading.Name = "PictureBoxLoading";
            this.PictureBoxLoading.Size = new System.Drawing.Size(32, 32);
            this.PictureBoxLoading.TabIndex = 111;
            this.PictureBoxLoading.TabStop = false;
            // 
            // IconButtonRegistry
            // 
            this.IconButtonRegistry.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(38)))), ((int)(((byte)(70)))));
            this.IconButtonRegistry.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconButtonRegistry.FlatAppearance.BorderSize = 0;
            this.IconButtonRegistry.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconButtonRegistry.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IconButtonRegistry.ForeColor = System.Drawing.Color.White;
            this.IconButtonRegistry.IconChar = FontAwesome.Sharp.IconChar.UserPlus;
            this.IconButtonRegistry.IconColor = System.Drawing.Color.White;
            this.IconButtonRegistry.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconButtonRegistry.IconSize = 24;
            this.IconButtonRegistry.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.IconButtonRegistry.Location = new System.Drawing.Point(25, 391);
            this.IconButtonRegistry.Name = "IconButtonRegistry";
            this.IconButtonRegistry.Size = new System.Drawing.Size(250, 30);
            this.IconButtonRegistry.TabIndex = 6;
            this.IconButtonRegistry.Text = "REGISTRATE";
            this.IconButtonRegistry.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.IconButtonRegistry.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.IconButtonRegistry.UseVisualStyleBackColor = false;
            this.IconButtonRegistry.Click += new System.EventHandler(this.IconButtonRegistry_Click);
            // 
            // IconButtonSigin
            // 
            this.IconButtonSigin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(38)))), ((int)(((byte)(70)))));
            this.IconButtonSigin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconButtonSigin.FlatAppearance.BorderSize = 0;
            this.IconButtonSigin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconButtonSigin.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IconButtonSigin.ForeColor = System.Drawing.Color.White;
            this.IconButtonSigin.IconChar = FontAwesome.Sharp.IconChar.SignInAlt;
            this.IconButtonSigin.IconColor = System.Drawing.Color.White;
            this.IconButtonSigin.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconButtonSigin.IconSize = 24;
            this.IconButtonSigin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.IconButtonSigin.Location = new System.Drawing.Point(25, 332);
            this.IconButtonSigin.Name = "IconButtonSigin";
            this.IconButtonSigin.Size = new System.Drawing.Size(250, 30);
            this.IconButtonSigin.TabIndex = 4;
            this.IconButtonSigin.Text = "INGRESA";
            this.IconButtonSigin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.IconButtonSigin.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.IconButtonSigin.UseVisualStyleBackColor = false;
            this.IconButtonSigin.Click += new System.EventHandler(this.IconButtonSigin_Click);
            // 
            // IconPictureBoxLogo
            // 
            this.IconPictureBoxLogo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.IconPictureBoxLogo.BackColor = System.Drawing.Color.Transparent;
            this.IconPictureBoxLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.IconPictureBoxLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconPictureBoxLogo.IconChar = FontAwesome.Sharp.IconChar.GalacticSenate;
            this.IconPictureBoxLogo.IconColor = System.Drawing.Color.White;
            this.IconPictureBoxLogo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconPictureBoxLogo.IconSize = 126;
            this.IconPictureBoxLogo.Location = new System.Drawing.Point(87, 33);
            this.IconPictureBoxLogo.Margin = new System.Windows.Forms.Padding(0);
            this.IconPictureBoxLogo.Name = "IconPictureBoxLogo";
            this.IconPictureBoxLogo.Size = new System.Drawing.Size(126, 128);
            this.IconPictureBoxLogo.TabIndex = 15;
            this.IconPictureBoxLogo.TabStop = false;
            // 
            // Login
            // 
            this.AcceptButton = this.IconButtonSigin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(59)))), ((int)(((byte)(104)))));
            this.ClientSize = new System.Drawing.Size(300, 500);
            this.Controls.Add(this.PictureBoxLoading);
            this.Controls.Add(this.LabelInfo);
            this.Controls.Add(this.IconButtonRegistry);
            this.Controls.Add(this.LabelO);
            this.Controls.Add(this.IconButtonSigin);
            this.Controls.Add(this.LabelLogo);
            this.Controls.Add(this.IconPictureBoxLogo);
            this.Controls.Add(this.LinkLabelForgotPassword);
            this.Controls.Add(this.TextBoxPassword);
            this.Controls.Add(this.LabelPassword);
            this.Controls.Add(this.TextBoxUsernameOrEmail);
            this.Controls.Add(this.LabelUser);
            this.Controls.Add(this.PanelFooter);
            this.Controls.Add(this.PanelHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Silver;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ingreso.";
            this.Load += new System.EventHandler(this.Login_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Login_Paint);
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxLoading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconPictureBoxLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel PanelHeader;
        private System.Windows.Forms.Panel PanelFooter;
        private System.Windows.Forms.Label LabelUser;
        private System.Windows.Forms.TextBox TextBoxUsernameOrEmail;
        private System.Windows.Forms.TextBox TextBoxPassword;
        private System.Windows.Forms.Label LabelPassword;
        private System.Windows.Forms.LinkLabel LinkLabelForgotPassword;
        private System.Windows.Forms.Label LabelLogo;
        private FontAwesome.Sharp.IconPictureBox IconPictureBoxLogo;
        private FontAwesome.Sharp.IconButton IconButtonSigin;
        private FontAwesome.Sharp.IconButton IconButtonRegistry;
        private System.Windows.Forms.Label LabelO;
        private System.Windows.Forms.Label LabelInfo;
        private System.Windows.Forms.PictureBox PictureBoxLoading;
        private FontAwesome.Sharp.IconButton IconButtonClose;
        private System.Windows.Forms.Label LabelTitleToolBar;
        private FontAwesome.Sharp.IconButton IconButtonIco;
    }
}

