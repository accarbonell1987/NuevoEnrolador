//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EnroladorAccesoDatos.SQLite
{
    using System;
    using System.Collections.Generic;
    
    public partial class Empleado
    {
        public long IdEmpleado { get; set; }
        public string GuidEmpleado { get; set; }
        public string RUT { get; set; }
        public Nullable<long> EnrollId { get; set; }
        public string Correo { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public Nullable<long> TieneContrasena { get; set; }
        public string Contrasena { get; set; }
        public string NumeroTelefono { get; set; }
        public Nullable<long> Sincronizado { get; set; }
    }
}
