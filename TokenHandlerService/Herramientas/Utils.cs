using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Herramientas
{
    public static class Utils
    {

        public static Turno GetTurnoActual(List<Turno> turnos)
        {
            var horaActual = DateTime.Now;
            foreach(var turno in turnos)
    {
                if (horaActual.TimeOfDay >= turno.HoraInicio.TimeOfDay && horaActual.TimeOfDay < turno.HoraFin.TimeOfDay)
                {
                    return turno;
                }
            }
            return null;
        }
    }
}
