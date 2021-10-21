using FluentValidation;
using livrariawdaweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace livrariawdaweb.validators
{
    public class AddAluguelValidator : AbstractValidator<Aluguel>
    {
        public AddAluguelValidator()
        {
            RuleFor(m => m.livroalu)
                .NotEmpty()
                    .WithMessage("O nome do livro não deve ser nulo!");
            RuleFor(m => m.clientealu)
                .NotEmpty()
                    .WithMessage("O nome do cliente não deve ser nulo!");
            RuleFor(m => m.dataalu)
                .NotEmpty()
                    .WithMessage("A data do aluguel não deve ser nulo!");
            RuleFor(m => m.dataprevdev)
                .NotEmpty()
                    .WithMessage("A data de previsão de devolução não deve ser nulo!");
        }
    }
}
