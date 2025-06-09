using FastTech.Catalogo.Domain.Interfaces;
using FastTech.Catalogo.Infrastructure.Persistence.Command;
using FastTech.Catalogo.Infrastructure.Persistence.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FastTech.Catalogo.Infrastructure.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly CatalogoCommandDbContext _commandContext;
        protected readonly CatalogoQueryDbContext _queryContext;
        protected readonly DbSet<T> _commandSet;
        protected readonly DbSet<T> _querySet;

        protected RepositoryBase(CatalogoCommandDbContext commandContext, CatalogoQueryDbContext queryContext)
        {
            _commandContext = commandContext;
            _queryContext = queryContext;
            _commandSet = _commandContext.Set<T>();
            _querySet = _queryContext.Set<T>();
        }

        public async Task<IEnumerable<T>> ListarAsync(Expression<Func<T, bool>>? filtro = null)
        {
            var query = _querySet.AsNoTracking();

            if (filtro != null)
                query = query.Where(filtro);

            return await query.ToListAsync();
        }

        public async Task<T?> ObterAsync(Expression<Func<T, bool>> filtro)
            => await _querySet.AsNoTracking().FirstOrDefaultAsync(filtro);

        public async Task<bool> ExisteAsync(Expression<Func<T, bool>> filtro)
            => await _querySet.AsNoTracking().AnyAsync(filtro);

        public async Task AdicionarAsync(T entidade)
            => await _commandSet.AddAsync(entidade);

        public void Atualizar(T entidade)
            => _commandSet.Update(entidade);

        public void Remover(T entidade)
            => _commandSet.Remove(entidade);

        public async Task SalvarAlteracoesAsync()
            => await _commandContext.SaveChangesAsync();
    }
}
