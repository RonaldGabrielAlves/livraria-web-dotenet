using FluentValidation;
using livrariawdaweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace livrariawdaweb.validators
{
    public class AddClientesValidator : AbstractValidator<Clientes> 
    {
        public AddClientesValidator()
        {
            RuleFor(m => m.nomecli)
                .NotEmpty()
                    .WithMessage("O nome do cliente não deve ser nulo!")
                .MaximumLength(50)
                    .WithMessage("O número de caracteres não deve ser maior que 50!")
                .MinimumLength(3)
                    .WithMessage("O número de caracteres não deve ser menor que 3!");
            RuleFor(m => m.enderecocli)
                .NotEmpty()
                    .WithMessage("O endereço do cliente não deve ser nulo!")
                .MaximumLength(50)
                    .WithMessage("O número de caracteres não deve ser maior que 50!")
                .MinimumLength(5)
                    .WithMessage("O número de caracteres não deve ser menor que 5!");
            RuleFor(m => m.cidadecli)
                .NotEmpty()
                    .WithMessage("A cidade do cliente não deve ser nulo!")
                .MaximumLength(50)
                    .WithMessage("O número de caracteres não deve ser maior que 50!")
                .MinimumLength(5)
                    .WithMessage("O número de caracteres não deve ser menor que 5!");
            RuleFor(m => m.emailcli)
                .NotEmpty()
                    .WithMessage("O email do cliente não deve ser nulo!")
                .MaximumLength(50)
                    .WithMessage("O número de caracteres não deve ser maior que 50!")
                .EmailAddress()
                    .WithMessage("Digite um endereço de email válido!");
        }
    }
}
