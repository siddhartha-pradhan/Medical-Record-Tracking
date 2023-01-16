using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Silverline.Core.Entities;

public class MedicineCart
{
    [Key]
    public Guid Id { get; set; }

    public Guid PatientId { get; set; }

    public Guid MedicineId { get; set; }

    public int Count { get; set; }

    public Guid ApprovedBy { get; set; }

    [ForeignKey("PatientId")]
    public virtual Patient Patient { get; set; }

    [ForeignKey("MedicineId")]
    public virtual Medicine Medicine { get; set; }

    [ForeignKey("ApprovedBy")]
    public virtual Doctor Doctor { get; set; }
}
