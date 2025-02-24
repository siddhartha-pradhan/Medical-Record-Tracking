using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMR.Core.Entities;

public class OrderDetail
{
    [Key]
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public Guid MedicineId { get; set; }

    public int Count { get; set; }

    public float Price { get; set; }

    public Guid ApprovedBy { get; set; }

    [ForeignKey("OrderId")]
    public virtual OrderHeader OrderHeader { get; set; }

    [ForeignKey("MedicineId")]
    public virtual Medicine Medicine { get; set; }

    [ForeignKey("ApprovedBy")]
    public virtual Doctor Doctor { get; set; }
}
