using System;
using System.Collections.Generic;
using TelaLogin.Model;

namespace TelaLogin.Interfaces
{
    public interface IUsuarioRepositorio
    {
        List<Usuario> listarUsuarios();
        Usuario BuscarUsuario(int id);
        int SalvarUsuario(Usuario usuario);
        int ExcluirUsuario(int id);
        int AlterarUsuario(Usuario usuario);

        Usuario BuscarPorEmail(string email);

        bool VerificaDuplicidadeEmail(string email);
        Usuario VerificaLogin(LoginUsuario login);

    }
}
