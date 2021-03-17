namespace Asgard
{
    partial class Password
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Password));
            this.PanelHeader = new System.Windows.Forms.Panel();
            this.IconButtonClose = new FontAwesome.Sharp.IconButton();
            this.IconButtonBack = new FontAwesome.Sharp.IconButton();
            this.labelTitleToolBar = new System.Windows.Forms.Label();
            this.PanelFooter = new System.Windows.Forms.Panel();
            this.LabelUser = new System.Windows.Forms.Label();
            this.TextBoxUsernameOrEmail = new System.Windows.Forms.TextBox();
            this.LabelInfo = new System.Windows.Forms.Label();
            this.PictureBoxLoading = new System.Windows.Forms.PictureBox();
            this.IconButtonGenerateNewPassword = new FontAwesome.Sharp.IconButton();
            this.LabelDescription = new System.Windows.Forms.Label();
            this.LabelLogo = new System.Windows.Forms.Label();
            this.IconPictureBoxLogo = new FontAwesome.Sharp.IconPictureBox();
            this.PanelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxLoading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconPictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(38)))), ((int)(((byte)(70)))));
            this.PanelHeader.Controls.Add(this.IconButtonClose);
            this.PanelHeader.Controls.Add(this.IconButtonBack);
            this.PanelHeader.Controls.Add(this.labelTitleToolBar);
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(300, 30);
            this.PanelHeader.TabIndex = 150;
            this.PanelHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelHeader_MouseDown);
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
            this.IconButtonClose.TabIndex = 101;
            this.IconButtonClose.UseVisualStyleBackColor = true;
            this.IconButtonClose.Click += new System.EventHandler(this.IconButtonClose_Click);
            // 
            // IconButtonBack
            // 
            this.IconButtonBack.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IconButtonBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconButtonBack.FlatAppearance.BorderSize = 0;
            this.IconButtonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconButtonBack.IconChar = FontAwesome.Sharp.IconChar.ArrowCircleLeft;
            this.IconButtonBack.IconColor = System.Drawing.Color.White;
            this.IconButtonBack.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconButtonBack.IconSize = 24;
            this.IconButtonBack.Location = new System.Drawing.Point(3, 3);
            this.IconButtonBack.Margin = new System.Windows.Forms.Padding(0);
            this.IconButtonBack.Name = "IconButtonBack";
            this.IconButtonBack.Size = new System.Drawing.Size(24, 24);
            this.IconButtonBack.TabIndex = 100;
            this.IconButtonBack.UseVisualStyleBackColor = true;
            this.IconButtonBack.Click += new System.EventHandler(this.IconButtonBack_Click);
            // 
            // labelTitleToolBar
            // 
            this.labelTitleToolBar.AutoSize = true;
            this.labelTitleToolBar.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitleToolBar.ForeColor = System.Drawing.Color.White;
            this.labelTitleToolBar.Location = new System.Drawing.Point(31, 5);
            this.labelTitleToolBar.Name = "labelTitleToolBar";
            this.labelTitleToolBar.Size = new System.Drawing.Size(169, 21);
            this.labelTitleToolBar.TabIndex = 113;
            this.labelTitleToolBar.Text = "Recuperar contraseña";
            this.labelTitleToolBar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PanelFooter
            // 
            this.PanelFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(38)))), ((int)(((byte)(70)))));
            this.PanelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelFooter.Location = new System.Drawing.Point(0, 423);
            this.PanelFooter.Name = "PanelFooter";
            this.PanelFooter.Size = new System.Drawing.Size(300, 5);
            this.PanelFooter.TabIndex = 140;
            // 
            // LabelUser
            // 
            this.LabelUser.AutoSize = true;
            this.LabelUser.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelUser.ForeColor = System.Drawing.Color.Silver;
            this.LabelUser.Location = new System.Drawing.Point(25, 260);
            this.LabelUser.Name = "LabelUser";
            this.LabelUser.Size = new System.Drawing.Size(98, 13);
            this.LabelUser.TabIndex = 130;
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
            this.TextBoxUsernameOrEmail.Location = new System.Drawing.Point(25, 277);
            this.TextBoxUsernameOrEmail.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.TextBoxUsernameOrEmail.Name = "TextBoxUsernameOrEmail";
            this.TextBoxUsernameOrEmail.Size = new System.Drawing.Size(250, 22);
            this.TextBoxUsernameOrEmail.TabIndex = 1;
            this.TextBoxUsernameOrEmail.TextChanged += new System.EventHandler(this.TextBoxUsernameOrEmail_TextChanged);
            // 
            // LabelInfo
            // 
            this.LabelInfo.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelInfo.ForeColor = System.Drawing.Color.White;
            this.LabelInfo.Location = new System.Drawing.Point(25, 407);
            this.LabelInfo.Name = "LabelInfo";
            this.LabelInfo.Size = new System.Drawing.Size(250, 12);
            this.LabelInfo.TabIndex = 110;
            this.LabelInfo.Text = "4SG4RD TEAM - 2021";
            this.LabelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PictureBoxLoading
            // 
            this.PictureBoxLoading.Image = global::Asgard.Properties.Resources.Eclipse_1s_32px1;
            this.PictureBoxLoading.Location = new System.Drawing.Point(134, 366);
            this.PictureBoxLoading.Name = "PictureBoxLoading";
            this.PictureBoxLoading.Size = new System.Drawing.Size(32, 32);
            this.PictureBoxLoading.TabIndex = 111;
            this.PictureBoxLoading.TabStop = false;
            // 
            // IconButtonGenerateNewPassword
            // 
            this.IconButtonGenerateNewPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(38)))), ((int)(((byte)(70)))));
            this.IconButtonGenerateNewPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconButtonGenerateNewPassword.FlatAppearance.BorderSize = 0;
            this.IconButtonGenerateNewPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconButtonGenerateNewPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IconButtonGenerateNewPassword.ForeColor = System.Drawing.Color.White;
            this.IconButtonGenerateNewPassword.IconChar = FontAwesome.Sharp.IconChar.Key;
            this.IconButtonGenerateNewPassword.IconColor = System.Drawing.Color.White;
            this.IconButtonGenerateNewPassword.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconButtonGenerateNewPassword.IconSize = 24;
            this.IconButtonGenerateNewPassword.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.IconButtonGenerateNewPassword.Location = new System.Drawing.Point(25, 318);
            this.IconButtonGenerateNewPassword.Name = "IconButtonGenerateNewPassword";
            this.IconButtonGenerateNewPassword.Size = new System.Drawing.Size(250, 30);
            this.IconButtonGenerateNewPassword.TabIndex = 2;
            this.IconButtonGenerateNewPassword.Text = "GENERAR";
            this.IconButtonGenerateNewPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.IconButtonGenerateNewPassword.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.IconButtonGenerateNewPassword.UseVisualStyleBackColor = false;
            this.IconButtonGenerateNewPassword.Click += new System.EventHandler(this.IconButtonGenerateNewPassword_Click);
            // 
            // LabelDescription
            // 
            this.LabelDescription.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelDescription.ForeColor = System.Drawing.Color.Silver;
            this.LabelDescription.Location = new System.Drawing.Point(25, 190);
            this.LabelDescription.Name = "LabelDescription";
            this.LabelDescription.Size = new System.Drawing.Size(251, 54);
            this.LabelDescription.TabIndex = 105;
            this.LabelDescription.Text = "¿Un severo caso de amnesia? Tranquilo.  4SG4RD generara una nueva contraseña, la " +
    "cual sera enviada a su correo.";
            this.LabelDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.LabelLogo.TabIndex = 151;
            this.LabelLogo.Text = "4SG4RD";
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
            this.IconPictureBoxLogo.TabIndex = 152;
            this.IconPictureBoxLogo.TabStop = false;
            // 
            // Password
            // 
            this.AcceptButton = this.IconButtonGenerateNewPassword;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(59)))), ((int)(((byte)(104)))));
            this.ClientSize = new System.Drawing.Size(300, 428);
            this.Controls.Add(this.LabelLogo);
            this.Controls.Add(this.IconPictureBoxLogo);
            this.Controls.Add(this.LabelDescription);
            this.Controls.Add(this.PictureBoxLoading);
            this.Controls.Add(this.LabelInfo);
            this.Controls.Add(this.IconButtonGenerateNewPassword);
            this.Controls.Add(this.TextBoxUsernameOrEmail);
            this.Controls.Add(this.LabelUser);
            this.Controls.Add(this.PanelFooter);
            this.Controls.Add(this.PanelHeader);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Silver;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Password";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ingreso.";
            this.Load += new System.EventHandler(this.Password_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Password_Paint);
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
        private FontAwesome.Sharp.IconButton IconButtonGenerateNewPassword;
        private System.Windows.Forms.Label LabelInfo;
        private System.Windows.Forms.PictureBox PictureBoxLoading;
        private FontAwesome.Sharp.IconButton IconButtonBack;
        private System.Windows.Forms.Label labelTitleToolBar;
        private System.Windows.Forms.Label LabelDescription;
        private FontAwesome.Sharp.IconButton IconButtonClose;
        private System.Windows.Forms.Label LabelLogo;
        private FontAwesome.Sharp.IconPictureBox IconPictureBoxLogo;
    }
}

