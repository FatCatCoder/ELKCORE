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
    [HttpGet("sucess")]
    public async Task<IActionResult> GetGoodTest()
    {
        Log.Information("Ypu may proceed");
        return new JsonResult(new{StatusCode = 200, Message = "Everything is all okay"});
    }
    [HttpGet("error")]
    public async Task<IActionResult> GetBadTest()
    {
        Log.Information("Bad Auth, Please Try again");
        return new JsonResult(403);
    }
    [HttpGet("exception")]
    public async Task<IActionResult> GetExceptionTest()
    {
        try
        {
            Log.Information("I'm about to crash!");
            throw new System.Exception("Server Has been hit");

            return new JsonResult(403);
        }
        catch (System.Exception ex)
        {
            return new JsonResult(new {StatusCode = 500, ExceptionMessage = ex.Message});
        }
        
    }
}
