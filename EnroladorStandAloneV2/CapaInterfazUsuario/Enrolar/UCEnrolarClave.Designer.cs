namespace EnroladorStandAloneV2.CapaInterfazUsuario.Enrolar {
    partial class UCEnrolarClave {
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
            this.DevLayoutControlComponentesAcceso = new DevExpress.XtraLayout.LayoutControl();
            this.DevTextEditRevalidarClave = new DevExpress.XtraEditors.TextEdit();
            this.DevTextClave = new DevExpress.XtraEditors.TextEdit();
            this.DevLayoutControlGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.DevLayoutControlItemClave = new DevExpress.XtraLayout.LayoutControlItem();
            this.DevLayoutControlItemRevalidarClave = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.DevSimpleButtonAplicar = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlComponentesAcceso)).BeginInit();
            this.DevLayoutControlComponentesAcceso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DevTextEditRevalidarClave.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevTextClave.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlItemClave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlItemRevalidarClave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // DevLayoutControlComponentesAcceso
            // 
            this.DevLayoutControlComponentesAcceso.Controls.Add(this.DevTextEditRevalidarClave);
            this.DevLayoutControlComponentesAcceso.Controls.Add(this.DevTextClave);
            this.DevLayoutControlComponentesAcceso.Dock = System.Windows.Forms.DockStyle.Top;
            this.DevLayoutControlComponentesAcceso.Location = new System.Drawing.Point(0, 0);
            this.DevLayoutControlComponentesAcceso.Name = "DevLayoutControlComponentesAcceso";
            this.DevLayoutControlComponentesAcceso.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(47, 121, 450, 400);
            this.DevLayoutControlComponentesAcceso.Root = this.DevLayoutControlGroup;
            this.DevLayoutControlComponentesAcceso.Size = new System.Drawing.Size(224, 69);
            this.DevLayoutControlComponentesAcceso.TabIndex = 0;
            this.DevLayoutControlComponentesAcceso.Text = "layoutControl1";
            // 
            // DevTextEditRevalidarClave
            // 
            this.DevTextEditRevalidarClave.EditValue = "";
            this.DevTextEditRevalidarClave.Location = new System.Drawing.Point(94, 36);
            this.DevTextEditRevalidarClave.Name = "DevTextEditRevalidarClave";
            this.DevTextEditRevalidarClave.Properties.PasswordChar = '*';
            this.DevTextEditRevalidarClave.Size = new System.Drawing.Size(118, 20);
            this.DevTextEditRevalidarClave.StyleController = this.DevLayoutControlComponentesAcceso;
            this.DevTextEditRevalidarClave.TabIndex = 4;
            this.DevTextEditRevalidarClave.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DevTextEditRevalidarClave_KeyPress);
            this.DevTextEditRevalidarClave.Validated += new System.EventHandler(this.DevTextEditRevalidarClave_Validated);
            // 
            // DevTextClave
            // 
            this.DevTextClave.EditValue = "";
            this.DevTextClave.Location = new System.Drawing.Point(94, 12);
            this.DevTextClave.Name = "DevTextClave";
            this.DevTextClave.Properties.PasswordChar = '*';
            this.DevTextClave.Size = new System.Drawing.Size(118, 20);
            this.DevTextClave.StyleController = this.DevLayoutControlComponentesAcceso;
            this.DevTextClave.TabIndex = 0;
            this.DevTextClave.EditValueChanged += new System.EventHandler(this.DevTextClave_EditValueChanged);
            // 
            // DevLayoutControlGroup
            // 
            this.DevLayoutControlGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.DevLayoutControlGroup.GroupBordersVisible = false;
            this.DevLayoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.DevLayoutControlItemClave,
            this.DevLayoutControlItemRevalidarClave});
            this.DevLayoutControlGroup.Location = new System.Drawing.Point(0, 0);
            this.DevLayoutControlGroup.Name = "DevLayoutControlGroup";
            this.DevLayoutControlGroup.Size = new System.Drawing.Size(224, 69);
            this.DevLayoutControlGroup.TextVisible = false;
            // 
            // DevLayoutControlItemClave
            // 
            this.DevLayoutControlItemClave.Control = this.DevTextClave;
            this.DevLayoutControlItemClave.Location = new System.Drawing.Point(0, 0);
            this.DevLayoutControlItemClave.Name = "DevLayoutControlItemClave";
            this.DevLayoutControlItemClave.Size = new System.Drawing.Size(204, 24);
            this.DevLayoutControlItemClave.Text = "Clave:";
            this.DevLayoutControlItemClave.TextSize = new System.Drawing.Size(79, 13);
            // 
            // DevLayoutControlItemRevalidarClave
            // 
            this.DevLayoutControlItemRevalidarClave.Control = this.DevTextEditRevalidarClave;
            this.DevLayoutControlItemRevalidarClave.Enabled = false;
            this.DevLayoutControlItemRevalidarClave.Location = new System.Drawing.Point(0, 24);
            this.DevLayoutControlItemRevalidarClave.Name = "DevLayoutControlItemRevalidarClave";
            this.DevLayoutControlItemRevalidarClave.Size = new System.Drawing.Size(204, 25);
            this.DevLayoutControlItemRevalidarClave.Text = "Revalidar Clave:";
            this.DevLayoutControlItemRevalidarClave.TextSize = new System.Drawing.Size(79, 13);
            // 
            // dxErrorProvider
            // 
            this.dxErrorProvider.ContainerControl = this;
            // 
            // DevSimpleButtonAplicar
            // 
            this.DevSimpleButtonAplicar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.DevSimpleButtonAplicar.Enabled = false;
            this.DevSimpleButtonAplicar.Image = global::EnroladorStandAloneV2.Properties.Resources.apply_16x161;
            this.DevSimpleButtonAplicar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.DevSimpleButtonAplicar.Location = new System.Drawing.Point(169, 62);
            this.DevSimpleButtonAplicar.Name = "DevSimpleButtonAplicar";
            this.DevSimpleButtonAplicar.Size = new System.Drawing.Size(43, 23);
            this.DevSimpleButtonAplicar.TabIndex = 8;
            this.DevSimpleButtonAplicar.ToolTip = "Caduca el contrato seleccionado...";
            this.DevSimpleButtonAplicar.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.DevSimpleButtonAplicar.ToolTipTitle = "Caducar";
            this.DevSimpleButtonAplicar.Click += new System.EventHandler(this.DevSimpleButtonAplicar_Click);
            // 
            // UCEnrolarClave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DevSimpleButtonAplicar);
            this.Controls.Add(this.DevLayoutControlComponentesAcceso);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(224, 69);
            this.Name = "UCEnrolarClave";
            this.Size = new System.Drawing.Size(224, 89);
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlComponentesAcceso)).EndInit();
            this.DevLayoutControlComponentesAcceso.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DevTextEditRevalidarClave.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevTextClave.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlItemClave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlItemRevalidarClave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl DevLayoutControlComponentesAcceso;
        private DevExpress.XtraLayout.LayoutControlGroup DevLayoutControlGroup;
        private DevExpress.XtraEditors.TextEdit DevTextClave;
        private DevExpress.XtraLayout.LayoutControlItem DevLayoutControlItemClave;
        private DevExpress.XtraEditors.TextEdit DevTextEditRevalidarClave;
        private DevExpress.XtraLayout.LayoutControlItem DevLayoutControlItemRevalidarClave;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
        private DevExpress.XtraEditors.SimpleButton DevSimpleButtonAplicar;
    }
}
