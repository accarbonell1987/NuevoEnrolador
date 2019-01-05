using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnroladorAccesoDatos.Dominio {
    public class POCOCargo {
        public Guid GuidCargo { get; set; }
        public Guid GuidEmpresa { get; set; }
        public string NombreCargo { get; set; }
    }
}
