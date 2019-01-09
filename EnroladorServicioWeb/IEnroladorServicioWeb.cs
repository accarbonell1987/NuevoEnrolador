using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using EnroladorAccesoDatos.Dominio;

namespace EnroladorServicioWeb {
    [ServiceContract]
    public interface IEnroladorServicioWeb {

        #region Sistema
        [OperationContract]
        bool IdentificarHardware(Guid hardwareId);
        #endregion

        #region Autenticacion
        [OperationContract]
        POCOUsuario AutenticacionUsuario(string nombreUsuario, string claveUsuario);

        [OperationContract]
        POCOUsuario Revalidar(POCOUsuario Usuario);
        #endregion

        #region Lectura y Obtencion de Datos
        [OperationContract]
        IList<POCOHuella> LeeHuellasDelUsuario(Guid GuidUsuario);

        [OperationContract]
        IList<POCOCadena> LeeCadenasDelUsuario(Guid GuidUsuario);

        [OperationContract]
        IList<POCOCargo> LeeCargosDelUsuario(Guid GuidUsuario);

        [OperationContract]
        IList<POCOContrato> LeeContratosDelUsuario(Guid GuidUsuario);

        [OperationContract]
        IList<POCOCuenta> LeeCuentasDelUsuario(Guid GuidUsuario);

        [OperationContract]
        IList<POCODispositivo> LeeDispositivoDelUsuario(Guid GuidUsuario);

        [OperationContract]
        IList<POCOEmpleadoDispositivo> LeeEmpleadosDispositivos();

        [OperationContract]
        IList<POCOEmpresa> LeeEmpresaDelUsuario(Guid GuidUsuario);

        [OperationContract]
        IList<POCOHuella> LeeTodasHuellas();

        [OperationContract]
        IList<POCOInstalacion> LeeInstalacionesDelUsuario(Guid GuidUsuario);

        [OperationContract]
        IList<POCOEmpleado> LeeTodosEmpleados();

        [OperationContract]
        IList<POCOServicioCasino> LeeServicioCasinoDelUsuario(Guid GuidUsuario);

        [OperationContract]
        IList<POCOTurnoServicio> LeeTurnoServicioDelUsuario(Guid GuidUsuario);

        [OperationContract]
        IList<POCOEmpleadoTurnoServicioCasino> LeeEmpleadoTurnoServicioCasino(Guid GuidUsuario);
        #endregion

        #region Acciones

        #region Huellas

        [OperationContract]
        string InsertarHuella(Guid responsable, POCOHuella pocoHuella);

        [OperationContract]

        string ActualizarHuella(Guid responsable, POCOHuella pocoHuella);

        #endregion

        #region Empleados

        [OperationContract]
        string InsertarEmpleado(Guid responsable, POCOEmpleado pocoEmpleado);

        [OperationContract]
        string ActualizarEmpleado(Guid responsable, POCOEmpleado pocoEmpleado);

        #endregion

        #region Turno Servicio Casino
        [OperationContract]
        string InsertarEmpleadoTurnoServicioCasino(Guid responsable, POCOEmpleadoTurnoServicioCasino empleadoTurnoServicioCasino);

        [OperationContract]
        string EliminarEmpleadoTurnoServicioCasino(Guid responsable, POCOEmpleadoTurnoServicioCasino empleadoTurnoServicioCasino);
        #endregion

        #region Contratos

        [OperationContract]
        string ActualizarContrato(Guid responsable, POCOContrato pocoContrato);

        [OperationContract]
        string InsertarContrato(Guid responsable, POCOContrato pocoContrato);

        #endregion

        #region EmpleadosDispositivos

        [OperationContract]
        string InsertarEmpleadoDispositivo(Guid responsable, POCOEmpleadoDispositivo pocoEmpleadoDispositivo);

        #endregion

        #endregion
    }
}
