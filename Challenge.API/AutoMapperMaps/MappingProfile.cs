using AutoMapper;
using Challenge.API.Utilities;
using Challenge.API.ViewModels;
using Challenge.Domain;
using Challenge.Domain.Enums;
using Challenge.Services.DTO;

namespace Challenge.API.AutoMapperMaps;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DespesasDTO, Despesas>()
            .ConstructUsing(src => new Despesas(src.Descricao, src.Valor, src.Data, src.Categorias))
            .ReverseMap();

        CreateMap<Despesas, ResponseDespesa>()
            .ForMember(dest => dest.Categorias,
                opt => opt
                    .MapFrom(src => Enum.GetName(typeof(Categoria), src.Categorias)));

        CreateMap<CreateDespesaViewModel, DespesasDTO>()
            .ForMember(dest => dest.Categorias,
                opt => opt
                    .MapFrom<CategoriesEnumResolver>());

        CreateMap<UpdateDespesaViewModel, DespesasDTO>()
            .ForMember(dest => dest.Categorias,
                opt => opt
                    .MapFrom<CategoriesEnumResolver>());
        
        CreateMap<Receitas, ReceitasDTO>().ReverseMap();
        CreateMap<CreateReceitaViewModel, ReceitasDTO>().ReverseMap();
        CreateMap<UpdateReceitaViewModel, ReceitasDTO>().ReverseMap();
    }
}