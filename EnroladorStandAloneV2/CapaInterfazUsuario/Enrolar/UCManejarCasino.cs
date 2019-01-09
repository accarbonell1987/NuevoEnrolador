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
    public partial class UCManejarCasino : DevExpress.XtraEditors.XtraUserControl {
        #region Atributos
        UCEnrolarContratos Padre;
        NegocioEnrolador Negocio;
        POCOEmpleado empleado;
        #endregion

        #region Constructor
        public UCManejarCasino(UCEnrolarContratos Padre, NegocioEnrolador Negocio, POCOEmpleado empleado) {
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
        private void UCManejarCasino_Load(object sender, EventArgs e) {
            try {
                if (empleado != null) {
                    CargarDatos();
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }

        public void CargarDatos() {
            bsEmpleadoTurnoServicioCasino.DataSource = Negocio.ObtenerTodosEmpleadoTurnoServicioCasinoDelEmpleado(empleado.GuidEmpleado);
            bsInstalaciones.DataSource = Negocio.ObtenerTodasInstalaciones();
        }
        #endregion

        private void DevLookUpEditInstalacion_EditValueChanged(object sender, EventArgs e) {
            try {
                var GuidInstalacion = DevLookUpEditCasino.GetColumnValue("GuidInstalacion");
                if (GuidInstalacion == null) {
                    DevLookUpEditServicio.Enabled = false;
                }
                var instalacion = Negocio.ObtenerInstalacion((Guid)GuidInstalacion);
                var servicios = instalacion.ServiciosDelCasino;

                //chequear que existan servicios para la instalacion escogida
                if (servicios.Count > 0) {
                    bsServicios.DataSource = servicios;
                    DevLookUpEditServicio.Enabled = true;
                } else {
                    Negocio.AdicionarNotificacionListadoVacio("No existen servicios para la instalacion: " + instalacion.NombreInstalacion);
                    DevLookUpEditServicio.Enabled = false;
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }

        private void DevLookUpEditServicio_EditValueChanged(object sender, EventArgs e) {
            try {
                var GuidServicio = DevLookUpEditCasino.GetColumnValue("GuidServicioCasino");
                if (GuidServicio == null) {
                    DevLookUpEditTurno.Enabled = false;
                }

                var servicios = Negocio.ObtenerServicio((Guid)GuidServicio);
                var turnos = servicios.TurnosDelServicio;

                //chequear que existan servicios para la instalacion escogida
                if (turnos.Count > 0) {
                    bsTurnos.DataSource = turnos;
                    DevLookUpEditTurno.Enabled = true;
                } else {
                    Negocio.AdicionarNotificacionListadoVacio("No existen turnos para el servicio: " + servicios.NombreServicioCasino);
                    DevLookUpEditTurno.Enabled = false;
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }

        private void DevLookUpEditTurno_EditValueChanged(object sender, EventArgs e) {
            //ver si hay alguno escogido
            //ver si este ya no esta en la lista
            //SI
            //activar el boton adicionar
            //NO
            //desactivar
        }
    }
}
