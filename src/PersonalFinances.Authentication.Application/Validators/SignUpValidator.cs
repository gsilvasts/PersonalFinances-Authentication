using FluentValidation;
using PersonalFinances.Authentication.Application.Models.InputModels;
using PersonalFinances.Authentication.CrossCultting.Extensions;

namespace PersonalFinances.Authentication.Application.Validators
{
    public class SignUpValidator: AbstractValidator<SignUpInputModel>
    {
        public SignUpValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("O nome do usuário é obrigatório")
                .NotNull().WithMessage("O nome do usuário é obrigatório");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("O nome do usuário é obrigatório")
                .NotNull().WithMessage("O nome do usuário é obrigatório");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório")
                .NotNull().WithMessage("O e-mail é obrigatório")
                .EmailAddress().WithMessage("O e-mail informado não é válido");

            RuleFor(x => x.Password)
                .Must(p => p.IsValidPassword()).WithMessage("Digite uma senha válida.")
                .NotEmpty().WithMessage("A senha é obrigatória")
                .NotNull().WithMessage("A senha é obrigatória");
        }
    }
}
