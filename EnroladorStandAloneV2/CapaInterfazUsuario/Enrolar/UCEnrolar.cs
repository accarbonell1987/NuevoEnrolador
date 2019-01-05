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
using EnroladorAccesoDatos.Ayudantes;
using System.Reflection;

namespace EnroladorStandAloneV2.CapaInterfazUsuario.Enrolar {
    public partial class UCEnrolar : DevExpress.XtraEditors.XtraUserControl {
        #region MyRegion
        NegocioEnrolador Negocio;
        private string RUT;
        #endregion

        #region Constructor
        public UCEnrolar(NegocioEnrolador Negocio, string RUT) {
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
        private void UCEnrolar_Load(object sender, EventArgs e) {
            try {
                bsEmpleados.DataSource = Negocio.lEmpleados;
                //seleccionar el RUT en el textbox
                if (RUT != String.Empty) {
                    DevLookUpEditRUTEmpleado.Text = RUT;

                    var empleado = Negocio.ObtenerEmpleadoDeLista(RUT);

                    UCEnrolarDatosEmpleado uCEmpleados = new UCEnrolarDatosEmpleado(Negocio, empleado) {
                        Dock = DockStyle.Fill
                    };
                    UCEnrolarContratos uCContratos = new UCEnrolarContratos(Negocio, empleado) {
                        Dock = DockStyle.Fill
                    };

                    DevPanelControlDatosEmpleado.Controls.Add(uCEmpleados);
                    DevPanelControlContratos.Controls.Add(uCContratos);
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        #endregion
    }
}
