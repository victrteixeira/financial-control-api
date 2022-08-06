using Challenge.Core;
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
    
    public override bool Validate()
    {
        var validator = new BaseEntityValidator();
        var validation = validator.Validate(this);

        if (!validation.IsValid)
        {
            foreach (var error in validation.Errors)
                _errors.Add(error.ErrorMessage);

            throw new DomainException("Alguns campos não válidso", _errors);
        }

        return true;
    }
}