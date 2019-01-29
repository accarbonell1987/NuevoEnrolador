using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EnroladorStandAloneV2.Herramientas;
using EnroladorStandAloneV2.CapaLogicaNegocio;
using EnroladorAccesoDatos.SQLite;
using EnroladorStandAloneV2.EnroladorServicioWeb;
using EnroladorAccesoDatos.Ayudantes;
using DevExpress.XtraEditors;

namespace EnroladorStandAloneV2.CapaInterfazUsuario {
    public partial class FrmAutenticacionUsuario : DevExpress.XtraEditors.XtraForm {
        #region Atributos
        private NegocioEnrolador Negocio;
        private Usuario UsuarioLocal = null;

        private CancellationTokenSource ctsMensaje;
        #endregion

        #region Constructor
        public FrmAutenticacionUsuario(NegocioEnrolador negocio) {
            InitializeComponent();
            Negocio = negocio;
            negocio.Online = true;
        }

        public FrmAutenticacionUsuario(NegocioEnrolador negocio, bool cambiarUsuario) {
            InitializeComponent();
            Negocio = negocio;

            UsuarioLocal = negocio.ObtenerUsuarioLocal();

            if (UsuarioLocal == null) return;

            DevTextUsuario.Text = UsuarioLocal.NombreUsuario;
            DevTextUsuario.Enabled = false;

            if (cambiarUsuario) {
                DevButtonCambiarUsuario.Visible = true;
            }

            dxErrorProvider.SetIconAlignment(DevTextUsuario, ErrorIconAlignment.MiddleRight);
            dxErrorProvider.SetIconAlignment(DevTextClave, ErrorIconAlignment.MiddleRight);
        }
        #endregion

        #region Metodos y Eventos
        private async void DevButtonAceptar_Click(object sender, EventArgs e) {
            try {
                if (ctsMensaje != null) {
                    ctsMensaje.Cancel();
                    ctsMensaje = null;
                    dxErrorProvider.ClearErrors();
                }

                bool Error = false;

                string NombreUsuario = DevTextUsuario.Text;

                if (string.IsNullOrEmpty(NombreUsuario)) {
                    DevTextUsuario.Focus();
                    dxErrorProvider.SetError(DevTextUsuario, "Ingrese un usuario...");
                    Error = true;
                }

                string ClaveUsuario = DevTextClave.Text;

                if (!Error && string.IsNullOrEmpty(ClaveUsuario)) {
                    DevTextClave.Focus();
                    dxErrorProvider.SetError(DevTextClave, "Ingrese una contraseña...");
                    Error = true;
                }

                if (!Error) {
                    if (Negocio.Online) {
                        try {
                            var usuarioRetornado = await Negocio.AutenticarUsuario(NombreUsuario, ClaveUsuario);

                            if (usuarioRetornado == null) throw new Exception("Usuario NULL");

                            Negocio.UsuarioAutenticado = TransformacionDatos.DePOCOUsuarioAUsuario(usuarioRetornado);

                            if (Negocio.UsuarioAutenticado != null) {
                                DialogResult = DialogResult.OK;
                                return;
                            }

                        } catch (Exception eX) {
                            XtraMessageBox.Show("Ocurrió un problema al intentar iniciar sesión. Compruebe la conexión con el servidor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                            return;
                        }
                    } else {

                        var GuidUsuario = Guid.Parse(UsuarioLocal.GuidUsuario);

                        if (VerificaContraseñas.AreEqual(UsuarioLocal.ClaveUsuario, ClaveUsuario, GuidUsuario)) {

                            Negocio.UsuarioAutenticado = UsuarioLocal;

                            DialogResult = DialogResult.OK;
                            return;
                        } else {
                            DevTextClave.Text = "";
                            DevTextClave.Focus();
                            dxErrorProvider.SetError(DevTextClave, "Contraseña incorrecta...");
                            Error = true;
                        }
                    }
                }

                if (!Error) {
                    DevTextUsuario.Text = "";
                    DevTextClave.Text = "";
                    DevTextUsuario.Focus();
                    dxErrorProvider.SetError(DevTextUsuario, "Usuario o contraseña incorrectos...");
                    dxErrorProvider.SetError(DevTextClave, "Usuario o contraseña incorrectos...");
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }

            try {
                await Task.Delay(5000, (ctsMensaje = new CancellationTokenSource()).Token);
                if (ctsMensaje != null && !ctsMensaje.IsCancellationRequested) {
                    dxErrorProvider.ClearErrors();
                    ctsMensaje = null;
                }
            } catch (TaskCanceledException) { }
        }

        private void LogInDialog_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == '\r') {
                DevButtonAceptar_Click(this, EventArgs.Empty);
                e.Handled = true;
            }
        }

        private void DevButtonCambiarUsuario_Click(object sender, EventArgs e) {
            if (Negocio.Online) {
                Negocio.Online = false;
                DevButtonCambiarUsuario.Text = "Cambiar usuario";
                DevTextUsuario.Text = UsuarioLocal.NombreUsuario;
                DevTextUsuario.Enabled = false;
                DevTextClave.Focus();
            } else {
                if (XtraMessageBox.Show("Se conectará al servidor para iniciar sesión con otro usuario. Asegurese de que exista conexión con el servidor", "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) {
                    Negocio.Online = true;
                    DevButtonCambiarUsuario.Text = "Volver";
                    DevTextUsuario.Text = "";
                    DevTextUsuario.Enabled = true;
                    DevTextUsuario.Focus();
                }
            }
            dxErrorProvider.ClearErrors();
        }
        #endregion
    }
}
