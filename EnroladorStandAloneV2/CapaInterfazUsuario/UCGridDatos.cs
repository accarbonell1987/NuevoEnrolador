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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace EnroladorStandAloneV2.CapaInterfazUsuario {
    public partial class UCGridDatos : DevExpress.XtraEditors.XtraUserControl {
        #region Atributos
        NegocioEnrolador Negocio;
        #endregion

        #region Constructor
        public UCGridDatos() {
            InitializeComponent();
        }
        public UCGridDatos(NegocioEnrolador negocio) {
            InitializeComponent();
            Negocio = negocio;
            Negocio.lEmpleados = null;

            bsEmpleados.Clear();
            DevGridViewEmpleados.RefreshData();

            Dock = DockStyle.Fill;
        }
        #endregion

        #region Metodos y Eventos
        /// <summary>
        /// Para el filtrado de la grid
        /// </summary>
        /// <param name="tipoLlenado">int tipoLlenado Contratos Activos o Vencidos</param>
        private void ProcesarGrid(int tipoLlenado) {
            try {
                switch (tipoLlenado) {
                    case 0: {
                            if (Negocio.lEmpleados == null) Negocio.ObtenerTodosEmpleadosParaGrid();
                            else {
                                if (Negocio.CantidadDeEmpleados() != Negocio.lEmpleados.Count) {
                                    Negocio.ObtenerTodosEmpleadosParaGrid();
                                }
                            }
                            
                            bsEmpleados.DataSource = new BindingList<POCOEmpleado>(Negocio.lEmpleados);
                        };
                        break;
                    case 1: {
                            var lEmpleadosContratosActivos = Negocio.ObtenerListaContratosEmpleado(true);
                            bsEmpleados.DataSource = lEmpleadosContratosActivos;
                        }; break;
                    default: {
                            var lEmpleadosContratosVencidos = Negocio.ObtenerListaContratosEmpleado(false);
                            bsEmpleados.DataSource = lEmpleadosContratosVencidos;
                        }; break;
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }

        private void DevRadioGroupFiltroContratos_EditValueChanged(object sender, EventArgs e) {
            ProcesarGrid(Convert.ToInt16(DevRadioGroupFiltroContratos.EditValue));
        }

        private void DevGridControlEmpleados_DoubleClick(object sender, EventArgs e) {
            try {
                string RUT = (string)DevGridViewEmpleados.GetFocusedRowCellValue("RUT");
                if (!string.IsNullOrEmpty(RUT)) {
                    (FindForm() as FrmPrincipal).CrearPaginaNavegacion("Enrolar", RUT);
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        private void UCGridDatos_Load(object sender, EventArgs e) {
            ProcesarGrid(0);
        }
        #endregion
    }
}
