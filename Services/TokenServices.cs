using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TelaLogin.Model;

namespace TelaLogin.Services
{
    public class TokenServices
    {
       static private readonly string senhaSecreta = Environment.GetEnvironmentVariable("My_Password_Secrets");//pelo dotenv
        public static string GerarToken(Usuario usuario)
        {
            var key = Encoding.ASCII.GetBytes(senhaSecreta);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                   Subject = new ClaimsIdentity(new Claim[]
                   {
                       new Claim(ClaimTypes.Name, usuario.Id.ToString()),
                       new Claim(ClaimTypes.Name, usuario.Nome.ToString())
                   }),
                   Expires = DateTime.UtcNow.AddHours(3),
                   SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                   SecurityAlgorithms.HmacSha256Signature)                                      
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenCriado = tokenHandler.WriteToken(token);
            return tokenCriado;
        }
    }
}
