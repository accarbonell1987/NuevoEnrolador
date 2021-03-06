﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnroladorAccesoDatos.Dominio {
    public class POCOEmpresa {
        public Guid GuidEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public List<POCOCuenta> Cuentas { get; set; }
        public List<POCOCargo> Cargos { get; set; }
    }
}
