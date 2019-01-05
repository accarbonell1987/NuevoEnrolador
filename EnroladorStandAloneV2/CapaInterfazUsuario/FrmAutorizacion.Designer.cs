namespace EnroladorStandAloneV2.CapaInterfazUsuario {
    partial class FrmAutorizacion {
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
            this.DevButtonCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.DevLabelControlIntentos = new DevExpress.XtraEditors.LabelControl();
            this.DevLabelControlMensaje = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // DevButtonCancelar
            // 
            this.DevButtonCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.DevButtonCancelar.ImageUri.Uri = "Cancel;Size16x16;Office2013";
            this.DevButtonCancelar.Location = new System.Drawing.Point(444, 121);
            this.DevButtonCancelar.Name = "DevButtonCancelar";
            this.DevButtonCancelar.Size = new System.Drawing.Size(79, 23);
            this.DevButtonCancelar.TabIndex = 0;
            this.DevButtonCancelar.Text = "Cancelar";
            this.DevButtonCancelar.Click += new System.EventHandler(this.DevButtonCancelar_Click);
            // 
            // DevLabelControlIntentos
            // 
            this.DevLabelControlIntentos.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.DevLabelControlIntentos.Appearance.ForeColor = System.Drawing.Color.Red;
            this.DevLabelControlIntentos.Appearance.Options.UseFont = true;
            this.DevLabelControlIntentos.Appearance.Options.UseForeColor = true;
            this.DevLabelControlIntentos.Location = new System.Drawing.Point(328, 126);
            this.DevLabelControlIntentos.Name = "DevLabelControlIntentos";
            this.DevLabelControlIntentos.Size = new System.Drawing.Size(103, 13);
            this.DevLabelControlIntentos.TabIndex = 1;
            this.DevLabelControlIntentos.Text = "Quedan N intentos";
            this.DevLabelControlIntentos.Visible = false;
            // 
            // DevLabelControlMensaje
            // 
            this.DevLabelControlMensaje.Location = new System.Drawing.Point(12, 12);
            this.DevLabelControlMensaje.Name = "DevLabelControlMensaje";
            this.DevLabelControlMensaje.Size = new System.Drawing.Size(40, 13);
            this.DevLabelControlMensaje.TabIndex = 2;
            this.DevLabelControlMensaje.Text = "Mensaje";
            // 
            // FrmAutorizacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.DevButtonCancelar;
            this.ClientSize = new System.Drawing.Size(535, 156);
            this.Controls.Add(this.DevLabelControlMensaje);
            this.Controls.Add(this.DevLabelControlIntentos);
            this.Controls.Add(this.DevButtonCancelar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmAutorizacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Autorizar Acción";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAutorizacion_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton DevButtonCancelar;
        private DevExpress.XtraEditors.LabelControl DevLabelControlIntentos;
        private DevExpress.XtraEditors.LabelControl DevLabelControlMensaje;
    }
}