using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMR.Core.Entities;

public class MedicalRecord
{
    [Key]
    public Guid Id { get; set; }

    public string Specialty { get; set; }

    public Guid PatientId { get; set; } 

    public string DoctorName { get; set; }  

    public string DateOfAppointment { get; set; }

    public string Title { get; set; }   

    public string Description { get; set; } 

    public string Medicines { get; set; }

    public string LaboratoryTests { get; set; }

    [ForeignKey("PatientId")]
    public Patient Patient { get; set; }
}
