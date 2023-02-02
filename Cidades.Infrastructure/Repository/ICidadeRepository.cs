using Cidades.Domain.ValueObject;

namespace Cidades.Infrastructure.Repository;

public interface ICidadeRepository
{
    Task<CidadesVO> Adicionar(CidadesVO cidadesVo);

    Task<CidadesVO> Atualizar(CidadesVO cidadesVo);

    Task<CidadesVO?> LocalizarCidade(long ibge);

    Task<IList<CidadesPortePopulacaoVO>> PortePopulacao();

    Task<IEnumerable<CidadesVO>> Listar3MaioresCidade(string pesquisa);

}