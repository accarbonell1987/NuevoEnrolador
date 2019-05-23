using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraWaitForm;

namespace EnroladorStandAloneV2.CapaInterfazUsuario {
    public partial class FrmWait : WaitForm {
        public FrmWait() {
            InitializeComponent();
            this.DevProgressPanel.AutoHeight = true;
        }

        #region Overrides

        public override void SetCaption(string caption) {
            base.SetCaption(caption);
            this.DevProgressPanel.Caption = caption;
        }
        public override void SetDescription(string description) {
            base.SetDescription(description);
            this.DevProgressPanel.Description = description;
        }
        public override void ProcessCommand(Enum cmd, object arg) {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum WaitFormCommand {
        }
    }
}