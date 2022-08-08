using Challenge.Core;
using Challenge.Domain.Validators;
using FluentValidation;

namespace Challenge.Domain;

public class Despesas : Base
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
        Validate();
    }

    public bool Validate() => base.Validate(new DespesasValidator(), this);
}