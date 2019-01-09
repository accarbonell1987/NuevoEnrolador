using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnroladorAccesoDatos.Dominio {
    public class POCOEmpleadoTurnoServicioCasino {
        public Guid GuidEmpleado { get; set; }
        public Guid GuidTurnoServicio { get; set; }

        public string NombreServicio { get; set; }
        public string NombreTurno { get; set; }
        public string NombreCasino { get; set; } //es el nombre de la instalacion
        public Boolean Vigente { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
    }
}
