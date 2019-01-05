using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EnroladorStandAloneV2.CapaInterfazUsuario.Interfaces;

namespace EnroladorStandAloneV2.CapaInterfazUsuario {
    public partial class UCCargarDatos : DevExpress.XtraEditors.XtraUserControl, ICargaDatos {
        #region Atributos
        private string nombrePaso;
        private int numeroPaso;
        private int totalPasos;
        #endregion

        #region Constructor
        public UCCargarDatos() {
            InitializeComponent();
            DevLabelControlTotal.Text = "";
            DevLabelControlActual.Text = "";
        }
        #endregion

        #region Metodos
        public void PrimerPaso(int totalPasos, int totalPrimero, string nombrePrimero) {
            DevProgressBarControlTotal.Properties.Minimum = 0;
            DevProgressBarControlTotal.Properties.Maximum = (totalPasos - 1) * 10;
            DevProgressBarControlTotal.Position = 0;
            DevProgressBarControlTotal.Properties.Step = 1;

            DevProgressBarControlActual.Properties.Minimum = 0;
            DevProgressBarControlActual.Properties.Maximum = totalPrimero < 1 ? 1 : totalPrimero;
            DevProgressBarControlActual.Position = 0;
            DevProgressBarControlActual.Properties.Step = 1;

            this.totalPasos = totalPasos;
            nombrePaso = nombrePrimero;
            numeroPaso = 1;
            ActualizaLabels();
        }

        public void SiguientePaso(int totalActual, string nombrePaso) {
            numeroPaso++;

            DevProgressBarControlTotal.Position = (numeroPaso - 1) * 10;
            DevProgressBarControlActual.Position = 0;
            DevProgressBarControlActual.Properties.Maximum = totalActual < 1 ? 1 : totalActual;
            this.nombrePaso = nombrePaso;

            ActualizaLabels();
        }

        public void AvanzarActual() {
            //Hay que revisar que está dando problemas y no muestra lo que es
            DevProgressBarControlActual.PerformStep();

            var value = ((numeroPaso - 1) * 10) + (10 * DevProgressBarControlActual.Position / DevProgressBarControlActual.Properties.Maximum);
            DevProgressBarControlTotal.Position = value > 100 ? 100 : value;

            ActualizaLabels();
        }

        private void ActualizaLabels() {
            //DevLabelControlTotal.Text = string.Format("Paso {0} de {1}: {2}", numeroPaso, DevProgressBarControlTotal.Properties.Maximum / 10, nombrePaso);
            DevLabelControlTotal.Text = string.Format("Paso {0} de {1}: {2}", numeroPaso, totalPasos, nombrePaso);
            DevLabelControlActual.Text = ("Cargando: " + 100 * DevProgressBarControlActual.Position / DevProgressBarControlActual.Properties.Maximum).ToString() + "%";
        }
        #endregion
    }
}
