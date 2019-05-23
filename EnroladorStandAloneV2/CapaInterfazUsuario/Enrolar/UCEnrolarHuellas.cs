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
using EnroladorStandAloneV2.Herramientas.Huellero;
using DevExpress.Utils;

namespace EnroladorStandAloneV2.CapaInterfazUsuario.Enrolar {
    public partial class UCEnrolarHuellas : DevExpress.XtraEditors.XtraUserControl
    {
        #region Atributos
        private UCEnrolador Padre;
        private NegocioEnrolador Negocio;
        private POCOEmpleado empleado;

        private bool Enrolando = false;
        private int Dedos = 0;
        private TipoHuella Tipo;
        private string Data;
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
            if (empleado == null) return;

            var huellasDelUsuario = empleado.Huellas;
            foreach (var huella in huellasDelUsuario)
                MostarImagenSegunHuellaYTipo(huella.Tipo);
        }
        private void MostarImagenSegunHuellaYTipo(TipoHuella tipo) {
            try
            {
                switch (tipo)
                {
                    case TipoHuella.MENIZQ: {
                            DevPictureBoxMeñiqueIzquierdo.Image = Properties.Resources.OK16x16;
                        }; break;
                    case TipoHuella.MENDER: {
                            DevPictureBoxMeñiqueDerecho.Image = Properties.Resources.OK16x16;
                        }; break;
                    case TipoHuella.ANUIZQ: {
                            DevPictureBoxAnularIzquierdo.Image = Properties.Resources.OK16x16;
                        }; break;
                    case TipoHuella.ANUDER: {
                            DevPictureBoxAnularDerecho.Image = Properties.Resources.OK16x16;
                        }; break;
                    case TipoHuella.MEDIZQ: {
                            DevPictureBoxMedioIzquierdo.Image = Properties.Resources.OK16x16;
                        }; break;
                    case TipoHuella.MEDDER: {
                            DevPictureBoxMedioDerecho.Image = Properties.Resources.OK16x16;
                        }; break;
                    case TipoHuella.INDIZQ: {
                            DevPictureBoxIndiceIzquierdo.Image = Properties.Resources.OK16x16;
                        }; break;
                    case TipoHuella.INDDER: {
                            DevPictureBoxIndiceDerecho.Image = Properties.Resources.OK16x16;

                        }; break;
                    case TipoHuella.PULIZQ: {
                            DevPictureBoxPulgarIzquierdo.Image = Properties.Resources.OK16x16;
                        }; break;
                    case TipoHuella.PULDER: {
                            DevPictureBoxPulgarDerecho.Image = Properties.Resources.OK16x16;
                        }; break;
                }
            }
            catch (Exception eX)
            {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        private async void Huellero_FingerFeature(object sender, FingerFeatureEventArgs e) {
            try {
                if (Enrolando) {
                    Dedos = e.Count;

                    switch (Dedos) {
                        case 1: DevPictureBoxPrimera.Image = Properties.Resources.OK32x32; break;
                        case 2: DevPictureBoxSegunda.Image = Properties.Resources.OK32x32; break;
                        case 3: DevPictureBoxTercera.Image = Properties.Resources.OK32x32; break;
                    }

                    ActualizaStatusHuellas();
                    await Negocio.mHuellero.Sonido(HuelleroSonidos.Correcto);
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        private async void Huellero_Enrolled(object sender, EnrolledEventArgs e) {
            try {
                if (Enrolando) {
                    if (e.Success) {
                        DevLabelControlCantHuellasSelecionada.Text = "Enrolado con éxito";
                        Data = e.Template;
                        AlmacenarHuella(Data, Tipo);
                        flyoutPanel.HideBeakForm();
                    } else {
                        DevLabelControlCantHuellasSelecionada.Text = "Error al enrolar. Intente nuevamente...";
                        await Negocio.mHuellero.Sonido(HuelleroSonidos.Incorrecto);
                    }
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        private void ActualizaStatusHuellas() {
            DevLabelControlCantHuellasSelecionada.Text = string.Format("Se han leido {0} huellas", Dedos);
        }
        private void AlmacenarHuella(string data, TipoHuella tipo) {
            try {

                POCOHuella nHuella = new POCOHuella() {
                    GuidEmpleado = empleado.GuidEmpleado,
                    Tipo = tipo,
                    Data = data,
                    EstadoObjeto = EstadoObjeto.Almacenar
                };

                var huellasDelUsuario = empleado.Huellas;
                if (huellasDelUsuario.Count > 0) {
                    var huella = huellasDelUsuario.FirstOrDefault(p => p.Tipo == tipo);
                    if (huella == null) {
                        huella = nHuella;
                    } else {
                        huella.Data = data;
                    }
                } else {
                    huellasDelUsuario.Add(nHuella);
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        //private void wpMostrarHuellas_PageValidating(object sender, WizardPageValidatingEventArgs e) {
        //    if (nextPage == wpNuevaHuella) {
        //        if (gridView2.SelectedRowsCount == 0) {
        //            nextPage = null;
        //            e.ErrorText = "Debe seleccionar una huella para registrar";
        //            e.ErrorIconType = MessageBoxIcon.Hand;
        //            e.Valid = false;
        //            return;
        //        }
        //    } else if (e.Direction == Direction.Forward && bnlHuellas.Count<Tuple<string, bool, TipoHuella, Guid?>>((huella) => { return huella.Item2; }) == 0) {
        //        e.ErrorText = "Debe registrar al menos una huella";
        //        e.ErrorIconType = MessageBoxIcon.Hand;
        //        e.Valid = false;
        //        return;
        //    }
        //    e.Valid = true;
        //}

        //private void wpNuevaHuella_PageCommit(object sender, EventArgs e) {
        //    if (parent.EmpleadoRUTIndex.ContainsKey(RUT) && parent.EmpleadoTable.ContainsKey(parent.EmpleadoRUTIndex[RUT])) {
        //        if (actualizarHuella.HasValue) {
        //            Accion accion = new AccionActualizarHuella(actualizarHuella.Value, parent.EmpleadoRUTIndex[RUT], enrolledFingerData, parent);
        //            acciones.Add(accion);
        //            accionesActuales.Push(new Tuple<Accion, TipoAccion>(accion, TipoAccion.Nueva));
        //        } else {
        //            Accion accion = new AccionCrearHuella(parent.EmpleadoRUTIndex[RUT], enrolledFingerType, enrolledFingerData, parent);
        //            acciones.Add(accion);
        //            accionesActuales.Push(new Tuple<Accion, TipoAccion>(accion, TipoAccion.Nueva));
        //        }
        //    } else {
        //        // TODO: Error
        //    }
        //}
        private void DevPictureBoxMeñiqueIzquierdo_Click(object sender, EventArgs e) {
            EnsureShowBeakForm(TipoHuella.MENIZQ, sender);
        }
        private void DevPictureBoxAnularIzquierdo_Click(object sender, EventArgs e) {
            EnsureShowBeakForm(TipoHuella.ANUIZQ, sender);
        }
        private void DevPictureBoxMedioIzquierdo_Click(object sender, EventArgs e) {
            EnsureShowBeakForm(TipoHuella.MEDIZQ, sender);
        }
        private void DevPictureBoxIndiceIzquierdo_Click(object sender, EventArgs e) {
            EnsureShowBeakForm(TipoHuella.INDIZQ, sender);
        }
        private void DevPictureBoxPulgarIzquierdo_Click(object sender, EventArgs e) {
            EnsureShowBeakForm(TipoHuella.PULIZQ, sender);
        }
        private void DevPictureBoxPulgarDerecho_Click(object sender, EventArgs e) {
            EnsureShowBeakForm(TipoHuella.PULDER, sender);
        }
        private void DevPictureBoxIndiceDerecho_Click(object sender, EventArgs e) {
            EnsureShowBeakForm(TipoHuella.INDDER, sender);
        }
        private void DevPictureBoxMedioDerecho_Click(object sender, EventArgs e) {
            EnsureShowBeakForm(TipoHuella.MEDDER, sender);
        }
        private void DevPictureBoxAnularDerecho_Click(object sender, EventArgs e) {
            EnsureShowBeakForm(TipoHuella.ANUDER, sender);
        }
        private void DevPictureBoxMeñiqueDerecho_Click(object sender, EventArgs e) {
            EnsureShowBeakForm(TipoHuella.INDDER, sender);
        }
        private void EstablecerEstadoHuellero(EstadoHuellero estado) {
            if (Enrolando)

            if (estado == EstadoHuellero.Habilitado) {
                Negocio.mHuellero.FingerFeature += Huellero_FingerFeature;
                Negocio.mHuellero.Enrolled += Huellero_Enrolled;
            } else {
                Negocio.mHuellero.FingerFeature -= Huellero_FingerFeature;
                Negocio.mHuellero.Enrolled -= Huellero_Enrolled;
            }
        }
        #endregion

        #region Flyout Control
        void EnsureShowBeakForm(TipoHuella tipo, object sender) {
            try {
                if (flyoutPanel.FlyoutPanelState.IsActive) return;

                Enrolando = true;
                Tipo = tipo;

                //Negocio.mHuellero.Enroll();

                flyoutPanel.ShowBeakForm(GetHotPoint(sender));
                //EstablecerEstadoHuellero(EstadoHuellero.Habilitado);
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        Point GetHotPoint(object sender) {
            Point pt = new Point(0, (sender as PictureBox).Height / 2);
            BeakPanelBeakLocation edtiValue = BeakPanelBeakLocation.Top;
            if (edtiValue == BeakPanelBeakLocation.Right) {
                return (sender as PictureBox).PointToScreen(pt);
            }
            if (edtiValue == BeakPanelBeakLocation.Left) {
                pt.X += (sender as PictureBox).Width;
                return (sender as PictureBox).PointToScreen(pt);
            }
            pt = new Point(this.DevPictureBoxMeñiqueIzquierdo.Width / 2, 0);
            if (edtiValue == BeakPanelBeakLocation.Top) {
                pt.Y += (sender as PictureBox).Height;
            }
            return (sender as PictureBox).PointToScreen(pt);
        }
        void OnFlyoutPanelButtonClick(object sender, FlyoutPanelButtonClickEventArgs e) {
            string tag = e.Button.Tag as string;
            if (string.Equals(tag, "Exit", StringComparison.OrdinalIgnoreCase)) {
                flyoutPanel.HideBeakForm();

                Enrolando = false;
                Negocio.mHuellero.CancelarEnrolamiento();

                EstablecerEstadoHuellero(EstadoHuellero.Inhabilitado);
            }
        }
        #endregion
    }
}
