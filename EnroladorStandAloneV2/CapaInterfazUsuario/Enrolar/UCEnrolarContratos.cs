﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EnroladorStandAloneV2.CapaLogicaNegocio;
using EnroladorAccesoDatos.Dominio;
using EnroladorAccesoDatos.Ayudantes;
using System.Reflection;
using DevExpress.XtraEditors;

namespace EnroladorStandAloneV2.CapaInterfazUsuario.Enrolar {
    public partial class UCEnrolarContratos : DevExpress.XtraEditors.XtraUserControl {
        #region Atributos
        UCEnrolar Padre;
        NegocioEnrolador Negocio;
        POCOEmpleado empleado;
        #endregion

        #region Constructor
        public UCEnrolarContratos(UCEnrolar Padre, NegocioEnrolador Negocio, POCOEmpleado empleado) {
            InitializeComponent();
            try {
                this.Padre = Padre;
                this.Negocio = Negocio;
                this.empleado = empleado;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        #endregion

        #region Metodos y Eventos
        private void UCEnrolarContratos_Load(object sender, EventArgs e) {
            try {
                if (empleado != null) {
                    CargarDatos();
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }

        private void CargarDatos() {
            bsContratos.DataSource = empleado.Contratos;
            bsEmpresas.DataSource = Negocio.ObtenerTodasEmpresas();
            bsCuentas.DataSource = Negocio.ObtenerTodasCuentas();
            bsCargos.DataSource = Negocio.ObtenerTodosCargos();
        }

        private void DevGridControlContratos_Click(object sender, EventArgs e) {
            try {
                //string RUT = (string)DevGridViewEmpleados.GetFocusedRowCellValue("RUT");
                //if (!string.IsNullOrEmpty(RUT)) {
                //    (FindForm() as FrmPrincipal).CrearPaginaNavegacion("Enrolar", RUT);
                //}
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }

        private void DevCheckEditManejaAsistencia_EditValueChanged(object sender, EventArgs e) {
            if (DevCheckEditManejaAsistencia.Checked) {
                UCEnrolarAsistencia uC = new UCEnrolarAsistencia(this, Negocio, empleado);
                Padre.AdicionarUCAsistencia(uC);
            } else {
                Padre.EliminarUCAsistencia();
            }
        }
        #endregion
    }
}