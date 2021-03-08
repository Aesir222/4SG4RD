//using Eugenis;

namespace Asgard
{
    partial class Generator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Generator));
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.LabelGroupBoxGenerator = new System.Windows.Forms.Label();
            this.TextBoxBin = new System.Windows.Forms.TextBox();
            this.IconButtonGenerar = new FontAwesome.Sharp.IconButton();
            this.IconButtonVerify = new FontAwesome.Sharp.IconButton();
            this.MaskedTextBoxDate = new System.Windows.Forms.MaskedTextBox();
            this.TextBoxCC = new System.Windows.Forms.TextBox();
            this.LabelCC = new System.Windows.Forms.Label();
            this.TextBoxCvv = new System.Windows.Forms.TextBox();
            this.LabelCvv = new System.Windows.Forms.Label();
            this.TextBoxQuantity = new System.Windows.Forms.TextBox();
            this.LabelMonth = new System.Windows.Forms.Label();
            this.LabelQuatity = new System.Windows.Forms.Label();
            this.LabelBin = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LabelCountCC = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.ErrorProvider.ContainerControl = this;
            this.ErrorProvider.Icon = ((System.Drawing.Icon)(resources.GetObject("ErrorProvider.Icon")));
            // 
            // LabelGroupBoxGenerator
            // 
            this.LabelGroupBoxGenerator.AutoSize = true;
            this.LabelGroupBoxGenerator.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelGroupBoxGenerator.ForeColor = System.Drawing.Color.White;
            this.LabelGroupBoxGenerator.Location = new System.Drawing.Point(84, 5);
            this.LabelGroupBoxGenerator.Name = "LabelGroupBoxGenerator";
            this.LabelGroupBoxGenerator.Size = new System.Drawing.Size(87, 18);
            this.LabelGroupBoxGenerator.TabIndex = 3;
            this.LabelGroupBoxGenerator.Text = "Generador";
            this.LabelGroupBoxGenerator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TextBoxBin
            // 
            this.TextBoxBin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(94)))), ((int)(((byte)(129)))));
            this.TextBoxBin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxBin.ForeColor = System.Drawing.Color.White;
            this.TextBoxBin.Location = new System.Drawing.Point(27, 76);
            this.TextBoxBin.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.TextBoxBin.MaximumSize = new System.Drawing.Size(250, 21);
            this.TextBoxBin.MinimumSize = new System.Drawing.Size(25, 21);
            this.TextBoxBin.Name = "TextBoxBin";
            this.TextBoxBin.Size = new System.Drawing.Size(200, 21);
            this.TextBoxBin.TabIndex = 33;
            this.TextBoxBin.TextChanged += new System.EventHandler(this.TextBoxBin_TextChanged);
            this.TextBoxBin.Enter += new System.EventHandler(this.TextBoxBin_Enter);
            this.TextBoxBin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxBin_KeyUp);
            this.TextBoxBin.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBoxBin_KeyUp);
            this.TextBoxBin.Leave += new System.EventHandler(this.TextBoxBin_Leave);
            this.TextBoxBin.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxBin_Validating);
            // 
            // IconButtonGenerar
            // 
            this.IconButtonGenerar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(97)))), ((int)(((byte)(238)))));
            this.IconButtonGenerar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconButtonGenerar.FlatAppearance.BorderSize = 0;
            this.IconButtonGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconButtonGenerar.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.IconButtonGenerar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IconButtonGenerar.ForeColor = System.Drawing.Color.White;
            this.IconButtonGenerar.IconChar = FontAwesome.Sharp.IconChar.Cogs;
            this.IconButtonGenerar.IconColor = System.Drawing.Color.White;
            this.IconButtonGenerar.IconSize = 24;
            this.IconButtonGenerar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.IconButtonGenerar.Location = new System.Drawing.Point(27, 179);
            this.IconButtonGenerar.Name = "IconButtonGenerar";
            this.IconButtonGenerar.Rotation = 0D;
            this.IconButtonGenerar.Size = new System.Drawing.Size(200, 30);
            this.IconButtonGenerar.TabIndex = 43;
            this.IconButtonGenerar.Text = "GENERAR";
            this.IconButtonGenerar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.IconButtonGenerar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.IconButtonGenerar.UseVisualStyleBackColor = false;
            this.IconButtonGenerar.Click += new System.EventHandler(this.IconButtonGenerar_Click);
            // 
            // IconButtonVerify
            // 
            this.IconButtonVerify.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(97)))), ((int)(((byte)(238)))));
            this.IconButtonVerify.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IconButtonVerify.FlatAppearance.BorderSize = 0;
            this.IconButtonVerify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconButtonVerify.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.IconButtonVerify.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IconButtonVerify.ForeColor = System.Drawing.Color.White;
            this.IconButtonVerify.IconChar = FontAwesome.Sharp.IconChar.Robot;
            this.IconButtonVerify.IconColor = System.Drawing.Color.White;
            this.IconButtonVerify.IconSize = 24;
            this.IconButtonVerify.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.IconButtonVerify.Location = new System.Drawing.Point(27, 418);
            this.IconButtonVerify.Name = "IconButtonVerify";
            this.IconButtonVerify.Rotation = 0D;
            this.IconButtonVerify.Size = new System.Drawing.Size(200, 30);
            this.IconButtonVerify.TabIndex = 44;
            this.IconButtonVerify.Text = "VERIFICAR";
            this.IconButtonVerify.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.IconButtonVerify.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.IconButtonVerify.UseVisualStyleBackColor = false;
            // 
            // MaskedTextBoxDate
            // 
            this.MaskedTextBoxDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(94)))), ((int)(((byte)(129)))));
            this.MaskedTextBoxDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MaskedTextBoxDate.ForeColor = System.Drawing.Color.White;
            this.MaskedTextBoxDate.Location = new System.Drawing.Point(27, 131);
            this.MaskedTextBoxDate.Name = "MaskedTextBoxDate";
            this.MaskedTextBoxDate.Size = new System.Drawing.Size(40, 21);
            this.MaskedTextBoxDate.TabIndex = 41;
            this.MaskedTextBoxDate.TextChanged += new System.EventHandler(this.MaskedTextBoxDate_TextChanged);
            this.MaskedTextBoxDate.Enter += new System.EventHandler(this.MaskedTextBoxDate_Enter);
            this.MaskedTextBoxDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MaskedTextBoxDate_KeyUp);
            this.MaskedTextBoxDate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MaskedTextBoxDate_KeyUp);
            this.MaskedTextBoxDate.Leave += new System.EventHandler(this.MaskedTextBoxDate_Leave);
            // 
            // TextBoxCC
            // 
            this.TextBoxCC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(94)))), ((int)(((byte)(129)))));
            this.TextBoxCC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxCC.ForeColor = System.Drawing.Color.White;
            this.TextBoxCC.Location = new System.Drawing.Point(28, 259);
            this.TextBoxCC.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.TextBoxCC.Multiline = true;
            this.TextBoxCC.Name = "TextBoxCC";
            this.TextBoxCC.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBoxCC.Size = new System.Drawing.Size(200, 135);
            this.TextBoxCC.TabIndex = 40;
            this.TextBoxCC.TextChanged += new System.EventHandler(this.TextBoxCC_TextChanged);
            // 
            // LabelCC
            // 
            this.LabelCC.AutoSize = true;
            this.LabelCC.ForeColor = System.Drawing.Color.Silver;
            this.LabelCC.Location = new System.Drawing.Point(25, 243);
            this.LabelCC.Name = "LabelCC";
            this.LabelCC.Size = new System.Drawing.Size(47, 13);
            this.LabelCC.TabIndex = 39;
            this.LabelCC.Text = "Tarjetas";
            // 
            // TextBoxCvv
            // 
            this.TextBoxCvv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(94)))), ((int)(((byte)(129)))));
            this.TextBoxCvv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxCvv.ForeColor = System.Drawing.Color.White;
            this.TextBoxCvv.Location = new System.Drawing.Point(108, 131);
            this.TextBoxCvv.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.TextBoxCvv.MaximumSize = new System.Drawing.Size(250, 21);
            this.TextBoxCvv.MinimumSize = new System.Drawing.Size(25, 21);
            this.TextBoxCvv.Name = "TextBoxCvv";
            this.TextBoxCvv.Size = new System.Drawing.Size(40, 21);
            this.TextBoxCvv.TabIndex = 38;
            this.TextBoxCvv.TextChanged += new System.EventHandler(this.TextBoxCvv_TextChanged);
            this.TextBoxCvv.Enter += new System.EventHandler(this.TextBoxCvv_Enter);
            this.TextBoxCvv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxCvv_KeyUp);
            this.TextBoxCvv.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBoxCvv_KeyUp);
            this.TextBoxCvv.Leave += new System.EventHandler(this.TextBoxCvv_Leave);
            // 
            // LabelCvv
            // 
            this.LabelCvv.AutoSize = true;
            this.LabelCvv.ForeColor = System.Drawing.Color.Silver;
            this.LabelCvv.Location = new System.Drawing.Point(105, 115);
            this.LabelCvv.Name = "LabelCvv";
            this.LabelCvv.Size = new System.Drawing.Size(26, 13);
            this.LabelCvv.TabIndex = 37;
            this.LabelCvv.Text = "Cvv";
            this.LabelCvv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TextBoxQuantity
            // 
            this.TextBoxQuantity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(94)))), ((int)(((byte)(129)))));
            this.TextBoxQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxQuantity.ForeColor = System.Drawing.Color.White;
            this.TextBoxQuantity.Location = new System.Drawing.Point(187, 131);
            this.TextBoxQuantity.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.TextBoxQuantity.MaximumSize = new System.Drawing.Size(250, 21);
            this.TextBoxQuantity.MinimumSize = new System.Drawing.Size(25, 21);
            this.TextBoxQuantity.Name = "TextBoxQuantity";
            this.TextBoxQuantity.Size = new System.Drawing.Size(40, 21);
            this.TextBoxQuantity.TabIndex = 36;
            this.TextBoxQuantity.Text = "10";
            this.TextBoxQuantity.TextChanged += new System.EventHandler(this.TextBoxQuatity_TextChanged);
            this.TextBoxQuantity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxQuatity_KeyUp);
            this.TextBoxQuantity.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBoxQuatity_KeyUp);
            this.TextBoxQuantity.Leave += new System.EventHandler(this.TextBoxQuatity_Leave);
            // 
            // LabelMonth
            // 
            this.LabelMonth.AutoSize = true;
            this.LabelMonth.ForeColor = System.Drawing.Color.Silver;
            this.LabelMonth.Location = new System.Drawing.Point(25, 115);
            this.LabelMonth.Name = "LabelMonth";
            this.LabelMonth.Size = new System.Drawing.Size(36, 13);
            this.LabelMonth.TabIndex = 35;
            this.LabelMonth.Text = "Fecha";
            // 
            // LabelQuatity
            // 
            this.LabelQuatity.AutoSize = true;
            this.LabelQuatity.ForeColor = System.Drawing.Color.Silver;
            this.LabelQuatity.Location = new System.Drawing.Point(184, 115);
            this.LabelQuatity.Name = "LabelQuatity";
            this.LabelQuatity.Size = new System.Drawing.Size(34, 13);
            this.LabelQuatity.TabIndex = 34;
            this.LabelQuatity.Text = "Cant.";
            this.LabelQuatity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LabelBin
            // 
            this.LabelBin.AutoSize = true;
            this.LabelBin.ForeColor = System.Drawing.Color.Silver;
            this.LabelBin.Location = new System.Drawing.Point(25, 60);
            this.LabelBin.Name = "LabelBin";
            this.LabelBin.Size = new System.Drawing.Size(21, 13);
            this.LabelBin.TabIndex = 32;
            this.LabelBin.Text = "Bin";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkGray;
            this.panel1.Location = new System.Drawing.Point(28, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 1);
            this.panel1.TabIndex = 45;
            // 
            // LabelCountCC
            // 
            this.LabelCountCC.AutoSize = true;
            this.LabelCountCC.ForeColor = System.Drawing.Color.White;
            this.LabelCountCC.Location = new System.Drawing.Point(78, 244);
            this.LabelCountCC.Name = "LabelCountCC";
            this.LabelCountCC.Size = new System.Drawing.Size(13, 13);
            this.LabelCountCC.TabIndex = 46;
            this.LabelCountCC.Text = "0";
            // 
            // Generator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(38)))), ((int)(((byte)(70)))));
            this.ClientSize = new System.Drawing.Size(255, 469);
            this.Controls.Add(this.LabelCountCC);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.LabelGroupBoxGenerator);
            this.Controls.Add(this.TextBoxBin);
            this.Controls.Add(this.IconButtonGenerar);
            this.Controls.Add(this.IconButtonVerify);
            this.Controls.Add(this.MaskedTextBoxDate);
            this.Controls.Add(this.TextBoxCC);
            this.Controls.Add(this.LabelCC);
            this.Controls.Add(this.TextBoxCvv);
            this.Controls.Add(this.LabelCvv);
            this.Controls.Add(this.TextBoxQuantity);
            this.Controls.Add(this.LabelMonth);
            this.Controls.Add(this.LabelQuatity);
            this.Controls.Add(this.LabelBin);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Silver;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Generator";
            this.Load += new System.EventHandler(this.Generator_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Generator_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ErrorProvider ErrorProvider;
        private System.Windows.Forms.Label LabelGroupBoxGenerator;
        private System.Windows.Forms.TextBox TextBoxBin;
        private FontAwesome.Sharp.IconButton IconButtonGenerar;
        private FontAwesome.Sharp.IconButton IconButtonVerify;
        private System.Windows.Forms.MaskedTextBox MaskedTextBoxDate;
        private System.Windows.Forms.TextBox TextBoxCC;
        private System.Windows.Forms.Label LabelCC;
        private System.Windows.Forms.TextBox TextBoxCvv;
        private System.Windows.Forms.Label LabelCvv;
        private System.Windows.Forms.TextBox TextBoxQuantity;
        private System.Windows.Forms.Label LabelMonth;
        private System.Windows.Forms.Label LabelQuatity;
        private System.Windows.Forms.Label LabelBin;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label LabelCountCC;
    }
}