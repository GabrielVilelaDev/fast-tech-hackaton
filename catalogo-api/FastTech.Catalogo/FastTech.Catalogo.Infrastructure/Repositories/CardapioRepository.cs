using System;
using System.Linq.Expressions;
using FastTech.Catalogo.Domain.Entities;
using FastTech.Catalogo.Domain.Interfaces;
using FastTech.Catalogo.Infrastructure.Persistence.Command;
using FastTech.Catalogo.Infrastructure.Persistence.Query;
using Microsoft.EntityFrameworkCore;

namespace FastTech.Catalogo.Infrastructure.Repositories;

public class CardapioRepository : RepositoryBase<Cardapio>, ICardapioRepository
{
    public CardapioRepository(CatalogoCommandDbContext commandContext, CatalogoQueryDbContext queryContext)
    : base(commandContext, queryContext) { }

    public async Task<Cardapio?> ObterPorNomeAsync(string nome)
    => await _querySet.AsNoTracking()
        .FirstOrDefaultAsync(t => t.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase) && t.DataExclusao == null);

    public async Task<Cardapio?> ObterPorIdAsync(Guid id)
    => await _querySet
        .AsNoTracking()
        .FirstOrDefaultAsync(i => i.Id == id && i.DataExclusao == null);
}
