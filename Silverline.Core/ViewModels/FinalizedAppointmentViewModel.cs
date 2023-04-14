namespace Silverline.Core.ViewModels; 

public class FinalizedAppointmentViewModel 
{
    public Guid DoctorId { get; set; }

    public byte[] DoctorImage { get; set; }

    public string DoctorName { get; set; }

    public string Specialty { get; set; }

    public string HighestMedicalDegree { get; set; }

    public List<AppointmentFinalizedViewModel> FinalizedAppointments { get; set; }
}
