using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IGenericService<Entity,T,P> where T : class where Entity : EntidadBase
    {
        Task<Entity> GetAsync(int id);
        Task<List<Entity>> ListAsync(Expression<Func<Entity, bool>> predicate, params string[] includes);
        Task<List<Entity>> ListAsync(params string[] includes);
        Task<int> AgregarAsync(Entity item);
        Task<ICollection<Entity>> GetTodosAsync();
        Task<int> UpdateAsync(Entity item);
        Task DeleteAsync(int id);
        Task<ICollection<Entity>> GetConFiltro(Expression<Func<Entity, bool>> predicado);
        Task DeleteRange(IEnumerable<Entity> elements);
    }
}
