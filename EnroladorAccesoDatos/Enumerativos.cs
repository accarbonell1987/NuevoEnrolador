using System;

namespace EnroladorAccesoDatos {
    #region Enums
    public enum TipoHuella {
        [EnumDisplayName("Meñique Izquierdo")]
        MENIZQ,
        [EnumDisplayName("Anular Izquierdo")]
        ANUIZQ,
        [EnumDisplayName("Medio Izquierdo")]
        MEDIZQ,
        [EnumDisplayName("Indice Izquierdo")]
        INDIZQ,
        [EnumDisplayName("Pulgar Izquierdo")]
        PULIZQ,
        [EnumDisplayName("Pulgar Derecho")]
        PULDER,
        [EnumDisplayName("Indice Derecho")]
        INDDER,
        [EnumDisplayName("Medio Derecho")]
        MEDDER,
        [EnumDisplayName("Anular Derecho")]
        ANUDER,
        [EnumDisplayName("Meñique Derecho")]
        MENDER
    }
    public enum HuelleroSonidos {
        Correcto,
        Incorrecto,
        Solicitud
    }
    public enum ResultadoAutorizacion {
        Aceptado,
        Rechazado,
        Cancelado
    }
    public enum TipoDispositivo {
        Invalido,
        [EnumDisplayName("SCCS.Module.BusinessObjects.Dispositivos.Reloj.Reloj")]
        Reloj,
        [EnumDisplayName("SCCS.Module.BusinessObjects.Dispositivos.PuntoAcceso.PuntoAcceso")]
        PuntoAcceso
    }
    public enum TipoNotificacion {
        Exito,
        Cuidado,
        Critica,
        Informativa
    }
    public enum TipoIdentificacion {
        Huella,
        Clave
    }
    public enum EstadoContrato {
        Activo,
        Vencido
    }
    public enum TipoSincronizacion {
        SinModificacion = 0,
        Insertar = 1,
        Eliminar = 2,
        Modificar = 3
    }
    public enum EstadoObjeto {
        Almacenado = 0,
        Almacenar = 1,
        Eliminar = 2
    }
    public enum EstadoUsoSistema {
        Habilitado = 0,
        Inhabilitado = 1
    }
    public enum EstadoHuellero {
        Habilitado = 0,
        Inhabilitado = 1
    }
    #endregion

    #region Clases
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EnumDisplayNameAttribute : Attribute {
        public EnumDisplayNameAttribute(string displayName) {
            DisplayName = displayName;
        }

        public string DisplayName { get; set; }
    }
    #endregion
}
