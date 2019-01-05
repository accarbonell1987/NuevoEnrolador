using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnroladorAccesoDatos.Dominio {
    public class POCOTurnoServicio {
        public Guid GuidTurnoServicio { get; set; }
        public Guid GuidServicio { get; set; }
        public string NombreTurnoServicio { get; set; }
        public Boolean Vigente { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
    }
}
