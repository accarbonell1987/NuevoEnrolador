using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DevExpress.XtraEditors;
using EnroladorAccesoDatos.Ayudantes;
using EnroladorAccesoDatos.SQLite;
using EnroladorAccesoDatos.Dominio;
using EnroladorStandAloneV2.EnroladorServicioWeb;
using EnroladorStandAloneV2.Herramientas;
using EnroladorStandAloneV2.Herramientas.Huellero;
using EnroladorStandAloneV2.CapaInterfazUsuario.Interfaces;
using EnroladorAccesoDatos;
using EnroladorStandAloneV2.CapaInterfazUsuario;

namespace EnroladorStandAloneV2.CapaLogicaNegocio {
    public class NegocioEnrolador {
        #region Atributos
        public DateTime UltimaConexion { get; set; }
        public bool ExistenDatos { get; set; }
        public bool Actualizando { get; set; }
        public bool Online { get; set; }
        public Guid MyHardwareId { get; set; }

        private const int diasRecomendandosParaConectar = 15;
        public static int DiasRecomendandosParaConectar => diasRecomendandosParaConectar;

        public Usuario UsuarioAutenticado { get; set; }
        public List<Huella> HuellasUsuario { get; set; }

        public Huellero mHuellero { get; set; }
        public SQLiteEnrollEntities mContext { get; set; }
        public List<Notificacion> lNotificaciones { get; set; }

        public List<POCOEmpleado> lEmpleados { get; set; }
        #endregion

        #region Constructor
        public NegocioEnrolador() {
            mContext = new SQLiteEnrollEntities();
            lNotificaciones = new List<Notificacion>();

            Online = false;
            ExistenDatos = false;
            Actualizando = false;
        }
        #endregion

        #region Metodos

        #region Obtener de SQLite
        /// <summary>
        /// Chequea la existencia del Archivo de la BD y si tiene datos
        /// </summary>
        /// <returns>bool</returns>
        public bool IdentificarLocal() {
            try {
                string nombreArchivo = AyudanteDirectorioDatos.ObtenerDirectorioDelEnsamblado() + @"\Roll.sqlite3";

                if (AyudanteDirectorioDatos.ExisteArchivo(nombreArchivo)) {
                    if (mContext.Hardware.Count() > 0) {
                        var hardware = mContext.Hardware.FirstOrDefault();
                        if (hardware.GuidHardware.Equals(MyHardwareId.ToString())) {
                            return true;
                        }
                    }
                }
                return false;

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return false;
            }
        }

        /// <summary>
        /// Saber si existen datos locales (usuario) y (huella)
        /// </summary>
        /// <returns>bool</returns>
        public bool ExisteDatosAutenticacionUsuario() {
            if ((mContext.Usuario.Count() > 0) || (mContext.Huella.Count() > 0))
                return true;
            return false;
        }

        public Usuario ObtenerUsuarioLocal() {
            try {
                var usuario = mContext.Usuario.FirstOrDefault();
                if (usuario == null) return null;

                HuellasUsuario = ObtenerHuellaUsuarioLocal(usuario.GuidUsuario);

                return usuario;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }
        public List<POCOEmpleado> ObtenerTodosEmpleados() {
            try {
                List<POCOEmpleado> lPOCOEmpleados = new List<POCOEmpleado>();
                //cargar empleados locales
                var todosEmpleados = mContext.Empleado.ToList();
                var todosContratos = mContext.Contrato.ToList();
                var todasHuellas = mContext.Huella.ToList();
                var todosEmpleadosDispositivos = mContext.EmpleadoDispositivo.ToList();
                var todosDispositivos = mContext.Dispositivo.ToList();

                foreach (var empleado in todosEmpleados) {
                    //transformar el empleado a poco
                    var pocoEmpleado = TransformacionDatos.DeEmpleadoAPOCOEmpleado(empleado);

                    pocoEmpleado.TipoIdentificacion = pocoEmpleado.TieneContraseña ? TipoIdentificacion.Clave.ToString() : TipoIdentificacion.Huella.ToString();

                    //cargar contratos del empleado
                    var contratos = todosContratos.Where(p => p.GuidEmpleado == empleado.GuidEmpleado).ToList();

                    if (contratos.Count > 0) {
                        //adicionar contratos al empleado
                        List<POCOContrato> lPOCOContratos = new List<POCOContrato>();
                        foreach (var contrato in contratos) {
                            POCOContrato pContrato = TransformacionDatos.DeContratoAPOCOContrato(contrato);

                            var empresa = TransformacionDatos.DeEmpresaAPOCOEmpresa(mContext.Empresa.FirstOrDefault(p => p.GuidEmpresa == pContrato.GuidEmpresa.ToString()));
                            pContrato.NombreEmpresa = empresa == null ? "Sin Empresa" : empresa.NombreEmpresa;

                            var cuenta = TransformacionDatos.DeCuentaAPOCOCuenta(mContext.Cuenta.FirstOrDefault(p => p.GuidCuenta == pContrato.GuidCuenta.ToString()));
                            pContrato.NombreCuenta = cuenta == null ? "Sin Cuenta" : cuenta.NombreCuenta;

                            var cargo = TransformacionDatos.DeCargoAPOCOCargo(mContext.Cargo.FirstOrDefault(p => p.GuidCargo == pContrato.GuidCargo.ToString()));
                            pContrato.NombreCargo = cargo == null ? "Sin Cargo" : cargo.NombreCargo;

                            pocoEmpleado.Contratos.Add(pContrato);
                        }

                        //cargar huellas del empleado
                        var huellas = todasHuellas.Where(p => p.GuidEmpleado == empleado.GuidEmpleado).ToList();

                        //adicionar huellas al empleado
                        List<POCOHuella> lPOCOHuellas = new List<POCOHuella>();
                        foreach (var huella in huellas) {
                            pocoEmpleado.Huellas.Add(TransformacionDatos.DeHuellaAPOCOHuella(huella));
                        }

                        //cargar dispositivos del empleado
                        var dispEmpleado = todosEmpleadosDispositivos.Where(p => p.GuidEmpleado == empleado.GuidEmpleado).ToList();

                        var dispositivos = todosDispositivos.Where(p => dispEmpleado.Any(a => a.GuidDispositivo == p.GuidDispositivo)).Select(p => p).ToList();
                        //var dispositivos = ObtenerTodosDispositivos().Where(p => dispEmpleado.Any(a => a.GuidDispositivo == p.GuidDispositivo.ToString())).Select(p => p).ToList();

                        //adicionar dispositivos al empleado
                        foreach (var dispositivo in dispositivos) {
                            var pocoDispositivo = TransformacionDatos.DeDispositivoAPOCODispositivo(dispositivo);

                            var instalacion = ObtenerInstalacionDelDispositivo(pocoDispositivo.GuidInstalacion);
                            var cadena = ObtenerCadenaDeInstalacion(instalacion.GuidCadena);

                            pocoDispositivo.NombreCadena = cadena.NombreCadena;
                            pocoDispositivo.NombreInstalacion = instalacion.NombreInstalacion;

                            pocoEmpleado.Dispositivos.Add(pocoDispositivo);
                        }

                        lPOCOEmpleados.Add(pocoEmpleado);
                    }
                }

                lEmpleados = lPOCOEmpleados;                
                return lPOCOEmpleados;

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }
        public List<POCOEmpresa> ObtenerTodasEmpresas() {
            try {
                List<POCOEmpresa> lPOCOEmpresas = new List<POCOEmpresa>();
                var empresas = mContext.Empresa.ToList();

                foreach (var empresa in empresas) {
                    var pocoEmpresa = TransformacionDatos.DeEmpresaAPOCOEmpresa(empresa);
                    lPOCOEmpresas.Add(pocoEmpresa);
                }

                return lPOCOEmpresas;

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }
        public List<POCOCuenta> ObtenerTodasCuentas() {
            try {
                List<POCOCuenta> lPOCOCuentas = new List<POCOCuenta>();
                var cuentas = mContext.Cuenta.ToList();

                foreach (var cuenta in cuentas) {
                    var pocoCuenta = TransformacionDatos.DeCuentaAPOCOCuenta(cuenta);
                    lPOCOCuentas.Add(pocoCuenta);
                }

                return lPOCOCuentas;

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }
        public List<POCOCargo> ObtenerTodosCargos() {
            try {
                List<POCOCargo> lPOCOCargos = new List<POCOCargo>();
                var cargos = mContext.Cargo.ToList();

                foreach (var cargo in cargos) {
                    var pocoCargo = TransformacionDatos.DeCargoAPOCOCargo(cargo);
                    lPOCOCargos.Add(pocoCargo);
                }

                return lPOCOCargos;

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }

        public List<POCOCadena> ObtenerTodasCadenas() {
            try {
                List<POCOCadena> lPOCOCadenas = new List<POCOCadena>();
                var cadenas = mContext.Cadena.ToList();

                foreach (var cadena in cadenas) {
                    var pocoCadena = TransformacionDatos.DeCadenaAPOCOCadena(cadena);
                    //buscar todas las intalaciones de la cadena
                    pocoCadena.Instalaciones = ObtenerTodasInstalacionesDeCadena(pocoCadena.GuidCadena);
                    lPOCOCadenas.Add(pocoCadena);
                }

                return lPOCOCadenas;

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }
        public POCOCadena ObtenerCadenaDeInstalacion(Guid GuidCadena) {
            try {
                var cadena = mContext.Cadena.FirstOrDefault(p => p.GuidCadena == GuidCadena.ToString());
                if (cadena == null) return null;

                return TransformacionDatos.DeCadenaAPOCOCadena(cadena);
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }

        public List<POCOInstalacion> ObtenerTodasInstalaciones() {
            try {
                List<POCOInstalacion> lPOCOInstalacions = new List<POCOInstalacion>();
                var instalacions = mContext.Instalacion.ToList();

                foreach (var instalacion in instalacions) {
                    var pocoInstalacion = TransformacionDatos.DeInstalacionAPOCOInstalacion(instalacion);
                    //buscar nombre de cadena que pertenece la instalacion
                    pocoInstalacion.NombreCadena = ObtenerCadenaDeInstalacion(pocoInstalacion.GuidCadena).NombreCadena;
                    //buscar todos los dispositivos de la instalacion
                    pocoInstalacion.Dispositivos = ObtenerTodosDispositivosDeInstalacion(pocoInstalacion.GuidInstalacion);
                    lPOCOInstalacions.Add(pocoInstalacion);
                }

                return lPOCOInstalacions;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }
        public List<POCOInstalacion> ObtenerTodasInstalacionesDeCadena(Guid GuidCadena) {
            try {
                List<POCOInstalacion> lPOCOInstalacions = new List<POCOInstalacion>();
                var instalaciones = mContext.Instalacion.Where(p => p.GuidCadena == GuidCadena.ToString()).ToList();

                foreach (var instalacion in instalaciones) {
                    var pocoInstalacion = TransformacionDatos.DeInstalacionAPOCOInstalacion(instalacion);
                    //buscar nombre de cadena que pertenece la instalacion
                    pocoInstalacion.NombreCadena = ObtenerCadenaDeInstalacion(pocoInstalacion.GuidCadena).NombreCadena;
                    //buscar todos los dispositivos de la instalacion
                    pocoInstalacion.Dispositivos = ObtenerTodosDispositivosDeInstalacion(pocoInstalacion.GuidInstalacion);
                    lPOCOInstalacions.Add(pocoInstalacion);
                }

                return lPOCOInstalacions;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }
        public POCOInstalacion ObtenerInstalacion(Guid GuidInstalacion) {
            try {
                var instalacion = mContext.Instalacion.FirstOrDefault(p => p.GuidInstalacion == GuidInstalacion.ToString());
                if (instalacion == null) return null;

                var pocoInstalacion = TransformacionDatos.DeInstalacionAPOCOInstalacion(instalacion);
                //buscar nombre de cadena que pertenece la instalacion
                pocoInstalacion.NombreCadena = ObtenerCadenaDeInstalacion(pocoInstalacion.GuidCadena).NombreCadena;
                //buscar todos los dispositivos de la instalacion
                pocoInstalacion.Dispositivos = ObtenerTodosDispositivosDeInstalacion(pocoInstalacion.GuidInstalacion);

                return pocoInstalacion;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }
        public POCOInstalacion ObtenerInstalacionDelDispositivo(Guid GuidInstalacion) {
            try {
                var instalacion = mContext.Instalacion.FirstOrDefault(p => p.GuidInstalacion == GuidInstalacion.ToString());
                if (instalacion == null) return null;

                var pocoInstalacion = TransformacionDatos.DeInstalacionAPOCOInstalacion(instalacion);
                //buscar nombre de cadena que pertenece la instalacion
                pocoInstalacion.NombreCadena = ObtenerCadenaDeInstalacion(pocoInstalacion.GuidCadena).NombreCadena;
                //buscar todos los dispositivos de la instalacion
                pocoInstalacion.Dispositivos = ObtenerTodosDispositivosDeInstalacion(pocoInstalacion.GuidInstalacion);

                return pocoInstalacion;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }

        public List<POCODispositivo> ObtenerTodosDispositivos() {
            try {
                List<POCODispositivo> lPOCODispositivos = new List<POCODispositivo>();
                var dispositivos = mContext.Dispositivo.ToList();

                foreach (var dispositivo in dispositivos) {
                    var pocoDispositivo = TransformacionDatos.DeDispositivoAPOCODispositivo(dispositivo);
                    lPOCODispositivos.Add(pocoDispositivo);
                }

                return lPOCODispositivos;

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }
        public List<POCODispositivo> ObtenerTodosDispositivosDeInstalacion(Guid GuidInstalacion) {
            try {
                List<POCODispositivo> lPOCODispositivos = new List<POCODispositivo>();
                var dispositivos = mContext.Dispositivo.Where(p => p.GuidInstalacion == GuidInstalacion.ToString()).ToList();

                foreach (var dispositivo in dispositivos) {
                    var pocoDispositivo = TransformacionDatos.DeDispositivoAPOCODispositivo(dispositivo);
                    lPOCODispositivos.Add(pocoDispositivo);
                }

                return lPOCODispositivos;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }

        public POCOEmpleado ObtenerEmpleadoDeLista(string RUT) {
            try {
                if (lEmpleados == null) return null;
                return lEmpleados.FirstOrDefault(p => p.RUT == RUT);
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }
        public POCOEmpleado ObtenerEmpleadoDeBaseDatos(string RUT) {
            ObtenerTodosEmpleados();
            return ObtenerEmpleadoDeLista(RUT);
        }
        /// <summary>
        /// Obtener un string de NombreEmpresa - NombreCuenta - NombreCargo de cada contrato del empleado
        /// </summary>
        /// <param name="pRut">string pRut</param>
        /// <returns>string</returns>
        public List<POCOEmpleado> ObtenerListaContratosEmpleado(bool Activos) {
            //saber si la lista esta en blanco si lo esta intento llenarla nuevamente
            if ((lEmpleados == null) || (lEmpleados.Count == 0)) {
                lEmpleados = ObtenerTodosEmpleados();

                if ((lEmpleados == null) || (lEmpleados.Count == 0)) {
                    return null;
                }
            }

            if (Activos)
                return lEmpleados.Where(p => p.Contratos.Where(a => a.Estado == EstadoContrato.Activo).ToList().Count > 0).ToList();
            
            return lEmpleados.Where(p => p.Contratos.Where(a => a.Estado == EstadoContrato.Vencido).ToList().Count > 0).ToList();
        }

        /// <summary>
        /// Eliminar todos los registros de una tabla
        /// </summary>
        /// <param name="nombreTabla">string nombreTabla</param>
        public void EliminarTodosRegistros(string nombreTabla) {
            try {
                string consulta = @"delete from " + nombreTabla;
                AyudanteSQL.EjecutarConsulta(consulta, mContext, lNotificaciones);

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
            }
        }

        public List<Huella> ObtenerHuellaUsuarioLocal(string GuidUsuario) => mContext.Huella.Where(p => p.GuidEmpleado == GuidUsuario).ToList();
        
        /// <summary>
        /// Saber si existe alguna sincronizacion en el sistema
        /// </summary>
        /// <returns>bool</returns>
        public bool ExisteSincronizacionAnterior() {
            try {
                if (mContext.Sincronizacion == null) return false;

                var ultimaSincronizacion = mContext.Sincronizacion.OrderBy(p => p.IdSincronizacion).FirstOrDefault();
                UltimaConexion = DateTime.Parse(ultimaSincronizacion.FechaSincronizacion);

                return ultimaSincronizacion == null ? false : true;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return false;
            }
        }

        /// <summary>
        /// Adiciona un nuevo registro de sincronizacion
        /// </summary>
        public void AdicionarSincronizacion() {
            try {
                //adicionar la sincronizacion a la tabla local
                mContext.Sincronizacion.Add(new Sincronizacion() {
                    FechaSincronizacion = DateTime.Now.ToString()
                });
                mContext.SaveChanges();

                ExistenDatos = true;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
            }
        }
        #endregion

        #region Leer Datos de WebServices
        /// <summary>
        /// Identifica el HardwareId
        /// </summary>
        /// <returns>Task<bool></returns>
        public async Task<bool> IdentificarHardwareOnline() {
            try {
                var respuesta = await new EnroladorServicioWebClient().IdentificarHardwareAsync(MyHardwareId);

                if (respuesta) {
                    mContext.Hardware.Add(new Hardware() {
                        GuidHardware = MyHardwareId.ToString()
                    });

                    mContext.SaveChanges();
                }
                return respuesta;

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return false;
            }
        }
        /// <summary>
        /// Devuelve un usuario del WebService llamando al metodo AutenticacionUsuarioAsync(nombreUsuario, claveUsuario)
        /// </summary>
        /// <param name="nombreUsuario">string nombreUsuario</param>
        /// <param name="claveUsuario">string claveUsuario</param>
        /// <returns>Task<UsuarioSerializable></returns>
        public async Task<POCOUsuario> AutenticarUsuario(string nombreUsuario, string claveUsuario) {
            try {
                var usuarioRetornado = await new EnroladorServicioWebClient().AutenticacionUsuarioAsync(nombreUsuario, claveUsuario);

                return usuarioRetornado;

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }
        /// <summary>
        /// Almacena en la BD Local SQLite los datos de las Huellas del usuario y en una variable HuellasUsuario
        /// </summary>
        public async Task<bool> HuellasDelUsuarioAutenticado() {

            HuellasUsuario = new List<Huella>();

            try {
                var GuidUsuario = Guid.Parse(UsuarioAutenticado.GuidUsuario);

                //Leer Huellas de Usuario
                POCOHuella[] huellas = await new EnroladorServicioWebClient().LeeHuellasDelUsuarioAsync(GuidUsuario);
                List<POCOHuella> sList = new List<POCOHuella>(huellas);

                //Eliminar Datos anteriores
                mContext.Database.ExecuteSqlCommand("delete from Usuario");
                mContext.Database.ExecuteSqlCommand("delete from Huella");

                //Salvar Usuario en SQLite
                mContext.Usuario.Add(new Usuario() {
                    GuidUsuario = UsuarioAutenticado.GuidUsuario,
                    NombreUsuario = UsuarioAutenticado.NombreUsuario,
                    ClaveUsuario = UsuarioAutenticado.ClaveUsuario,
                    SHAClave = UsuarioAutenticado.SHAClave
                });

                //Salvar Huella en SQLite
                foreach (var pHuella in sList) {
                    //transformar el tipo de objeto
                    var tHuella = TransformacionDatos.DePOCOHuellaAHuella(pHuella);

                    //almacenar en mi listado local y en contexto
                    HuellasUsuario.Add(tHuella);
                    mContext.Huella.Add(tHuella);
                }

                mContext.SaveChanges();

                if (sList.Count > 0) return true;

                return false;

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return false;
            }
        }

        /// <summary>
        /// Revalidar el usuario autenticado
        /// </summary>
        /// <returns>Task<POCOUsuario></returns>
        public async Task<POCOUsuario> RevalidarUsuarioAutenticado() {
            try {
                var usuario = TransformacionDatos.DeUsuarioAPOCOUsuario(UsuarioAutenticado);
                var usuarioRetornado = await new EnroladorServicioWebClient().RevalidarAsync(usuario);

                if (usuarioRetornado == null) return new POCOUsuario();

                return usuarioRetornado;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return new POCOUsuario();
            }
        }

        public async Task<List<POCOCadena>> CargarCadenas() {
            try {
                var GuidUsuario = Guid.Parse(UsuarioAutenticado.GuidUsuario);

                //Leer Cadenas de Usuario
                POCOCadena[] cadenas = await new EnroladorServicioWebClient().LeeCadenasDelUsuarioAsync(GuidUsuario);
                //Convertir Arreglo a Lista
                List<POCOCadena> sList = new List<POCOCadena>(cadenas);

                return sList;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }
        public async Task<List<POCOInstalacion>> CargarInstalaciones() {
            try {
                var GuidUsuario = Guid.Parse(UsuarioAutenticado.GuidUsuario);

                //Leer Instalaciones de Usuario
                POCOInstalacion[] instalaciones = await new EnroladorServicioWebClient().LeeInstalacionesDelUsuarioAsync(GuidUsuario);
                //Convertir Arreglo a Lista
                List<POCOInstalacion> sList = new List<POCOInstalacion>(instalaciones);

                return sList;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }
        public async Task<List<POCODispositivo>> CargarDispositivos() {
            try {
                var GuidUsuario = Guid.Parse(UsuarioAutenticado.GuidUsuario);

                //Leer Dispositivos de Usuario
                POCODispositivo[] dispositivos = await new EnroladorServicioWebClient().LeeDispositivoDelUsuarioAsync(GuidUsuario);
                //Convertir Arreglo a Lista
                List<POCODispositivo> sList = new List<POCODispositivo>(dispositivos);

                return sList;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }
        public async Task<List<POCOEmpresa>> CargarEmpresas() {
            try {
                var GuidUsuario = Guid.Parse(UsuarioAutenticado.GuidUsuario);

                //Leer Empresas de Usuario
                POCOEmpresa[] empresas = await new EnroladorServicioWebClient().LeeEmpresaDelUsuarioAsync(GuidUsuario);
                //Convertir Arreglo a Lista
                List<POCOEmpresa> sList = new List<POCOEmpresa>(empresas);

                return sList;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }
        public async Task<List<POCOCargo>> CargarCargos() {
            try {
                var GuidUsuario = Guid.Parse(UsuarioAutenticado.GuidUsuario);

                //Leer Cargos de Usuario
                POCOCargo[] cargos = await new EnroladorServicioWebClient().LeeCargosDelUsuarioAsync(GuidUsuario);
                //Convertir Arreglo a Lista
                List<POCOCargo> sList = new List<POCOCargo>(cargos);

                return sList;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }
        public async Task<List<POCOCuenta>> CargarCuentas() {
            try {
                var GuidUsuario = Guid.Parse(UsuarioAutenticado.GuidUsuario);

                //Leer Cuentas de Usuario
                POCOCuenta[] cuentas = await new EnroladorServicioWebClient().LeeCuentasDelUsuarioAsync(GuidUsuario);
                //Convertir Arreglo a Lista
                List<POCOCuenta> sList = new List<POCOCuenta>(cuentas);

                return sList;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }
        public async Task<List<POCOEmpleado>> CargarEmpleados() {
            try {

                //Leer Empleados de Usuario
                POCOEmpleado[] empleados = await new EnroladorServicioWebClient().LeeTodosEmpleadosAsync();
                //Convertir Arreglo a Lista
                List<POCOEmpleado> sList = new List<POCOEmpleado>(empleados);

                return sList;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }
        public async Task<List<POCOHuella>> CargarHuellas() {
            try {
                var GuidUsuario = Guid.Parse(UsuarioAutenticado.GuidUsuario);

                //Leer Huellas de Usuario
                POCOHuella[] huellas = await new EnroladorServicioWebClient().LeeTodasHuellasAsync();
                //Convertir Arreglo a Lista
                List<POCOHuella> sList = new List<POCOHuella>(huellas);

                return sList;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }
        public async Task<List<POCOEmpleadoDispositivo>> CargarEmpleadoDispositivos() {
            try {
                //Leer EmpleadoDispositivos de Usuario
                POCOEmpleadoDispositivo[] empleadoDispositivos = await new EnroladorServicioWebClient().LeeEmpleadosDispositivosAsync();
                //Convertir Arreglo a Lista
                List<POCOEmpleadoDispositivo> sList = new List<POCOEmpleadoDispositivo>(empleadoDispositivos);

                return sList;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }
        public async Task<List<POCOContrato>> CargarContratos() {
            try {
                var GuidUsuario = Guid.Parse(UsuarioAutenticado.GuidUsuario);

                //Leer Contratos de Usuario
                POCOContrato[] contratos = await new EnroladorServicioWebClient().LeeContratosDelUsuarioAsync(GuidUsuario);
                //Convertir Arreglo a Lista
                List<POCOContrato> sList = new List<POCOContrato>(contratos);

                return sList;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }
        public async Task<List<POCOEmpleadoTurnoServicioCasino>> CargarEmpleadoTurnoServicioCasinos() {
            try {
                var GuidUsuario = Guid.Parse(UsuarioAutenticado.GuidUsuario);

                //Leer EmpleadoTurnoServicioCasinos de Usuario
                POCOEmpleadoTurnoServicioCasino[] empleadoTurnoServicioCasinos = await new EnroladorServicioWebClient().LeeEmpleadoTurnoServicioCasinoAsync(GuidUsuario);
                //Convertir Arreglo a Lista
                List<POCOEmpleadoTurnoServicioCasino> sList = new List<POCOEmpleadoTurnoServicioCasino>(empleadoTurnoServicioCasinos);

                return sList;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }
        public async Task<List<POCOServicioCasino>> CargarServicioCasinos() {
            try {
                var GuidUsuario = Guid.Parse(UsuarioAutenticado.GuidUsuario);

                //Leer ServicioCasinos de Usuario
                POCOServicioCasino[] servicioCasinos = await new EnroladorServicioWebClient().LeeServicioCasinoDelUsuarioAsync(GuidUsuario);
                //Convertir Arreglo a Lista
                List<POCOServicioCasino> sList = new List<POCOServicioCasino>(servicioCasinos);

                return sList;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }
        public async Task<List<POCOTurnoServicio>> CargarTurnoServicios() {
            try {
                var GuidUsuario = Guid.Parse(UsuarioAutenticado.GuidUsuario);

                //Leer TurnoServicios de Usuario
                POCOTurnoServicio[] turnoServicios = await new EnroladorServicioWebClient().LeeTurnoServicioDelUsuarioAsync(GuidUsuario);
                //Convertir Arreglo a Lista
                List<POCOTurnoServicio> sList = new List<POCOTurnoServicio>(turnoServicios);

                return sList;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }


        #endregion

        #region Notificacion
        public UCNotificaciones ObtenerUCNotificaciones(string enunciadoNotificaciones) {

            if (enunciadoNotificaciones == String.Empty) {
                int cantidadNotificaciones = lNotificaciones.Count;
                string Ss = lNotificaciones.Count > 1 ? "es" : "";
                enunciadoNotificaciones = "Tiene " + cantidadNotificaciones + " Notificacion" + Ss;
            }
            
            UCNotificaciones uCNotificaciones = new UCNotificaciones(this, enunciadoNotificaciones) {
                Dock = System.Windows.Forms.DockStyle.Fill
            };
            return uCNotificaciones;
        }

        /// <summary>
        /// Adicionar una notificacion de cadenas vacias
        /// </summary>
        /// <param name="mensajeAdicional">string mensajeAdicional</param>
        public void AdicionarNotificacionListadoVacio(string mensajeAdicional) {
            DateTime ahora = DateTime.Now;

            string mensaje = "Cadena Vacia";
            if (mensajeAdicional != String.Empty) {
                mensaje += " - " + mensajeAdicional;
            }

            Notificacion notificacion = new Notificacion() {
                IdNotificacion = ahora.ToBinary().ToString(),
                FechaNotificacion = ahora,
                MensajeNotificacion = mensaje,
                Tipo = TipoNotificacion.Cuidado,
                ImagenDeNotificacion = Properties.Resources.warning_32x32
            };

            lNotificaciones.Add(notificacion);
        }

        /// <summary>
        /// Adicionar notificacion de proceso terminado con exito
        /// </summary>
        /// <param name="mensajeAdicional">string mensajeAdicional</param>
        public void AdicionarNotificacionProcesoRealizadoCorrectamente(string mensajeAdicional) {

            DateTime ahora = DateTime.Now;

            string mensaje = "Proceso realizado correctamente...";

            if (mensajeAdicional != String.Empty) {
                mensaje += " - " + mensajeAdicional;
            }

            Notificacion notificacion = new Notificacion() {
                IdNotificacion = ahora.ToBinary().ToString(),
                FechaNotificacion = ahora,
                MensajeNotificacion = mensaje,
                Tipo = TipoNotificacion.Exito,
                ImagenDeNotificacion = Properties.Resources.apply_32x32
            };

            lNotificaciones.Add(notificacion);
        }
        #endregion
        #endregion
    }
}