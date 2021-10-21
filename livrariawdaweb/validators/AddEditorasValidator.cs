using FluentValidation;
using livrariawdaweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace livrariawdaweb.validators
{
    public class AddEditorasValidator : AbstractValidator<Editoras>
    {
        public AddEditorasValidator()
        {
            RuleFor(m => m.nomedi)
                .NotEmpty()
                    .WithMessage("O nome da editora não deve ser nulo!")
                .MaximumLength(50)
                    .WithMessage("O número de caracteres não deve ser maior que 50!")
                .MinimumLength(3)
                    .WithMessage("O número de caracteres não deve ser menor que 3!");
            RuleFor(m => m.cidadedi)
                .NotEmpty()
                    .WithMessage("A cidade da editora não deve ser nulo!")
                .MaximumLength(50)
                    .WithMessage("O número de caracteres não deve ser maior que 50!")
                .MinimumLength(3)
                    .WithMessage("O número de caracteres não deve ser menor que 5!");
        }
    }
}
