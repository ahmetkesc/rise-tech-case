using Microsoft.AspNetCore.Mvc;

namespace API.Report.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    
    [HttpGet("get")]
    public IActionResult Get()
    {
        return Ok("Report API running on microservice.");
    }
}