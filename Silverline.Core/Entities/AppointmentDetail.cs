using System.ComponentModel.DataAnnotations.Schema;

namespace Silverline.Core.Entities;

public class AppointmentDetail
{
    public Guid Id { get; set; }    

    public Guid AppointmentId { get; set; }
    
    public string AppointmentTitle { get; set;}

    public string AppointmentDescription { get; set; }

    [ForeignKey("AppointmentId")]
    public Appointment? Appointment { get; set; }

    public virtual ICollection<MedicationTreatment>? MedicalTreatments { get; set; }

    public virtual ICollection<LaboratoryDiagnosis>? LaboratoryDiagnosis { get; set; }

}
