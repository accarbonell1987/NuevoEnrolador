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
using System.Linq.Expressions;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Common;
using System.Data.SQLite;
using System.Drawing;
using System.Transactions;

namespace EnroladorStandAloneV2.CapaLogicaNegocio {
    public class NegocioEnrolador {
        #region Atributos
        public DateTime UltimaConexion { get; set; }
        public bool ExistenDatos { get; set; }
        public bool Actualizando { get; set; }
        public bool Online { get; set; }
        public EstadoUsoSistema EstadoUsoSistema { get; set; }
        public Guid MyHardwareId { get; set; }

        private const int diasRecomendandosParaConectar = 15;
        public static int DiasRecomendandosParaConectar => diasRecomendandosParaConectar;

        public Usuario UsuarioAutenticado { get; set; }
        public List<Huella> HuellasUsuario { get; set; }

        public Huellero mHuellero { get; set; }
        public SQLiteEnrollEntities mContext { get; set; }
        public List<POCONotificacion> lNotificaciones { get; set; }
        public int CantidadElementosNotificaciones { get; set; }
        public int CantidadAccionesPorEnviar { get; set; }

        public List<POCOEmpleado> lEmpleados { get; set; }
        #endregion

        #region Constructor
        public NegocioEnrolador() {
            mContext = new SQLiteEnrollEntities();
            mContext.Configuration.AutoDetectChangesEnabled = true;
            mContext.Configuration.ValidateOnSaveEnabled = true;

            lNotificaciones = new List<POCONotificacion>();
            CantidadElementosNotificaciones = 0;

            Online = false;
            ExistenDatos = false;
            Actualizando = false;
        }
        #endregion

        #region Metodos

        #region SQLite
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

        public void CargarCantidadDeAccionesPendientes() {
            try {
                CantidadAccionesPorEnviar = mContext.PorSincronizar.Count();
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
            }
        }

        public Usuario ObtenerUsuarioLocal() {
            try {
                var usuario = mContext.Usuario.FirstOrDefault();
                if (usuario == null) return null;

                HuellasUsuario = ObtenerHuellaUsuarioLocal(usuario.GuidUsuario);
                CargarCantidadDeAccionesPendientes();

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
                var todosTurnoServicioCasino = mContext.EmpleadoTurnoServicioCasino.ToList();

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
                    }

                    //cargar huellas del empleado
                    var huellas = todasHuellas.Where(p => p.GuidEmpleado == empleado.GuidEmpleado).ToList();

                    //adicionar huellas al empleado
                    List<POCOHuella> lPOCOHuellas = new List<POCOHuella>();
                    foreach (var huella in huellas) {
                        pocoEmpleado.Huellas.Add(TransformacionDatos.DeHuellaAPOCOHuella(huella));
                    }

                    //cargar dispositivos del empleado
                    var dispEmpleado = todosEmpleadosDispositivos.Where(p => p.GuidEmpleado == empleado.GuidEmpleado);

                    //List<Dispositivo> dispositivos = new List<Dispositivo>();
                    //foreach (var dEmpleado in dispEmpleado) {
                    //    var dispositivo = todosDispositivos.First(p => p.GuidDispositivo == dEmpleado.GuidDispositivo);
                    //    dispositivos.Add(dispositivo);
                    //}

                    var dispositivos = todosDispositivos.Where(p => dispEmpleado.Any(a => a.GuidDispositivo == p.GuidDispositivo)).Select(p => p).ToList();

                    pocoEmpleado.Dispositivos = new List<POCODispositivo>();
                    //adicionar dispositivos al empleado
                    foreach (var dispositivo in dispositivos) {
                        var pocoDispositivo = TransformacionDatos.DeDispositivoAPOCODispositivo(dispositivo);

                        var instalacion = ObtenerInstalacionDelDispositivo(pocoDispositivo.GuidInstalacion);
                        var cadena = ObtenerCadenaDeInstalacion(instalacion.GuidCadena);

                        pocoDispositivo.NombreCadena = cadena.NombreCadena;
                        pocoDispositivo.NombreInstalacion = instalacion.NombreInstalacion;

                        pocoEmpleado.Dispositivos.Add(pocoDispositivo);
                    }

                    //cargar Turnos Servicio Casino
                    var turnoServicioCasinoEmpleado = todosTurnoServicioCasino.Where(p => p.GuidEmpleado == empleado.GuidEmpleado).ToList();

                    //obtener todos los turnos del empleado
                    List<TurnoServicio> turnoServicios = new List<TurnoServicio>();
                    foreach (var turnoEmpleado in turnoServicioCasinoEmpleado) {
                        var turno = mContext.TurnoServicio.First(p => p.GuidTurnoServicio == turnoEmpleado.GuidTurnoServicio);
                        turnoServicios.Add(turno);
                    }
                    
                    pocoEmpleado.TurnoServicioCasino = new List<POCOEmpleadoTurnoServicioCasino>();
                    //adicionar la relacion de turno servicio casino al empleado
                    foreach (var turno in turnoServicios) {
                        var pocoTurnoServicio = TransformacionDatos.DeTurnoServicioAPOCOTurnoServicio(turno);

                        var servicio = ObtenerServicio(pocoTurnoServicio.GuidServicio);
                        var instalacion = ObtenerInstalacion(servicio.GuidCasino);

                        pocoEmpleado.TurnoServicioCasino.Add(new POCOEmpleadoTurnoServicioCasino() {
                            GuidEmpleado = Guid.Parse(empleado.GuidEmpleado),
                            GuidTurnoServicio = pocoTurnoServicio.GuidTurnoServicio,
                            HoraInicio = pocoTurnoServicio.HoraInicio,
                            HoraFin = pocoTurnoServicio.HoraFin,
                            Vigente = pocoTurnoServicio.Vigente,
                            NombreCasino = instalacion.NombreInstalacion,
                            NombreServicio = servicio.NombreServicioCasino,
                            NombreTurno = pocoTurnoServicio.NombreTurnoServicio
                        });
                    }

                    lPOCOEmpleados.Add(pocoEmpleado);
                }

            lEmpleados = lPOCOEmpleados;
            return lPOCOEmpleados;

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }
        public List<POCOEmpleado> ObtenerTodosEmpleadosParaGrid() {
            try {
                List<POCOEmpleado> lPOCOEmpleados = new List<POCOEmpleado>();
                //cargar empleados locales
                var empleados = mContext.Empleado.ToList();
                var contratos = mContext.Contrato.ToList();
                var empresas = mContext.Empresa.ToList();
                var cuentas = mContext.Cuenta.ToList();
                var cargos = mContext.Cargo.ToList();

                foreach (var empleado in empleados) {
                    //transformar el empleado a poco
                    var pocoEmpleado = TransformacionDatos.DeEmpleadoAPOCOEmpleado(empleado);

                    pocoEmpleado.TipoIdentificacion = pocoEmpleado.TieneContraseña ? TipoIdentificacion.Clave.ToString() : TipoIdentificacion.Huella.ToString();

                    //cargar contratos del empleado
                    var contratosDelEmpleado = contratos.Where(p => p.GuidEmpleado == empleado.GuidEmpleado).ToList();

                    if (contratosDelEmpleado.Count > 0) {
                        //adicionar contratos al empleado
                        List<POCOContrato> lPOCOContratos = new List<POCOContrato>();

                        foreach (var contrato in contratosDelEmpleado) {
                            POCOContrato pContrato = TransformacionDatos.DeContratoAPOCOContrato(contrato);

                            var empresa = TransformacionDatos.DeEmpresaAPOCOEmpresa(empresas.FirstOrDefault(p => p.GuidEmpresa == pContrato.GuidEmpresa.ToString()));
                            pContrato.NombreEmpresa = empresa == null ? "Sin Empresa" : empresa.NombreEmpresa;

                            var cuenta = TransformacionDatos.DeCuentaAPOCOCuenta(cuentas.FirstOrDefault(p => p.GuidCuenta == pContrato.GuidCuenta.ToString()));
                            pContrato.NombreCuenta = cuenta == null ? "Sin Cuenta" : cuenta.NombreCuenta;

                            var cargo = TransformacionDatos.DeCargoAPOCOCargo(cargos.FirstOrDefault(p => p.GuidCargo == pContrato.GuidCargo.ToString()));
                            pContrato.NombreCargo = cargo == null ? "Sin Cargo" : cargo.NombreCargo;

                            pocoEmpleado.Contratos.Add(pContrato);
                        }
                    }
                    
                    lPOCOEmpleados.Add(pocoEmpleado);
                }

                lEmpleados = lPOCOEmpleados;
                return lPOCOEmpleados;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }
        public POCOEmpleado ObtenerEmpleadoDeBD(string RUT) {
            try {
                //cargar empleados locales
                var empleado = mContext.Empleado.FirstOrDefault(p => p.RUT == RUT);
                if (empleado == null) return null;

                //transformar el empleado a poco
                var pocoEmpleado = TransformacionDatos.DeEmpleadoAPOCOEmpleado(empleado);

                pocoEmpleado.TipoIdentificacion = pocoEmpleado.TieneContraseña ? TipoIdentificacion.Clave.ToString() : TipoIdentificacion.Huella.ToString();

                //cargar contratos del empleado
                var contratos = mContext.Contrato.Where(p => p.GuidEmpleado == empleado.GuidEmpleado).ToList();

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
                }

                //cargar huellas del empleado
                var huellas = mContext.Huella.Where(p => p.GuidEmpleado == empleado.GuidEmpleado).ToList();
                //adicionar huellas al empleado
                List<POCOHuella> lPOCOHuellas = new List<POCOHuella>();
                foreach (var huella in huellas)
                    pocoEmpleado.Huellas.Add(TransformacionDatos.DeHuellaAPOCOHuella(huella));
                //cargar dispositivos del empleado
                var dispEmpleado = mContext.EmpleadoDispositivo.Where(p => p.GuidEmpleado == empleado.GuidEmpleado);
                var dispositivos = mContext.Dispositivo.Where(p => dispEmpleado.Any(a => a.GuidDispositivo == p.GuidDispositivo)).Select(p => p).ToList();

                pocoEmpleado.Dispositivos = new List<POCODispositivo>();
                //adicionar dispositivos al empleado
                foreach (var dispositivo in dispositivos) {
                    var pocoDispositivo = TransformacionDatos.DeDispositivoAPOCODispositivo(dispositivo);

                    var instalacion = ObtenerInstalacionDelDispositivo(pocoDispositivo.GuidInstalacion);
                    var cadena = ObtenerCadenaDeInstalacion(instalacion.GuidCadena);

                    pocoDispositivo.NombreCadena = cadena.NombreCadena;
                    pocoDispositivo.NombreInstalacion = instalacion.NombreInstalacion;

                    pocoEmpleado.Dispositivos.Add(pocoDispositivo);
                }

                //cargar Turnos Servicio Casino
                var turnoServicioCasinoEmpleado = mContext.EmpleadoTurnoServicioCasino.Where(p => p.GuidEmpleado == empleado.GuidEmpleado).ToList();
                //obtener todos los turnos del empleado
                List<TurnoServicio> turnoServicios = new List<TurnoServicio>();
                foreach (var turnoEmpleado in turnoServicioCasinoEmpleado) {
                    var turno = mContext.TurnoServicio.First(p => p.GuidTurnoServicio == turnoEmpleado.GuidTurnoServicio);
                    turnoServicios.Add(turno);
                }

                pocoEmpleado.TurnoServicioCasino = new List<POCOEmpleadoTurnoServicioCasino>();
                //adicionar la relacion de turno servicio casino al empleado
                foreach (var turno in turnoServicios) {
                    var pocoTurnoServicio = TransformacionDatos.DeTurnoServicioAPOCOTurnoServicio(turno);

                    var servicio = ObtenerServicio(pocoTurnoServicio.GuidServicio);
                    var instalacion = ObtenerInstalacion(servicio.GuidCasino);

                    pocoEmpleado.TurnoServicioCasino.Add(new POCOEmpleadoTurnoServicioCasino() {
                        GuidEmpleado = Guid.Parse(empleado.GuidEmpleado),
                        GuidTurnoServicio = pocoTurnoServicio.GuidTurnoServicio,
                        HoraInicio = pocoTurnoServicio.HoraInicio,
                        HoraFin = pocoTurnoServicio.HoraFin,
                        Vigente = pocoTurnoServicio.Vigente,
                        NombreCasino = instalacion.NombreInstalacion,
                        NombreServicio = servicio.NombreServicioCasino,
                        NombreTurno = pocoTurnoServicio.NombreTurnoServicio
                    });
                }

                return pocoEmpleado;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }

        /// <summary>
        /// Obtener un string de NombreEmpresa - NombreCuenta - NombreCargo de cada contrato del empleado
        /// </summary>
        /// <param name="pRut">string pRut</param>
        /// <returns>string</returns>
        public List<POCOEmpleado> FiltrarEmpleadosPorContratos(bool Activos) {
            if (Activos)
                //foreach (var empleado in lEmpleados.Where(p => p.Contratos.Where(a => a.Estado == EstadoContrato.Activo).ToList().Count > 0))
                //    yield return empleado;
                return lEmpleados.Where(p => p.Contratos.Where(a => a.Estado == EstadoContrato.Activo).ToList().Count > 0).ToList();
            else
                //foreach (var empleado in lEmpleados.Where(p => p.Contratos.Where(a => a.Estado == EstadoContrato.Vencido).ToList().Count > 0))
                //    yield return empleado;
                return lEmpleados.Where(p => p.Contratos.Where(a => a.Estado == EstadoContrato.Vencido).ToList().Count > 0).ToList();
        }
        public int CantidadDeEmpleados() {
            var empleado = mContext.Empleado.FirstOrDefault();
            if (empleado == null) return 0;

            return mContext.Empleado.Count();
        }

        public List<POCOEmpresa> ObtenerTodasEmpresas() {
            try {
                List<POCOEmpresa> lPOCOEmpresas = new List<POCOEmpresa>();
                var empresas = mContext.Empresa.ToList();

                foreach (var empresa in empresas) {
                    var pocoEmpresa = TransformacionDatos.DeEmpresaAPOCOEmpresa(empresa);

                    pocoEmpresa.Cuentas = ObtenerTodasCuentas().Where(p => p.GuidEmpresa == pocoEmpresa.GuidEmpresa).ToList();
                    pocoEmpresa.Cargos = ObtenerTodosCargos().Where(p => p.GuidEmpresa == pocoEmpresa.GuidEmpresa).ToList();

                    lPOCOEmpresas.Add(pocoEmpresa);
                }

                return lPOCOEmpresas;

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }
        public POCOEmpresa ObtenerEmpresa(Guid GuidEmpresa) {
            try {
                var empresa = mContext.Empresa.FirstOrDefault(p => p.GuidEmpresa == GuidEmpresa.ToString());

                if (empresa == null) return null;

                var pocoEmpresa = TransformacionDatos.DeEmpresaAPOCOEmpresa(empresa);

                pocoEmpresa.Cuentas = ObtenerTodasCuentas().Where(p => p.GuidEmpresa == pocoEmpresa.GuidEmpresa).ToList();
                pocoEmpresa.Cargos = ObtenerTodosCargos().Where(p => p.GuidEmpresa == pocoEmpresa.GuidEmpresa).ToList();

                return pocoEmpresa;
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

        public POCOContrato ObtenerContrato(Guid GuidEmpleado, Guid GuidContrato) {
            try {
                var todosContratos = mContext.Contrato.Where(p => p.GuidEmpleado == GuidEmpleado.ToString()).ToList();
                //cargar contratos del empleado
                var contrato = todosContratos.FirstOrDefault(p => p.GuidContrato == GuidContrato.ToString());

                if (contrato == null) return null;

                POCOContrato pContrato = TransformacionDatos.DeContratoAPOCOContrato(contrato);

                var empresa = TransformacionDatos.DeEmpresaAPOCOEmpresa(mContext.Empresa.FirstOrDefault(p => p.GuidEmpresa == pContrato.GuidEmpresa.ToString()));
                pContrato.NombreEmpresa = empresa == null ? "Sin Empresa" : empresa.NombreEmpresa;

                var cuenta = TransformacionDatos.DeCuentaAPOCOCuenta(mContext.Cuenta.FirstOrDefault(p => p.GuidCuenta == pContrato.GuidCuenta.ToString()));
                pContrato.NombreCuenta = cuenta == null ? "Sin Cuenta" : cuenta.NombreCuenta;

                var cargo = TransformacionDatos.DeCargoAPOCOCargo(mContext.Cargo.FirstOrDefault(p => p.GuidCargo == pContrato.GuidCargo.ToString()));
                pContrato.NombreCargo = cargo == null ? "Sin Cargo" : cargo.NombreCargo;

                return pContrato;
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
                    //buscar todos los servicios de la instalacion
                    pocoInstalacion.ServiciosDelCasino = ObtenerTodosServiciosDelCasino(pocoInstalacion.GuidInstalacion);
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
                List<POCOInstalacion> lPOCOInstalaciones = new List<POCOInstalacion>();
                var instalaciones = mContext.Instalacion.Where(p => p.GuidCadena == GuidCadena.ToString()).ToList();

                foreach (var instalacion in instalaciones) {
                    var pocoInstalacion = TransformacionDatos.DeInstalacionAPOCOInstalacion(instalacion);
                    //buscar nombre de cadena que pertenece la instalacion
                    pocoInstalacion.NombreCadena = ObtenerCadenaDeInstalacion(pocoInstalacion.GuidCadena).NombreCadena;
                    //buscar todos los dispositivos de la instalacion
                    pocoInstalacion.Dispositivos = ObtenerTodosDispositivosDeInstalacion(pocoInstalacion.GuidInstalacion);
                    //buscar todos los servicios de la instalacion
                    pocoInstalacion.ServiciosDelCasino = ObtenerTodosServiciosDelCasino(pocoInstalacion.GuidInstalacion);

                    lPOCOInstalaciones.Add(pocoInstalacion);
                }

                return lPOCOInstalaciones;
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
                //buscar todos los servicios de la instalacion
                pocoInstalacion.ServiciosDelCasino = ObtenerTodosServiciosDelCasino(pocoInstalacion.GuidInstalacion);

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

        /// <summary>
        /// Obtener todos los servicios del casino (Casino = Instalacion)
        /// </summary>
        /// <param name="Casino">Guid Casino</param>
        /// <returns>List<POCOServicioCasino></returns>
        public List<POCOServicioCasino> ObtenerTodosServiciosDelCasino(Guid GuidCasino) {
            try {
                List<POCOServicioCasino> lPOCOServicios = new List<POCOServicioCasino>();
                var servicios = mContext.ServicioCasino.Where(p => p.GuidCasino == GuidCasino.ToString()).ToList();

                foreach (var servicio in servicios) {
                    var pocoServicio = TransformacionDatos.DeServicioCasinoAPOCOServicioCasino(servicio);
                    pocoServicio.TurnosDelServicio = ObtenerTodosLosTurnosDelServicio(pocoServicio.GuidServicioCasino);

                    lPOCOServicios.Add(pocoServicio);
                }

                return lPOCOServicios;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }
        public POCOServicioCasino ObtenerServicio(Guid GuidServicio) {
            try {
                var servicio = mContext.ServicioCasino.FirstOrDefault(p => p.GuidServicioCasino == GuidServicio.ToString());
                if (servicio == null) return null;

                var pocoServicio = TransformacionDatos.DeServicioCasinoAPOCOServicioCasino(servicio);
                //buscar turnos del servicio
                pocoServicio.TurnosDelServicio = ObtenerTodosLosTurnosDelServicio(pocoServicio.GuidServicioCasino);

                return pocoServicio;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }

        public List<POCOTurnoServicio> ObtenerTodosLosTurnosDelServicio(Guid GuidServicio) {
            try {
                List<POCOTurnoServicio> lPOCOTurnos = new List<POCOTurnoServicio>();
                var turnos = mContext.TurnoServicio.Where(p => p.GuidServicio == GuidServicio.ToString()).ToList();

                foreach (var turno in turnos) {
                    var pocoTurno = TransformacionDatos.DeTurnoServicioAPOCOTurnoServicio(turno);
                    lPOCOTurnos.Add(pocoTurno);
                }

                return lPOCOTurnos;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }
        public POCOTurnoServicio ObtenerTurnoServicio(Guid GuidTurno) {
            try {
                var turnoServicio = mContext.TurnoServicio.FirstOrDefault(p => p.GuidTurnoServicio == GuidTurno.ToString());
                if (turnoServicio == null) return null;

                var pocoTurnoServicio = TransformacionDatos.DeTurnoServicioAPOCOTurnoServicio(turnoServicio);
                return pocoTurnoServicio;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                return null;
            }
        }

        public List<POCOEmpleadoTurnoServicioCasino> ObtenerTodosEmpleadoTurnoServicioCasinoDelEmpleado(Guid GuidEmpleado) {
            try {
                List<POCOEmpleadoTurnoServicioCasino> lPOCOEmpleadoTurnoServicioCasino = new List<POCOEmpleadoTurnoServicioCasino>();
                var empleadoTurnoServicioCasino = mContext.EmpleadoTurnoServicioCasino.Where(p => p.GuidEmpleado == GuidEmpleado.ToString()).ToList();

                foreach (var eTSC in empleadoTurnoServicioCasino) {
                    var pocoETSC = TransformacionDatos.DeEmpleadoTurnoServicioCasinoAPOCOEmpleadoTurnoServicioCasino(eTSC);

                    var turno = ObtenerTurnoServicio(pocoETSC.GuidTurnoServicio);
                    var servicio = ObtenerServicio(turno.GuidServicio);
                    var casino = ObtenerInstalacion(servicio.GuidCasino); //Casino = Instalacion

                    pocoETSC.NombreTurno = turno.NombreTurnoServicio;
                    pocoETSC.NombreServicio = servicio.NombreServicioCasino;
                    pocoETSC.NombreCasino = casino.NombreInstalacion;
                    pocoETSC.HoraInicio = turno.HoraInicio;
                    pocoETSC.HoraFin = turno.HoraFin;
                    pocoETSC.Vigente = turno.Vigente;

                    lPOCOEmpleadoTurnoServicioCasino.Add(pocoETSC);
                }

                return lPOCOEmpleadoTurnoServicioCasino;
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
        /// <summary>
        /// Salver en la BD el Lote
        /// </summary>
        /// <param name="listadoEntidades">IEnumerable<Object> listadoEntidades</param>
        public void SalvarLote(IEnumerable<Object> listadoEntidades) {
            try {
                mContext.BulkInsert(listadoEntidades);
                mContext.BulkSaveChanges();
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
            }
        }
        public void SalvarCambios(POCOEmpleado pocoEmpleado) {
            try {
                var pocoTurnoServiciosCasinosParaAdicionar = pocoEmpleado.TurnoServicioCasino.Where(p => p.EstadoObjeto != EstadoObjeto.Almacenado).ToList();
                var pocoEmpleadoDispositivosParaAdicionar = pocoEmpleado.Dispositivos.Where(p => p.EstadoObjeto != EstadoObjeto.Almacenado).ToList();
                var pocoHuellasParaAdicionar = pocoEmpleado.Huellas.Where(p => p.EstadoObjeto != EstadoObjeto.Almacenado).ToList();
                var pocoContratosParaAdicionar = pocoEmpleado.Contratos.Where(p => p.EstadoObjeto == EstadoObjeto.Almacenar).ToList();

                var trans = mContext.Database.BeginTransaction();
                try {
                    //convertir empleado
                    var empleado = SQLiteModificarEmpleado(pocoEmpleado);

                    //recorro todos los turnos a insertar y los inserto pue...
                    foreach (var turno in pocoTurnoServiciosCasinosParaAdicionar) {
                        turno.GuidEmpleado = Guid.Parse(empleado.GuidEmpleado);
                        SQLiteModificarEmpleadoTurnoServicio(turno);
                    }

                    //recorro todos los dispositivos a insertar y los inserto pue...
                    foreach (var dispositivo in pocoEmpleadoDispositivosParaAdicionar) {
                        var empleadoDispositivo = new POCOEmpleadoDispositivo() {
                            GuidEmpleado = Guid.Parse(empleado.GuidEmpleado),
                            GuidDispositivo = dispositivo.GuidDispositivo,
                            EstadoObjeto = dispositivo.EstadoObjeto
                        };
                        SQLiteModificarEmpleadoDispostivo(empleadoDispositivo);
                    }

                    //recorro todas las huellas a insertar y las inserto...then...
                    foreach (var huella in pocoHuellasParaAdicionar) {
                        huella.GuidEmpleado = Guid.Parse(empleado.GuidEmpleado);
                        SQLiteModificarHuella(huella);
                    }

                    //...
                    foreach (var contratos in pocoContratosParaAdicionar) {
                        contratos.GuidEmpleado = Guid.Parse(empleado.GuidEmpleado);
                        SQLiteModificarContrato(contratos);
                    }


                    trans.Commit();

                    AdicionarNotificacion("Empleado: " + empleado.Nombres + " " + empleado.Apellidos + " RUT: " + empleado.RUT + " Salvado Correctamente", TipoNotificacion.Exito);
                } catch (Exception eX) {
                    trans.Rollback();
                    AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
            }
        }

        /// <summary>
        /// Adiciona un empleado chequeando si existe o no en la BD si existe lo actualiza
        /// </summary>
        /// <param name="pocoEmpleado">POCOEmpleado pocoEmpleado</param>
        /// <returns>Empleado</returns>
        public Empleado SQLiteModificarEmpleado(POCOEmpleado pocoEmpleado) {
            try {
                var empleado = TransformacionDatos.DePOCOEmpleadoAEmpleado(pocoEmpleado);

                if (mContext.Empleado.FirstOrDefault(p => p.GuidEmpleado == pocoEmpleado.GuidEmpleado.ToString()) == null) {
                    //Insertar empleado
                    empleado.Sincronizado = (int)TipoSincronizacion.Insertar;
                    mContext.Empleado.Add(empleado);
                    mContext.SaveChanges();
                    //crear registro para sincronizar y la accion
                    CrearPorSincronizarYAccion(empleado);
                    return empleado;
                } else {
                    //Modificar empleado
                    var bdEmpleado = mContext.Empleado.FirstOrDefault(p => p.GuidEmpleado == pocoEmpleado.GuidEmpleado.ToString());

                    bdEmpleado.NumeroTelefono = empleado.NumeroTelefono;
                    bdEmpleado.Correo = empleado.Correo;
                    bdEmpleado.TieneContrasena = empleado.TieneContrasena;
                    bdEmpleado.Contrasena = empleado.Contrasena;
                    bdEmpleado.Sincronizado = (int)TipoSincronizacion.Modificar;
                    mContext.SaveChanges();
                    //crear registro para sincronizar y la accion
                    CrearPorSincronizarYAccion(bdEmpleado);
                    return bdEmpleado;
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                throw eX;
            }
        }
        /// <summary>
        /// Adiciona un EmpleadoDispositivo chequeando si existe o no en la BD si existe lo actualiza
        /// </summary>
        /// <param name="pocoEmpleado">POCOEmpleadoDispositivo pocoEmpleadoDispositivo</param>
        /// <returns>EmpleadoDispositivo</returns>
        public void SQLiteModificarEmpleadoDispostivo(POCOEmpleadoDispositivo pocoEmpleadoDispositivo) {
            try {
                var empleadoDispositivo = TransformacionDatos.DePOCOEmpleadoDispositivoAEmpleadoDispositivo(pocoEmpleadoDispositivo);
                
                if (pocoEmpleadoDispositivo.EstadoObjeto == EstadoObjeto.Almacenar) {
                    //Adicionar empleadoDispositivo
                    if (mContext.EmpleadoDispositivo.FirstOrDefault(p => (p.GuidEmpleado == pocoEmpleadoDispositivo.GuidEmpleado.ToString()) && (p.GuidDispositivo == pocoEmpleadoDispositivo.GuidDispositivo.ToString())) == null) {

                        empleadoDispositivo.Sincronizado = (int)TipoSincronizacion.Insertar;
                        mContext.EmpleadoDispositivo.Add(empleadoDispositivo);
                        mContext.SaveChanges();
                        //crear registro para sincronizar y la accion
                        CrearPorSincronizarYAccion(empleadoDispositivo);
                    } else {
                        //Modificar empleadoDispositivo
                        var bdEmpleadoDispositivo = mContext.EmpleadoDispositivo.FirstOrDefault(p => (p.GuidEmpleado == pocoEmpleadoDispositivo.GuidEmpleado.ToString()) && (p.GuidDispositivo == pocoEmpleadoDispositivo.GuidDispositivo.ToString()));

                        bdEmpleadoDispositivo.GuidDispositivo = empleadoDispositivo.GuidDispositivo;
                        bdEmpleadoDispositivo.GuidEmpleado = empleadoDispositivo.GuidEmpleado;
                        bdEmpleadoDispositivo.Sincronizado = (int)TipoSincronizacion.Modificar;
                        mContext.SaveChanges();
                        //crear registro para sincronizar y la accion
                        CrearPorSincronizarYAccion(empleadoDispositivo);
                    }
                } else if (pocoEmpleadoDispositivo.EstadoObjeto == EstadoObjeto.Eliminar) {
                    //Eliminar empleadoDispositivo
                    var bdEmpleadoDispositivo = mContext.EmpleadoDispositivo.FirstOrDefault(p => (p.GuidEmpleado == pocoEmpleadoDispositivo.GuidEmpleado.ToString()) && (p.GuidDispositivo == pocoEmpleadoDispositivo.GuidDispositivo.ToString()));
                    
                    bdEmpleadoDispositivo.Sincronizado = (int)TipoSincronizacion.Eliminar;

                    mContext.EmpleadoDispositivo.Remove(bdEmpleadoDispositivo);
                    mContext.SaveChanges();
                    //crear registro para sincronizar y la accion
                    CrearPorSincronizarYAccion(empleadoDispositivo);
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                throw eX;
            }
        }
        /// <summary>
        /// Adiciona un EmpleadoTurnoServicioCasino chequeando si existe o no en la BD si existe lo actualiza
        /// </summary>
        /// <param name="pocoEmpleadoTurnoServicioCasino">POCOEmpleadoTurnoServicioCasino pocoEmpleadoTurnoServicioCasino</param>
        /// <returns>EmpleadoTurnoServicioCasino</returns>
        public void SQLiteModificarEmpleadoTurnoServicio(POCOEmpleadoTurnoServicioCasino pocoEmpleadoTurnoServicioCasino) {
            try {
                var empleadoTurnoServicioCasino = TransformacionDatos.DePOCOEmpleadoTurnoServicioCasinoAEmpleadoTurnoServicioCasino(pocoEmpleadoTurnoServicioCasino);

                if (pocoEmpleadoTurnoServicioCasino.EstadoObjeto == EstadoObjeto.Almacenar) {
                    //Adicionar empleadoTurnoServicioCasino
                    if (mContext.EmpleadoTurnoServicioCasino.FirstOrDefault(p => (p.GuidEmpleado == pocoEmpleadoTurnoServicioCasino.GuidEmpleado.ToString()) && (p.GuidTurnoServicio == pocoEmpleadoTurnoServicioCasino.GuidTurnoServicio.ToString())) == null) {

                        empleadoTurnoServicioCasino.Sincronizado = (int)TipoSincronizacion.Insertar;

                        mContext.EmpleadoTurnoServicioCasino.Add(empleadoTurnoServicioCasino);
                        mContext.SaveChanges();
                        //crear registro para sincronizar y la accion
                        CrearPorSincronizarYAccion(empleadoTurnoServicioCasino);

                    } else {
                        //Modificar empleadoTurnoServicioCasino
                        var bdEmpleadoTurnoServicioCasino = mContext.EmpleadoTurnoServicioCasino.FirstOrDefault(p => (p.GuidEmpleado == pocoEmpleadoTurnoServicioCasino.GuidEmpleado.ToString()) && (p.GuidTurnoServicio == pocoEmpleadoTurnoServicioCasino.GuidTurnoServicio.ToString()));

                        bdEmpleadoTurnoServicioCasino.GuidTurnoServicio = empleadoTurnoServicioCasino.GuidTurnoServicio;
                        bdEmpleadoTurnoServicioCasino.GuidEmpleado = empleadoTurnoServicioCasino.GuidEmpleado;
                        bdEmpleadoTurnoServicioCasino.Sincronizado = (int)TipoSincronizacion.Modificar;

                        mContext.SaveChanges();
                        //crear registro para sincronizar y la accion
                        CrearPorSincronizarYAccion(bdEmpleadoTurnoServicioCasino);
                    }
                } else if (pocoEmpleadoTurnoServicioCasino.EstadoObjeto == EstadoObjeto.Eliminar) {
                    //Eliminar empleadoTurnoServicioCasino
                    var bdEmpleadoTurnoServicioCasino = mContext.EmpleadoTurnoServicioCasino.FirstOrDefault(p => (p.GuidEmpleado == pocoEmpleadoTurnoServicioCasino.GuidEmpleado.ToString()) && (p.GuidTurnoServicio == pocoEmpleadoTurnoServicioCasino.GuidTurnoServicio.ToString()));

                    bdEmpleadoTurnoServicioCasino.Sincronizado = (int)TipoSincronizacion.Eliminar;

                    mContext.EmpleadoTurnoServicioCasino.Remove(bdEmpleadoTurnoServicioCasino);
                    mContext.SaveChanges();
                    //crear registro para sincronizar y la accion
                    CrearPorSincronizarYAccion(bdEmpleadoTurnoServicioCasino);
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                throw eX;
            }
        }
        /// <summary>
        /// Adiciona una huella chequeando si existe o no en la BD si existe lo actualiza
        /// </summary>
        /// <param name="pocoHuella">POCOHuella pocoHuella</param>
        /// <returns>Huella</returns>
        public void SQLiteModificarHuella(POCOHuella pocoHuella) {
            try {
                var huella = TransformacionDatos.DePOCOHuellaAHuella(pocoHuella);

                if (pocoHuella.EstadoObjeto == EstadoObjeto.Almacenar) {
                    //Adicionar Huella
                    if (mContext.Huella.FirstOrDefault(p => (p.GuidEmpleado == pocoHuella.GuidEmpleado.ToString()) && (p.GuidHuella == pocoHuella.GuidHuella.ToString())) == null) {

                        huella.GuidHuella = Guid.NewGuid().ToString();
                        huella.Sincronizado = (int)TipoSincronizacion.Insertar;

                        mContext.Huella.Add(huella);
                        mContext.SaveChanges();
                        //crear registro para sincronizar y la accion
                        CrearPorSincronizarYAccion(huella);
                    } else {
                        var bdHuella = mContext.Huella.FirstOrDefault(p => (p.GuidEmpleado == pocoHuella.GuidEmpleado.ToString()) && (p.GuidHuella == pocoHuella.GuidHuella.ToString()));

                        bdHuella.GuidHuella = huella.GuidHuella;
                        bdHuella.GuidEmpleado = huella.GuidEmpleado;
                        bdHuella.Tipo = huella.Tipo;
                        bdHuella.Data = huella.Data;
                        bdHuella.Sincronizado = (int)TipoSincronizacion.Modificar;

                        mContext.SaveChanges();
                        //crear registro para sincronizar y la accion
                        CrearPorSincronizarYAccion(bdHuella);
                    }
                } else if (pocoHuella.EstadoObjeto == EstadoObjeto.Eliminar){
                    //Eliminar Huella
                    var bdHuella = mContext.Huella.FirstOrDefault(p => (p.GuidEmpleado == pocoHuella.GuidEmpleado.ToString()) && (p.GuidHuella == pocoHuella.GuidHuella.ToString()));

                    bdHuella.Sincronizado = (int)TipoSincronizacion.Eliminar;

                    mContext.Huella.Remove(bdHuella);
                    mContext.SaveChanges();
                    //crear registro para sincronizar y la accion
                    CrearPorSincronizarYAccion(bdHuella);
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                throw eX;
            }
        }
        /// <summary>
        /// Adiciona un Contrato chequeando si existe o no en la BD si existe lo actualiza
        /// </summary>
        /// <param name="pocoContrato">POCOContrato pocoContrato</param>
        /// <returns>Contrato</returns>
        public void SQLiteModificarContrato(POCOContrato pocoContrato) {
            try {
                var contrato = TransformacionDatos.DePOCOContratoAContrato(pocoContrato);

                if (pocoContrato.EstadoObjeto == EstadoObjeto.Almacenar) {
                    //Almacenar Contrato
                    if (mContext.Contrato.FirstOrDefault(p => (p.GuidEmpleado == pocoContrato.GuidEmpleado.ToString()) && (p.CodigoContrato == pocoContrato.CodigoContrato)) == null) {

                        contrato.GuidContrato = Guid.NewGuid().ToString();
                        contrato.Sincronizado = (int)TipoSincronizacion.Insertar;

                        mContext.Contrato.Add(contrato);
                        mContext.SaveChanges();
                        //crear registro para sincronizar y la accion
                        CrearPorSincronizarYAccion(contrato);
                    } else {
                        var bdContrato = mContext.Contrato.FirstOrDefault(p => (p.GuidEmpleado == pocoContrato.GuidEmpleado.ToString()) && (p.CodigoContrato == pocoContrato.CodigoContrato));

                        bdContrato.GuidContrato = contrato.GuidContrato;
                        bdContrato.GuidCargo = contrato.GuidCargo;
                        bdContrato.GuidCuenta = contrato.GuidCuenta;
                        bdContrato.GuidEmpleado = contrato.GuidEmpleado;
                        bdContrato.GuidEmpresa = contrato.GuidEmpresa;
                        bdContrato.InicioVigencia = contrato.InicioVigencia;
                        bdContrato.FinVigencia = contrato.FinVigencia;
                        bdContrato.CodigoContrato = contrato.CodigoContrato;
                        bdContrato.ConsideraAsistencia = contrato.ConsideraAsistencia;
                        bdContrato.ConsideraCasino = contrato.ConsideraCasino;
                        bdContrato.Sincronizado = (int)TipoSincronizacion.Modificar;

                        mContext.SaveChanges();
                        //crear registro para sincronizar y la accion
                        CrearPorSincronizarYAccion(bdContrato);
                    }
                } else {
                    //Eliminar Contrato
                    var bdContrato = mContext.Contrato.FirstOrDefault(p => (p.GuidEmpleado == pocoContrato.GuidEmpleado.ToString()) && (p.CodigoContrato == pocoContrato.CodigoContrato));

                    bdContrato.Sincronizado = (int)TipoSincronizacion.Eliminar;

                    mContext.Contrato.Remove(bdContrato);
                    mContext.SaveChanges();
                    //crear registro para sincronizar y la accion
                    CrearPorSincronizarYAccion(bdContrato);
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
            }
        }
        public void CrearPorSincronizarYAccion(object mObjeto) {
            try {
                var registro = CrearRegistroParaSincronizar(mObjeto);
                mContext.PorSincronizar.Add(registro);
                mContext.SaveChanges();
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                throw eX;
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

            POCONotificacion notificacion = new POCONotificacion() {
                IdNotificacion = ahora.ToBinary().ToString(),
                FechaNotificacion = ahora,
                MensajeNotificacion = mensaje,
                Tipo = TipoNotificacion.Cuidado,
                ImagenDeNotificacion = Properties.Resources.warning_32x32
            };

            lNotificaciones.Add(notificacion);
        }

        /// <summary>
        /// Adicionar una notificacion
        /// </summary>
        /// <param name="mensajeAdicional">string mensajeAdicional</param>
        public void AdicionarNotificacion(string mensaje, TipoNotificacion tipo) {
            DateTime ahora = DateTime.Now;

            Image icono = Properties.Resources.info_32x32;

            switch (tipo) {
                case TipoNotificacion.Exito: icono = Properties.Resources.apply_32x32;
                    break;
                case TipoNotificacion.Cuidado: icono = Properties.Resources.warning_32x32;
                    break;
                case TipoNotificacion.Critica: icono = Properties.Resources.error_32x32;
                    break;
                case TipoNotificacion.Informativa: icono = Properties.Resources.info_32x32;
                    break;
            }

            POCONotificacion notificacion = new POCONotificacion() {
                IdNotificacion = ahora.ToBinary().ToString(),
                FechaNotificacion = ahora,
                MensajeNotificacion = mensaje,
                Tipo = tipo,
                ImagenDeNotificacion = icono
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

            POCONotificacion notificacion = new POCONotificacion() {    
                IdNotificacion = ahora.ToBinary().ToString(),
                FechaNotificacion = ahora,
                MensajeNotificacion = mensaje,
                Tipo = TipoNotificacion.Exito,
                ImagenDeNotificacion = Properties.Resources.apply_32x32
            };

            lNotificaciones.Add(notificacion);
        }
        #endregion

        #region Acciones
        /// <summary>
        /// Ejecuta una acción para un solo elemento que se valla a sincronizar
        /// </summary>
        /// <param name="elemento">Elemento a sincronizar</param>
        public string EjecutarAccion(object elemento)
        {
            var TipoOperacion = Convert.ToInt32((elemento as dynamic).Sincronizado);

            string NombreOperacion = string.Empty;

            switch (TipoOperacion)
            {
                case 1 : //Insert
                    NombreOperacion = "Insertar"; 
                    break;
                case 2: //Delete
                    NombreOperacion = "Eliminar";
                    break;
                case 3: //Update
                    NombreOperacion = "Actualizar";
                    break;
                default:
                    break;
            }

            if (string.IsNullOrEmpty(NombreOperacion)) return string.Empty;

            var NombreMetodo = NombreOperacion + elemento.GetType().Name;
            var tipoWS = typeof(EnroladorServicioWebClient);
            var WS = new EnroladorServicioWebClient();
            var metodo = typeof(EnroladorServicioWebClient).GetMethod(NombreMetodo);

            try
            {
                var ElementoPOCO = TransformacionDatos.Convertir(elemento);
                object[] arregloParametros = new object[2] { new Guid(UsuarioAutenticado.GuidUsuario), ElementoPOCO };

                var res = metodo.Invoke(WS, arregloParametros);
                return res == null ? "" : res.ToString();
            }
            catch(Exception Ex)
            {
                throw new Exception("Error sincronizando la acción", Ex);
            }
        }

        /// <summary>
        /// Sincroniza todas las operaciones pendientes
        /// </summary>
        public void EnviarAcciones()
        {
            try
            {
                //Busco la lista de todos los tipos de la aplicacion
                var listaTipos = AppDomain.CurrentDomain.GetAssemblies().ToList().SelectMany(q => q.GetTypes());

                //Busco lo pendiente por sincronizar
                var lSincronizar = mContext.PorSincronizar.ToList();

                foreach (var sincronizar in lSincronizar)
                {
                    //Datos necesarios para sincronizar
                    var llave = sincronizar.IdRegistroASincronizar;
                    var tabla = sincronizar.TablaASincronizar;
                    var TipoModificacion = sincronizar.IdRegistroASincronizar;

                    //Busco el tipo SQLite a sincronizar
                    var tipo = listaTipos.FirstOrDefault(p => p.Name == tabla);

                    //Conformo la consulta para buscarlo en la BD SQLite
                    var sql = string.Format("SELECT * FROM {0} WHERE Id{0}={1}", tabla, sincronizar.IdRegistroASincronizar);

                    //Chequeando la bd que este abierta
                    if (mContext.Database.Connection.State != System.Data.ConnectionState.Open) mContext.Database.Connection.Open();
                    var command = (mContext.Database.Connection as SQLiteConnection).CreateCommand();
                    command.CommandText = sql;

                    //Creo una instancia del registro a sincronizar
                    var instancia = Activator.CreateInstance(tipo);

                    //Ahora busco todas propiedades para moverme entre ellas
                    var propertyInfos = tipo.GetProperties();

                    //Recorro el datareader y vuelco en valor en la instancia
                    using (var rdr = command.ExecuteReader())
                    {
                        rdr.Read();
                        foreach (var prop in propertyInfos)
                        {
                            try
                            {
                                prop.SetValue(instancia, rdr.GetValue(rdr.GetOrdinal(prop.Name)));
                            }
                            catch { }
                        }
                    }

                    //Ejecuto el sincronismo
                    var res = EjecutarAccion(instancia);

                    if (string.IsNullOrEmpty(res))
                    {
                        //Si no hay errores actualizo el cliente
                        var tran = mContext.Database.BeginTransaction();
                        try
                        {
                            mContext.PorSincronizar.Remove(sincronizar);

                            var sql1 = string.Format("UPDATE {0} SET Sincronizado = 0 WHERE Id{0}={1}", tabla, sincronizar.IdRegistroASincronizar);

                            if (mContext.Database.Connection.State != System.Data.ConnectionState.Open) mContext.Database.Connection.Open();
                            var command1 = (mContext.Database.Connection as SQLiteConnection).CreateCommand();
                            command1.CommandText = sql1;
                            command1.ExecuteNonQuery();
                            mContext.SaveChanges();
                            tran.Commit();
                        }
                        catch (Exception Ex)
                        {
                            tran.Rollback();
                            throw new Exception("Actualizando elementos ya sincronizados en el cliente.", Ex);
                        }

                    }
                    else
                    {
                        throw new Exception("Ocurrió un error en la sincronización. " + res);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("Error sincronizando los datos", Ex);
            }
        }

        public PorSincronizar CrearRegistroParaSincronizar(object mObjeto) {
            try {
                string nombreObjeto = mObjeto.GetType().Name;
                string nombrePropiedad = string.Format("Id{0}", nombreObjeto);

                var propiedad = mObjeto.GetType().GetProperties().FirstOrDefault(p => p.Name == nombrePropiedad);
                var valorPropiedad = Convert.ToInt32(propiedad.GetValue(mObjeto));

                var tipoSincronizacion = Convert.ToInt32((mObjeto as dynamic).Sincronizado);

                var nRegistro = new PorSincronizar() {
                    FechaCambio = DateTime.Now.ToString(),
                    TablaASincronizar = nombreObjeto,
                    IdRegistroASincronizar = valorPropiedad.ToString(),
                    TipoModificacion = tipoSincronizacion
                };

                return nRegistro;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
                throw eX;
            }
        }
        #endregion

        #endregion
    }
}