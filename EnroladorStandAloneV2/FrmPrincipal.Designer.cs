namespace EnroladorStandAloneV2 {
    partial class FrmPrincipal {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
            this.DevRibbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.DevBarButtonSalir = new DevExpress.XtraBars.BarButtonItem();
            this.DevBarButtonSincronizar = new DevExpress.XtraBars.BarButtonItem();
            this.DevBarButtonEnrolar = new DevExpress.XtraBars.BarButtonItem();
            this.DevBarButtonItemGuardar = new DevExpress.XtraBars.BarButtonItem();
            this.DevBarButtonItemDescartar = new DevExpress.XtraBars.BarButtonItem();
            this.DevRibbonPageMenu = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.DevRibbonPageGroupArchivos = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.DevRibbonPageGroupOpciones = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.DevRibbonPageGroupEnrolar = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.DevGroupControlNotificacionesAcciones = new DevExpress.XtraEditors.GroupControl();
            this.DevPanelControlPrincipal = new DevExpress.XtraEditors.PanelControl();
            this.DevNavigationPanePrincipal = new DevExpress.XtraBars.Navigation.NavigationPane();
            this.backgroundWorkerChequeoNotificaciones = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.DevRibbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGroupControlNotificacionesAcciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevPanelControlPrincipal)).BeginInit();
            this.DevPanelControlPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DevNavigationPanePrincipal)).BeginInit();
            this.SuspendLayout();
            // 
            // DevRibbonControl
            // 
            this.DevRibbonControl.ExpandCollapseItem.Id = 0;
            this.DevRibbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.DevRibbonControl.ExpandCollapseItem,
            this.DevBarButtonSalir,
            this.DevBarButtonSincronizar,
            this.DevBarButtonEnrolar,
            this.DevBarButtonItemGuardar,
            this.DevBarButtonItemDescartar});
            this.DevRibbonControl.Location = new System.Drawing.Point(0, 0);
            this.DevRibbonControl.MaxItemId = 6;
            this.DevRibbonControl.Name = "DevRibbonControl";
            this.DevRibbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.DevRibbonPageMenu});
            this.DevRibbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2010;
            this.DevRibbonControl.Size = new System.Drawing.Size(1333, 143);
            this.DevRibbonControl.MinimizedChanged += new System.EventHandler(this.DevRibbonControl_MinimizedChanged);
            // 
            // DevBarButtonSalir
            // 
            this.DevBarButtonSalir.Caption = "Salir";
            this.DevBarButtonSalir.Id = 1;
            this.DevBarButtonSalir.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("DevBarButtonSalir.ImageOptions.Image")));
            this.DevBarButtonSalir.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("DevBarButtonSalir.ImageOptions.LargeImage")));
            this.DevBarButtonSalir.Name = "DevBarButtonSalir";
            this.DevBarButtonSalir.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItemSalir_ItemClick);
            // 
            // DevBarButtonSincronizar
            // 
            this.DevBarButtonSincronizar.Caption = "Sincronizar";
            this.DevBarButtonSincronizar.Id = 2;
            this.DevBarButtonSincronizar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("DevBarButtonSincronizar.ImageOptions.Image")));
            this.DevBarButtonSincronizar.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("DevBarButtonSincronizar.ImageOptions.LargeImage")));
            this.DevBarButtonSincronizar.Name = "DevBarButtonSincronizar";
            this.DevBarButtonSincronizar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.DevBarButtonSincronizar_ItemClick);
            // 
            // DevBarButtonEnrolar
            // 
            this.DevBarButtonEnrolar.Caption = "Enrolar";
            this.DevBarButtonEnrolar.Id = 3;
            this.DevBarButtonEnrolar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("DevBarButtonEnrolar.ImageOptions.Image")));
            this.DevBarButtonEnrolar.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("DevBarButtonEnrolar.ImageOptions.LargeImage")));
            this.DevBarButtonEnrolar.Name = "DevBarButtonEnrolar";
            this.DevBarButtonEnrolar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.DevBarButtonEnrolar_ItemClick);
            // 
            // DevBarButtonItemGuardar
            // 
            this.DevBarButtonItemGuardar.Caption = "Guardar";
            this.DevBarButtonItemGuardar.Id = 4;
            this.DevBarButtonItemGuardar.ImageOptions.Image = global::EnroladorStandAloneV2.Properties.Resources.save_32x32;
            this.DevBarButtonItemGuardar.ImageOptions.LargeImage = global::EnroladorStandAloneV2.Properties.Resources.additem_32x32;
            this.DevBarButtonItemGuardar.Name = "DevBarButtonItemGuardar";
            // 
            // DevBarButtonItemDescartar
            // 
            this.DevBarButtonItemDescartar.Caption = "Descartar";
            this.DevBarButtonItemDescartar.Id = 5;
            this.DevBarButtonItemDescartar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("DevBarButtonItemDescartar.ImageOptions.Image")));
            this.DevBarButtonItemDescartar.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("DevBarButtonItemDescartar.ImageOptions.LargeImage")));
            this.DevBarButtonItemDescartar.Name = "DevBarButtonItemDescartar";
            this.DevBarButtonItemDescartar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.DevBarButtonItemDescartar_ItemClick);
            // 
            // DevRibbonPageMenu
            // 
            this.DevRibbonPageMenu.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.DevRibbonPageGroupArchivos,
            this.DevRibbonPageGroupOpciones,
            this.DevRibbonPageGroupEnrolar});
            this.DevRibbonPageMenu.Name = "DevRibbonPageMenu";
            this.DevRibbonPageMenu.Text = "Menu";
            // 
            // DevRibbonPageGroupArchivos
            // 
            this.DevRibbonPageGroupArchivos.ItemLinks.Add(this.DevBarButtonSalir);
            this.DevRibbonPageGroupArchivos.Name = "DevRibbonPageGroupArchivos";
            this.DevRibbonPageGroupArchivos.Text = "Archivos";
            // 
            // DevRibbonPageGroupOpciones
            // 
            this.DevRibbonPageGroupOpciones.ItemLinks.Add(this.DevBarButtonSincronizar);
            this.DevRibbonPageGroupOpciones.ItemLinks.Add(this.DevBarButtonEnrolar);
            this.DevRibbonPageGroupOpciones.Name = "DevRibbonPageGroupOpciones";
            this.DevRibbonPageGroupOpciones.Text = "Acciones";
            // 
            // DevRibbonPageGroupEnrolar
            // 
            this.DevRibbonPageGroupEnrolar.ItemLinks.Add(this.DevBarButtonItemGuardar);
            this.DevRibbonPageGroupEnrolar.ItemLinks.Add(this.DevBarButtonItemDescartar);
            this.DevRibbonPageGroupEnrolar.Name = "DevRibbonPageGroupEnrolar";
            this.DevRibbonPageGroupEnrolar.Text = "Acciones de Enrolador";
            this.DevRibbonPageGroupEnrolar.Visible = false;
            // 
            // DevGroupControlNotificacionesAcciones
            // 
            this.DevGroupControlNotificacionesAcciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DevGroupControlNotificacionesAcciones.Location = new System.Drawing.Point(0, 706);
            this.DevGroupControlNotificacionesAcciones.Name = "DevGroupControlNotificacionesAcciones";
            this.DevGroupControlNotificacionesAcciones.Size = new System.Drawing.Size(1333, 77);
            this.DevGroupControlNotificacionesAcciones.TabIndex = 1;
            this.DevGroupControlNotificacionesAcciones.Text = "Notificaciones y Acciones";
            this.DevGroupControlNotificacionesAcciones.DoubleClick += new System.EventHandler(this.DevGroupControlNotificacionesAcciones_DoubleClick);
            // 
            // DevPanelControlPrincipal
            // 
            this.DevPanelControlPrincipal.Controls.Add(this.DevNavigationPanePrincipal);
            this.DevPanelControlPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DevPanelControlPrincipal.Location = new System.Drawing.Point(0, 143);
            this.DevPanelControlPrincipal.Name = "DevPanelControlPrincipal";
            this.DevPanelControlPrincipal.Size = new System.Drawing.Size(1333, 563);
            this.DevPanelControlPrincipal.TabIndex = 3;
            // 
            // DevNavigationPanePrincipal
            // 
            this.DevNavigationPanePrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DevNavigationPanePrincipal.Location = new System.Drawing.Point(2, 2);
            this.DevNavigationPanePrincipal.Name = "DevNavigationPanePrincipal";
            this.DevNavigationPanePrincipal.RegularSize = new System.Drawing.Size(1329, 559);
            this.DevNavigationPanePrincipal.SelectedPage = null;
            this.DevNavigationPanePrincipal.Size = new System.Drawing.Size(1329, 559);
            this.DevNavigationPanePrincipal.TabIndex = 0;
            this.DevNavigationPanePrincipal.Text = "navigationPane1";
            this.DevNavigationPanePrincipal.SelectedPageChanged += new DevExpress.XtraBars.Navigation.SelectedPageChangedEventHandler(this.DevNavigationPanePrincipal_SelectedPageChanged);
            this.DevNavigationPanePrincipal.QueryControl += new DevExpress.XtraBars.Navigation.QueryControlEventHandler(this.DevNavigationPanePrincipal_QueryControl);
            // 
            // backgroundWorkerChequeoNotificaciones
            // 
            this.backgroundWorkerChequeoNotificaciones.WorkerReportsProgress = true;
            this.backgroundWorkerChequeoNotificaciones.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerChequeoNotificaciones_DoWork);
            this.backgroundWorkerChequeoNotificaciones.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorkerChequeoNotificaciones_ProgressChanged);
            // 
            // FrmPrincipal
            // 
            this.AllowFormGlass = DevExpress.Utils.DefaultBoolean.False;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1333, 783);
            this.Controls.Add(this.DevPanelControlPrincipal);
            this.Controls.Add(this.DevGroupControlNotificacionesAcciones);
            this.Controls.Add(this.DevRibbonControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1343, 726);
            this.Name = "FrmPrincipal";
            this.Ribbon = this.DevRibbonControl;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enrolador";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DevRibbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGroupControlNotificacionesAcciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevPanelControlPrincipal)).EndInit();
            this.DevPanelControlPrincipal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DevNavigationPanePrincipal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl DevRibbonControl;
        private DevExpress.XtraBars.Ribbon.RibbonPage DevRibbonPageMenu;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup DevRibbonPageGroupArchivos;
        private DevExpress.XtraBars.BarButtonItem DevBarButtonSalir;
        private DevExpress.XtraBars.BarButtonItem DevBarButtonSincronizar;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup DevRibbonPageGroupOpciones;
        private DevExpress.XtraBars.BarButtonItem DevBarButtonEnrolar;
        private DevExpress.XtraEditors.GroupControl DevGroupControlNotificacionesAcciones;
        private DevExpress.XtraEditors.PanelControl DevPanelControlPrincipal;
        private DevExpress.XtraBars.Navigation.NavigationPane DevNavigationPanePrincipal;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup DevRibbonPageGroupEnrolar;
        private DevExpress.XtraBars.BarButtonItem DevBarButtonItemGuardar;
        private DevExpress.XtraBars.BarButtonItem DevBarButtonItemDescartar;
        private System.ComponentModel.BackgroundWorker backgroundWorkerChequeoNotificaciones;
    }
}

