using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Silverline.Core.Entities;

public class Patient
{
    [Key]
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public DateTime DateOfBirth { get; set; }   

    public string Street { get; set; }
    
    public string Address { get; set; }

    public string CreditPoints { get; set; }

    [ForeignKey("UserId")]
    public virtual AppUser AppUser { get; set; }

    public ICollection<MedicationTreatment> MedicationTreatments { get; set; }

    public ICollection<MedicineCart> MedicineCarts { get; set; }

    public ICollection<TestCart> TestCarts { get; set; }

    public ICollection<TestHeader> TestHeaders { get; set; }

    public ICollection<RecordHeader> RecordHeaders { get; set; }

    public ICollection<OrderHeader> OrderHeaders { get; set; }

    public ICollection<Appointment> Appointments { get; set; }
}
