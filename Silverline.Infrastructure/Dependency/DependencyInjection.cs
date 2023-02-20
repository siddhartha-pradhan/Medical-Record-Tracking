using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Silverline.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI.Services;
using Silverline.Application.Interfaces.Services;
using Silverline.Application.Interfaces.Repositories;
using Silverline.Infrastructure.Implementation.Services;
using Silverline.Infrastructure.Implementation.Repositories;
using Silverline.Core.Entities;

namespace Silverline.Infrastructure.Dependency;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddIdentity<IdentityUser, IdentityRole>(options => 
            options.SignIn.RequireConfirmedAccount = true)
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddTransient<IUnitOfWork, UnitOfWork>();

        services.AddTransient<IAppUserService, AppUserService>();
        services.AddTransient<IPatientService, PatientService>();
        services.AddTransient<IDoctorService, DoctorService>();
        services.AddTransient<ILabTechnicianService, LabTechnicianService>();
        services.AddTransient<IPharmacistService, PharmacistService>();
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
