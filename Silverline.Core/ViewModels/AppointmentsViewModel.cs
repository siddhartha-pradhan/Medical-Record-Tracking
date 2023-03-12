namespace Silverline.Core.ViewModels;

public class AppointmentsViewModel
{
    public Guid AppointmentId { get; set; }

    public byte[] PatientImage { get; set; }

    public string PatientName { get; set; } 

    public string PatientRequest { get; set; }  
}
