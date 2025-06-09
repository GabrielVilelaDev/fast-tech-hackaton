using FastTech.Catalogo.Application.Services;
using FastTech.Catalogo.Domain.Entities;
using FastTech.Catalogo.Domain.Interfaces;
using FastTech.Catalogo.Domain.ValueObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastTech.Catalogo.Application.Test.ItemServiceTest
{
    public class AtualizarAsyncTest
    {
        [Fact]
        public async Task AtualizarAsync_DeveAtualizarSeItemExistir()
        {
            var tipo = new TipoRefeicao(1, "Almoço");
            var item = new Item("Item 1", "Desc", new Preco(10m), tipo);

            var itemRepo = new Mock<IItemRepository>();
            var tipoRepo = new Mock<ITipoRefeicaoRepository>();
            itemRepo.Setup(r => r.ObterPorIdAsync(item.Id)).ReturnsAsync(item);

            var service = new ItemService(itemRepo.Object, tipoRepo.Object);
            await service.AtualizarAsync(item);

            itemRepo.Verify(r => r.Atualizar(item), Times.Once);
            itemRepo.Verify(r => r.SalvarAlteracoesAsync(), Times.Once);
        }

        [Fact]
        public async Task AtualizarAsync_DeveLancarExcecaoSeNaoExistir()
        {
            var tipo = new TipoRefeicao(1, "Almoço");
            var item = new Item("Item 1", "Desc", new Preco(10m), tipo);

            var itemRepo = new Mock<IItemRepository>();
            var tipoRepo = new Mock<ITipoRefeicaoRepository>();
            itemRepo.Setup(r => r.ObterPorIdAsync(item.Id)).ReturnsAsync((Item)null!);

            var service = new ItemService(itemRepo.Object, tipoRepo.Object);
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.AtualizarAsync(item));
        }
    }
}
