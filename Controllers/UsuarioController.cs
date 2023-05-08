//using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TelaLogin.Model;
using System;
using AutoMapper;
using TelaLogin.DTO;
using FluentValidation;
using TelaLogin.Interfaces;
using TelaLogin.ExceptionResponse;

namespace TelaLogin.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IValidator<UsuarioRequest> _validatorUsuario;
        private readonly IValidator<LoginUsuario> _validatorLogin;
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IMapper mapper, IValidator<UsuarioRequest> validatorUsuario, IValidator<LoginUsuario> validatorLogin, IUsuarioService usuarioService)
        {
            _mapper = mapper;
            _validatorUsuario = validatorUsuario;
            _validatorLogin = validatorLogin;
            _usuarioService = usuarioService;
        }

        [HttpGet("Ola")]
        public ActionResult Ola()
        {
            return Ok("ola");
        }

        [HttpGet("Listar")]
        public ActionResult ListarUsuario()
        {
            try
            {
                var resp = _usuarioService.listarUsuarios();
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("Buscar/{id}")]
        public ActionResult<UsuarioResponse> BuscarUsuario(int id)
        {
            try
            {
                var resp = _usuarioService.BuscarUsuario(id);
                return resp;
            }
            catch (NaoEncontradoException ex)
            {
                return StatusCode(ex.StatusCode, new { ex.Message });
            }
            catch (Exception ex)
            {
                /*if (ex is NaoEncontradoException)
                {
                    return StatusCode(404, new { ex.Message });
                }*/
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("cadastrar")]
        public ActionResult Salvar([FromBody] UsuarioRequest usuarioRequest)
        {
           
            var result = _validatorUsuario.Validate(usuarioRequest);
            if (result.IsValid == false)
                return BadRequest(new { Messager = result.Errors.First().ErrorMessage });
           
            try
            {
                
                var resp = _usuarioService.SalvarUsuario(usuarioRequest);
                
                return Ok(new { Message = "salvo com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /* [HttpPut("alterar")]
         public ActionResult Alterar([FromBody] Usuario usuario)
         {         
             try
             {
                var comparaUsuario = _usuarioService.BuscarUsuario(usuario.Id);
                 if(comparaUsuario != null && comparaUsuario.Email != usuario.Email)
                 {
                     if (_usuarioService.VerificaDuplicidadeEmail(usuario.Email))
                     {
                         return BadRequest(new { Message = "Este email já cadastrado!" });
                     }
                 }

                 int resp = _usuarioService.AlterarUsuario(usuario);
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
         }*/

        [HttpDelete("excluir/{id}")]
        public ActionResult Excluir(int id)
        {
            try
            {
                int resp = _usuarioService.ExcluirUsuario(id);
                return Ok(new { Message = "Excluido com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
        /*[HttpPost("logar")]
        public ActionResult Logar([FromBody] LoginUsuario login)
        {
            //var validator = new UsuarioLoginValidator();
            var result = _validatorLogin.Validate(login);
            if (result.IsValid == false)
            {
                return BadRequest(new { Messager = result.Errors[0].ErrorMessage });
            }
            try
            {
                var resp = _usuarioRepository.VerificaLogin(login);

                if (resp != null)
                {                                    

                    return Ok(new { resp,token="tokenTeste",refreshToken="refreshTokenTeste" });
                }
                return BadRequest(new { message = "email ou senha invalidos" });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }*/

    }
}
