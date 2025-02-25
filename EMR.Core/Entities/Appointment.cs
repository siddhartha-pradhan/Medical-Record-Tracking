using EMR.Core.Entities.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMR.Core.Entities;

public class Appointment() : BaseEntity<Guid>(Guid.NewGuid())
{
    [ForeignKey(nameof(Patient))]
    public Guid PatientId { get; set; } 

    [ForeignKey(nameof(Doctor))]
    public Guid DoctorId { get; set; }

    public DateTime BookedDate { get; set; } = DateTime.Now;

    public DateTime DateOfAppointment { get; set; }

    public string AppointmentStatus { get; set; } = Constants.Constants.Booked;

    public string PaymentStatus { get; set; } = Constants.Constants.Pending;

    public float FeeAmount { get; set; } = 0;

    public string AppointmentRequest { get; set; } = string.Empty;

    public DateTime StartTime { get; set; }

    public DateTime FinalizedTime { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual MedicalOfficer? Doctor { get; set; }
}
