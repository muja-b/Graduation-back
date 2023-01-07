using Microsoft.EntityFrameworkCore;

namespace WebApplication1.services;

public class PostsService:IPostsService
{
    private modelContext _con;

    public PostsService(modelContext con)
    {
        _con = con;
    }
    public async Task<List<Post>> getPosts()
    {
        var res =await _con.Posts.ToListAsync();
        return res;
    }

    public async Task<bool> addPosts(Post post)
    {
        await _con.Posts.AddAsync(post);
        return await SaveChanges();
    }

    public async Task<bool> likePost(int postId)
    {
        var likedPost = await _con.Posts.FirstAsync(post => post.id == postId);
        likedPost.Likes = likedPost.Likes + 1;
        return await SaveChanges();
    }

    private async Task<bool> SaveChanges()
    {
        var added=await _con.SaveChangesAsync();
        if (added > 0)
            return true;
        else
        {
            return false;
        }
    }
}