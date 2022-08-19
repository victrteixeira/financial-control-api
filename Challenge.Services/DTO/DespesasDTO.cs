using Challenge.Domain.Enums;

namespace Challenge.Services.DTO;

public class DespesasDto
{
    public long Id { get; set; }
    public string Descricao { get; set; }
    public double Valor { get; set; }
    public DateTime Data { get; set; }
    public Categoria Categorias { get; set; } = Categoria.Outros;

    public DespesasDto(long id, string descricao, double valor, DateTime data, Categoria categorias)
    {
        Id = id;
        Descricao = descricao;
        Valor = valor;
        Data = data;
        Categorias = categorias;
    }

    public DespesasDto()
    {
    }
}