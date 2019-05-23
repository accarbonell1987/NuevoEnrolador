using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnroladorAccesoDatos.Dominio;

namespace EnroladorAccesoDatos.Ayudantes {
    public static class AyudanteLogs {
        /// <summary>
        /// Extensión utilizada para generar salva de log ante fallo inesperado, el nombre del archivo con el que se va a guardar
        /// </summary>
        /// <param name="ex">Exception ex</param>
        /// <param name="nombreArchivo">string nombreArchivo</param>
        /// <returns>Exception</returns>
        //public static Exception Log(this Exception ex, string nombreArchivo) {
        //    string mensaje = string.Concat(ex.InnerExceptions().Select(e => e.Message + "\n"));
            
        //    var nombreFichero = AyudanteDirectorioDatos.ObtenerDirectorioDelEnsamblado() + @"\" + String.Format(nombreArchivo + "-Log {0:yyyy-MM-dd}.log", DateTime.Now);
        //    var textoError = String.Format("{0:HH:mm:ss}: {1}\n {2}\n", DateTime.Now, "Programador", mensaje);
                
        //    File.AppendAllText(nombreFichero, textoError);

        //    return ex;
        //}

        /// <summary>
        /// Extensión utilizada para generar salva de log ante fallo inesperado, el nombre del archivo con el que se va a guardar
        /// </summary>
        /// <param name="ex">Exception ex</param>
        /// <param name="nombreArchivo">string nombreArchivo</param>
        /// <returns>Exception</returns>
        public static Exception Log(this Exception ex, string nombreArchivo, string nombreProcedimiento, List<POCONotificacion> lNotificaciones) {
            string mensaje = "";

            foreach (Exception error in ex.InnerExceptions()) {
                mensaje += (error.GetType() + ": " + error.Message + " - " + error.InnerException + Environment.NewLine);
            }
            DateTime ahora = DateTime.Now;

            var nombreFichero = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), String.Format(@"EnroladorV2\" + nombreArchivo + "-Log {0:yyyy-MM-dd}.log", ahora));

            var textoError = String.Format("{0:HH:mm:ss}: {1}-{2}\n {3}\n", ahora, "Programador", nombreProcedimiento, mensaje);
            if (File.Exists(nombreFichero))
                File.AppendAllText(nombreFichero, textoError);
            else {
                var fileDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), String.Format(@"EnroladorV2"));
                Directory.CreateDirectory(fileDir);
                File.AppendAllText(nombreFichero, textoError);
            }

            string idNotificacion = ahora.ToBinary().ToString();

            lNotificaciones.Add(new POCONotificacion() {
                IdNotificacion = idNotificacion,
                FechaNotificacion = ahora,
                MensajeNotificacion = textoError,
                Tipo = TipoNotificacion.Critica,
            });

            return ex;
        }

        public static Exception Log(this Exception ex, string nombreArchivo, string nombreProcedimiento) {
            string mensaje = "";

            foreach (Exception error in ex.InnerExceptions()) {
                mensaje += (error.GetType() + ": " + error.Message + " - " + error.InnerException + Environment.NewLine);
            }
            DateTime ahora = DateTime.Now;

            var nombreFichero = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), String.Format(@"EnroladorV2\"+nombreArchivo + "-Log {0:yyyy-MM-dd}.log", ahora));

            var textoError = String.Format("{0:HH:mm:ss}: {1}-{2}\n {3}\n", ahora, "Programador", nombreProcedimiento, mensaje);
            if (File.Exists(nombreFichero))
                File.AppendAllText(nombreFichero, textoError);
            else {
                var fileDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), String.Format(@"EnroladorV2"));
                Directory.CreateDirectory(fileDir);
                File.AppendAllText(nombreFichero, textoError);
            }

            return ex;
        }

        /// <summary>
        /// Obtener excepciones de forma recursiva
        /// </summary>
        /// <param name="exception">Exception exception</param>
        /// <returns>IEnumerable<Exception></returns>
        internal static IEnumerable<Exception> InnerExceptions(this Exception exception) {
            Exception ex = exception;

            while (ex != null) {
                yield return ex;
                ex = ex.InnerException;
            }
        }
    }
}