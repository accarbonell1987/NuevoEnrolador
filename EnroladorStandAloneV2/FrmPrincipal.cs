using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using EnroladorAccesoDatos.SQLite;
using EnroladorStandAloneV2.CapaInterfazUsuario;
using EnroladorStandAloneV2.CapaLogicaNegocio;
using EnroladorStandAloneV2.Herramientas;
using EnroladorStandAloneV2.Herramientas.Huellero;
using EnroladorAccesoDatos.Ayudantes;
using EnroladorAccesoDatos;
using EnroladorAccesoDatos.Dominio;
using System.Threading;
using EnroladorStandAloneV2.CapaInterfazUsuario.Interfaces;
using System.Transactions;
using DevExpress.XtraBars.Navigation;
using EnroladorStandAloneV2.CapaInterfazUsuario.Enrolar;

namespace EnroladorStandAloneV2 {
    public partial class FrmPrincipal : DevExpress.XtraBars.Ribbon.RibbonForm {
        #region Atributos
        private NegocioEnrolador Negocio;
        private FrmSplashScreen frmSplashScreen;
        #endregion

        #region Constructor
        public FrmPrincipal() {
            InitializeComponent();
            Negocio = new NegocioEnrolador();

            //mostar splash
            frmSplashScreen = new FrmSplashScreen();
            frmSplashScreen.Show(this);

            //inicializar el hilo de chequeo de notificaciones
            backgroundWorkerChequeoNotificaciones.RunWorkerAsync();
        }
        #endregion

        #region Eventos Form
        private void BarButtonItemSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //Chequear:
            //1. Actualizando?
            //2. Cargando Datos?
            //3. Acciones por enviar?
            // Mostrar Mensaje "¿Hay acciones que no han sido enviadas. Enviar ahora?"

            //Destruir Huellero

            //if (!actualizando && dataLoaded && accionesPorEnviar.Count() > 0) {
            //    DialogResult res = MessageBox.Show("Hay acciones que no han sido enviadas. Enviar ahora?", "Acciones no enviadas", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
            //    if (res != DialogResult.No) {
            //        e.Cancel = true;
            //        if (res == DialogResult.Yes) {
            //            recargar_ItemClick(this, null);
            //        }
            //        return;
            //    }
            //}
            //if (huellero != null) {
            //    huellero.Dispose();
            //    huellero = null;
            //}
        }

        private async void FrmPrincipal_Load(object sender, EventArgs e) {
            DevGroupControlNotificacionesAcciones.Controls.Clear();
            DevGroupControlNotificacionesAcciones.Controls.Add(Negocio.ObtenerUCNotificaciones("Inicializando el sistema, espere por favor..."));

            DesactivarSistema();
            await InicializarSistema();
            ActivarSistema();
        }
        #endregion

        #region Metodos
        #region Sistema
        /// <summary>
        /// Inicializa el sistema
        /// </summary>
        /// <returns></returns>
        public async Task InicializarSistema() {
            if (BuscarActualizaciones()) {
                return;
            }

            Negocio.MyHardwareId = HardwareID.GetBaseHardwareFingerPrint();
#if DEBUG
            Negocio.MyHardwareId = new Guid("f6a74090-e0d1-daf9-e8c4-7d7497f4c9ad");
#endif
            //mostar form y esconder Splash
            frmSplashScreen.Hide();

            if (!Negocio.IdentificarLocal()) {
                if (XtraMessageBox.Show("No existe autorización almacenada para este equipo. Se conectará al servidor para solicitar autorización. Asegurese de que exista conexión con el servidor", "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel) {
                    Application.Exit();
                    return;
                }

                Negocio.Online = true;

                while (!(await Negocio.IdentificarHardwareOnline())) {
                    if (XtraMessageBox.Show("No se ha podido obtener autorización para este equipo. Compruebe la conexión con el servidor", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Hand) == DialogResult.Cancel) {
                        Application.Exit();
                        return;
                    }
                }
            }

            if (Negocio.Online) {
                bool autenticado = await AutenticarseOnline();
                if (!autenticado) {
                    Application.Exit();
                    return;
                }
            } else {
                // Login offline puede convertirse en online
                bool autenticado = await AutenticacionOffline();
                if (!autenticado) {
                    Application.Exit();
                    return;
                }
            }

#if DEBUG
            //ACCM da un error en el ActiveX
            //Negocio.mHuellero = new Huellero();
#else
            while (!(await Task<bool>.Factory.StartNew(() =>
            {
                //return huellero.Connect(huellaUserTable);
                return Negocio.mHuellero.Connect(Negocio.HuellasUsuario);
            })))
            {
                if (MessageBox.Show("No se ha podido conectar con el huellero. Compruebe la conexión del dispositivo", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Hand) == DialogResult.Cancel)
                {
                    Application.Exit();
                    return;
                }
            }
#endif

            if (Negocio.Online || !DatosOffline()) {
                //Pedir autorizacion para cargar data online si no hay datos Locales

                if (await PedirAutorizacion("Por favor autorice con su huella la descarga de datos a este equipo", this) != ResultadoAutorizacion.Aceptado) {
                    XtraMessageBox.Show("Cerrando aplicación. No se autorizó la descarga de datos", "No autorizado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    return;
                }


                Negocio.Online = true;
                while (!(await DatosOnline())) {
                    if (XtraMessageBox.Show("No se han podido descargar los datos. Compruebe la conexión con el servidor", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Hand) == DialogResult.Cancel) {
                        Application.Exit();
                        return;
                    }
                }
            }

            int diasDesdeUltimaConexion = (int)DateTime.Now.Subtract(Negocio.UltimaConexion).TotalDays;

            if (!Negocio.Online && diasDesdeUltimaConexion > NegocioEnrolador.DiasRecomendandosParaConectar) {
                if (XtraMessageBox.Show(string.Format("Han pasado {0} días desde la última sincronización. Desea sincronizar ahora?", diasDesdeUltimaConexion), "Sincronizar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                    //await Sincronizar();
                    return;
                }
            }

            Negocio.AdicionarNotificacionProcesoRealizadoCorrectamente("Inicializar Sistema");

            CargarGridEmpleados();

            //mostrar las notificaciones
        }

        private void DesactivarSistema() {
            DevRibbonControl.Enabled = false;
        }
        private void ActivarSistema() {
            DevRibbonControl.Enabled = true;
        }
        #endregion

        #region Cargar Datos Locales
        /// <summary>
        /// Autenticacion Offline
        /// </summary>
        /// <returns>Task<bool></returns>
        private async Task<bool> AutenticacionOffline() {
            try {
                if (!Negocio.ExisteDatosAutenticacionUsuario()) {
                    if (XtraMessageBox.Show("No existe información almacenada para iniciar sesión. Se conectará al servidor para iniciar sesión. Asegurese de que exista conexión con el servidor", "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel) {
                        return false;
                    }
                    bool autenticado = await AutenticarseOnline();
                    return autenticado;
                }

                //Leer Usuario de la BD Local y lanzar el Autenticar Local
                FrmAutenticacionUsuario frm = new FrmAutenticacionUsuario(Negocio, false);
                if (frm.ShowDialog(this) == DialogResult.Cancel) return false;

                //Si es null hago lo de arriba DE NUEVO... cheando que tenga datos en las variables
                if ((Negocio.UsuarioAutenticado == null) || (Negocio.HuellasUsuario == null)) {
                    if (XtraMessageBox.Show("No existe información almacenada para iniciar sesión. Se conectará al servidor para iniciar sesión. Asegurese de que exista conexión con el servidor", "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel) {
                        return false;
                    }
                    bool autenticado = await AutenticarseOnline();
                    return autenticado;
                }

                return true;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return false;
            }
        }
        /// <summary>
        /// Cargar Datos desde la BD Local
        /// </summary>
        /// <returns>bool</returns>
        private bool DatosOffline() {
            try {

                //cargar en lista todos los datos de la BDSQLite
                if (Negocio.ExisteSincronizacionAnterior()) {
                    //normalmente se cargan los datos a memoria, porque venian de archivos
                    //ya no es necesario hacerlo solo se devuelve 

                    Negocio.ExistenDatos = true;

                    //ACCM se debe de implementar
                    //Se debe de ReplicarAcciones
                    return true;
                }

                return false;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return false;
            }
        }
        #endregion

        #region Cargar Datos Online
        /// <summary>
        /// Autenticacion Online
        /// </summary>
        /// <returns>Task<bool></returns>
        private async Task<bool> AutenticarseOnline() {
            try {
                FrmAutenticacionUsuario frm = new FrmAutenticacionUsuario(Negocio);
                if (frm.ShowDialog(this) == DialogResult.Cancel) return false;

                await Negocio.HuellasDelUsuarioAutenticado();

                return true;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return false;
            }
        }

        /// <summary>
        /// Carga todos los datos Online a la BD Local
        /// </summary>
        /// <returns>Task<bool></returns>
        private async Task<bool> DatosOnline() {
            try {
                if (!Negocio.ExistenDatos) {

                    UCCargarDatos uCargarDatos = null;

                    try {
                        //inicializar user control
                        uCargarDatos = new UCCargarDatos {
                            Dock = DockStyle.Fill
                        };
                        //limpiar controles en el area de notificaciones
                        DevGroupControlNotificacionesAcciones.Controls.Clear();
                        DevGroupControlNotificacionesAcciones.Controls.Add(uCargarDatos);

                        //cargar Cadenas
                        List<POCOCadena> cadenas = await CargarCadenas(uCargarDatos);
                        //adicionar una notificacion en caso de que este la lista vacia
                        if (cadenas == null) 
                            Negocio.AdicionarNotificacionListadoVacio("cadenas");

                        //cargar Instalaciones
                        List<POCOInstalacion> instalaciones = await CargarInstalaciones(uCargarDatos, cadenas);
                        if (instalaciones == null)
                            Negocio.AdicionarNotificacionListadoVacio("instalaciones");

                        //cargar Dispositivos
                        List<POCODispositivo> dispositivos = await CargarDispositivos(uCargarDatos, instalaciones);
                        if (dispositivos == null)
                            Negocio.AdicionarNotificacionListadoVacio("dispositivos");

                        //cargar Empresas
                        List<POCOEmpresa> empresas = await CargarEmpresas(uCargarDatos);
                        if (empresas == null)
                            Negocio.AdicionarNotificacionListadoVacio("empresas");

                        //cargar Cargos
                        List<POCOCargo> cargos = await CargarCargos(uCargarDatos, empresas);
                        if (cargos == null)
                            Negocio.AdicionarNotificacionListadoVacio("cargos");

                        //cargar Cuentas
                        List<POCOCuenta> cuentas = await CargarCuentas(uCargarDatos, empresas);
                        if (cuentas == null)
                            Negocio.AdicionarNotificacionListadoVacio("cuentas");

                        //cargar Empleados
                        List<POCOEmpleado> empleados = await CargarEmpleados(uCargarDatos);
                        if (empleados == null)
                            Negocio.AdicionarNotificacionListadoVacio("empleados");

                        //cargar Huellas
                        List<POCOHuella> huellas = await CargarHuellas(uCargarDatos, empleados);
                        if (huellas == null)
                            Negocio.AdicionarNotificacionListadoVacio("huellas");

                        //cargar EmpleadoDispositivo
                        List<POCOEmpleadoDispositivo> empleadoDispositivos = await CargarEmpleadoDispositivos(uCargarDatos, empleados, dispositivos);
                        if (empleadoDispositivos == null)
                            Negocio.AdicionarNotificacionListadoVacio("empleadoDispositivos");

                        //cargar Contrato
                        List<POCOContrato> contratos = await CargarContratos(uCargarDatos, empleados, empresas, cuentas, cargos);
                        if (contratos == null)
                            Negocio.AdicionarNotificacionListadoVacio("contratos");

                        //cargar EmpleadoTurnoServicioCasinos
                        List<POCOEmpleadoTurnoServicioCasino> empleadoTurnoServicioCasinos = await CargarEmpleadoTurnoServicioCasinos(uCargarDatos);
                        if (empleadoTurnoServicioCasinos == null)
                            Negocio.AdicionarNotificacionListadoVacio("empleadoTurnoServicioCasinos");

                        //cargar ServicioCasinos
                        List<POCOServicioCasino> servicioCasinoss = await CargarServicioCasinos(uCargarDatos);
                        if (servicioCasinoss == null)
                            Negocio.AdicionarNotificacionListadoVacio("servicioCasinoss");

                        //cargar TurnoServicioss
                        List<POCOTurnoServicio> turnoServicioss = await CargarTurnoServicios(uCargarDatos);
                        if (turnoServicioss == null)
                            Negocio.AdicionarNotificacionListadoVacio("turnoServicioss");

                        //ACCM
                        //ReaplicarAcciones();

                        Negocio.AdicionarSincronizacion();

                        return true;
                    } catch (Exception eX) {
                        AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                        return false;
                    } finally {
                        if (uCargarDatos != null) {
                            DevGroupControlNotificacionesAcciones.Controls.Clear();
                        }
                    }
                }
                return true;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return false;
            }
        }

        private async Task<List<POCOCadena>> CargarCadenas(ICargaDatos iCargaDatos) {
            try {
                List<POCOCadena> sList = await Negocio.CargarCadenas();

                iCargaDatos.PrimerPaso(13, sList.Count, "Cargando Cadenas");
                Application.DoEvents();

                int contadorElementos = 0;
                
                //eliminar todos los registros
                Negocio.EliminarTodosRegistros("Cadena");

                foreach (var pCadena in sList) {
                    //transformar datos
                    var tCadena = TransformacionDatos.DePOCOCadenaACadena(pCadena);

                    //adicionar al contexto
                    Negocio.mContext.Cadena.Add(tCadena);

                    iCargaDatos.AvanzarActual();

                    //salvar cada 1000 elementos
                    if (contadorElementos++ % 1000 == 0) {
                        Negocio.mContext.SaveChanges();
                        Application.DoEvents();
                    }
                }

                //salvar restantes
                Negocio.mContext.SaveChanges();

                return sList;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return null;
            }
        }

        private async Task<List<POCOInstalacion>> CargarInstalaciones(ICargaDatos iCargaDatos, List<POCOCadena> cadenas) {
            try {
                List<POCOInstalacion> sList = await Negocio.CargarInstalaciones();
                List<POCOInstalacion> lInstalaciones = new List<POCOInstalacion>();

                iCargaDatos.SiguientePaso(sList.Count, "Cargando Instalaciones");
                Application.DoEvents();

                //eliminar todos los registros
                Negocio.EliminarTodosRegistros("Instalacion");

                int contadorElementos = 0;

                foreach (var pInstalacion in sList) {
                    //saber si existe la instalacion en la cadena
                    bool cadenaContieneInstalacion = cadenas.Any(p => p.GuidCadena == pInstalacion.GuidCadena);

                    if (cadenaContieneInstalacion) {
                        //transformar datos
                        var tInstalacion = TransformacionDatos.DePOCOInstalacionAInstalacion(pInstalacion);

                        //adicionar al contexto
                        Negocio.mContext.Instalacion.Add(tInstalacion);

                        lInstalaciones.Add(pInstalacion);
                    }

                    iCargaDatos.AvanzarActual();

                    //salvar cada 1000 elementos
                    if (contadorElementos++ % 1000 == 0) {
                        Negocio.mContext.SaveChanges();
                        Application.DoEvents();
                    }
                }

                //salvar restantes
                Negocio.mContext.SaveChanges();

                return lInstalaciones;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return null;
            }
        }

        private async Task<List<POCODispositivo>> CargarDispositivos(ICargaDatos iCargaDatos, List<POCOInstalacion> instalaciones) {

            List<POCODispositivo> sList = await Negocio.CargarDispositivos();
            List<POCODispositivo> lDispositivos = new List<POCODispositivo>();

            try {
                iCargaDatos.SiguientePaso(sList.Count, "Cargando Dispositivos");
                Application.DoEvents();

                int contadorElementos = 0;

                //eliminar todos los registros
                Negocio.EliminarTodosRegistros("Dispositivo");

                foreach (var pDispositivo in sList) {
                    pDispositivo.Tipo = TipoDispositivo.Reloj;
                    //saber si existe la dispositivo en instalacion
                    bool instalacionContieneDispositivo = instalaciones.Any(p => p.GuidInstalacion == pDispositivo.GuidInstalacion);

                    if ((pDispositivo.Tipo != TipoDispositivo.Invalido) && (instalacionContieneDispositivo)) {
                        //transformar datos
                        var tDispositivo = TransformacionDatos.DePOCODispositivoADispositivo(pDispositivo);

                        //adicionar al contexto
                        Negocio.mContext.Dispositivo.Add(tDispositivo);

                        lDispositivos.Add(pDispositivo);
                    }

                    iCargaDatos.AvanzarActual();

                    //salvar cada 1000 elementos
                    if (contadorElementos++ % 1000 == 0) {
                        Negocio.mContext.SaveChanges();
                        Application.DoEvents();
                    }
                }

                //salvar restantes
                Negocio.mContext.SaveChanges();

                return lDispositivos;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return null;
            }
        }

        private async Task<List<POCOEmpresa>> CargarEmpresas(ICargaDatos iCargaDatos) {

            List<POCOEmpresa> sList = await Negocio.CargarEmpresas();
            List<POCOEmpresa> lDispositivos = new List<POCOEmpresa>();

            try {
                iCargaDatos.SiguientePaso(sList.Count, "Cargando Empresas");
                Application.DoEvents();

                //eliminar todos los registros
                Negocio.EliminarTodosRegistros("Empresa");

                int contadorElementos = 0;

                foreach (var pEmpresa in sList) {
                    //transformar datos
                    var tEmpresa = TransformacionDatos.DePOCOEmpresaAEmpresa(pEmpresa);

                    //adicionar al contexto
                    Negocio.mContext.Empresa.Add(tEmpresa);

                    iCargaDatos.AvanzarActual();

                    //salvar cada 1000 elementos
                    if (contadorElementos++ % 1000 == 0) {
                        Negocio.mContext.SaveChanges();
                        Application.DoEvents();
                    }
                }

                //salvar restantes
                Negocio.mContext.SaveChanges();

                return sList;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return null;
            }
        }

        private async Task<List<POCOCargo>> CargarCargos(ICargaDatos iCargaDatos, List<POCOEmpresa> empresas) {

            List<POCOCargo> sList = await Negocio.CargarCargos();
            List<POCOCargo> lCargos = new List<POCOCargo>();

            try {
                iCargaDatos.SiguientePaso(sList.Count, "Cargando Cargos");
                Application.DoEvents();

                //eliminar todos los registros
                Negocio.EliminarTodosRegistros("Cargo");

                int contadorElementos = 0;

                foreach (var pCargo in sList) {

                    //saber si existe la cargo en empresas
                    bool empresaContieneCargo = empresas.Any(p => p.GuidEmpresa == pCargo.GuidEmpresa);

                    if (empresaContieneCargo) {
                        //transformar datos
                        var tCargo = TransformacionDatos.DePOCOCargoACargo(pCargo);

                        //adicionar al contexto
                        Negocio.mContext.Cargo.Add(tCargo);

                        lCargos.Add(pCargo);
                    }

                    iCargaDatos.AvanzarActual();

                    //salvar cada 1000 elementos
                    if (contadorElementos++ % 1000 == 0) {
                        Negocio.mContext.SaveChanges();
                        Application.DoEvents();
                    }
                }

                //salvar restantes
                Negocio.mContext.SaveChanges();

                return lCargos;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return null;
            }
        }

        private async Task<List<POCOCuenta>> CargarCuentas(ICargaDatos iCargaDatos, List<POCOEmpresa> empresas) {

            List<POCOCuenta> sList = await Negocio.CargarCuentas();
            List<POCOCuenta> lCuentas = new List<POCOCuenta>();

            try {
                iCargaDatos.SiguientePaso(sList.Count, "Cargando Cuentas");
                Application.DoEvents();

                //eliminar todos los registros
                Negocio.EliminarTodosRegistros("Cuenta");

                foreach (var pCuenta in sList) {

                    //saber si existe la cuenta en empresas
                    bool empresaContieneCuenta = empresas.Any(p => p.GuidEmpresa == pCuenta.GuidEmpresa);

                    if (empresaContieneCuenta) {
                        //transformar datos
                        var tCuenta = TransformacionDatos.DePOCOCuentaACuenta(pCuenta);

                        //adicionar al contexto
                        Negocio.mContext.Cuenta.Add(tCuenta);

                        lCuentas.Add(pCuenta);
                    }

                    iCargaDatos.AvanzarActual();
                }

                Negocio.mContext.SaveChanges();
                return lCuentas;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return null;
            }
        }

        private async Task<List<POCOEmpleado>> CargarEmpleados(ICargaDatos iCargaDatos) {

            List<POCOEmpleado> sList = await Negocio.CargarEmpleados();
            List<POCOEmpleado> lEmpleados = new List<POCOEmpleado>();

            try {
                iCargaDatos.SiguientePaso(sList.Count, "Cargando Empleados");
                Application.DoEvents();

                int contadorElementos = 0;

                //eliminar todos los registros
                Negocio.EliminarTodosRegistros("Empleado");

                foreach (var pEmpleado in sList) {

                    string RUT = pEmpleado.RUT;
                    //si no se puede validar continua, si puede corrige la RUT
                    if (!ValidadorRUT.Validar(RUT, out RUT)) {
                        iCargaDatos.AvanzarActual();
                        continue;
                    }
                    pEmpleado.RUT = RUT;

                    //transformar datos
                    var tEmpleado = TransformacionDatos.DePOCOEmpleadoAEmpleado(pEmpleado);

                    //adicionar al contexto
                    Negocio.mContext.Empleado.Add(tEmpleado);

                    lEmpleados.Add(pEmpleado);

                    iCargaDatos.AvanzarActual();
                    
                    //salvar cada 1000 elementos
                    if (contadorElementos++ % 1000 == 0) {
                        Negocio.mContext.SaveChanges();
                        Application.DoEvents();
                    }
                }

                //salvar restantes
                Negocio.mContext.SaveChanges();
                return lEmpleados;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return null;
            }
        }

        private async Task<List<POCOHuella>> CargarHuellas(ICargaDatos iCargaDatos, List<POCOEmpleado> empleados) {

            List<POCOHuella> sList = await Negocio.CargarHuellas();
            List<POCOHuella> lHuellas = new List<POCOHuella>();

            try {
                iCargaDatos.SiguientePaso(sList.Count, "Cargando Huellas");
                Application.DoEvents();

                //eliminar todos los registros
                Negocio.EliminarTodosRegistros("Huella");

                int contadorElementos = 0;

                foreach (var pHuella in sList) {

                    //saber si existe la huella en empleados
                    bool empleadoContieneHuella = empleados.Any(p => p.GuidEmpleado == pHuella.GuidEmpleado);

                    if ((empleadoContieneHuella)&&(Enum.IsDefined(typeof(TipoHuella), pHuella.Tipo))) {
                        //transformar datos
                        var tHuella = TransformacionDatos.DePOCOHuellaAHuella(pHuella);

                        //adicionar al contexto
                        Negocio.mContext.Huella.Add(tHuella);

                        lHuellas.Add(pHuella);
                    }

                    //salvar cada 1000 elementos
                    if (contadorElementos++ % 1000 == 0) {
                        Negocio.mContext.SaveChanges();
                        Application.DoEvents();
                    }

                    iCargaDatos.AvanzarActual();
                }

                //salvar restantes
                Negocio.mContext.SaveChanges();
                return lHuellas;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return null;
            }
        }

        private async Task<List<POCOEmpleadoDispositivo>> CargarEmpleadoDispositivos(ICargaDatos iCargaDatos, List<POCOEmpleado> empleados, List<POCODispositivo> dispositivos) {

            List<POCOEmpleadoDispositivo> sList = await Negocio.CargarEmpleadoDispositivos();
            List<POCOEmpleadoDispositivo> lEmpleadoDispositivos = new List<POCOEmpleadoDispositivo>();

            try {
                iCargaDatos.SiguientePaso(sList.Count, "Cargando Relacion entre Empleados y Dispositivos");
                Application.DoEvents();

                //eliminar todos los registros
                Negocio.EliminarTodosRegistros("EmpleadoDispositivo");

                int contadorElementos = 0;

                foreach (var pEmpleadoDispositivo in sList) {

                    //saber si existe la empleadoDispositivo en empleados y dispositivos
                    bool empleadoContieneEmpleadoDispositivo = empleados.Any(p => p.GuidEmpleado == pEmpleadoDispositivo.GuidEmpleado);
                    bool dispositivoContieneEmpleadoDispositivo = dispositivos.Any(p => p.GuidDispositivo == pEmpleadoDispositivo.GuidDispositivo);

                    if ((empleadoContieneEmpleadoDispositivo) && (dispositivoContieneEmpleadoDispositivo)) {
                        //transformar datos
                        var tEmpleadoDispositivo = TransformacionDatos.DePOCOEmpleadoDispositivoAEmpleadoDispositivo(pEmpleadoDispositivo);

                        //adicionar al contexto
                        Negocio.mContext.EmpleadoDispositivo.Add(tEmpleadoDispositivo);

                        lEmpleadoDispositivos.Add(pEmpleadoDispositivo);
                    }

                    //salvar cada 1000 elementos
                    if (contadorElementos++ % 1000 == 0) {
                        Negocio.mContext.SaveChanges();
                        Application.DoEvents();
                    }

                    iCargaDatos.AvanzarActual();
                }

                //salvar restantes
                Negocio.mContext.SaveChanges();
                return lEmpleadoDispositivos;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return null;
            }
        }

        private async Task<List<POCOContrato>> CargarContratos(ICargaDatos iCargaDatos, List<POCOEmpleado> empleados, List<POCOEmpresa> empresas, List<POCOCuenta> cuentas, List<POCOCargo> cargos) {

            List<POCOContrato> sList = await Negocio.CargarContratos();
            List<POCOContrato> lContratos = new List<POCOContrato>();

            try {
                iCargaDatos.SiguientePaso(sList.Count, "Cargando Contratos");

                Application.DoEvents();

                //eliminar todos los registros
                Negocio.EliminarTodosRegistros("Contrato");

                int contadorElementos = 0;

                foreach (var pContrato in sList) {

                    //saber si existe la contrato en empleados, empresa, cuenta y cargo
                    bool empleadoContieneContrato = empleados.Any(p => p.GuidEmpleado == pContrato.GuidEmpleado);

                    bool empresaContieneContrato = empresas.Any(p => p.GuidEmpresa == pContrato.GuidEmpresa);

                    bool cuentaContieneContrato = cuentas.Any(p => p.GuidCuenta == pContrato.GuidCuenta);

                    bool cargoContieneContrato = cargos.Any(p => p.GuidCargo == pContrato.GuidCargo);

                    //condicional que habia en la carga de contratos
                    if (pContrato.GuidContrato.ToString().ToUpper().Equals("2D4554D6-6A05-4B08-BA50-874C1C2989F9")) {
                        iCargaDatos.SiguientePaso(sList.Count, "Cargando Contratos Entré");
                    }

                    if ((empleadoContieneContrato) && (empresaContieneContrato) && (cuentaContieneContrato) && (cargoContieneContrato)) {
                        //transformar datos
                        var tContrato = TransformacionDatos.DePOCOContratoAContrato(pContrato);

                        //adicionar al contexto
                        Negocio.mContext.Contrato.Add(tContrato);

                        lContratos.Add(pContrato);
                    }

                    //salvar cada 1000 elementos
                    if (contadorElementos++ % 1000 == 0) {
                        Negocio.mContext.SaveChanges();
                        Application.DoEvents();
                    }

                    iCargaDatos.AvanzarActual();
                }

                //salvar restantes
                Negocio.mContext.SaveChanges();
                return lContratos;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return null;
            }
        }

        private async Task<List<POCOEmpleadoTurnoServicioCasino>> CargarEmpleadoTurnoServicioCasinos(ICargaDatos iCargaDatos) {

            List<POCOEmpleadoTurnoServicioCasino> sList = await Negocio.CargarEmpleadoTurnoServicioCasinos();
            List<POCOEmpleadoTurnoServicioCasino> lEmpleadoTurnoServicioCasinoTurnoServicioCasinos = new List<POCOEmpleadoTurnoServicioCasino>();

            try {
                iCargaDatos.SiguientePaso(sList.Count, "Cargando Relacion entre Empleado, TurnoServicio, ServicioCasino");
                Application.DoEvents();

                //eliminar todos los registros
                Negocio.EliminarTodosRegistros("EmpleadoTurnoServicioCasino");

                int contadorElementos = 0;

                foreach (var pEmpleadoTurnoServicioCasino in sList) {

                    //transformar datos
                    var tEmpleadoTurnoServicioCasino = TransformacionDatos.DePOCOEmpleadoTurnoServicioCasinoAEmpleadoTurnoServicioCasino(pEmpleadoTurnoServicioCasino);

                    //adicionar al contexto
                    Negocio.mContext.EmpleadoTurnoServicioCasino.Add(tEmpleadoTurnoServicioCasino);

                    lEmpleadoTurnoServicioCasinoTurnoServicioCasinos.Add(pEmpleadoTurnoServicioCasino);

                    iCargaDatos.AvanzarActual();

                    //salvar cada 1000 elementos
                    if (contadorElementos++ % 1000 == 0) {
                        Negocio.mContext.SaveChanges();
                        Application.DoEvents();
                    }
                }

                //salvar restantes
                Negocio.mContext.SaveChanges();
                return lEmpleadoTurnoServicioCasinoTurnoServicioCasinos;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return null;
            }
        }

        private async Task<List<POCOServicioCasino>> CargarServicioCasinos(ICargaDatos iCargaDatos) {

            List<POCOServicioCasino> sList = await Negocio.CargarServicioCasinos();
            List<POCOServicioCasino> lServicioCasinoTurnoServicioCasinos = new List<POCOServicioCasino>();

            try {
                iCargaDatos.SiguientePaso(sList.Count, "Cargando ServicioCasino");
                Application.DoEvents();

                //eliminar todos los registros
                Negocio.EliminarTodosRegistros("ServicioCasino");

                int contadorElementos = 0;

                foreach (var pServicioCasino in sList) {

                    //transformar datos
                    var tServicioCasino = TransformacionDatos.DePOCOServicioCasinoAServicioCasino(pServicioCasino);

                    //adicionar al contexto
                    Negocio.mContext.ServicioCasino.Add(tServicioCasino);

                    lServicioCasinoTurnoServicioCasinos.Add(pServicioCasino);

                    iCargaDatos.AvanzarActual();

                    //salvar cada 1000 elementos
                    if (contadorElementos++ % 1000 == 0) {
                        Negocio.mContext.SaveChanges();
                        Application.DoEvents();
                    }
                }

                //salvar restantes
                Negocio.mContext.SaveChanges();
                return lServicioCasinoTurnoServicioCasinos;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return null;
            }
        }

        private async Task<List<POCOTurnoServicio>> CargarTurnoServicios(ICargaDatos iCargaDatos) {

            List<POCOTurnoServicio> sList = await Negocio.CargarTurnoServicios();
            List<POCOTurnoServicio> lTurnoServicioTurnoTurnoServicios = new List<POCOTurnoServicio>();

            try {
                iCargaDatos.SiguientePaso(sList.Count, "Cargando TurnoServicio");
                Application.DoEvents();

                //eliminar todos los registros
                Negocio.EliminarTodosRegistros("TurnoServicio");

                int contadorElementos = 0;

                foreach (var pTurnoServicio in sList) {

                    //transformar datos
                    var tTurnoServicio = TransformacionDatos.DePOCOTurnoServicioATurnoServicio(pTurnoServicio);

                    //adicionar al contexto
                    Negocio.mContext.TurnoServicio.Add(tTurnoServicio);

                    lTurnoServicioTurnoTurnoServicios.Add(pTurnoServicio);

                    iCargaDatos.AvanzarActual();

                    //salvar cada 1000 elementos
                    if (contadorElementos++ % 1000 == 0) {
                        Negocio.mContext.SaveChanges();
                        Application.DoEvents();
                    }
                }

                //salvar restantes
                Negocio.mContext.SaveChanges();
                return lTurnoServicioTurnoTurnoServicios;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return null;
            }
        }

        #endregion

        #region Metodos Y Eventos
        private void CargarGridEmpleados() {
            try {
                CrearPaginaNavegacion("Empleados", "Todos");
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }

        private void DevBarButtonEnrolar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            try {
                CrearPaginaNavegacion("Enrolar", "Nuevo");
                DevRibbonPageGroupEnrolar.Visible = true;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }

        public void CrearPaginaNavegacion(string tituloPagina, string PageName) {
            try {
                NavigationPage navigationPage = new NavigationPage {
                    PageText = PageName,
                    Caption = tituloPagina + "-" + PageName,
                    Name = PageName,
                };

                //no duplicar la misma pagina
                var existePagina = DevNavigationPanePrincipal.Pages.Any(p => p.Caption == navigationPage.Caption);
                if (!existePagina) {
                    DevNavigationPanePrincipal.Pages.Add(navigationPage);
                    DevNavigationPanePrincipal.SelectedPageIndex = DevNavigationPanePrincipal.Pages.Count;
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }

        private void DevNavigationPanePrincipal_QueryControl(object sender, QueryControlEventArgs e) {
            try {
                NavigationPageBase page = null;
                string nombrePage = "Sin Nombre";

                if ((sender as NavigationPane).Name == "DevNavigationPanePrincipal") {
                    page = DevNavigationPanePrincipal.Pages.LastOrDefault();
                } else {
                    var framePage = e.Page;
                    page = DevNavigationPanePrincipal.Pages.Where(p => p.Caption == framePage.Caption).FirstOrDefault();
                }

                if (page == null) return;

                nombrePage = page.Caption.Split('-')[0];
                string RUT = page.Name;
                
                XtraUserControl uControl;

                switch (nombrePage) {
                    case "Enrolar": {
                            uControl = new UCEnrolador(Negocio, RUT);
                        };
                        break;
                    default: {
                            uControl = new UCGridDatos(Negocio);
                        };
                        break;
                }

                e.Control = uControl;
                
                MostrarNotificaciones();
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }

        private void DevBarButtonItemDescartar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            try {
                var framePage = DevNavigationPanePrincipal.SelectedPage;
                var page = DevNavigationPanePrincipal.Pages.Where(p => p.Caption == framePage.Caption).FirstOrDefault();

                if (page == null) return;

                DevNavigationPanePrincipal.Pages.Remove(page);

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }

        private void DevNavigationPanePrincipal_SelectedPageChanged(object sender, SelectedPageChangedEventArgs e) {
            if (DevNavigationPanePrincipal.SelectedPage.Caption.Split('-')[0] != "Enrolar")
                DevRibbonPageGroupEnrolar.Visible = false;
            else
                DevRibbonPageGroupEnrolar.Visible = true;
        }

        //Estos metodos se usan para chequear cada 2 seg si hay alguna notificacion nueva y mostrarlas
        private void BackgroundWorkerChequeoNotificaciones_DoWork(object sender, DoWorkEventArgs e) {
            while (true) {
                backgroundWorkerChequeoNotificaciones.ReportProgress(Negocio.lNotificaciones.Count);
                Thread.Sleep(2000);
                Console.WriteLine("Chequeado");
            }
        }
        private void BackgroundWorkerChequeoNotificaciones_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            if (e.ProgressPercentage > Negocio.CantidadElementosNotificaciones) {
                Negocio.CantidadElementosNotificaciones = Negocio.lNotificaciones.Count;
                MostrarNotificaciones();
            }
        }
        /// <summary>
        /// Mostar las notificaciones
        /// </summary>
        public void MostrarNotificaciones() {
            if (Negocio.lNotificaciones.Count > 0) {
                DevGroupControlNotificacionesAcciones.Controls.Clear();
                DevGroupControlNotificacionesAcciones.Controls.Add(Negocio.ObtenerUCNotificaciones(String.Empty));
            }
        }
        #endregion

        #region Otros
        /// <summary>
        /// Metodo para buscar actualizaciones, copiado de la V1
        /// </summary>
        /// <returns>bool</returns>
        private bool BuscarActualizaciones() {
            UpdateCheckInfo info = null;

            if (ApplicationDeployment.IsNetworkDeployed) {
                ApplicationDeployment applicationDeployment = ApplicationDeployment.CurrentDeployment;
                try {
                    info = applicationDeployment.CheckForDetailedUpdate();

                } catch (DeploymentDownloadException eX) {
                    XtraMessageBox.Show("No fue posible descargar la actualización. \n\nRevise su conexión o intente más tarde. Mensaje: " + eX.Message);
                    return false;
                } catch (InvalidDeploymentException eX) {
                    XtraMessageBox.Show("No fue posible buscar actualizaciones. Mensaje: " + eX.Message);
                    return false;
                } catch (InvalidOperationException eX) {
                    XtraMessageBox.Show("No es posible actualizar esta aplicación. Mensaje: " + eX.Message);
                    return false;
                } catch (Exception eX) {
                    XtraMessageBox.Show("No es posible actualizar esta aplicación. Mensaje: " + eX.Message);
                    return false;
                }

                if (info.UpdateAvailable) {
                    Boolean hacerUpdate = true;

                    if (!info.IsUpdateRequired) {
                        //MostrarMensaje("Actualización Disponible", TipoMensaje.Correcto);
                        if (XtraMessageBox.Show("Hay una nueva versión disponible. Presione OK para actualizar.", "Actualización disponible", MessageBoxButtons.OKCancel) != DialogResult.OK) {
                            hacerUpdate = false;
                        }
                    } else {
                        //MostrarMensaje("Actualización Obligatoria", TipoMensaje.Advertencia);
                        XtraMessageBox.Show("Hay una nueva versión disponible. Esta actualización es obligatoria." +
                            "La aplicación se actualizará y se reiniciará automáticamente.",
                            "Actualización obligatoria", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }

                    if (hacerUpdate) {
                        try {
                            applicationDeployment.Update();
                            Negocio.Actualizando = true;
                            //MostrarMensaje("Actualización completada.", TipoMensaje.Correcto);
                            XtraMessageBox.Show("Se ha actualizado la aplicación. Ahora se reiniciará.", "Actualización completada");
                            Application.Restart();
                            return true;
                        } catch (DeploymentDownloadException eX) {
                            XtraMessageBox.Show("No fue posible actualizar.\n\nIntente más tarde.\n\nError: " + eX);
                            Negocio.Actualizando = false;
                            return false;
                        } catch (TrustNotGrantedException eX) {
                            XtraMessageBox.Show("No fue posible actualizar. Error: " + eX.Message);
                            Negocio.Actualizando = false;
                            return false;
                        } catch (Exception eX) {
                            XtraMessageBox.Show("No fue posible actualizar. Error: " + eX.Message);
                            Negocio.Actualizando = false;
                            return false;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Pide autorizacion al Huellero en un formulario
        /// </summary>
        /// <param name="mensaje"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        private async Task<ResultadoAutorizacion> PedirAutorizacion(string mensaje, IWin32Window owner) {
            return ResultadoAutorizacion.Aceptado;

            //Estaba asi en el Enrolador, aunque nunca se ejecuta
            return await Task<ResultadoAutorizacion>.Factory.StartNew((obj) => {
                using (FrmAutorizacion authDialog = new FrmAutorizacion(Negocio.mHuellero, mensaje)) {
                    DialogResult res = authDialog.ShowDialog(owner);
                    if (res == DialogResult.Yes) {
                        return ResultadoAutorizacion.Aceptado;
                    } else if (res == DialogResult.No) {
                        return ResultadoAutorizacion.Rechazado;
                    } else {
                        return ResultadoAutorizacion.Cancelado;
                    }
                }
            }, null, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
        }
        #endregion

        #endregion

        #region Optimizacion de Interfaz
        /// <summary>
        /// Mejora el Parpadeo
        /// https://es.stackoverflow.com/questions/127139/c%C3%B3mo-evitar-el-parpadeo-de-los-controles-windows-forms-c
        /// </summary>
        //protected override CreateParams CreateParams {
        //    get {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
        //        return cp;
        //    }
        //}

        #endregion
    }
}
