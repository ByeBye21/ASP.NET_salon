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

		public DbSet<Category> Categories { get; set; } // Categories DbSet'ini ekliyoruz
		public DbSet<Salon> Salons { get; set; } // Salon modelini ekliyoruz
		public DbSet<Service> Services { get; set; } // Service modelini ekliyoruz

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			// Varsayılan salon verisi ekle
			modelBuilder.Entity<Salon>().HasData(
				new Salon { Id = 1, Name = "My Salon", Address = "123 Salon Street", PhoneNumber = "123-456-7890", WorkingHours="09.00- 18.00" }
					);
		}
	}
}
