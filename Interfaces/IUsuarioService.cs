using System.Collections.Generic;
using TelaLogin.DTO;
using TelaLogin.Model;

namespace TelaLogin.Interfaces
{
    public interface IUsuarioService
    {
        List<UsuarioResponse> listarUsuarios();
        UsuarioResponse BuscarUsuario(int id);
        int SalvarUsuario(UsuarioRequest usuario);
        int ExcluirUsuario(int id);
        int AlterarUsuario(Usuario usuario);

        bool VerificaDuplicidadeEmail(string email);
        Usuario VerificaLogin(LoginUsuario login);
        string GerarToken(Usuario usuario);
        string GerarRefreshToken(Usuario usuario);
    }
}
