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
            try
            {
                var resp = UsuarioRepository.BuscarUsuario(id);
                if (resp != null)
                {
                    return Ok(resp);
                }
                return BadRequest(new { Message = "Usuario não encontrado" });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("cadastrar")]
        public ActionResult Salvar([FromBody] Usuario usuario)
        {
            Console.WriteLine(usuario.Id); 
            var validator = new UsuarioCreateValidator();
            var result = validator.Validate(usuario);
            if (result.IsValid == false)
            {
                return BadRequest(new { Messager = result.Errors.First().ErrorMessage });
            }
            try
            {
                if (UsuarioRepository.VerificaDuplicidadeEmail(usuario.Email))
                {
                    return BadRequest(new { Message = "Este email já cadastrado!" });
                }

                var resp = UsuarioRepository.salvarUsuario(usuario);
                if (resp > 0)
                {
                    return Ok(new { Message = "salvo com sucesso" });
                }
                return StatusCode(200,new { Message = "não foi salvo" });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("alterar")]
        public ActionResult Alterar([FromBody] Usuario usuario)
        {         
            try
            {
               var comparaUsuario = UsuarioRepository.BuscarUsuario(usuario.Id);
                if(comparaUsuario != null && comparaUsuario.Email != usuario.Email)
                {
                    if (UsuarioRepository.VerificaDuplicidadeEmail(usuario.Email))
                    {
                        return BadRequest(new { Message = "Este email já cadastrado!" });
                    }
                }
               
                int resp = UsuarioRepository.AlterarUsuario(usuario);
                if (resp > 0)
                {
                    return Ok(new { message = "alterado com sucesso" });
                }
                return Ok(new { message = "não foi alterado" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("excluir/{id}")]
        public ActionResult Excluir(int id)
        {
            try
            {
                int resp = (int)UsuarioRepository.ExcluirUsuario(id);
                if (resp > 0)
                {
                    return Ok(new {Message= "Excluido com sucesso" });
                }
                return Ok(new { Message = "não foi possivel fazer exclusão" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("logar")]
        public ActionResult Logar([FromBody] LoginUsuario login)
        {
            var validator = new UsuarioLoginValidator();
            var result = validator.Validate(login);
            if (result.IsValid == false)
            {
                return BadRequest(new { Messager = result.Errors[0].ErrorMessage });
            }
            try
            {                
                var resp = UsuarioRepository.VerificaLogin(login);
                if(resp!=null)
                {
                    return Ok(resp);
                }
                return BadRequest(new {message="email ou senha invalidos"});
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
