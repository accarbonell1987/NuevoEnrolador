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
            this.DevSplitContainerControlAccesos = new DevExpress.XtraEditors.SplitContainerControl();
            this.DevPanelControlAsistencia = new DevExpress.XtraEditors.PanelControl();
            this.DevPanelControlCasinos = new DevExpress.XtraEditors.PanelControl();
            this.DevGroupControlDatosContratos = new DevExpress.XtraEditors.GroupControl();
            this.DevPanelControlContratos = new DevExpress.XtraEditors.PanelControl();
            this.DevGroupControlDatosEmpleado = new DevExpress.XtraEditors.GroupControl();
            this.DevPanelControlDatosEmpleado = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.DevLookUpEditRUTEmpleado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEmpleados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevPanelControlPrincipal)).BeginInit();
            this.DevPanelControlPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DevGroupControlDatosAcceso)).BeginInit();
            this.DevGroupControlDatosAcceso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DevSplitContainerControlAccesos)).BeginInit();
            this.DevSplitContainerControlAccesos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DevPanelControlAsistencia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevPanelControlCasinos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGroupControlDatosContratos)).BeginInit();
            this.DevGroupControlDatosContratos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DevPanelControlContratos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGroupControlDatosEmpleado)).BeginInit();
            this.DevGroupControlDatosEmpleado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DevPanelControlDatosEmpleado)).BeginInit();
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
            this.DevGroupControlDatosAcceso.Controls.Add(this.DevSplitContainerControlAccesos);
            this.DevGroupControlDatosAcceso.Location = new System.Drawing.Point(786, 5);
            this.DevGroupControlDatosAcceso.Name = "DevGroupControlDatosAcceso";
            this.DevGroupControlDatosAcceso.Size = new System.Drawing.Size(464, 536);
            this.DevGroupControlDatosAcceso.TabIndex = 2;
            this.DevGroupControlDatosAcceso.Text = "Control Accesos";
            // 
            // DevSplitContainerControlAccesos
            // 
            this.DevSplitContainerControlAccesos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DevSplitContainerControlAccesos.Horizontal = false;
            this.DevSplitContainerControlAccesos.Location = new System.Drawing.Point(2, 20);
            this.DevSplitContainerControlAccesos.Name = "DevSplitContainerControlAccesos";
            this.DevSplitContainerControlAccesos.Panel1.Controls.Add(this.DevPanelControlAsistencia);
            this.DevSplitContainerControlAccesos.Panel1.Text = "Panel1";
            this.DevSplitContainerControlAccesos.Panel2.Controls.Add(this.DevPanelControlCasinos);
            this.DevSplitContainerControlAccesos.Panel2.Text = "Panel2";
            this.DevSplitContainerControlAccesos.Size = new System.Drawing.Size(460, 514);
            this.DevSplitContainerControlAccesos.SplitterPosition = 251;
            this.DevSplitContainerControlAccesos.TabIndex = 0;
            this.DevSplitContainerControlAccesos.Text = "splitContainerControl1";
            // 
            // DevPanelControlAsistencia
            // 
            this.DevPanelControlAsistencia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DevPanelControlAsistencia.Location = new System.Drawing.Point(0, 0);
            this.DevPanelControlAsistencia.Name = "DevPanelControlAsistencia";
            this.DevPanelControlAsistencia.Size = new System.Drawing.Size(460, 251);
            this.DevPanelControlAsistencia.TabIndex = 0;
            // 
            // DevPanelControlCasinos
            // 
            this.DevPanelControlCasinos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DevPanelControlCasinos.Location = new System.Drawing.Point(0, 0);
            this.DevPanelControlCasinos.Name = "DevPanelControlCasinos";
            this.DevPanelControlCasinos.Size = new System.Drawing.Size(460, 258);
            this.DevPanelControlCasinos.TabIndex = 0;
            // 
            // DevGroupControlDatosContratos
            // 
            this.DevGroupControlDatosContratos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DevGroupControlDatosContratos.Controls.Add(this.DevPanelControlContratos);
            this.DevGroupControlDatosContratos.Location = new System.Drawing.Point(334, 5);
            this.DevGroupControlDatosContratos.Name = "DevGroupControlDatosContratos";
            this.DevGroupControlDatosContratos.Size = new System.Drawing.Size(448, 536);
            this.DevGroupControlDatosContratos.TabIndex = 1;
            this.DevGroupControlDatosContratos.Text = "Datos Contratos";
            // 
            // DevPanelControlContratos
            // 
            this.DevPanelControlContratos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DevPanelControlContratos.Location = new System.Drawing.Point(2, 20);
            this.DevPanelControlContratos.Name = "DevPanelControlContratos";
            this.DevPanelControlContratos.Size = new System.Drawing.Size(444, 514);
            this.DevPanelControlContratos.TabIndex = 0;
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
            this.DevGroupControlDatosAcceso.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DevSplitContainerControlAccesos)).EndInit();
            this.DevSplitContainerControlAccesos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DevPanelControlAsistencia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevPanelControlCasinos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGroupControlDatosContratos)).EndInit();
            this.DevGroupControlDatosContratos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DevPanelControlContratos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGroupControlDatosEmpleado)).EndInit();
            this.DevGroupControlDatosEmpleado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DevPanelControlDatosEmpleado)).EndInit();
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
        private DevExpress.XtraEditors.SplitContainerControl DevSplitContainerControlAccesos;
        private DevExpress.XtraEditors.PanelControl DevPanelControlAsistencia;
        private DevExpress.XtraEditors.PanelControl DevPanelControlCasinos;
    }
}
