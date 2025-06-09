using FastTech.Catalogo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastTech.Catalogo.Application.Interfaces
{
    public interface ITipoRefeicaoService
    {
        Task<IEnumerable<TipoRefeicao>> ListarTodosAsync();
        Task<TipoRefeicao?> ObterPorIdAsync(int id);
        Task<TipoRefeicao?> ObterPorNomeAsync(string nome);
    }
}
