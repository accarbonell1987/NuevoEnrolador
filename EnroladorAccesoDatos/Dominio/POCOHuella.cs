using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnroladorAccesoDatos.Dominio {
    public class POCOHuella {
        public Guid GuidHuella { get; set; }
        public Guid GuidEmpleado { get; set; }
        public TipoHuella Tipo { get; set; }
        public string Data { get; set; }
    }
}
