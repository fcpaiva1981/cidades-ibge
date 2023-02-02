namespace Cidades.Domain.ValueObject;

public class CidadesVO
{
    public int id { get; set; }
    public long IBGE { get; set; }
    public long IBGE7 { get; set; }
    public string UF { get; set; }
    public string Municipio { get; set; }
    public string Regiao { get; set; }
    public string Porte { get; set; }
    public long Populacao { get; set; }
}