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
using System.Reflection;
using EnroladorAccesoDatos.Ayudantes;
using EnroladorStandAloneV2.Herramientas;
using EnroladorAccesoDatos.Dominio;

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
                //seleccionar el RUT en el textbox
                if (RUT != String.Empty) {
                    DevLookUpEditRUTEmpleado.Text = RUT;
                    var empleado = Negocio.ObtenerEmpleadoDeLista(RUT);
                    CargarEmpleado(empleado);
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }

        public void AdicionarUCAsistencia(UCManejarAsistencia uCAsistencia) {
            DevPanelControlAsistencia.Controls.Add(uCAsistencia);
        }
        public void EliminarUCAsistencia() {
            DevPanelControlAsistencia.Controls.Clear();
        }

        public void AdicionarUCCasino(UCManejarCasino uCAsistencia) {
            DevPanelControlCasinos.Controls.Add(uCAsistencia);
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
