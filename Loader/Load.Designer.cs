namespace Loader
{
    partial class Load
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
            this.components = new System.ComponentModel.Container();
            this.LabelLogo = new System.Windows.Forms.Label();
            this.LabelInfo = new System.Windows.Forms.Label();
            this.IconPictureBoxLogo = new FontAwesome.Sharp.IconPictureBox();
            this.CircularProgressBar = new CircularProgressBar.CircularProgressBar();
            this.TimerFadeOut = new System.Windows.Forms.Timer(this.components);
            this.TimerFadeIn = new System.Windows.Forms.Timer(this.components);
            this.LabelDownload = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.IconPictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelLogo
            // 
            this.LabelLogo.AutoSize = true;
            this.LabelLogo.BackColor = System.Drawing.Color.Transparent;
            this.LabelLogo.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelLogo.ForeColor = System.Drawing.Color.White;
            this.LabelLogo.Location = new System.Drawing.Point(14, 83);
            this.LabelLogo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelLogo.Name = "LabelLogo";
            this.LabelLogo.Size = new System.Drawing.Size(88, 23);
            this.LabelLogo.TabIndex = 71;
            this.LabelLogo.Text = "4SG4RD";
            // 
            // LabelInfo
            // 
            this.LabelInfo.BackColor = System.Drawing.Color.Transparent;
            this.LabelInfo.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelInfo.ForeColor = System.Drawing.Color.White;
            this.LabelInfo.Location = new System.Drawing.Point(125, 26);
            this.LabelInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelInfo.Name = "LabelInfo";
            this.LabelInfo.Size = new System.Drawing.Size(448, 55);
            this.LabelInfo.TabIndex = 68;
            this.LabelInfo.Text = "\"En el cielo Dios soberano, y en la tierra la orden del cartel.\"";
            this.LabelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.IconPictureBoxLogo.IconSize = 96;
            this.IconPictureBoxLogo.Location = new System.Drawing.Point(10, 8);
            this.IconPictureBoxLogo.Margin = new System.Windows.Forms.Padding(0);
            this.IconPictureBoxLogo.Name = "IconPictureBoxLogo";
            this.IconPictureBoxLogo.Size = new System.Drawing.Size(96, 96);
            this.IconPictureBoxLogo.TabIndex = 69;
            this.IconPictureBoxLogo.TabStop = false;
            // 
            // CircularProgressBar
            // 
            this.CircularProgressBar.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner;
            this.CircularProgressBar.AnimationSpeed = 500;
            this.CircularProgressBar.BackColor = System.Drawing.Color.Transparent;
            this.CircularProgressBar.Font = new System.Drawing.Font("Segoe UI", 27F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CircularProgressBar.ForeColor = System.Drawing.Color.Transparent;
            this.CircularProgressBar.InnerColor = System.Drawing.Color.Transparent;
            this.CircularProgressBar.InnerMargin = 2;
            this.CircularProgressBar.InnerWidth = -1;
            this.CircularProgressBar.Location = new System.Drawing.Point(476, 358);
            this.CircularProgressBar.MarqueeAnimationSpeed = 2000;
            this.CircularProgressBar.Name = "CircularProgressBar";
            this.CircularProgressBar.OuterColor = System.Drawing.Color.White;
            this.CircularProgressBar.OuterMargin = -25;
            this.CircularProgressBar.OuterWidth = 26;
            this.CircularProgressBar.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(38)))), ((int)(((byte)(70)))));
            this.CircularProgressBar.ProgressWidth = 5;
            this.CircularProgressBar.SecondaryFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.CircularProgressBar.Size = new System.Drawing.Size(96, 96);
            this.CircularProgressBar.StartAngle = 270;
            this.CircularProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.CircularProgressBar.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.CircularProgressBar.SubscriptMargin = new System.Windows.Forms.Padding(3);
            this.CircularProgressBar.SubscriptText = "";
            this.CircularProgressBar.SuperscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.CircularProgressBar.SuperscriptMargin = new System.Windows.Forms.Padding(10, 30, 0, 0);
            this.CircularProgressBar.SuperscriptText = "";
            this.CircularProgressBar.TabIndex = 70;
            this.CircularProgressBar.Text = "0";
            this.CircularProgressBar.TextMargin = new System.Windows.Forms.Padding(0);
            this.CircularProgressBar.Value = 68;
            // 
            // TimerFadeOut
            // 
            this.TimerFadeOut.Interval = 20;
            this.TimerFadeOut.Tick += new System.EventHandler(this.TimerFadeOut_Tick);
            // 
            // TimerFadeIn
            // 
            this.TimerFadeIn.Interval = 20;
            this.TimerFadeIn.Tick += new System.EventHandler(this.TimerFadeIn_Tick);
            // 
            // LabelDownload
            // 
            this.LabelDownload.BackColor = System.Drawing.Color.Transparent;
            this.LabelDownload.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelDownload.ForeColor = System.Drawing.Color.White;
            this.LabelDownload.Location = new System.Drawing.Point(111, 321);
            this.LabelDownload.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelDownload.Name = "LabelDownload";
            this.LabelDownload.Size = new System.Drawing.Size(360, 24);
            this.LabelDownload.TabIndex = 72;
            this.LabelDownload.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Load
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Loader.Properties.Resources.Baldr_dead_by_Eckersberg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(583, 462);
            this.Controls.Add(this.LabelDownload);
            this.Controls.Add(this.LabelLogo);
            this.Controls.Add(this.LabelInfo);
            this.Controls.Add(this.IconPictureBoxLogo);
            this.Controls.Add(this.CircularProgressBar);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Load";
            this.Opacity = 0D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "4SG4RD";
            this.Load += new System.EventHandler(this.Load_Load);
            ((System.ComponentModel.ISupportInitialize)(this.IconPictureBoxLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelLogo;
        private System.Windows.Forms.Label LabelInfo;
        private FontAwesome.Sharp.IconPictureBox IconPictureBoxLogo;
        private CircularProgressBar.CircularProgressBar CircularProgressBar;
        private System.Windows.Forms.Timer TimerFadeOut;
        private System.Windows.Forms.Timer TimerFadeIn;
        private System.Windows.Forms.Label LabelDownload;
    }
}

