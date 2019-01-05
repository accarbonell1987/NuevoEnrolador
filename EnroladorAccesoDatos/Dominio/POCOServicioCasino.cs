using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnroladorAccesoDatos.Dominio {
    public class POCOServicioCasino {
        public Guid GuidServicioCasino { get; set; }
        public Guid GuidCasino { get; set; }
        public string NombreServicioCasino { get; set; }
        public Boolean Vigente { get; set; }
    }
}
