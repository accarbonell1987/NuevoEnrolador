namespace EnroladorStandAloneV2.CapaInterfazUsuario {
    partial class FrmAutenticacionUsuario {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAutenticacionUsuario));
            this.DevButtonAceptar = new DevExpress.XtraEditors.SimpleButton();
            this.DevButtonCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.DevTextUsuario = new DevExpress.XtraEditors.TextEdit();
            this.DevTextClave = new DevExpress.XtraEditors.TextEdit();
            this.DevButtonCambiarUsuario = new DevExpress.XtraEditors.SimpleButton();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lblUser = new DevExpress.XtraEditors.LabelControl();
            this.lblPass = new DevExpress.XtraEditors.LabelControl();
            this.ribbonPage3 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DevTextUsuario.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevTextClave.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DevButtonAceptar
            // 
            this.DevButtonAceptar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.DevButtonAceptar.Image = global::EnroladorStandAloneV2.Properties.Resources.apply_16x161;
            this.DevButtonAceptar.Location = new System.Drawing.Point(215, 114);
            this.DevButtonAceptar.Name = "DevButtonAceptar";
            this.DevButtonAceptar.Size = new System.Drawing.Size(75, 23);
            this.DevButtonAceptar.TabIndex = 2;
            this.DevButtonAceptar.Text = "Aceptar";
            this.DevButtonAceptar.Click += new System.EventHandler(this.DevButtonAceptar_Click);
            // 
            // DevButtonCancelar
            // 
            this.DevButtonCancelar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.DevButtonCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.DevButtonCancelar.Image = global::EnroladorStandAloneV2.Properties.Resources.cancel_16x161;
            this.DevButtonCancelar.Location = new System.Drawing.Point(296, 114);
            this.DevButtonCancelar.Name = "DevButtonCancelar";
            this.DevButtonCancelar.Size = new System.Drawing.Size(75, 23);
            this.DevButtonCancelar.TabIndex = 3;
            this.DevButtonCancelar.Text = "Cancelar";
            // 
            // DevTextUsuario
            // 
            this.DevTextUsuario.EditValue = "Fmolina";
            this.DevTextUsuario.Location = new System.Drawing.Point(80, 35);
            this.DevTextUsuario.Name = "DevTextUsuario";
            this.DevTextUsuario.Size = new System.Drawing.Size(256, 20);
            this.DevTextUsuario.TabIndex = 0;
            // 
            // DevTextClave
            // 
            this.DevTextClave.EditValue = "asd123";
            this.DevTextClave.Location = new System.Drawing.Point(80, 61);
            this.DevTextClave.Name = "DevTextClave";
            this.DevTextClave.Properties.PasswordChar = '*';
            this.DevTextClave.Size = new System.Drawing.Size(256, 20);
            this.DevTextClave.TabIndex = 1;
            // 
            // DevButtonCambiarUsuario
            // 
            this.DevButtonCambiarUsuario.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.DevButtonCambiarUsuario.Image = global::EnroladorStandAloneV2.Properties.Resources.usergroup_16x16;
            this.DevButtonCambiarUsuario.Location = new System.Drawing.Point(4, 114);
            this.DevButtonCambiarUsuario.Name = "DevButtonCambiarUsuario";
            this.DevButtonCambiarUsuario.Size = new System.Drawing.Size(108, 23);
            this.DevButtonCambiarUsuario.TabIndex = 7;
            this.DevButtonCambiarUsuario.Text = "Cambiar Usuario";
            this.DevButtonCambiarUsuario.Click += new System.EventHandler(this.DevButtonCambiarUsuario_Click);
            // 
            // separatorControl1
            // 
            this.separatorControl1.Location = new System.Drawing.Point(4, 85);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Size = new System.Drawing.Size(352, 23);
            this.separatorControl1.TabIndex = 8;
            // 
            // dxErrorProvider
            // 
            this.dxErrorProvider.ContainerControl = this;
            // 
            // ribbonPage2
            // 
            this.ribbonPage2.Name = "ribbonPage2";
            this.ribbonPage2.Text = "ribbonPage2";
            // 
            // groupControl1
            // 
            this.groupControl1.CaptionImage = global::EnroladorStandAloneV2.Properties.Resources.employee_16x16;
            this.groupControl1.Controls.Add(this.separatorControl1);
            this.groupControl1.Controls.Add(this.DevTextUsuario);
            this.groupControl1.Controls.Add(this.DevButtonCambiarUsuario);
            this.groupControl1.Controls.Add(this.DevTextClave);
            this.groupControl1.Controls.Add(this.lblUser);
            this.groupControl1.Controls.Add(this.DevButtonCancelar);
            this.groupControl1.Controls.Add(this.lblPass);
            this.groupControl1.Controls.Add(this.DevButtonAceptar);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(377, 142);
            this.groupControl1.TabIndex = 8;
            this.groupControl1.Text = "Autenticación de Usuario";
            // 
            // lblUser
            // 
            this.lblUser.Location = new System.Drawing.Point(38, 38);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(40, 13);
            this.lblUser.TabIndex = 4;
            this.lblUser.Text = "Usuario:";
            // 
            // lblPass
            // 
            this.lblPass.Location = new System.Drawing.Point(18, 64);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(60, 13);
            this.lblPass.TabIndex = 5;
            this.lblPass.Text = "Contraseña:";
            // 
            // ribbonPage3
            // 
            this.ribbonPage3.Name = "ribbonPage3";
            this.ribbonPage3.Text = "ribbonPage3";
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "DevExpress Dark Style";
            // 
            // FrmAutenticacionUsuario
            // 
            this.AcceptButton = this.DevButtonAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.DevButtonCancelar;
            this.ClientSize = new System.Drawing.Size(377, 142);
            this.Controls.Add(this.groupControl1);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.None;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmAutenticacionUsuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enrolador Biometria Aplicada";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LogInDialog_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.DevTextUsuario.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevTextClave.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton DevButtonAceptar;
        private DevExpress.XtraEditors.SimpleButton DevButtonCancelar;
        private DevExpress.XtraEditors.TextEdit DevTextUsuario;
        private DevExpress.XtraEditors.TextEdit DevTextClave;
        private DevExpress.XtraEditors.SimpleButton DevButtonCambiarUsuario;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl lblUser;
        private DevExpress.XtraEditors.LabelControl lblPass;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage3;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
    }
}