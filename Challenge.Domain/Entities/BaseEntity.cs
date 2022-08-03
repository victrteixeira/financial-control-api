namespace Challenge.Domain;

public abstract class BaseEntity
{
    public long Id { get; set; }
    public string Descricao { get; set; }
    public double Valor { get; set; }
    public DateTime Data { get; set; }

    internal List<string> _errors;
    public IReadOnlyCollection<string> Errors => _errors;
    public abstract bool Validate();
}