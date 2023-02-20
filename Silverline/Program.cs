using Silverline.Infrastructure.Dependency;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configurations = builder.Configuration;

services.AddInfrastructure(configurations);

services.AddControllersWithViews();

services.AddRazorPages().AddRazorRuntimeCompilation();

services.ConfigureApplicationCookie(options =>
{
	options.LogoutPath = $"/Identity/Account/Logout";
	options.LoginPath = $"/Identity/Account/Login";
	options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{Area=Admin}/{controller=Home}/{action=Index}/{id?}");

app.Run();
