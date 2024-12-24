using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using GestaoContactosAPI.Models;

[Authorize] // Garante que apenas utilizadores autenticados acessem os endpoints
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public UserController(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("SoapClient");
    }

    // Endpoint para criar um novo utilizador
    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        var soapRequest = CreateSoapRequest("AddUser", user);
        var response = await _httpClient.PostAsync("", soapRequest);
        var soapResponse = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return Ok("Utilizador criado com sucesso!");
        }

        return BadRequest("Erro ao criar utilizador: " + soapResponse);
    }

    // Endpoint para buscar informações do utilizador autenticado
    [HttpGet("me")]
    public IActionResult GetCurrentUser()
    {
        var username = User.FindFirstValue(ClaimTypes.Name);
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (username == null || userId == null)
        {
            return Unauthorized("Token inválido ou expirado.");
        }

        return Ok(new
        {
            UserId = userId,
            Username = username
        });
    }

    // Cria o envelope SOAP para chamadas ao serviço SOAP
    private StringContent CreateSoapRequest(string method, User user = null)
    {
        var soapEnvelope = new StringBuilder();

        soapEnvelope.AppendLine(@"<?xml version=""1.0"" encoding=""utf-8""?>");
        soapEnvelope.AppendLine(@"<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">");
        soapEnvelope.AppendLine(@"  <soap:Body>");

        if (method == "AddUser" && user != null)
        {
            soapEnvelope.AppendLine(@"    <AddUser xmlns=""http://tempuri.org/"">");
            soapEnvelope.AppendLine($"      <Username>{user.Username}</Username>");
            soapEnvelope.AppendLine($"      <PasswordHash>{user.PasswordHash}</PasswordHash>");
            soapEnvelope.AppendLine(@"    </AddUser>");
        }

        soapEnvelope.AppendLine(@"  </soap:Body>");
        soapEnvelope.AppendLine(@"</soap:Envelope>");

        return new StringContent(soapEnvelope.ToString(), Encoding.UTF8, "text/xml");
    }
}

