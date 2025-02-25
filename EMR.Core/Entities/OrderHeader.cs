using EMR.Core.Entities.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMR.Core.Entities;

public class OrderHeader() : BaseEntity<Guid>(Guid.NewGuid())
{
    [ForeignKey(nameof(Patient))]
    public Guid PatientId { get; set; }

    public DateTime OrderedDate { get; set; }

    public string PaymentStatus { get; set; } = string.Empty;

    public float TotalAmount { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
}
