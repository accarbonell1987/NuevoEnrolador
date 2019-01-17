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
        private async Task ProcesarGrid(int tipoLlenado) {
            try {
                switch (tipoLlenado) {
                    case 0: {
                            if (Negocio.lEmpleados == null) Negocio.ObtenerTodosEmpleados();
                            else {
                                if (Negocio.CantidadDeEmpleados() != Negocio.lEmpleados.Count) {
                                    Negocio.ObtenerTodosEmpleados();
                                }
                            }
                            
                            bsEmpleados.DataSource = new BindingList<POCOEmpleado>(Negocio.lEmpleados);
                        };
                        break;
                    case 1: {
                            var lEmpleadosContratosActivos = await Negocio.ObtenerListaContratosEmpleado(true);
                            bsEmpleados.DataSource = lEmpleadosContratosActivos;
                        }; break;
                    default: {
                            var lEmpleadosContratosVencidos = await Negocio.ObtenerListaContratosEmpleado(false);
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
        private async void UCGridDatos_Load(object sender, EventArgs e) {
            await ProcesarGrid(0);
            //bsEmpleados.DataSource = await GetDataSourceEmpleados();
        }
        #endregion
    }
}
