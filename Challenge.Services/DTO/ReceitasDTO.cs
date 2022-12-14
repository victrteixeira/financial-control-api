namespace Challenge.Services.DTO;

public class ReceitasDto
{
    public long Id { get; set; }
    public string Descricao { get; set; }
    public double Valor { get; set; }
    public DateTime Data { get; set; }

    public ReceitasDto(long id, string descricao, double valor, DateTime data)
    {
        Id = id;
        Descricao = descricao;
        Valor = valor;
        Data = data;
    }

    public ReceitasDto()
    {
    }
}