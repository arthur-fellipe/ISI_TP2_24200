using GestaoContactosAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public ContactController(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("SoapClient");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllContacts()
    {
        var soapRequest = CreateSoapRequest("GetAllContacts");
        var response = await _httpClient.PostAsync("", soapRequest);
        var result = await response.Content.ReadAsStringAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddContact([FromBody] Contact contact)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        contact.UserID = int.Parse(userId);

        var soapRequest = CreateSoapRequest("AddContact", contact);
        var response = await _httpClient.PostAsync("", soapRequest);
        var result = await response.Content.ReadAsStringAsync();
        return Ok(result);
    }

    private StringContent CreateSoapRequest(string method, Contact contact = null)
    {
        var soapEnvelope = new StringBuilder();
        soapEnvelope.AppendLine(@"<?xml version=""1.0"" encoding=""utf-8""?>");
        soapEnvelope.AppendLine(@"<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">");
        soapEnvelope.AppendLine(@"  <soap:Body>");

        if (method == "GetAllContacts")
        {
            soapEnvelope.AppendLine(@"    <GetAllContacts xmlns=""http://tempuri.org/"" />");
        }
        else if (method == "AddContact" && contact != null)
        {
            soapEnvelope.AppendLine(@"    <AddContact xmlns=""http://tempuri.org/"">");
            soapEnvelope.AppendLine($"      <Nome>{contact.Nome}</Nome>");
            soapEnvelope.AppendLine($"      <UserID>{contact.UserID}</UserID>");
            soapEnvelope.AppendLine(@"    </AddContact>");
        }

        soapEnvelope.AppendLine(@"  </soap:Body>");
        soapEnvelope.AppendLine(@"</soap:Envelope>");

        return new StringContent(soapEnvelope.ToString(), Encoding.UTF8, "text/xml");
    }
}

