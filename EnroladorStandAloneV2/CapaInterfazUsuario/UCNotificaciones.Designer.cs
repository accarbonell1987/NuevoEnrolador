namespace EnroladorStandAloneV2.CapaInterfazUsuario {
    partial class UCNotificaciones {
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
            this.DevPanelControlWarning = new DevExpress.XtraEditors.PanelControl();
            this.DevLabelControlNotificacion = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.DevPanelControlWarning)).BeginInit();
            this.DevPanelControlWarning.SuspendLayout();
            this.SuspendLayout();
            // 
            // DevPanelControlWarning
            // 
            this.DevPanelControlWarning.ContentImage = global::EnroladorStandAloneV2.Properties.Resources.apply_32x32;
            this.DevPanelControlWarning.ContentImageAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.DevPanelControlWarning.Controls.Add(this.DevLabelControlNotificacion);
            this.DevPanelControlWarning.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DevPanelControlWarning.Location = new System.Drawing.Point(10, 10);
            this.DevPanelControlWarning.Name = "DevPanelControlWarning";
            this.DevPanelControlWarning.Size = new System.Drawing.Size(801, 40);
            this.DevPanelControlWarning.TabIndex = 4;
            this.DevPanelControlWarning.Click += new System.EventHandler(this.DevPanelControlNotificaciones_Click);
            this.DevPanelControlWarning.MouseEnter += new System.EventHandler(this.DevPanelControlWarning_MouseEnter);
            // 
            // DevLabelControlNotificacion
            // 
            this.DevLabelControlNotificacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DevLabelControlNotificacion.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.DevLabelControlNotificacion.Location = new System.Drawing.Point(47, 13);
            this.DevLabelControlNotificacion.Name = "DevLabelControlNotificacion";
            this.DevLabelControlNotificacion.Size = new System.Drawing.Size(113, 13);
            this.DevLabelControlNotificacion.TabIndex = 3;
            this.DevLabelControlNotificacion.Text = "Mensaje de Notificacion";
            // 
            // UCNotificaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DevPanelControlWarning);
            this.Name = "UCNotificaciones";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(821, 60);
            ((System.ComponentModel.ISupportInitialize)(this.DevPanelControlWarning)).EndInit();
            this.DevPanelControlWarning.ResumeLayout(false);
            this.DevPanelControlWarning.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl DevLabelControlNotificacion;
        private DevExpress.XtraEditors.PanelControl DevPanelControlWarning;
    }
}
