using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnroladorAccesoDatos.Dominio {
    public class POCOInstalacion {
        //los casinos son instalaciones
        public Guid GuidInstalacion { get; set; }
        public Guid GuidCadena { get; set; }
        public string NombreInstalacion { get; set; }
        public string NombreCadena { get; set; }
        public List<POCODispositivo> Dispositivos { get; set; }
        public List<POCOServicioCasino> ServiciosDelCasino { get; set; }
    }
}
