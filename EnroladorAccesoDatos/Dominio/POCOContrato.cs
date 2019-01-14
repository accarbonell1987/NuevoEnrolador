using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnroladorAccesoDatos.Dominio {
    public class POCOContrato : POCOGlobal {
        public Guid GuidContrato { get; set; }
        public Guid GuidEmpresa { get; set; }
        public Guid GuidCuenta { get; set; }
        public Guid GuidCargo { get; set; }
        public Guid GuidEmpleado { get; set; }
        public DateTime InicioVigencia { get; set; }
        public DateTime? FinVigencia { get; set; }
        public string CodigoContrato { get; set; }
        public bool ConsideraAsistencia { get; set; }
        public bool ConsideraCasino { get; set; }
        public string Descripcion { get; set; }
        public EstadoContrato Estado { get; set; }

        public string NombreEmpresa { get; set; }
        public string NombreCuenta { get; set; }
        public string NombreCargo { get; set; }
    }
}
