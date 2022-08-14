using Challenge.Domain.Enums;

namespace Challenge.Services.DTO;

public class CategoriaDTO
{
    public CategoriaDTO(decimal? value, Categoria categoria)
    {
        Value = value;
        Categoria = categoria;
    }

    public CategoriaDTO()
    {
    }

    public decimal? Value { get; private set; }
    public Categoria Categoria { get; private set; }
}