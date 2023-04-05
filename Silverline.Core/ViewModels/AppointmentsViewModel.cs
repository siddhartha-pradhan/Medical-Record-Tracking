namespace Silverline.Core.ViewModels;

public class AppointmentsViewModel
{
    public Guid AppointmentId { get; set; }

	public Guid PatientId { get; set; }

	public byte[] PatientImage { get; set; }

	public int PatientAge { get; set; }

	public string PatientName { get; set; } 

    public string PatientRequest { get; set; }  
}
