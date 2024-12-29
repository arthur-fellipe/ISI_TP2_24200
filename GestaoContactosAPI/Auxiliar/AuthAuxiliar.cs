using GestaoContactosAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace GestaoContactosAPI.Auxiliar
{
    public static class AuthAuxiliar
    {
        [SwaggerOperation(
            Summary = "Gera token"
        )]
        public static string GenerateToken(User user, IConfiguration _config)
        {
            // Criação de claims (informações sobre o usuário) que serão incluídas no token JWT
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("UserID", user.UserID.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
            };

            // Chave de assinatura do token
            var jwtKey = _config["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new Exception("Chave JWT não configurada.");
            }

            // Criação do token JWT
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:ExpiryMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
