using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using WEB_Project.Models;
using WEB_Project.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddRazorPages();

builder.Services.AddScoped<IEmailSender, EmailSender>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
	var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
	var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

	// Ensure the roles exist
	string[] roles = { "Admin", "Customer" };
	foreach (var role in roles)
	{
		if (!await roleManager.RoleExistsAsync(role))
		{
			await roleManager.CreateAsync(new IdentityRole(role));
		}
	}

	// Create an admin user if it doesn't exist
	string email = "b221210588@sakarya.edu.tr";
	string password = "1234aA!";

	if (await userManager.FindByEmailAsync(email) == null)
	{
		var user = new IdentityUser
		{
			UserName = email,
			Email = email,
			EmailConfirmed = true // Optionally confirm the email
		};

		var result = await userManager.CreateAsync(user, password);
		if (result.Succeeded)
		{
			await userManager.AddToRoleAsync(user, "Admin");
		}
	}

	// Seed data for BarberShopInfo table if not already present
	if (!context.BarberShopInfos.Any())
	{
		var barberShopInfo = new BarberShopInfo
		{
			Name = "Umut Berber",
			Address = "Kemalpaşa mahallesi Üniversite Caddesi No:49 Serdivan/Sakarya",
			Phone = "+90 553 025 42 86",
			WorkingHours = "09:00 - 21:00"
		};
		context.BarberShopInfos.Add(barberShopInfo);
		await context.SaveChangesAsync();
	}
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
	name: "default",
	pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
