namespace Asgard
{
    partial class Video1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Video1));
            this.PanelTitleShop = new System.Windows.Forms.Panel();
            this.PictureBoxLoadShop1 = new System.Windows.Forms.PictureBox();
            this.IconButtonBack = new FontAwesome.Sharp.IconButton();
            this.IconButtonClose = new FontAwesome.Sharp.IconButton();
            this.labelTitleToolBar = new System.Windows.Forms.Label();
            this.PanelBrowser = new System.Windows.Forms.Panel();
            this.PanelTitleShop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxLoadShop1)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelTitleShop
            // 
            this.PanelTitleShop.BackColor = System.Drawing.Color.DarkRed;
            this.PanelTitleShop.Controls.Add(this.PictureBoxLoadShop1);
            this.PanelTitleShop.Controls.Add(this.IconButtonBack);
            this.PanelTitleShop.Controls.Add(this.IconButtonClose);
            this.PanelTitleShop.Controls.Add(this.labelTitleToolBar);
            this.PanelTitleShop.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelTitleShop.Location = new System.Drawing.Point(0, 0);
            this.PanelTitleShop.Name = "PanelTitleShop";
            this.PanelTitleShop.Size = new System.Drawing.Size(730, 30);
            this.PanelTitleShop.TabIndex = 1;
            // 
            // PictureBoxLoadShop1
            // 
            this.PictureBoxLoadShop1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBoxLoadShop1.Image")));
            this.PictureBoxLoadShop1.Location = new System.Drawing.Point(3, 3);
            this.PictureBoxLoadShop1.Name = "PictureBoxLoadShop1";
            this.PictureBoxLoadShop1.Size = new System.Drawing.Size(24, 24);
            this.PictureBoxLoadShop1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PictureBoxLoadShop1.TabIndex = 116;
            this.PictureBoxLoadShop1.TabStop = false;
            // 
            // IconButtonBack
            // 
            this.IconButtonBack.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IconButtonBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconButtonBack.FlatAppearance.BorderSize = 0;
            this.IconButtonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconButtonBack.IconChar = FontAwesome.Sharp.IconChar.Youtube;
            this.IconButtonBack.IconColor = System.Drawing.Color.White;
            this.IconButtonBack.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconButtonBack.IconSize = 24;
            this.IconButtonBack.Location = new System.Drawing.Point(3, 3);
            this.IconButtonBack.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.IconButtonBack.Name = "IconButtonBack";
            this.IconButtonBack.Size = new System.Drawing.Size(24, 24);
            this.IconButtonBack.TabIndex = 110;
            this.IconButtonBack.UseVisualStyleBackColor = true;
            this.IconButtonBack.Click += new System.EventHandler(this.IconButtonBack_Click);
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
            this.IconButtonClose.Location = new System.Drawing.Point(701, 3);
            this.IconButtonClose.Margin = new System.Windows.Forms.Padding(0);
            this.IconButtonClose.Name = "IconButtonClose";
            this.IconButtonClose.Size = new System.Drawing.Size(24, 24);
            this.IconButtonClose.TabIndex = 109;
            this.IconButtonClose.UseVisualStyleBackColor = true;
            this.IconButtonClose.Click += new System.EventHandler(this.IconButtonClose_Click);
            // 
            // labelTitleToolBar
            // 
            this.labelTitleToolBar.AutoSize = true;
            this.labelTitleToolBar.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitleToolBar.ForeColor = System.Drawing.Color.White;
            this.labelTitleToolBar.Location = new System.Drawing.Point(33, 5);
            this.labelTitleToolBar.Name = "labelTitleToolBar";
            this.labelTitleToolBar.Size = new System.Drawing.Size(16, 21);
            this.labelTitleToolBar.TabIndex = 1;
            this.labelTitleToolBar.Text = "-";
            // 
            // PanelBrowser
            // 
            this.PanelBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelBrowser.Location = new System.Drawing.Point(0, 30);
            this.PanelBrowser.Name = "PanelBrowser";
            this.PanelBrowser.Size = new System.Drawing.Size(730, 469);
            this.PanelBrowser.TabIndex = 2;
            // 
            // Video1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(59)))), ((int)(((byte)(104)))));
            this.ClientSize = new System.Drawing.Size(730, 499);
            this.Controls.Add(this.PanelBrowser);
            this.Controls.Add(this.PanelTitleShop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Video1";
            this.Text = "Video1";
            this.Load += new System.EventHandler(this.Browser_Load);
            this.PanelTitleShop.ResumeLayout(false);
            this.PanelTitleShop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxLoadShop1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelTitleShop;
        private FontAwesome.Sharp.IconButton IconButtonBack;
        private FontAwesome.Sharp.IconButton IconButtonClose;
        private System.Windows.Forms.Label labelTitleToolBar;
        private System.Windows.Forms.Panel PanelBrowser;
        private System.Windows.Forms.PictureBox PictureBoxLoadShop1;
    }
}