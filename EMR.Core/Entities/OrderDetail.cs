using EMR.Core.Entities.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMR.Core.Entities;

public class OrderDetail() : BaseEntity<Guid>(Guid.NewGuid())
{
    [ForeignKey(nameof(MedicalOfficer))]
    public Guid ApprovedOfficer { get; set; }
    
    [ForeignKey(nameof(OrderHeader))]
    public Guid OrderId { get; set; }

    [ForeignKey(nameof(Medicine))]
    public Guid MedicineId { get; set; }

    public int Count { get; set; }

    public float Price { get; set; }

    public virtual OrderHeader? OrderHeader { get; set; }

    public virtual Medicine? Medicine { get; set; }

    public virtual MedicalOfficer? MedicalOfficer { get; set; }
}