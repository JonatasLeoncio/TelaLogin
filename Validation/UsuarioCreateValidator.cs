using FluentValidation;
using System;
using System.Security.Cryptography;
using TelaLogin.Model;
using TelaLogin.Repository;

namespace TelaLogin.Validation
{
    public class UsuarioCreateValidator:AbstractValidator<Usuario>
    {
        public UsuarioCreateValidator()
        {
          Usuario compara = new Usuario();
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

            /*RuleFor(user => user.Email).Must(VerificaDuplicidade)
                .WithMessage("Este email já esta cadastrado!");*/
        }
        /*private bool VerificaDuplicidade(string email)
        {
            if (UsuarioRepository.VerificaDuplicidadeEmail(email))
            {
                return !true;
            }
            return !false;
        }*/
    }
}
