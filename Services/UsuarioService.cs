using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using TelaLogin.DTO;
using TelaLogin.ExceptionResponse;
using TelaLogin.Interfaces;
using TelaLogin.Model;

namespace TelaLogin.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IMapper _mapper;
        public UsuarioService(IUsuarioRepositorio usuarioRepositorio, IMapper mapper)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
        }

        public List<UsuarioResponse> listarUsuarios()
        {
            var resp = _usuarioRepositorio.listarUsuarios();
            var listaUsuarioResponse = _mapper.Map<List<UsuarioResponse>>(resp);
            return listaUsuarioResponse;
        }

        public UsuarioResponse BuscarUsuario(int id)
        {

            var resp = _usuarioRepositorio.BuscarUsuario(id);
            var usuarioResponse = _mapper.Map<UsuarioResponse>(resp);
            if (usuarioResponse != null)
            {
                return usuarioResponse;
            }
            //throw new NaoEncontradoException($"Usuário com id {id} não foi encontrado", StatusCodes.Status404NotFound);
            throw new NaoEncontradoException();

        }
        public int SalvarUsuario(UsuarioRequest usuario)
        {

            if (VerificaDuplicidadeEmail(usuario.Email))
            {
                throw new Exception(message: "Email já cadastrado");
            }

            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha, 10);
            var resp = _usuarioRepositorio.SalvarUsuario(_mapper.Map<Usuario>(usuario));
            if (resp > 0)
                return resp;
            throw new Exception(message: "Não foi possivel Salvar Cadastro");
        }
        public int AlterarUsuario(Usuario usuario)
        {
            var comparaUsuario = _usuarioRepositorio.BuscarUsuario(usuario.Id);
            Console.WriteLine(comparaUsuario.Senha);
            if (comparaUsuario != null && comparaUsuario.Email != usuario.Email)
            {
                if (VerificaDuplicidadeEmail(usuario.Email))
                {
                    throw new Exception(message: "Email já cadastrado");
                }
            }
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha, 10);
            var resp = _usuarioRepositorio.AlterarUsuario(usuario);
            if (resp > 0)
            {
                return resp;
            }
            throw new Exception(message: "Não foi possivel alterar Cadastro.");
        }
        public int ExcluirUsuario(int id)
        {
            var resp = _usuarioRepositorio.ExcluirUsuario(id);
            if (resp > 0)
            {
                return resp;
            }
            throw new Exception(message: "Erro ao Excluir.");
        }
        private bool VerificaDuplicidadeEmail(string email)
        {

            var resp = _usuarioRepositorio.BuscarPorEmail(email);
            if (resp != null)
            {
                return true;
            }
            return false;
        }

        public Usuario VerificaLogin(LoginUsuario login)
        {
            throw new System.NotImplementedException();
        }
        public string GerarToken(Usuario usuario)
        {
            throw new System.NotImplementedException();
        }

        public string GerarRefreshToken(Usuario usuario)
        {
            throw new System.NotImplementedException();
        }





    }
}
