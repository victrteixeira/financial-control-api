using Challenge.Core;
using Challenge.Domain.Validators;

namespace Challenge.Domain;

public class Despesas : BaseEntity
{
    public Despesas() // EF Constructor
    {
    }

    public Despesas(string descricao, double valor, DateTime data)
    {
        Descricao = descricao;
        Valor = valor;
        Data = data;
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

    public override bool Validate()
    {
        var validator = new BaseEntityValidator();
        var validation = validator.Validate(this);

        if (!validation.IsValid)
        {
            foreach (var error in validation.Errors)
                _errors.Add(error.ErrorMessage);

            throw new DomainException("Alguns campos não são válidos", _errors);
        }

        return true;
    }
}