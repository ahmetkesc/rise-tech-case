using Business.Manager;
using Microsoft.AspNetCore.Mvc;

namespace API.Report.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    private IReport _report;

    public ReportController(IReport report)
    {
        _report = report;
    }

    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var result = _report.GetAll();
        if (result.Success) return Ok(result);

        return BadRequest(result);
    }

    [HttpGet("requestreport")]
    public IActionResult RequestReport()
    {
        var result = _report.RequestReport();
        return Ok(result);
    }

    [HttpGet("getreport")]
    public IActionResult GetReport(Guid id)
    {
        var result = _report.GetReport(id);
        return Ok(result);
    }
}