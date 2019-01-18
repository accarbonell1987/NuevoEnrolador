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
using System.Data.SQLite;

namespace EnroladorStandAloneV2 {
    public partial class FrmPrincipal : DevExpress.XtraBars.Ribbon.RibbonForm {
        #region Atributos
        private NegocioEnrolador Negocio;
        private FrmSplashScreen frmSplashScreen;
        #endregion

        #region Constructor
        public FrmPrincipal() {
            InitializeComponent();

            //Si la pantalla no es full HD minimizo los botones
            if (Height < 800) DevRibbonControl.Minimized = true;

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

            EstablecerEstadoSistema(EstadoUsoSistema.Inhabilitado);
            await InicializarSistema();
            EstablecerEstadoSistema(EstadoUsoSistema.Habilitado);
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
            Negocio.mHuellero = new Huellero();
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
                    await SincronizarDatos();
                    return;
                }
            }

            CargarGridEmpleados();
            Negocio.AdicionarNotificacionProcesoRealizadoCorrectamente("Inicializar Sistema");
        }

        private void EstablecerEstadoSistema(EstadoUsoSistema estado) {
            Negocio.EstadoUsoSistema = estado;

            if (estado == EstadoUsoSistema.Habilitado) {
                DevRibbonControl.Enabled = true;
                DevPanelControlPrincipal.Enabled = true;
            } else {
                DevRibbonControl.Enabled = false;
                DevPanelControlPrincipal.Enabled = false;
            }
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
                    return await SincronizarDatos();
                }
                return true;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return false;
            }
        }
        /// <summary>
        /// Sincronizacion de datos, envio las acciones luego intento leer nuevamente los datos Online
        /// </summary>
        /// <returns></returns>
        private async Task<bool> SincronizarDatos() {
            //No hay conexion
            if (!await Negocio.IdentificarHardwareOnline()) {
                Negocio.AdicionarNotificacion("Al parecer no existe conexion con los servicios, compruebe su conexión y vuelva a intentarlo", TipoNotificacion.Cuidado);
                return false;
            }
            
            //desactivar propiedades del contexto para optimizar el tiempo de salva
            Negocio.mContext.Configuration.AutoDetectChangesEnabled = false;
            Negocio.mContext.Configuration.ValidateOnSaveEnabled = false;

            MostrarNotificaciones();

            UCCargarDatos uCargarDatos = null;

            try {
                //inicializar user control
                uCargarDatos = new UCCargarDatos {
                    Dock = DockStyle.Fill
                };
                //limpiar controles en el area de notificaciones
                DevGroupControlNotificacionesAcciones.Controls.Clear();
                DevGroupControlNotificacionesAcciones.Controls.Add(uCargarDatos);

                //se deben de realizar los cambios en los progress bar
                Negocio.EnviarAcciones();

                //cargar Cadenas
                List<POCOCadena> cadenas = await CargarCadenas(uCargarDatos);
                //adicionar una notificacion en caso de que este la lista vacia
                if (cadenas == null)
                    Negocio.AdicionarNotificacionListadoVacio("cadenas");

                //cargar Contrato
                List<POCOContrato> contratos = await CargarContratos(uCargarDatos);
                if (contratos == null)
                    Negocio.AdicionarNotificacionListadoVacio("contratos");

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

                Negocio.AdicionarSincronizacion();

                //cargar grid
                CargarGridEmpleados();

                return true;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return false;
            } finally {
                if (uCargarDatos != null) {
                    DevGroupControlNotificacionesAcciones.Controls.Clear();
                }
                //activar propiedades del contexto para optimizar el tiempo de salva
                Negocio.mContext.Configuration.AutoDetectChangesEnabled = true;
                Negocio.mContext.Configuration.ValidateOnSaveEnabled = true;

            }
        }

        private async Task<List<POCOCadena>> CargarCadenas(ICargaDatos iCargaDatos) {
            try {
                List<POCOCadena> sList = await Negocio.CargarCadenas();
                List<Cadena> dbList = new List<Cadena>();

                iCargaDatos.PrimerPaso(13, sList.Count, "Cargando Cadenas");
                Application.DoEvents();

                int contadorElementos = 0;
                
                //eliminar todos los registros
                Negocio.EliminarTodosRegistros("Cadena");

                foreach (var pCadena in sList) {
                    //transformar datos
                    var tCadena = TransformacionDatos.DePOCOCadenaACadena(pCadena);
                    dbList.Add(tCadena);
                    iCargaDatos.AvanzarActual();

                    //mostar progreso cada 100 elementos
                    if (contadorElementos++ % 100 == 0) {
                        Application.DoEvents();
                    }
                }

                Negocio.SalvarLote(dbList);
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
                List<Instalacion> dbList = new List<Instalacion>();

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
                        dbList.Add(tInstalacion);
                        lInstalaciones.Add(pInstalacion);
                    }

                    iCargaDatos.AvanzarActual();

                    //salvar cada 100 elementos
                    if (contadorElementos++ % 100 == 0) {
                        Application.DoEvents();
                    }
                }

                Negocio.SalvarLote(dbList);

                return lInstalaciones;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return null;
            }
        }
        private async Task<List<POCODispositivo>> CargarDispositivos(ICargaDatos iCargaDatos, List<POCOInstalacion> instalaciones) {

            List<POCODispositivo> sList = await Negocio.CargarDispositivos();
            List<POCODispositivo> lDispositivos = new List<POCODispositivo>();
            List<Dispositivo> dbList = new List<Dispositivo>();

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
                        dbList.Add(tDispositivo);
                        lDispositivos.Add(pDispositivo);
                    }

                    iCargaDatos.AvanzarActual();

                    //salvar cada 100 elementos
                    if (contadorElementos++ % 100 == 0) {
                        Application.DoEvents();
                    }
                }

                Negocio.SalvarLote(dbList);
                return lDispositivos;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return null;
            }
        }
        private async Task<List<POCOEmpresa>> CargarEmpresas(ICargaDatos iCargaDatos) {

            List<POCOEmpresa> sList = await Negocio.CargarEmpresas();
            List<POCOEmpresa> lDispositivos = new List<POCOEmpresa>();
            List<Empresa> dbList = new List<Empresa>();

            try {
                iCargaDatos.SiguientePaso(sList.Count, "Cargando Empresas");
                Application.DoEvents();

                //eliminar todos los registros
                Negocio.EliminarTodosRegistros("Empresa");

                int contadorElementos = 0;

                foreach (var pEmpresa in sList) {
                    //transformar datos
                    var tEmpresa = TransformacionDatos.DePOCOEmpresaAEmpresa(pEmpresa);
                    dbList.Add(tEmpresa);
                    iCargaDatos.AvanzarActual();

                    //salvar cada 100 elementos
                    if (contadorElementos++ % 100 == 0) {
                        Application.DoEvents();
                    }
                }

                Negocio.SalvarLote(dbList);
                return sList;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return null;
            }
        }
        private async Task<List<POCOCargo>> CargarCargos(ICargaDatos iCargaDatos, List<POCOEmpresa> empresas) {

            List<POCOCargo> sList = await Negocio.CargarCargos();
            List<POCOCargo> lCargos = new List<POCOCargo>();
            List<Cargo> dbList = new List<Cargo>();

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
                        dbList.Add(tCargo);
                        lCargos.Add(pCargo);
                    }

                    iCargaDatos.AvanzarActual();

                    //salvar cada 100 elementos
                    if (contadorElementos++ % 100 == 0) {
                        Application.DoEvents();
                    }
                }

                Negocio.SalvarLote(dbList);

                return lCargos;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return null;
            }
        }
        private async Task<List<POCOCuenta>> CargarCuentas(ICargaDatos iCargaDatos, List<POCOEmpresa> empresas) {

            List<POCOCuenta> sList = await Negocio.CargarCuentas();
            List<POCOCuenta> lCuentas = new List<POCOCuenta>();
            List<Cuenta> dbList = new List<Cuenta>();
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
                        dbList.Add(tCuenta);
                        lCuentas.Add(pCuenta);
                    }

                    iCargaDatos.AvanzarActual();
                }
                Negocio.SalvarLote(dbList);
                return lCuentas;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return null;
            }
        }
        private async Task<List<POCOEmpleado>> CargarEmpleados(ICargaDatos iCargaDatos) {

            List<POCOEmpleado> sList = await Negocio.CargarEmpleados();
            List<POCOEmpleado> lEmpleados = new List<POCOEmpleado>();
            List<Empleado> dbList = new List<Empleado>();

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
                    dbList.Add(tEmpleado);
                    lEmpleados.Add(pEmpleado);

                    iCargaDatos.AvanzarActual();

                    //salvar cada 100 elementos
                    if (contadorElementos++ % 100 == 0) {
                        Application.DoEvents();
                    }
                }

                Negocio.SalvarLote(dbList);
                return lEmpleados;

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return null;
            }
        }
        private async Task<List<POCOHuella>> CargarHuellas(ICargaDatos iCargaDatos, List<POCOEmpleado> empleados) {

            List<POCOHuella> sList = await Negocio.CargarHuellas();
            List<POCOHuella> lHuellas = new List<POCOHuella>();
            List<Huella> dbList = new List<Huella>();
            try {
                iCargaDatos.SiguientePaso(sList.Count, "Cargando Huellas");
                Application.DoEvents();

                //eliminar todos los registros
                Negocio.EliminarTodosRegistros("Huella");

                int contadorElementos = 0;

                foreach (var pHuella in sList) {

                    //saber si existe la huella en empleados
                    bool empleadoContieneHuella = empleados.Any(p => p.GuidEmpleado == pHuella.GuidEmpleado);

                    if ((empleadoContieneHuella) && (Enum.IsDefined(typeof(TipoHuella), pHuella.Tipo))) {
                        //transformar datos
                        var tHuella = TransformacionDatos.DePOCOHuellaAHuella(pHuella);
                        dbList.Add(tHuella);
                        lHuellas.Add(pHuella);
                    }

                    //salvar cada 100 elementos
                    if (contadorElementos++ % 100 == 0) {
                        Application.DoEvents();
                    }

                    iCargaDatos.AvanzarActual();
                }
                Negocio.SalvarLote(dbList);

                return lHuellas;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return null;
            }
        }
        private async Task<List<POCOEmpleadoDispositivo>> CargarEmpleadoDispositivos(ICargaDatos iCargaDatos, List<POCOEmpleado> empleados, List<POCODispositivo> dispositivos) {

            List<POCOEmpleadoDispositivo> sList = await Negocio.CargarEmpleadoDispositivos();
            List<POCOEmpleadoDispositivo> lEmpleadoDispositivos = new List<POCOEmpleadoDispositivo>();
            List<EmpleadoDispositivo> dbList = new List<EmpleadoDispositivo>();
            try {
                iCargaDatos.SiguientePaso(sList.Count, "Cargando Relacion entre Empleados y Dispositivos");
                Application.DoEvents();

                //eliminar todos los registros
                Negocio.EliminarTodosRegistros("EmpleadoDispositivo");

                int contadorElementos = 0;

                foreach (var pEmpleadoDispositivo in sList) {
                    //transformar datos
                    var tEmpleadoDispositivo = TransformacionDatos.DePOCOEmpleadoDispositivoAEmpleadoDispositivo(pEmpleadoDispositivo);
                    dbList.Add(tEmpleadoDispositivo);
                    lEmpleadoDispositivos.Add(pEmpleadoDispositivo);
                    
                    //salvar cada 100 elementos
                    if (contadorElementos++ % 100 == 0) {
                        Application.DoEvents();
                    }

                    iCargaDatos.AvanzarActual();
                }
                Negocio.SalvarLote(dbList);
                return lEmpleadoDispositivos;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return null;
            }
        }
        private async Task<List<POCOContrato>> CargarContratos(ICargaDatos iCargaDatos) {

            List<POCOContrato> sList = await Negocio.CargarContratos();
            List<POCOContrato> lContratos = new List<POCOContrato>();
            List<Contrato> dbList = new List<Contrato>();

            try {
                iCargaDatos.SiguientePaso(sList.Count, "Cargando Contratos");

                Application.DoEvents();

                //eliminar todos los registros
                Negocio.EliminarTodosRegistros("Contrato");

                int contadorElementos = 0;

                foreach (var pContrato in sList) {

                    //condicional que habia en la carga de contratos
                    if (pContrato.GuidContrato.ToString().ToUpper().Equals("2D4554D6-6A05-4B08-BA50-874C1C2989F9")) {
                        iCargaDatos.SiguientePaso(sList.Count, "Cargando Contratos Entré");
                    }
                    
                    //transformar datos
                    var tContrato = TransformacionDatos.DePOCOContratoAContrato(pContrato);
                    dbList.Add(tContrato);
                    lContratos.Add(pContrato);
                    
                    //salvar cada 100 elementos
                    if (contadorElementos++ % 100 == 0) {
                        Application.DoEvents();
                    }

                    iCargaDatos.AvanzarActual();
                }
                Negocio.SalvarLote(dbList);
                return lContratos;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return null;
            }
        }
        private async Task<List<POCOEmpleadoTurnoServicioCasino>> CargarEmpleadoTurnoServicioCasinos(ICargaDatos iCargaDatos) {

            List<POCOEmpleadoTurnoServicioCasino> sList = await Negocio.CargarEmpleadoTurnoServicioCasinos();
            List<POCOEmpleadoTurnoServicioCasino> lEmpleadoTurnoServicioCasinoTurnoServicioCasinos = new List<POCOEmpleadoTurnoServicioCasino>();
            List<EmpleadoTurnoServicioCasino> dbList = new List<EmpleadoTurnoServicioCasino>();
            try {
                iCargaDatos.SiguientePaso(sList.Count, "Cargando Relacion entre Empleado, TurnoServicio, ServicioCasino");
                Application.DoEvents();

                //eliminar todos los registros
                Negocio.EliminarTodosRegistros("EmpleadoTurnoServicioCasino");

                int contadorElementos = 0;

                foreach (var pEmpleadoTurnoServicioCasino in sList) {

                    //transformar datos
                    var tEmpleadoTurnoServicioCasino = TransformacionDatos.DePOCOEmpleadoTurnoServicioCasinoAEmpleadoTurnoServicioCasino(pEmpleadoTurnoServicioCasino);
                    dbList.Add(tEmpleadoTurnoServicioCasino);
                    lEmpleadoTurnoServicioCasinoTurnoServicioCasinos.Add(pEmpleadoTurnoServicioCasino);

                    iCargaDatos.AvanzarActual();

                    //salvar cada 100 elementos
                    if (contadorElementos++ % 100 == 0) {
                        Application.DoEvents();
                    }
                }
                Negocio.SalvarLote(dbList);
                return lEmpleadoTurnoServicioCasinoTurnoServicioCasinos;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return null;
            }
        }
        private async Task<List<POCOServicioCasino>> CargarServicioCasinos(ICargaDatos iCargaDatos) {

            List<POCOServicioCasino> sList = await Negocio.CargarServicioCasinos();
            List<POCOServicioCasino> lServicioCasinoTurnoServicioCasinos = new List<POCOServicioCasino>();
            List<ServicioCasino> dbList = new List<ServicioCasino>();
            try {
                iCargaDatos.SiguientePaso(sList.Count, "Cargando ServicioCasino");
                Application.DoEvents();

                //eliminar todos los registros
                Negocio.EliminarTodosRegistros("ServicioCasino");

                int contadorElementos = 0;

                foreach (var pServicioCasino in sList) {

                    //transformar datos
                    var tServicioCasino = TransformacionDatos.DePOCOServicioCasinoAServicioCasino(pServicioCasino);
                    dbList.Add(tServicioCasino);
                    lServicioCasinoTurnoServicioCasinos.Add(pServicioCasino);

                    iCargaDatos.AvanzarActual();

                    //salvar cada 100 elementos
                    if (contadorElementos++ % 100 == 0) {
                        Application.DoEvents();
                    }
                }
                Negocio.SalvarLote(dbList);
                return lServicioCasinoTurnoServicioCasinos;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return null;
            }
        }
        private async Task<List<POCOTurnoServicio>> CargarTurnoServicios(ICargaDatos iCargaDatos) {

            List<POCOTurnoServicio> sList = await Negocio.CargarTurnoServicios();
            List<POCOTurnoServicio> lTurnoServicioTurnoTurnoServicios = new List<POCOTurnoServicio>();
            List<TurnoServicio> dbList = new List<TurnoServicio>();
            try {
                iCargaDatos.SiguientePaso(sList.Count, "Cargando TurnoServicio");
                Application.DoEvents();

                //eliminar todos los registros
                Negocio.EliminarTodosRegistros("TurnoServicio");

                int contadorElementos = 0;

                foreach (var pTurnoServicio in sList) {

                    //transformar datos
                    var tTurnoServicio = TransformacionDatos.DePOCOTurnoServicioATurnoServicio(pTurnoServicio);
                    dbList.Add(tTurnoServicio);
                    lTurnoServicioTurnoTurnoServicios.Add(pTurnoServicio);

                    iCargaDatos.AvanzarActual();

                    //salvar cada 100 elementos
                    if (contadorElementos++ % 100 == 0) {
                        Application.DoEvents();
                    }
                }
                Negocio.SalvarLote(dbList);
                return lTurnoServicioTurnoTurnoServicios;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
                return null;
            }
        }
        #endregion

        #region Accion de Botones
        private void DevBarButtonEnrolar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            try {
                CrearPaginaNavegacion("Enrolar", "Nuevo");
                DevRibbonPageGroupEnrolar.Visible = true;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        private void DevBarButtonItemGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            try {
                if (DevNavigationPanePrincipal.SelectedPage.Caption.Split('-')[0] == "Enrolar") {
                    var framePage = DevNavigationPanePrincipal.SelectedPage;
                    NavigationPageBase page = DevNavigationPanePrincipal.Pages.Where(p => p.Caption == framePage.Caption).FirstOrDefault();
                    UCEnrolador uC = (UCEnrolador)page.Controls[0];

                    Negocio.SalvarCambios(uC.empleado);
                    DevNavigationPanePrincipal.Pages.Remove(page);

                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        private void DevBarButtonItemDescartar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            try {
                if (DevNavigationPanePrincipal.SelectedPage.Caption.Split('-')[0] == "Enrolar") {
                    var framePage = DevNavigationPanePrincipal.SelectedPage;
                    NavigationPageBase page = DevNavigationPanePrincipal.Pages.Where(p => p.Caption == framePage.Caption).FirstOrDefault();
                    UCEnrolador uC = (UCEnrolador)page.Controls[0];
                    uC.DescartarCambios();
                    DevNavigationPanePrincipal.Pages.Remove(page);
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }
        #endregion

        #region Panel de Navegacion
        private void CargarGridEmpleados() {
            try {
                CrearPaginaNavegacion("Empleados", "Todos");
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
        private void DevNavigationPanePrincipal_SelectedPageChanged(object sender, SelectedPageChangedEventArgs e) {
            if (DevNavigationPanePrincipal.SelectedPage.Caption.Split('-')[0] != "Enrolar")
                DevRibbonPageGroupEnrolar.Visible = false;
            else
                DevRibbonPageGroupEnrolar.Visible = true;
        }
        #endregion

        #region Notificaciones
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
                //minimizo el control
                if (Height < 800) DevRibbonControl.Minimized = true;

                DevGroupControlNotificacionesAcciones.Height = 77;
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
            //return await Task<ResultadoAutorizacion>.Factory.StartNew((obj) => {
            //    using (FrmAutorizacion authDialog = new FrmAutorizacion(Negocio.mHuellero, mensaje)) {
            //        DialogResult res = authDialog.ShowDialog(owner);
            //        if (res == DialogResult.Yes) {
            //            return ResultadoAutorizacion.Aceptado;
            //        } else if (res == DialogResult.No) {
            //            return ResultadoAutorizacion.Rechazado;
            //        } else {
            //            return ResultadoAutorizacion.Cancelado;
            //        }
            //    }
            //}, null, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async void DevBarButtonSincronizar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try {
                //desactivar sistema
                EstablecerEstadoSistema(EstadoUsoSistema.Inhabilitado);

                var sincronizo = await SincronizarDatos();
                if (sincronizo) Negocio.AdicionarNotificacionProcesoRealizadoCorrectamente("Sincronizacion Correcta");
                else Negocio.AdicionarNotificacion("No se pudo sincronizar", TipoNotificacion.Cuidado);

                //activar sistema
                EstablecerEstadoSistema(EstadoUsoSistema.Habilitado);
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, Negocio.lNotificaciones);
            }
        }

        /// <summary>
        /// Controla el evento de la paleta de iconos cuando la resolucion es baja
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DevRibbonControl_MinimizedChanged(object sender, EventArgs e)
        {
            var notificacionSize = DevGroupControlNotificacionesAcciones.Size;

            if (DevRibbonControl.Minimized) notificacionSize.Height = 77;
            else notificacionSize.Height = 20;

            DevGroupControlNotificacionesAcciones.Size = notificacionSize;
        }
        private void DevGroupControlNotificacionesAcciones_DoubleClick(object sender, EventArgs e)
        {
            var notificacionSize = DevGroupControlNotificacionesAcciones.Size;

            if (notificacionSize.Height == 77) notificacionSize.Height = 20;
            else notificacionSize.Height = 77;

            DevGroupControlNotificacionesAcciones.Size = notificacionSize;
        }
        #endregion

        #endregion

        #region Optimizacion de Interfaz
        /// <summary>
        /// Mejora el Parpadeo
        /// https://es.stackoverflow.com/questions/127139/c%C3%B3mo-evitar-el-parpadeo-de-los-controles-windows-forms-c
        /// </summary>
        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
        #endregion
    }
}
