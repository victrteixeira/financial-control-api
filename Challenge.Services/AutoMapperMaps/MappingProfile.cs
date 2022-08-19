using AutoMapper;
using Challenge.Services.Utilities;
using Challenge.Services.ViewModels;
using Challenge.Domain;
using Challenge.Domain.Enums;
using Challenge.Services.DTO;

namespace Challenge.Services.AutoMapperMaps;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DespesasDto, Despesas>()
            .ConstructUsing(src => new Despesas(src.Descricao, src.Valor, src.Data, src.Categorias))
            .ReverseMap();

        CreateMap<Despesas, ResponseDespesa>()
            .ForMember(dest => dest.Categorias,
                opt => opt
                    .MapFrom(src => Enum.GetName(typeof(Categoria), src.Categorias)));

        CreateMap<CreateDespesaViewModel, DespesasDto>()
            .ForMember(dest => dest.Categorias,
                opt => opt
                    .MapFrom<CategoriesEnumResolver>());

        CreateMap<UpdateDespesaViewModel, DespesasDto>()
            .ForMember(dest => dest.Categorias,
                opt => opt
                    .MapFrom<CategoriesEnumResolver>());
        
        CreateMap<Receitas, ReceitasDto>().ReverseMap();
        CreateMap<CreateReceitaViewModel, ReceitasDto>().ReverseMap();
        CreateMap<UpdateReceitaViewModel, ReceitasDto>().ReverseMap();
    }
}