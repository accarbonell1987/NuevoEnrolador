namespace EnroladorStandAloneV2.CapaInterfazUsuario.Enrolar {
    partial class UCEnrolador {
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
            this.DevGroupControlRUT = new DevExpress.XtraEditors.GroupControl();
            this.DevLookUpEditRUTEmpleado = new DevExpress.XtraEditors.LookUpEdit();
            this.bsEmpleados = new System.Windows.Forms.BindingSource(this.components);
            this.DevGroupControlDatosEmpleado = new DevExpress.XtraEditors.GroupControl();
            this.DevPanelControlDatosEmpleado = new DevExpress.XtraEditors.PanelControl();
            this.DevGroupControlAccesos = new DevExpress.XtraEditors.GroupControl();
            this.DevSplitContainerControlAccesos = new DevExpress.XtraEditors.SplitContainerControl();
            this.DevPanelControlAsistencia = new DevExpress.XtraEditors.PanelControl();
            this.DevPanelControlCasinos = new DevExpress.XtraEditors.PanelControl();
            this.DevGroupControlContratos = new DevExpress.XtraEditors.GroupControl();
            this.DevPanelControlContratos = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.DevGroupControlRUT)).BeginInit();
            this.DevGroupControlRUT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DevLookUpEditRUTEmpleado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEmpleados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGroupControlDatosEmpleado)).BeginInit();
            this.DevGroupControlDatosEmpleado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DevPanelControlDatosEmpleado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGroupControlAccesos)).BeginInit();
            this.DevGroupControlAccesos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DevSplitContainerControlAccesos)).BeginInit();
            this.DevSplitContainerControlAccesos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DevPanelControlAsistencia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevPanelControlCasinos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGroupControlContratos)).BeginInit();
            this.DevGroupControlContratos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DevPanelControlContratos)).BeginInit();
            this.SuspendLayout();
            // 
            // DevGroupControlRUT
            // 
            this.DevGroupControlRUT.Controls.Add(this.DevLookUpEditRUTEmpleado);
            this.DevGroupControlRUT.Dock = System.Windows.Forms.DockStyle.Top;
            this.DevGroupControlRUT.Location = new System.Drawing.Point(0, 0);
            this.DevGroupControlRUT.Name = "DevGroupControlRUT";
            this.DevGroupControlRUT.ShowCaption = false;
            this.DevGroupControlRUT.Size = new System.Drawing.Size(1306, 30);
            this.DevGroupControlRUT.TabIndex = 0;
            this.DevGroupControlRUT.Text = "RUT";
            // 
            // DevLookUpEditRUTEmpleado
            // 
            this.DevLookUpEditRUTEmpleado.Location = new System.Drawing.Point(7, 4);
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
            this.DevLookUpEditRUTEmpleado.TabIndex = 1;
            this.DevLookUpEditRUTEmpleado.EditValueChanged += new System.EventHandler(this.DevLookUpEditRUTEmpleado_EditValueChanged);
            // 
            // bsEmpleados
            // 
            this.bsEmpleados.DataSource = typeof(EnroladorAccesoDatos.Dominio.POCOEmpleado);
            // 
            // DevGroupControlDatosEmpleado
            // 
            this.DevGroupControlDatosEmpleado.Controls.Add(this.DevPanelControlDatosEmpleado);
            this.DevGroupControlDatosEmpleado.Dock = System.Windows.Forms.DockStyle.Left;
            this.DevGroupControlDatosEmpleado.Location = new System.Drawing.Point(0, 30);
            this.DevGroupControlDatosEmpleado.MinimumSize = new System.Drawing.Size(368, 0);
            this.DevGroupControlDatosEmpleado.Name = "DevGroupControlDatosEmpleado";
            this.DevGroupControlDatosEmpleado.Size = new System.Drawing.Size(368, 669);
            this.DevGroupControlDatosEmpleado.TabIndex = 1;
            this.DevGroupControlDatosEmpleado.Text = "Datos del Empleado";
            // 
            // DevPanelControlDatosEmpleado
            // 
            this.DevPanelControlDatosEmpleado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DevPanelControlDatosEmpleado.Location = new System.Drawing.Point(2, 20);
            this.DevPanelControlDatosEmpleado.Name = "DevPanelControlDatosEmpleado";
            this.DevPanelControlDatosEmpleado.Size = new System.Drawing.Size(364, 647);
            this.DevPanelControlDatosEmpleado.TabIndex = 1;
            // 
            // DevGroupControlAccesos
            // 
            this.DevGroupControlAccesos.Controls.Add(this.DevSplitContainerControlAccesos);
            this.DevGroupControlAccesos.Dock = System.Windows.Forms.DockStyle.Right;
            this.DevGroupControlAccesos.Location = new System.Drawing.Point(859, 30);
            this.DevGroupControlAccesos.MinimumSize = new System.Drawing.Size(447, 0);
            this.DevGroupControlAccesos.Name = "DevGroupControlAccesos";
            this.DevGroupControlAccesos.Size = new System.Drawing.Size(447, 669);
            this.DevGroupControlAccesos.TabIndex = 2;
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
            this.DevSplitContainerControlAccesos.Size = new System.Drawing.Size(443, 647);
            this.DevSplitContainerControlAccesos.SplitterPosition = 299;
            this.DevSplitContainerControlAccesos.TabIndex = 1;
            this.DevSplitContainerControlAccesos.Text = "splitContainerControl1";
            // 
            // DevPanelControlAsistencia
            // 
            this.DevPanelControlAsistencia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DevPanelControlAsistencia.Location = new System.Drawing.Point(0, 0);
            this.DevPanelControlAsistencia.Name = "DevPanelControlAsistencia";
            this.DevPanelControlAsistencia.Size = new System.Drawing.Size(443, 299);
            this.DevPanelControlAsistencia.TabIndex = 0;
            // 
            // DevPanelControlCasinos
            // 
            this.DevPanelControlCasinos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DevPanelControlCasinos.Location = new System.Drawing.Point(0, 0);
            this.DevPanelControlCasinos.Name = "DevPanelControlCasinos";
            this.DevPanelControlCasinos.Size = new System.Drawing.Size(443, 343);
            this.DevPanelControlCasinos.TabIndex = 0;
            // 
            // DevGroupControlContratos
            // 
            this.DevGroupControlContratos.Controls.Add(this.DevPanelControlContratos);
            this.DevGroupControlContratos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DevGroupControlContratos.Location = new System.Drawing.Point(368, 30);
            this.DevGroupControlContratos.Name = "DevGroupControlContratos";
            this.DevGroupControlContratos.Size = new System.Drawing.Size(491, 669);
            this.DevGroupControlContratos.TabIndex = 3;
            this.DevGroupControlContratos.Text = "Contratos";
            // 
            // DevPanelControlContratos
            // 
            this.DevPanelControlContratos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DevPanelControlContratos.Location = new System.Drawing.Point(2, 20);
            this.DevPanelControlContratos.Name = "DevPanelControlContratos";
            this.DevPanelControlContratos.Size = new System.Drawing.Size(487, 647);
            this.DevPanelControlContratos.TabIndex = 1;
            // 
            // UCEnrolador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DevGroupControlContratos);
            this.Controls.Add(this.DevGroupControlAccesos);
            this.Controls.Add(this.DevGroupControlDatosEmpleado);
            this.Controls.Add(this.DevGroupControlRUT);
            this.DoubleBuffered = true;
            this.Name = "UCEnrolador";
            this.Size = new System.Drawing.Size(1306, 699);
            this.Load += new System.EventHandler(this.UCEnrolador_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DevGroupControlRUT)).EndInit();
            this.DevGroupControlRUT.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DevLookUpEditRUTEmpleado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEmpleados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGroupControlDatosEmpleado)).EndInit();
            this.DevGroupControlDatosEmpleado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DevPanelControlDatosEmpleado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGroupControlAccesos)).EndInit();
            this.DevGroupControlAccesos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DevSplitContainerControlAccesos)).EndInit();
            this.DevSplitContainerControlAccesos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DevPanelControlAsistencia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevPanelControlCasinos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGroupControlContratos)).EndInit();
            this.DevGroupControlContratos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DevPanelControlContratos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl DevGroupControlRUT;
        private DevExpress.XtraEditors.GroupControl DevGroupControlDatosEmpleado;
        private DevExpress.XtraEditors.GroupControl DevGroupControlAccesos;
        private DevExpress.XtraEditors.GroupControl DevGroupControlContratos;
        private DevExpress.XtraEditors.LookUpEdit DevLookUpEditRUTEmpleado;
        private DevExpress.XtraEditors.SplitContainerControl DevSplitContainerControlAccesos;
        private DevExpress.XtraEditors.PanelControl DevPanelControlAsistencia;
        private DevExpress.XtraEditors.PanelControl DevPanelControlCasinos;
        private DevExpress.XtraEditors.PanelControl DevPanelControlContratos;
        private DevExpress.XtraEditors.PanelControl DevPanelControlDatosEmpleado;
        private System.Windows.Forms.BindingSource bsEmpleados;
    }
}
