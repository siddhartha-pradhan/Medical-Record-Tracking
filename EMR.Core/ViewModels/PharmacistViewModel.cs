﻿using System.ComponentModel.DataAnnotations;

namespace Silverline.Core.ViewModels;

public class PharmacistViewModel
{
	public string UserId { get; set; }	

	public string PharmacistId { get; set; }

	public string Name { get; set; }

	public string Image { get; set; }

	public string ProfileImage { get; set; }

	[Display(Name = "Phone Number")]
	public string PhoneNumber { get; set; }
	
	public string Email { get; set; }
	
	[Display(Name = "Certification Number")]
	public string CertificationNumber { get; set; }
	
	[Display(Name = "Highest Medical Degree")]
	public string HighestDegree { get; set; }
	
	public string Resume { get; set; }

	public string Certification { get; set; }

	public bool IsLocked { get; set; }	
}
