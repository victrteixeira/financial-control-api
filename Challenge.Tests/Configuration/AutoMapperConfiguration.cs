using Challenge.Domain.Enums;

namespace Challenge.Tests.Configuration;

public class AutoMapperConfiguration
{
    public static IMapper GetConfiguration()
    {
        var autoMapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Despesas, DespesasDto>().ReverseMap();
            cfg.CreateMap<Receitas, ReceitasDto>().ReverseMap();
            cfg.CreateMap<Despesas, ResponseDespesa>()
                .ForMember(dest => dest.Categorias,
                    opt => opt
                        .MapFrom(src => Enum.GetName(typeof(Categoria), src.Categorias)));
        });
        return autoMapperConfig.CreateMapper();
    }
}