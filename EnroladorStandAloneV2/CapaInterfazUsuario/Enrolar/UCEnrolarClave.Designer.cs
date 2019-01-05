﻿namespace EnroladorStandAloneV2.CapaInterfazUsuario.Enrolar {
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
            this.DevLayoutControlComponentesAcceso = new DevExpress.XtraLayout.LayoutControl();
            this.DevLayoutControlGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.DevTextClave = new DevExpress.XtraEditors.TextEdit();
            this.DevLayoutControlItemClave = new DevExpress.XtraLayout.LayoutControlItem();
            this.DevTextEditRevalidarClave = new DevExpress.XtraEditors.TextEdit();
            this.DevLayoutControlItemRevalidarClave = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlComponentesAcceso)).BeginInit();
            this.DevLayoutControlComponentesAcceso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevTextClave.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlItemClave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevTextEditRevalidarClave.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlItemRevalidarClave)).BeginInit();
            this.SuspendLayout();
            // 
            // DevLayoutControlComponentesAcceso
            // 
            this.DevLayoutControlComponentesAcceso.Controls.Add(this.DevTextEditRevalidarClave);
            this.DevLayoutControlComponentesAcceso.Controls.Add(this.DevTextClave);
            this.DevLayoutControlComponentesAcceso.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DevLayoutControlComponentesAcceso.Location = new System.Drawing.Point(0, 0);
            this.DevLayoutControlComponentesAcceso.Name = "DevLayoutControlComponentesAcceso";
            this.DevLayoutControlComponentesAcceso.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(47, 121, 450, 400);
            this.DevLayoutControlComponentesAcceso.Root = this.DevLayoutControlGroup;
            this.DevLayoutControlComponentesAcceso.Size = new System.Drawing.Size(224, 69);
            this.DevLayoutControlComponentesAcceso.TabIndex = 0;
            this.DevLayoutControlComponentesAcceso.Text = "layoutControl1";
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
            // DevTextClave
            // 
            this.DevTextClave.EditValue = "";
            this.DevTextClave.Location = new System.Drawing.Point(95, 12);
            this.DevTextClave.Name = "DevTextClave";
            this.DevTextClave.Properties.PasswordChar = '*';
            this.DevTextClave.Size = new System.Drawing.Size(117, 20);
            this.DevTextClave.StyleController = this.DevLayoutControlComponentesAcceso;
            this.DevTextClave.TabIndex = 0;
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
            // DevTextEditRevalidarClave
            // 
            this.DevTextEditRevalidarClave.EditValue = "";
            this.DevTextEditRevalidarClave.Location = new System.Drawing.Point(95, 36);
            this.DevTextEditRevalidarClave.Name = "DevTextEditRevalidarClave";
            this.DevTextEditRevalidarClave.Properties.PasswordChar = '*';
            this.DevTextEditRevalidarClave.Size = new System.Drawing.Size(117, 20);
            this.DevTextEditRevalidarClave.StyleController = this.DevLayoutControlComponentesAcceso;
            this.DevTextEditRevalidarClave.TabIndex = 4;
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
            // UCClave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DevLayoutControlComponentesAcceso);
            this.MinimumSize = new System.Drawing.Size(224, 69);
            this.Name = "UCClave";
            this.Size = new System.Drawing.Size(224, 69);
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlComponentesAcceso)).EndInit();
            this.DevLayoutControlComponentesAcceso.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevTextClave.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlItemClave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevTextEditRevalidarClave.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevLayoutControlItemRevalidarClave)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl DevLayoutControlComponentesAcceso;
        private DevExpress.XtraLayout.LayoutControlGroup DevLayoutControlGroup;
        private DevExpress.XtraEditors.TextEdit DevTextClave;
        private DevExpress.XtraLayout.LayoutControlItem DevLayoutControlItemClave;
        private DevExpress.XtraEditors.TextEdit DevTextEditRevalidarClave;
        private DevExpress.XtraLayout.LayoutControlItem DevLayoutControlItemRevalidarClave;
    }
}
