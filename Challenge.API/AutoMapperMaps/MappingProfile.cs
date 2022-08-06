using AutoMapper;
using Challenge.API.ViewModels;
using Challenge.Domain;
using Challenge.Services.DTO;

namespace Challenge.API.AutoMapperMaps;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Despesas, DespesasDTO>().ReverseMap();
        CreateMap<Receitas, ReceitasDTO>().ReverseMap();
        CreateMap<CreateDespesaViewModel, DespesasDTO>().ReverseMap();
        CreateMap<UpdateDespesaViewModel, DespesasDTO>().ReverseMap();
        CreateMap<CreateReceitaViewModel, ReceitasDTO>().ReverseMap();
        CreateMap<UpdateReceitaViewModel, ReceitasDTO>().ReverseMap();
    }
}