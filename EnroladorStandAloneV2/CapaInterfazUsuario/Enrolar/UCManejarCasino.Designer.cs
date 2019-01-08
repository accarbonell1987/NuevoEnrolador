namespace EnroladorStandAloneV2.CapaInterfazUsuario.Enrolar {
    partial class UCManejarCasino {
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
            this.bsEmpleadoTurnoServicioCasino = new System.Windows.Forms.BindingSource(this.components);
            this.DevGridViewAsistencias = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colGuidEmpleado = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGuidTurnoServicio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNombreServicio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNombreTurno = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNombreCasino = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DevGroupControlAsistencia = new DevExpress.XtraEditors.GroupControl();
            this.DevSimpleButtonAdicionar = new DevExpress.XtraEditors.SimpleButton();
            this.DevLayoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.DevLookUpEditTurno = new DevExpress.XtraEditors.LookUpEdit();
            this.bsTurnos = new System.Windows.Forms.BindingSource(this.components);
            this.DevLookUpEditServicio = new DevExpress.XtraEditors.LookUpEdit();
            this.bsServicios = new System.Windows.Forms.BindingSource(this.components);
            this.DevLookUpEditCasino = new DevExpress.XtraEditors.LookUpEdit();
            this.bsInstalaciones = new System.Windows.Forms.BindingSource(this.components);
            this.DevLayoutControlGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.DevLayoutControlItemCasino = new DevExpress.XtraLayout.LayoutControlItem();
            this.DevLayoutControlItemServicio = new DevExpress.XtraLayout.LayoutControlItem();
            this.DevLayoutControlItemTurno = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.DevGridControlAsistencias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEmpleadoTurnoServicioCasino)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGridViewAsistencias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGroupControlAsistencia)).BeginInit();
            this.DevGroupControlAsistencia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControl)).BeginInit();
            this.DevLayoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DevLookUpEditTurno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsTurnos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLookUpEditServicio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsServicios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLookUpEditCasino.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsInstalaciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlItemCasino)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlItemServicio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlItemTurno)).BeginInit();
            this.SuspendLayout();
            // 
            // DevGridControlAsistencias
            // 
            this.DevGridControlAsistencias.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DevGridControlAsistencias.DataSource = this.bsEmpleadoTurnoServicioCasino;
            this.DevGridControlAsistencias.Location = new System.Drawing.Point(3, 3);
            this.DevGridControlAsistencias.MainView = this.DevGridViewAsistencias;
            this.DevGridControlAsistencias.Name = "DevGridControlAsistencias";
            this.DevGridControlAsistencias.Size = new System.Drawing.Size(441, 234);
            this.DevGridControlAsistencias.TabIndex = 0;
            this.DevGridControlAsistencias.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.DevGridViewAsistencias});
            // 
            // bsEmpleadoTurnoServicioCasino
            // 
            this.bsEmpleadoTurnoServicioCasino.DataSource = typeof(EnroladorAccesoDatos.Dominio.POCOEmpleadoTurnoServicioCasino);
            // 
            // DevGridViewAsistencias
            // 
            this.DevGridViewAsistencias.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colGuidEmpleado,
            this.colGuidTurnoServicio,
            this.colNombreServicio,
            this.colNombreTurno,
            this.colNombreCasino});
            this.DevGridViewAsistencias.GridControl = this.DevGridControlAsistencias;
            this.DevGridViewAsistencias.Name = "DevGridViewAsistencias";
            this.DevGridViewAsistencias.OptionsBehavior.Editable = false;
            // 
            // colGuidEmpleado
            // 
            this.colGuidEmpleado.FieldName = "GuidEmpleado";
            this.colGuidEmpleado.Name = "colGuidEmpleado";
            // 
            // colGuidTurnoServicio
            // 
            this.colGuidTurnoServicio.FieldName = "GuidTurnoServicio";
            this.colGuidTurnoServicio.Name = "colGuidTurnoServicio";
            // 
            // colNombreServicio
            // 
            this.colNombreServicio.FieldName = "NombreServicio";
            this.colNombreServicio.Name = "colNombreServicio";
            this.colNombreServicio.Visible = true;
            this.colNombreServicio.VisibleIndex = 0;
            // 
            // colNombreTurno
            // 
            this.colNombreTurno.FieldName = "NombreTurno";
            this.colNombreTurno.Name = "colNombreTurno";
            this.colNombreTurno.Visible = true;
            this.colNombreTurno.VisibleIndex = 1;
            // 
            // colNombreCasino
            // 
            this.colNombreCasino.FieldName = "NombreCasino";
            this.colNombreCasino.Name = "colNombreCasino";
            this.colNombreCasino.Visible = true;
            this.colNombreCasino.VisibleIndex = 2;
            // 
            // DevGroupControlAsistencia
            // 
            this.DevGroupControlAsistencia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DevGroupControlAsistencia.Controls.Add(this.DevSimpleButtonAdicionar);
            this.DevGroupControlAsistencia.Controls.Add(this.DevLayoutControl);
            this.DevGroupControlAsistencia.Location = new System.Drawing.Point(3, 244);
            this.DevGroupControlAsistencia.Name = "DevGroupControlAsistencia";
            this.DevGroupControlAsistencia.Size = new System.Drawing.Size(441, 145);
            this.DevGroupControlAsistencia.TabIndex = 1;
            this.DevGroupControlAsistencia.Text = "Adicionar Asistencia";
            // 
            // DevSimpleButtonAdicionar
            // 
            this.DevSimpleButtonAdicionar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.DevSimpleButtonAdicionar.Enabled = false;
            this.DevSimpleButtonAdicionar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.DevSimpleButtonAdicionar.ImageUri.Uri = "Add;Size16x16;Office2013";
            this.DevSimpleButtonAdicionar.Location = new System.Drawing.Point(382, 111);
            this.DevSimpleButtonAdicionar.Name = "DevSimpleButtonAdicionar";
            this.DevSimpleButtonAdicionar.Size = new System.Drawing.Size(43, 23);
            this.DevSimpleButtonAdicionar.TabIndex = 1;
            // 
            // DevLayoutControl
            // 
            this.DevLayoutControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DevLayoutControl.Controls.Add(this.DevLookUpEditTurno);
            this.DevLayoutControl.Controls.Add(this.DevLookUpEditServicio);
            this.DevLayoutControl.Controls.Add(this.DevLookUpEditCasino);
            this.DevLayoutControl.Location = new System.Drawing.Point(5, 20);
            this.DevLayoutControl.Name = "DevLayoutControl";
            this.DevLayoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(600, 459, 450, 400);
            this.DevLayoutControl.Root = this.DevLayoutControlGroup;
            this.DevLayoutControl.Size = new System.Drawing.Size(431, 94);
            this.DevLayoutControl.TabIndex = 0;
            this.DevLayoutControl.Text = "layoutControl1";
            // 
            // DevLookUpEditTurno
            // 
            this.DevLookUpEditTurno.Enabled = false;
            this.DevLookUpEditTurno.Location = new System.Drawing.Point(56, 60);
            this.DevLookUpEditTurno.Name = "DevLookUpEditTurno";
            this.DevLookUpEditTurno.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DevLookUpEditTurno.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GuidTurnoServicio", "Guid Turno Servicio", 115, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GuidServicio", "Guid Servicio", 71, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NombreTurnoServicio", "Nombre Turno Servicio", 118, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("HoraInicio", "Hora Inicio", 61, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("HoraFin", "Hora Fin", 50, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Vigente", "Vigente", 46, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.DevLookUpEditTurno.Properties.DataSource = this.bsTurnos;
            this.DevLookUpEditTurno.Properties.DisplayMember = "NombreTurnoServicio";
            this.DevLookUpEditTurno.Properties.ValueMember = "GuidTurnoServicio";
            this.DevLookUpEditTurno.Size = new System.Drawing.Size(363, 20);
            this.DevLookUpEditTurno.StyleController = this.DevLayoutControl;
            this.DevLookUpEditTurno.TabIndex = 4;
            this.DevLookUpEditTurno.EditValueChanged += new System.EventHandler(this.DevLookUpEditTurno_EditValueChanged);
            // 
            // bsTurnos
            // 
            this.bsTurnos.DataSource = typeof(EnroladorAccesoDatos.Dominio.POCOTurnoServicio);
            // 
            // DevLookUpEditServicio
            // 
            this.DevLookUpEditServicio.Enabled = false;
            this.DevLookUpEditServicio.Location = new System.Drawing.Point(56, 36);
            this.DevLookUpEditServicio.Name = "DevLookUpEditServicio";
            this.DevLookUpEditServicio.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DevLookUpEditServicio.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GuidServicioCasino", "Guid Servicio Casino", 119, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GuidCasino", "Guid Casino", 66, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NombreServicioCasino", "Nombre Servicio Casino", 122, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Vigente", "Vigente", 46, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.DevLookUpEditServicio.Properties.DataSource = this.bsServicios;
            this.DevLookUpEditServicio.Properties.DisplayMember = "NombreServicioCasino";
            this.DevLookUpEditServicio.Properties.ValueMember = "GuidServicioCasino";
            this.DevLookUpEditServicio.Size = new System.Drawing.Size(363, 20);
            this.DevLookUpEditServicio.StyleController = this.DevLayoutControl;
            this.DevLookUpEditServicio.TabIndex = 2;
            this.DevLookUpEditServicio.EditValueChanged += new System.EventHandler(this.DevLookUpEditServicio_EditValueChanged);
            // 
            // bsServicios
            // 
            this.bsServicios.DataSource = typeof(EnroladorAccesoDatos.Dominio.POCOServicioCasino);
            // 
            // DevLookUpEditCasino
            // 
            this.DevLookUpEditCasino.Location = new System.Drawing.Point(56, 12);
            this.DevLookUpEditCasino.Name = "DevLookUpEditCasino";
            this.DevLookUpEditCasino.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DevLookUpEditCasino.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GuidInstalacion", "Guid Instalacion", 99, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GuidCadena", "Guid Cadena", 71, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NombreInstalacion", "Nombre Instalacion", 102, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NombreCadena", "Nombre Cadena", 87, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.DevLookUpEditCasino.Properties.DataSource = this.bsInstalaciones;
            this.DevLookUpEditCasino.Properties.DisplayMember = "NombreInstalacion";
            this.DevLookUpEditCasino.Properties.ValueMember = "GuidInstalacion";
            this.DevLookUpEditCasino.Size = new System.Drawing.Size(363, 20);
            this.DevLookUpEditCasino.StyleController = this.DevLayoutControl;
            this.DevLookUpEditCasino.TabIndex = 0;
            this.DevLookUpEditCasino.EditValueChanged += new System.EventHandler(this.DevLookUpEditInstalacion_EditValueChanged);
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
            this.DevLayoutControlItemCasino,
            this.DevLayoutControlItemServicio,
            this.DevLayoutControlItemTurno});
            this.DevLayoutControlGroup.Location = new System.Drawing.Point(0, 0);
            this.DevLayoutControlGroup.Name = "Root";
            this.DevLayoutControlGroup.Size = new System.Drawing.Size(431, 94);
            this.DevLayoutControlGroup.TextVisible = false;
            // 
            // DevLayoutControlItemCasino
            // 
            this.DevLayoutControlItemCasino.Control = this.DevLookUpEditCasino;
            this.DevLayoutControlItemCasino.Location = new System.Drawing.Point(0, 0);
            this.DevLayoutControlItemCasino.Name = "DevLayoutControlItemCasino";
            this.DevLayoutControlItemCasino.Size = new System.Drawing.Size(411, 24);
            this.DevLayoutControlItemCasino.Text = "Casino:";
            this.DevLayoutControlItemCasino.TextSize = new System.Drawing.Size(41, 13);
            // 
            // DevLayoutControlItemServicio
            // 
            this.DevLayoutControlItemServicio.Control = this.DevLookUpEditServicio;
            this.DevLayoutControlItemServicio.Location = new System.Drawing.Point(0, 24);
            this.DevLayoutControlItemServicio.Name = "DevLayoutControlItemServicio";
            this.DevLayoutControlItemServicio.Size = new System.Drawing.Size(411, 24);
            this.DevLayoutControlItemServicio.Text = "Servicio:";
            this.DevLayoutControlItemServicio.TextSize = new System.Drawing.Size(41, 13);
            // 
            // DevLayoutControlItemTurno
            // 
            this.DevLayoutControlItemTurno.Control = this.DevLookUpEditTurno;
            this.DevLayoutControlItemTurno.Location = new System.Drawing.Point(0, 48);
            this.DevLayoutControlItemTurno.Name = "DevLayoutControlItemTurno";
            this.DevLayoutControlItemTurno.Size = new System.Drawing.Size(411, 26);
            this.DevLayoutControlItemTurno.Text = "Turnos:";
            this.DevLayoutControlItemTurno.TextSize = new System.Drawing.Size(41, 13);
            // 
            // UCManejarCasino
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DevGroupControlAsistencia);
            this.Controls.Add(this.DevGridControlAsistencias);
            this.Name = "UCManejarCasino";
            this.Size = new System.Drawing.Size(447, 395);
            this.Load += new System.EventHandler(this.UCManejarCasino_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DevGridControlAsistencias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEmpleadoTurnoServicioCasino)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGridViewAsistencias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGroupControlAsistencia)).EndInit();
            this.DevGroupControlAsistencia.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControl)).EndInit();
            this.DevLayoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DevLookUpEditTurno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsTurnos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLookUpEditServicio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsServicios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLookUpEditCasino.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsInstalaciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlItemCasino)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlItemServicio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlItemTurno)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl DevGridControlAsistencias;
        private DevExpress.XtraGrid.Views.Grid.GridView DevGridViewAsistencias;
        private DevExpress.XtraEditors.GroupControl DevGroupControlAsistencia;
        private DevExpress.XtraLayout.LayoutControl DevLayoutControl;
        private DevExpress.XtraEditors.LookUpEdit DevLookUpEditCasino;
        private DevExpress.XtraLayout.LayoutControlGroup DevLayoutControlGroup;
        private DevExpress.XtraLayout.LayoutControlItem DevLayoutControlItemCasino;
        private DevExpress.XtraEditors.LookUpEdit DevLookUpEditServicio;
        private DevExpress.XtraLayout.LayoutControlItem DevLayoutControlItemServicio;
        private DevExpress.XtraEditors.SimpleButton DevSimpleButtonAdicionar;
        private System.Windows.Forms.BindingSource bsInstalaciones;
        private System.Windows.Forms.BindingSource bsEmpleadoTurnoServicioCasino;
        private DevExpress.XtraEditors.LookUpEdit DevLookUpEditTurno;
        private DevExpress.XtraLayout.LayoutControlItem DevLayoutControlItemTurno;
        private DevExpress.XtraGrid.Columns.GridColumn colGuidEmpleado;
        private DevExpress.XtraGrid.Columns.GridColumn colGuidTurnoServicio;
        private DevExpress.XtraGrid.Columns.GridColumn colNombreServicio;
        private DevExpress.XtraGrid.Columns.GridColumn colNombreTurno;
        private DevExpress.XtraGrid.Columns.GridColumn colNombreCasino;
        private System.Windows.Forms.BindingSource bsServicios;
        private System.Windows.Forms.BindingSource bsTurnos;
    }
}
