using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EnroladorAccesoDatos.Dominio;

namespace EnroladorAccesoDatos.Ayudantes {
    public class AyudanteSQL {
        /// <summary>
        /// Ejecutar Consulta SQL
        /// </summary>
        /// <param name="consulta">string consulta</param>
        /// <param name="connectionString">string connectionString</param>
        /// <param name="lNotificaciones">List<Notificacion> lNotificaciones</param>
        public static void EjecutarConsulta(string consulta, string connectionString, List<Notificacion> lNotificaciones) {
            try {

                SqlConnection SQLCnx = new SqlConnection(connectionString);

                SqlCommand SQLCmd = new SqlCommand(consulta, SQLCnx);
                SQLCmd.Connection.Open();
                SQLCmd.ExecuteNonQuery();
                SQLCmd.Connection.Close();

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
            }
        }

        /// <summary>
        /// Ejecutar Consulta SQL
        /// </summary>
        /// <param name="consulta">string consulta</param>
        /// <param name="connectionString">string connectionString</param>
        public static void EjecutarConsulta(string consulta, string connectionString) {
            try {

                SqlConnection SQLCnx = new SqlConnection(connectionString);

                SqlCommand SQLCmd = new SqlCommand(consulta, SQLCnx);
                SQLCmd.Connection.Open();
                SQLCmd.ExecuteNonQuery();
                SQLCmd.Connection.Close();

            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Ejecutar Consulta SQL
        /// </summary>
        /// <param name="consulta">string consulta</param>
        /// <param name="contexto">DbContext contexto</param>
        /// <param name="lNotificaciones">List<Notificacion> lNotificaciones</param>
        public static void EjecutarConsulta(string consulta, DbContext contexto, List<Notificacion> lNotificaciones) {
            try {
                if (contexto != null) 
                    contexto.Database.ExecuteSqlCommand(consulta);
            } catch (Exception eX) {
                AyudanteLogs.Log(eX, "EnroladorStandAloneV2", MethodBase.GetCurrentMethod().Name, lNotificaciones);
            }
        }
    }
}
