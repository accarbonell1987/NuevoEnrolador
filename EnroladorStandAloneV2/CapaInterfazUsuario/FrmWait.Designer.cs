namespace EnroladorStandAloneV2.CapaInterfazUsuario {
    partial class FrmWait {
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
            this.DevProgressPanel = new DevExpress.XtraWaitForm.ProgressPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DevProgressPanel
            // 
            this.DevProgressPanel.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.DevProgressPanel.Appearance.Options.UseBackColor = true;
            this.DevProgressPanel.AppearanceCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.DevProgressPanel.AppearanceCaption.Options.UseFont = true;
            this.DevProgressPanel.AppearanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.DevProgressPanel.AppearanceDescription.Options.UseFont = true;
            this.DevProgressPanel.BarAnimationElementThickness = 2;
            this.DevProgressPanel.Caption = "Espere, por favor";
            this.DevProgressPanel.Description = "Cargando ...";
            this.DevProgressPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DevProgressPanel.ImageHorzOffset = 20;
            this.DevProgressPanel.Location = new System.Drawing.Point(0, 17);
            this.DevProgressPanel.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.DevProgressPanel.Name = "DevProgressPanel";
            this.DevProgressPanel.Size = new System.Drawing.Size(246, 39);
            this.DevProgressPanel.TabIndex = 0;
            this.DevProgressPanel.Text = "DevProgressPanel";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.DevProgressPanel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 14, 0, 14);
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(246, 73);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // FrmWait
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(246, 73);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "FrmWait";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraWaitForm.ProgressPanel DevProgressPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
