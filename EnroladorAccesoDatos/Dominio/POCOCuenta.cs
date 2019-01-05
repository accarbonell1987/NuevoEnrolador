using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnroladorAccesoDatos.Dominio {
    public class POCOCuenta {
        public Guid GuidCuenta { get; set; }
        public Guid GuidEmpresa { get; set; }
        public string NombreCuenta { get; set; }
        public DateTime? FechaUltimoCierre { get; set; }
    }
}
