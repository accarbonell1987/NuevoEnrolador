namespace EnroladorStandAloneV2 {
    partial class FrmSplashScreen {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSplashScreen));
            this.ucSpashScreen1 = new EnroladorStandAloneV2.CapaInterfazUsuario.UCSpashScreen();
            this.SuspendLayout();
            // 
            // ucSpashScreen1
            // 
            this.ucSpashScreen1.Location = new System.Drawing.Point(1, 1);
            this.ucSpashScreen1.Name = "ucSpashScreen1";
            this.ucSpashScreen1.Size = new System.Drawing.Size(597, 330);
            this.ucSpashScreen1.TabIndex = 0;
            this.ucSpashScreen1.UseWaitCursor = true;
            // 
            // FrmSplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 334);
            this.ControlBox = false;
            this.Controls.Add(this.ucSpashScreen1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSplashScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cargando Enrolador...";
            this.UseWaitCursor = true;
            this.ResumeLayout(false);

        }

        #endregion

        private CapaInterfazUsuario.UCSpashScreen ucSpashScreen1;
    }
}