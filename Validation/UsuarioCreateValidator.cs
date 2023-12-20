using FluentValidation;
using System;
using System.Security.Cryptography;
using TelaLogin.DTO;
using TelaLogin.Model;
using TelaLogin.Repository;

namespace TelaLogin.Validation
{
    public class UsuarioCreateValidator:AbstractValidator<UsuarioRequest>
    {
        public UsuarioCreateValidator()
        {
            RuleFor(user => user.Nome)
               .NotEmpty()
               .WithMessage("O Campo {PropertyName} é obrigatorio")
                .Length(3, 10);
             // .WithMessage("O nome precisa te de 3 a 100 caracteres")              
            RuleFor(user => user.Email)
               .NotEmpty()
               //.WithMessage("O Campo email é obrigatorio")
               .EmailAddress();
              // .WithMessage("O email não  valido");
            RuleFor(user => user.Senha)
               .NotEmpty()
               .Length(3,10)
               .WithMessage("{PropertyName} precisa de ter de 3 a 100 caracteres");

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
