namespace EnroladorStandAloneV2.CapaInterfazUsuario {
    partial class UCBarraInformacion {
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.DevButtonEditAccionesPendientes = new DevExpress.XtraEditors.ButtonEdit();
            this.DevButtonEditNotificacionesPendientes = new DevExpress.XtraEditors.ButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.DevButtonEditAccionesPendientes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevButtonEditNotificacionesPendientes.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // DevButtonEditAccionesPendientes
            // 
            this.DevButtonEditAccionesPendientes.Dock = System.Windows.Forms.DockStyle.Left;
            this.DevButtonEditAccionesPendientes.EditValue = "1";
            this.DevButtonEditAccionesPendientes.Location = new System.Drawing.Point(0, 0);
            this.DevButtonEditAccionesPendientes.Name = "DevButtonEditAccionesPendientes";
            this.DevButtonEditAccionesPendientes.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.DevButtonEditAccionesPendientes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleRight, global::EnroladorStandAloneV2.Properties.Resources.notes_16x16, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.DevButtonEditAccionesPendientes.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.DevButtonEditAccionesPendientes.Size = new System.Drawing.Size(60, 20);
            this.DevButtonEditAccionesPendientes.TabIndex = 0;
            this.DevButtonEditAccionesPendientes.ToolTip = "Cantidad de acciones pendientes";
            this.DevButtonEditAccionesPendientes.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.DevButtonEditAccionesPendientes.ToolTipTitle = "Información";
            // 
            // DevButtonEditNotificacionesPendientes
            // 
            this.DevButtonEditNotificacionesPendientes.Dock = System.Windows.Forms.DockStyle.Right;
            this.DevButtonEditNotificacionesPendientes.EditValue = "1";
            this.DevButtonEditNotificacionesPendientes.Location = new System.Drawing.Point(63, 0);
            this.DevButtonEditNotificacionesPendientes.Name = "DevButtonEditNotificacionesPendientes";
            this.DevButtonEditNotificacionesPendientes.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.DevButtonEditNotificacionesPendientes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleRight, global::EnroladorStandAloneV2.Properties.Resources.pielabelstooltips_16x16, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            this.DevButtonEditNotificacionesPendientes.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.DevButtonEditNotificacionesPendientes.Size = new System.Drawing.Size(60, 20);
            this.DevButtonEditNotificacionesPendientes.TabIndex = 1;
            this.DevButtonEditNotificacionesPendientes.ToolTip = "Cantidad de notificaciones pendientes";
            this.DevButtonEditNotificacionesPendientes.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.DevButtonEditNotificacionesPendientes.ToolTipTitle = "Información";
            // 
            // UCBarraInformacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DevButtonEditNotificacionesPendientes);
            this.Controls.Add(this.DevButtonEditAccionesPendientes);
            this.Name = "UCBarraInformacion";
            this.Size = new System.Drawing.Size(123, 20);
            ((System.ComponentModel.ISupportInitialize)(this.DevButtonEditAccionesPendientes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevButtonEditNotificacionesPendientes.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ButtonEdit DevButtonEditAccionesPendientes;
        private DevExpress.XtraEditors.ButtonEdit DevButtonEditNotificacionesPendientes;
    }
}
