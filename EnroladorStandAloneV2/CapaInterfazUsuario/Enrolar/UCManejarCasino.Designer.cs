﻿namespace EnroladorStandAloneV2.CapaInterfazUsuario.Enrolar {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCManejarCasino));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.DevGridControlTurnos = new DevExpress.XtraGrid.GridControl();
            this.bsEmpleadoTurnoServicioCasino = new System.Windows.Forms.BindingSource(this.components);
            this.DevGridViewTurnos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colGuidEmpleado = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGuidTurnoServicio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNombreServicio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNombreTurno = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNombreCasino = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEliminar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DevRepositoryItemButtonEditEliminar = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.DevGroupControlTurnos = new DevExpress.XtraEditors.GroupControl();
            this.DevSimpleButtonNuevo = new DevExpress.XtraEditors.SimpleButton();
            this.DevSimpleButtonModificar = new DevExpress.XtraEditors.SimpleButton();
            this.DevSimpleButtonDescartar = new DevExpress.XtraEditors.SimpleButton();
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
            this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DevGridControlTurnos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEmpleadoTurnoServicioCasino)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGridViewTurnos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevRepositoryItemButtonEditEliminar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGroupControlTurnos)).BeginInit();
            this.DevGroupControlTurnos.SuspendLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // DevGridControlTurnos
            // 
            this.DevGridControlTurnos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DevGridControlTurnos.DataSource = this.bsEmpleadoTurnoServicioCasino;
            this.DevGridControlTurnos.Location = new System.Drawing.Point(3, 3);
            this.DevGridControlTurnos.MainView = this.DevGridViewTurnos;
            this.DevGridControlTurnos.Name = "DevGridControlTurnos";
            this.DevGridControlTurnos.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.DevRepositoryItemButtonEditEliminar});
            this.DevGridControlTurnos.Size = new System.Drawing.Size(441, 154);
            this.DevGridControlTurnos.TabIndex = 0;
            this.DevGridControlTurnos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.DevGridViewTurnos});
            // 
            // bsEmpleadoTurnoServicioCasino
            // 
            this.bsEmpleadoTurnoServicioCasino.DataSource = typeof(EnroladorAccesoDatos.Dominio.POCOEmpleadoTurnoServicioCasino);
            // 
            // DevGridViewTurnos
            // 
            this.DevGridViewTurnos.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colGuidEmpleado,
            this.colGuidTurnoServicio,
            this.colNombreServicio,
            this.colNombreTurno,
            this.colNombreCasino,
            this.colEliminar});
            this.DevGridViewTurnos.GridControl = this.DevGridControlTurnos;
            this.DevGridViewTurnos.Name = "DevGridViewTurnos";
            this.DevGridViewTurnos.OptionsView.ShowGroupPanel = false;
            // 
            // colGuidEmpleado
            // 
            this.colGuidEmpleado.FieldName = "GuidEmpleado";
            this.colGuidEmpleado.Name = "colGuidEmpleado";
            this.colGuidEmpleado.OptionsColumn.AllowEdit = false;
            // 
            // colGuidTurnoServicio
            // 
            this.colGuidTurnoServicio.FieldName = "GuidTurnoServicio";
            this.colGuidTurnoServicio.Name = "colGuidTurnoServicio";
            this.colGuidTurnoServicio.OptionsColumn.AllowEdit = false;
            // 
            // colNombreServicio
            // 
            this.colNombreServicio.FieldName = "NombreServicio";
            this.colNombreServicio.Name = "colNombreServicio";
            this.colNombreServicio.OptionsColumn.AllowEdit = false;
            this.colNombreServicio.Visible = true;
            this.colNombreServicio.VisibleIndex = 0;
            // 
            // colNombreTurno
            // 
            this.colNombreTurno.FieldName = "NombreTurno";
            this.colNombreTurno.Name = "colNombreTurno";
            this.colNombreTurno.OptionsColumn.AllowEdit = false;
            this.colNombreTurno.Visible = true;
            this.colNombreTurno.VisibleIndex = 1;
            // 
            // colNombreCasino
            // 
            this.colNombreCasino.FieldName = "NombreCasino";
            this.colNombreCasino.Name = "colNombreCasino";
            this.colNombreCasino.OptionsColumn.AllowEdit = false;
            this.colNombreCasino.Visible = true;
            this.colNombreCasino.VisibleIndex = 2;
            // 
            // colEliminar
            // 
            this.colEliminar.Caption = "Eliminar";
            this.colEliminar.ColumnEdit = this.DevRepositoryItemButtonEditEliminar;
            this.colEliminar.FieldName = "NombreCasino";
            this.colEliminar.Name = "colEliminar";
            this.colEliminar.Visible = true;
            this.colEliminar.VisibleIndex = 3;
            // 
            // DevRepositoryItemButtonEditEliminar
            // 
            this.DevRepositoryItemButtonEditEliminar.AutoHeight = false;
            this.DevRepositoryItemButtonEditEliminar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.DevRepositoryItemButtonEditEliminar.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("DevRepositoryItemButtonEditEliminar.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.DevRepositoryItemButtonEditEliminar.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.DevRepositoryItemButtonEditEliminar.Name = "DevRepositoryItemButtonEditEliminar";
            this.DevRepositoryItemButtonEditEliminar.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.DevRepositoryItemButtonEditEliminar.Click += new System.EventHandler(this.DevRepositoryItemButtonEditEliminar_Click);
            // 
            // DevGroupControlTurnos
            // 
            this.DevGroupControlTurnos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DevGroupControlTurnos.Controls.Add(this.DevSimpleButtonNuevo);
            this.DevGroupControlTurnos.Controls.Add(this.DevSimpleButtonModificar);
            this.DevGroupControlTurnos.Controls.Add(this.DevSimpleButtonDescartar);
            this.DevGroupControlTurnos.Controls.Add(this.DevLayoutControl);
            this.DevGroupControlTurnos.Location = new System.Drawing.Point(3, 163);
            this.DevGroupControlTurnos.Name = "DevGroupControlTurnos";
            this.DevGroupControlTurnos.Size = new System.Drawing.Size(441, 145);
            this.DevGroupControlTurnos.TabIndex = 1;
            this.DevGroupControlTurnos.Text = "Adicionar Casino";
            // 
            // DevSimpleButtonNuevo
            // 
            this.DevSimpleButtonNuevo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DevSimpleButtonNuevo.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.DevSimpleButtonNuevo.Enabled = false;
            this.DevSimpleButtonNuevo.Image = ((System.Drawing.Image)(resources.GetObject("DevSimpleButtonNuevo.Image")));
            this.DevSimpleButtonNuevo.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.DevSimpleButtonNuevo.Location = new System.Drawing.Point(15, 112);
            this.DevSimpleButtonNuevo.Name = "DevSimpleButtonNuevo";
            this.DevSimpleButtonNuevo.Size = new System.Drawing.Size(43, 23);
            this.DevSimpleButtonNuevo.TabIndex = 10;
            this.DevSimpleButtonNuevo.ToolTip = "Adiciona una nueva relacion de turno con empleado...";
            this.DevSimpleButtonNuevo.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.DevSimpleButtonNuevo.ToolTipTitle = "Nuevo";
            this.DevSimpleButtonNuevo.Click += new System.EventHandler(this.DevSimpleButtonNuevo_Click);
            // 
            // DevSimpleButtonModificar
            // 
            this.DevSimpleButtonModificar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DevSimpleButtonModificar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.DevSimpleButtonModificar.Enabled = false;
            this.DevSimpleButtonModificar.Image = ((System.Drawing.Image)(resources.GetObject("DevSimpleButtonModificar.Image")));
            this.DevSimpleButtonModificar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.DevSimpleButtonModificar.Location = new System.Drawing.Point(64, 112);
            this.DevSimpleButtonModificar.Name = "DevSimpleButtonModificar";
            this.DevSimpleButtonModificar.Size = new System.Drawing.Size(43, 23);
            this.DevSimpleButtonModificar.TabIndex = 9;
            this.DevSimpleButtonModificar.ToolTip = "Modifica el contrato actual seleccionado...";
            this.DevSimpleButtonModificar.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.DevSimpleButtonModificar.ToolTipTitle = "Modificar Contrato";
            this.DevSimpleButtonModificar.Visible = false;
            // 
            // DevSimpleButtonDescartar
            // 
            this.DevSimpleButtonDescartar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DevSimpleButtonDescartar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.DevSimpleButtonDescartar.Image = ((System.Drawing.Image)(resources.GetObject("DevSimpleButtonDescartar.Image")));
            this.DevSimpleButtonDescartar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.DevSimpleButtonDescartar.Location = new System.Drawing.Point(113, 112);
            this.DevSimpleButtonDescartar.Name = "DevSimpleButtonDescartar";
            this.DevSimpleButtonDescartar.Size = new System.Drawing.Size(43, 23);
            this.DevSimpleButtonDescartar.TabIndex = 8;
            this.DevSimpleButtonDescartar.ToolTip = "Descarta todo lo antes hecho...";
            this.DevSimpleButtonDescartar.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.DevSimpleButtonDescartar.ToolTipTitle = "Descartar";
            this.DevSimpleButtonDescartar.Visible = false;
            this.DevSimpleButtonDescartar.Click += new System.EventHandler(this.DevSimpleButtonDescartar_Click);
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
            // dxErrorProvider
            // 
            this.dxErrorProvider.ContainerControl = this;
            // 
            // UCManejarCasino
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DevGroupControlTurnos);
            this.Controls.Add(this.DevGridControlTurnos);
            this.DoubleBuffered = true;
            this.Name = "UCManejarCasino";
            this.Size = new System.Drawing.Size(447, 312);
            this.Load += new System.EventHandler(this.UCManejarCasino_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DevGridControlTurnos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEmpleadoTurnoServicioCasino)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGridViewTurnos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevRepositoryItemButtonEditEliminar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGroupControlTurnos)).EndInit();
            this.DevGroupControlTurnos.ResumeLayout(false);
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
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl DevGridControlTurnos;
        private DevExpress.XtraGrid.Views.Grid.GridView DevGridViewTurnos;
        private DevExpress.XtraEditors.GroupControl DevGroupControlTurnos;
        private DevExpress.XtraLayout.LayoutControl DevLayoutControl;
        private DevExpress.XtraEditors.LookUpEdit DevLookUpEditCasino;
        private DevExpress.XtraLayout.LayoutControlGroup DevLayoutControlGroup;
        private DevExpress.XtraLayout.LayoutControlItem DevLayoutControlItemCasino;
        private DevExpress.XtraEditors.LookUpEdit DevLookUpEditServicio;
        private DevExpress.XtraLayout.LayoutControlItem DevLayoutControlItemServicio;
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
        private DevExpress.XtraGrid.Columns.GridColumn colEliminar;
        private DevExpress.XtraEditors.SimpleButton DevSimpleButtonNuevo;
        private DevExpress.XtraEditors.SimpleButton DevSimpleButtonModificar;
        private DevExpress.XtraEditors.SimpleButton DevSimpleButtonDescartar;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit DevRepositoryItemButtonEditEliminar;
    }
}
