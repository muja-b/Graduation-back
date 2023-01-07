using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    
    [HttpGet(Name = "Posts")]
    public Task<ActionResult<PostDTO>> Get()
    {
        return Ok();
    }
}
