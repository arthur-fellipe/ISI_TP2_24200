using GestaoContactosAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;


[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;

    public AuthController(IConfiguration config)
    {
        _config = config;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] User user)
    {
        if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.PasswordHash))
        {
            return BadRequest("O nome de utilizador e a palavra-passe são obrigatórios.");
        }

        using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var command = new SqlCommand("INSERT INTO Utilizadores (Username, PasswordHash) VALUES (@Username, @PasswordHash)", connection);
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@PasswordHash", BCrypt.Net.BCrypt.HashPassword(user.PasswordHash));

            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                return StatusCode(500, $"Erro ao registar utilizador: {ex.Message}");
            }
        }

        return Ok("Utilizador registado com sucesso!");
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] User loginRequest)
    {
        if (string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.PasswordHash))
        {
            return BadRequest("O nome de utilizador e a palavra-passe são obrigatórios.");
        }

        User user = null;

        using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var command = new SqlCommand("SELECT * FROM Utilizadores WHERE Username = @Username", connection);
            command.Parameters.AddWithValue("@Username", loginRequest.Username);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    user = new User
                    {
                        UserID = (int)reader["ID"],
                        Username = reader["Username"].ToString(),
                        PasswordHash = reader["PasswordHash"].ToString()
                    };
                }
            }
        }

        if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.PasswordHash, user.PasswordHash))
        {
            return Unauthorized("Credenciais inválidas.");
        }

        var token = GenerateToken(user);
        return Ok(new { Token = token });
    }

    [HttpPost("generate-token")]
    public string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("UserID", user.UserID.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

        var jwtKey = _config["Jwt:Key"];
        if (string.IsNullOrEmpty(jwtKey))
        {
            throw new Exception("Chave JWT não configurada.");
        }

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
