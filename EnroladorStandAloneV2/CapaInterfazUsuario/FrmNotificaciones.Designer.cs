namespace EnroladorStandAloneV2.CapaInterfazUsuario {
    partial class FrmNotificaciones {
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
        ///
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNotificaciones));
            this.DevLayoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.DevLabelControlNotificaciones = new DevExpress.XtraEditors.LabelControl();
            this.DevGridControlNotificaciones = new DevExpress.XtraGrid.GridControl();
            this.DevGridViewNotificaciones = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.DevLayoutControlGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.DevIemLabelNotificaciones = new DevExpress.XtraLayout.LayoutControlItem();
            this.DevItemGrid = new DevExpress.XtraLayout.LayoutControlItem();
            this.DevSeparatorControl = new DevExpress.XtraEditors.SeparatorControl();
            this.DevButtonCancelar = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControl)).BeginInit();
            this.DevLayoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DevGridControlNotificaciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGridViewNotificaciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevIemLabelNotificaciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevItemGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevSeparatorControl)).BeginInit();
            this.SuspendLayout();
            // 
            // DevLayoutControl
            // 
            this.DevLayoutControl.AllowCustomization = false;
            this.DevLayoutControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DevLayoutControl.Controls.Add(this.DevLabelControlNotificaciones);
            this.DevLayoutControl.Controls.Add(this.DevGridControlNotificaciones);
            this.DevLayoutControl.Location = new System.Drawing.Point(0, 0);
            this.DevLayoutControl.Name = "DevLayoutControl";
            this.DevLayoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(839, 267, 450, 400);
            this.DevLayoutControl.Root = this.DevLayoutControlGroup;
            this.DevLayoutControl.Size = new System.Drawing.Size(571, 268);
            this.DevLayoutControl.TabIndex = 1;
            // 
            // DevLabelControlNotificaciones
            // 
            this.DevLabelControlNotificaciones.AllowHtmlString = true;
            this.DevLabelControlNotificaciones.Appearance.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DevLabelControlNotificaciones.Appearance.ForeColor = System.Drawing.Color.Black;
            this.DevLabelControlNotificaciones.Appearance.Options.UseFont = true;
            this.DevLabelControlNotificaciones.Appearance.Options.UseForeColor = true;
            this.DevLabelControlNotificaciones.Appearance.Options.UseTextOptions = true;
            this.DevLabelControlNotificaciones.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.DevLabelControlNotificaciones.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.DevLabelControlNotificaciones.Location = new System.Drawing.Point(10, 0);
            this.DevLabelControlNotificaciones.Name = "DevLabelControlNotificaciones";
            this.DevLabelControlNotificaciones.Padding = new System.Windows.Forms.Padding(0, 3, 13, 6);
            this.DevLabelControlNotificaciones.Size = new System.Drawing.Size(551, 34);
            this.DevLabelControlNotificaciones.StyleController = this.DevLayoutControl;
            this.DevLabelControlNotificaciones.TabIndex = 4;
            this.DevLabelControlNotificaciones.Text = "Notificaciones";
            // 
            // DevGridControlNotificaciones
            // 
            this.DevGridControlNotificaciones.Location = new System.Drawing.Point(10, 34);
            this.DevGridControlNotificaciones.MainView = this.DevGridViewNotificaciones;
            this.DevGridControlNotificaciones.Name = "DevGridControlNotificaciones";
            this.DevGridControlNotificaciones.Size = new System.Drawing.Size(551, 234);
            this.DevGridControlNotificaciones.TabIndex = 2;
            this.DevGridControlNotificaciones.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.DevGridViewNotificaciones});
            // 
            // DevGridViewNotificaciones
            // 
            this.DevGridViewNotificaciones.GridControl = this.DevGridControlNotificaciones;
            this.DevGridViewNotificaciones.Name = "DevGridViewNotificaciones";
            this.DevGridViewNotificaciones.OptionsBehavior.Editable = false;
            this.DevGridViewNotificaciones.OptionsCustomization.AllowColumnMoving = false;
            this.DevGridViewNotificaciones.OptionsCustomization.AllowGroup = false;
            this.DevGridViewNotificaciones.OptionsCustomization.AllowQuickHideColumns = false;
            this.DevGridViewNotificaciones.OptionsMenu.EnableColumnMenu = false;
            this.DevGridViewNotificaciones.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.DevGridViewNotificaciones.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.DevGridViewNotificaciones.OptionsView.ShowGroupPanel = false;
            this.DevGridViewNotificaciones.OptionsView.ShowIndicator = false;
            // 
            // DevLayoutControlGroup
            // 
            this.DevLayoutControlGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.DevLayoutControlGroup.GroupBordersVisible = false;
            this.DevLayoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.DevIemLabelNotificaciones,
            this.DevItemGrid});
            this.DevLayoutControlGroup.Location = new System.Drawing.Point(0, 0);
            this.DevLayoutControlGroup.Name = "DevLayoutControlGroup";
            this.DevLayoutControlGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 0, 0);
            this.DevLayoutControlGroup.Size = new System.Drawing.Size(571, 268);
            this.DevLayoutControlGroup.TextVisible = false;
            // 
            // DevIemLabelNotificaciones
            // 
            this.DevIemLabelNotificaciones.Control = this.DevLabelControlNotificaciones;
            this.DevIemLabelNotificaciones.Location = new System.Drawing.Point(0, 0);
            this.DevIemLabelNotificaciones.Name = "DevIemLabelNotificaciones";
            this.DevIemLabelNotificaciones.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.DevIemLabelNotificaciones.Size = new System.Drawing.Size(551, 34);
            this.DevIemLabelNotificaciones.TextSize = new System.Drawing.Size(0, 0);
            this.DevIemLabelNotificaciones.TextVisible = false;
            // 
            // DevItemGrid
            // 
            this.DevItemGrid.Control = this.DevGridControlNotificaciones;
            this.DevItemGrid.Location = new System.Drawing.Point(0, 34);
            this.DevItemGrid.Name = "DevItemGrid";
            this.DevItemGrid.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.DevItemGrid.Size = new System.Drawing.Size(551, 234);
            this.DevItemGrid.TextSize = new System.Drawing.Size(0, 0);
            this.DevItemGrid.TextVisible = false;
            // 
            // DevSeparatorControl
            // 
            this.DevSeparatorControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DevSeparatorControl.Location = new System.Drawing.Point(12, 268);
            this.DevSeparatorControl.Name = "DevSeparatorControl";
            this.DevSeparatorControl.Size = new System.Drawing.Size(547, 23);
            this.DevSeparatorControl.TabIndex = 10;
            // 
            // DevButtonCancelar
            // 
            this.DevButtonCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DevButtonCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.DevButtonCancelar.Image = ((System.Drawing.Image)(resources.GetObject("DevButtonCancelar.Image")));
            this.DevButtonCancelar.Location = new System.Drawing.Point(484, 290);
            this.DevButtonCancelar.Name = "DevButtonCancelar";
            this.DevButtonCancelar.Size = new System.Drawing.Size(75, 23);
            this.DevButtonCancelar.TabIndex = 9;
            this.DevButtonCancelar.Text = "Cancelar";
            // 
            // FrmNotificaciones
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 325);
            this.Controls.Add(this.DevButtonCancelar);
            this.Controls.Add(this.DevSeparatorControl);
            this.Controls.Add(this.DevLayoutControl);
            this.MinimumSize = new System.Drawing.Size(587, 364);
            this.Name = "FrmNotificaciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControl)).EndInit();
            this.DevLayoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DevGridControlNotificaciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevGridViewNotificaciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevIemLabelNotificaciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevItemGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevSeparatorControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl DevLayoutControl;
        private DevExpress.XtraEditors.LabelControl DevLabelControlNotificaciones;
        private DevExpress.XtraGrid.GridControl DevGridControlNotificaciones;
        private DevExpress.XtraGrid.Views.Grid.GridView DevGridViewNotificaciones;
        private DevExpress.XtraLayout.LayoutControlGroup DevLayoutControlGroup;
        private DevExpress.XtraLayout.LayoutControlItem DevIemLabelNotificaciones;
        private DevExpress.XtraLayout.LayoutControlItem DevItemGrid;
        private DevExpress.XtraEditors.SeparatorControl DevSeparatorControl;
        private DevExpress.XtraEditors.SimpleButton DevButtonCancelar;
    }
}