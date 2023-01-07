using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApplication1.services;

public class CommentService:ICommentService
{
    private modelContext _con;

    public CommentService(modelContext con)
    {
        _con = con;
    }
    public async Task<bool> AddComment(Comment comment)
    {
        var res=await _con.Comments.AddAsync(comment);
        
        return await SaveChanges();
    }



    public async Task<List<Comment>> PostComments(int PostId)
    {
        var res = await _con.Comments.Where(curr => curr.PostId == PostId).ToListAsync();
        return res;
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