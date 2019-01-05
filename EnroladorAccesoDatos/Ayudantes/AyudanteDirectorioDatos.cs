using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EnroladorAccesoDatos.Ayudantes {
    public class AyudanteDirectorioDatos {
        #region Atributos

        #endregion

        #region Contructor

        #endregion

        #region Metodos
        /// <summary>
        /// Para conocer si existe un fichero o archivo
        /// </summary>
        /// <param name="ruta">string ruta</param>
        /// <returns>bool</returns>
        public static bool ExisteArchivo(string ruta) {
            return File.Exists(ruta) || Directory.Exists(ruta);
        }

        public static string ObtenerDirectorioDelEnsamblado() {
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly == null) return null;
            string appPath = entryAssembly.Location;
            return Path.GetDirectoryName(appPath);
        }
        #endregion
    }
}
