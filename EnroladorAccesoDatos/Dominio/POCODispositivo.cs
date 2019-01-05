﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnroladorAccesoDatos.Dominio {
    public class POCODispositivo {
        public Guid GuidDispositivo { get; set; }
        public Guid GuidInstalacion { get; set; }
        public string NombreDispositivo { get; set; }
        public string Host { get; set; }
        public int Puerto { get; set; }
        public TipoDispositivo Tipo {get; set;}
    }
}
