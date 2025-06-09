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
    public class ObterPorNomeAsyncTest
    { 
        [Fact]
        public async Task ObterPorNomeAsync_DeveRetornarTipo()
        {
            var tipo = new TipoRefeicao(1, "Jantar");
            var repoMock = new Mock<ITipoRefeicaoRepository>();
            repoMock.Setup(r => r.ObterPorNomeAsync("Jantar")).ReturnsAsync(tipo);

            var service = new TipoRefeicaoService(repoMock.Object);
            var result = await service.ObterPorNomeAsync("Jantar");

            Assert.Equal(tipo, result);
        }
    }
}
