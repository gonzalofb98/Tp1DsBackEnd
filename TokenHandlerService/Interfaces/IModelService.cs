using Dominio.Entidades;
using Dto;

namespace Services.Interfaces
{
    public interface IModelService : IGenericService<Modelo, ModeloDto>
    {
        Task<Modelo> CrearModelo(ModeloDto lineaDto);

        Task<int> ModificarModelo(int id, ModeloDto lineaDto);

        Task EliminarModelo(int id);
    }
}
