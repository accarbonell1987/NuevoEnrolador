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
        UCEnrolador Padre;

        POCOEmpleado copiaEmpleado;
        #endregion

        #region Constructor
        public UCEnrolarDatosEmpleado(UCEnrolador Padre, NegocioEnrolador Negocio, POCOEmpleado empleado) {
            InitializeComponent();
            try {
                this.Padre = Padre;
                this.Negocio = Negocio;
                this.empleado = empleado;

                if (empleado != null)
                    copiaEmpleado = (empleado.Clone() as POCOEmpleado);
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        #endregion

        #region Metodos y Eventos
        private void UCDatosEmpleado_Load(object sender, EventArgs e) {
            try {
                if (empleado == null) return;
                CargarDatos();
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        private void CargarDatos() {
            if (empleado == null) return;

            DevTextEditNombres.Text = empleado.Nombres;
            DevTextEditNombres.Enabled = false;

            DevTextEditApellidos.Text = empleado.Apellidos;
            DevTextEditApellidos.Enabled = false;

            DevTextEditCorreo.Text = empleado.Correo;
            DevTextEditTelefono.Text = empleado.NumeroTelefono;

            if (empleado.TieneContraseña) DevRadioGroupAcceso.SelectedIndex = 0;
            else DevRadioGroupAcceso.SelectedIndex = 1;
        }
        private void DevRadioGroupAcceso_EditValueChanged(object sender, EventArgs e) {
            int valor = Convert.ToInt16(DevRadioGroupAcceso.EditValue);
            DevPanelControlAcceso.Controls.Clear();

            if (valor == 0) {
                UCEnrolarClave uC = new UCEnrolarClave(Padre, Negocio, empleado) {
                    Dock = DockStyle.Fill
                };

                DevPanelControlAcceso.Controls.Add(uC);
            } else {
                UCEnrolarHuellas uC = new UCEnrolarHuellas(Padre, Negocio, empleado) {
                    Dock = DockStyle.Fill
                };

                DevPanelControlAcceso.Controls.Add(uC);
            }
        }
        public void DescartarCambios() {
            empleado = copiaEmpleado;
        }
        #endregion
    }
}
