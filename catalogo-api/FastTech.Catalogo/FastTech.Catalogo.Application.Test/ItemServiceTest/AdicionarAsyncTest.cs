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
    public class AdicionarAsyncTest
    {
        [Fact]
        public async Task AdicionarAsync_DeveAdicionarSeTipoExistir()
        {
            var tipo = new TipoRefeicao(1, "Almoço");
            var item = new Item("Item 1", "Desc", new Preco(10m), tipo);

            var itemRepo = new Mock<IItemRepository>();
            var tipoRepo = new Mock<ITipoRefeicaoRepository>();
            tipoRepo.Setup(r => r.ObterPorIdAsync(1)).ReturnsAsync(tipo);

            var service = new ItemService(itemRepo.Object, tipoRepo.Object);
            await service.AdicionarAsync(item);

            itemRepo.Verify(r => r.AdicionarAsync(item), Times.Once);
            itemRepo.Verify(r => r.SalvarAlteracoesAsync(), Times.Once);
        }

        [Fact]
        public async Task AdicionarAsync_DeveLancarExcecaoSeTipoNaoExistir()
        {
            var tipo = new TipoRefeicao(99, "Inexistente");
            var item = new Item("Item 1", "Desc", new Preco(10m), tipo);

            var itemRepo = new Mock<IItemRepository>();
            var tipoRepo = new Mock<ITipoRefeicaoRepository>();
            tipoRepo.Setup(r => r.ObterPorIdAsync(tipo.Id)).ReturnsAsync((TipoRefeicao)null!);

            var service = new ItemService(itemRepo.Object, tipoRepo.Object);
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.AdicionarAsync(item));
        }
    }
}
