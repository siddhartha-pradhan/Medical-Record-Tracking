namespace EMR.Core.ViewModels;

public class BookedAppointmentViewModel
{
    public Guid AppointmentId { get; set; }

    public Guid DoctorId { get; set; }  

    public string DoctorImage { get; set; }

    public string DoctorName { get; set; }  

    public string Specialty { get; set; }

    public string HighestMedicalDegree { get; set; }

    public string Title { get; set; }

    public string DateOfAppointment { get; set; }

    public string BookedDate { get; set; }

    public string PaymentStatus { get; set; }

}
