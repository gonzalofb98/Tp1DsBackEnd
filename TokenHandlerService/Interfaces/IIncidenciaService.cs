using Dominio.Entidades;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IIncidenciaService : IGenericService<Incidencia, IncidenciaDto>
    {
        Task<int> CrearIncidencia(string nroOp, IncidenciaDto incidenciaDto);

        Task EliminarIncidencia(int id);

        Task<List<Incidencia>> GetByDefecto(string defecto);
    }
}
