using Challenge.Core;
using Challenge.Domain.Validators;

namespace Challenge.Domain;

public class Receitas : Base
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
    
    public void SetDescricao(string descricao)
    {
        Descricao = descricao;
        Validate();
    }

    public void SetValor(double valor)
    {
        Valor = valor;
        Validate();
    }

    public void SetData(DateTime data)
    {
        Data = data;
    }
    public bool Validate() => base.Validate(new ReceitasValidator(), this);
}