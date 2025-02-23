using Silverline.Core.Entities;
using Silverline.Core.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Silverline.Infrastructure.Persistence.Seed;

public class DbInitializer : IDbInitializer
{
    private readonly ApplicationDbContext _dbContext;
	private readonly UserManager<IdentityUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;

	public DbInitializer(ApplicationDbContext dbContext, 
        UserManager<IdentityUser> userManager, 
        RoleManager<IdentityRole> roleManager)
    {
		_dbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    public async Task Initialize()
    {
        try
        {
			if (_dbContext.Database.GetPendingMigrations().Count() > 0)
			{
				_dbContext.Database.Migrate();
			}
		}
		catch (Exception)
		{
            throw;
		}

        if (!_roleManager.RoleExistsAsync(Constants.Admin).GetAwaiter().GetResult())
        {
			_roleManager.CreateAsync(new IdentityRole(Constants.Admin)).GetAwaiter().GetResult();
			_roleManager.CreateAsync(new IdentityRole(Constants.Patient)).GetAwaiter().GetResult();
			_roleManager.CreateAsync(new IdentityRole(Constants.Doctor)).GetAwaiter().GetResult();
			_roleManager.CreateAsync(new IdentityRole(Constants.LabTechnician)).GetAwaiter().GetResult();
			_roleManager.CreateAsync(new IdentityRole(Constants.Pharmacist)).GetAwaiter().GetResult();
		}

        var user = new AppUser
        {
            FullName = "Siddhartha Pradhan",
            Email = "siddhartha.pradhan.ix+admin@gmail.com",
            NormalizedEmail = "SIDDHARTHA.PRADHAN.IX@GMAIL.COM",
            UserName = "siddhartha.pradhan.ix+admin@gmail.com",
            NormalizedUserName = "SIDDHARTHA.PRADHAN.IX@GMAIL.COM",
            PhoneNumber = "9803364638",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString("D")
        };

		var userManager = _userManager.CreateAsync(user, "@ff!N1ty").GetAwaiter().GetResult();

		var result = _dbContext.Users.FirstOrDefault(u => u.Email == "siddhartha.pradhan.ix+admin@gmail.com");

		_userManager.AddToRoleAsync(user, Constants.Admin).GetAwaiter().GetResult();

		await _dbContext.SaveChangesAsync();
    }
}
