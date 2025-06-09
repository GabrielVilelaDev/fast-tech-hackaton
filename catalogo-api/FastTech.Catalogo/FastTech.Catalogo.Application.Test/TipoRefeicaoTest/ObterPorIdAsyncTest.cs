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
    public class ObterPorIdAsyncTest
    {
        [Fact]
        public async Task ObterPorIdAsync_DeveRetornarTipo()
        {
            var tipo = new TipoRefeicao(1, "Almoço");
            var repoMock = new Mock<ITipoRefeicaoRepository>();
            repoMock.Setup(r => r.ObterPorIdAsync(1)).ReturnsAsync(tipo);

            var service = new TipoRefeicaoService(repoMock.Object);
            var result = await service.ObterPorIdAsync(1);

            Assert.Equal(tipo, result);
        }
    }
}
