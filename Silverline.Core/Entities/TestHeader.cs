using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Silverline.Core.Entities;

public class TestHeader
{
    [Key]
    public Guid Id { get; set; }

    public Guid PatientId { get; set; }

    public string Status { get; set; } = Constants.Constants.Booked;

    public DateTime OrderedDate { get; set; } = DateTime.Now;

    public string PaymentStatus { get; set; } = Constants.Constants.Pending;

    public float TotalAmount { get; set; }

    [ForeignKey("PatientId")]
    public virtual Patient? Patient { get; set; }

    public virtual ICollection<TestDetail>? TestDetails { get; set; }
}
