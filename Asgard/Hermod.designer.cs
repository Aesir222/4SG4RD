namespace Asgard
{
    partial class Hermod
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Hermod));
            this.PanelHeader = new System.Windows.Forms.Panel();
            this.IconButtonClose = new FontAwesome.Sharp.IconButton();
            this.LabelTitle = new System.Windows.Forms.Label();
            this.IconPictureBoxAsgard = new FontAwesome.Sharp.IconPictureBox();
            this.PanelFooter = new System.Windows.Forms.Panel();
            this.IconButtonAccept = new FontAwesome.Sharp.IconButton();
            this.LabelText = new System.Windows.Forms.Label();
            this.PanelHeader.SuspendLayout();
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
            // LabelTitle
            // 
            this.LabelTitle.AutoSize = true;
            this.LabelTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTitle.ForeColor = System.Drawing.Color.White;
            this.LabelTitle.Location = new System.Drawing.Point(36, 5);
            this.LabelTitle.Name = "LabelTitle";
            this.LabelTitle.Size = new System.Drawing.Size(71, 21);
            this.LabelTitle.TabIndex = 17;
            this.LabelTitle.Text = "Ingreso.";
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
            // PanelFooter
            // 
            this.PanelFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(38)))), ((int)(((byte)(70)))));
            this.PanelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelFooter.Location = new System.Drawing.Point(0, 175);
            this.PanelFooter.Name = "PanelFooter";
            this.PanelFooter.Size = new System.Drawing.Size(260, 5);
            this.PanelFooter.TabIndex = 1;
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
            this.IconButtonAccept.IconChar = FontAwesome.Sharp.IconChar.SignInAlt;
            this.IconButtonAccept.IconColor = System.Drawing.Color.White;
            this.IconButtonAccept.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconButtonAccept.IconSize = 24;
            this.IconButtonAccept.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.IconButtonAccept.Location = new System.Drawing.Point(41, 134);
            this.IconButtonAccept.Name = "IconButtonAccept";
            this.IconButtonAccept.Rotation = 0D;
            this.IconButtonAccept.Size = new System.Drawing.Size(178, 30);
            this.IconButtonAccept.TabIndex = 106;
            this.IconButtonAccept.Text = "ACEPTAR";
            this.IconButtonAccept.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.IconButtonAccept.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.IconButtonAccept.UseVisualStyleBackColor = false;
            this.IconButtonAccept.Click += new System.EventHandler(this.IconButtonAccept_Click);
            // 
            // LabelText
            // 
            this.LabelText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelText.ForeColor = System.Drawing.Color.Silver;
            this.LabelText.Location = new System.Drawing.Point(24, 47);
            this.LabelText.Name = "LabelText";
            this.LabelText.Size = new System.Drawing.Size(213, 67);
            this.LabelText.TabIndex = 2;
            this.LabelText.Text = "-";
            this.LabelText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Hermod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(59)))), ((int)(((byte)(104)))));
            this.ClientSize = new System.Drawing.Size(260, 180);
            this.Controls.Add(this.IconButtonAccept);
            this.Controls.Add(this.LabelText);
            this.Controls.Add(this.PanelFooter);
            this.Controls.Add(this.PanelHeader);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Silver;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Hermod";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ingreso.";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Hermod_Load);
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IconPictureBoxAsgard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelHeader;
        private System.Windows.Forms.Panel PanelFooter;
        private FontAwesome.Sharp.IconButton IconButtonAccept;
        private System.Windows.Forms.Label LabelTitle;
        private FontAwesome.Sharp.IconPictureBox IconPictureBoxAsgard;
        private FontAwesome.Sharp.IconButton IconButtonClose;
        private System.Windows.Forms.Label LabelText;
    }
}

