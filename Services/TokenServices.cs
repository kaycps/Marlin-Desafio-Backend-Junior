using Marlin_Desafio_Backend_Junior.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Marlin_Desafio_Backend_Junior.Services
{
    public static class TokenServices
    {
        public static string GerarToken(Usuario usuario)
        {

            var tokenHandle = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(settings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, usuario.Login.ToString())

                }),

                Expires = DateTime.UtcNow.AddHours(2),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandle.CreateToken(tokenDescriptor);
            return tokenHandle.WriteToken(token);
        }

    }
}
