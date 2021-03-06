﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.ComponentModel.DataAnnotations;
using DevExpress.XtraBars;
using EnroladorStandAloneV2.Herramientas;
using EnroladorAccesoDatos.Dominio;

namespace EnroladorStandAloneV2.CapaInterfazUsuario {
    public partial class FrmNotificaciones : XtraForm {
        #region Atributos
        public List<POCONotificacion> ListaNotificaciones { get; set; }
        #endregion

        #region Constructor
        public FrmNotificaciones() {
            InitializeComponent();
            ListaNotificaciones = new List<POCONotificacion>();

            DevGridControlNotificaciones.DataSource = GetDataSource();
        }

        public FrmNotificaciones(List<POCONotificacion> lNotificaciones) {
            InitializeComponent();
            ListaNotificaciones = lNotificaciones;

            DevGridControlNotificaciones.DataSource = GetDataSource();
        }
        #endregion

        #region Metodos y Eventos
        public BindingList<POCONotificacion> GetDataSource() {
            //setearle el icono porque las que vienen desde las Excepciones no se le agrega alla
            foreach (var notificacion in ListaNotificaciones) {
                if (notificacion.Tipo == EnroladorAccesoDatos.TipoNotificacion.Critica) {
                    notificacion.ImagenDeNotificacion = Properties.Resources.error_32x32;
                }
            }
            BindingList<POCONotificacion> blNotificaciones = new BindingList<POCONotificacion>(ListaNotificaciones);
            return blNotificaciones;
        }
        #endregion
    }
}
