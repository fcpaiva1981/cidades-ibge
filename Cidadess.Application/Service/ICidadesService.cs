using Cidades.Domain.ValueObject;
using Microsoft.AspNetCore.Http;

namespace Cidadess.Application.Service;

public interface ICidadesService
{
    Task<bool> ImportarCidades(IFormFile arquivo);

    Task<IEnumerable<CidadesPortePopulacaoVO>> ListarPortePopulacao();

    Task<IEnumerable<CidadesVO>> Buscar3MaioresCidaes(string pesquisa);

}