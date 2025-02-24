using Microsoft.AspNetCore.Identity;

namespace EMR.Core.Entities;

public class User : IdentityUser
{
    public string FullName { get; set; }

    public string ImageURL { get; set; }

    public byte[] ProfileImage { get; set; }
}