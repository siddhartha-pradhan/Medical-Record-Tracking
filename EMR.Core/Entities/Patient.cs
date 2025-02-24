using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMR.Core.Entities;

public class Patient
{
    [Key]
    public Guid Id { get; set; }

    public string UserId { get; set; }

    public DateTime DateOfBirth { get; set; }   

    public string Street { get; set; }
    
    public string Address { get; set; }

    public int CreditPoints { get; set; }

    [ForeignKey("UserId")]
    public virtual User? AppUser { get; set; }

    public virtual ICollection<MedicationTreatment>? MedicationTreatments { get; set; }

    public virtual ICollection<MedicineCart>? MedicineCarts { get; set; }

    public virtual ICollection<TestCart>? TestCarts { get; set; }

    public virtual ICollection<TestHeader>? TestHeaders { get; set; }

    public virtual ICollection<RecordHeader>? RecordHeaders { get; set; }

    public virtual ICollection<OrderHeader>? OrderHeaders { get; set; }

    public virtual ICollection<Appointment>? Appointments { get; set; }
}
