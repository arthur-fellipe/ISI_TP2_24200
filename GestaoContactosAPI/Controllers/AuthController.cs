using GestaoContactosAPI.Models;
using GestaoContactosAPI.Auxiliar;
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
using Swashbuckle.AspNetCore.Annotations;


[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    // Configuração
    private readonly IConfiguration _config;

    // Construtor
    public AuthController(IConfiguration config)
    {
        _config = config;
    }

    [HttpPost("register")]
    [SwaggerOperation(
        Summary = "Registar utilizador",
        Description = "Regista um novo utilizador na aplicação.",
        OperationId = "Register",
        Tags = new[] { "Auth" }
    )]
    [SwaggerResponse(200, "Utilizador registado com sucesso!")]
    [SwaggerResponse(400, "O nome de utilizador e a palavra-passe são obrigatórios.")]
    [SwaggerResponse(500, "Erro ao registar utilizador.")]
    public IActionResult Register([FromBody] User user)
    {
        // Verifica se o nome de utilizador e a palavra-passe são válidos
        if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.PasswordHash))
        {
            return BadRequest("O nome de utilizador e a palavra-passe são obrigatórios.");
        }

        // Regista o utilizador na base de dados
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
    [SwaggerOperation(
        Summary = "Login",
        Description = "Autentica um utilizador na aplicação.",
        OperationId = "Login",
        Tags = new[] { "Auth" }
    )]
    [SwaggerResponse(200, "Autenticado com sucesso!")]
    [SwaggerResponse(400, "O nome de utilizador e a palavra-passe são obrigatórios.")]
    [SwaggerResponse(401, "Credenciais inválidas.")]
    public IActionResult Login([FromBody] User loginRequest)
    {
        // Verifica se o nome de utilizador e a palavra-passe são válidos
        if (string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.PasswordHash))
        {
            return BadRequest("O nome de utilizador e a palavra-passe são obrigatórios.");
        }

        // Procura o utilizador na base de dados  
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

        // Verifica se o utilizador existe e se a palavra-passe está correta
        if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.PasswordHash, user.PasswordHash))
        {
            return Unauthorized("Credenciais inválidas.");
        }

        // Gera um token JWT
        var token = AuthAuxiliar.GenerateToken(user, _config);
        return Ok(new { Token = token });
    }
}
