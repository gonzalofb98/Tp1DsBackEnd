using Dominio.Entidades;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ILineService : IGenericService<LineaDeTrabajo, LineaDeTrabajoDto>
    {
        Task<LineaDeTrabajo> CrearLineaDeTrabajo(LineaDeTrabajoDto lineaDto);

        Task<int> ModificarLineaDeTrabajo(int id, LineaDeTrabajoDto lineaDto);

        Task EliminarLineaDeTrabajo(int id);
    }
}
