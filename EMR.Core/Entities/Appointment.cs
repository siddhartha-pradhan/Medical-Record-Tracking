using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMR.Core.Entities;

public class Appointment
{
    [Key]
    public Guid Id { get; set; }

    public Guid PatientId { get; set; } 

    public Guid DoctorId { get; set; }

    public DateTime BookedDate { get; set; } = DateTime.Now;

    [Display(Name = "Date of Appointment")]
    public DateTime DateOfAppointment { get; set; }

    public string AppointmentStatus { get; set; } = Constants.Constants.Booked;

    public string PaymentStatus { get; set; } = Constants.Constants.Pending;

    public float FeeAmount { get; set; } = 0;

    [Display(Name = "Appointment Request")]
    public string AppointmentRequest { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime FinalizedTime { get; set; }

    [ForeignKey("PatientId")]
    public virtual Patient? Patient { get; set; }

    [ForeignKey("DoctorId")]
    public virtual Doctor? Doctor { get; set; }
}
