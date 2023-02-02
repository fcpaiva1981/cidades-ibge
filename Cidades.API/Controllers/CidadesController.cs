using Cidadess.Application.Service;
using Microsoft.AspNetCore.Mvc;


namespace Cidades.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CidadesController : Controller
{

    private readonly ICidadesService _cidadesService;

    public CidadesController(ICidadesService cidadesService)
    {
        _cidadesService = cidadesService ?? throw new ArgumentNullException(nameof(cidadesService));
    }

    /// <summary>
    /// Importa arquivo CSV com as cidades.
    /// </summary>
    [HttpPost("importacao-arquivo")]
    public async Task<IActionResult> Importar(IFormFile arquivo)
    {
        var result  = await _cidadesService.ImportarCidades(arquivo);
        return Ok(result);
    }

    /// <summary>
    /// Retorna a lista contendo a média por Região, UF e Porte das cidades
    /// </summary>
    [HttpGet]
    [Route("mediapopulacional")]
    public async Task<IActionResult> MediaPopulacional()
    {
        var response = await _cidadesService.ListarPortePopulacao();

       return Ok(response);
    }

    /// <summary>
    /// Pesquisa as 3 maiores cidades por UF ou Região
    /// </summary>
    [HttpGet]
    [Route("maiorescidades")]
    public async Task<IActionResult> BucarMaioresCidades(string pesquisa)
    {
        var response = await _cidadesService.Buscar3MaioresCidaes(pesquisa);
        return Ok(response);
    }
}