using FluentValidation;

namespace Challenge.Domain.Validators;

public class ReceitasValidator : AbstractValidator<Receitas>
{
    public ReceitasValidator()
    {
        Include(new BaseValidator());
    }
}