using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Silverline.Core.Entities;

public class OrderDetail
{
    public Guid OrderId { get; set; }

    public Guid MedicineId { get; set; }

    public int Count { get; set; }

    public float Price { get; set; }

    public Guid ApprovedBy { get; set; }

    [ForeignKey("OrderId")]
    public OrderHeader OrderHeader { get; set; }

    [ForeignKey("MedicineId")]
    public Medicine Medicine { get; set; }

    [ForeignKey("ApprovedBy")]
    public Doctor Doctor { get; set; }
}
