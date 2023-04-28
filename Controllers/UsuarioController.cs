//using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
//using System.Data.SQLite;
using System.Linq;
using TelaLogin.Repository;
using TelaLogin.Model;
using System.Threading.Tasks;
using System;
using TelaLogin.Validation;

namespace TelaLogin.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet("Ola")]
        public ActionResult Ola()
        {
            return Ok("ola");
        }

        [HttpGet("Listar")]
        public async Task<ActionResult> ListarUsuario()
        {
            try
            {
                var resp = await UsuarioRepository.listarUsuarios();
                return Ok(resp);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
           
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
            var validator = new UsuarioValidator();
            var result = validator.Validate(usuario);
            if (result.IsValid==false)
            {
                return BadRequest(new { Messager = result.Errors.First().ErrorMessage } );
            }
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
            var validator = new LoginUsuarioValidator();
            var result = validator.Validate(login);
            if (result.IsValid == false)
            {
                return BadRequest(new { Messager = result.Errors[0].ErrorMessage });
            }
            var resp = UsuarioRepository.VerificaLogin(login);
            return Ok(resp);
        }

    }
}
