using FastTech.Catalogo.Application.Interfaces;
using FastTech.Catalogo.Domain.Entities;
using FastTech.Catalogo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastTech.Catalogo.Application.Services
{
    public class TipoRefeicaoService : ITipoRefeicaoService
    {
        private readonly ITipoRefeicaoRepository _repository;

        public TipoRefeicaoService(ITipoRefeicaoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TipoRefeicao>> ListarTodosAsync()
            => await _repository.ListarTodosAsync();

        public async Task<TipoRefeicao?> ObterPorIdAsync(int id)
            => await _repository.ObterPorIdAsync(id);

        public async Task<TipoRefeicao?> ObterPorNomeAsync(string nome)
            => await _repository.ObterPorNomeAsync(nome);
    }
}
