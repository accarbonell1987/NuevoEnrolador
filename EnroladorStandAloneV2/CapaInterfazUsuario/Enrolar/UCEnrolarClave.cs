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

namespace EnroladorStandAloneV2.CapaInterfazUsuario.Enrolar {
    public partial class UCEnrolarClave : DevExpress.XtraEditors.XtraUserControl {
        #region Atributos
        private UCEnrolar Padre;
        private NegocioEnrolador Negocio;
        private POCOEmpleado empleado;

        private Timer temporizador;
        #endregion

        #region Constructor
        public UCEnrolarClave(UCEnrolar Padre, NegocioEnrolador Negocio, POCOEmpleado empleado) {
            InitializeComponent();
            //temporizador para el chequeo de la revalidacion de contraseña
            temporizador = new Timer {
                Interval = 2000
            };
            temporizador.Tick += Temporizador_Tick;

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
        private void Temporizador_Tick(object sender, EventArgs e) {
            if (!DevTextClave.Text.Equals(DevTextEditRevalidarClave.Text)) {
                dxErrorProvider.SetError(DevTextEditRevalidarClave, "Contraseñas diferentes");
            }
            temporizador.Stop();
        }
        #endregion

        private void DevTextClave_EditValueChanged(object sender, EventArgs e) {
            if (DevTextClave.Text != String.Empty)
                DevTextEditRevalidarClave.Enabled = true;
            else
                DevTextEditRevalidarClave.Enabled = false;
        }

        private void DevTextEditRevalidarClave_Validated(object sender, EventArgs e) {
            temporizador.Stop();
            temporizador.Start();
        }
    }
}
