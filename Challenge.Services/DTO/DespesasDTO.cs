namespace Challenge.Services.DTO;

public class DespesasDTO
{
    public long Id { get; set; }
    public string Descricao { get; set; }
    public double Valor { get; set; }
    public DateTime Data { get; set; }

    public DespesasDTO(string descricao, double valor, DateTime data)
    {
        Descricao = descricao;
        Valor = valor;
        Data = data;
    }

    public DespesasDTO()
    {
    }
}