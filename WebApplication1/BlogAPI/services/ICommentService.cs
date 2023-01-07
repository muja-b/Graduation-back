namespace WebApplication1.services;

public interface ICommentService
{
    Task <bool> AddComment(Comment comment);
    Task<List<Comment>> PostComments(int PostId);
}