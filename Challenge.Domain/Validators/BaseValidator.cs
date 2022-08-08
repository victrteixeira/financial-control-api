using FluentValidation;

namespace Challenge.Domain.Validators;

public class BaseValidator : AbstractValidator<Base>
{
    public BaseValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("A entidade não pode estar vazia.")
            .NotNull()
            .WithMessage("A entidade não pode ser nula.");

        RuleFor(x => x.Descricao)
            .NotEmpty()
            .WithMessage("A {PropertyName} da despesa não pode estar vazia.")
            .NotNull()
            .WithMessage("A {PropertyName} da despesa não pode ser nula.")
            .MinimumLength(3)
            .WithMessage("A {PropertyName} da despesa precisa ter no mínimo 5 caracteres.")
            .MaximumLength(100)
            .WithMessage("A {PropertyName} da despesa precisa ter no máximo 100 caracteres.");

        RuleFor(x => x.Valor)
            .NotEmpty()
            .WithMessage("O {PropertyName} da despesa não pode estar vazio.")
            .NotNull()
            .WithMessage("O {PropertyName} da despesa não pode ser nulo.")
            .GreaterThan(0.9999999999999999)
            .WithMessage("O {PropertyName} da despesa é muito pequeno, por isso irrelevante.");

        RuleFor(x => x.Data)
            .NotEmpty()
            .WithMessage("O {PropertyName} da despesa não pode estar vazio.")
            .NotNull()
            .WithMessage("O {PropertyName} da despesa não pode ser nulo.")
            .ExclusiveBetween(new DateTime(2021, 12, 31), new DateTime(2023, 01, 01))
            .WithMessage("A {PropertyName} da despesa só pode ser do ano de 2022");
    }
}