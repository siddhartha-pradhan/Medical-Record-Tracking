using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Silverline.Application.Interfaces.Repositories;
using Silverline.Application.Interfaces.Services;
using Silverline.Infrastructure.Implementation.Repositories;
using Silverline.Infrastructure.Implementation.Services;
using Silverline.Infrastructure.Persistence;

namespace Silverline.Infrastructure.Dependency;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SilverlineContextConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddIdentity<IdentityUser, IdentityRole>(options => 
            options.SignIn.RequireConfirmedAccount = true)
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddTransient<IUnitOfWork, UnitOfWork>();

        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddTransient<IManufacturerService, ManufacturerService>();
        services.AddTransient<IMedicineService, MedicineService>();
        services.AddTransient<ISpecialtyService, SpecialtyService>();
        services.AddTransient<ITestService, TestService>();
        services.AddTransient<ITestTypeService, TestTypeService>();

        return services;
    }
}
