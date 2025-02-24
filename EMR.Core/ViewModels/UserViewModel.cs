using System.ComponentModel.DataAnnotations;

namespace EMR.Core.ViewModels;

public class UserViewModel
{
	public string UserId { get; set; }

	public Guid StaffId { get; set; }

	public string Name { get; set; }

	public string Role { get; set; }

	public string Email { get; set; }

	[Display(Name = "Phone Number")]
	public string PhoneNumber { get; set; }

	[Display(Name = "Highest Medical Degree")]
	public string HighestMedicalDegree { get; set; }

	[Display(Name = "Certification Number")]
	public string CertificationNumber { get; set; }

	public Guid? Speciality { get; set; }

	public string ProfileImage { get; set; }
}
