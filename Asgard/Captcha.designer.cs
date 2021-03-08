namespace Asgard
{
    partial class Captcha
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
            this.PanelHeader = new System.Windows.Forms.Panel();
            this.LabelTitle = new System.Windows.Forms.Label();
            this.PanelFooter = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.IconButtonAccept = new FontAwesome.Sharp.IconButton();
            this.IconButtonClose = new FontAwesome.Sharp.IconButton();
            this.IconPictureBoxAsgard = new FontAwesome.Sharp.IconPictureBox();
            this.TextBoxBin = new System.Windows.Forms.TextBox();
            this.LabelTextOdin = new System.Windows.Forms.Label();
            this.LinkLabelForgotPassword = new System.Windows.Forms.LinkLabel();
            this.PanelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconPictureBoxAsgard)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(38)))), ((int)(((byte)(70)))));
            this.PanelHeader.Controls.Add(this.IconButtonClose);
            this.PanelHeader.Controls.Add(this.LabelTitle);
            this.PanelHeader.Controls.Add(this.IconPictureBoxAsgard);
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(260, 30);
            this.PanelHeader.TabIndex = 0;
            this.PanelHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelHeader_MouseDown);
            // 
            // LabelTitle
            // 
            this.LabelTitle.AutoSize = true;
            this.LabelTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTitle.ForeColor = System.Drawing.Color.White;
            this.LabelTitle.Location = new System.Drawing.Point(31, 5);
            this.LabelTitle.Name = "LabelTitle";
            this.LabelTitle.Size = new System.Drawing.Size(72, 21);
            this.LabelTitle.TabIndex = 17;
            this.LabelTitle.Text = "Captcha";
            // 
            // PanelFooter
            // 
            this.PanelFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(38)))), ((int)(((byte)(70)))));
            this.PanelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelFooter.Location = new System.Drawing.Point(0, 289);
            this.PanelFooter.Name = "PanelFooter";
            this.PanelFooter.Size = new System.Drawing.Size(260, 5);
            this.PanelFooter.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(30, 103);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 70);
            this.pictureBox1.TabIndex = 107;
            this.pictureBox1.TabStop = false;
            // 
            // IconButtonAccept
            // 
            this.IconButtonAccept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(38)))), ((int)(((byte)(70)))));
            this.IconButtonAccept.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconButtonAccept.FlatAppearance.BorderSize = 0;
            this.IconButtonAccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconButtonAccept.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.IconButtonAccept.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IconButtonAccept.ForeColor = System.Drawing.Color.White;
            this.IconButtonAccept.IconChar = FontAwesome.Sharp.IconChar.Cannabis;
            this.IconButtonAccept.IconColor = System.Drawing.Color.White;
            this.IconButtonAccept.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconButtonAccept.IconSize = 24;
            this.IconButtonAccept.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.IconButtonAccept.Location = new System.Drawing.Point(30, 241);
            this.IconButtonAccept.Name = "IconButtonAccept";
            this.IconButtonAccept.Rotation = 0D;
            this.IconButtonAccept.Size = new System.Drawing.Size(200, 30);
            this.IconButtonAccept.TabIndex = 106;
            this.IconButtonAccept.Text = "CONTINUAR";
            this.IconButtonAccept.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.IconButtonAccept.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.IconButtonAccept.UseVisualStyleBackColor = false;
            this.IconButtonAccept.Click += new System.EventHandler(this.IconButtonAccept_Click);
            // 
            // IconButtonClose
            // 
            this.IconButtonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IconButtonClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconButtonClose.FlatAppearance.BorderSize = 0;
            this.IconButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconButtonClose.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.IconButtonClose.IconChar = FontAwesome.Sharp.IconChar.Times;
            this.IconButtonClose.IconColor = System.Drawing.Color.White;
            this.IconButtonClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconButtonClose.IconSize = 18;
            this.IconButtonClose.Location = new System.Drawing.Point(231, 3);
            this.IconButtonClose.Margin = new System.Windows.Forms.Padding(0);
            this.IconButtonClose.Name = "IconButtonClose";
            this.IconButtonClose.Rotation = 0D;
            this.IconButtonClose.Size = new System.Drawing.Size(24, 24);
            this.IconButtonClose.TabIndex = 19;
            this.IconButtonClose.UseVisualStyleBackColor = true;
            // 
            // IconPictureBoxAsgard
            // 
            this.IconPictureBoxAsgard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.IconPictureBoxAsgard.BackColor = System.Drawing.Color.Transparent;
            this.IconPictureBoxAsgard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.IconPictureBoxAsgard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconPictureBoxAsgard.IconChar = FontAwesome.Sharp.IconChar.GalacticSenate;
            this.IconPictureBoxAsgard.IconColor = System.Drawing.Color.White;
            this.IconPictureBoxAsgard.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconPictureBoxAsgard.IconSize = 24;
            this.IconPictureBoxAsgard.Location = new System.Drawing.Point(4, 3);
            this.IconPictureBoxAsgard.Margin = new System.Windows.Forms.Padding(0);
            this.IconPictureBoxAsgard.Name = "IconPictureBoxAsgard";
            this.IconPictureBoxAsgard.Size = new System.Drawing.Size(24, 24);
            this.IconPictureBoxAsgard.TabIndex = 18;
            this.IconPictureBoxAsgard.TabStop = false;
            // 
            // TextBoxBin
            // 
            this.TextBoxBin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TextBoxBin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(94)))), ((int)(((byte)(129)))));
            this.TextBoxBin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxBin.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxBin.ForeColor = System.Drawing.Color.White;
            this.TextBoxBin.Location = new System.Drawing.Point(30, 183);
            this.TextBoxBin.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.TextBoxBin.MaximumSize = new System.Drawing.Size(250, 21);
            this.TextBoxBin.MinimumSize = new System.Drawing.Size(25, 21);
            this.TextBoxBin.Name = "TextBoxBin";
            this.TextBoxBin.Size = new System.Drawing.Size(200, 21);
            this.TextBoxBin.TabIndex = 108;
            // 
            // LabelTextOdin
            // 
            this.LabelTextOdin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTextOdin.ForeColor = System.Drawing.Color.Silver;
            this.LabelTextOdin.Location = new System.Drawing.Point(30, 50);
            this.LabelTextOdin.Name = "LabelTextOdin";
            this.LabelTextOdin.Size = new System.Drawing.Size(200, 40);
            this.LabelTextOdin.TabIndex = 109;
            this.LabelTextOdin.Text = "Estamos trabajando para evitarle estas molestias.\r\n";
            this.LabelTextOdin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LinkLabelForgotPassword
            // 
            this.LinkLabelForgotPassword.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(97)))), ((int)(((byte)(238)))));
            this.LinkLabelForgotPassword.AutoSize = true;
            this.LinkLabelForgotPassword.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LinkLabelForgotPassword.LinkColor = System.Drawing.Color.Silver;
            this.LinkLabelForgotPassword.Location = new System.Drawing.Point(76, 207);
            this.LinkLabelForgotPassword.Name = "LinkLabelForgotPassword";
            this.LinkLabelForgotPassword.Size = new System.Drawing.Size(109, 13);
            this.LinkLabelForgotPassword.TabIndex = 110;
            this.LinkLabelForgotPassword.TabStop = true;
            this.LinkLabelForgotPassword.Text = "Ver caracteres nuevos\r\n";
            this.LinkLabelForgotPassword.VisitedLinkColor = System.Drawing.Color.Silver;
            // 
            // Captcha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(59)))), ((int)(((byte)(104)))));
            this.ClientSize = new System.Drawing.Size(260, 294);
            this.Controls.Add(this.LinkLabelForgotPassword);
            this.Controls.Add(this.LabelTextOdin);
            this.Controls.Add(this.TextBoxBin);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.IconButtonAccept);
            this.Controls.Add(this.PanelFooter);
            this.Controls.Add(this.PanelHeader);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Silver;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Captcha";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ingreso.";
            this.Load += new System.EventHandler(this.Captcha_Load);
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconPictureBoxAsgard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel PanelHeader;
        private System.Windows.Forms.Panel PanelFooter;
        private FontAwesome.Sharp.IconButton IconButtonAccept;
        private System.Windows.Forms.Label LabelTitle;
        private FontAwesome.Sharp.IconPictureBox IconPictureBoxAsgard;
        private FontAwesome.Sharp.IconButton IconButtonClose;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox TextBoxBin;
        private System.Windows.Forms.Label LabelTextOdin;
        private System.Windows.Forms.LinkLabel LinkLabelForgotPassword;
    }
}

