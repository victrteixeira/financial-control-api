using Challenge.Domain.Enums;
using Challenge.Domain.Validators;

namespace Challenge.Domain;

public sealed class Receitas : BaseEntity
{
    public Categoria Categorias { get; private set; }
    
    protected Receitas() // EF Constructor
    {
    }

    public Receitas(string descricao, double valor, DateTime data, Categoria categoria)
    {
        Descricao = descricao;
        Valor = valor;
        Data = data;
        Categorias = categoria;
        _errors = new List<string>();
        Validate();
    }

    public bool Validate() => base.Validate(new ReceitasValidator(), this);
}