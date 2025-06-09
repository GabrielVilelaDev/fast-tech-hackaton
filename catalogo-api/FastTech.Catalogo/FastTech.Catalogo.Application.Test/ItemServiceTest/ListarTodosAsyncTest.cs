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
    public class ListarTodosAsyncTest
    {
        [Fact]
        public async Task ListarTodosAsync_DeveRetornarItens()
        {
            var lista = new List<Item> { new("Item 1", "Desc", new Preco(10m), new TipoRefeicao(1, "Almoço")) };

            var itemRepo = new Mock<IItemRepository>();
            var tipoRepo = new Mock<ITipoRefeicaoRepository>();
            itemRepo.Setup(r => r.ListarTodosAsync()).ReturnsAsync(lista);

            var service = new ItemService(itemRepo.Object, tipoRepo.Object);
            var result = await service.ListarTodosAsync();

            Assert.Single(result);
        }
    }
}
