using System.ComponentModel.DataAnnotations;

namespace EMR.Core.ViewModels;

public class AddPatientViewModel
{
    [Required]
    [Display(Name = "Patient Name")]
    public string Name { get; set; }

    [Required]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; }

	[Required]
    [Display(Name = "Email Address")]
	public string EmailAddress { get; set; }

	[Required]
    [Display(Name = "Date of Birth")]
    public DateTime DateBirth { get; set; }

	[Required]
	public string Street { get; set; }

    [Required]
	public string Address { get; set; }
}
