using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Silverline.Core.Entities;

public class MedicationTreatment
{
    [Key]
    public Guid Id { get; set; }

    public Guid PatientId { get; set; }

    public Guid MedicineId { get; set; }

    public string Dose { get; set; }

    public bool IsCompleted { get; set; }

    public int TimePeriod { get; set; }  
    
    public string TimeFormat { get; set; }

    public Guid ApprovedBy { get; set; }

    [ForeignKey("PatientId")]
    public  virtual Patient Patient { get; set; }

    [ForeignKey("MedicineId")]
    public virtual Medicine Medicine { get; set; }

    [ForeignKey("ApprovedBy")]
    public virtual Doctor Doctor { get; set; }
}
