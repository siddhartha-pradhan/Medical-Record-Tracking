using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Silverline.Core.Entities;

public class MedicationTreatment
{
    [Key]
    public Guid Id { get; set; }

    public Guid MedicineId { get; set; }

    public Guid ReferralId { get; set; }

    public string Status { get; set; }

    public string Dose { get; set; }

    public bool IsCompleted { get; set; }

    public int TimePeriod { get; set; }  
    
    public string TimeFormat { get; set; }
    
    public string Remarks { get; set; }

    public Guid PharmacistId { get; set; }

    [ForeignKey("PharmacistId")]
    public virtual Pharmacist? Pharmacist { get; set; }

    [ForeignKey("MedicineId")]
    public virtual Medicine Medicine { get; set; }

    [ForeignKey("ReferralId")]
    public virtual AppointmentDetail AppointmentDetail { get; set; }
}
