using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace elkcore.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetTest()
    {
        Log.Information("HELLO FROM THE WORLD WIDE WEB");
        return new JsonResult(200);
    }
}
