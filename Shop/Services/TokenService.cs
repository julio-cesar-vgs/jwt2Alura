using Microsoft.IdentityModel.Tokens;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services
{
    public static class TokenService
    {
        public static string GenerateToken(User user)
        {
            var direitos = new[]
             {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Username.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                    //new Claim(ClaimTypes.Name, user.Username.ToString()),
                    //new Claim(ClaimTypes.Role, user.Role.ToString())
             };
            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.Secret));
            //gerado a credenciais para gerar o token
            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "Shop.WebApp",
                audience: "Postman",
                claims: direitos,
                signingCredentials: credenciais,
                expires: DateTime.Now.AddMinutes(30)
                );

            var validacaoToken = new JwtSecurityTokenHandler().WriteToken(token);

            return validacaoToken;
        }
    }
}
