using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using TelaLogin.Model;

namespace TelaLogin.Repository
{
    public class UsuarioRepository
    {
      static  private string  stringDeConexao="Data Source=C:\\Users\\Micro\\Desktop\\Login\\TelaLogin\\Banco\\bancoLogin.db";
       static public async Task<List<Usuario>> listarUsuarios()
        {
            using (var conexao = new SQLiteConnection(stringDeConexao))
            {
                string sql = "SELECT * FROM usuarios";
                var usuario = await conexao.QueryAsync<Usuario>(sql);
                
                return usuario.ToList();
            }
           
        }
        static public int salvarUsuario(Usuario usuario)
        {
            using (var conexao = new SQLiteConnection(stringDeConexao)) {
                string sql = "insert into usuarios (nome, email, senha)values(@nome, @email, @senha)";
                int linhasAfetadas = conexao.Execute(sql,usuario);
                return linhasAfetadas;
            }
           
        }

        public static object ExcluirUsuario(int id)
        {
            using (var conexao = new SQLiteConnection(stringDeConexao))
            {
                string sql = "DELETE FROM usuarios WHERE id=@id";
                int linhasAfetadas = conexao.Execute(sql,new { id = id });
                Console.WriteLine(linhasAfetadas);
                return linhasAfetadas;
            }
        }

        public static int AlterarUsuario(Usuario usuario)
        {
           using(var conexao = new  SQLiteConnection(stringDeConexao))
            {
                string sql = "UPDATE usuarios SET nome=@nome,email=@email,senha=@senha WHERE id=@id";
                int linhasAfetadas = conexao.Execute(sql, usuario );
                return linhasAfetadas;

            }
       
        }
    }
}
