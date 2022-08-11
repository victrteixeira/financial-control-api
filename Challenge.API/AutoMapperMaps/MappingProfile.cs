using AutoMapper;
using Challenge.API.ViewModels;
using Challenge.Domain;
using Challenge.Services.DTO;

namespace Challenge.API.AutoMapperMaps;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DespesasDTO, Despesas>()
            .ConstructUsing(src => new Despesas(src.Descricao, src.Valor, src.Data, src.Categorias))
            .ReverseMap();

        CreateMap<CreateDespesaViewModel, DespesasDTO>()
            .ForMember(dest => dest.Categorias,
                opt => opt.MapFrom(src => src.Categorias))
            .ReverseMap().ForMember(dest => dest.Categorias,
                opt => opt.MapFrom(src => src.Categorias.ToString()));
        CreateMap<UpdateDespesaViewModel, DespesasDTO>()
            .ForMember(dest => dest.Categorias,
                opt => opt.MapFrom(src => src.Categorias))
            .ReverseMap().ForMember(dest => dest.Categorias,
                opt => opt.MapFrom(src => src.Categorias.ToString()));
        
        CreateMap<Receitas, ReceitasDTO>().ReverseMap();
        CreateMap<CreateReceitaViewModel, ReceitasDTO>().ReverseMap();
        CreateMap<UpdateReceitaViewModel, ReceitasDTO>().ReverseMap();
    }
}