using Challenge.Domain.Enums;

namespace Challenge.Services.DTO;

public class CategoriaDto
{
    public CategoriaDto(decimal? value, Categoria categoria)
    {
        Value = value;
        Categoria = categoria;
    }

    public CategoriaDto()
    {
    }

    public decimal? Value { get; private set; }
    public Categoria Categoria { get; private set; }
}