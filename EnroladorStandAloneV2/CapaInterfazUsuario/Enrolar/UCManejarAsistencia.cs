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

        private POCOInstalacion instalacionSeleccionada;
        int cantPresionadoNuevo = 0;
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
            if (empleado.Dispositivos.Count > 0) {
                DevGridControlAsistencias.Enabled = true;
                DevLayoutControl.Enabled = false;
                DevLookUpEditInstalacion.Enabled = true;
            } else {
                DevGridControlAsistencias.Enabled = false;
                DevLayoutControl.Enabled = true;
                DevLookUpEditInstalacion.Enabled = false;
            }
            bsEmpleadoDispositivos.DataSource = empleado.Dispositivos;
            bsInstalaciones.DataSource = Negocio.ObtenerTodasInstalaciones();

            DevGridViewAsistencias.RefreshData();
        }
        private void LimpiarCampos() {
            //limpiando campos
            DevLookUpEditInstalacion.EditValue = String.Empty;
            DevLookUpEditDispositivo.EditValue = String.Empty;

            DevLookUpEditDispositivo.Enabled = false;
        }
        private bool AdicionarNuevo() {
            try {
                Guid GuidDispositivo = (Guid)DevLookUpEditDispositivo.GetColumnValue("GuidDispositivo");
                if (string.IsNullOrEmpty(GuidDispositivo.ToString())) {
                    dxErrorProvider.SetError(DevLookUpEditDispositivo, "Dispositivo no disponible...");
                    return false;
                }
                if (instalacionSeleccionada == null) {
                    dxErrorProvider.SetError(DevLookUpEditInstalacion, "Instalacion no disponible...");
                    return false;
                }
                //chequeo que no exista en la relacion ya

                var dispositivo = instalacionSeleccionada.Dispositivos.FirstOrDefault(p => p.GuidDispositivo == GuidDispositivo);
                dispositivo.NombreCadena = instalacionSeleccionada.NombreCadena;
                dispositivo.NombreInstalacion = instalacionSeleccionada.NombreInstalacion;

                if (!empleado.Dispositivos.Any(p => p.GuidDispositivo == dispositivo.GuidDispositivo)) {
                    Negocio.AdicionarEmpleadoInstalacionDispostivoSinSalvar(new POCOEmpleadoDispositivo() {
                        GuidDispositivo = dispositivo.GuidDispositivo,
                        GuidEmpleado = empleado.GuidEmpleado
                    });
                    empleado.Dispositivos.Add(dispositivo);
                    return true;
                } else {
                    dxErrorProvider.SetError(DevLookUpEditInstalacion, "Relacion ya existente...");
                    return false;
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return false;
            }
        }

        private void DevLookUpEditInstalacion_EditValueChanged(object sender, EventArgs e) {
            try {
                var GuidInstalacion = DevLookUpEditInstalacion.GetColumnValue("GuidInstalacion");
                if (GuidInstalacion == null) {
                    DevLookUpEditDispositivo.Enabled = false;
                }

                instalacionSeleccionada = Negocio.ObtenerInstalacion((Guid)GuidInstalacion);
                var dispositivos = instalacionSeleccionada.Dispositivos;

                //chequear que existan servicios para la instalacion escogida
                if (dispositivos.Count > 0) {
                    bsDispositivos.DataSource = dispositivos;
                    DevLookUpEditDispositivo.Enabled = true;
                } else {
                    Negocio.AdicionarNotificacionListadoVacio("No existen dispositivos para la instalacion: " + instalacionSeleccionada.NombreInstalacion);
                    DevLookUpEditDispositivo.Enabled = false;
                }
                DevLookUpEditDispositivo.Enabled = true;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        private void DevSimpleButtonDescartar_Click(object sender, EventArgs e)
        {
            DevSimpleButtonNuevo.Enabled = true;
            LimpiarCampos();
            CargarDatos();
        }
        private void DevSimpleButtonNuevo_Click(object sender, EventArgs e)
        {
            try {
                cantPresionadoNuevo++;

                if (cantPresionadoNuevo == 1) {
                    DevGridControlAsistencias.Enabled = false;
                    DevLayoutControl.Enabled = true;
                    DevSimpleButtonDescartar.Visible = true;
                    DevSimpleButtonNuevo.Enabled = false;
                    DevLookUpEditInstalacion.Enabled = true;

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
        private void DevLookUpEditDispositivo_EditValueChanged(object sender, EventArgs e) {
            DevSimpleButtonNuevo.Enabled = true;
        }
        #endregion


    }
}
