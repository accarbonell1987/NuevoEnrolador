using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EnroladorAccesoDatos;
using EnroladorStandAloneV2.Herramientas.Huellero;

namespace EnroladorStandAloneV2.CapaInterfazUsuario {
    public partial class FrmAutorizacion : Form {
        #region Atributos
        private const int MAX_INTENTOS_FALLIDOS = 10;

        private static int intentosFallidos = 0;
        private Huellero huellero;
        #endregion

        #region Constructor
        public FrmAutorizacion() {
            InitializeComponent();
        }

        public FrmAutorizacion(Huellero huellero, string mensaje) {
            InitializeComponent();
            this.huellero = huellero;
            huellero.Rejected += Huellero_Rejected;
            huellero.Validated += Huellero_Validated;

            huellero.Habilitar(true);
            DevLabelControlMensaje.Text = mensaje;

            RefrescarLabelIntentos();
        }
        #endregion

        #region Metodos y Eventos
        private void DevButtonCancelar_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
        }

        private void FrmAutorizacion_FormClosing(object sender, FormClosingEventArgs e) {
            huellero.Rejected -= Huellero_Rejected;
            huellero.Validated -= Huellero_Validated;
            huellero.Habilitar(false);
        }

        private async void Huellero_Validated(object sender, System.EventArgs e) {
            await huellero.Sonido(HuelleroSonidos.Correcto);
            intentosFallidos = 0;
            DialogResult = DialogResult.Yes;
        }

        private async void Huellero_Rejected(object sender, System.EventArgs e) {
            await huellero.Sonido(HuelleroSonidos.Incorrecto);
            intentosFallidos++;
            RefrescarLabelIntentos();
        }

        private void RefrescarLabelIntentos() {
            if (intentosFallidos >= MAX_INTENTOS_FALLIDOS - 3) {
                int rem = MAX_INTENTOS_FALLIDOS - intentosFallidos;
                if (rem <= 0) {
                    DialogResult = DialogResult.No;
                    return;
                } else {
                    DevLabelControlIntentos.Visible = true;
                    DevLabelControlIntentos.Text = string.Format("Queda{0} {1} intento{2}", rem > 1 ? "n" : "", rem, rem > 1 ? "s" : "");
                }
            }
        }
        #endregion

        
    }
}
