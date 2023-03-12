using Microsoft.AspNetCore.Cors;
using Silverline.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Silverline.Core.ViewModels;

public class AppointmentViewModel
{
    public Appointment Appointment { get; set; }

    [Display(Name = "Patient Name")]
    public string? PatientName { get; set; }

    [Display(Name = "Patient Age")]
    public int? Age { get; set; }

    [Display(Name = "Doctor Name")]
    public string? DoctorName { get; set; }

    public byte[]? DoctorProfileImage { get; set; }

    public string? DoctorSpecialty { get; set; }

    public string? HighestMedicalDegree { get; set; }    
}
