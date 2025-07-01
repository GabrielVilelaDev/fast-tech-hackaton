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
            return item is null ? null : MapearItemParaOutput(item);
        }

        public async Task<IEnumerable<ItemOutputDto>> ListarTodosAsync()
        {
            var itens = await _itemRepository.ListarTodosAsync();
            return MapearItensParaOutputs(itens);
        }

        public async Task<IEnumerable<ItemOutputDto>> ListarPorTipoAsync(Guid tipoRefeicaoId)
        {
            var itens = await _itemRepository.ListarPorTipoAsync(tipoRefeicaoId);
            return itens is null || !itens.Any() ? [] : MapearItensParaOutputs(itens);
        }

        public async Task<Guid> AdicionarAsync(ItemInputDto dto)
        {
            var tipo = await _tipoRefeicaoRepository.ObterPorIdAsync(dto.TipoRefeicaoId);
            if (tipo is null)
                throw new InvalidOperationException("Tipo de refeição inválido.");

            var itemMesmoNome = await _itemRepository.ObterPorNomeAsync(dto.Nome);
            if(itemMesmoNome is not null)
                throw new InvalidOperationException("Já existe um item com o mesmo nome.");

            var item = new Item(dto.Nome, dto.Descricao, tipo, new Preco(dto.Valor));
            await _itemRepository.AdicionarAsync(item);
            await _itemRepository.SalvarAlteracoesAsync();

            return item.Id;
        }

        public async Task AtualizarAsync(ItemUpdateDto dto)
        {
            var existente = await _itemRepository.ObterPorIdAsync(dto.Id);
            if (existente is null)
                throw new ArgumentException("Item não encontrado.");

            var tipo = await _tipoRefeicaoRepository.ObterPorIdAsync(dto.TipoRefeicaoId);
            if (tipo is null)
                throw new ArgumentException("Tipo de refeição inválido.");

            var itemMesmoNome = await _itemRepository.ObterPorNomeAsync(dto.Nome);
            if (itemMesmoNome is not null && itemMesmoNome.Id != dto.Id)
                throw new InvalidOperationException("Já existe um item com o mesmo nome.");

            existente.Atualizar(dto.Nome, dto.Descricao, tipo, new Preco(dto.Valor));
            _itemRepository.Atualizar(existente);
            await _itemRepository.SalvarAlteracoesAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            var existente = await _itemRepository.ObterPorIdAsync(id);

            if (existente is null)
                throw new InvalidOperationException("Item não encontrado.");

            existente.Excluir();

            _itemRepository.Atualizar(existente);
            await _itemRepository.SalvarAlteracoesAsync();
        }

        private static IEnumerable<ItemOutputDto> MapearItensParaOutputs(IEnumerable<Item> itens)
        {
            return itens.Select(i => MapearItemParaOutput(i));
        }

        private static ItemOutputDto MapearItemParaOutput(Item item)
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
