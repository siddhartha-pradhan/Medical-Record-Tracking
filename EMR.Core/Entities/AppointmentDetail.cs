using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMR.Core.Entities;

public class AppointmentDetail
{
    public Guid Id { get; set; }    

    public Guid AppointmentId { get; set; }

    [Display(Name = "Diagnostic Title")]
    public string AppointmentTitle { get; set;}

    [Display(Name = "Diagnostic Description")]
    public string AppointmentDescription { get; set; }

    [ForeignKey("AppointmentId")]
    public Appointment? Appointment { get; set; }

    public virtual List<MedicationTreatment>? MedicalTreatments { get; set; }

    public virtual List<LaboratoryDiagnosis>? LaboratoryDiagnosis { get; set; }

}
