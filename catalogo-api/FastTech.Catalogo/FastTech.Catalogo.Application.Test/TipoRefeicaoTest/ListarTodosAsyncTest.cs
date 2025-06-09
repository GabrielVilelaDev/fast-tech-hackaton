using FastTech.Catalogo.Application.Services;
using FastTech.Catalogo.Domain.Entities;
using FastTech.Catalogo.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastTech.Catalogo.Application.Test.TipoRefeicaoTest
{
    public class ListarTodosAsyncTest
    {
        [Fact]
        public async Task ListarTodosAsync_DeveRetornarLista()
        {
            var lista = new List<TipoRefeicao> { new(1, "Café da Manhã") };
            var repoMock = new Mock<ITipoRefeicaoRepository>();
            repoMock.Setup(r => r.ListarTodosAsync()).ReturnsAsync(lista);

            var service = new TipoRefeicaoService(repoMock.Object);
            var result = await service.ListarTodosAsync();

            Assert.Single(result);
        }
    }
}
