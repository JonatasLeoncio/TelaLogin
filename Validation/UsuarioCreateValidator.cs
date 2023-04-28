using FluentValidation;
using System.Security.Cryptography;
using TelaLogin.Model;

namespace TelaLogin.Validation
{
    public class UsuarioCreateValidator:AbstractValidator<Usuario>
    {
        public UsuarioCreateValidator()
        {
            RuleFor(user => user.Nome.Trim())
               .NotEmpty()
               .WithMessage("O Campo Nome é obrigatorio")
               /* .Length(3, 10)
                .WithMessage("O nome precisa te de 3 a 100 caracteres")*/
               .MinimumLength(3).MaximumLength(20);
            RuleFor(user => user.Email)
               .NotEmpty().NotNull()
               .WithMessage("O Campo email é obrigatorio")
               .EmailAddress()
               .WithMessage("O gg email não  valido");
            RuleFor(user => user.Senha.Trim())
               .NotEmpty().NotNull().MinimumLength(3).MaximumLength(100)
               .WithMessage("Senha precisa de ter de 3 a 100 caracteres");

        }
    }
}
