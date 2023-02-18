﻿using Microsoft.AspNetCore.Identity;

namespace Silverline.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }

        public string PhotoUrl { get; set; }
    }
}
