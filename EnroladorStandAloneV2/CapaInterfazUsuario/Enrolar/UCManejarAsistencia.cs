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
using EnroladorAccesoDatos.Dominio;
using EnroladorAccesoDatos.Ayudantes;
using System.Reflection;
using DevExpress.XtraEditors;

namespace EnroladorStandAloneV2.CapaInterfazUsuario.Enrolar {
    #region Atributos

    #endregion
    public partial class UCManejarAsistencia : DevExpress.XtraEditors.XtraUserControl {
        #region Atributos
        UCEnrolarContratos Padre;
        NegocioEnrolador Negocio;
        POCOEmpleado empleado;
        #endregion

        #region Constructor
        public UCManejarAsistencia(UCEnrolarContratos Padre, NegocioEnrolador Negocio, POCOEmpleado empleado) {
            InitializeComponent();
            Dock = DockStyle.Fill;

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
        private void UCEnrolarAsistencia_Load(object sender, EventArgs e) {
            try {
                if (empleado != null) {
                    CargarDatos();
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }

        public void CargarDatos() {
            bsEmpleadoDispositivos.DataSource = empleado.Dispositivos;
            bsInstalaciones.DataSource = Negocio.ObtenerTodasInstalaciones();
        }

        private void DevLookUpEditInstalacion_EditValueChanged(object sender, EventArgs e) {
            try {
                var GuidInstalacion = DevLookUpEditInstalacion.GetColumnValue("GuidInstalacion");
                if (GuidInstalacion == null) {
                    DevLookUpEditDispositivo.Enabled = false;
                }

                var instalacion = Negocio.ObtenerInstalacion((Guid)GuidInstalacion);
                var dispositivos = instalacion.Dispositivos;

                //chequear que existan servicios para la instalacion escogida
                if (dispositivos.Count > 0) {
                    bsDispositivos.DataSource = dispositivos;
                    DevLookUpEditDispositivo.Enabled = true;
                } else {
                    Negocio.AdicionarNotificacionListadoVacio("No existen dispositivos para la instalacion: " + instalacion.NombreInstalacion);
                    DevLookUpEditDispositivo.Enabled = false;
                }
                DevLookUpEditDispositivo.Enabled = true;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }

        private void DevLookUpEditDispositivo_EditValueChanged(object sender, EventArgs e) {
            //ver si hay alguno escogido
            //ver si este ya no esta en la lista
            //SI
            //activar el boton adicionar
            //NO
            //desactivar
        }
        #endregion
    }
}
