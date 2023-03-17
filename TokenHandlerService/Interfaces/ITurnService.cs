using Dominio.Entidades;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ITurnService : IGenericService<Turno, TurnoDto>
    {
        Task<Turno> CrearTurno(TurnoDto turnoDto);

        Task<int> ModificarTurno(int id, TurnoDto turnoDto);

        Task EliminarTurno(int id);

        Task<List<int>> GetTotalHorasByDescripcion(string desc);
    }
}
