using AutoMapper;
using Cidades.Infrastructure.Repository;
using Cidadess.Application.Service.Implementation;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace Cidades.Testes.Service;

public class CidadeServiceTeste
{
    private CidadesService _cidadeService;

    private CidadeServiceTeste()
    {
        _cidadeService = new CidadesService(new Mock<ICidadeRepository>().Object);
    }

    [Fact]
    public async Task ImportarCidades()
    {
        var exception = await Assert.ThrowsAsync<Exception>(() => _cidadeService.ImportarCidades(new Mock<IFormFile>().Object));
        Assert.Equal("Arquivo em branco", exception.Message);
    }
}