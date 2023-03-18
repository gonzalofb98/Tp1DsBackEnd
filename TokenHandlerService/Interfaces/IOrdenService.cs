using Dominio.Entidades;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IOrdenService : IGenericService<OrdenDeProduccion, OrdenDeProduccionDto>
    {
        Task<int> CrearOrden(OrdenDeProduccionDto ordenDeProduccionDto);

        Task<int> ModificarOrden(int id, OrdenDeProduccionDto ordenDeProduccionDto);

        Task EliminarOrden(int id);

    }
}
