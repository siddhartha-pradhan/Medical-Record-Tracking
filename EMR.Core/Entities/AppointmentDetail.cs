using EMR.Core.Entities.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMR.Core.Entities;

public class AppointmentDetail() : BaseEntity<Guid>(Guid.NewGuid())
{
    [ForeignKey(nameof(Appointment))]
    public Guid AppointmentId { get; set; }

    public string AppointmentTitle { get; set;} = string.Empty;

    public string AppointmentDescription { get; set; } = string.Empty;

    public Appointment? Appointment { get; set; }

    public virtual List<MedicationTreatment>? MedicalTreatments { get; set; }

    public virtual List<LaboratoryDiagnosis>? LaboratoryDiagnosis { get; set; }

}
