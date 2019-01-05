namespace EnroladorStandAloneV2.CapaInterfazUsuario.Enrolar
    {
    partial class UCEnrolar {
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
            this.components = new System.ComponentModel.Container();
            this.DevLabelControlRUT = new DevExpress.XtraEditors.LabelControl();
            this.DevLookUpEditRUTEmpleado = new DevExpress.XtraEditors.LookUpEdit();
            this.bsEmpleados = new System.Windows.Forms.BindingSource(this.components);
            this.DevPanelControlPrincipal = new DevExpress.XtraEditors.PanelControl();
            this.DevGroupControlDatosAcceso = new DevExpress.XtraEditors.GroupControl();
            this.DevGroupControlDatosContratos = new DevExpress.XtraEditors.GroupControl();
            this.DevGroupControlDatosEmpleado = new DevExpress.XtraEditors.GroupControl();
            this.DevPanelControlDatosEmpleado = new DevExpress.XtraEditors.PanelControl();
            this.DevPanelControlContratos = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.DevLookUpEditRUTEmpleado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEmpleados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevPanelControlPrincipal)).BeginInit();
            this.DevPanelControlPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DevGroupControlDatosAcceso)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGroupControlDatosContratos)).BeginInit();
            this.DevGroupControlDatosContratos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DevGroupControlDatosEmpleado)).BeginInit();
            this.DevGroupControlDatosEmpleado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DevPanelControlDatosEmpleado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevPanelControlContratos)).BeginInit();
            this.SuspendLayout();
            // 
            // DevLabelControlRUT
            // 
            this.DevLabelControlRUT.Location = new System.Drawing.Point(12, 10);
            this.DevLabelControlRUT.Name = "DevLabelControlRUT";
            this.DevLabelControlRUT.Size = new System.Drawing.Size(24, 13);
            this.DevLabelControlRUT.TabIndex = 1;
            this.DevLabelControlRUT.Text = "RUT:";
            // 
            // DevLookUpEditRUTEmpleado
            // 
            this.DevLookUpEditRUTEmpleado.Location = new System.Drawing.Point(42, 7);
            this.DevLookUpEditRUTEmpleado.Name = "DevLookUpEditRUTEmpleado";
            this.DevLookUpEditRUTEmpleado.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DevLookUpEditRUTEmpleado.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GuidEmpleado", "Guid Empleado", 93, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("RUT", "RUT", 30, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("EnrollId", "Enroll Id", 49, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Correo", "Correo", 43, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Nombres", "Nombres", 52, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Apellidos", "Apellidos", 52, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TieneContraseña", "Tiene Contraseña", 95, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TipoIdentificacion", "Tipo Identificacion", 97, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Contraseña", "Contraseña", 66, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NumeroTelefono", "Numero Telefono", 92, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Near)});
            this.DevLookUpEditRUTEmpleado.Properties.DataSource = this.bsEmpleados;
            this.DevLookUpEditRUTEmpleado.Properties.DisplayMember = "RUT";
            this.DevLookUpEditRUTEmpleado.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.DevLookUpEditRUTEmpleado.Properties.ValueMember = "EnrollId";
            this.DevLookUpEditRUTEmpleado.Size = new System.Drawing.Size(359, 20);
            this.DevLookUpEditRUTEmpleado.TabIndex = 0;
            // 
            // bsEmpleados
            // 
            this.bsEmpleados.DataSource = typeof(EnroladorAccesoDatos.Dominio.POCOEmpleado);
            // 
            // DevPanelControlPrincipal
            // 
            this.DevPanelControlPrincipal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DevPanelControlPrincipal.Controls.Add(this.DevGroupControlDatosAcceso);
            this.DevPanelControlPrincipal.Controls.Add(this.DevGroupControlDatosContratos);
            this.DevPanelControlPrincipal.Controls.Add(this.DevGroupControlDatosEmpleado);
            this.DevPanelControlPrincipal.Location = new System.Drawing.Point(9, 33);
            this.DevPanelControlPrincipal.Name = "DevPanelControlPrincipal";
            this.DevPanelControlPrincipal.Size = new System.Drawing.Size(1255, 546);
            this.DevPanelControlPrincipal.TabIndex = 2;
            // 
            // DevGroupControlDatosAcceso
            // 
            this.DevGroupControlDatosAcceso.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DevGroupControlDatosAcceso.Location = new System.Drawing.Point(880, 5);
            this.DevGroupControlDatosAcceso.Name = "DevGroupControlDatosAcceso";
            this.DevGroupControlDatosAcceso.Size = new System.Drawing.Size(370, 536);
            this.DevGroupControlDatosAcceso.TabIndex = 2;
            this.DevGroupControlDatosAcceso.Text = "Control Accesos";
            // 
            // DevGroupControlDatosContratos
            // 
            this.DevGroupControlDatosContratos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DevGroupControlDatosContratos.Controls.Add(this.DevPanelControlContratos);
            this.DevGroupControlDatosContratos.Location = new System.Drawing.Point(336, 5);
            this.DevGroupControlDatosContratos.Name = "DevGroupControlDatosContratos";
            this.DevGroupControlDatosContratos.Size = new System.Drawing.Size(538, 536);
            this.DevGroupControlDatosContratos.TabIndex = 1;
            this.DevGroupControlDatosContratos.Text = "Datos Contratos";
            // 
            // DevGroupControlDatosEmpleado
            // 
            this.DevGroupControlDatosEmpleado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DevGroupControlDatosEmpleado.Controls.Add(this.DevPanelControlDatosEmpleado);
            this.DevGroupControlDatosEmpleado.Location = new System.Drawing.Point(5, 5);
            this.DevGroupControlDatosEmpleado.Name = "DevGroupControlDatosEmpleado";
            this.DevGroupControlDatosEmpleado.Size = new System.Drawing.Size(325, 536);
            this.DevGroupControlDatosEmpleado.TabIndex = 0;
            this.DevGroupControlDatosEmpleado.Text = "Datos Empleado";
            // 
            // DevPanelControlDatosEmpleado
            // 
            this.DevPanelControlDatosEmpleado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DevPanelControlDatosEmpleado.Location = new System.Drawing.Point(2, 20);
            this.DevPanelControlDatosEmpleado.Name = "DevPanelControlDatosEmpleado";
            this.DevPanelControlDatosEmpleado.Size = new System.Drawing.Size(321, 514);
            this.DevPanelControlDatosEmpleado.TabIndex = 0;
            // 
            // DevPanelControlContratos
            // 
            this.DevPanelControlContratos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DevPanelControlContratos.Location = new System.Drawing.Point(2, 20);
            this.DevPanelControlContratos.Name = "DevPanelControlContratos";
            this.DevPanelControlContratos.Size = new System.Drawing.Size(534, 514);
            this.DevPanelControlContratos.TabIndex = 0;
            // 
            // UCEnrolar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DevPanelControlPrincipal);
            this.Controls.Add(this.DevLabelControlRUT);
            this.Controls.Add(this.DevLookUpEditRUTEmpleado);
            this.Name = "UCEnrolar";
            this.Size = new System.Drawing.Size(1278, 593);
            this.Load += new System.EventHandler(this.UCEnrolar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DevLookUpEditRUTEmpleado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEmpleados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevPanelControlPrincipal)).EndInit();
            this.DevPanelControlPrincipal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DevGroupControlDatosAcceso)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGroupControlDatosContratos)).EndInit();
            this.DevGroupControlDatosContratos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DevGroupControlDatosEmpleado)).EndInit();
            this.DevGroupControlDatosEmpleado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DevPanelControlDatosEmpleado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevPanelControlContratos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.LookUpEdit DevLookUpEditRUTEmpleado;
        private DevExpress.XtraEditors.LabelControl DevLabelControlRUT;
        private System.Windows.Forms.BindingSource bsEmpleados;
        private DevExpress.XtraEditors.PanelControl DevPanelControlPrincipal;
        private DevExpress.XtraEditors.GroupControl DevGroupControlDatosAcceso;
        private DevExpress.XtraEditors.GroupControl DevGroupControlDatosContratos;
        private DevExpress.XtraEditors.GroupControl DevGroupControlDatosEmpleado;
        private DevExpress.XtraEditors.PanelControl DevPanelControlDatosEmpleado;
        private DevExpress.XtraEditors.PanelControl DevPanelControlContratos;
    }
}
