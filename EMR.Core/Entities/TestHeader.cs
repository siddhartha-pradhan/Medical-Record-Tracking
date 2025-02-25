using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EMR.Core.Entities.Shared;

namespace EMR.Core.Entities;

public class TestHeader() : BaseEntity<Guid>(Guid.NewGuid())
{
    [ForeignKey(nameof(Patient))]
    public Guid PatientId { get; set; }
    
    public float TotalAmount { get; set; }

    public DateTime OrderedDate { get; set; } = DateTime.Now;

    public string Status { get; set; } = Constants.Constants.Booked;

    public string PaymentStatus { get; set; } = Constants.Constants.Pending;

    public virtual Patient? Patient { get; set; }

    public virtual ICollection<TestDetail>? TestDetails { get; set; }
}