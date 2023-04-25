﻿//using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
//using System.Data.SQLite;
using System.Linq;
using TelaLogin.Repository;
using TelaLogin.Model;
using System.Threading.Tasks;
using System;
using System.Net.Mail;
using System.Security.Permissions;

namespace TelaLogin.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet("oi")]
        public ActionResult oi()
        {
            return Ok("oi");
        }
        [HttpGet("getUsuarios")]
        public async Task<ActionResult> ListarUsuario()
        {
            var resp = await UsuarioRepository.listarUsuarios();
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

        [HttpDelete("excluirUsuario/{id}")]
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
            return Ok(new { login,token="testeToken",refreshToken="teste RefreshToken" } );
        }

    }
}
