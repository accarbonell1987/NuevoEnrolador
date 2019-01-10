namespace EnroladorStandAloneV2.CapaInterfazUsuario {
    partial class UCSpashScreen {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCSpashScreen));
            this.DevPanelControlSplashScreen = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.DevPanelControlSplashScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // DevPanelControlSplashScreen
            // 
            this.DevPanelControlSplashScreen.ContentImage = ((System.Drawing.Image)(resources.GetObject("DevPanelControlSplashScreen.ContentImage")));
            this.DevPanelControlSplashScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DevPanelControlSplashScreen.Location = new System.Drawing.Point(0, 0);
            this.DevPanelControlSplashScreen.Name = "DevPanelControlSplashScreen";
            this.DevPanelControlSplashScreen.Size = new System.Drawing.Size(597, 330);
            this.DevPanelControlSplashScreen.TabIndex = 0;
            // 
            // UCSpashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DevPanelControlSplashScreen);
            this.Name = "UCSpashScreen";
            this.Size = new System.Drawing.Size(597, 330);
            ((System.ComponentModel.ISupportInitialize)(this.DevPanelControlSplashScreen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl DevPanelControlSplashScreen;
    }
}
