using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WEB_Project.Models;

namespace WEB_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Expertise> Expertises { get; set; }
		public DbSet<BarberShopInfo> BarberShopInfos { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define relationship between ApplicationUser and Expertise
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Expertises)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
