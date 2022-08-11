using FluentValidation;

namespace Challenge.Domain.Validators;

public class DespesasValidator : AbstractValidator<Despesas>
{
    public DespesasValidator()
    {
        Include(new BaseValidator());
        RuleFor(x => x.Categorias)
            .IsInEnum()
            .NotNull()
            .NotEmpty();
    }
}