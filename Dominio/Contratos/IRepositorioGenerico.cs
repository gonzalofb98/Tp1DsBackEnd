using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Contratos
{
    public interface IRepositorioGenerico<T> where T : EntidadBase
    {
        Task<T> GetAsync(int id);
        Task<int> AgregarAsync(T item);
        Task<ICollection<T>> GetTodosAsync();
        Task<int> UpdateAsync(T item);
        Task DeleteAsync(int id);
        Task<ICollection<T>> GetConFiltro(Expression<Func<T, bool>> predicado);
        Task DeleteRange(IEnumerable<T> elements);
    }
}
