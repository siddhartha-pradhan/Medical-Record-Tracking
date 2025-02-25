using EMR.Core.Entities.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMR.Core.Entities;

public class MedicineCart() : BaseEntity<Guid>(Guid.NewGuid())
{
    [ForeignKey(nameof(MedicalOfficer))]
    public Guid ApprovedOfficer { get; set; }
    
    [ForeignKey(nameof(Patient))]
    public Guid PatientId { get; set; }

    [ForeignKey(nameof(Medicine))]
    public Guid MedicineId { get; set; }

    public int Count { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual Medicine? Medicine { get; set; }

    public virtual MedicalOfficer? MedicalOfficer { get; set; }
}