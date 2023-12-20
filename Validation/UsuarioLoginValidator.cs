using FluentValidation;
using TelaLogin.Model;

namespace TelaLogin.Validation
{
    public class UsuarioLoginValidator: AbstractValidator<LoginUsuario>
    {
        public UsuarioLoginValidator()
        {
            RuleFor(user => user.Email)
              .NotEmpty()
              .EmailAddress();
            RuleFor(user => user.Senha)
                .NotEmpty()               
                .MinimumLength(3)
                .MaximumLength(100);
        }
    }
}
