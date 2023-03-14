using Dominio.Entidades;
using Dto;

namespace Services.Interfaces
{
    public interface IDefectService : IGenericService<Defecto, DefectoDto>
    {
        Task<Defecto> CrearDefecto(DefectoDto defectoDto);

        Task<int> ModificarDefecto(int id, DefectoDto defectoDto);

        Task EliminarDefecto(int id);
    }
}
