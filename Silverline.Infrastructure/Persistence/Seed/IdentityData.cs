using Silverline.Core.Entities;
using Silverline.Core.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Silverline.Infrastructure.Persistence.Seed;

public class IdentityData
{
    private readonly ApplicationDbContext _context;

    public IdentityData(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Initialize()
    {
        var roles = new string[] { Constants.Admin, Constants.Doctor, Constants.Patient, Constants.LabTechnician, Constants.Pharmacist };

        foreach (string role in roles)
        {
            var roleStore = new RoleStore<IdentityRole>(_context);

            if (!_context.Roles.Any(r => r.Name == role))
            {
                roleStore.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
            }
        }

        var user = new AppUser
        {
            FullName = "Anuj Pradhan",
            Email = "anuj.pradhan.ix@gmail.com",
            NormalizedEmail = "ANUJ.PRADHAN.IX@GMAIL.COM",
            UserName = "Anuj Pradhan",
            NormalizedUserName = "ANUJ PRADHAN",
            PhoneNumber = "9861592574",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString("D")
        };


        if (!_context.Users.Any(u => u.UserName == user.UserName))
        {
            var userStore = new UserStore<AppUser>(_context);

            var password = new PasswordHasher<AppUser>();
            var hashed = password.HashPassword(user, "@ff!N1ty");
            user.PasswordHash = hashed;

            var result = userStore.CreateAsync(user);
            await userStore.AddToRoleAsync(user, Constants.Admin);

        }

        await _context.SaveChangesAsync();
    }
}
