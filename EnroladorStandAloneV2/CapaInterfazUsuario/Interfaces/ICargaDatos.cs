using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnroladorStandAloneV2.CapaInterfazUsuario.Interfaces {
    public interface ICargaDatos {
        void PrimerPaso(int totalPasos, int totalPrimero, string nombrePrimero);
        void SiguientePaso(int totalActual, string nombrePaso);
        void AvanzarActual();
    }
}
