using Challenge.Domain.Validators;

namespace Challenge.Domain;

public sealed class Despesas : BaseEntity
{
    protected Despesas() // EF Constructor
    {
    }
    public Despesas(string descricao, double valor, DateTime data)
    {
        Descricao = descricao;
        Valor = valor;
        Data = data;
        _errors = new List<string>();
        Validate();
    }

    public bool Validate() => base.Validate(new DespesasValidator(), this);
}