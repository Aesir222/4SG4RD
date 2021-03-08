

namespace Asgard
{
    partial class Result
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
            this.LabelGroupBoxGenerator = new System.Windows.Forms.Label();
            this.TextBoxDie = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.TextBoxLive = new System.Windows.Forms.TextBox();
            this.LabelCC = new System.Windows.Forms.Label();
            this.PanelSeparator = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // LabelGroupBoxGenerator
            // 
            this.LabelGroupBoxGenerator.AutoSize = true;
            this.LabelGroupBoxGenerator.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelGroupBoxGenerator.ForeColor = System.Drawing.Color.White;
            this.LabelGroupBoxGenerator.Location = new System.Drawing.Point(196, 7);
            this.LabelGroupBoxGenerator.Name = "LabelGroupBoxGenerator";
            this.LabelGroupBoxGenerator.Size = new System.Drawing.Size(83, 18);
            this.LabelGroupBoxGenerator.TabIndex = 5;
            this.LabelGroupBoxGenerator.Text = "Resultado";
            this.LabelGroupBoxGenerator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LabelGroupBoxGenerator.Click += new System.EventHandler(this.LabelGroupBoxGenerator_Click);
            // 
            // TextBoxDie
            // 
            this.TextBoxDie.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(94)))), ((int)(((byte)(129)))));
            this.TextBoxDie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxDie.ForeColor = System.Drawing.Color.Silver;
            this.TextBoxDie.Location = new System.Drawing.Point(249, 69);
            this.TextBoxDie.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.TextBoxDie.Multiline = true;
            this.TextBoxDie.Name = "TextBoxDie";
            this.TextBoxDie.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBoxDie.Size = new System.Drawing.Size(200, 135);
            this.TextBoxDie.TabIndex = 39;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(246, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Helheim";
            // 
            // iconButton1
            // 
            this.iconButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(97)))), ((int)(((byte)(238)))));
            this.iconButton1.FlatAppearance.BorderSize = 0;
            this.iconButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton1.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.iconButton1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButton1.ForeColor = System.Drawing.Color.White;
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.HandPaper;
            this.iconButton1.IconColor = System.Drawing.Color.White;
            this.iconButton1.IconSize = 24;
            this.iconButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton1.Location = new System.Drawing.Point(137, 228);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Rotation = 0D;
            this.iconButton1.Size = new System.Drawing.Size(200, 30);
            this.iconButton1.TabIndex = 37;
            this.iconButton1.Text = "DETENER";
            this.iconButton1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.iconButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.iconButton1.UseVisualStyleBackColor = false;
            // 
            // TextBoxLive
            // 
            this.TextBoxLive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(94)))), ((int)(((byte)(129)))));
            this.TextBoxLive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxLive.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxLive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.TextBoxLive.Location = new System.Drawing.Point(29, 69);
            this.TextBoxLive.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.TextBoxLive.Multiline = true;
            this.TextBoxLive.Name = "TextBoxLive";
            this.TextBoxLive.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBoxLive.Size = new System.Drawing.Size(200, 135);
            this.TextBoxLive.TabIndex = 35;
            // 
            // LabelCC
            // 
            this.LabelCC.AutoSize = true;
            this.LabelCC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.LabelCC.Location = new System.Drawing.Point(26, 53);
            this.LabelCC.Name = "LabelCC";
            this.LabelCC.Size = new System.Drawing.Size(43, 13);
            this.LabelCC.TabIndex = 34;
            this.LabelCC.Text = "Valhalla";
            // 
            // PanelSeparator
            // 
            this.PanelSeparator.BackColor = System.Drawing.Color.DarkGray;
            this.PanelSeparator.Location = new System.Drawing.Point(29, 37);
            this.PanelSeparator.Name = "PanelSeparator";
            this.PanelSeparator.Size = new System.Drawing.Size(420, 1);
            this.PanelSeparator.TabIndex = 43;
            // 
            // Result
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(38)))), ((int)(((byte)(70)))));
            this.ClientSize = new System.Drawing.Size(475, 280);
            this.Controls.Add(this.PanelSeparator);
            this.Controls.Add(this.LabelGroupBoxGenerator);
            this.Controls.Add(this.TextBoxDie);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.iconButton1);
            this.Controls.Add(this.TextBoxLive);
            this.Controls.Add(this.LabelCC);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Silver;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Result";
            this.Load += new System.EventHandler(this.Generator_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Result_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label LabelGroupBoxGenerator;
        private System.Windows.Forms.TextBox TextBoxDie;
        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconButton iconButton1;
        private System.Windows.Forms.TextBox TextBoxLive;
        private System.Windows.Forms.Label LabelCC;
        private System.Windows.Forms.Panel PanelSeparator;
    }
}