using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Text.Json;

namespace elkcore.Controllers{

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTest()
    {
        Log.Information("HELLO FROM THE WORLD WIDE WEB");
        return new JsonResult(200);
    }
    [HttpPost("ingest")]
    public async Task<IActionResult> GetTest([FromBody] object body)
    {
        Log.Information(JsonSerializer.Serialize(body));
        return new JsonResult(200);
    }
}
}