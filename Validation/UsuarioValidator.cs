using FluentValidation;
using TelaLogin.Model;


namespace TelaLogin.Validation
{    

    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
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
    public class LoginUsuarioValidator : AbstractValidator<LoginUsuario>
    {
        public LoginUsuarioValidator()
        {
            RuleFor(user => user.Email)
               .NotEmpty()              
               .EmailAddress();              
            RuleFor(user => user.Senha)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(100);     
        }

    }

}
