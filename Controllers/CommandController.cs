using Microsoft.AspNetCore.Mvc;
using SerialWebAPI.Models;
using SerialWebAPI.Services;

namespace SerialWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommandController : ControllerBase
{
    private readonly SerialService _serialService;
    public CommandController(SerialService serialService)
    {
        _serialService = serialService;
    }
    [HttpPost]
    public IActionResult Send([FromBody] CommandRequest request)
    {
        _serialService.SendCommand(request.Command);
        return Ok(request.Command + " Command sent");
    }
}