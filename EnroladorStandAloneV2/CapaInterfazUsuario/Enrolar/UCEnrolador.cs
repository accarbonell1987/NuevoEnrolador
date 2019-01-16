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
using System.Reflection;
using EnroladorAccesoDatos.Ayudantes;
using EnroladorStandAloneV2.Herramientas;
using EnroladorAccesoDatos.Dominio;
using DevExpress.XtraEditors;

namespace EnroladorStandAloneV2.CapaInterfazUsuario.Enrolar {
    public partial class UCEnrolador : DevExpress.XtraEditors.XtraUserControl {
        #region MyRegion
        NegocioEnrolador Negocio;
        private string RUT;
        #endregion

        #region Constructor
        public UCEnrolador(NegocioEnrolador Negocio, string RUT) {
            InitializeComponent();

            try {
                this.Negocio = Negocio;
                this.RUT = RUT;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        #endregion

        #region Metodos y Atributos
        private void UCEnrolador_Load(object sender, EventArgs e) {
            try {
                bsEmpleados.DataSource = Negocio.lEmpleados;

                POCOEmpleado empleado = null;

                //seleccionar el RUT en el textbox
                if ((RUT != String.Empty)&&(!RUT.Equals("Nuevo"))) {
                    DevLookUpEditRUTEmpleado.Text = RUT;
                    empleado = Negocio.ObtenerEmpleadoDeLista(RUT);
                }

                CargarEmpleado(empleado);
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }

        public void AdicionarUCAsistenciaOCasino(XtraUserControl uC) {
            int hSize = DevSplitContainerControlAccesos.Size.Height;
            hSize = hSize / 2 - 10;
            Size s = new Size(uC.Size.Width, hSize);
            DevSplitContainerControlAccesos.SplitterPosition = hSize;

            uC.Size = s;

            var tipo = uC.GetType();
            if (uC is UCManejarAsistencia) {
                DevPanelControlAsistencia.Controls.Add(uC);
            } else {
                DevPanelControlCasinos.Controls.Add(uC);
            }
        }
        public void EliminarUCAsistencia() {
            DevPanelControlAsistencia.Controls.Clear();
        }
        public void EliminarUCCasino() {
            DevPanelControlCasinos.Controls.Clear();
        }
        private void DevLookUpEditRUTEmpleado_EditValueChanged(object sender, EventArgs e) {
            try {
                string RUT = DevLookUpEditRUTEmpleado.Text;
                if (RUT != String.Empty) {

                    var empleado = Negocio.ObtenerEmpleadoDeLista(RUT);
                    if (empleado == null) return;

                    this.RUT = RUT;
                    CargarEmpleado(empleado);
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        private void CargarEmpleado(POCOEmpleado empleado) {
            try {
                UCEnrolarDatosEmpleado uCEmpleados = new UCEnrolarDatosEmpleado(this, Negocio, empleado) {
                    Dock = DockStyle.Fill
                };
                UCEnrolarContratos uCContratos = new UCEnrolarContratos(this, Negocio, empleado) {
                    Dock = DockStyle.Fill
                };

                DevPanelControlDatosEmpleado.Controls.Clear();
                DevPanelControlContratos.Controls.Clear();

                DevPanelControlDatosEmpleado.Controls.Add(uCEmpleados);
                DevPanelControlContratos.Controls.Add(uCContratos);
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        #endregion
    }
}
