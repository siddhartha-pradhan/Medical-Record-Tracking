using System.ComponentModel.DataAnnotations.Schema;

namespace Silverline.Core.Entities;

public class Appointment
{
    public Guid PatientId { get; set; } 

    public Guid DoctorId { get; set; }

    public DateTime DateOfAppointment { get; set; }
    
    public string AppointmentStatus { get; set; }

    public string PaymentStatus { get; set; }

    public float FeeAmount { get; set; }

    public string DiagnosisDescription { get; set; }

    [ForeignKey("PatientId")]
    public Patient Patient { get; set; }

    [ForeignKey("DoctorId")]
    public Doctor Doctor { get; set; }
}
