using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using EnroladorAccesoDatos.Ayudantes;
using EnroladorAccesoDatos.SQLite;
using EnroladorStandAloneV2.EnroladorServicioWeb;
using EnroladorAccesoDatos.Dominio;
using EnroladorAccesoDatos;

namespace EnroladorStandAloneV2.Herramientas {
    public static class TransformacionDatos {

        #region Usuario
        /// <summary>
        /// Transformar de un tipo de POCOUsuario a Usuario
        /// </summary>
        /// <param name="pocoUsuario">POCOUsuario pocoUsuario</param>
        /// <returns>Usuario</returns>
        public static Usuario DePOCOUsuarioAUsuario(POCOUsuario pocoUsuario) {
            try {
                if (pocoUsuario == null) return null;

                var usuario = new Usuario() {
                    NombreUsuario = pocoUsuario.NombreUsuario,
                    ClaveUsuario = pocoUsuario.ClaveUsuario,
                    GuidUsuario = pocoUsuario.GuidUsuario.ToString(),
                    SHAClave = pocoUsuario.SHAClave
                };

                return usuario;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorAccesoDatos", MethodBase.GetCurrentMethod().Name);
                throw eX;
            }
        }

        /// <summary>
        /// Transformar de un tipo de Usuario a POCOUsuario
        /// </summary>
        /// <param name="usuario">Usuario usuario</param>
        /// <returns>POCOUsuario</returns>
        public static POCOUsuario DeUsuarioAPOCOUsuario(Usuario usuario) {
            try {
                if (usuario == null) return null;

                var GuidUsuario = Guid.Parse(usuario.GuidUsuario);

                var pocoUsuario = new POCOUsuario() {
                    NombreUsuario = usuario.NombreUsuario,
                    ClaveUsuario = usuario.ClaveUsuario,
                    GuidUsuario = GuidUsuario,
                    SHAClave = usuario.SHAClave
                };

                return pocoUsuario;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorAccesoDatos", MethodBase.GetCurrentMethod().Name);
                throw eX;
            }

        }
        #endregion

        #region Huella
        /// <summary>
        /// Transformar de un tipo de POCOHuella a Huella
        /// </summary>
        /// <param name="pocoHuella">POCOHuella pocoHuella</param>
        /// <returns>Huella</returns>
        public static Huella DePOCOHuellaAHuella(POCOHuella pocoHuella) {
            try {
                if (pocoHuella == null) return null;

                var huella = new Huella() {
                    GuidHuella = pocoHuella.GuidHuella.ToString(),
                    Data = pocoHuella.Data,
                    Tipo = (int)pocoHuella.Tipo,
                    GuidEmpleado = pocoHuella.GuidEmpleado.ToString()
                };

                return huella;

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorAccesoDatos", MethodBase.GetCurrentMethod().Name);
                throw eX;
            }
        }

        /// <summary>
        /// Transformar de un tipo de Huella a POCOHuella
        /// </summary>
        /// <param name="huella">Huella huella</param>
        /// <returns>POCOHuella</returns>
        public static POCOHuella DeHuellaAPOCOHuella(Huella huella) {
            try {
                if (huella == null) return null;

                var GuidHuella = Guid.Parse(huella.GuidHuella);
                var GuidUsuario = Guid.Parse(huella.GuidEmpleado);

                var pocoHuella = new POCOHuella() {
                    GuidEmpleado = GuidUsuario,
                    GuidHuella = GuidHuella,
                    Data = huella.Data,
                    Tipo = (TipoHuella)huella.Tipo
                };

                return pocoHuella;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorAccesoDatos", MethodBase.GetCurrentMethod().Name);
                throw eX;
            }
        }
        #endregion

        #region Cadena
        /// <summary>
        /// Transformar de un tipo de POCOCadena a Cadena
        /// </summary>
        /// <param name="pocoCadena">POCOCadena pocoHuella</param>
        /// <returns>Cadena</returns>
        public static Cadena DePOCOCadenaACadena(POCOCadena pocoCadena) {
            try {
                if (pocoCadena == null) return null;

                var cadena = new Cadena() {
                    GuidCadena = pocoCadena.GuidCadena.ToString(),
                    NombreCadena = pocoCadena.NombreCadena
                };

                return cadena;

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorAccesoDatos", MethodBase.GetCurrentMethod().Name);
                throw eX;
            }
        }

        /// <summary>
        /// Transformar de un tipo de Cadena a POCOCadena
        /// </summary>
        /// <param name="cadena">Cadena cadena</param>
        /// <returns>POCOCadena</returns>
        public static POCOCadena DeCadenaAPOCOCadena(Cadena cadena) {
            try {
                if (cadena == null) return null;

                var GuidCadena = Guid.Parse(cadena.GuidCadena);

                var pocoCadena = new POCOCadena() {
                    GuidCadena = GuidCadena,
                    NombreCadena = cadena.NombreCadena
                };

                return pocoCadena;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorAccesoDatos", MethodBase.GetCurrentMethod().Name);
                throw eX;
            }
        }
        #endregion

        #region Instalacion
        /// <summary>
        /// Transformar de un tipo de POCOInstalacion a Instalacion
        /// </summary>
        /// <param name="pocoInstalacion">POCOInstalacion pocoHuella</param>
        /// <returns>Instalacion</returns>
        public static Instalacion DePOCOInstalacionAInstalacion(POCOInstalacion pocoInstalacion) {
            try {
                if (pocoInstalacion == null) return null;

                var instalacion = new Instalacion() {
                    GuidInstalacion = pocoInstalacion.GuidInstalacion.ToString(),
                    GuidCadena = pocoInstalacion.GuidCadena.ToString(),
                    NombreInstalacion = pocoInstalacion.NombreInstalacion
                };

                return instalacion;

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorAccesoDatos", MethodBase.GetCurrentMethod().Name);
                throw eX;
            }
        }

        /// <summary>
        /// Transformar de un tipo de Instalacion a POCOInstalacion
        /// </summary>
        /// <param name="instalacion">Instalacion instalacion</param>
        /// <returns>POCOInstalacion</returns>
        public static POCOInstalacion DeInstalacionAPOCOInstalacion(Instalacion instalacion) {
            try {
                if (instalacion == null) return null;

                var GuidInstalacion = Guid.Parse(instalacion.GuidInstalacion);
                var GuidCadena = Guid.Parse(instalacion.GuidCadena);

                var pocoInstalacion = new POCOInstalacion() {
                    GuidInstalacion = GuidInstalacion,
                    GuidCadena = GuidCadena,
                    NombreInstalacion = instalacion.NombreInstalacion
                };

                return pocoInstalacion;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorAccesoDatos", MethodBase.GetCurrentMethod().Name);
                throw eX;
            }
        }
        #endregion

        #region Dispositivo
        /// <summary>
        /// Transformar de un tipo de POCODispositivo a Dispositivo
        /// </summary>
        /// <param name="pocoDispositivo">POCODispositivo pocoHuella</param>
        /// <returns>Dispositivo</returns>
        public static Dispositivo DePOCODispositivoADispositivo(POCODispositivo pocoDispositivo) {
            try {
                if (pocoDispositivo == null) return null;

                var dispositivo = new Dispositivo() {
                    GuidDispositivo = pocoDispositivo.GuidDispositivo.ToString(),
                    GuidInstalacion = pocoDispositivo.GuidInstalacion.ToString(),
                    NombreDispositivo = pocoDispositivo.NombreDispositivo,
                    Host = pocoDispositivo.Host,
                    Puerto = pocoDispositivo.Puerto,
                    Tipo = (int)pocoDispositivo.Tipo
                };

                return dispositivo;

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorAccesoDatos", MethodBase.GetCurrentMethod().Name);
                throw eX;
            }
        }

        /// <summary>
        /// Transformar de un tipo de Dispositivo a POCODispositivo
        /// </summary>
        /// <param name="dispositivo">Dispositivo dispositivo</param>
        /// <returns>POCODispositivo</returns>
        public static POCODispositivo DeDispositivoAPOCODispositivo(Dispositivo dispositivo) {
            try {
                if (dispositivo == null) return null;

                var GuidDispositivo = Guid.Parse(dispositivo.GuidDispositivo);
                var GuidInstalacion = Guid.Parse(dispositivo.GuidInstalacion);

                var pocoDispositivo = new POCODispositivo() {
                    GuidDispositivo = GuidDispositivo,
                    GuidInstalacion = GuidInstalacion,
                    NombreDispositivo = dispositivo.NombreDispositivo,
                    Host = dispositivo.Host,
                    Puerto = int.Parse(dispositivo.Puerto.ToString()),
                    Tipo = (TipoDispositivo)dispositivo.Tipo
                };

                return pocoDispositivo;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorAccesoDatos", MethodBase.GetCurrentMethod().Name);
                throw eX;
            }
        }
        #endregion

        #region Empresa
        /// <summary>
        /// Transformar de un tipo de POCOEmpresa a Empresa
        /// </summary>
        /// <param name="pocoEmpresa">POCOEmpresa pocoHuella</param>
        /// <returns>Empresa</returns>
        public static Empresa DePOCOEmpresaAEmpresa(POCOEmpresa pocoEmpresa) {
            try {
                if (pocoEmpresa == null) return null;

                var empresa = new Empresa() {
                    GuidEmpresa = pocoEmpresa.GuidEmpresa.ToString(),
                    NombreEmpresa = pocoEmpresa.NombreEmpresa
                };

                return empresa;

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorAccesoDatos", MethodBase.GetCurrentMethod().Name);
                throw eX;
            }
        }

        /// <summary>
        /// Transformar de un tipo de Empresa a POCOEmpresa
        /// </summary>
        /// <param name="empresa">Empresa empresa</param>
        /// <returns>POCOEmpresa</returns>
        public static POCOEmpresa DeEmpresaAPOCOEmpresa(Empresa empresa) {
            try {
                if (empresa == null) return null;

                var GuidEmpresa = Guid.Parse(empresa.GuidEmpresa);

                var pocoEmpresa = new POCOEmpresa() {
                    GuidEmpresa = GuidEmpresa,
                    NombreEmpresa = empresa.NombreEmpresa,
                };

                return pocoEmpresa;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorAccesoDatos", MethodBase.GetCurrentMethod().Name);
                throw eX;
            }
        }
        #endregion

        #region Cargo
        /// <summary>
        /// Transformar de un tipo de POCOCargo a Cargo
        /// </summary>
        /// <param name="pocoCargo">POCOCargo pocoHuella</param>
        /// <returns>Cargo</returns>
        public static Cargo DePOCOCargoACargo(POCOCargo pocoCargo) {
            try {
                if (pocoCargo == null) return null;

                var cargo = new Cargo() {
                    GuidCargo = pocoCargo.GuidCargo.ToString(),
                    GuidEmpresa = pocoCargo.GuidEmpresa.ToString(),
                    NombreCargo = pocoCargo.NombreCargo
                };

                return cargo;

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorAccesoDatos", MethodBase.GetCurrentMethod().Name);
                throw eX;
            }
        }

        /// <summary>
        /// Transformar de un tipo de Cargo a POCOCargo
        /// </summary>
        /// <param name="cargo">Cargo cargo</param>
        /// <returns>POCOCargo</returns>
        public static POCOCargo DeCargoAPOCOCargo(Cargo cargo) {
            try {
                if (cargo == null) return null;

                var GuidCargo = Guid.Parse(cargo.GuidCargo);
                var GuidEmpresa = Guid.Parse(cargo.GuidEmpresa);

                var pocoCargo = new POCOCargo() {
                    GuidCargo = GuidCargo,
                    GuidEmpresa = GuidEmpresa,
                    NombreCargo = cargo.NombreCargo,
                };

                return pocoCargo;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorAccesoDatos", MethodBase.GetCurrentMethod().Name);
                throw eX;
            }
        }
        #endregion

        #region Cuenta
        /// <summary>
        /// Transformar de un tipo de POCOCuenta a Cuenta
        /// </summary>
        /// <param name="pocoCuenta">POCOCuenta pocoHuella</param>
        /// <returns>Cuenta</returns>
        public static Cuenta DePOCOCuentaACuenta(POCOCuenta pocoCuenta) {
            try {
                if (pocoCuenta == null) return null;

                var cuenta = new Cuenta() {
                    GuidCuenta = pocoCuenta.GuidCuenta.ToString(),
                    GuidEmpresa = pocoCuenta.GuidEmpresa.ToString(),
                    NombreCuenta = pocoCuenta.NombreCuenta,
                    FechaUltimoCierre = pocoCuenta.FechaUltimoCierre.ToString()
                };

                return cuenta;

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorAccesoDatos", MethodBase.GetCurrentMethod().Name);
                throw eX;
            }
        }

        /// <summary>
        /// Transformar de un tipo de Cuenta a POCOCuenta
        /// </summary>
        /// <param name="cuenta">Cuenta cuenta</param>
        /// <returns>POCOCuenta</returns>
        public static POCOCuenta DeCuentaAPOCOCuenta(Cuenta cuenta) {
            try {
                if (cuenta == null) return null;

                var GuidCuenta = Guid.Parse(cuenta.GuidCuenta);
                var GuidEmpresa = Guid.Parse(cuenta.GuidEmpresa);



                var pocoCuenta = new POCOCuenta() {
                    GuidCuenta = GuidCuenta,
                    GuidEmpresa = GuidEmpresa,
                    NombreCuenta = cuenta.NombreCuenta,
                    FechaUltimoCierre = cuenta.FechaUltimoCierre == String.Empty ? Convert.ToDateTime(null) : DateTime.Parse(cuenta.FechaUltimoCierre)
                };

                return pocoCuenta;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorAccesoDatos", MethodBase.GetCurrentMethod().Name);
                throw eX;
            }
        }
        #endregion

        #region Empleado
        /// <summary>
        /// Transformar de un tipo de POCOEmpleado a Empleado
        /// </summary>
        /// <param name="pocoEmpleado">POCOEmpleado pocoHuella</param>
        /// <returns>Empleado</returns>
        public static Empleado DePOCOEmpleadoAEmpleado(POCOEmpleado pocoEmpleado) {
            try {
                if (pocoEmpleado == null) return null;

                int TieneContraseña = pocoEmpleado.TieneContraseña ? 1 : 0;

                var empleado = new Empleado() {
                    GuidEmpleado = pocoEmpleado.GuidEmpleado.ToString(),
                    RUT = pocoEmpleado.RUT,
                    EnrollId = pocoEmpleado.EnrollId,
                    Correo = pocoEmpleado.Correo,
                    Nombres = pocoEmpleado.Nombres,
                    Apellidos = pocoEmpleado.Apellidos,
                    TieneContrasena = TieneContraseña,
                    Contrasena = pocoEmpleado.Contraseña,
                    NumeroTelefono = pocoEmpleado.NumeroTelefono
                };

                return empleado;

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorAccesoDatos", MethodBase.GetCurrentMethod().Name);
                throw eX;
            }
        }

        /// <summary>
        /// Transformar de un tipo de Empleado a POCOEmpleado
        /// </summary>
        /// <param name="empleado">Empleado empleado</param>
        /// <returns>POCOEmpleado</returns>
        public static POCOEmpleado DeEmpleadoAPOCOEmpleado(Empleado empleado) {
            try {
                if (empleado == null) return null;

                var GuidEmpleado = Guid.Parse(empleado.GuidEmpleado);
                bool TieneConstraseña = empleado.TieneContrasena == 1 ? true : false;

                var pocoEmpleado = new POCOEmpleado() {
                    GuidEmpleado = GuidEmpleado,
                    RUT = empleado.RUT,
                    EnrollId = int.Parse(empleado.EnrollId.ToString()),
                    Correo = empleado.Correo,
                    Nombres = empleado.Nombres,
                    Apellidos = empleado.Apellidos,
                    TieneContraseña = TieneConstraseña,
                    Contraseña = empleado.Contrasena,
                    NumeroTelefono = empleado.NumeroTelefono
                };

                return pocoEmpleado;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorAccesoDatos", MethodBase.GetCurrentMethod().Name);
                throw eX;
            }
        }
        #endregion

        #region EmpleadoDispositivo
        /// <summary>
        /// Transformar de un tipo de POCOEmpleadoDispositivo a EmpleadoDispositivo
        /// </summary>
        /// <param name="pocoEmpleadoDispositivo">POCOEmpleadoDispositivo pocoHuella</param>
        /// <returns>EmpleadoDispositivo</returns>
        public static EmpleadoDispositivo DePOCOEmpleadoDispositivoAEmpleadoDispositivo(POCOEmpleadoDispositivo pocoEmpleadoDispositivo) {
            try {
                if (pocoEmpleadoDispositivo == null) return null;

                var empleadoDispositivo = new EmpleadoDispositivo() {
                    GuidDispositivo = pocoEmpleadoDispositivo.GuidDispositivo.ToString(),
                    GuidEmpleado = pocoEmpleadoDispositivo.GuidEmpleado.ToString()
                };

                return empleadoDispositivo;

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorAccesoDatos", MethodBase.GetCurrentMethod().Name);
                throw eX;
            }
        }

        /// <summary>
        /// Transformar de un tipo de EmpleadoDispositivo a POCOEmpleadoDispositivo
        /// </summary>
        /// <param name="empleadoDispositivo">EmpleadoDispositivo empleadoDispositivo</param>
        /// <returns>POCOEmpleadoDispositivo</returns>
        public static POCOEmpleadoDispositivo DeEmpleadoDispositivoAPOCOEmpleadoDispositivo(EmpleadoDispositivo empleadoDispositivo) {
            try {
                if (empleadoDispositivo == null) return null;

                var GuidDispositivo = Guid.Parse(empleadoDispositivo.GuidDispositivo);
                var GuidEmpleado = Guid.Parse(empleadoDispositivo.GuidEmpleado);

                var pocoEmpleadoDispositivo = new POCOEmpleadoDispositivo() {
                    GuidDispositivo = GuidDispositivo,
                    GuidEmpleado = GuidEmpleado
                };

                return pocoEmpleadoDispositivo;
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorAccesoDatos", MethodBase.GetCurrentMethod().Name);
                throw eX;
            }
        }
        #endregion

        #region Contrato
        /// <summary>
        /// Transformar de un tipo de POCOContrato a Contrato
        /// </summary>
        /// <param name="pocoContrato">POCOContrato pocoHuella</param>
        /// <returns>Contrato</returns>
        public static Contrato DePOCOContratoAContrato(POCOContrato pocoContrato) {
            try {
                if (pocoContrato == null) return null;

                int ConsideraAsistencia = pocoContrato.ConsideraAsistencia ? 1 : 0;
                int ConsideraCasino = pocoContrato.ConsideraCasino ? 1 : 0;

                var contrato = new Contrato() {
                    GuidContrato = pocoContrato.GuidContrato.ToString(),
                    GuidEmpresa = pocoContrato.GuidEmpresa.ToString(),
                    GuidCuenta = pocoContrato.GuidCuenta.ToString(),
                    GuidCargo = pocoContrato.GuidCargo.ToString(),
                    GuidEmpleado = pocoContrato.GuidEmpleado.ToString(),
                    InicioVigencia = pocoContrato.InicioVigencia.ToString(),
                    FinVigencia = pocoContrato.FinVigencia.ToString(),
                    CodigoContrato = pocoContrato.CodigoContrato,
                    ConsideraAsistencia = ConsideraAsistencia,
                    ConsideraCasino = ConsideraCasino
                };

                return contrato;

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorAccesoDatos", MethodBase.GetCurrentMethod().Name);
                throw eX;
            }
        }

        /// <summary>
        /// Transformar de un tipo de Contrato a POCOContrato
        /// </summary>
        /// <param name="contrato">Contrato contrato</param>
        /// <returns>POCOContrato</returns>
        public static POCOContrato DeContratoAPOCOContrato(Contrato contrato) {
            try {
                if (contrato == null) return null;

                var GuidContrato = Guid.Parse(contrato.GuidContrato);
                var GuidEmpresa = Guid.Parse(contrato.GuidEmpresa);
                var GuidCuenta = Guid.Parse(contrato.GuidCuenta);
                var GuidCargo = Guid.Parse(contrato.GuidCargo);
                var GuidEmpleado = Guid.Parse(contrato.GuidEmpleado);

                bool ConsideraAsistencia = contrato.ConsideraAsistencia == 1 ? true : false;
                bool ConsideraCasino = contrato.ConsideraCasino == 1 ? true : false;

                DateTime InicioVigencia = DateTime.Parse(contrato.InicioVigencia);
                DateTime? FinVigencia = contrato.FinVigencia == String.Empty ? Convert.ToDateTime(null) : DateTime.Parse(contrato.FinVigencia);

                EstadoContrato Estado = EstadoContrato.Activo;
                string Descripcion = "Sin descripcion";

                if ((FinVigencia == null) || (FinVigencia == Convert.ToDateTime(null))) {

                    var inicioContrato = ((InicioVigencia == null) || (InicioVigencia == DateTime.MinValue) || (InicioVigencia == new DateTime(1900, 1, 1))) ? "Contrato Permanente" : Convert.ToDateTime(InicioVigencia).Date.ToShortDateString();

                    var finContrato = ((FinVigencia == null) || (FinVigencia == DateTime.MinValue)) ? "Contrato Permanente" : Convert.ToDateTime(FinVigencia).Date.ToShortDateString();

                    var rangoContrato = (inicioContrato == finContrato) ? "Contrato Permanente" :
                                            "(" +
                                                InicioVigencia.Date.ToShortDateString() + "-" +
                                                finContrato +
                                            ")";
                    Descripcion = rangoContrato;
                }else if ((FinVigencia != null) && (Convert.ToDateTime(FinVigencia).Date <= DateTime.Now)) {
                    Estado = EstadoContrato.Vencido;
                }

                var pocoContrato = new POCOContrato() {
                    GuidContrato = GuidContrato,
                    GuidEmpresa = GuidEmpresa,
                    GuidCuenta = GuidCuenta,
                    GuidCargo = GuidCargo,
                    GuidEmpleado = GuidEmpleado,
                    InicioVigencia = InicioVigencia,
                    FinVigencia = FinVigencia,
                    CodigoContrato = contrato.CodigoContrato,
                    ConsideraAsistencia = ConsideraAsistencia,
                    ConsideraCasino = ConsideraCasino,
                    Descripcion = Descripcion,
                    Estado = Estado
                };

                return pocoContrato;

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorAccesoDatos", MethodBase.GetCurrentMethod().Name);
                throw eX;
            }
        }
        #endregion

        #region EmpleadoTurnoServicioCasino
        /// <summary>
        /// Transformar de un tipo de POCOEmpleadoTurnoServicioCasino a EmpleadoTurnoServicioCasino
        /// </summary>
        /// <param name="pocoEmpleadoTurnoServicioCasino">POCOEmpleadoTurnoServicioCasino pocoHuella</param>
        /// <returns>EmpleadoTurnoServicioCasino</returns>
        public static EmpleadoTurnoServicioCasino DePOCOEmpleadoTurnoServicioCasinoAEmpleadoTurnoServicioCasino(POCOEmpleadoTurnoServicioCasino pocoEmpleadoTurnoServicioCasino) {
            try {
                if (pocoEmpleadoTurnoServicioCasino == null) return null;

                var empleadoTurnoServicioCasino = new EmpleadoTurnoServicioCasino() {
                    GuidEmpleado = pocoEmpleadoTurnoServicioCasino.GuidEmpleado.ToString(),
                    GuidTurnoServicio = pocoEmpleadoTurnoServicioCasino.GuidTurnoServicio.ToString()
                };

                return empleadoTurnoServicioCasino;

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorAccesoDatos", MethodBase.GetCurrentMethod().Name);
                throw eX;
            }
        }

        /// <summary>
        /// Transformar de un tipo de EmpleadoTurnoServicioCasino a POCOEmpleadoTurnoServicioCasino
        /// </summary>
        /// <param name="empleadoTurnoServicioCasino">EmpleadoTurnoServicioCasino empleadoTurnoServicioCasino</param>
        /// <returns>POCOEmpleadoTurnoServicioCasino</returns>
        public static POCOEmpleadoTurnoServicioCasino DeEmpleadoTurnoServicioCasinoAPOCOEmpleadoTurnoServicioCasino(EmpleadoTurnoServicioCasino empleadoTurnoServicioCasino) {
            try {
                if (empleadoTurnoServicioCasino == null) return null;

                var GuidEmpleado = Guid.Parse(empleadoTurnoServicioCasino.GuidEmpleado);
                var GuidTurnoServicio = Guid.Parse(empleadoTurnoServicioCasino.GuidTurnoServicio);

                var pocoEmpleadoTurnoServicioCasino = new POCOEmpleadoTurnoServicioCasino() {
                    GuidEmpleado = GuidEmpleado,
                    GuidTurnoServicio = GuidTurnoServicio
                };

                return pocoEmpleadoTurnoServicioCasino;

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorAccesoDatos", MethodBase.GetCurrentMethod().Name);
                throw eX;
            }
        }
        #endregion

        #region ServicioCasino
        /// <summary>
        /// Transformar de un tipo de POCOServicioCasino a ServicioCasino
        /// </summary>
        /// <param name="pocoServicioCasino">POCOServicioCasino pocoHuella</param>
        /// <returns>ServicioCasino</returns>
        public static ServicioCasino DePOCOServicioCasinoAServicioCasino(POCOServicioCasino pocoServicioCasino) {
            try {
                if (pocoServicioCasino == null) return null;

                int Vigente = pocoServicioCasino.Vigente ? 1 : 0;

                var servicioCasino = new ServicioCasino() {
                    GuidServicioCasino = pocoServicioCasino.GuidServicioCasino.ToString(),
                    GuidCasino = pocoServicioCasino.GuidCasino.ToString(),
                    NombreServicioCasino = pocoServicioCasino.NombreServicioCasino,
                    Vigente = Vigente
                };

                return servicioCasino;

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorAccesoDatos", MethodBase.GetCurrentMethod().Name);
                throw eX;
            }
        }

        /// <summary>
        /// Transformar de un tipo de ServicioCasino a POCOServicioCasino
        /// </summary>
        /// <param name="servicioCasino">ServicioCasino servicioCasino</param>
        /// <returns>POCOServicioCasino</returns>
        public static POCOServicioCasino DeServicioCasinoAPOCOServicioCasino(ServicioCasino servicioCasino) {
            try {
                if (servicioCasino == null) return null;

                var GuidServicioCasino = Guid.Parse(servicioCasino.GuidServicioCasino);
                var GuidCasino = Guid.Parse(servicioCasino.GuidCasino);

                bool Vigente = servicioCasino.Vigente == 1 ? true : false;

                var pocoServicioCasino = new POCOServicioCasino() {
                    GuidServicioCasino = GuidServicioCasino,
                    GuidCasino = GuidCasino,
                    NombreServicioCasino = servicioCasino.NombreServicioCasino,
                    Vigente = Vigente
                };

                return pocoServicioCasino;

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorAccesoDatos", MethodBase.GetCurrentMethod().Name);
                throw eX;
            }
        }
        #endregion

        #region TurnoServicio
        /// <summary>
        /// Transformar de un tipo de POCOTurnoServicio a TurnoServicio
        /// </summary>
        /// <param name="pocoTurnoServicio">POCOTurnoServicio pocoHuella</param>
        /// <returns>TurnoServicio</returns>
        public static TurnoServicio DePOCOTurnoServicioATurnoServicio(POCOTurnoServicio pocoTurnoServicio) {
            try {
                if (pocoTurnoServicio == null) return null;

                int Vigente = pocoTurnoServicio.Vigente ? 1 : 0;

                var turnoServicio = new TurnoServicio() {
                    GuidTurnoServicio = pocoTurnoServicio.GuidTurnoServicio.ToString(),
                    GuidServicio = pocoTurnoServicio.GuidServicio.ToString(),
                    NombreTurnoServicio = pocoTurnoServicio.NombreTurnoServicio,
                    Vigente = Vigente,
                    HoraInicio = pocoTurnoServicio.HoraInicio.ToString(),
                    HoraFin = pocoTurnoServicio.HoraFin.ToString()
                };

                return turnoServicio;

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorAccesoDatos", MethodBase.GetCurrentMethod().Name);
                throw eX;
            }
        }

        /// <summary>
        /// Transformar de un tipo de TurnoServicio a POCOTurnoServicio
        /// </summary>
        /// <param name="turnoServicio">TurnoServicio turnoServicio</param>
        /// <returns>POCOTurnoServicio</returns>
        public static POCOTurnoServicio DeTurnoServicioAPOCOTurnoServicio(TurnoServicio turnoServicio) {
            try {
                if (turnoServicio == null) return null;

                var GuidTurnoServicio = Guid.Parse(turnoServicio.GuidTurnoServicio);
                var GuidServicio = Guid.Parse(turnoServicio.GuidServicio);

                bool Vigente = turnoServicio.Vigente == 1 ? true : false;

                TimeSpan HoraInicio = TimeSpan.Parse(turnoServicio.HoraInicio);
                TimeSpan HoraFin = TimeSpan.Parse(turnoServicio.HoraFin);

                var pocoTurnoServicio = new POCOTurnoServicio() {
                    GuidTurnoServicio = GuidTurnoServicio,
                    GuidServicio = GuidServicio,
                    NombreTurnoServicio = turnoServicio.NombreTurnoServicio,
                    Vigente = Vigente,
                    HoraInicio = HoraInicio,
                    HoraFin = HoraFin
                };

                return pocoTurnoServicio;

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorAccesoDatos", MethodBase.GetCurrentMethod().Name);
                throw eX;
            }
        }
        #endregion
    }
}
