using Silverline.Core.Entities.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Silverline.Core.Entities;

public class Medicine : BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    [Display(Name = "Unit Price")]
    public float UnitPrice { get; set; }

    [Required]
    public string Type { get; set; }

    [Display(Name = "Image")]
    public string? ImageURL { get; set; }

    [Required]
    [Display(Name = "Manufacturer")]
    public Guid ManufacturerId { get; set; }    

    [Required]
    [Display(Name = "Category")]
    public Guid CategoryId { get; set; }

    [ForeignKey("ManufacturerId")]
    public virtual Manufacturer? Manufacturer { get; set; }

    [ForeignKey("CategoryId")]
    public virtual Category? Category { get; set; }

    public virtual ICollection<MedicineCart>? MedicineCarts { get; set; }

    public virtual ICollection<MedicationTreatment>? MedicationTreatments { get; set; }

    public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
}
