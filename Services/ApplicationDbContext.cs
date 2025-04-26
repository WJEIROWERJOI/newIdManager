using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using newIdManager.Data.ApplicationUsers;
using newIdManager.Data.Posts;

namespace newIdManager.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<PostEntity> PostEntities { get; set; }
    public DbSet<CommentEntity> CommentEntities { get; set; }
    public DbSet<Board> Boards { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PostEntity>().ToTable("Posts");
        modelBuilder.Entity<CommentEntity>().ToTable("Comments");
        modelBuilder.Entity<Board>().ToTable("Boards");
    }


}