using FastTech.Catalogo.Application.Dtos;
using FastTech.Catalogo.Domain.Entities;
using FastTech.Catalogo.Domain.Interfaces;
using FastTech.Catalogo.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastTech.Catalogo.Application.Interfaces
{
    interface IItemService
    {
        Task<ItemOutputDto?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<ItemOutputDto>> ListarTodosAsync();
        Task<IEnumerable<ItemOutputDto>> ListarPorTipoAsync(int tipoRefeicaoId);
        Task AdicionarAsync(ItemInputDto dto);
        Task AtualizarAsync(ItemUpdateDto dto);
        Task RemoverAsync(Guid id);
    }
}
