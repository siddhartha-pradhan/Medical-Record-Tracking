using EMR.Core.Entities.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMR.Core.Entities;

public class Medicine() : BaseEntity<Guid>(Guid.NewGuid())
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public float UnitPrice { get; set; }

    public string Type { get; set; } = string.Empty;

    public string? Image { get; set; } = string.Empty;

    [ForeignKey(nameof(Manufacturer))]
    public Guid ManufacturerId { get; set; }    

    [ForeignKey(nameof(Category))]
    public Guid CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Manufacturer? Manufacturer { get; set; }

    public virtual ICollection<OrderDetail>? OrderDetails { get; set; }

    public virtual ICollection<MedicineCart>? MedicineCarts { get; set; }

    public virtual ICollection<MedicationTreatment>? MedicationTreatments { get; set; }
}