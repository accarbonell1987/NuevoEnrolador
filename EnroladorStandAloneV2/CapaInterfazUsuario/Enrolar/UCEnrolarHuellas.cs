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
using EnroladorAccesoDatos;

namespace EnroladorStandAloneV2.CapaInterfazUsuario.Enrolar {
    public partial class UCEnrolarHuellas : DevExpress.XtraEditors.XtraUserControl
    {
        #region Atributos
        private UCEnrolador Padre;
        private NegocioEnrolador Negocio;
        private POCOEmpleado empleado;

        #endregion

        #region Constructor
        public UCEnrolarHuellas(UCEnrolador Padre, NegocioEnrolador Negocio, POCOEmpleado empleado)
        {
            InitializeComponent();
            try
            {
                this.Padre = Padre;
                this.Negocio = Negocio;
                this.empleado = empleado;
            }
            catch (Exception eX)
            {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        #endregion

        #region Metodos y Eventos
        private void UCEnrolarHuellas_Load(object sender, EventArgs e)
        {
            var huellasDelUsuario = empleado.Huellas;
            foreach (var huella in huellasDelUsuario)
            {
                MostarImagenSegunHuellaYTipo(huella.Tipo);
            }
        }

        private void MostarImagenSegunHuellaYTipo(TipoHuella tipo) {
            try
            {
                switch (tipo)
                {
                    case TipoHuella.MENIZQ: {
                            DevPictureBoxMeñiqueIzquierdo.Image = Properties.Resources.Huella_Seteada;
                        }; break;
                    case TipoHuella.MENDER: {
                            DevPictureBoxMeñiqueDerecho.Image = Properties.Resources.Huella_Seteada;
                        }; break;
                    case TipoHuella.ANUIZQ: {
                            DevPictureBoxAnularIzquierdo.Image = Properties.Resources.Huella_Seteada;
                        }; break;
                    case TipoHuella.ANUDER: {
                            DevPictureBoxAnularDerecho.Image = Properties.Resources.Huella_Seteada;
                        }; break;
                    case TipoHuella.MEDIZQ: {
                            DevPictureBoxMedioIzquierdo.Image = Properties.Resources.Huella_Seteada;
                        }; break;
                    case TipoHuella.MEDDER: {
                            DevPictureBoxMedioDerecho.Image = Properties.Resources.Huella_Seteada;
                        }; break;
                    case TipoHuella.INDIZQ: {
                            DevPictureBoxIndiceIzquierdo.Image = Properties.Resources.Huella_Seteada;
                        }; break;
                    case TipoHuella.INDDER: {
                            DevPictureBoxIndiceDerecho.Image = Properties.Resources.Huella_Seteada;

                        }; break;
                    case TipoHuella.PULIZQ: {
                            DevPictureBoxPulgarIzquierdo.Image = Properties.Resources.Huella_Seteada;
                        }; break;
                    case TipoHuella.PULDER: {
                            DevPictureBoxPulgarDerecho.Image = Properties.Resources.Huella_Seteada;
                        }; break;
                }
            }
            catch (Exception eX)
            {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        #endregion
    }
}
