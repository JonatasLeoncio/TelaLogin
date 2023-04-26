//using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
//using System.Data.SQLite;
using System.Linq;
using TelaLogin.Repository;
using TelaLogin.Model;
using System.Threading.Tasks;
using System;

namespace TelaLogin.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet("Ola")]
        public ActionResult Oi()
        {
            return Ok("oi");
        }

        [HttpGet("Listar")]
        public async Task<ActionResult> ListarUsuario()
        {
            var resp = await UsuarioRepository.listarUsuarios();
            return Ok(resp);
        }

        [HttpGet("Buscar/{id}")]
        public ActionResult BuscarUsuario(int id)
        {
            var resp = UsuarioRepository.BuscarUsuario(id);
            return Ok(resp);
        }

        [HttpPost("cadastrar")]
        public ActionResult Salvar([FromBody] Usuario usuario)
        {
            try
            {
                var resp = UsuarioRepository.salvarUsuario(usuario);
                if (resp > 0)
                {
                    return Ok("salvo com sucesso");
                }
                return Ok("não foi salvo");

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPut("alterar")]
        public ActionResult Alterar([FromBody] Usuario usuario)
        {
            var resp = UsuarioRepository.AlterarUsuario(usuario);
            if (resp > 0)
            {
                return Ok("alterado com sucesso");
            }
            return Ok("não foi alterado");

        }

        [HttpDelete("excluir/{id}")]
        public ActionResult Excluir(int id)
        {
            int resp = (int)UsuarioRepository.ExcluirUsuario(id);
            if (resp > 0)
            {
                return Ok("Excluido com sucesso");
            }
            return Ok("não foi Excluido");
        }

      
        [HttpPost("logar")]
        public ActionResult Logar([FromBody]LoginUsuario login)
        {
            var result = UsuarioRepository.VerificaLogin(login);
            return Ok(result);
        }

    }
}
