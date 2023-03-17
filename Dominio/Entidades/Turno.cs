using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Turno : EntidadBase
    {
        public Turno(DateTime horaInicio, DateTime horaFin, string descripcion)
        {
            HoraInicio = horaInicio;
            HoraFin = horaFin;
            Descripcion = descripcion;
        }

        #region Propiedades
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
        public string Descripcion { get; set; }
        #endregion

        public List<int> getTotalHoras()
        {
            var listaHoras = new List<int>();
            for (var i = HoraInicio.Hour; i < HoraFin.Hour; i++)
            {
                listaHoras.Add(i);
                if (i+1 == HoraFin.Hour && HoraFin.Minute > 0)
                    listaHoras.Add(i+1);
            }
            return listaHoras;
        }
    }
}
