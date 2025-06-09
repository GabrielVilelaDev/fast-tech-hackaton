using FastTech.Catalogo.Application.Dtos;
using FastTech.Catalogo.Application.Interfaces;
using FastTech.Catalogo.Domain.Entities;
using FastTech.Catalogo.Domain.Interfaces;
using FastTech.Catalogo.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastTech.Catalogo.Application.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ITipoRefeicaoRepository _tipoRefeicaoRepository;

        public ItemService(IItemRepository itemRepository, ITipoRefeicaoRepository tipoRefeicaoRepository)
        {
            _itemRepository = itemRepository;
            _tipoRefeicaoRepository = tipoRefeicaoRepository;
        }

        public async Task<ItemOutputDto?> ObterPorIdAsync(Guid id)
        {
            var item = await _itemRepository.ObterPorIdAsync(id);
            return item is null ? null : MapToOutput(item);
        }

        public async Task<IEnumerable<ItemOutputDto>> ListarTodosAsync()
        {
            var itens = await _itemRepository.ListarTodosAsync();
            return itens.Select(MapToOutput);
        }

        public async Task<IEnumerable<ItemOutputDto>> ListarPorTipoAsync(int tipoRefeicaoId)
        {
            var itens = await _itemRepository.ListarPorTipoAsync(tipoRefeicaoId);
            return itens.Select(MapToOutput);
        }

        public async Task AdicionarAsync(ItemInputDto dto)
        {
            var tipo = await _tipoRefeicaoRepository.ObterPorIdAsync(dto.TipoRefeicaoId);
            if (tipo is null)
                throw new InvalidOperationException("Tipo de refeição inválido.");

            var item = new Item(dto.Nome, dto.Descricao, tipo, new Preco(dto.Valor));
            await _itemRepository.AdicionarAsync(item);
            await _itemRepository.SalvarAlteracoesAsync();
        }

        public async Task AtualizarAsync(ItemUpdateDto dto)
        {
            var existente = await _itemRepository.ObterPorIdAsync(dto.Id);
            if (existente is null)
                throw new InvalidOperationException("Item não encontrado.");

            var tipo = await _tipoRefeicaoRepository.ObterPorIdAsync(dto.TipoRefeicaoId);
            if (tipo is null)
                throw new InvalidOperationException("Tipo de refeição inválido.");

            existente.Atualizar(dto.Nome, dto.Descricao, tipo, new Preco(dto.Valor));
            _itemRepository.Atualizar(existente);
            await _itemRepository.SalvarAlteracoesAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            var existente = await _itemRepository.ObterPorIdAsync(id);
            if (existente is null)
                throw new InvalidOperationException("Item não encontrado.");

            _itemRepository.Remover(existente);
            await _itemRepository.SalvarAlteracoesAsync();
        }

        private static ItemOutputDto MapToOutput(Item item)
        {
            return new ItemOutputDto
            {
                Id = item.Id,
                Nome = item.Nome,
                Descricao = item.Descricao,
                TipoRefeicaoNome = item.TipoRefeicao?.Nome ?? string.Empty,
                Valor = item.Preco.Valor
            };
        }
    }
}
