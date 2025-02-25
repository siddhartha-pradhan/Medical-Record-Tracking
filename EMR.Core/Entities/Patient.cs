using EMR.Core.Entities.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMR.Core.Entities;

public class Patient() : BaseEntity<Guid>(Guid.NewGuid())
{
    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string? Street { get; set; } = string.Empty;
    
    public string? Address { get; set; } = string.Empty;

    public int CreditPoints { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<TestCart>? TestCarts { get; set; }

    public virtual ICollection<TestHeader>? TestHeaders { get; set; }

    public virtual ICollection<OrderHeader>? OrderHeaders { get; set; }
    
    public virtual ICollection<RecordHeader>? RecordHeaders { get; set; }

    public virtual ICollection<MedicineCart>? MedicineCarts { get; set; }

    public virtual ICollection<Appointment>? Appointments { get; set; }
    
    public virtual ICollection<MedicationTreatment>? MedicationTreatments { get; set; }
}
