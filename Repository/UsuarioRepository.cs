using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using TelaLogin.Interfaces;
using TelaLogin.Model;
//using static BCrypt.Net.BCrypt;


namespace TelaLogin.Repository
{
    public class UsuarioRepository : IUsuarioRepositorio
    {              
       
        private readonly IConfiguration _configuration;
        public UsuarioRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = configuration.GetConnectionString("MYSQLite");
        }         
        private  readonly string connectionString;//pelo appsetings
        private readonly string stringDeConexao = Environment.GetEnvironmentVariable("My_Conexao_SQLite");//pelo dotenv
        

        public List<Usuario> listarUsuarios()
        {
            /* var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                 .AddEnvironmentVariables().Build();

             string connectionString = builder.GetConnectionString("TESTE");*/

          // var connectionString = _configuration.GetConnectionString("MYSQLite");//ou dessa forma
           
          
            using (var conexao = new SQLiteConnection(connectionString))
            {
                string sql = "SELECT * FROM usuarios";
                var usuario = conexao.Query<Usuario>(sql);

                return usuario.ToList();
            }

        }
        public Usuario BuscarUsuario(int id)
        {
            try
            {
                string sql = "select * from usuarios where id = @id";
                using (var conexao = new SQLiteConnection(stringDeConexao))
                {
                    var usuario = conexao.QueryFirstOrDefault<Usuario>(sql, new { id });
                    return usuario;
                }

            }
            catch (Exception ex)
            {

                throw new(ex.Message + " Erro na Conexão");
            }

        }
        public int SalvarUsuario(Usuario usuario)
        {
            try
            {               
                using (var conexao = new SQLiteConnection(stringDeConexao))
                {
                    string sql = "insert into usuarios (nome, email, senha)values(@nome, @email, @senha)";
                    int linhasAfetadas = conexao.Execute(sql, usuario);
                    return linhasAfetadas;
                }

            }
            catch (Exception ex)
            {

                throw new(ex.Message + "Erro na Conexão");
            }

        }
        public int ExcluirUsuario(int id)
        {
            using (var conexao = new SQLiteConnection(stringDeConexao))
            {
                string sql = "DELETE FROM usuarios WHERE id=@id";
                int linhasAfetadas = conexao.Execute(sql, new { id = id });
                Console.WriteLine(linhasAfetadas);
                return linhasAfetadas;
            }
        }
        public int AlterarUsuario(Usuario usuario)
        {
            using (var conexao = new SQLiteConnection(stringDeConexao))
            {
                string sql = "UPDATE usuarios SET nome=@nome,email=@email,senha=@senha WHERE id=@id";
                int linhasAfetadas = conexao.Execute(sql, usuario);
                return linhasAfetadas;
            }

        }
        public bool VerificaDuplicidadeEmail(string email)
        {
            string sql = "SELECT * FROM usuarios WHERE email = @email";
            using (var conexao = new SQLiteConnection(stringDeConexao))
            {
                var resp = conexao.Query(sql, new { email }).FirstOrDefault();
                if (resp != null)
                {
                    return true;
                }
                return false;
            }
        }
        public Usuario VerificaLogin(LoginUsuario login)
        {
            string sql = "select * from usuarios where email= @email";
            using (var conexao = new SQLiteConnection(stringDeConexao))
            {
                var user = conexao.Query<Usuario>(sql, new { login.Email }).FirstOrDefault();

                if (user != null && BCrypt.Net.BCrypt.Verify(login.Senha, user.Senha))
                {
                    return user;
                }
                return null;

            }
        }
        public Usuario BuscarPorEmail(string email)
        {

            string sql = "select * from usuarios where email = @email";
            using (var conexao = new SQLiteConnection(stringDeConexao))
            {
                var usuario = conexao.QueryFirstOrDefault<Usuario>(sql, new { email });
                return usuario;
            }
        }
    }
}
