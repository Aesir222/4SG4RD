namespace Asgard
{
    partial class Shop5
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
            this.PanelTitleShop = new System.Windows.Forms.Panel();
            this.PictureBoxLoadShop4 = new System.Windows.Forms.PictureBox();
            this.IconButtonBack = new FontAwesome.Sharp.IconButton();
            this.IconButtonClose = new FontAwesome.Sharp.IconButton();
            this.labelTitleToolBar = new System.Windows.Forms.Label();
            this.PanelBrowser = new System.Windows.Forms.Panel();
            this.PanelWebControl = new System.Windows.Forms.Panel();
            this.IconButtonDecreaseZoom = new FontAwesome.Sharp.IconButton();
            this.IconButtonIncreaseZoom = new FontAwesome.Sharp.IconButton();
            this.IconButtonGoBack = new FontAwesome.Sharp.IconButton();
            this.IconButtonGoForward = new FontAwesome.Sharp.IconButton();
            this.IconButtonRefresh = new FontAwesome.Sharp.IconButton();
            this.PanelTitleShop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxLoadShop4)).BeginInit();
            this.PanelBrowser.SuspendLayout();
            this.PanelWebControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelTitleShop
            // 
            this.PanelTitleShop.BackColor = System.Drawing.Color.Navy;
            this.PanelTitleShop.Controls.Add(this.PictureBoxLoadShop4);
            this.PanelTitleShop.Controls.Add(this.IconButtonBack);
            this.PanelTitleShop.Controls.Add(this.IconButtonClose);
            this.PanelTitleShop.Controls.Add(this.labelTitleToolBar);
            this.PanelTitleShop.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelTitleShop.Location = new System.Drawing.Point(0, 0);
            this.PanelTitleShop.Name = "PanelTitleShop";
            this.PanelTitleShop.Size = new System.Drawing.Size(730, 30);
            this.PanelTitleShop.TabIndex = 1;
            // 
            // PictureBoxLoadShop4
            // 
            this.PictureBoxLoadShop4.Image = global::Asgard.Properties.Resources.Eclipse_1s_24px__6_;
            this.PictureBoxLoadShop4.Location = new System.Drawing.Point(3, 3);
            this.PictureBoxLoadShop4.Name = "PictureBoxLoadShop4";
            this.PictureBoxLoadShop4.Size = new System.Drawing.Size(24, 24);
            this.PictureBoxLoadShop4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PictureBoxLoadShop4.TabIndex = 116;
            this.PictureBoxLoadShop4.TabStop = false;
            // 
            // IconButtonBack
            // 
            this.IconButtonBack.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IconButtonBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconButtonBack.FlatAppearance.BorderSize = 0;
            this.IconButtonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconButtonBack.IconChar = FontAwesome.Sharp.IconChar.Compass;
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
            this.PanelBrowser.Controls.Add(this.PanelWebControl);
            this.PanelBrowser.Location = new System.Drawing.Point(0, 30);
            this.PanelBrowser.Name = "PanelBrowser";
            this.PanelBrowser.Size = new System.Drawing.Size(730, 469);
            this.PanelBrowser.TabIndex = 2;
            // 
            // PanelWebControl
            // 
            this.PanelWebControl.BackColor = System.Drawing.Color.Navy;
            this.PanelWebControl.Controls.Add(this.IconButtonDecreaseZoom);
            this.PanelWebControl.Controls.Add(this.IconButtonIncreaseZoom);
            this.PanelWebControl.Controls.Add(this.IconButtonGoBack);
            this.PanelWebControl.Controls.Add(this.IconButtonGoForward);
            this.PanelWebControl.Controls.Add(this.IconButtonRefresh);
            this.PanelWebControl.Location = new System.Drawing.Point(294, 0);
            this.PanelWebControl.Name = "PanelWebControl";
            this.PanelWebControl.Size = new System.Drawing.Size(142, 24);
            this.PanelWebControl.TabIndex = 115;
            // 
            // IconButtonDecreaseZoom
            // 
            this.IconButtonDecreaseZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IconButtonDecreaseZoom.BackColor = System.Drawing.Color.Transparent;
            this.IconButtonDecreaseZoom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconButtonDecreaseZoom.FlatAppearance.BorderSize = 0;
            this.IconButtonDecreaseZoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconButtonDecreaseZoom.IconChar = FontAwesome.Sharp.IconChar.Minus;
            this.IconButtonDecreaseZoom.IconColor = System.Drawing.Color.White;
            this.IconButtonDecreaseZoom.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconButtonDecreaseZoom.IconSize = 24;
            this.IconButtonDecreaseZoom.Location = new System.Drawing.Point(115, 0);
            this.IconButtonDecreaseZoom.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.IconButtonDecreaseZoom.Name = "IconButtonDecreaseZoom";
            this.IconButtonDecreaseZoom.Size = new System.Drawing.Size(24, 24);
            this.IconButtonDecreaseZoom.TabIndex = 123;
            this.IconButtonDecreaseZoom.UseVisualStyleBackColor = false;
            this.IconButtonDecreaseZoom.Click += new System.EventHandler(this.IconButtonDecreaseZoom_Click);
            // 
            // IconButtonIncreaseZoom
            // 
            this.IconButtonIncreaseZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IconButtonIncreaseZoom.BackColor = System.Drawing.Color.Transparent;
            this.IconButtonIncreaseZoom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconButtonIncreaseZoom.FlatAppearance.BorderSize = 0;
            this.IconButtonIncreaseZoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconButtonIncreaseZoom.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.IconButtonIncreaseZoom.IconColor = System.Drawing.Color.White;
            this.IconButtonIncreaseZoom.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconButtonIncreaseZoom.IconSize = 24;
            this.IconButtonIncreaseZoom.Location = new System.Drawing.Point(87, 0);
            this.IconButtonIncreaseZoom.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.IconButtonIncreaseZoom.Name = "IconButtonIncreaseZoom";
            this.IconButtonIncreaseZoom.Size = new System.Drawing.Size(24, 24);
            this.IconButtonIncreaseZoom.TabIndex = 122;
            this.IconButtonIncreaseZoom.UseVisualStyleBackColor = false;
            this.IconButtonIncreaseZoom.Click += new System.EventHandler(this.IconButtonIncreaseZoom_Click);
            // 
            // IconButtonGoBack
            // 
            this.IconButtonGoBack.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IconButtonGoBack.BackColor = System.Drawing.Color.Transparent;
            this.IconButtonGoBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconButtonGoBack.FlatAppearance.BorderSize = 0;
            this.IconButtonGoBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconButtonGoBack.IconChar = FontAwesome.Sharp.IconChar.ArrowLeft;
            this.IconButtonGoBack.IconColor = System.Drawing.Color.White;
            this.IconButtonGoBack.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconButtonGoBack.IconSize = 24;
            this.IconButtonGoBack.Location = new System.Drawing.Point(3, 0);
            this.IconButtonGoBack.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.IconButtonGoBack.Name = "IconButtonGoBack";
            this.IconButtonGoBack.Size = new System.Drawing.Size(24, 24);
            this.IconButtonGoBack.TabIndex = 119;
            this.IconButtonGoBack.UseVisualStyleBackColor = false;
            this.IconButtonGoBack.Click += new System.EventHandler(this.IconButtonGoBack_Click);
            // 
            // IconButtonGoForward
            // 
            this.IconButtonGoForward.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IconButtonGoForward.BackColor = System.Drawing.Color.Transparent;
            this.IconButtonGoForward.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconButtonGoForward.FlatAppearance.BorderSize = 0;
            this.IconButtonGoForward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconButtonGoForward.IconChar = FontAwesome.Sharp.IconChar.ArrowRight;
            this.IconButtonGoForward.IconColor = System.Drawing.Color.White;
            this.IconButtonGoForward.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconButtonGoForward.IconSize = 24;
            this.IconButtonGoForward.Location = new System.Drawing.Point(31, 0);
            this.IconButtonGoForward.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.IconButtonGoForward.Name = "IconButtonGoForward";
            this.IconButtonGoForward.Size = new System.Drawing.Size(24, 24);
            this.IconButtonGoForward.TabIndex = 121;
            this.IconButtonGoForward.UseVisualStyleBackColor = false;
            this.IconButtonGoForward.Click += new System.EventHandler(this.IconButtonGoForward_Click);
            // 
            // IconButtonRefresh
            // 
            this.IconButtonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IconButtonRefresh.BackColor = System.Drawing.Color.Transparent;
            this.IconButtonRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconButtonRefresh.FlatAppearance.BorderSize = 0;
            this.IconButtonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconButtonRefresh.IconChar = FontAwesome.Sharp.IconChar.Undo;
            this.IconButtonRefresh.IconColor = System.Drawing.Color.White;
            this.IconButtonRefresh.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconButtonRefresh.IconSize = 24;
            this.IconButtonRefresh.Location = new System.Drawing.Point(59, 0);
            this.IconButtonRefresh.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.IconButtonRefresh.Name = "IconButtonRefresh";
            this.IconButtonRefresh.Size = new System.Drawing.Size(24, 24);
            this.IconButtonRefresh.TabIndex = 120;
            this.IconButtonRefresh.UseVisualStyleBackColor = false;
            this.IconButtonRefresh.Click += new System.EventHandler(this.IconButtonRefresh_Click);
            // 
            // Shop5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(59)))), ((int)(((byte)(104)))));
            this.ClientSize = new System.Drawing.Size(730, 499);
            this.Controls.Add(this.PanelBrowser);
            this.Controls.Add(this.PanelTitleShop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Shop5";
            this.Text = "Shop5";
            this.Load += new System.EventHandler(this.Browser_Load);
            this.PanelTitleShop.ResumeLayout(false);
            this.PanelTitleShop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxLoadShop4)).EndInit();
            this.PanelBrowser.ResumeLayout(false);
            this.PanelWebControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelTitleShop;
        private FontAwesome.Sharp.IconButton IconButtonBack;
        private FontAwesome.Sharp.IconButton IconButtonClose;
        private System.Windows.Forms.Label labelTitleToolBar;
        private System.Windows.Forms.Panel PanelBrowser;
        private System.Windows.Forms.Panel PanelWebControl;
        private FontAwesome.Sharp.IconButton IconButtonDecreaseZoom;
        private FontAwesome.Sharp.IconButton IconButtonIncreaseZoom;
        private FontAwesome.Sharp.IconButton IconButtonGoBack;
        private FontAwesome.Sharp.IconButton IconButtonGoForward;
        private FontAwesome.Sharp.IconButton IconButtonRefresh;
        private System.Windows.Forms.PictureBox PictureBoxLoadShop4;
    }
}