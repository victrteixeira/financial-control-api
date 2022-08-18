using AutoMapper;
using Challenge.Services.ViewModels;
using Challenge.Domain.Enums;
using Challenge.Services.DTO;

namespace Challenge.Services.Utilities;

public class CategoriesEnumResolver : IValueResolver<CreateDespesaViewModel, DespesasDTO, Categoria>, IValueResolver<UpdateDespesaViewModel, DespesasDTO, Categoria>
{
    public Categoria Resolve(CreateDespesaViewModel source, DespesasDTO destination, Categoria destMember,
        ResolutionContext context)
    {
        if (Enum.TryParse<Categoria>(source.Categorias, out var categoria))
            return categoria;

        return Categoria.Outros;
    }

    public Categoria Resolve(UpdateDespesaViewModel source, DespesasDTO destination, Categoria destMember,
        ResolutionContext context)
    {
        if (Enum.TryParse<Categoria>(source.Categorias, out var categoria))
            return categoria;

        return Categoria.Outros;
    }
}