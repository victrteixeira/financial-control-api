using AutoMapper;
using Challenge.Domain;
using Challenge.Services.DTO;

namespace Challenge.Tests.Configuration;

public class AutoMapperConfiguration
{
    public static IMapper GetConfiguration()
    {
        var autoMapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Despesas, DespesasDTO>().ReverseMap();
            cfg.CreateMap<Receitas, ReceitasDTO>().ReverseMap();
        });
        return autoMapperConfig.CreateMapper();
    }
}