using Desafio.Model;
using Desafio.Repository;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.API.Service
{
    public static class TokenService
    {
        private const double EXPIRE_HOURS = 2.0;

        public static string GenerateToken(UserResponse user)
        {
            try
            {
                //cria chave para encriptação do token
                var key = Encoding.ASCII.GetBytes(Settings.Secret);
                var tokenHandler = new JwtSecurityTokenHandler();

                //Valida se os dados são existentes
                if (user.Email == null || user.Nome == null || user.ID == 0)
                {
                    return string.Empty;
                }

                var descriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Email, user.Email.ToString()),
                    new Claim(ClaimTypes.Name, user.ID.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(EXPIRE_HOURS),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(descriptor);
                return tokenHandler.WriteToken(token);

            }
            catch (Exception ex)
            {
                throw new Exception("Falha na autenticação do usuário");
            }

        }
    }
}
