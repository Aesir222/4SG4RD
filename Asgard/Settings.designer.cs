namespace Asgard
{
    partial class Settings
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
            this.PanelTitleOdin = new System.Windows.Forms.Panel();
            this.IconButtonBack = new FontAwesome.Sharp.IconButton();
            this.labelTitleToolBar = new System.Windows.Forms.Label();
            this.PanelTitleOdin.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelTitleOdin
            // 
            this.PanelTitleOdin.BackColor = System.Drawing.Color.Indigo;
            this.PanelTitleOdin.Controls.Add(this.IconButtonBack);
            this.PanelTitleOdin.Controls.Add(this.labelTitleToolBar);
            this.PanelTitleOdin.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelTitleOdin.Location = new System.Drawing.Point(0, 0);
            this.PanelTitleOdin.Name = "PanelTitleOdin";
            this.PanelTitleOdin.Size = new System.Drawing.Size(730, 30);
            this.PanelTitleOdin.TabIndex = 2;
            // 
            // IconButtonBack
            // 
            this.IconButtonBack.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IconButtonBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconButtonBack.FlatAppearance.BorderSize = 0;
            this.IconButtonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconButtonBack.IconChar = FontAwesome.Sharp.IconChar.Tools;
            this.IconButtonBack.IconColor = System.Drawing.Color.White;
            this.IconButtonBack.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconButtonBack.IconSize = 24;
            this.IconButtonBack.Location = new System.Drawing.Point(3, 3);
            this.IconButtonBack.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.IconButtonBack.Name = "IconButtonBack";
            this.IconButtonBack.Size = new System.Drawing.Size(24, 24);
            this.IconButtonBack.TabIndex = 110;
            this.IconButtonBack.UseVisualStyleBackColor = true;
            // 
            // labelTitleToolBar
            // 
            this.labelTitleToolBar.AutoSize = true;
            this.labelTitleToolBar.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitleToolBar.ForeColor = System.Drawing.Color.White;
            this.labelTitleToolBar.Location = new System.Drawing.Point(33, 5);
            this.labelTitleToolBar.Name = "labelTitleToolBar";
            this.labelTitleToolBar.Size = new System.Drawing.Size(113, 21);
            this.labelTitleToolBar.TabIndex = 1;
            this.labelTitleToolBar.Text = "Configuracion";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(59)))), ((int)(((byte)(104)))));
            this.ClientSize = new System.Drawing.Size(730, 499);
            this.Controls.Add(this.PanelTitleOdin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Settings";
            this.Text = "Settings";
            this.PanelTitleOdin.ResumeLayout(false);
            this.PanelTitleOdin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelTitleOdin;
        private FontAwesome.Sharp.IconButton IconButtonBack;
        private System.Windows.Forms.Label labelTitleToolBar;
    }
}