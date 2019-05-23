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
using DevExpress.XtraEditors;
using System.Runtime.Serialization;
using System.Xml;

namespace EnroladorStandAloneV2.CapaInterfazUsuario.Enrolar {
    public partial class UCEnrolador : DevExpress.XtraEditors.XtraUserControl {
        #region Atributos
        NegocioEnrolador Negocio;
        private string RUT;
        public POCOEmpleado empleado { get; set; }

        //POCOEmpleado copiaEmpleado;
        //List<POCODispositivo> copiaDispositivos;
        //List<POCOEmpleadoTurnoServicioCasino> copiaTurnoServicios;
        //List<POCOContrato> copiaContratos;
        //List<POCOHuella> copiaHuellas;

        public UCEnrolarDatosEmpleado uCEmpleados { get; set; }
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
                if ((RUT != String.Empty) && (!RUT.Equals("Nuevo"))) {
                    DevLookUpEditRUTEmpleado.Text = RUT;
                    empleado = Negocio.ObtenerEmpleadoDeBD(RUT);
                    CargarEmpleado(empleado);
                } else {
                    DevLookUpEditRUTEmpleado.Visible = false;
                    DevTextEditRUT.Location = DevLookUpEditRUTEmpleado.Location;
                    DevTextEditRUT.Visible = true;
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        private void DevTextEditRUT_KeyPress(object sender, KeyPressEventArgs e) {
            try {
                if (e.KeyChar == (char)Keys.Enter) {
                    string rut = DevTextEditRUT.Text;
                    RUT = rut;

                    if (RUT != String.Empty) {
                        if (ValidadorRUT.Validar(RUT, out RUT)) {
                            empleado = Negocio.ObtenerEmpleadoDeBD(rut);
                            if (empleado == null) {
                                //crear nuevo empleado con el Guid y el rut
                                empleado = new POCOEmpleado {
                                    GuidEmpleado = Guid.NewGuid(),
                                    RUT = RUT
                                };
                                CargarEmpleado(empleado);
                                DevTextEditRUT.Enabled = false;
                            } else {
                                XtraMessageBox.Show(this, "Rut ya existente, intente con otro...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        } else {
                            XtraMessageBox.Show(this, "Rut incorrecto, intente con otro...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        private void DevLookUpEditRUTEmpleado_EditValueChanged(object sender, EventArgs e) {
            try {
                string rut = DevLookUpEditRUTEmpleado.Text;
                if (rut != String.Empty) {
                    var empleado = Negocio.ObtenerEmpleadoDeBD(rut);
                    if (empleado != null) {
                        RUT = rut;
                        CargarEmpleado(empleado);
                    }
                }
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
        private void CargarEmpleado(POCOEmpleado empleado) {
            try {
                DevPanelControlDatosEmpleado.Enabled = true;
                DevPanelControlContratos.Enabled = true;
                DevPanelControlCasinos.Enabled = true;
                DevPanelControlAsistencia.Enabled = true;

                uCEmpleados = new UCEnrolarDatosEmpleado(this, Negocio, empleado) {
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
        public void DescartarCambios() {
            //copiaEmpleado.Dispositivos = copiaDispositivos;
            //copiaEmpleado.Huellas = copiaHuellas;
            //copiaEmpleado.Contratos = copiaContratos;
            //copiaEmpleado.TurnoServicioCasino = copiaTurnoServicios;
        }
        #endregion
    }
}
