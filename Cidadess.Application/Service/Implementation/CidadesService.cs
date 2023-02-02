using Microsoft.AspNetCore.Http;
using Cidades.Domain.ValueObject;
using Cidades.Infrastructure.Repository;

namespace Cidadess.Application.Service.Implementation;

public class CidadesService : ICidadesService
{
    private readonly ICidadeRepository _cidadesRepository;

    public CidadesService(ICidadeRepository cidadesRepository)
    {
        _cidadesRepository = cidadesRepository;
    }

    public async Task<bool> ImportarCidades(IFormFile arquivo)
    {
        if (arquivo.Length == 0)
            return false;

        IList<CidadesVO> cidadesNovas = new List<CidadesVO>();
        IList<CidadesVO> cidadesAtualizar = new List<CidadesVO>();
        using StreamReader sr = new StreamReader(arquivo.OpenReadStream());
        while (!sr.EndOfStream)
        {
            var linha = sr.ReadLine()?.Split(';');

            long vl1 = 0;
            long vl2 = 0;
            long vl3 = 0;
            if (long.TryParse(linha[1], out vl1) && long.TryParse(linha[2], out vl2) &&
                long.TryParse(linha[6], out vl3))
            {
                var cidadeVo = new CidadesVO()
                {
                    IBGE = long.Parse(linha[1]),
                    IBGE7 = long.Parse(linha[2]),
                    UF = linha[3],
                    Municipio = linha[4],
                    Regiao = linha[5],
                    Populacao = long.Parse(linha[6]),
                    Porte = linha[7]
                };
                var existeCidade = _cidadesRepository.LocalizarCidade(cidadeVo.IBGE);

                if (existeCidade != null)
                {
                    cidadeVo.id = existeCidade.Result.id;
                    await _cidadesRepository.Atualizar(cidadeVo);
                }
                else
                {
                    await _cidadesRepository.Adicionar(cidadeVo);
                }
            }
        }

        return true;
    }

    public async Task<IEnumerable<CidadesPortePopulacaoVO>> ListarPortePopulacao()
    {
       return await _cidadesRepository.PortePopulacao();
    }

    public async Task<IEnumerable<CidadesVO>> Buscar3MaioresCidaes(string pesquisa)
    {
        return await _cidadesRepository.Listar3MaioresCidade(pesquisa);
    }
}