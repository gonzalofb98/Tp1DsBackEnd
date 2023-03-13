using Dominio.Entidades;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IColorService : IGenericService<Color, ColorDto>
    {
        Task<Color> CrearColor(ColorDto lineaDto);

        Task<int> ModificarColor(int id, ColorDto lineaDto);

        Task EliminarColor(int id);
    
    }
}
