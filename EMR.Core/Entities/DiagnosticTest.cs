using EMR.Core.Entities.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMR.Core.Entities;

public class DiagnosticTest : BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Display(Name = "Initial Range")]
    public float InitialRange { get; set; }

    [Display(Name = "Final Range")]
    public float FinalRange { get; set; }

    public string Unit { get; set; } = string.Empty;

    [Display(Name = "Unit Price")]
    public float UnitPrice { get; set; }

    [Display(Name = "Test Type")]
    public Guid ClassId { get; set; }

    [ForeignKey("ClassId ")]
    public  virtual TestType? TestType { get; set; }

    public virtual ICollection<TestDetail>? TestDetails { get; set; }

    public virtual ICollection<TestCart>? TestCarts { get; set; }


}
