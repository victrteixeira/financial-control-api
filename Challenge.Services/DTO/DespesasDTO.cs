namespace Challenge.Services.DTO;

public class DespesasDTO
{
    public long Id { get; set; }
    public string Descricao { get; set; }
    public double Valor { get; set; }
    public DateTime Data { get; set; }

    public DespesasDTO(long id, string descricao, double valor, DateTime data)
    {
        Id = id;
        Descricao = descricao;
        Valor = valor;
        Data = data;
    }

    public DespesasDTO()
    {
    }
}