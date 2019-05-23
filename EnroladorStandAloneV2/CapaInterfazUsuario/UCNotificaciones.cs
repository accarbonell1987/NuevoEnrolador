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
using EnroladorAccesoDatos;
using System.Resources;

namespace EnroladorStandAloneV2.CapaInterfazUsuario {
    public partial class UCNotificaciones : DevExpress.XtraEditors.XtraUserControl {
        #region Atributos
        private NegocioEnrolador Negocio;
        #endregion

        #region Constructor
        public UCNotificaciones() {
            InitializeComponent();
        }
        public UCNotificaciones(NegocioEnrolador negocio, string enunciadoNotificaciones) {
            InitializeComponent();
            Negocio = negocio;

            DevLabelControlNotificacion.Text = enunciadoNotificaciones;

            bool critica = negocio.lNotificaciones.Any(p => p.Tipo == TipoNotificacion.Critica);
            bool cuidado = negocio.lNotificaciones.Any(p => p.Tipo == TipoNotificacion.Cuidado);
            bool informativa = negocio.lNotificaciones.Any(p => p.Tipo == TipoNotificacion.Informativa);

            Bitmap imagen = null;

            if (critica)
                imagen = Properties.Resources.close_32x32;
            else if (cuidado) {
                imagen = Properties.Resources.warning_32x32;
            } else if (informativa) {
                imagen = Properties.Resources.about_32x32;
            } else {
                imagen = Properties.Resources.checkbox_32x32;
            }

            DevPanelControlWarning.ContentImage = imagen;
        }
        #endregion

        #region Metodos y Eventos
        private void DevPanelControlNotificaciones_Click(object sender, EventArgs e) {
            FrmNotificaciones frm = new FrmNotificaciones(Negocio.lNotificaciones);
            frm.ShowDialog(this);
        }

        private void DevPanelControlWarning_MouseEnter(object sender, EventArgs e) {
            DevPanelControlWarning.Cursor = Cursors.Hand;
        }
        #endregion
    }
}
