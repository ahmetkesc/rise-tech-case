using Business.Manager;
using Microsoft.AspNetCore.Mvc;
using Model.Classes;

namespace API.Contact.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private IContact _contact;

    public ContactController(IContact contact)
    {
        _contact = contact;
    }

    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var result = _contact.GetAll();
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getbylocation")]
    public IActionResult GetByLocation(Guid contactId)
    {
        var result = _contact.GetByLocation(contactId);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("add")]
    public IActionResult Add(Model.Classes.Contact contact)
    {
        var result = _contact.Add(contact);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("addlocation")]
    public IActionResult AddLocation(Guid contactId, Location location)
    {
        var result = _contact.AddLocation(contactId, location);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpDelete("deletelocation")]
    public IActionResult AddLocation(Guid contactId, Guid locationId)
    {
        var result = _contact.DeleteLocation(contactId, locationId);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }
}