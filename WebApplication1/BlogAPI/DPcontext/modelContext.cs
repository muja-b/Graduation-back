using Microsoft.EntityFrameworkCore;

public class modelContext : DbContext
{
        public DbSet<Post> Posts{ get; set; }
        public DbSet<Comment> Comments { get; set; }
        public string DbPath { get; set; }

        public modelContext()
        {
                var folder = Environment.SpecialFolder.LocalApplicationData;
                var path = Environment.GetFolderPath(folder);
                DbPath = Path.Join(path, "Blog");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
                optionsBuilder.UseSqlServer("server=.;database=Blog;trusted_connection=true;");
        }
}