using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using TelaLogin.Model;
//using static BCrypt.Net.BCrypt;


namespace TelaLogin.Repository
{
    public class UsuarioRepository
    {
        static private string stringDeConexao = "Data Source=C:\\Users\\Micro\\Documents\\GitHub\\TelaLogin\\Banco\\bancoLogin.db";
        static public async Task<object> listarUsuarios()
        {

            using (var conexao = new SQLiteConnection(stringDeConexao))
            {
                string sql = "SELECT * FROM usuarios";
                var usuario = await conexao.QueryAsync<Usuario>(sql);

                return usuario.ToList();
            }

        }
        public static object BuscarUsuario(int id)
        {
            string sql = "select * from usuarios where id = @id";
            using (var conexao = new SQLiteConnection(stringDeConexao))
            {
                var usuario = conexao.QueryFirstOrDefault<Usuario>(sql, new { id });
                //objeto anonimo substituindo DTO
                var user = new
                {                                
                    id = usuario.Id,
                    nome = usuario.Nome,
                    email = usuario.Email,                  
                };

                if (usuario != null)
                {
                    return user;
                }
                return new { messager = "Usuario não encontrado" };
            }

        }
        static public int salvarUsuario(Usuario usuario)
        {

            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);


            using (var conexao = new SQLiteConnection(stringDeConexao))
            {
                string sql = "insert into usuarios (nome, email, senha)values(@nome, @email, @senha)";
                int linhasAfetadas = conexao.Execute(sql, usuario);
                return linhasAfetadas;
            }

        }

        public static object ExcluirUsuario(int id)
        {
            using (var conexao = new SQLiteConnection(stringDeConexao))
            {
                string sql = "DELETE FROM usuarios WHERE id=@id";
                int linhasAfetadas = conexao.Execute(sql, new { id = id });
                Console.WriteLine(linhasAfetadas);
                return linhasAfetadas;
            }
        }

        public static int AlterarUsuario(Usuario usuario)
        {
            using (var conexao = new SQLiteConnection(stringDeConexao))
            {
                string sql = "UPDATE usuarios SET nome=@nome,email=@email,senha=@senha WHERE id=@id";
                int linhasAfetadas = conexao.Execute(sql, usuario);
                return linhasAfetadas;

            }

        }

        static public object VerificaLogin(LoginUsuario login)
        {

            string sql = "select * from usuarios where email= @email";
            using (var conexao = new SQLiteConnection(stringDeConexao))
            {
                var user = conexao.Query<Usuario>(sql, new { login.Email }).FirstOrDefault();


                if (user != null && BCrypt.Net.BCrypt.Verify(login.Senha, user.Senha))
                {
                    return new { user, Token = "TesteToken", RefreshToken = "TesteRefreshToken" };
                }
                return new { messager = "email ou senhas invalidos" };

            }
        }

    }
}
