using Microsoft.EntityFrameworkCore;
using WEB_Project.Models;

namespace WEB_Project.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Num = 1, Name = "ByeBye21" },
                new Category { Id = 2, Num = 2, Name = "ByeBye22" },
                new Category { Id = 3, Num = 3, Name = "ByeBye23" }
                );
        }
    }
}
