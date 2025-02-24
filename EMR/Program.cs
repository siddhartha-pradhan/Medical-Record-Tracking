using AspNetCoreHero.ToastNotification;
using EMR.Infrastructure.Dependency;
using EMR.Infrastructure.Persistence.Seed;
using AspNetCoreHero.ToastNotification.Extensions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configurations = builder.Configuration;

services.AddInfrastructure(configurations);

services.AddControllersWithViews();

services.AddNotyf(config =>
{
	config.DurationInSeconds = 10; 
	config.IsDismissable = true; 
	config.Position = NotyfPosition.TopRight;
});

services.AddRazorPages().AddRazorRuntimeCompilation();

services.ConfigureApplicationCookie(options =>
{
	options.LogoutPath = "/Identity/Account/Logout";
	options.LoginPath = "/Identity/Account/Login";
	options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseNotyf();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{Area=Admin}/{controller=Home}/{action=Index}/{id?}");

SeedDatabase();

app.Run();

void SeedDatabase()
{
	using var scope = app.Services.CreateScope();
	
	var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
	
	dbInitializer.Initialize();
}