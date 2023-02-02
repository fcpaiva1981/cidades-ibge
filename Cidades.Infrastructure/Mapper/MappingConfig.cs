using AutoMapper;
using Cidades.Domain.ValueObject;

namespace Cidades.Infrastructure.Mapper;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<CidadesVO, Domain.Model.Cidades>();
            config.CreateMap<Domain.Model.Cidades, CidadesVO>();
        });
        return mappingConfig;
    }
}
