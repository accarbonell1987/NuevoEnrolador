using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using EnroladorStandAloneV2.Herramientas;

namespace EnroladorStandAloneV2 {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            BonusSkins.Register();
            SkinManager.EnableFormSkins();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SingleGlobalInstance.Run(5000, new FrmPrincipal());
        }
    }
}
