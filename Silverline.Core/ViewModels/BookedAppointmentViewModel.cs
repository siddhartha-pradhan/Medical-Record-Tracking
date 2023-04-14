namespace Silverline.Core.ViewModels;

public class BookedAppointmentViewModel
{
    public Guid DoctorId { get; set; }  

    public byte[] DoctorImage { get; set; }

    public string DoctorName { get; set; }  

    public string Specialty { get; set; }

    public string HighestMedicalDegree { get; set; }

    public string Title { get; set; }

    public string DateOfAppointment { get; set; }

    public string BookedDate { get; set; }

}
