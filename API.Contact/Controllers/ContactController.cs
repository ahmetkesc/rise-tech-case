using Microsoft.AspNetCore.Mvc;

namespace API.Contact.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    [HttpGet("get")]
    public IActionResult Get()
    {
        return Ok("Contact API running on microservice.");
    }
}