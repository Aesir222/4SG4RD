namespace Asgard
{
    partial class Jotun
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
            this.components = new System.ComponentModel.Container();
            this.labelTitleToolBar = new System.Windows.Forms.Label();
            this.PanelTitleJotun = new System.Windows.Forms.Panel();
            this.IconButtonBack = new FontAwesome.Sharp.IconButton();
            this.PanelGenerator = new System.Windows.Forms.Panel();
            this.PictureBoxYmir = new System.Windows.Forms.PictureBox();
            this.LabelTextYmir = new System.Windows.Forms.Label();
            this.LabelRunes = new System.Windows.Forms.Label();
            this.LabelTitleYmir = new System.Windows.Forms.Label();
            this.IconButtonYmir = new FontAwesome.Sharp.IconButton();
            this.IconButtonYmirOff = new FontAwesome.Sharp.IconPictureBox();
            this.IconButtonYmirOn = new FontAwesome.Sharp.IconPictureBox();
            this.TimerSync = new System.Windows.Forms.Timer(this.components);
            this.PanelTitleJotun.SuspendLayout();
            this.PanelGenerator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxYmir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconButtonYmirOff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconButtonYmirOn)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTitleToolBar
            // 
            this.labelTitleToolBar.AutoSize = true;
            this.labelTitleToolBar.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitleToolBar.ForeColor = System.Drawing.Color.White;
            this.labelTitleToolBar.Location = new System.Drawing.Point(33, 5);
            this.labelTitleToolBar.Name = "labelTitleToolBar";
            this.labelTitleToolBar.Size = new System.Drawing.Size(50, 21);
            this.labelTitleToolBar.TabIndex = 1;
            this.labelTitleToolBar.Text = "Jotun";
            // 
            // PanelTitleJotun
            // 
            this.PanelTitleJotun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(127)))), ((int)(((byte)(0)))));
            this.PanelTitleJotun.Controls.Add(this.IconButtonBack);
            this.PanelTitleJotun.Controls.Add(this.labelTitleToolBar);
            this.PanelTitleJotun.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelTitleJotun.ForeColor = System.Drawing.Color.White;
            this.PanelTitleJotun.Location = new System.Drawing.Point(0, 0);
            this.PanelTitleJotun.Name = "PanelTitleJotun";
            this.PanelTitleJotun.Size = new System.Drawing.Size(730, 30);
            this.PanelTitleJotun.TabIndex = 126;
            // 
            // IconButtonBack
            // 
            this.IconButtonBack.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IconButtonBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconButtonBack.FlatAppearance.BorderSize = 0;
            this.IconButtonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconButtonBack.ForeColor = System.Drawing.Color.White;
            this.IconButtonBack.IconChar = FontAwesome.Sharp.IconChar.ArrowCircleLeft;
            this.IconButtonBack.IconColor = System.Drawing.Color.White;
            this.IconButtonBack.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconButtonBack.IconSize = 24;
            this.IconButtonBack.Location = new System.Drawing.Point(4, 3);
            this.IconButtonBack.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.IconButtonBack.Name = "IconButtonBack";
            this.IconButtonBack.Size = new System.Drawing.Size(24, 24);
            this.IconButtonBack.TabIndex = 110;
            this.IconButtonBack.UseVisualStyleBackColor = true;
            this.IconButtonBack.Click += new System.EventHandler(this.IconButtonBack_Click);
            // 
            // PanelGenerator
            // 
            this.PanelGenerator.BackColor = System.Drawing.Color.Transparent;
            this.PanelGenerator.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PanelGenerator.Controls.Add(this.PictureBoxYmir);
            this.PanelGenerator.Controls.Add(this.LabelTextYmir);
            this.PanelGenerator.Controls.Add(this.LabelRunes);
            this.PanelGenerator.Controls.Add(this.LabelTitleYmir);
            this.PanelGenerator.Controls.Add(this.IconButtonYmir);
            this.PanelGenerator.Controls.Add(this.IconButtonYmirOff);
            this.PanelGenerator.Controls.Add(this.IconButtonYmirOn);
            this.PanelGenerator.Location = new System.Drawing.Point(244, 106);
            this.PanelGenerator.Name = "PanelGenerator";
            this.PanelGenerator.Padding = new System.Windows.Forms.Padding(10);
            this.PanelGenerator.Size = new System.Drawing.Size(243, 287);
            this.PanelGenerator.TabIndex = 127;
            // 
            // PictureBoxYmir
            // 
            this.PictureBoxYmir.Image = global::Asgard.Properties.Resources.e66c06c180fef010f6b3a84a0e0c0ae6d006c447_128;
            this.PictureBoxYmir.Location = new System.Drawing.Point(76, 28);
            this.PictureBoxYmir.Name = "PictureBoxYmir";
            this.PictureBoxYmir.Size = new System.Drawing.Size(90, 90);
            this.PictureBoxYmir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBoxYmir.TabIndex = 83;
            this.PictureBoxYmir.TabStop = false;
            // 
            // LabelTextYmir
            // 
            this.LabelTextYmir.Font = new System.Drawing.Font("Segoe UI Semilight", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTextYmir.ForeColor = System.Drawing.Color.Silver;
            this.LabelTextYmir.Location = new System.Drawing.Point(21, 147);
            this.LabelTextYmir.Name = "LabelTextYmir";
            this.LabelTextYmir.Size = new System.Drawing.Size(200, 54);
            this.LabelTextYmir.TabIndex = 80;
            this.LabelTextYmir.Text = "Mientras Ymir dormía, comenzó a sudar y así concibió la raza de los gigantes.";
            this.LabelTextYmir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LabelRunes
            // 
            this.LabelRunes.AutoSize = true;
            this.LabelRunes.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelRunes.ForeColor = System.Drawing.Color.White;
            this.LabelRunes.Location = new System.Drawing.Point(93, 210);
            this.LabelRunes.Name = "LabelRunes";
            this.LabelRunes.Size = new System.Drawing.Size(56, 15);
            this.LabelRunes.TabIndex = 94;
            this.LabelRunes.Text = "20 Runas";
            this.LabelRunes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LabelTitleYmir
            // 
            this.LabelTitleYmir.AutoSize = true;
            this.LabelTitleYmir.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTitleYmir.ForeColor = System.Drawing.Color.White;
            this.LabelTitleYmir.Location = new System.Drawing.Point(101, 5);
            this.LabelTitleYmir.Name = "LabelTitleYmir";
            this.LabelTitleYmir.Size = new System.Drawing.Size(40, 17);
            this.LabelTitleYmir.TabIndex = 78;
            this.LabelTitleYmir.Text = "YMIR";
            this.LabelTitleYmir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IconButtonYmir
            // 
            this.IconButtonYmir.BackColor = System.Drawing.Color.DarkRed;
            this.IconButtonYmir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconButtonYmir.FlatAppearance.BorderSize = 0;
            this.IconButtonYmir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconButtonYmir.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IconButtonYmir.ForeColor = System.Drawing.Color.White;
            this.IconButtonYmir.IconChar = FontAwesome.Sharp.IconChar.PrayingHands;
            this.IconButtonYmir.IconColor = System.Drawing.Color.White;
            this.IconButtonYmir.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconButtonYmir.IconSize = 24;
            this.IconButtonYmir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.IconButtonYmir.Location = new System.Drawing.Point(21, 237);
            this.IconButtonYmir.Name = "IconButtonYmir";
            this.IconButtonYmir.Size = new System.Drawing.Size(200, 30);
            this.IconButtonYmir.TabIndex = 6;
            this.IconButtonYmir.Text = "INVOCAR A YMIR";
            this.IconButtonYmir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.IconButtonYmir.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.IconButtonYmir.UseVisualStyleBackColor = false;
            this.IconButtonYmir.Click += new System.EventHandler(this.IconButtonYmir_Click);
            // 
            // IconButtonYmirOff
            // 
            this.IconButtonYmirOff.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.IconButtonYmirOff.BackColor = System.Drawing.Color.Transparent;
            this.IconButtonYmirOff.ForeColor = System.Drawing.Color.DimGray;
            this.IconButtonYmirOff.IconChar = FontAwesome.Sharp.IconChar.ToggleOff;
            this.IconButtonYmirOff.IconColor = System.Drawing.Color.DimGray;
            this.IconButtonYmirOff.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconButtonYmirOff.IconSize = 24;
            this.IconButtonYmirOff.Location = new System.Drawing.Point(109, 123);
            this.IconButtonYmirOff.Margin = new System.Windows.Forms.Padding(0);
            this.IconButtonYmirOff.Name = "IconButtonYmirOff";
            this.IconButtonYmirOff.Size = new System.Drawing.Size(24, 24);
            this.IconButtonYmirOff.TabIndex = 95;
            this.IconButtonYmirOff.TabStop = false;
            // 
            // IconButtonYmirOn
            // 
            this.IconButtonYmirOn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.IconButtonYmirOn.BackColor = System.Drawing.Color.Transparent;
            this.IconButtonYmirOn.ForeColor = System.Drawing.Color.Lime;
            this.IconButtonYmirOn.IconChar = FontAwesome.Sharp.IconChar.ToggleOn;
            this.IconButtonYmirOn.IconColor = System.Drawing.Color.Lime;
            this.IconButtonYmirOn.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconButtonYmirOn.IconSize = 24;
            this.IconButtonYmirOn.Location = new System.Drawing.Point(109, 123);
            this.IconButtonYmirOn.Margin = new System.Windows.Forms.Padding(0);
            this.IconButtonYmirOn.Name = "IconButtonYmirOn";
            this.IconButtonYmirOn.Size = new System.Drawing.Size(24, 24);
            this.IconButtonYmirOn.TabIndex = 96;
            this.IconButtonYmirOn.TabStop = false;
            // 
            // TimerSync
            // 
            this.TimerSync.Interval = 1000;
            this.TimerSync.Tick += new System.EventHandler(this.TimerSync_Tick);
            // 
            // Jotun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(59)))), ((int)(((byte)(104)))));
            this.ClientSize = new System.Drawing.Size(730, 499);
            this.Controls.Add(this.PanelTitleJotun);
            this.Controls.Add(this.PanelGenerator);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Jotun";
            this.Opacity = 0.6D;
            this.Text = "Jotun";
            this.Load += new System.EventHandler(this.Jotun_Load);
            this.PanelTitleJotun.ResumeLayout(false);
            this.PanelTitleJotun.PerformLayout();
            this.PanelGenerator.ResumeLayout(false);
            this.PanelGenerator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxYmir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconButtonYmirOff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconButtonYmirOn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labelTitleToolBar;
        private System.Windows.Forms.Panel PanelTitleJotun;
        private FontAwesome.Sharp.IconButton IconButtonBack;
        private System.Windows.Forms.Panel PanelGenerator;
        private System.Windows.Forms.Label LabelTitleYmir;
        private FontAwesome.Sharp.IconButton IconButtonYmir;
        private System.Windows.Forms.Label LabelTextYmir;
        private System.Windows.Forms.PictureBox PictureBoxYmir;
        private System.Windows.Forms.Label LabelRunes;
        private FontAwesome.Sharp.IconPictureBox IconButtonYmirOff;
        private FontAwesome.Sharp.IconPictureBox IconButtonYmirOn;
        private System.Windows.Forms.Timer TimerSync;
    }
}