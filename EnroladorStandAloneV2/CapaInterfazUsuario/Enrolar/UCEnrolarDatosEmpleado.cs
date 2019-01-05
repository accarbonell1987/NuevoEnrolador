using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EnroladorStandAloneV2.CapaLogicaNegocio;
using EnroladorAccesoDatos.Ayudantes;
using System.Reflection;
using EnroladorAccesoDatos.Dominio;

namespace EnroladorStandAloneV2.CapaInterfazUsuario.Enrolar {
    public partial class UCEnrolarDatosEmpleado : DevExpress.XtraEditors.XtraUserControl {
        #region Atributos
        NegocioEnrolador Negocio;
        POCOEmpleado empleado;
        #endregion

        #region Constructor
        public UCEnrolarDatosEmpleado(NegocioEnrolador Negocio, POCOEmpleado empleado) {
            InitializeComponent();
            this.empleado = empleado;
            try {
                this.Negocio = Negocio;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        #endregion

        #region Metodos y Eventos
        private void UCDatosEmpleado_Load(object sender, EventArgs e) {
            try {
                if (empleado != null) {
                    CargarDatos();
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }

        private void CargarDatos() {
            DevTextEditNombres.Text = empleado.Nombres;
            DevTextEditApellidos.Text = empleado.Apellidos;
            DevTextEditCorreo.Text = empleado.Correo;
            DevTextEditTelefono.Text = empleado.NumeroTelefono;
        }

        private void DevRadioGroupAcceso_EditValueChanged(object sender, EventArgs e) {
            int valor = Convert.ToInt16(DevRadioGroupAcceso.EditValue);
            DevPanelControlAcceso.Controls.Clear();
            if (valor == 0) {
                UCEnrolarClave uC = new UCEnrolarClave() {
                    Dock = DockStyle.Fill
                };

                DevPanelControlAcceso.Controls.Add(uC);
            }
        }
        #endregion
    }
}
