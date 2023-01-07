namespace WebApplication1.services;

public interface IPostsService
{
    Task<List<Post>> getPosts();
    Task<bool> addPosts(Post post);
}