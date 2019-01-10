using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnroladorStandAloneV2 {
    public partial class FrmSplashScreen : DevExpress.XtraEditors.XtraForm {
        //private System.Windows.Forms.Timer timer;
        //private int Tiempo = 0;

        public FrmSplashScreen() {
            InitializeComponent();
        }

        //private void DesactivarForm(object sender, EventArgs e) {
        //    if (Tiempo == 3000) Dispose();
        //    else Tiempo += 1000;
        //}

        //private void FrmBase_Load(object sender, EventArgs e) {
        //    timer = new System.Windows.Forms.Timer() {
        //        Interval = 1000
        //    };
        //    timer.Tick += DesactivarForm;
        //    timer.Start();
        //}
    }
}
