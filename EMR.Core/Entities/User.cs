using Microsoft.AspNetCore.Identity;

namespace EMR.Core.Entities;

public class User : IdentityUser<Guid>
{
    public string Name { get; set; } = string.Empty;

    public string Image { get; set; } = string.Empty;
}