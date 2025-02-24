namespace EMR.Core.ViewModels;

public class AppointmentsViewModel
{
    public Guid AppointmentId { get; set; }

	public Guid PatientId { get; set; }

	public string PatientImage { get; set; }

	public int PatientAge { get; set; }

	public string PatientName { get; set; } 

    public string PatientRequest { get; set; }  

	public string PaymentStatus { get; set; }
}
