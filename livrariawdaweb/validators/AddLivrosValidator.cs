using FluentValidation;
using livrariawdaweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace livrariawdaweb.validators
{
    public class AddLivrosValidator : AbstractValidator<Livros>
    {
        public AddLivrosValidator()
        {
            RuleFor(m => m.nomeliv)
                .NotEmpty()
                    .WithMessage("O nome do Livro não deve ser nulo!")
                .MaximumLength(50)
                    .WithMessage("O número de caracteres não deve ser maior que 50!")
                .MinimumLength(2)
                    .WithMessage("O número de caracteres não deve ser menor que 2!");
            RuleFor(m => m.editliv)
                .NotEmpty()
                    .WithMessage("A editora do Livro não deve ser nulo!");
            RuleFor(m => m.autorliv)
                .NotEmpty()
                    .WithMessage("O autor do Livro não deve ser nulo!")
                .MaximumLength(50)
                    .WithMessage("O número de caracteres não deve ser maior que 50!")
                .MinimumLength(3)
                    .WithMessage("O número de caracteres não deve ser menor que 3!");
            RuleFor(m => m.lcmliv)
                .NotEmpty()
                    .WithMessage("A data de lançamento do Livro não deve ser nulo!");
            RuleFor(m => m.qtdliv)
                .NotEmpty()
                    .WithMessage("A quantidade de livros não deve ser nula!")
                .GreaterThan(0)
                    .WithMessage("A quantidade de livros deve ser maior ou igual a 1!");
        }
    }
}
