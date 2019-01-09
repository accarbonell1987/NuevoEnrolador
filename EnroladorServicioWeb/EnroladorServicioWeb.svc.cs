using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using EnroladorAccesoDatos.Ayudantes;
using EnroladorAccesoDatos.Dominio;
using EnroladorAccesoDatos;

namespace EnroladorServicioWeb {
    public class EnroladorServicioWeb : IEnroladorServicioWeb {
        #region Propiedades
        private string ConnectionString() => Properties.Settings.Default.ConnectionString;
        #endregion

        #region Sistema
        /// <summary>
        /// Identificar Hardware Online
        /// </summary>
        /// <param name="HardwareId">Guid HardwareId</param>
        /// <returns>bool</returns>
        public bool IdentificarHardware(Guid HardwareId) {
            try {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                using (SqlCommand comm = new SqlCommand("ESA_Identificar", conn) { CommandType = CommandType.StoredProcedure }) {
                    conn.Open();

                    comm.Parameters.Add("@HWID", SqlDbType.UniqueIdentifier).Value = HardwareId;
                    SqlParameter outParam = new SqlParameter("@Identificado", SqlDbType.Bit) {
                        Direction = ParameterDirection.Output
                    };
                    comm.Parameters.Add(outParam);

                    comm.ExecuteNonQuery();

                    if (!(outParam.Value is DBNull)) {
#if DEBUG
                        return true;
#else
                        return (bool)outParam.Value;
#endif
                    } else {
                        return false;
                    }
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "WebServices", MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }
        #endregion

        #region Autenticacion
        /// <summary>
        /// Autenticar usuario Online
        /// </summary>
        /// <param name="nombreUsuario">string nombreUsuario</param>
        /// <param name="claveUsuario">string claveUsuario</param>
        /// <returns>UsuarioSerializable</returns>
        public POCOUsuario AutenticacionUsuario(string nombreUsuario, string claveUsuario) {

            POCOUsuario mUsuario = new POCOUsuario();

            try {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                using (SqlCommand comm = new SqlCommand("ESA_Login", conn) { CommandType = CommandType.StoredProcedure }) {
                    conn.Open();

                    comm.Parameters.Add("@User", SqlDbType.NVarChar).Value = nombreUsuario;
                    comm.Parameters.Add("@Pass", SqlDbType.NVarChar).Value = claveUsuario;

                    SqlParameter GuidUsuario = new SqlParameter("@OidUsuario", SqlDbType.UniqueIdentifier) {
                        Direction = ParameterDirection.Output
                    };
                    comm.Parameters.Add(GuidUsuario);

                    SqlParameter SHAClave = new SqlParameter("@SHAPassword", SqlDbType.NVarChar, -1) {
                        //SqlParameter outParam2 = new SqlParameter("@Pass", SqlDbType.VarBinary, -1);
                        Direction = ParameterDirection.Output
                    };
                    comm.Parameters.Add(SHAClave);

                    comm.ExecuteNonQuery();

                    if (!(GuidUsuario.Value is DBNull) && !(SHAClave.Value is DBNull)) {

                        mUsuario = new POCOUsuario() {
                            GuidUsuario = (Guid)GuidUsuario.Value,
                            NombreUsuario = nombreUsuario,
                            ClaveUsuario = claveUsuario,
                            SHAClave = (string)SHAClave.Value
                        };
                        return mUsuario;

                    } else {
                        return null;
                    }
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "WebServices", MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }

        /// <summary>
        /// Revalidar el usuario
        /// </summary>
        /// <param name="Usuario">POCOUsuario Usuario</param>
        /// <returns>POCOUsuario</returns>
        public POCOUsuario Revalidar(POCOUsuario Usuario) {

            POCOUsuario mUsuario = new POCOUsuario();

            try {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                using (SqlCommand comm = new SqlCommand("ESA_Revalidar", conn) { CommandType = CommandType.StoredProcedure }) {
                    conn.Open();

                    comm.Parameters.Add("@LoggedUserOid", SqlDbType.UniqueIdentifier).Value = Usuario.GuidUsuario;

                    SqlParameter usuario = new SqlParameter("@User", SqlDbType.NVarChar, -1) {
                        Direction = ParameterDirection.Output
                    };
                    comm.Parameters.Add(usuario);

                    SqlParameter SHAClave = new SqlParameter("@Pass", SqlDbType.NVarChar, -1) {
                        Direction = ParameterDirection.Output
                    };
                    comm.Parameters.Add(SHAClave);

                    comm.ExecuteNonQuery();

                    if (!(usuario.Value is DBNull) && !(SHAClave.Value is DBNull)) {

                        mUsuario = new POCOUsuario() {
                            GuidUsuario = Usuario.GuidUsuario,
                            NombreUsuario = (string)usuario.Value,
                            ClaveUsuario = Usuario.ClaveUsuario,
                            SHAClave = (string)SHAClave.Value
                        };
                        return mUsuario;

                    } else {
                        return null;
                    }
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "WebServices", MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }
        #endregion

        #region Lectura y Obtencion de Datos
        /// <summary>
        /// Devuelve listado de HuellaUsuarioSerializable segun el GuidUsuario
        /// </summary>
        /// <param name="GuidUsuario">Guid GuidUsuario</param>
        /// <returns>IList<HuellaUsuarioSerializable></returns>
        public IList<POCOHuella> LeeHuellasDelUsuario(Guid GuidUsuario) {

            List<POCOHuella> lHuellas = new List<POCOHuella>();

            try {
                string sql = string.Format("SELECT Oid, TipoHuella, Data FROM ESA_Huella_Usuario WHERE Usuario = '{0}'", GuidUsuario);

                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                using (SqlCommand comm = new SqlCommand(sql, conn)) {
                    SqlDataReader reader = null;
                    try {
                        conn.Open();
                        reader = comm.ExecuteReader();

                        while (reader.Read()) {
                            lHuellas.Add(new POCOHuella() {
                                GuidHuella = reader.GetFieldValue<Guid>(0),
                                Tipo = (TipoHuella)reader.GetFieldValue<int>(1),
                                Data = reader.GetFieldValue<string>(2),
                                GuidEmpleado = GuidUsuario
                            });
                        }

                        return lHuellas;
                    } finally {
                        reader.Close();
                    }
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "WebServices", MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }

        /// <summary>
        /// Devuelve listado de POCOCadena segun el GuidUsuario
        /// </summary>
        /// <param name="GuidUsuario">Guid GuidUsuario</param>
        /// <returns>IList<POCOCadena></returns>
        public IList<POCOCadena> LeeCadenasDelUsuario(Guid GuidUsuario) {
            string sql = string.Format("SELECT Oid, Nombre FROM ESA_Cadena WHERE Usuario = '{0}'", GuidUsuario);

            List<POCOCadena> lCadenas = new List<POCOCadena>();

            try {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                using (SqlCommand comm = new SqlCommand("", conn)) {
                    conn.Open();
                    comm.CommandText = sql;
                    SqlDataReader reader = null;

                    try {
                        reader = comm.ExecuteReader();

                        while (reader.Read()) {
                            Guid Oid = reader.GetFieldValue<Guid>(0);
                            string Nombre = reader.GetFieldValue<string>(1);

                            lCadenas.Add(new POCOCadena() {
                                GuidCadena = Oid,
                                NombreCadena = Nombre
                            });
                        }
                    } finally {
                        reader.Close();
                    }

                    return lCadenas;
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "WebServices", MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }

        /// <summary>
        /// Devuelve listado de POCOCargo segun el GuidUsuario
        /// </summary>
        /// <param name="GuidUsuario">Guid GuidUsuario</param>
        /// <returns>IList<POCOCargo></returns>
        public IList<POCOCargo> LeeCargosDelUsuario(Guid GuidUsuario) {
            string sql = string.Format("SELECT Oid, Nombre, Empresa FROM ESA_Cargo WHERE Usuario = '{0}'", GuidUsuario);

            List<POCOCargo> lCargos = new List<POCOCargo>();

            try {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                using (SqlCommand comm = new SqlCommand("", conn)) {
                    conn.Open();

                    comm.CommandText = sql;
                    SqlDataReader reader = null;
                    try {
                        reader = comm.ExecuteReader();

                        while (reader.Read()) {
                            Guid Oid = reader.GetFieldValue<Guid>(0);
                            string Nombre = reader.GetFieldValue<string>(1);
                            Guid Empresa = reader.GetFieldValue<Guid>(2);
                            lCargos.Add(new POCOCargo() {
                                GuidCargo = Oid,
                                NombreCargo = Nombre,
                                GuidEmpresa = Empresa
                            });
                        }
                    } finally {
                        reader.Close();
                    }

                    return lCargos;
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "WebServices", MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }

        /// <summary>
        /// Devuelve listado de POCOContrato segun el GuidUsuario
        /// </summary>
        /// <param name="GuidUsuario">Guid GuidUsuario</param>
        /// <returns>IList<POCOContrato></returns>
        public IList<POCOContrato> LeeContratosDelUsuario(Guid GuidUsuario) {

            string sql = string.Format("SELECT Oid, Empresa, Cuenta, Cargo, InicioVigencia, FinVigencia, Empleado, Codigo, ConsideraAsistencia, ConsideraCasino FROM ESA_Contrato " +
                "WHERE /*(GETDATE() BETWEEN ISNULL(InicioVigencia, GETDATE()) AND ISNULL(FinVigencia, GETDATE())) AND */ Usuario = '{0}'", GuidUsuario);

            List<POCOContrato> lContratos = new List<POCOContrato>();

            try {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                using (SqlCommand comm = new SqlCommand("", conn)) {
                    conn.Open();

                    comm.CommandText = sql;
                    SqlDataReader reader = null;
                    try {
                        reader = comm.ExecuteReader();
                        
                        int i = 0;
                        while (reader.Read()) {
                            try {
                                Guid Oid = reader.GetFieldValue<Guid>(0);
                                Guid Empresa = reader.GetFieldValue<Guid>(1);
                                Guid Cuenta = reader.GetFieldValue<Guid>(2);
                                Guid Cargo = reader.GetFieldValue<Guid>(3);
                                DateTime InicioVigencia = reader.IsDBNull(4) ? DateTime.Today : reader.GetFieldValue<DateTime>(4);
                                DateTime? FinVigencia = null;
                                if (!reader.IsDBNull(5)) {
                                    FinVigencia = reader.GetFieldValue<DateTime>(5);
                                }
                                Guid Empleado = reader.GetFieldValue<Guid>(6);
                                var CodigoContrato = reader.IsDBNull(7) ? "SIN_CODIGO_CONTRATO" : reader.GetFieldValue<string>(7).ToString();
                                var ConsideraAsistencia = reader.IsDBNull(8) ? false : reader.GetFieldValue<bool>(8);
                                var ConsideraCasino = reader.IsDBNull(9) ? false : reader.GetFieldValue<bool>(9);

                                lContratos.Add(new POCOContrato() {
                                    GuidContrato = Oid,
                                    GuidEmpresa = Empresa,
                                    GuidCargo = Cargo,
                                    GuidCuenta = Cuenta,
                                    GuidEmpleado = Empleado,
                                    InicioVigencia = InicioVigencia,
                                    FinVigencia = FinVigencia,
                                    CodigoContrato = CodigoContrato,
                                    ConsideraAsistencia = ConsideraAsistencia,
                                    ConsideraCasino = ConsideraCasino
                                });

                                i++;
                            } catch (Exception Ex) {
                                throw new Exception("Error cargando contratos", Ex);
                            }
                        }
                    } finally {
                        reader.Close();
                    }

                    return lContratos;
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "WebServices", MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }

        /// <summary>
        /// Devuelve listado de POCOCuenta segun el GuidUsuario
        /// </summary>
        /// <param name="GuidUsuario">Guid GuidUsuario</param>
        /// <returns>IList<POCOCuenta></returns>
        public IList<POCOCuenta> LeeCuentasDelUsuario(Guid GuidUsuario) {
            string sql = string.Format(@"SELECT EC.Oid, EC.Nombre, EC.Empresa, C.FechaUltimoCierre FROM ESA_Cuenta EC
                                                INNER JOIN Cuenta C ON C.Oid = EC.Oid WHERE EC.Usuario = '{0}'", GuidUsuario);

            List<POCOCuenta> lCuentas = new List<POCOCuenta>();

            try {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                using (SqlCommand comm = new SqlCommand("", conn)) {
                    conn.Open();

                    comm.CommandText = sql;
                    SqlDataReader reader = null;
                    try {
                        reader = comm.ExecuteReader();

                        while (reader.Read()) {
                            Guid Oid = reader.GetFieldValue<Guid>(0);
                            string Nombre = reader.GetFieldValue<string>(1);
                            Guid Empresa = reader.GetFieldValue<Guid>(2);
                            DateTime? FechaUltimoCierre = !(reader[3] is DBNull) ? reader.GetFieldValue<DateTime?>(3) : null;

                            lCuentas.Add(new POCOCuenta() {
                                GuidCuenta = Oid,
                                GuidEmpresa = Empresa,
                                NombreCuenta = Nombre,
                                FechaUltimoCierre = FechaUltimoCierre
                            });
                        }
                    } finally {
                        reader.Close();
                    }

                    return lCuentas ;
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "WebServices", MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }

        /// <summary>
        /// Devuelve listado de POCODispositivo segun el GuidUsuario
        /// </summary>
        /// <param name="GuidUsuario">Guid GuidUsuario</param>
        /// <returns>IList<POCODispositivo></returns>
        public IList<POCODispositivo> LeeDispositivoDelUsuario(Guid GuidUsuario) {
            string sql = string.Format("SELECT Oid, Nombre, Host, Puerto, Instalacion FROM ESA_Dispositivo WHERE Usuario = '{0}'", GuidUsuario);

            List<POCODispositivo> lDispositivo = new List<POCODispositivo>();

            try {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                using (SqlCommand comm = new SqlCommand("", conn)) {
                    conn.Open();

                    comm.CommandText = sql;
                    SqlDataReader reader = null;
                    try {
                        reader = comm.ExecuteReader();

                        while (reader.Read()) {
                            Guid Oid = reader.GetFieldValue<Guid>(0);
                            string Nombre = reader.GetFieldValue<string>(1);
                            string Host = reader.IsDBNull(2) ? "" : reader.GetFieldValue<string>(2);
                            int Puerto = reader.IsDBNull(3) ? 0 : reader.GetFieldValue<int>(3);
                            Guid Instalacion = reader.GetFieldValue<Guid>(4);

                            lDispositivo.Add(new POCODispositivo() {
                                GuidDispositivo = Oid,
                                GuidInstalacion = Instalacion,
                                NombreDispositivo = Nombre,
                                Host = Host,
                                Puerto = Puerto
                            });
                    
                        }
                    } finally {
                        reader.Close();
                    }

                    return lDispositivo;
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "WebServices", MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }

        /// <summary>
        /// Devuelve listado de POCOEmpleadoDispositivo
        /// </summary>
        /// <returns>IList<POCOEmpleadoDispositivo></returns>
        public IList<POCOEmpleadoDispositivo> LeeEmpleadosDispositivos() {
            string sql = "SELECT Dispositivo,Empleado FROM ESA_PuedeMarcarEn";
            List<POCOEmpleadoDispositivo> lEmpleadoDispositivo = new List<POCOEmpleadoDispositivo>();

            try {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                using (SqlCommand comm = new SqlCommand("", conn)) {
                    conn.Open();

                    comm.CommandText = sql;
                    SqlDataReader reader = null;
                    try {
                        reader = comm.ExecuteReader();

                        while (reader.Read()) {
                            Guid Dispositivo = reader.GetFieldValue<Guid>(0);
                            Guid Empleado = reader.GetFieldValue<Guid>(1);

                            lEmpleadoDispositivo.Add(new POCOEmpleadoDispositivo() {
                                GuidDispositivo = Dispositivo,
                                GuidEmpleado = Empleado
                            });
                        }
                    } finally {
                        reader.Close();
                    }

                    return lEmpleadoDispositivo;
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "WebServices", MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }

        /// <summary>
        /// Devuelve listado de POCOEmpresa segun el GuidUsuario
        /// </summary>
        /// <param name="GuidUsuario">Guid GuidUsuario</param>
        /// <returns>IList<POCOEmpresa></returns>
        public IList<POCOEmpresa> LeeEmpresaDelUsuario(Guid GuidUsuario) {
            string sql = string.Format("SELECT Oid, Name FROM ESA_Empresa WHERE Usuario = '{0}'", GuidUsuario);

            List<POCOEmpresa> lEmpresa = new List<POCOEmpresa>();

            try {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                using (SqlCommand comm = new SqlCommand("", conn)) {
                    conn.Open();

                    comm.CommandText = sql;
                    SqlDataReader reader = null;
                    try {
                        reader = comm.ExecuteReader();

                        while (reader.Read()) {
                            Guid Oid = reader.GetFieldValue<Guid>(0);
                            string Nombre = reader.GetFieldValue<string>(1);

                            lEmpresa.Add(new POCOEmpresa() {
                                GuidEmpresa = Oid,
                                NombreEmpresa = Nombre
                            });
                        }
                    } finally {
                        reader.Close();
                    }

                    return lEmpresa;
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "WebServices", MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }

        /// <summary>
        /// Devuelve listado de POCOHuella
        /// </summary>
        /// <returns>IList<POCOHuella></returns>
        public IList<POCOHuella> LeeTodasHuellas() {
            string sql = "SELECT Oid, TipoHuella, Empleado FROM ESA_Huella";

            List<POCOHuella> lHuellas = new List<POCOHuella>();

            try {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                using (SqlCommand comm = new SqlCommand("", conn)) {
                    conn.Open();

                    comm.CommandText = sql;
                    SqlDataReader reader = null;
                    try {
                        reader = comm.ExecuteReader();

                        while (reader.Read()) {
                            Guid Oid = reader.GetFieldValue<Guid>(0);
                            int TipoHuella = reader.GetFieldValue<int>(1);
                            Guid Empleado = reader.GetFieldValue<Guid>(2);

                            lHuellas.Add(new POCOHuella() {
                                GuidHuella = Oid,
                                Tipo = (TipoHuella)TipoHuella,
                                GuidEmpleado = Empleado
                            });
                        }
                    } finally {
                        reader.Close();
                    }
                    
                    return lHuellas;
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "WebServices", MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }

        /// <summary>
        /// Devuelve listado de POCOInstalacion segun el GuidUsuario
        /// </summary>
        /// <param name="GuidUsuario">Guid GuidUsuario</param>
        /// <returns>IList<POCOInstalacion></returns>
        public IList<POCOInstalacion> LeeInstalacionesDelUsuario(Guid GuidUsuario) {
            string sql = string.Format("SELECT Oid, Nombre, Cadena FROM ESA_Instalacion WHERE Usuario = '{0}'", GuidUsuario);

            List<POCOInstalacion> lInstalacion = new List<POCOInstalacion>();

            try {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                using (SqlCommand comm = new SqlCommand("", conn)) {
                    conn.Open();
                    
                    comm.CommandText = sql;
                    SqlDataReader reader = null;
                    try {
                        reader = comm.ExecuteReader();

                        while (reader.Read()) {
                            Guid Oid = reader.GetFieldValue<Guid>(0);
                            string Nombre = reader.GetFieldValue<string>(1);
                            Guid Cadena = reader.GetFieldValue<Guid>(2);

                            lInstalacion.Add(new POCOInstalacion() {
                                GuidInstalacion = Oid,
                                GuidCadena = Cadena,
                                NombreInstalacion = Nombre
                            });
                        }
                    } finally {
                        reader.Close();
                    }

                    return lInstalacion;
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "WebServices", MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }

        /// <summary>
        /// Devuelve listado de POCOEmpleado
        /// </summary>
        /// <returns>IList<POCOEmpleado></returns>
        public IList<POCOEmpleado> LeeTodosEmpleados() {
            //ACCM
            string sql = string.Format(@"SELECT TOP 5000 E.Oid, E.EnrollID, E.RUT, P.FirstName, P.LastName, P.Email, PN.Number, E.Contraseña 
            FROM ESA_Empleado E
            INNER JOIN Person P ON E.Oid = P.Oid
			INNER JOIN Party PT ON PT.Oid = P.Oid
            LEFT OUTER JOIN PhoneNumber PN ON PN.Party = PT.Oid");

            List<POCOEmpleado> lEmpleados = new List<POCOEmpleado>();

            try {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                using (SqlCommand comm = new SqlCommand(sql, conn)) {
                    conn.Open();
                    SqlDataReader reader = null;
                    try {
                        reader = comm.ExecuteReader();

                        while (reader.Read()) {
                            var pEmpleado = new POCOEmpleado() {
                                GuidEmpleado = reader.GetFieldValue<Guid>(0),
                                EnrollId = reader.GetFieldValue<int>(1),
                                RUT = reader.GetFieldValue<string>(2),
                                Nombres = reader.GetFieldValue<string>(3),
                                Apellidos = reader.GetFieldValue<string>(4),
                                Correo = reader.IsDBNull(5) ? "" : reader.GetFieldValue<string>(5),
                                NumeroTelefono = reader.IsDBNull(6) ? "" : reader.GetFieldValue<string>(6),
                                TieneContraseña = reader.GetFieldValue<int>(7) == 0 ? false : true
                            };

                            lEmpleados.Add(pEmpleado);
                        }
                    } finally {
                        reader.Close();
                    }
                    return lEmpleados;
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "WebServices", MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }

        /// <summary>
        /// Devuelve listado de POCOServicioCasino segun el GuidUsuario
        /// </summary>
        /// <param name="GuidUsuario">Guid GuidUsuario</param>
        /// <returns>IList<POCOServicioCasino></returns>
        public IList<POCOServicioCasino> LeeServicioCasinoDelUsuario(Guid GuidUsuario) {
            string sql = string.Format(@"SELECT SC.Oid, SC.Casino, SC.Nombre, SC.Vigente FROM ServicioCasino SC
									INNER JOIN ESA_Instalacion EI ON SC.Casino = EI.Oid
									WHERE EI.Usuario = '{0}'", GuidUsuario);

            List<POCOServicioCasino> lServicioCasino = new List<POCOServicioCasino>();

            try {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                using (SqlCommand comm = new SqlCommand(sql, conn)) {
                    conn.Open();
                    SqlDataReader reader = null;
                    try {
                        reader = comm.ExecuteReader();

                        while (reader.Read()) {
                            var servicioCasino = new POCOServicioCasino() {
                                GuidServicioCasino = reader.GetFieldValue<Guid>(0),
                                GuidCasino = reader.GetFieldValue<Guid>(1),
                                NombreServicioCasino = reader.GetFieldValue<string>(2),
                                Vigente = reader.GetFieldValue<Boolean>(3)
                            };

                            lServicioCasino.Add(servicioCasino);
                        }
                    } finally {
                        reader.Close();
                    }
                    return lServicioCasino;
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "WebServices", MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }

        /// <summary>
        /// Devuelve listado de POCOTurnoServicio segun el GuidUsuario
        /// </summary>
        /// <param name="GuidUsuario">Guid GuidUsuario</param>
        /// <returns>IList<POCOTurnoServicio></returns>
        public IList<POCOTurnoServicio> LeeTurnoServicioDelUsuario(Guid GuidUsuario) {
            string sql = string.Format(@"SELECT TS.Oid
                                                  ,TS.Servicio
                                                  ,TS.Nombre
                                                  ,TS.Vigente
                                                  ,TS.HoraInicio
                                                  ,TS.HoraFin
                                            FROM TurnoServicio TS INNER JOIN ServicioCasino SC ON TS.Servicio = SC.Oid
                                                                    INNER JOIN ESA_Instalacion EI ON SC.Casino = EI.Oid
                                            WHERE EI.Usuario = '{0}'", GuidUsuario);

            List<POCOTurnoServicio> lTurnoServicio = new List<POCOTurnoServicio>();

            try {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                using (SqlCommand comm = new SqlCommand(sql, conn)) {
                    conn.Open();
                    SqlDataReader reader = null;
                    try {
                        reader = comm.ExecuteReader();

                        while (reader.Read()) {
                            try {
                                var turnoServicio = new POCOTurnoServicio() {
                                    GuidTurnoServicio = reader.GetFieldValue<Guid>(0),
                                    GuidServicio = reader.GetFieldValue<Guid>(1),
                                    NombreTurnoServicio = reader.GetFieldValue<string>(2),
                                    Vigente = reader.GetFieldValue<Boolean>(3),
                                    HoraInicio = reader.GetFieldValue<TimeSpan>(4),
                                    HoraFin = reader.GetFieldValue<TimeSpan>(5)
                                };

                                lTurnoServicio.Add(turnoServicio);

                            } catch (Exception eX) {
                                AyudanteLogs.Log(eX, "WebServices", MethodBase.GetCurrentMethod().Name);
                            }
                        }
                    } finally {
                        reader.Close();
                    }

                    return lTurnoServicio;
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "WebServices", MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }

        /// <summary>
        /// Devuelve listado de POCOEmpleadoTurnoServicioCasino segun el GuidUsuario
        /// </summary>
        /// <param name="GuidUsuario">Guid GuidUsuario</param>
        /// <returns>IList<POCOEmpleadoTurnoServicioCasino></returns>
        public IList<POCOEmpleadoTurnoServicioCasino> LeeEmpleadoTurnoServicioCasino(Guid GuidUsuario) {
            string sql = string.Format(@"SELECT ETSC.Empleado, ETSC.TurnoServicio 
                                            FROM EmpleadoTurnoServicioCasino ETSC 
                                            INNER JOIN TurnoServicio TS ON ETSC.TurnoServicio = TS.Oid INNER JOIN ServicioCasino SC ON TS.Servicio = SC.Oid
                                            INNER JOIN ESA_Instalacion EI ON SC.Casino = EI.Oid 
                                            WHERE EI.Usuario = '{0}'", GuidUsuario);

            List<POCOEmpleadoTurnoServicioCasino> lEmpleadoTurnoServicioCasino = new List<POCOEmpleadoTurnoServicioCasino>();

            try {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                using (SqlCommand comm = new SqlCommand(sql, conn)) {
                    conn.Open();
                    SqlDataReader reader = null;
                    try {
                        reader = comm.ExecuteReader();

                        while (reader.Read()) {
                            var turnoServicio = new POCOEmpleadoTurnoServicioCasino() {
                                GuidEmpleado = reader.GetFieldValue<Guid>(0),
                                GuidTurnoServicio = reader.GetFieldValue<Guid>(1)
                            };
                            lEmpleadoTurnoServicioCasino.Add(turnoServicio);
                        }
                    } finally {
                        reader.Close();
                    }
                    return lEmpleadoTurnoServicioCasino;
                }
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "WebServices", MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }
        #endregion

        #region Acciones

        #region Huellas
        /// <summary>
        /// Inserta una huella
        /// </summary>
        /// <param name="responsable">Usuario del sistema</param>
        /// <param name="pocoHuella">Huella</param>
        /// <returns>Mensaje de error o en blanco si no existe</returns>
        public string InsertHuella(Guid responsable, POCOHuella pocoHuella)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                using (SqlCommand comm = new SqlCommand("ESA_Crear_Huella", conn) { CommandType = CommandType.StoredProcedure })
                {
                    conn.Open();

                    comm.Parameters.Add("@LoggedUserOid", SqlDbType.UniqueIdentifier).Value = responsable;
                    comm.Parameters.Add("@Oid", SqlDbType.UniqueIdentifier).Value = pocoHuella.GuidHuella;
                    comm.Parameters.Add("@Empleado", SqlDbType.UniqueIdentifier).Value = pocoHuella.GuidEmpleado;
                    comm.Parameters.Add("@TipoHuella", SqlDbType.Int).Value = pocoHuella.Tipo;
                    comm.Parameters.Add("@Data", SqlDbType.VarChar).Value = pocoHuella.Data;
                    SqlParameter outParam = new SqlParameter("@Error", SqlDbType.NVarChar, -1);
                    outParam.Direction = ParameterDirection.Output;
                    comm.Parameters.Add(outParam);

                    comm.ExecuteNonQuery();

                    if (!(outParam.Value is DBNull) && !string.IsNullOrEmpty((string)outParam.Value))
                    {
                        return (string)outParam.Value;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Actualiza una huella
        /// </summary>
        /// <param name="responsable">Usuario del sistema</param>
        /// <param name="pocoHuella">Huella</param>
        /// <returns>Mensaje de error o en blanco si no existe</returns>
        public string UpdateHuella(Guid responsable, POCOHuella pocoHuella)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                using (SqlCommand comm = new SqlCommand("ESA_Actualizar_Huella", conn) { CommandType = CommandType.StoredProcedure })
                {
                    conn.Open();

                    comm.Parameters.Add("@LoggedUserOid", SqlDbType.UniqueIdentifier).Value = responsable;
                    comm.Parameters.Add("@Oid", SqlDbType.UniqueIdentifier).Value = pocoHuella.GuidHuella;
                    comm.Parameters.Add("@Data", SqlDbType.VarChar).Value = pocoHuella.Data;
                    SqlParameter outParam = new SqlParameter("@Error", SqlDbType.NVarChar, -1);
                    outParam.Direction = ParameterDirection.Output;
                    comm.Parameters.Add(outParam);

                    comm.ExecuteNonQuery();

                    if (!(outParam.Value is DBNull) && !string.IsNullOrEmpty((string)outParam.Value))
                    {
                        return (string)outParam.Value;
                    }
                    return "";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region Empleados
        /// <summary>
        /// Inserta un empleado
        /// </summary>
        /// <param name="responsable">Usuario del sistema</param>
        /// <param name="pocoHuella">Huella</param>
        /// <returns>Mensaje de error o en blanco si no existe</returns>
        public string InsertEmpleado(Guid responsable, POCOEmpleado pocoEmpleado)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                using (SqlCommand comm = new SqlCommand("ESA_Crear_Empleado", conn) { CommandType = CommandType.StoredProcedure })
                {
                    conn.Open();

                    comm.Parameters.Add("@LoggedUserOid", SqlDbType.UniqueIdentifier).Value = responsable;
                    comm.Parameters.Add("@Oid", SqlDbType.UniqueIdentifier).Value = pocoEmpleado.GuidEmpleado;
                    comm.Parameters.Add("@RUT", SqlDbType.VarChar).Value = pocoEmpleado.RUT;
                    comm.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = pocoEmpleado.Nombres;
                    comm.Parameters.Add("@LastName", SqlDbType.VarChar).Value = pocoEmpleado.Apellidos;
                    comm.Parameters.Add("@Correo", SqlDbType.VarChar).Value = pocoEmpleado.Correo;
                    comm.Parameters.Add("@Telefono", SqlDbType.VarChar).Value = pocoEmpleado.NumeroTelefono;
                    comm.Parameters.Add("@EnrollID", SqlDbType.Int).Value = pocoEmpleado.EnrollId;
                    comm.Parameters.Add("@Contraseña", SqlDbType.NVarChar).Value = pocoEmpleado.Contraseña;
                    SqlParameter outParam = new SqlParameter("@Error", SqlDbType.NVarChar, -1);
                    outParam.Direction = ParameterDirection.Output;
                    comm.Parameters.Add(outParam);

                    comm.ExecuteNonQuery();

                    if (!(outParam.Value is DBNull) && !string.IsNullOrEmpty((string)outParam.Value))
                    {
                        return (string)outParam.Value;
                    }
                    return "";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Actualiza un empleado. Por el momento solo actualiza la contraseña
        /// </summary>
        /// <param name="responsable">Usuario del sistema</param>
        /// <param name="pocoHuella">Huella</param>
        /// <returns>Mensaje de error o en blanco si no existe</returns>
        public string UpdateEmpleado(Guid responsable, POCOEmpleado pocoEmpleado)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                using (SqlCommand comm = new SqlCommand("", conn) { CommandType = CommandType.StoredProcedure })
                {
                    conn.Open();

                    if (string.IsNullOrEmpty(pocoEmpleado.Contraseña))
                    {
                        comm.CommandText = "ESA_Eliminar_Contraseña";
                        comm.Parameters.Add("@LoggedUserOid", SqlDbType.UniqueIdentifier).Value = responsable;
                        comm.Parameters.Add("@EmpleadoOid", SqlDbType.UniqueIdentifier).Value = pocoEmpleado.GuidEmpleado;
                        SqlParameter outParam = new SqlParameter("@Error", SqlDbType.NVarChar);
                        outParam.Direction = ParameterDirection.Output;
                        outParam.Size = -1; // nvarchar(max)
                        comm.Parameters.Add(outParam);

                        comm.ExecuteNonQuery();

                        if (!(outParam.Value is DBNull) && !string.IsNullOrEmpty((string)outParam.Value))
                        {
                            return (string)outParam.Value;
                        }
                        return null;
                    }
                    else
                    {
                        comm.CommandText = "ESA_Crear_Contraseña";
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Parameters.Clear();
                        comm.Parameters.Add("@LoggedUserOid", SqlDbType.UniqueIdentifier).Value = responsable;
                        comm.Parameters.Add("@EmpleadoOid", SqlDbType.UniqueIdentifier).Value = pocoEmpleado.GuidEmpleado;
                        comm.Parameters.Add("@Contraseña", SqlDbType.VarChar).Value = pocoEmpleado.Contraseña;
                        SqlParameter outParam = new SqlParameter("@Error", SqlDbType.NVarChar);
                        outParam.Direction = ParameterDirection.Output;
                        outParam.Size = -1; // nvarchar(max)
                        comm.Parameters.Add(outParam);

                        comm.ExecuteNonQuery();

                        if (!(outParam.Value is DBNull) && !string.IsNullOrEmpty((string)outParam.Value))
                        {
                            return (string)outParam.Value;
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region Turno Servicio Casino
        /// <summary>
        /// Inserta un Turno de Servicio a un Empleado
        /// </summary>
        /// <param name="responsable">Usuario del sistema</param>
        /// <param name="pocoHuella">Huella</param>
        /// <returns>Mensaje de error o en blanco si no existe</returns>
        public string InsertTurnoServicioCasino(Guid responsable, POCOEmpleadoTurnoServicioCasino empleadoTurnoServicioCasino)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                using (SqlCommand comm = new SqlCommand("", conn) { CommandType = CommandType.StoredProcedure })
                {
                    conn.Open();

                    comm.CommandText = "ESA_Crear_EmpleadoTurnoServicioCasino";

                    comm.Parameters.Add("@LoggedUserOid", SqlDbType.UniqueIdentifier).Value = responsable;
                    comm.Parameters.Add("@Empleado", SqlDbType.UniqueIdentifier).Value = empleadoTurnoServicioCasino.GuidEmpleado;
                    comm.Parameters.Add("@TurnoServicio", SqlDbType.UniqueIdentifier).Value = empleadoTurnoServicioCasino.GuidTurnoServicio;

                    SqlParameter outParam = new SqlParameter("@Error", SqlDbType.NVarChar);

                    outParam.Direction = ParameterDirection.Output;
                    outParam.Size = -1; // nvarchar(max)
                    comm.Parameters.Add(outParam);

                    comm.ExecuteNonQuery();

                    if (!(outParam.Value is DBNull) && !string.IsNullOrEmpty((string)outParam.Value))
                    {
                        return (string)outParam.Value;
                    }
                    return "";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Elimina un Turno de Servicio a un Empleado
        /// </summary>
        /// <param name="responsable">Usuario del sistema</param>
        /// <param name="pocoHuella">Huella</param>
        /// <returns>Mensaje de error o en blanco si no existe</returns>
        public string DeleteTurnoServicioCasino(Guid responsable, POCOEmpleadoTurnoServicioCasino empleadoTurnoServicioCasino)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                using (SqlCommand comm = new SqlCommand("", conn) { CommandType = CommandType.StoredProcedure })
                {
                    conn.Open();

                    comm.CommandText = "ESA_Eliminar_EmpleadoTurnoServicioCasino";

                    comm.Parameters.Add("@LoggedUserOid", SqlDbType.UniqueIdentifier).Value = responsable;
                    comm.Parameters.Add("@Empleado", SqlDbType.UniqueIdentifier).Value = empleadoTurnoServicioCasino.GuidEmpleado;
                    comm.Parameters.Add("@TurnoServicio", SqlDbType.UniqueIdentifier).Value = empleadoTurnoServicioCasino.GuidTurnoServicio;

                    SqlParameter outParam = new SqlParameter("@Error", SqlDbType.NVarChar);

                    outParam.Direction = ParameterDirection.Output;
                    outParam.Size = -1; // nvarchar(max)
                    comm.Parameters.Add(outParam);

                    comm.ExecuteNonQuery();

                    if (!(outParam.Value is DBNull) && !string.IsNullOrEmpty((string)outParam.Value))
                    {
                        return (string)outParam.Value;
                    }
                    return "";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region Contratos

        /// <summary>
        /// Actualiza un contrato. Lo que hace es Caducarlo
        /// </summary>
        /// <param name="responsable">Usuario del sistema</param>
        /// <param name="pocoHuella">Huella</param>
        /// <returns>Mensaje de error o en blanco si no existe</returns>
        public string UpdateContratos(Guid responsable, POCOContrato pocoContrato)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                using (SqlCommand comm = new SqlCommand("ESA_Caducar_Contrato", conn) { CommandType = CommandType.StoredProcedure })
                {
                    conn.Open();

                    comm.Parameters.Add("@LoggedUserOid", SqlDbType.UniqueIdentifier).Value = responsable;
                    comm.Parameters.Add("@ContratoOid", SqlDbType.UniqueIdentifier).Value = pocoContrato.GuidContrato;
                    if (pocoContrato.FinVigencia.HasValue)
                    {
                        comm.Parameters.Add("@FinVigencia", SqlDbType.DateTime).Value = pocoContrato.FinVigencia.Value;
                    }
                    else
                    {
                        comm.Parameters.Add("@FinVigencia", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    SqlParameter outParam = new SqlParameter("@Error", SqlDbType.NVarChar, -1);
                    outParam.Direction = ParameterDirection.Output;
                    comm.Parameters.Add(outParam);

                    comm.ExecuteNonQuery();

                    if (!(outParam.Value is DBNull) && !string.IsNullOrEmpty((string)outParam.Value))
                    {
                        return (string)outParam.Value;
                    }
                    return "";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Inserta un contrato a un empleado
        /// </summary>
        /// <param name="responsable">Usuario del sistema</param>
        /// <param name="pocoHuella">Huella</param>
        /// <returns>Mensaje de error o en blanco si no existe</returns>
        public string InsertarContratos(Guid responsable, POCOContrato pocoContrato)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                using (SqlCommand comm = new SqlCommand("ESA_Crear_Contrato", conn) { CommandType = CommandType.StoredProcedure })
                {
                    conn.Open();

                    comm.Parameters.Add("@LoggedUserOid", SqlDbType.UniqueIdentifier).Value = responsable;
                    comm.Parameters.Add("@Oid", SqlDbType.UniqueIdentifier).Value = pocoContrato.GuidContrato;
                    comm.Parameters.Add("@Empleado", SqlDbType.UniqueIdentifier).Value = pocoContrato.GuidEmpleado;
                    comm.Parameters.Add("@Empresa", SqlDbType.UniqueIdentifier).Value = pocoContrato.GuidEmpresa;
                    comm.Parameters.Add("@Cuenta", SqlDbType.UniqueIdentifier).Value = pocoContrato.GuidCuenta;
                    comm.Parameters.Add("@Cargo", SqlDbType.UniqueIdentifier).Value = pocoContrato.GuidCargo;
                    comm.Parameters.Add("@InicioVigencia", SqlDbType.DateTime).Value = pocoContrato.InicioVigencia;
                    comm.Parameters.Add("@CodigoContrato", SqlDbType.VarChar).Value = pocoContrato.CodigoContrato.Length > 100 ? pocoContrato.CodigoContrato.Substring(0, 100) : pocoContrato.CodigoContrato;
                    comm.Parameters.Add("@ConsideraColacion", SqlDbType.Bit).Value = pocoContrato.ConsideraAsistencia;
                    comm.Parameters.Add("@ConsideraCasino", SqlDbType.Bit).Value = pocoContrato.ConsideraCasino;

                    if (pocoContrato.FinVigencia.HasValue)
                    {
                        comm.Parameters.Add("@FinVigencia", SqlDbType.DateTime).Value = pocoContrato.FinVigencia.Value;
                    }
                    else
                    {
                        comm.Parameters.Add("@FinVigencia", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    SqlParameter outParam = new SqlParameter("@Error", SqlDbType.NVarChar, -1);
                    outParam.Direction = ParameterDirection.Output;
                    comm.Parameters.Add(outParam);

                    comm.ExecuteNonQuery();

                    if (!(outParam.Value is DBNull) && !string.IsNullOrEmpty((string)outParam.Value))
                    {
                        return (string)outParam.Value;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        #region Empleados Dispositivos


        public string InsertarEmpleadosDispositivos(Guid responsable, POCOEmpleadoDispositivo pocoEmpleadoDispositivo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                using (SqlCommand comm = new SqlCommand("ESA_Crear_Autorizacion", conn) { CommandType = CommandType.StoredProcedure })
                {
                    conn.Open();

                    comm.Parameters.Add("@LoggedUserOid", SqlDbType.UniqueIdentifier).Value = responsable;
                    comm.Parameters.Add("@Oid", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
                    comm.Parameters.Add("@Empleado", SqlDbType.UniqueIdentifier).Value = pocoEmpleadoDispositivo.GuidEmpleado;
                    comm.Parameters.Add("@Dispositivo", SqlDbType.UniqueIdentifier).Value = pocoEmpleadoDispositivo.GuidDispositivo;
                    SqlParameter outParam = new SqlParameter("@Error", SqlDbType.NVarChar, -1);
                    outParam.Direction = ParameterDirection.Output;
                    comm.Parameters.Add(outParam);

                    comm.ExecuteNonQuery();

                    if (!(outParam.Value is DBNull) && !string.IsNullOrEmpty((string)outParam.Value))
                    {
                        return (string)outParam.Value;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        #endregion

        #endregion
    }
}
