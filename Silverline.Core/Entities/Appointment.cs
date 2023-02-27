using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Silverline.Core.Entities;

public class Appointment
{
    [Key]
    public Guid Id { get; set; }

    public Guid PatientId { get; set; } 

    public Guid DoctorId { get; set; }

    public DateTime DateOfAppointment { get; set; }
    
    public string AppointmentStatus { get; set; }

    public string PaymentStatus { get; set; } = "Pending";

    public float FeeAmount { get; set; } = 5;

    public string AppointmentRequest { get; set; }

    [ForeignKey("PatientId")]
    public virtual Patient Patient { get; set; }

    [ForeignKey("DoctorId")]
    public virtual Doctor Doctor { get; set; }
}
