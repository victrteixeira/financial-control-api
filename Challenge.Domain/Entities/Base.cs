﻿using Challenge.Domain.Validators;
using FluentValidation;
using FluentValidation.Results;

namespace Challenge.Domain;

public abstract class Base
{
    public long Id { get; set; }
    public string Descricao { get; set; }
    public double Valor { get; set; }
    public DateTime Data { get; set; }

    internal List<string> _errors;
    public IReadOnlyCollection<string> Errors => _errors;
    
    public bool IsValid() => _errors.Count == 0;

    private void AddErrorList(IList<ValidationFailure> errors)
    {
        foreach (var error in errors)
        {
            _errors.Add(error.ErrorMessage);
        }
    }

    protected bool Validate<T, J>(T validator, J obj) where T : AbstractValidator<J>
    {
        var validation = validator.Validate(obj);
        if(validation.Errors.Count > 0)
            AddErrorList(validation.Errors);

        return IsValid();
    }
    
}