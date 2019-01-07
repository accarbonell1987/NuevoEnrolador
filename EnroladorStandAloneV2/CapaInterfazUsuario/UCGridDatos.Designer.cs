namespace EnroladorStandAloneV2.CapaInterfazUsuario {
    partial class UCGridDatos {
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode3 = new DevExpress.XtraGrid.GridLevelNode();
            this.DevGridViewContratos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colGuidContrato = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGuidEmpresa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGuidCuenta = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGuidCargo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGuidEmpleado1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInicioVigencia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFinVigencia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCodigoContrato = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConsideraAsistencia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConsideraCasino = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescripcion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEstado = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNombreEmpresa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNombreCuenta = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNombreCargo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DevGridControlEmpleados = new DevExpress.XtraGrid.GridControl();
            this.bsEmpleados = new System.Windows.Forms.BindingSource(this.components);
            this.DevGridViewHuellas = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colGuidHuella = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGuidEmpleado2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTipo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colData = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DevGridViewDispositivos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.DevGridViewEmpleados = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colGuidEmpleado = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRUT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEnrollId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCorreo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNombres = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colApellidos = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTieneContraseña = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTipoIdentificacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContraseña = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumeroTelefono = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DevGroupControlFiltroContratos = new DevExpress.XtraEditors.GroupControl();
            this.DevRadioGroupFiltroContratos = new DevExpress.XtraEditors.RadioGroup();
            this.colGuidDispositivo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGuidInstalacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNombreDispositivo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHost = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPuerto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTipo1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNombreInstalacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNombreCadena = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DevGridViewContratos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGridControlEmpleados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEmpleados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGridViewHuellas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGridViewDispositivos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGridViewEmpleados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGroupControlFiltroContratos)).BeginInit();
            this.DevGroupControlFiltroContratos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DevRadioGroupFiltroContratos.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // DevGridViewContratos
            // 
            this.DevGridViewContratos.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colGuidContrato,
            this.colGuidEmpresa,
            this.colGuidCuenta,
            this.colGuidCargo,
            this.colGuidEmpleado1,
            this.colInicioVigencia,
            this.colFinVigencia,
            this.colCodigoContrato,
            this.colConsideraAsistencia,
            this.colConsideraCasino,
            this.colDescripcion,
            this.colEstado,
            this.colNombreEmpresa,
            this.colNombreCuenta,
            this.colNombreCargo});
            this.DevGridViewContratos.GridControl = this.DevGridControlEmpleados;
            this.DevGridViewContratos.Name = "DevGridViewContratos";
            this.DevGridViewContratos.OptionsBehavior.Editable = false;
            // 
            // colGuidContrato
            // 
            this.colGuidContrato.FieldName = "GuidContrato";
            this.colGuidContrato.Name = "colGuidContrato";
            // 
            // colGuidEmpresa
            // 
            this.colGuidEmpresa.FieldName = "GuidEmpresa";
            this.colGuidEmpresa.Name = "colGuidEmpresa";
            // 
            // colGuidCuenta
            // 
            this.colGuidCuenta.FieldName = "GuidCuenta";
            this.colGuidCuenta.Name = "colGuidCuenta";
            // 
            // colGuidCargo
            // 
            this.colGuidCargo.FieldName = "GuidCargo";
            this.colGuidCargo.Name = "colGuidCargo";
            // 
            // colGuidEmpleado1
            // 
            this.colGuidEmpleado1.FieldName = "GuidEmpleado";
            this.colGuidEmpleado1.Name = "colGuidEmpleado1";
            // 
            // colInicioVigencia
            // 
            this.colInicioVigencia.FieldName = "InicioVigencia";
            this.colInicioVigencia.Name = "colInicioVigencia";
            this.colInicioVigencia.Visible = true;
            this.colInicioVigencia.VisibleIndex = 0;
            // 
            // colFinVigencia
            // 
            this.colFinVigencia.FieldName = "FinVigencia";
            this.colFinVigencia.Name = "colFinVigencia";
            this.colFinVigencia.Visible = true;
            this.colFinVigencia.VisibleIndex = 1;
            // 
            // colCodigoContrato
            // 
            this.colCodigoContrato.FieldName = "CodigoContrato";
            this.colCodigoContrato.Name = "colCodigoContrato";
            this.colCodigoContrato.Visible = true;
            this.colCodigoContrato.VisibleIndex = 2;
            // 
            // colConsideraAsistencia
            // 
            this.colConsideraAsistencia.FieldName = "ConsideraAsistencia";
            this.colConsideraAsistencia.Name = "colConsideraAsistencia";
            this.colConsideraAsistencia.Visible = true;
            this.colConsideraAsistencia.VisibleIndex = 3;
            // 
            // colConsideraCasino
            // 
            this.colConsideraCasino.FieldName = "ConsideraCasino";
            this.colConsideraCasino.Name = "colConsideraCasino";
            this.colConsideraCasino.Visible = true;
            this.colConsideraCasino.VisibleIndex = 4;
            // 
            // colDescripcion
            // 
            this.colDescripcion.FieldName = "Descripcion";
            this.colDescripcion.Name = "colDescripcion";
            this.colDescripcion.Visible = true;
            this.colDescripcion.VisibleIndex = 5;
            // 
            // colEstado
            // 
            this.colEstado.FieldName = "Estado";
            this.colEstado.Name = "colEstado";
            this.colEstado.Visible = true;
            this.colEstado.VisibleIndex = 6;
            // 
            // colNombreEmpresa
            // 
            this.colNombreEmpresa.FieldName = "NombreEmpresa";
            this.colNombreEmpresa.Name = "colNombreEmpresa";
            this.colNombreEmpresa.Visible = true;
            this.colNombreEmpresa.VisibleIndex = 7;
            // 
            // colNombreCuenta
            // 
            this.colNombreCuenta.FieldName = "NombreCuenta";
            this.colNombreCuenta.Name = "colNombreCuenta";
            this.colNombreCuenta.Visible = true;
            this.colNombreCuenta.VisibleIndex = 8;
            // 
            // colNombreCargo
            // 
            this.colNombreCargo.FieldName = "NombreCargo";
            this.colNombreCargo.Name = "colNombreCargo";
            this.colNombreCargo.Visible = true;
            this.colNombreCargo.VisibleIndex = 9;
            // 
            // DevGridControlEmpleados
            // 
            this.DevGridControlEmpleados.DataSource = this.bsEmpleados;
            this.DevGridControlEmpleados.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.LevelTemplate = this.DevGridViewContratos;
            gridLevelNode1.RelationName = "Contratos";
            gridLevelNode2.LevelTemplate = this.DevGridViewHuellas;
            gridLevelNode2.RelationName = "Huellas";
            gridLevelNode3.LevelTemplate = this.DevGridViewDispositivos;
            gridLevelNode3.RelationName = "Dispositivos";
            this.DevGridControlEmpleados.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1,
            gridLevelNode2,
            gridLevelNode3});
            this.DevGridControlEmpleados.Location = new System.Drawing.Point(0, 57);
            this.DevGridControlEmpleados.MainView = this.DevGridViewEmpleados;
            this.DevGridControlEmpleados.Name = "DevGridControlEmpleados";
            this.DevGridControlEmpleados.Size = new System.Drawing.Size(1043, 504);
            this.DevGridControlEmpleados.TabIndex = 1;
            this.DevGridControlEmpleados.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.DevGridViewHuellas,
            this.DevGridViewDispositivos,
            this.DevGridViewEmpleados,
            this.DevGridViewContratos});
            this.DevGridControlEmpleados.DoubleClick += new System.EventHandler(this.DevGridControlEmpleados_DoubleClick);
            // 
            // bsEmpleados
            // 
            this.bsEmpleados.DataSource = typeof(EnroladorAccesoDatos.Dominio.POCOEmpleado);
            // 
            // DevGridViewHuellas
            // 
            this.DevGridViewHuellas.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colGuidHuella,
            this.colGuidEmpleado2,
            this.colTipo,
            this.colData});
            this.DevGridViewHuellas.GridControl = this.DevGridControlEmpleados;
            this.DevGridViewHuellas.Name = "DevGridViewHuellas";
            this.DevGridViewHuellas.OptionsBehavior.Editable = false;
            // 
            // colGuidHuella
            // 
            this.colGuidHuella.FieldName = "GuidHuella";
            this.colGuidHuella.Name = "colGuidHuella";
            // 
            // colGuidEmpleado2
            // 
            this.colGuidEmpleado2.FieldName = "GuidEmpleado";
            this.colGuidEmpleado2.Name = "colGuidEmpleado2";
            // 
            // colTipo
            // 
            this.colTipo.FieldName = "Tipo";
            this.colTipo.Name = "colTipo";
            this.colTipo.Visible = true;
            this.colTipo.VisibleIndex = 0;
            // 
            // colData
            // 
            this.colData.FieldName = "Data";
            this.colData.Name = "colData";
            // 
            // DevGridViewDispositivos
            // 
            this.DevGridViewDispositivos.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colGuidDispositivo,
            this.colGuidInstalacion,
            this.colNombreCadena,
            this.colNombreInstalacion,
            this.colNombreDispositivo,
            this.colHost,
            this.colPuerto,
            this.colTipo1});
            this.DevGridViewDispositivos.GridControl = this.DevGridControlEmpleados;
            this.DevGridViewDispositivos.Name = "DevGridViewDispositivos";
            this.DevGridViewDispositivos.OptionsBehavior.Editable = false;
            this.DevGridViewDispositivos.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never;
            // 
            // DevGridViewEmpleados
            // 
            this.DevGridViewEmpleados.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colGuidEmpleado,
            this.colRUT,
            this.colEnrollId,
            this.colCorreo,
            this.colNombres,
            this.colApellidos,
            this.colTieneContraseña,
            this.colTipoIdentificacion,
            this.colContraseña,
            this.colNumeroTelefono});
            this.DevGridViewEmpleados.GridControl = this.DevGridControlEmpleados;
            this.DevGridViewEmpleados.Name = "DevGridViewEmpleados";
            this.DevGridViewEmpleados.OptionsBehavior.Editable = false;
            // 
            // colGuidEmpleado
            // 
            this.colGuidEmpleado.FieldName = "GuidEmpleado";
            this.colGuidEmpleado.Name = "colGuidEmpleado";
            // 
            // colRUT
            // 
            this.colRUT.FieldName = "RUT";
            this.colRUT.Name = "colRUT";
            this.colRUT.Visible = true;
            this.colRUT.VisibleIndex = 0;
            // 
            // colEnrollId
            // 
            this.colEnrollId.FieldName = "EnrollId";
            this.colEnrollId.Name = "colEnrollId";
            // 
            // colCorreo
            // 
            this.colCorreo.FieldName = "Correo";
            this.colCorreo.Name = "colCorreo";
            this.colCorreo.Visible = true;
            this.colCorreo.VisibleIndex = 4;
            // 
            // colNombres
            // 
            this.colNombres.FieldName = "Nombres";
            this.colNombres.Name = "colNombres";
            this.colNombres.Visible = true;
            this.colNombres.VisibleIndex = 1;
            // 
            // colApellidos
            // 
            this.colApellidos.FieldName = "Apellidos";
            this.colApellidos.Name = "colApellidos";
            this.colApellidos.Visible = true;
            this.colApellidos.VisibleIndex = 2;
            // 
            // colTieneContraseña
            // 
            this.colTieneContraseña.FieldName = "TieneContraseña";
            this.colTieneContraseña.Name = "colTieneContraseña";
            // 
            // colTipoIdentificacion
            // 
            this.colTipoIdentificacion.FieldName = "TipoIdentificacion";
            this.colTipoIdentificacion.Name = "colTipoIdentificacion";
            this.colTipoIdentificacion.Visible = true;
            this.colTipoIdentificacion.VisibleIndex = 3;
            // 
            // colContraseña
            // 
            this.colContraseña.FieldName = "Contraseña";
            this.colContraseña.Name = "colContraseña";
            // 
            // colNumeroTelefono
            // 
            this.colNumeroTelefono.FieldName = "NumeroTelefono";
            this.colNumeroTelefono.Name = "colNumeroTelefono";
            this.colNumeroTelefono.Visible = true;
            this.colNumeroTelefono.VisibleIndex = 5;
            // 
            // DevGroupControlFiltroContratos
            // 
            this.DevGroupControlFiltroContratos.Controls.Add(this.DevRadioGroupFiltroContratos);
            this.DevGroupControlFiltroContratos.Dock = System.Windows.Forms.DockStyle.Top;
            this.DevGroupControlFiltroContratos.Location = new System.Drawing.Point(0, 0);
            this.DevGroupControlFiltroContratos.Name = "DevGroupControlFiltroContratos";
            this.DevGroupControlFiltroContratos.Size = new System.Drawing.Size(1043, 57);
            this.DevGroupControlFiltroContratos.TabIndex = 0;
            this.DevGroupControlFiltroContratos.Text = "Filtrar Contratos:";
            // 
            // DevRadioGroupFiltroContratos
            // 
            this.DevRadioGroupFiltroContratos.EditValue = ((short)(0));
            this.DevRadioGroupFiltroContratos.Location = new System.Drawing.Point(8, 25);
            this.DevRadioGroupFiltroContratos.Name = "DevRadioGroupFiltroContratos";
            this.DevRadioGroupFiltroContratos.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.DevRadioGroupFiltroContratos.Properties.Appearance.Options.UseBackColor = true;
            this.DevRadioGroupFiltroContratos.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.DevRadioGroupFiltroContratos.Properties.Columns = 3;
            this.DevRadioGroupFiltroContratos.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(0)), "Todos", true, ((short)(0))),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(1)), "Activos", true, ((short)(1))),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(2)), "Vencidos", true, ((short)(2)))});
            this.DevRadioGroupFiltroContratos.Size = new System.Drawing.Size(282, 24);
            this.DevRadioGroupFiltroContratos.TabIndex = 0;
            this.DevRadioGroupFiltroContratos.EditValueChanged += new System.EventHandler(this.DevRadioGroupFiltroContratos_EditValueChanged);
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
            // colTipo1
            // 
            this.colTipo1.FieldName = "Tipo";
            this.colTipo1.Name = "colTipo1";
            this.colTipo1.Visible = true;
            this.colTipo1.VisibleIndex = 5;
            // 
            // colNombreInstalacion
            // 
            this.colNombreInstalacion.FieldName = "NombreInstalacion";
            this.colNombreInstalacion.Name = "colNombreInstalacion";
            this.colNombreInstalacion.Visible = true;
            this.colNombreInstalacion.VisibleIndex = 1;
            // 
            // colNombreCadena
            // 
            this.colNombreCadena.FieldName = "NombreCadena";
            this.colNombreCadena.Name = "colNombreCadena";
            this.colNombreCadena.Visible = true;
            this.colNombreCadena.VisibleIndex = 0;
            // 
            // UCGridDatos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DevGridControlEmpleados);
            this.Controls.Add(this.DevGroupControlFiltroContratos);
            this.Name = "UCGridDatos";
            this.Size = new System.Drawing.Size(1043, 561);
            ((System.ComponentModel.ISupportInitialize)(this.DevGridViewContratos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGridControlEmpleados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEmpleados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGridViewHuellas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGridViewDispositivos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGridViewEmpleados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGroupControlFiltroContratos)).EndInit();
            this.DevGroupControlFiltroContratos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DevRadioGroupFiltroContratos.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl DevGroupControlFiltroContratos;
        private DevExpress.XtraEditors.RadioGroup DevRadioGroupFiltroContratos;
        private DevExpress.XtraGrid.GridControl DevGridControlEmpleados;
        private DevExpress.XtraGrid.Views.Grid.GridView DevGridViewEmpleados;
        private System.Windows.Forms.BindingSource bsEmpleados;
        private DevExpress.XtraGrid.Views.Grid.GridView DevGridViewContratos;
        private DevExpress.XtraGrid.Views.Grid.GridView DevGridViewHuellas;
        private DevExpress.XtraGrid.Columns.GridColumn colGuidHuella;
        private DevExpress.XtraGrid.Columns.GridColumn colGuidEmpleado2;
        private DevExpress.XtraGrid.Columns.GridColumn colTipo;
        private DevExpress.XtraGrid.Columns.GridColumn colData;
        private DevExpress.XtraGrid.Views.Grid.GridView DevGridViewDispositivos;
        private DevExpress.XtraGrid.Columns.GridColumn colGuidEmpleado;
        private DevExpress.XtraGrid.Columns.GridColumn colRUT;
        private DevExpress.XtraGrid.Columns.GridColumn colEnrollId;
        private DevExpress.XtraGrid.Columns.GridColumn colCorreo;
        private DevExpress.XtraGrid.Columns.GridColumn colNombres;
        private DevExpress.XtraGrid.Columns.GridColumn colApellidos;
        private DevExpress.XtraGrid.Columns.GridColumn colTieneContraseña;
        private DevExpress.XtraGrid.Columns.GridColumn colTipoIdentificacion;
        private DevExpress.XtraGrid.Columns.GridColumn colContraseña;
        private DevExpress.XtraGrid.Columns.GridColumn colNumeroTelefono;
        private DevExpress.XtraGrid.Columns.GridColumn colGuidContrato;
        private DevExpress.XtraGrid.Columns.GridColumn colGuidEmpresa;
        private DevExpress.XtraGrid.Columns.GridColumn colGuidCuenta;
        private DevExpress.XtraGrid.Columns.GridColumn colGuidCargo;
        private DevExpress.XtraGrid.Columns.GridColumn colGuidEmpleado1;
        private DevExpress.XtraGrid.Columns.GridColumn colInicioVigencia;
        private DevExpress.XtraGrid.Columns.GridColumn colFinVigencia;
        private DevExpress.XtraGrid.Columns.GridColumn colCodigoContrato;
        private DevExpress.XtraGrid.Columns.GridColumn colConsideraAsistencia;
        private DevExpress.XtraGrid.Columns.GridColumn colConsideraCasino;
        private DevExpress.XtraGrid.Columns.GridColumn colDescripcion;
        private DevExpress.XtraGrid.Columns.GridColumn colEstado;
        private DevExpress.XtraGrid.Columns.GridColumn colNombreEmpresa;
        private DevExpress.XtraGrid.Columns.GridColumn colNombreCuenta;
        private DevExpress.XtraGrid.Columns.GridColumn colNombreCargo;
        private DevExpress.XtraGrid.Columns.GridColumn colGuidDispositivo;
        private DevExpress.XtraGrid.Columns.GridColumn colGuidInstalacion;
        private DevExpress.XtraGrid.Columns.GridColumn colNombreCadena;
        private DevExpress.XtraGrid.Columns.GridColumn colNombreInstalacion;
        private DevExpress.XtraGrid.Columns.GridColumn colNombreDispositivo;
        private DevExpress.XtraGrid.Columns.GridColumn colHost;
        private DevExpress.XtraGrid.Columns.GridColumn colPuerto;
        private DevExpress.XtraGrid.Columns.GridColumn colTipo1;
    }
}
