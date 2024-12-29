using GestaoContactosAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ServiceReference1;
using Swashbuckle.AspNetCore.Annotations;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    // Instância do cliente SOAP
    private readonly ContactServiceClient soapClient;

    // Construtor
    public ContactController(IHttpClientFactory clientFactory)
    {
        soapClient = new ContactServiceClient(ContactServiceClient.EndpointConfiguration.BasicHttpBinding_IContactService);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Obter todos os contactos",
        Description = "Obtém todos os contactos do utilizador autenticado.",
        OperationId = "GetAllContacts",
        Tags = new[] { "Contact" }
    )]
    [SwaggerResponse(200, "Contactos obtidos com sucesso.")]
    [SwaggerResponse(404, "Não foram encontrados contactos.")]
    [SwaggerResponse(500, "Erro interno do servidor.")]
    public async Task<IActionResult> GetAllContacts()
    {
        try
        {
            Contact[] contactos = await soapClient.GetAllContactsAsync();
            if (contactos == null || contactos.Length == 0)
            {
                return NotFound("No contacts found.");
            }
            return Ok(contactos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Obter contacto por ID",
        Description = "Obtém um contacto específico pelo seu ID.",
        OperationId = "GetContactById",
        Tags = new[] { "Contact" }
    )]
    [SwaggerResponse(200, "Contacto obtido com sucesso.")]
    [SwaggerResponse(404, "Contacto não encontrado.")]
    [SwaggerResponse(500, "Erro interno do servidor.")]
    public async Task<IActionResult> GetContactById(int id)
    {
        try
        {
            Contact contact = await soapClient.GetContactByIdAsync(id);
            if (contact == null)
            {
                return NotFound("Contact not found.");
            }
            return Ok(contact);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Criar contacto",
        Description = "Cria um novo contacto.",
        OperationId = "CreateContact",
        Tags = new[] { "Contact" }
    )]
    [SwaggerResponse(200, "Contacto criado com sucesso.")]
    [SwaggerResponse(400, "Erro ao criar contacto.")]
    [SwaggerResponse(500, "Erro interno do servidor.")]
    public async Task<IActionResult> CreateContact([FromBody] Contact contact)
    {
        try
        {
            // Obter o UserID do token JWT
            var userIdClaim = User.FindFirst("UserID")?.Value;

            // Converter o UserID para inteiro
            var userId = int.Parse(userIdClaim);

            // Associar o UserID ao novo contacto
            contact.UserID = userId;

            int result = await soapClient.AddContactAsync(contact);
            if (result > 0)
            {
                return Ok("Contact created successfully.");
            }
            return BadRequest("Error creating contact.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut]
    [SwaggerOperation(
        Summary = "Atualizar contacto",
        Description = "Atualiza um contacto existente.",
        OperationId = "UpdateContact",
        Tags = new[] { "Contact" }
    )]
    [SwaggerResponse(200, "Contacto atualizado com sucesso.")]
    [SwaggerResponse(400, "Erro ao atualizar contacto.")]
    [SwaggerResponse(500, "Erro interno do servidor.")]
    public async Task<IActionResult> UpdateContact([FromBody] Contact contact)
    {
        try
        {
            int result = await soapClient.UpdateContactAsync(contact);
            if (result > 0)
            {
                return Ok("Contact updated successfully.");
            }
            return BadRequest("Error updating contact.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete]
    [SwaggerOperation(
        Summary = "Eliminar contacto",
        Description = "Elimina um contacto existente.",
        OperationId = "DeleteContact",
        Tags = new[] { "Contact" }
    )]
    [SwaggerResponse(200, "Contacto eliminado com sucesso.")]
    [SwaggerResponse(400, "Erro ao eliminar contacto.")]
    [SwaggerResponse(500, "Erro interno do servidor.")]
    public async Task<IActionResult> DeleteContact(int id)
    {
        try
        {
            int result = await soapClient.DeleteContactAsync(id);
            if (result > 0)
            {
                return Ok("Contact deleted successfully.");
            }
            return BadRequest("Error deleting contact.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}

