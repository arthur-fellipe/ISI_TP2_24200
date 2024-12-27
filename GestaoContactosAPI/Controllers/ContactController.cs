using GestaoContactosAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ServiceReference1;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly ContactServiceClient soapClient;

    public ContactController(IHttpClientFactory clientFactory)
    {
        soapClient = new ContactServiceClient(ContactServiceClient.EndpointConfiguration.BasicHttpBinding_IContactService);
    }

    [HttpGet]
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

