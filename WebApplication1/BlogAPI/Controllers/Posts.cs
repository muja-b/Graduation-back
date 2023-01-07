using Microsoft.AspNetCore.Mvc;
using WebApplication1.services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private IPostsService _postsService;

    public PostController(IPostsService postsService)
    {
        _postsService = postsService;
    }
    [HttpGet(Name = "Posts")]
    public async Task<ActionResult<List<Post>>> Get()
    {
        return  Ok(await _postsService.getPosts());
    }

    [HttpPost(Name = "Post")]
    public async Task<ActionResult<bool>> Post(Post post)
    {
        return Ok(await _postsService.addPosts(post));
    }

    [HttpPost("Like")]
    public async Task<ActionResult<bool>> LikePost(int postId)
    {
        return Ok(await _postsService.likePost(postId));
    }
}
