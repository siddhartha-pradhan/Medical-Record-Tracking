using Microsoft.AspNetCore.Identity;

namespace Silverline.Core.Entities;

public class User : IdentityUser
{
    public string FullName { get; set; }

    public string ImageURL { get; set; }

    public byte[] ProfileImage { get; set; }
}