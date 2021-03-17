namespace Asgard
{
    partial class Register
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Register));
            this.PanelHeader = new System.Windows.Forms.Panel();
            this.IconButtonClose = new FontAwesome.Sharp.IconButton();
            this.IconButtonBack = new FontAwesome.Sharp.IconButton();
            this.labelTitleToolBar = new System.Windows.Forms.Label();
            this.PanelFooter = new System.Windows.Forms.Panel();
            this.PictureBoxLoading = new System.Windows.Forms.PictureBox();
            this.LabelInfo = new System.Windows.Forms.Label();
            this.LabelLogo = new System.Windows.Forms.Label();
            this.IconPictureBoxLogo = new FontAwesome.Sharp.IconPictureBox();
            this.TextBoxConfirmPassword = new System.Windows.Forms.TextBox();
            this.LabelConfirmPassword = new System.Windows.Forms.Label();
            this.TextBoxEmail = new System.Windows.Forms.TextBox();
            this.LabelEmail = new System.Windows.Forms.Label();
            this.IconButtonRegister = new FontAwesome.Sharp.IconButton();
            this.TextBoxPassword = new System.Windows.Forms.TextBox();
            this.LabelPassword = new System.Windows.Forms.Label();
            this.TextBoxUser = new System.Windows.Forms.TextBox();
            this.LabelUser = new System.Windows.Forms.Label();
            this.TextBoxReferred = new System.Windows.Forms.TextBox();
            this.LabelReferred = new System.Windows.Forms.Label();
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
            this.PanelHeader.TabIndex = 111;
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
            this.labelTitleToolBar.Location = new System.Drawing.Point(32, 5);
            this.labelTitleToolBar.Name = "labelTitleToolBar";
            this.labelTitleToolBar.Size = new System.Drawing.Size(72, 21);
            this.labelTitleToolBar.TabIndex = 108;
            this.labelTitleToolBar.Text = "Registro";
            // 
            // PanelFooter
            // 
            this.PanelFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(38)))), ((int)(((byte)(70)))));
            this.PanelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelFooter.Location = new System.Drawing.Point(0, 525);
            this.PanelFooter.Name = "PanelFooter";
            this.PanelFooter.Size = new System.Drawing.Size(300, 5);
            this.PanelFooter.TabIndex = 110;
            // 
            // PictureBoxLoading
            // 
            this.PictureBoxLoading.Image = ((System.Drawing.Image)(resources.GetObject("PictureBoxLoading.Image")));
            this.PictureBoxLoading.Location = new System.Drawing.Point(134, 469);
            this.PictureBoxLoading.Name = "PictureBoxLoading";
            this.PictureBoxLoading.Size = new System.Drawing.Size(32, 32);
            this.PictureBoxLoading.TabIndex = 128;
            this.PictureBoxLoading.TabStop = false;
            // 
            // LabelInfo
            // 
            this.LabelInfo.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelInfo.ForeColor = System.Drawing.Color.White;
            this.LabelInfo.Location = new System.Drawing.Point(25, 508);
            this.LabelInfo.Name = "LabelInfo";
            this.LabelInfo.Size = new System.Drawing.Size(250, 12);
            this.LabelInfo.TabIndex = 123;
            this.LabelInfo.Text = "4SG4RD TEAM - 2021";
            this.LabelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LabelLogo
            // 
            this.LabelLogo.AutoSize = true;
            this.LabelLogo.BackColor = System.Drawing.Color.Transparent;
            this.LabelLogo.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelLogo.ForeColor = System.Drawing.Color.White;
            this.LabelLogo.Location = new System.Drawing.Point(87, 135);
            this.LabelLogo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelLogo.Name = "LabelLogo";
            this.LabelLogo.Size = new System.Drawing.Size(126, 33);
            this.LabelLogo.TabIndex = 124;
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
            this.IconPictureBoxLogo.IconSize = 128;
            this.IconPictureBoxLogo.Location = new System.Drawing.Point(86, 37);
            this.IconPictureBoxLogo.Margin = new System.Windows.Forms.Padding(0);
            this.IconPictureBoxLogo.Name = "IconPictureBoxLogo";
            this.IconPictureBoxLogo.Size = new System.Drawing.Size(128, 128);
            this.IconPictureBoxLogo.TabIndex = 127;
            this.IconPictureBoxLogo.TabStop = false;
            // 
            // TextBoxConfirmPassword
            // 
            this.TextBoxConfirmPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxConfirmPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(94)))), ((int)(((byte)(129)))));
            this.TextBoxConfirmPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxConfirmPassword.ForeColor = System.Drawing.Color.White;
            this.TextBoxConfirmPassword.Location = new System.Drawing.Point(25, 349);
            this.TextBoxConfirmPassword.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.TextBoxConfirmPassword.Name = "TextBoxConfirmPassword";
            this.TextBoxConfirmPassword.PasswordChar = '$';
            this.TextBoxConfirmPassword.Size = new System.Drawing.Size(250, 22);
            this.TextBoxConfirmPassword.TabIndex = 119;
            this.TextBoxConfirmPassword.TextChanged += new System.EventHandler(this.TextBoxConfirmPassword_TextChanged);
            // 
            // LabelConfirmPassword
            // 
            this.LabelConfirmPassword.AutoSize = true;
            this.LabelConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelConfirmPassword.ForeColor = System.Drawing.Color.Silver;
            this.LabelConfirmPassword.Location = new System.Drawing.Point(25, 331);
            this.LabelConfirmPassword.Name = "LabelConfirmPassword";
            this.LabelConfirmPassword.Size = new System.Drawing.Size(126, 13);
            this.LabelConfirmPassword.TabIndex = 121;
            this.LabelConfirmPassword.Text = "Confirmar contraseña.*";
            // 
            // TextBoxEmail
            // 
            this.TextBoxEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(94)))), ((int)(((byte)(129)))));
            this.TextBoxEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxEmail.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxEmail.ForeColor = System.Drawing.Color.White;
            this.TextBoxEmail.Location = new System.Drawing.Point(25, 248);
            this.TextBoxEmail.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.TextBoxEmail.Name = "TextBoxEmail";
            this.TextBoxEmail.Size = new System.Drawing.Size(250, 22);
            this.TextBoxEmail.TabIndex = 117;
            this.TextBoxEmail.TextChanged += new System.EventHandler(this.TextBoxEmail_TextChanged);
            // 
            // LabelEmail
            // 
            this.LabelEmail.AutoSize = true;
            this.LabelEmail.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelEmail.ForeColor = System.Drawing.Color.Silver;
            this.LabelEmail.Location = new System.Drawing.Point(25, 231);
            this.LabelEmail.Name = "LabelEmail";
            this.LabelEmail.Size = new System.Drawing.Size(110, 13);
            this.LabelEmail.TabIndex = 122;
            this.LabelEmail.Text = "Correo electronico.*";
            // 
            // IconButtonRegister
            // 
            this.IconButtonRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(38)))), ((int)(((byte)(70)))));
            this.IconButtonRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconButtonRegister.FlatAppearance.BorderSize = 0;
            this.IconButtonRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconButtonRegister.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IconButtonRegister.ForeColor = System.Drawing.Color.White;
            this.IconButtonRegister.IconChar = FontAwesome.Sharp.IconChar.UserPlus;
            this.IconButtonRegister.IconColor = System.Drawing.Color.White;
            this.IconButtonRegister.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconButtonRegister.IconSize = 24;
            this.IconButtonRegister.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.IconButtonRegister.Location = new System.Drawing.Point(26, 432);
            this.IconButtonRegister.Name = "IconButtonRegister";
            this.IconButtonRegister.Size = new System.Drawing.Size(248, 30);
            this.IconButtonRegister.TabIndex = 120;
            this.IconButtonRegister.Text = "REGISTRAR";
            this.IconButtonRegister.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.IconButtonRegister.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.IconButtonRegister.UseVisualStyleBackColor = false;
            this.IconButtonRegister.Click += new System.EventHandler(this.IconButtonRegister_Click);
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
            this.TextBoxPassword.Location = new System.Drawing.Point(25, 297);
            this.TextBoxPassword.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.TextBoxPassword.Name = "TextBoxPassword";
            this.TextBoxPassword.PasswordChar = '$';
            this.TextBoxPassword.Size = new System.Drawing.Size(250, 22);
            this.TextBoxPassword.TabIndex = 118;
            this.TextBoxPassword.TextChanged += new System.EventHandler(this.TextBoxPassword_TextChanged);
            // 
            // LabelPassword
            // 
            this.LabelPassword.AutoSize = true;
            this.LabelPassword.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPassword.ForeColor = System.Drawing.Color.Silver;
            this.LabelPassword.Location = new System.Drawing.Point(25, 280);
            this.LabelPassword.Name = "LabelPassword";
            this.LabelPassword.Size = new System.Drawing.Size(74, 13);
            this.LabelPassword.TabIndex = 125;
            this.LabelPassword.Text = "Contraseña.*";
            // 
            // TextBoxUser
            // 
            this.TextBoxUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(94)))), ((int)(((byte)(129)))));
            this.TextBoxUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxUser.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxUser.ForeColor = System.Drawing.Color.White;
            this.TextBoxUser.Location = new System.Drawing.Point(25, 200);
            this.TextBoxUser.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.TextBoxUser.Name = "TextBoxUser";
            this.TextBoxUser.Size = new System.Drawing.Size(250, 22);
            this.TextBoxUser.TabIndex = 116;
            this.TextBoxUser.TextChanged += new System.EventHandler(this.TextBoxUser_TextChanged);
            this.TextBoxUser.Leave += new System.EventHandler(this.TextBoxUser_Leave);
            // 
            // LabelUser
            // 
            this.LabelUser.AutoSize = true;
            this.LabelUser.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelUser.ForeColor = System.Drawing.Color.Silver;
            this.LabelUser.Location = new System.Drawing.Point(25, 183);
            this.LabelUser.Name = "LabelUser";
            this.LabelUser.Size = new System.Drawing.Size(98, 13);
            this.LabelUser.TabIndex = 126;
            this.LabelUser.Text = "Nombre usuario.*";
            // 
            // TextBoxReferred
            // 
            this.TextBoxReferred.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxReferred.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.TextBoxReferred.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.TextBoxReferred.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(94)))), ((int)(((byte)(129)))));
            this.TextBoxReferred.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxReferred.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxReferred.ForeColor = System.Drawing.Color.White;
            this.TextBoxReferred.Location = new System.Drawing.Point(25, 395);
            this.TextBoxReferred.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.TextBoxReferred.Name = "TextBoxReferred";
            this.TextBoxReferred.Size = new System.Drawing.Size(250, 22);
            this.TextBoxReferred.TabIndex = 129;
            this.TextBoxReferred.TextChanged += new System.EventHandler(this.TextBoxReferred_TextChanged);
            // 
            // LabelReferred
            // 
            this.LabelReferred.AutoSize = true;
            this.LabelReferred.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelReferred.ForeColor = System.Drawing.Color.Silver;
            this.LabelReferred.Location = new System.Drawing.Point(25, 378);
            this.LabelReferred.Name = "LabelReferred";
            this.LabelReferred.Size = new System.Drawing.Size(75, 13);
            this.LabelReferred.TabIndex = 130;
            this.LabelReferred.Text = "Referido por.";
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(59)))), ((int)(((byte)(104)))));
            this.ClientSize = new System.Drawing.Size(300, 530);
            this.Controls.Add(this.TextBoxReferred);
            this.Controls.Add(this.LabelReferred);
            this.Controls.Add(this.PictureBoxLoading);
            this.Controls.Add(this.LabelInfo);
            this.Controls.Add(this.LabelLogo);
            this.Controls.Add(this.IconPictureBoxLogo);
            this.Controls.Add(this.TextBoxConfirmPassword);
            this.Controls.Add(this.LabelConfirmPassword);
            this.Controls.Add(this.TextBoxEmail);
            this.Controls.Add(this.LabelEmail);
            this.Controls.Add(this.IconButtonRegister);
            this.Controls.Add(this.TextBoxPassword);
            this.Controls.Add(this.LabelPassword);
            this.Controls.Add(this.TextBoxUser);
            this.Controls.Add(this.LabelUser);
            this.Controls.Add(this.PanelFooter);
            this.Controls.Add(this.PanelHeader);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Silver;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Register";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ingreso.";
            this.Load += new System.EventHandler(this.Register_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Register_Paint);
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
        private FontAwesome.Sharp.IconButton IconButtonBack;
        private System.Windows.Forms.Label labelTitleToolBar;
        private FontAwesome.Sharp.IconButton IconButtonClose;
        private System.Windows.Forms.PictureBox PictureBoxLoading;
        private System.Windows.Forms.Label LabelInfo;
        private System.Windows.Forms.Label LabelLogo;
        private FontAwesome.Sharp.IconPictureBox IconPictureBoxLogo;
        private System.Windows.Forms.TextBox TextBoxConfirmPassword;
        private System.Windows.Forms.Label LabelConfirmPassword;
        private System.Windows.Forms.TextBox TextBoxEmail;
        private System.Windows.Forms.Label LabelEmail;
        private FontAwesome.Sharp.IconButton IconButtonRegister;
        private System.Windows.Forms.TextBox TextBoxPassword;
        private System.Windows.Forms.Label LabelPassword;
        private System.Windows.Forms.TextBox TextBoxUser;
        private System.Windows.Forms.Label LabelUser;
        private System.Windows.Forms.TextBox TextBoxReferred;
        private System.Windows.Forms.Label LabelReferred;
    }
}

