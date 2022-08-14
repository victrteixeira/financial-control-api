namespace Challenge.Services.DTO;

public class ResponseDespesa
{
    public long Id { get; set; }
    public string Descricao { get; set; }
    public double Valor { get; set; }
    public DateTime Data { get; set; }
    public string Categorias { get; set; }

    public ResponseDespesa(long id, string descricao, double valor, DateTime data, string categorias)
    {
        Id = id;
        Descricao = descricao;
        Valor = valor;
        Data = data;
        Categorias = categorias;
    }

    public ResponseDespesa()
    {
    }
}