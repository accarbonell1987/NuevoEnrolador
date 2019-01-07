namespace EnroladorStandAloneV2.CapaInterfazUsuario.Enrolar {
    partial class UCEnrolarAsistencia {
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
            this.DevGridControlAsistencias = new DevExpress.XtraGrid.GridControl();
            this.bsEmpleadoDispositivos = new System.Windows.Forms.BindingSource(this.components);
            this.DevGridViewAsistencias = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.DevGroupControlAsistencia = new DevExpress.XtraEditors.GroupControl();
            this.DevSimpleButtonAdicionar = new DevExpress.XtraEditors.SimpleButton();
            this.DevLayoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.DevLookUpEditDispositivo = new DevExpress.XtraEditors.LookUpEdit();
            this.bsDispositivos = new System.Windows.Forms.BindingSource(this.components);
            this.DevLookUpEditInstalacion = new DevExpress.XtraEditors.LookUpEdit();
            this.bsInstalaciones = new System.Windows.Forms.BindingSource(this.components);
            this.DevLayoutControlGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.DevLayoutControlItemInstalacion = new DevExpress.XtraLayout.LayoutControlItem();
            this.DevLayoutControlItemDispositivo = new DevExpress.XtraLayout.LayoutControlItem();
            this.colGuidDispositivo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGuidInstalacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNombreDispositivo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHost = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPuerto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTipo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNombreInstalacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNombreCadena = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DevGridControlAsistencias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEmpleadoDispositivos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGridViewAsistencias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGroupControlAsistencia)).BeginInit();
            this.DevGroupControlAsistencia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControl)).BeginInit();
            this.DevLayoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DevLookUpEditDispositivo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDispositivos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLookUpEditInstalacion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsInstalaciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlItemInstalacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlItemDispositivo)).BeginInit();
            this.SuspendLayout();
            // 
            // DevGridControlAsistencias
            // 
            this.DevGridControlAsistencias.DataSource = this.bsEmpleadoDispositivos;
            this.DevGridControlAsistencias.Location = new System.Drawing.Point(3, 3);
            this.DevGridControlAsistencias.MainView = this.DevGridViewAsistencias;
            this.DevGridControlAsistencias.Name = "DevGridControlAsistencias";
            this.DevGridControlAsistencias.Size = new System.Drawing.Size(441, 234);
            this.DevGridControlAsistencias.TabIndex = 0;
            this.DevGridControlAsistencias.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.DevGridViewAsistencias});
            // 
            // bsEmpleadoDispositivos
            // 
            this.bsEmpleadoDispositivos.DataSource = typeof(EnroladorAccesoDatos.Dominio.POCODispositivo);
            // 
            // DevGridViewAsistencias
            // 
            this.DevGridViewAsistencias.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colGuidDispositivo,
            this.colGuidInstalacion,
            this.colNombreCadena,
            this.colNombreInstalacion,
            this.colNombreDispositivo,
            this.colHost,
            this.colPuerto,
            this.colTipo});
            this.DevGridViewAsistencias.GridControl = this.DevGridControlAsistencias;
            this.DevGridViewAsistencias.Name = "DevGridViewAsistencias";
            // 
            // DevGroupControlAsistencia
            // 
            this.DevGroupControlAsistencia.Controls.Add(this.DevSimpleButtonAdicionar);
            this.DevGroupControlAsistencia.Controls.Add(this.DevLayoutControl);
            this.DevGroupControlAsistencia.Location = new System.Drawing.Point(3, 243);
            this.DevGroupControlAsistencia.Name = "DevGroupControlAsistencia";
            this.DevGroupControlAsistencia.Size = new System.Drawing.Size(441, 114);
            this.DevGroupControlAsistencia.TabIndex = 1;
            this.DevGroupControlAsistencia.Text = "Adicionar Asistencia";
            // 
            // DevSimpleButtonAdicionar
            // 
            this.DevSimpleButtonAdicionar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.DevSimpleButtonAdicionar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.DevSimpleButtonAdicionar.ImageUri.Uri = "Add;Size16x16;Office2013";
            this.DevSimpleButtonAdicionar.Location = new System.Drawing.Point(382, 86);
            this.DevSimpleButtonAdicionar.Name = "DevSimpleButtonAdicionar";
            this.DevSimpleButtonAdicionar.Size = new System.Drawing.Size(43, 23);
            this.DevSimpleButtonAdicionar.TabIndex = 1;
            // 
            // DevLayoutControl
            // 
            this.DevLayoutControl.Controls.Add(this.DevLookUpEditDispositivo);
            this.DevLayoutControl.Controls.Add(this.DevLookUpEditInstalacion);
            this.DevLayoutControl.Location = new System.Drawing.Point(5, 23);
            this.DevLayoutControl.Name = "DevLayoutControl";
            this.DevLayoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(600, 459, 450, 400);
            this.DevLayoutControl.Root = this.DevLayoutControlGroup;
            this.DevLayoutControl.Size = new System.Drawing.Size(431, 70);
            this.DevLayoutControl.TabIndex = 0;
            this.DevLayoutControl.Text = "layoutControl1";
            // 
            // DevLookUpEditDispositivo
            // 
            this.DevLookUpEditDispositivo.Enabled = false;
            this.DevLookUpEditDispositivo.Location = new System.Drawing.Point(71, 36);
            this.DevLookUpEditDispositivo.Name = "DevLookUpEditDispositivo";
            this.DevLookUpEditDispositivo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DevLookUpEditDispositivo.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GuidDispositivo", "Guid Dispositivo", 98, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GuidInstalacion", "Guid Instalacion", 86, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NombreDispositivo", "Nombre Dispositivo", 101, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Host", "Host", 32, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Puerto", "Puerto", 42, DevExpress.Utils.FormatType.Numeric, "", true, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Tipo", "Tipo", 30, DevExpress.Utils.FormatType.Numeric, "", true, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NombreInstalacion", "Nombre Instalacion", 102, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NombreCadena", "Nombre Cadena", 87, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Near)});
            this.DevLookUpEditDispositivo.Properties.DataSource = this.bsDispositivos;
            this.DevLookUpEditDispositivo.Properties.DisplayMember = "NombreDispositivo";
            this.DevLookUpEditDispositivo.Properties.ValueMember = "GuidDispositivo";
            this.DevLookUpEditDispositivo.Size = new System.Drawing.Size(348, 20);
            this.DevLookUpEditDispositivo.StyleController = this.DevLayoutControl;
            this.DevLookUpEditDispositivo.TabIndex = 5;
            // 
            // bsDispositivos
            // 
            this.bsDispositivos.DataSource = typeof(EnroladorAccesoDatos.Dominio.POCODispositivo);
            // 
            // DevLookUpEditInstalacion
            // 
            this.DevLookUpEditInstalacion.Location = new System.Drawing.Point(71, 12);
            this.DevLookUpEditInstalacion.Name = "DevLookUpEditInstalacion";
            this.DevLookUpEditInstalacion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DevLookUpEditInstalacion.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GuidInstalacion", "Guid Instalacion", 99, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GuidCadena", "Guid Cadena", 71, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NombreCadena", "Nombre Cadena", 87, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NombreInstalacion", "Nombre Instalacion", 102, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.DevLookUpEditInstalacion.Properties.DataSource = this.bsInstalaciones;
            this.DevLookUpEditInstalacion.Properties.DisplayMember = "NombreInstalacion";
            this.DevLookUpEditInstalacion.Properties.ValueMember = "GuidInstalacion";
            this.DevLookUpEditInstalacion.Size = new System.Drawing.Size(348, 20);
            this.DevLookUpEditInstalacion.StyleController = this.DevLayoutControl;
            this.DevLookUpEditInstalacion.TabIndex = 4;
            this.DevLookUpEditInstalacion.EditValueChanged += new System.EventHandler(this.DevLookUpEditInstalacion_EditValueChanged);
            // 
            // bsInstalaciones
            // 
            this.bsInstalaciones.DataSource = typeof(EnroladorAccesoDatos.Dominio.POCOInstalacion);
            // 
            // DevLayoutControlGroup
            // 
            this.DevLayoutControlGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.DevLayoutControlGroup.GroupBordersVisible = false;
            this.DevLayoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.DevLayoutControlItemInstalacion,
            this.DevLayoutControlItemDispositivo});
            this.DevLayoutControlGroup.Location = new System.Drawing.Point(0, 0);
            this.DevLayoutControlGroup.Name = "DevLayoutControlGroup";
            this.DevLayoutControlGroup.Size = new System.Drawing.Size(431, 70);
            this.DevLayoutControlGroup.TextVisible = false;
            // 
            // DevLayoutControlItemInstalacion
            // 
            this.DevLayoutControlItemInstalacion.Control = this.DevLookUpEditInstalacion;
            this.DevLayoutControlItemInstalacion.Location = new System.Drawing.Point(0, 0);
            this.DevLayoutControlItemInstalacion.Name = "DevLayoutControlItemInstalacion";
            this.DevLayoutControlItemInstalacion.Size = new System.Drawing.Size(411, 24);
            this.DevLayoutControlItemInstalacion.Text = "Instalacion:";
            this.DevLayoutControlItemInstalacion.TextSize = new System.Drawing.Size(56, 13);
            // 
            // DevLayoutControlItemDispositivo
            // 
            this.DevLayoutControlItemDispositivo.Control = this.DevLookUpEditDispositivo;
            this.DevLayoutControlItemDispositivo.Location = new System.Drawing.Point(0, 24);
            this.DevLayoutControlItemDispositivo.Name = "DevLayoutControlItemDispositivo";
            this.DevLayoutControlItemDispositivo.Size = new System.Drawing.Size(411, 26);
            this.DevLayoutControlItemDispositivo.Text = "Dispositivo:";
            this.DevLayoutControlItemDispositivo.TextSize = new System.Drawing.Size(56, 13);
            // 
            // colGuidDispositivo
            // 
            this.colGuidDispositivo.FieldName = "GuidDispositivo";
            this.colGuidDispositivo.Name = "colGuidDispositivo";
            // 
            // colGuidInstalacion
            // 
            this.colGuidInstalacion.FieldName = "GuidInstalacion";
            this.colGuidInstalacion.Name = "colGuidInstalacion";
            // 
            // colNombreDispositivo
            // 
            this.colNombreDispositivo.Caption = "Dispositivo";
            this.colNombreDispositivo.FieldName = "NombreDispositivo";
            this.colNombreDispositivo.Name = "colNombreDispositivo";
            this.colNombreDispositivo.Visible = true;
            this.colNombreDispositivo.VisibleIndex = 2;
            // 
            // colHost
            // 
            this.colHost.FieldName = "Host";
            this.colHost.Name = "colHost";
            this.colHost.Visible = true;
            this.colHost.VisibleIndex = 3;
            // 
            // colPuerto
            // 
            this.colPuerto.FieldName = "Puerto";
            this.colPuerto.Name = "colPuerto";
            this.colPuerto.Visible = true;
            this.colPuerto.VisibleIndex = 4;
            // 
            // colTipo
            // 
            this.colTipo.FieldName = "Tipo";
            this.colTipo.Name = "colTipo";
            this.colTipo.Visible = true;
            this.colTipo.VisibleIndex = 5;
            // 
            // colNombreInstalacion
            // 
            this.colNombreInstalacion.Caption = "Instalacion";
            this.colNombreInstalacion.FieldName = "NombreInstalacion";
            this.colNombreInstalacion.Name = "colNombreInstalacion";
            this.colNombreInstalacion.Visible = true;
            this.colNombreInstalacion.VisibleIndex = 1;
            // 
            // colNombreCadena
            // 
            this.colNombreCadena.Caption = "Cadena";
            this.colNombreCadena.FieldName = "NombreCadena";
            this.colNombreCadena.Name = "colNombreCadena";
            this.colNombreCadena.Visible = true;
            this.colNombreCadena.VisibleIndex = 0;
            // 
            // UCEnrolarAsistencia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DevGroupControlAsistencia);
            this.Controls.Add(this.DevGridControlAsistencias);
            this.Name = "UCEnrolarAsistencia";
            this.Size = new System.Drawing.Size(447, 361);
            this.Load += new System.EventHandler(this.UCEnrolarAsistencia_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DevGridControlAsistencias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEmpleadoDispositivos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGridViewAsistencias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGroupControlAsistencia)).EndInit();
            this.DevGroupControlAsistencia.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControl)).EndInit();
            this.DevLayoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DevLookUpEditDispositivo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDispositivos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLookUpEditInstalacion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsInstalaciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlItemInstalacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlItemDispositivo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl DevGridControlAsistencias;
        private DevExpress.XtraGrid.Views.Grid.GridView DevGridViewAsistencias;
        private DevExpress.XtraEditors.GroupControl DevGroupControlAsistencia;
        private DevExpress.XtraLayout.LayoutControl DevLayoutControl;
        private DevExpress.XtraEditors.LookUpEdit DevLookUpEditInstalacion;
        private DevExpress.XtraLayout.LayoutControlGroup DevLayoutControlGroup;
        private DevExpress.XtraLayout.LayoutControlItem DevLayoutControlItemInstalacion;
        private DevExpress.XtraEditors.LookUpEdit DevLookUpEditDispositivo;
        private DevExpress.XtraLayout.LayoutControlItem DevLayoutControlItemDispositivo;
        private DevExpress.XtraEditors.SimpleButton DevSimpleButtonAdicionar;
        private System.Windows.Forms.BindingSource bsDispositivos;
        private System.Windows.Forms.BindingSource bsInstalaciones;
        private System.Windows.Forms.BindingSource bsEmpleadoDispositivos;
        private DevExpress.XtraGrid.Columns.GridColumn colGuidDispositivo;
        private DevExpress.XtraGrid.Columns.GridColumn colGuidInstalacion;
        private DevExpress.XtraGrid.Columns.GridColumn colNombreCadena;
        private DevExpress.XtraGrid.Columns.GridColumn colNombreInstalacion;
        private DevExpress.XtraGrid.Columns.GridColumn colNombreDispositivo;
        private DevExpress.XtraGrid.Columns.GridColumn colHost;
        private DevExpress.XtraGrid.Columns.GridColumn colPuerto;
        private DevExpress.XtraGrid.Columns.GridColumn colTipo;
    }
}
