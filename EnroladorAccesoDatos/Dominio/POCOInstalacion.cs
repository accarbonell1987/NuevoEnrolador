using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnroladorAccesoDatos.Dominio {
    public class POCOInstalacion {
        public Guid GuidInstalacion { get; set; }
        public Guid GuidCadena { get; set; }
        public string NombreInstalacion { get; set; }
    }
}
