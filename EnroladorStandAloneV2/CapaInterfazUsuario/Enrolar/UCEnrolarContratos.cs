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

namespace EnroladorStandAloneV2.CapaInterfazUsuario.Enrolar {
    public partial class UCEnrolarContratos : DevExpress.XtraEditors.XtraUserControl {
        #region Atributos
        UCEnrolador Padre;
        NegocioEnrolador Negocio;
        POCOEmpleado empleado;

        int cantPresionadoNuevo = 0;
        int cantPresionadoCaducar = 0;
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
                if (empleado != null) {
                    CargarDatos();
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }

        private void CargarDatos() {
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
                DevLayoutControlDatosDelContacto.Enabled = true;
            }
            //carga los binding
            bsContratos.DataSource = empleado.Contratos;
            bsEmpresas.DataSource = Negocio.ObtenerTodasEmpresas();
        }

        private void CargarDatos(POCOContrato contrato) {
            try {
                if (contrato == null) return;

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
                var empresa = Negocio.ObtenerEmpresa((Guid)GuidEmpresa);

                if (empresa == null) return;

                bsCuentas.DataSource = empresa.Cuentas;
                bsCargos.DataSource = empresa.Cargos;

                DevLookUpEditCuenta.Enabled = true;
                DevLookUpEditCargo.Enabled = true;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        #endregion

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
                DevSimpleButtonDescartar.Visible = false;
                DevSimpleButtonNuevo.Image = Properties.Resources.additem_16x16;
                //Validar e insertar y cargo datos

                LimpiarCampos();
                CargarDatos();
            }
        }

        private void DevSimpleButtonDescartar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarDatos();
        }

        private void DevSimpleButtonModificar_Click(object sender, EventArgs e)
        {
            //chequear cambios   
            //MODIFICAR
            LimpiarCampos();
            CargarDatos();
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
                DevSimpleButtonCaducar.Image = Properties.Resources.monthview_16x16;
                DevSimpleButtonDescartar.Visible = false;
                DevLabelControlFechaFinVigencia.Visible = false;
                DevDateEditCaducar.Visible = false;
                //Validar e insertar y cargo datos
                LimpiarCampos();
                CargarDatos();
            }

            //desactivar todo
            //desactivar boton caducar
            //visible boton deshacer

            //activo el boton modificar
            //muestro el labelcaducar y el finvigencia
        }

        private void LimpiarCampos() {
            DevLabelControlFechaFinVigencia.Visible = false;
            DevDateEditCaducar.Visible = false;
            DevLookUpEditCargo.Enabled = false;
            DevLookUpEditCuenta.Enabled = false;
            //limpiando campos
            DevLookUpEditEmpresa.Text = String.Empty;
            DevLookUpEditCargo.Text = String.Empty;
            DevLookUpEditCuenta.Text = String.Empty;
            DevTextEditCodigo.Text = String.Empty;
            DevDateEditInicioVigencia.Text = String.Empty;
            DevDateEditCaducar.Text = String.Empty;

            DevCheckEditManejaAsistencia.Checked = false;
            DevCheckEditManejaCasino.Checked = false;
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
    }
}