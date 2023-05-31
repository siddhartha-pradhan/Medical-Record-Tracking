using System.ComponentModel.DataAnnotations;

namespace Silverline.Core.ViewModels;

public class PatientViewModel
{
    public string UserId { get; set; }

    public string PatientId { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }

    public string ProfileImage { get; set; }

    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; }
    
    public string Email { get; set; }
    
    public string Address { get; set; }
    
    [Display(Name = "Date of Birth")]
    public string DateOfBirth { get; set; }
    
    [Display(Name = "Credit Points")]
    public int CreditPoints { get; set; }

    public bool IsLocked { get; set; }
}
