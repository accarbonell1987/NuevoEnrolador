namespace EnroladorStandAloneV2.CapaInterfazUsuario {
    partial class UCCargarDatos {
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
            this.DevProgressBarControlActual = new DevExpress.XtraEditors.ProgressBarControl();
            this.DevProgressBarControlTotal = new DevExpress.XtraEditors.ProgressBarControl();
            this.DevLabelControlTotal = new DevExpress.XtraEditors.LabelControl();
            this.DevLabelControlActual = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.DevProgressBarControlActual.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevProgressBarControlTotal.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // DevProgressBarControlActual
            // 
            this.DevProgressBarControlActual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DevProgressBarControlActual.Location = new System.Drawing.Point(569, 14);
            this.DevProgressBarControlActual.Name = "DevProgressBarControlActual";
            this.DevProgressBarControlActual.Size = new System.Drawing.Size(329, 10);
            this.DevProgressBarControlActual.TabIndex = 0;
            // 
            // DevProgressBarControlTotal
            // 
            this.DevProgressBarControlTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DevProgressBarControlTotal.Location = new System.Drawing.Point(23, 14);
            this.DevProgressBarControlTotal.Name = "DevProgressBarControlTotal";
            this.DevProgressBarControlTotal.Size = new System.Drawing.Size(532, 10);
            this.DevProgressBarControlTotal.TabIndex = 1;
            // 
            // DevLabelControlTotal
            // 
            this.DevLabelControlTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DevLabelControlTotal.Location = new System.Drawing.Point(23, 30);
            this.DevLabelControlTotal.Name = "DevLabelControlTotal";
            this.DevLabelControlTotal.Size = new System.Drawing.Size(28, 13);
            this.DevLabelControlTotal.TabIndex = 2;
            this.DevLabelControlTotal.Text = "Total:";
            // 
            // DevLabelControlActual
            // 
            this.DevLabelControlActual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DevLabelControlActual.Location = new System.Drawing.Point(569, 30);
            this.DevLabelControlActual.Name = "DevLabelControlActual";
            this.DevLabelControlActual.Size = new System.Drawing.Size(34, 13);
            this.DevLabelControlActual.TabIndex = 3;
            this.DevLabelControlActual.Text = "Actual:";
            // 
            // UCCargarDatos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DevLabelControlActual);
            this.Controls.Add(this.DevLabelControlTotal);
            this.Controls.Add(this.DevProgressBarControlTotal);
            this.Controls.Add(this.DevProgressBarControlActual);
            this.Name = "UCCargarDatos";
            this.Size = new System.Drawing.Size(917, 77);
            ((System.ComponentModel.ISupportInitialize)(this.DevProgressBarControlActual.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevProgressBarControlTotal.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ProgressBarControl DevProgressBarControlActual;
        private DevExpress.XtraEditors.ProgressBarControl DevProgressBarControlTotal;
        private DevExpress.XtraEditors.LabelControl DevLabelControlTotal;
        private DevExpress.XtraEditors.LabelControl DevLabelControlActual;
    }
}
