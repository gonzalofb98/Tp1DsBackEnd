using Dominio.Entidades;
using System.Linq.Expressions;
using Dominio.Contratos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Datos
{
    public class RepositorioGenerico<T> : IRepositorioGenerico<T>
        where T : EntidadBase
    {
        private readonly ControlCalidadContexto contexto;

        public RepositorioGenerico(ControlCalidadContexto contexto)
        {
            this.contexto = contexto;
        }

        public async Task<int> AgregarAsync(T item)
        {
            await contexto.Set<T>().AddAsync(item);
            await contexto.SaveChangesAsync();
            return item.Id;
        }

        public async Task<T> GetAsync(int id)
        {
            return await contexto.Set<T>()
                .FindAsync(id);
        }

        public async Task<ICollection<T>> GetTodosAsync()
        {
            return await contexto.Set<T>()
                .ToListAsync();
        }
        public async Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            return await Include(contexto.Set<T>(), includes).Where(predicate).ToListAsync();
        }
        public async Task<List<T>> ListAsync(params string[] includes)
        {
            return await Include(contexto.Set<T>(), includes).ToListAsync();
        }
        private IQueryable<T> Include(IQueryable<T> query, string[] includes)
        {
            var includedQuery = query;

            foreach (var include in includes)
            {
                includedQuery = includedQuery.Include(include);
            }

            return includedQuery;
        }

        public async Task<int> UpdateAsync(T item)
        {
            contexto.Set<T>().Attach(item);
            contexto.Entry(item).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return item.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var item = await contexto.Set<T>()
                .SingleOrDefaultAsync(p => p.Id == id);

            if (item != null)
            {
                contexto.Set<T>().Attach(item);
                contexto.Entry(item).State = EntityState.Deleted;
                await contexto.SaveChangesAsync();
            }
        }

        public async Task<ICollection<T>> GetConFiltro(Expression<Func<T, bool>> predicado)
        {
            return await contexto.Set<T>().Where(predicado).ToListAsync(); ;
        }

        public async Task DeleteRange(IEnumerable<T> elements)
        {
            contexto.Set<T>().RemoveRange(elements);
            await contexto.SaveChangesAsync();
        }
    }
}
