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
using EnroladorAccesoDatos;

namespace EnroladorStandAloneV2.CapaInterfazUsuario.Enrolar {
    public partial class UCManejarCasino : DevExpress.XtraEditors.XtraUserControl {
        #region Atributos
        UCEnrolarContratos Padre;
        NegocioEnrolador Negocio;
        POCOEmpleado empleado;

        private POCOInstalacion instalacionSeleccionada;
        private POCOServicioCasino servicioSeleccionado;
        int cantPresionadoNuevo = 0;
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
                if (empleado == null) return;
                CargarDatos();
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        public void CargarDatos() {
            dxErrorProvider.ClearErrors();
            //setea las veces presionadas
            cantPresionadoNuevo = 0;
            //restaura los iconos
            if (cantPresionadoNuevo == 0) DevSimpleButtonNuevo.Image = Properties.Resources.additem_16x16;
            //otorga visibilidad y actividad
            DevSimpleButtonDescartar.Visible = false;
            DevSimpleButtonModificar.Enabled = false;
            DevSimpleButtonDescartar.Visible = false;
            DevSimpleButtonNuevo.Enabled = true;
            //si la cantidad de dispositivos es menor que cero solo se activa el nuevo
            if (empleado.TurnoServicioCasino.Count > 0) {
                DevGridControlTurnos.Enabled = true;
                DevLayoutControl.Enabled = false;
                DevLookUpEditCasino.Enabled = true;
            } else {
                DevGridControlTurnos.Enabled = false;
                DevLayoutControl.Enabled = true;
                DevLookUpEditCasino.Enabled = false;
            }
            bsEmpleadoTurnoServicioCasino.DataSource = empleado.TurnoServicioCasino;
            bsInstalaciones.DataSource = Negocio.ObtenerTodasInstalaciones();

            DevGridViewAsistencias.RefreshData();
        }
        private void LimpiarCampos() {
            //limpiando campos
            DevLookUpEditCasino.EditValue = String.Empty;
            DevLookUpEditServicio.EditValue = String.Empty;
            DevLookUpEditTurno.EditValue = String.Empty;

            DevLookUpEditServicio.Enabled = false;
            DevLookUpEditTurno.Enabled = false;
        }
        private bool AdicionarNuevo() {
            try {
                Guid GuidTurnoServicio = (Guid)DevLookUpEditTurno.GetColumnValue("GuidTurnoServicio");
                
                if (instalacionSeleccionada == null) {
                    dxErrorProvider.SetError(DevLookUpEditCasino, "Casino no disponible...");
                    return false;
                }
                if (servicioSeleccionado == null) {
                    dxErrorProvider.SetError(DevLookUpEditServicio, "Servicio no disponible...");
                    return false;
                }
                if (string.IsNullOrEmpty(GuidTurnoServicio.ToString())) {
                    dxErrorProvider.SetError(DevLookUpEditTurno, "Turno no disponible...");
                    return false;
                }

                //chequeo que no exista en la relacion ya
                var turno = servicioSeleccionado.TurnosDelServicio.FirstOrDefault(p => p.GuidTurnoServicio == GuidTurnoServicio);

                if (!empleado.TurnoServicioCasino.Any(p => p.GuidTurnoServicio == turno.GuidTurnoServicio)) {

                    var pocoEmpleadoTurnoServicioCasino = new POCOEmpleadoTurnoServicioCasino() {
                        GuidTurnoServicio = GuidTurnoServicio,
                        GuidEmpleado = empleado.GuidEmpleado,
                        HoraInicio = turno.HoraInicio,
                        HoraFin = turno.HoraFin,
                        Vigente = turno.Vigente,
                        NombreCasino = instalacionSeleccionada.NombreInstalacion,
                        NombreServicio = servicioSeleccionado.NombreServicioCasino,
                        NombreTurno = turno.NombreTurnoServicio,
                        EstadoObjeto = EstadoObjeto.EnMemoria
                    };

                    empleado.TurnoServicioCasino.Add(pocoEmpleadoTurnoServicioCasino);
                    return true;
                } else {
                    dxErrorProvider.SetError(DevLookUpEditCasino, "Relacion ya existente...");
                    return false;
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return false;
            }
        }
        private void DevLookUpEditInstalacion_EditValueChanged(object sender, EventArgs e) {
            try {
                var GuidInstalacion = DevLookUpEditCasino.GetColumnValue("GuidInstalacion");
                if (GuidInstalacion == null) {
                    DevLookUpEditServicio.Enabled = false;
                }
                instalacionSeleccionada = Negocio.ObtenerInstalacion((Guid)GuidInstalacion);
                var servicios = instalacionSeleccionada.ServiciosDelCasino;

                //chequear que existan servicios para la instalacion escogida
                if (servicios.Count > 0) {
                    bsServicios.DataSource = servicios;
                    DevLookUpEditServicio.Enabled = true;
                } else {
                    Negocio.AdicionarNotificacionListadoVacio("No existen servicios para la instalacion: " + instalacionSeleccionada.NombreInstalacion);
                    DevLookUpEditServicio.Enabled = false;
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        private void DevLookUpEditServicio_EditValueChanged(object sender, EventArgs e) {
            try {
                var GuidServicio = DevLookUpEditServicio.GetColumnValue("GuidServicioCasino");
                if (GuidServicio == null) {
                    DevLookUpEditTurno.Enabled = false;
                }

                servicioSeleccionado = Negocio.ObtenerServicio((Guid)GuidServicio);
                var turnos = servicioSeleccionado.TurnosDelServicio;

                //chequear que existan servicios para la instalacion escogida
                if (turnos.Count > 0) {
                    bsTurnos.DataSource = turnos;
                    DevLookUpEditTurno.Enabled = true;
                } else {
                    Negocio.AdicionarNotificacionListadoVacio("No existen turnos para el servicio: " + servicioSeleccionado.NombreServicioCasino);
                    DevLookUpEditTurno.Enabled = false;
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        private void DevLookUpEditTurno_EditValueChanged(object sender, EventArgs e) {
            DevSimpleButtonNuevo.Enabled = true;
        }
        private void DevSimpleButtonNuevo_Click(object sender, EventArgs e) {
            try {
                cantPresionadoNuevo++;

                if (cantPresionadoNuevo == 1) {
                    DevGridControlTurnos.Enabled = false;
                    DevLayoutControl.Enabled = true;
                    DevSimpleButtonDescartar.Visible = true;
                    DevSimpleButtonNuevo.Enabled = false;
                    DevLookUpEditCasino.Enabled = true;

                    LimpiarCampos();
                    DevSimpleButtonNuevo.Image = Properties.Resources.saveas_16x16;
                } else {
                    //Validar e insertar y cargo datos
                    if (!AdicionarNuevo()) {
                        cantPresionadoNuevo = 1;
                        LimpiarCampos();
                    } else {
                        DevSimpleButtonNuevo.Enabled = true;
                        DevSimpleButtonDescartar.Visible = false;
                        DevSimpleButtonNuevo.Image = Properties.Resources.additem_16x16;
                        LimpiarCampos();
                        CargarDatos();
                    }
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        private void DevSimpleButtonDescartar_Click(object sender, EventArgs e) {
            LimpiarCampos();
            CargarDatos();
        }
        #endregion

        private void DevRepositoryItemButtonEditEliminar_Click(object sender, EventArgs e) {
            //Eliminar
        }
    }
}
