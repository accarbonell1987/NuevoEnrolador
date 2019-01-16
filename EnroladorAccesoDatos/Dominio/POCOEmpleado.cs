using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnroladorAccesoDatos.Dominio {
    public class POCOEmpleado : POCOGlobal, ICloneable {
        #region Atributos
        public Guid GuidEmpleado { get; set; }
        public string RUT { get; set; }
        public int EnrollId { get; set; }
        public string Correo { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public bool TieneContraseña { get; set; }
        public string TipoIdentificacion { get; set; }
        public string Contraseña { get; set; }
        public string NumeroTelefono { get; set; }
        public List<POCOContrato> Contratos { get; set; }
        public List<POCOHuella> Huellas { get; set; }
        public List<POCODispositivo> Dispositivos { get; set; }
        public List<POCOEmpleadoTurnoServicioCasino> TurnoServicioCasino { get; set; }
        #endregion

        #region Constructor
        public POCOEmpleado() {
            Contratos = new List<POCOContrato>();
            Huellas = new List<POCOHuella>();
            Dispositivos = new List<POCODispositivo>();
        }

        public object Clone() {
            return MemberwiseClone();
        }
        #endregion
    }
}
