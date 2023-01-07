using Microsoft.AspNetCore.Mvc;
using WebApplication1.services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class Comments: ControllerBase
{
    private ICommentService _commentService;

    public Comments(ICommentService commentService)
    {
        _commentService = commentService;
    }
    [HttpGet(Name = "Comments")]
    public async Task<ActionResult<PostDTO>> Get(int PostId)
    {
        return Ok(await _commentService.PostComments(PostId));
    }
    [HttpPost(Name = "Comments")]
    public async Task<ActionResult<PostDTO>> Post(Comment comment)
    {
        return Ok(await _commentService.AddComment(comment));
    }
}