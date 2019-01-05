using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnroladorAccesoDatos.Dominio {
    public class Notificacion {
        public string IdNotificacion { get; set; }
        public DateTime FechaNotificacion { get; set; }
        public string MensajeNotificacion { get; set; }
        public TipoNotificacion Tipo { get; set; }
        public Image ImagenDeNotificacion { get; set; }
    }
}
