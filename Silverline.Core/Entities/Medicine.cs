using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Silverline.Core.Entities;

public class Medicine
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public float UnitPrice { get; set; }

    public Guid ManufacturerId { get; set; }    

    public Guid CategoryId { get; set; }

    [ForeignKey("ManufacturerId")]
    public virtual Manufacturer Manufacturer { get; set; }

    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; }

    public ICollection<MedicineCart> MedicineCarts { get; set; }

    public ICollection<MedicationTreatment> MedicationTreatments { get; set; }

    public ICollection<OrderDetail> OrderDetails { get; set; }
}
