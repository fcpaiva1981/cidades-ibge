using AutoMapper;
using Cidades.Domain.Model.Context;
using Cidades.Domain.ValueObject;
using Microsoft.EntityFrameworkCore;

namespace Cidades.Infrastructure.Repository.Impementation;

public class CidadeRepository : ICidadeRepository
{
    private readonly SQLiteContext _context;
    private IMapper _mapper;

    public CidadeRepository(SQLiteContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<CidadesVO> Adicionar(CidadesVO cidadesVo)
    {
        var cidade = _mapper.Map<Domain.Model.Cidades>(cidadesVo);
        _context.Cidades.Add(cidade);
        await _context.SaveChangesAsync();
        return _mapper.Map<CidadesVO>(cidade);
    }

    public async Task<CidadesVO> Atualizar(CidadesVO cidadesVo)
    {
        var cidadeAtualizar = _mapper.Map<Domain.Model.Cidades>(cidadesVo);
        _context.Cidades.Update(cidadeAtualizar);
        await _context.SaveChangesAsync();
        return _mapper.Map<CidadesVO>(cidadeAtualizar);
    }

    public async Task<CidadesVO?> LocalizarCidade(long ibge)
    {
        var cidade = await _context.Cidades.AsNoTracking().SingleOrDefaultAsync(e => e.IBGE == ibge);
        return _mapper.Map<CidadesVO>(cidade) ?? null;
    }

    public async Task<IList<CidadesPortePopulacaoVO>> PortePopulacao()
    {
        var resultado = new List<CidadesPortePopulacaoVO>();
        var portePopulacao = _context.Cidades
            .GroupBy(g => new { g.Regiao, g.UF, g.Porte })
            .Select(r => new
            {
                Regiao = r.Key.Regiao,
                UF = r.Key.UF,
                Porte = r.Key.Porte
            });

        foreach (var data in portePopulacao)
        {
            var totalCidades = _context.Cidades.Where(c => c.UF.Equals(data.UF)).ToList().Count();
            var totalPopulacao = _context.Cidades
                .Where(c => c.UF.Equals(data.UF) && c.Porte.Equals(data.Porte)).Sum(c => c.Populacao);
            resultado.Add(new CidadesPortePopulacaoVO()
            {
                Porte = data.Porte,
                Regiao = data.Regiao,
                UF = data.UF,
                MediaPopulacao = totalPopulacao / totalCidades

            });
        }

        return resultado;
    }

    public async Task<IEnumerable<CidadesVO>> Listar3MaioresCidade(string pesquisa)
    {
        if (pesquisa.Trim().Length == 0)
            return new List<CidadesVO>();

        if (pesquisa.Trim().Length == 2)
        {
            var pesquisaUF =  _context.Cidades.Where(w => w.UF.Equals(pesquisa)).OrderByDescending(o => o.Populacao)
                .ToList().Take(3);
            return _mapper.Map<IEnumerable<CidadesVO>>(pesquisaUF);
        }

        var pesquisaRegiao =  _context.Cidades.Where(w => w.Regiao.Equals(pesquisa)).OrderByDescending(o => o.Populacao)
            .ToList().Take(3);
        return _mapper.Map<IEnumerable<CidadesVO>>(pesquisaRegiao); ;

    }
}