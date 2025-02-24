using Microsoft.AspNetCore.Cors;
using EMR.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EMR.Core.ViewModels;

public class AppointmentViewModel
{
    public Appointment Appointment { get; set; }

    [Display(Name = "Patient Name")]
    public string? PatientName { get; set; }

    [Display(Name = "Patient Age")]
    public int? Age { get; set; }

    [Display(Name = "Doctor Name")]
    public string? DoctorName { get; set; }

    [Display(Name = "Pay via Credits (50 Credit Points)")]
    public string PaymentStatus { get; set; }

    public string? DoctorProfileImage { get; set; }

    public string? DoctorSpecialty { get; set; }

    public string? HighestMedicalDegree { get; set; }    
}
