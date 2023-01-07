using Microsoft.EntityFrameworkCore;
using AuthApi.Models;
namespace AuthApi.DpContext;
public class ModelContext: DbContext
{
    public DbSet<User> users { get; set; }
    public  DbSet<Token> Tokens { get; set; }
    public string DbPath { get; set; }
    public ModelContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "TouchTypingUsers.db");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("server=.;database=TouchTypingUsers;trusted_connection=true;");
    }
}