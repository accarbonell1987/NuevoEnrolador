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
using DevExpress.XtraEditors.Repository;
using EnroladorAccesoDatos;
using System.Data.SqlTypes;

namespace EnroladorStandAloneV2.CapaInterfazUsuario.Enrolar {
    public partial class UCEnrolarContratos : DevExpress.XtraEditors.XtraUserControl {
        #region Atributos
        UCEnrolador Padre;
        NegocioEnrolador Negocio;
        POCOEmpleado empleado;

        int cantPresionadoNuevo = 0;
        int cantPresionadoCaducar = 0;

        POCOContrato contratoSeleccionado;
        POCOEmpresa empresaSeleccionada;
        POCOCuenta cuentaSeleccionada;
        POCOCargo cargoSeleccionado;
        #endregion

        #region Constructor
        public UCEnrolarContratos(UCEnrolador Padre, NegocioEnrolador Negocio, POCOEmpleado empleado) {
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
                if (empleado == null) return;
                CargarDatos();
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        private void LimpiarCampos() {
            DevLabelControlFechaFinVigencia.Visible = false;
            DevDateEditCaducar.Visible = false;
            DevLookUpEditCargo.Enabled = false;
            DevLookUpEditCuenta.Enabled = false;
            //limpiando campos
            DevLookUpEditEmpresa.Text = "";
            DevLookUpEditCargo.Text = "";
            DevLookUpEditCuenta.Text = "";

            DevTextEditCodigo.EditValue = String.Empty;
            DevDateEditInicioVigencia.EditValue = String.Empty;
            DevDateEditFinVigencia.EditValue = String.Empty;
            DevDateEditCaducar.EditValue = String.Empty;

            DevCheckEditManejaAsistencia.Checked = false;
            DevCheckEditManejaCasino.Checked = false;
        }
        private void CargarDatos() {
            DevDxErrorProvider.ClearErrors();
            //setea las veces presionadas
            cantPresionadoNuevo = 0;
            cantPresionadoCaducar = 0;
            //restaura los iconos
            if (cantPresionadoNuevo == 0) DevSimpleButtonNuevo.Image = Properties.Resources.additem_16x16;
            if (cantPresionadoCaducar == 0) DevSimpleButtonCaducar.Image = Properties.Resources.monthview_16x16;
            //otorga visibilidad y actividad
            DevSimpleButtonDescartar.Visible = false;
            DevSimpleButtonModificar.Enabled = false;
            DevSimpleButtonCaducar.Enabled = false;
            DevSimpleButtonDescartar.Visible = false;
            DevSimpleButtonNuevo.Enabled = true;
            //si la cantidad de contratos es menor que cero solo se activa el nuevo
            if (empleado.Contratos.Count > 0) {
                DevGridControlContratos.Enabled = true;
                DevLayoutControlDatosDelContacto.Enabled = false;
            } else {
                DevGridControlContratos.Enabled = false;
                DevLayoutControlDatosDelContacto.Enabled = false;
            }
            //carga los binding
            bsContratos.DataSource = empleado.Contratos.Where(p => p.EstadoObjeto != EstadoObjeto.Eliminar).ToList();
            bsEmpresas.DataSource = Negocio.ObtenerTodasEmpresas();

            DevGridViewContratos.RefreshData();
        }
        private void CargarDatos(POCOContrato contrato) {
            try {
                if (contrato == null) return;

                contratoSeleccionado = contrato;

                DevGridControlContratos.Enabled = false;
                DevSimpleButtonNuevo.Enabled = false;

                DevLayoutControlDatosDelContacto.Enabled = true;
                DevSimpleButtonModificar.Enabled = true;
                DevSimpleButtonDescartar.Visible = true;
                DevSimpleButtonCaducar.Enabled = true;

                DevLookUpEditEmpresa.Text = contrato.NombreEmpresa;
                DevLookUpEditCuenta.Text = contrato.NombreCuenta;
                DevLookUpEditCargo.Text = contrato.NombreCargo;
                DevDateEditInicioVigencia.Text = contrato.InicioVigencia.ToShortDateString();

                DevDateEditFinVigencia.Text = contrato.FinVigencia != null ? contrato.FinVigencia.Value.ToString("dd-MM-yyyy") : "";
                DevTextEditCodigo.Text = contrato.CodigoContrato;

                if ((contrato.ConsideraAsistencia)||(empleado.Dispositivos.Count > 0)) DevCheckEditManejaAsistencia.Checked = true;
                if (contrato.ConsideraCasino) DevCheckEditManejaCasino.Checked = true;

                DevLookUpEditCuenta.Enabled = true;
                DevLookUpEditCargo.Enabled = true;
            }
            catch (Exception eX)
            {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        private bool Caducar(DateTime finVigencia) {
            try {
                if (contratoSeleccionado == null) return false;

                if (finVigencia <= contratoSeleccionado.InicioVigencia) {
                    string mensaje = string.Format("El fin de vigencia {0}, debe de ser mayor que el inicio de vigencia {1} del contrato", finVigencia.Date.ToShortDateString(), contratoSeleccionado.InicioVigencia.Date.ToShortDateString());

                    DevDxErrorProvider.SetError(DevDateEditCaducar, mensaje);
                    Negocio.AdicionarNotificacion(mensaje, EnroladorAccesoDatos.TipoNotificacion.Cuidado);
                    return false;
                } else {
                    contratoSeleccionado.FinVigencia = finVigencia;
                    contratoSeleccionado.Estado = EstadoContrato.Vencido;
                    contratoSeleccionado.EstadoObjeto = EstadoObjeto.Almacenar;
                    return true;
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return false;
            }
        }
        private bool CrearModificarContrato(TipoSincronizacion tipo) {
            try {
                if (empresaSeleccionada == null) {
                    DevDxErrorProvider.SetError(DevLookUpEditEmpresa, "Instalacion no disponible...");
                    return false;
                }
                if (cargoSeleccionado == null) {
                    DevDxErrorProvider.SetError(DevLookUpEditCargo, "Cargo no disponible...");
                    return false;
                }
                if (cuentaSeleccionada == null) {
                    DevDxErrorProvider.SetError(DevLookUpEditCuenta, "Cuenta no disponible...");
                    return false;
                }

                var fechaInicio = DevDateEditInicioVigencia.DateTime.Date;
                if (fechaInicio == null) {
                    DevDxErrorProvider.SetError(DevDateEditInicioVigencia, "Fecha inicio vigencia no valida o nula...");
                    return false;
                }

                var fechaFin = DateTime.Parse(DevDateEditFinVigencia.OldEditValue.ToString());
                if (fechaFin < fechaInicio) {
                    DevDxErrorProvider.SetError(DevDateEditFinVigencia, "Fecha fin menor que fecha inicio...");
                    return false;
                }

                if ((fechaFin == null) || (fechaFin == DateTime.MinValue)) fechaFin = SqlDateTime.MinValue.Value;

                var codigo = DevTextEditCodigo.OldEditValue.ToString();
                if (codigo == null || codigo == String.Empty) {
                    DevDxErrorProvider.SetError(DevTextEditCodigo, "Codigo no valido o nulo...");
                    return false;
                }

                var cantidadDispositivosAsignados = empleado.Dispositivos.Count;
                var consideraAsistencia = DevCheckEditManejaAsistencia.Checked;
                if ((consideraAsistencia) && (cantidadDispositivosAsignados == 0)) {
                    DevDxErrorProvider.SetError(DevCheckEditManejaAsistencia, "Debe de asignar al menos un dispositivo para asistencia...");
                    return false;
                }

                var cantidadTurnosAsignados = empleado.TurnoServicioCasino.Count;
                var consideraCasino = DevCheckEditManejaCasino.Checked;
                if ((consideraCasino) && (cantidadTurnosAsignados == 0)) {
                    DevDxErrorProvider.SetError(DevCheckEditManejaCasino, "Debe de asignar al menos un turno del casino...");
                    return false;
                }

                if (tipo == TipoSincronizacion.Insertar) {
                    //el unico valor representativo del contrato es el codigo
                    //chequeo que no exista en la relacion ya
                    if (!empleado.Contratos.Any(p => p.CodigoContrato == codigo)) {
                        var pocoContrato = new POCOContrato() {
                            CodigoContrato = codigo,
                            ConsideraAsistencia = consideraAsistencia,
                            ConsideraCasino = consideraCasino,
                            Estado = EstadoContrato.Activo,
                            EstadoObjeto = EstadoObjeto.Almacenar,
                            GuidCargo = cargoSeleccionado.GuidCargo,
                            NombreCargo = cargoSeleccionado.NombreCargo,
                            GuidCuenta = cuentaSeleccionada.GuidCuenta,
                            NombreCuenta = cuentaSeleccionada.NombreCuenta,
                            GuidEmpleado = empleado.GuidEmpleado,
                            GuidEmpresa = empresaSeleccionada.GuidEmpresa,
                            NombreEmpresa = empresaSeleccionada.NombreEmpresa,
                            InicioVigencia = fechaInicio,
                            FinVigencia = fechaFin
                        };

                        pocoContrato.Descripcion = "Sin descripcion";

                        if ((pocoContrato.FinVigencia == null) || (pocoContrato.FinVigencia == Convert.ToDateTime(null))) {

                            var inicioContrato = ((pocoContrato.InicioVigencia == null) || (pocoContrato.InicioVigencia == DateTime.MinValue) || (pocoContrato.InicioVigencia == new DateTime(1900, 1, 1))) ? "Contrato Permanente" : Convert.ToDateTime(pocoContrato.InicioVigencia).Date.ToShortDateString();

                            var finContrato = ((pocoContrato.FinVigencia == null) || (pocoContrato.FinVigencia == DateTime.MinValue)) ? "Contrato Permanente" : Convert.ToDateTime(pocoContrato.FinVigencia).Date.ToShortDateString();

                            var rangoContrato = (inicioContrato == finContrato) ? "Contrato Permanente" :
                                                    "(" +
                                                        pocoContrato.InicioVigencia.Date.ToShortDateString() + "-" +
                                                        finContrato +
                                                    ")";
                            pocoContrato.Descripcion = rangoContrato;
                        } else if ((pocoContrato.FinVigencia != null) && (Convert.ToDateTime(pocoContrato.FinVigencia).Date <= DateTime.Now)) {
                            pocoContrato.Estado = EstadoContrato.Vencido;
                        }

                        empleado.Contratos.Add(pocoContrato);
                        return true;
                    } else {
                        DevDxErrorProvider.SetError(DevTextEditCodigo, "Código de contrato ya existente...");
                        return false;
                    }
                } else if (tipo == TipoSincronizacion.Modificar) {
                    if (!empleado.Contratos.Where(p => (p.GuidContrato != contratoSeleccionado.GuidContrato)).Any(q => q.CodigoContrato == codigo)) {

                        contratoSeleccionado.CodigoContrato = codigo;
                        contratoSeleccionado.ConsideraAsistencia = consideraAsistencia;
                        contratoSeleccionado.ConsideraCasino = consideraCasino;
                        contratoSeleccionado.Estado = EstadoContrato.Activo;
                        contratoSeleccionado.EstadoObjeto = EstadoObjeto.Almacenar;
                        contratoSeleccionado.GuidCargo = cargoSeleccionado.GuidCargo;
                        contratoSeleccionado.NombreCargo = cargoSeleccionado.NombreCargo;
                        contratoSeleccionado.GuidCuenta = cuentaSeleccionada.GuidCuenta;
                        contratoSeleccionado.NombreCuenta = cuentaSeleccionada.NombreCuenta;
                        contratoSeleccionado.GuidEmpleado = empleado.GuidEmpleado;
                        contratoSeleccionado.GuidEmpresa = empresaSeleccionada.GuidEmpresa;
                        contratoSeleccionado.NombreEmpresa = empresaSeleccionada.NombreEmpresa;
                        contratoSeleccionado.InicioVigencia = fechaInicio;
                        contratoSeleccionado.FinVigencia = fechaFin;

                        contratoSeleccionado.Descripcion = "Sin descripcion";

                        if ((contratoSeleccionado.FinVigencia == null) || (contratoSeleccionado.FinVigencia == Convert.ToDateTime(null))) {

                            var inicioContrato = ((contratoSeleccionado.InicioVigencia == null) || (contratoSeleccionado.InicioVigencia == DateTime.MinValue) || (contratoSeleccionado.InicioVigencia == new DateTime(1900, 1, 1))) ? "Contrato Permanente" : Convert.ToDateTime(contratoSeleccionado.InicioVigencia).Date.ToShortDateString();

                            var finContrato = ((contratoSeleccionado.FinVigencia == null) || (contratoSeleccionado.FinVigencia == DateTime.MinValue)) ? "Contrato Permanente" : Convert.ToDateTime(contratoSeleccionado.FinVigencia).Date.ToShortDateString();

                            var rangoContrato = (inicioContrato == finContrato) ? "Contrato Permanente" :
                                                    "(" +
                                                        contratoSeleccionado.InicioVigencia.Date.ToShortDateString() + "-" +
                                                        finContrato +
                                                    ")";
                            contratoSeleccionado.Descripcion = rangoContrato;
                        } else if ((contratoSeleccionado.FinVigencia != null) && (Convert.ToDateTime(contratoSeleccionado.FinVigencia).Date <= DateTime.Now)) {
                            contratoSeleccionado.Estado = EstadoContrato.Vencido;
                        }
                        return true;
                    } else {
                        DevDxErrorProvider.SetError(DevTextEditCodigo, "Código de contrato ya existente...");
                        return false;
                    }
                }
                return false;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return false;
            }
        }

        private void DevCheckEditManejaAsistencia_EditValueChanged(object sender, EventArgs e) {
            if (DevCheckEditManejaAsistencia.Checked) {
                UCManejarAsistencia uC = new UCManejarAsistencia(this, Negocio, empleado);

                Padre.AdicionarUCAsistenciaOCasino(uC);
            } else {
                Padre.EliminarUCAsistencia();
            }
        }
        private void DevCheckEditManejaCasino_EditValueChanged(object sender, EventArgs e) {
            if (DevCheckEditManejaCasino.Checked) {
                UCManejarCasino uC = new UCManejarCasino(this, Negocio, empleado);

                Padre.AdicionarUCAsistenciaOCasino(uC);
            } else {
                Padre.EliminarUCCasino();
            }
        }
        private void DevLookUpEditEmpresa_EditValueChanged(object sender, EventArgs e) {
            try {
                var GuidEmpresa = DevLookUpEditEmpresa.GetColumnValue("GuidEmpresa");
                if (GuidEmpresa == null) {
                    DevLookUpEditCuenta.Enabled = false;
                    DevLookUpEditCargo.Enabled = false;
                }
                empresaSeleccionada = Negocio.ObtenerEmpresa((Guid)GuidEmpresa);

                if (empresaSeleccionada == null) return;

                bsCuentas.DataSource = empresaSeleccionada.Cuentas;
                bsCargos.DataSource = empresaSeleccionada.Cargos;

                DevLookUpEditCuenta.Enabled = true;
                DevLookUpEditCargo.Enabled = true;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        private void DevLookUpEditCuenta_EditValueChanged(object sender, EventArgs e) {
            try {
                Guid GuidCuenta = (Guid)DevLookUpEditCuenta.GetColumnValue("GuidCuenta");
                if (string.IsNullOrEmpty(GuidCuenta.ToString())) {
                    DevDxErrorProvider.SetError(DevLookUpEditCuenta, "Cuenta no disponible...");
                    return;
                }
                if (empresaSeleccionada == null) {
                    DevDxErrorProvider.SetError(DevLookUpEditEmpresa, "Empresa no disponible...");
                    return;
                }

                cuentaSeleccionada = empresaSeleccionada.Cuentas.FirstOrDefault(p => p.GuidCuenta == GuidCuenta);
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        private void DevLookUpEditCargo_EditValueChanged(object sender, EventArgs e) {
            try {
                Guid GuidCargo = (Guid)DevLookUpEditCargo.GetColumnValue("GuidCargo");
                if (string.IsNullOrEmpty(GuidCargo.ToString())) {
                    DevDxErrorProvider.SetError(DevLookUpEditCargo, "Cargo no disponible...");
                    return;
                }
                if (empresaSeleccionada == null) {
                    DevDxErrorProvider.SetError(DevLookUpEditEmpresa, "Empresa no disponible...");
                    return;
                }

                cargoSeleccionado = empresaSeleccionada.Cargos.FirstOrDefault(p => p.GuidCargo == GuidCargo);
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        private void DevSimpleButtonNuevo_Click(object sender, EventArgs e)
        {
            cantPresionadoNuevo++;

            if (cantPresionadoNuevo == 1) {
                DevGridControlContratos.Enabled = false;
                DevLayoutControlDatosDelContacto.Enabled = true;
                DevSimpleButtonDescartar.Visible = true;

                DevLookUpEditCargo.Enabled = false;
                DevLookUpEditCuenta.Enabled = false;
                LimpiarCampos();
                DevSimpleButtonNuevo.Image = Properties.Resources.saveas_16x16;
            } else {

                //Validar e insertar y cargo datos
                if (!CrearModificarContrato(TipoSincronizacion.Insertar)) {
                    cantPresionadoNuevo = 1;
                } else {
                    DevSimpleButtonNuevo.Enabled = true;
                    DevSimpleButtonDescartar.Visible = false;
                    DevSimpleButtonNuevo.Image = Properties.Resources.additem_16x16;
                    LimpiarCampos();
                    CargarDatos();
                }
            }
        }
        private void DevSimpleButtonDescartar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarDatos();
        }
        private void DevSimpleButtonModificar_Click(object sender, EventArgs e)
        {
            try {
                Guid GuidContrato = (Guid)DevGridViewContratos.GetFocusedRowCellValue("GuidContrato");
                if (!string.IsNullOrEmpty(GuidContrato.ToString())) {
                    var contrato = empleado.Contratos.FirstOrDefault(p => p.GuidContrato == GuidContrato);
                    CargarDatos(contrato);
                    if (CrearModificarContrato(TipoSincronizacion.Modificar))
                        CargarDatos();
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        private void DevSimpleButtonCaducar_Click(object sender, EventArgs e)
        {
            cantPresionadoCaducar++;

            if (cantPresionadoCaducar == 1) {
                DevLayoutControlDatosDelContacto.Enabled = false;
                DevSimpleButtonDescartar.Visible = true;
                DevSimpleButtonModificar.Enabled = false;
                DevLabelControlFechaFinVigencia.Visible = true;
                DevDateEditCaducar.Visible = true;
                DevSimpleButtonCaducar.Image = Properties.Resources.apply_16x161;
                DevCheckEditManejaAsistencia.Checked = false;
                DevCheckEditManejaCasino.Checked = false;
            } else {
                if (Caducar(DevDateEditCaducar.DateTime.Date)) {
                    DevSimpleButtonCaducar.Image = Properties.Resources.monthview_16x16;
                    DevSimpleButtonDescartar.Visible = false;
                    DevLabelControlFechaFinVigencia.Visible = false;
                    DevDateEditCaducar.Visible = false;

                    LimpiarCampos();
                    CargarDatos();
                } else {
                    cantPresionadoCaducar = 1;
                    DevDateEditCaducar.Text = String.Empty;
                }
            }
        }
        private void DevGridControlContratos_DoubleClick(object sender, EventArgs e)
        {
            try {
                Guid GuidContrato = (Guid)DevGridViewContratos.GetFocusedRowCellValue("GuidContrato");
                if (!string.IsNullOrEmpty(GuidContrato.ToString())) {
                    var contrato = empleado.Contratos.FirstOrDefault(p => p.GuidContrato == GuidContrato);
                    CargarDatos(contrato);
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        private void DevRepositoryItemButtonEditEliminar_Click(object sender, EventArgs e) {
            try {
                Guid GuidContrato = (Guid)DevGridViewContratos.GetFocusedRowCellValue("GuidContrato");
                if (!string.IsNullOrEmpty(GuidContrato.ToString())) {
                    var contrato = empleado.Contratos.First(p => p.GuidContrato == GuidContrato);
                    if (contrato.EstadoObjeto != EstadoObjeto.Almacenado) {
                        contrato.EstadoObjeto = EstadoObjeto.Eliminar;
                        CargarDatos();
                    }
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        #endregion
    }
}