using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnroladorAccesoDatos.Dominio {
    public class POCOEmpleadoDispositivo : POCOGlobal {
        public Guid GuidDispositivo { get; set; }
        public Guid GuidEmpleado { get; set; }
    }
}
