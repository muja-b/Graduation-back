using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class Comments: ControllerBase
{
    [HttpGet(Name = "Comments")]
    public Task<ActionResult<PostDTO>> Get()
    {
        return Ok();
    }
}