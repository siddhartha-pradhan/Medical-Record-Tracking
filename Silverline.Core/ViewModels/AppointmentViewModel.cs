using Silverline.Core.Entities;

namespace Silverline.Core.ViewModels;

public class AppointmentViewModel
{
    public Appointment Appointment { get; set; }

    public string PatientName { get; set; }

    public int Age { get; set; }

    public string DoctorName { get; set; }

    public byte[] DoctorProfileImage { get; set; }

    public string DoctorSpecialty { get; set; }

    public string HighestMedicalDegree { get; set; }    
}
