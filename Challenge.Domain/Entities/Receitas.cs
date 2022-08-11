using Challenge.Domain.Enums;
using Challenge.Domain.Validators;

namespace Challenge.Domain;

public class Receitas : BaseEntity
{
    protected Receitas() // EF Constructor
    {
    }

    public Receitas(string descricao, double valor, DateTime data)
    {
        Descricao = descricao;
        Valor = valor;
        Data = data;
        _errors = new List<string>();
        Validate();
    }

    public bool Validate() => base.Validate(new ReceitasValidator(), this);
}