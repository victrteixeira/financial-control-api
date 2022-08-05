namespace Challenge.Services.DTO;

public class ReceitasDTO
{
    public long Id { get; set; }
    public string Descricao { get; set; }
    public double Valor { get; set; }
    public DateTime Data { get; set; }

    public ReceitasDTO(string descricao, double valor, DateTime data)
    {
        Descricao = descricao;
        Valor = valor;
        Data = data;
    }

    public ReceitasDTO()
    {
    }
}