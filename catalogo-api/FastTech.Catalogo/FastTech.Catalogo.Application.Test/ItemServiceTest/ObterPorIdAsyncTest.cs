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
    public class ObterPorIdAsyncTest
    {

        [Fact]
        public async Task ObterPorIdAsync_DeveRetornarItemSeExistir()
        {
            var item = new Item("Item 1", "Descrição", new Preco(10m), new TipoRefeicao(1, "Almoço"));
            var itemRepo = new Mock<IItemRepository>();
            var tipoRepo = new Mock<ITipoRefeicaoRepository>();
            itemRepo.Setup(r => r.ObterPorIdAsync(item.Id)).ReturnsAsync(item);

            var service = new ItemService(itemRepo.Object, tipoRepo.Object);
            var result = await service.ObterPorIdAsync(item.Id);

            Assert.Equal(item, result);
        }
    }
}
