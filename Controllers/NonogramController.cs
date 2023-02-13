using Microsoft.AspNetCore.Mvc;

namespace ReverseNonogramApi.Controllers;

[ApiController]
[Route("[controller]")]
public class NonogramController : ControllerBase
{
    private readonly ILogger<NonogramController> _logger;

    public NonogramController(ILogger<NonogramController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("yo what's up");
    }

    [HttpPost]
    public IActionResult CreateNonogram()
    {
        return Ok("testing");
    }
}
