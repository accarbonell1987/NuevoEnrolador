﻿using System;
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
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-Cl");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es");

            //var x = new CapaLogicaNegocio.NegocioEnrolador();
            //x.mContext = new EnroladorAccesoDatos.SQLite.SQLiteEnrollEntities();
            //var y = x.mContext.Hardware.ToList();
            //y.ForEach(p => x.mContext.Hardware.Remove(p));
            //x.mContext.SaveChanges();


            BonusSkins.Register();
            SkinManager.EnableFormSkins();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SingleGlobalInstance.Run(5000, new FrmPrincipal());
        }
    }
}
