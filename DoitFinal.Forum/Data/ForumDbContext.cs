using DoitFinal.Forum.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DoitFinal.Forum.Data
{
    public class ForumDbContext : IdentityDbContext<ForumUser>
    {
        public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options) { }

        public DbSet<ForumUser> ForumUsers { get; set; }
        public DbSet<ForumRoles> ForumRoles { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}
