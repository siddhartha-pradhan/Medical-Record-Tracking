using EMR.Core.Entities.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMR.Core.Entities;

public class DiagnosticTest() : BaseEntity<Guid>(Guid.NewGuid())
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public float InitialRange { get; set; }

    public float FinalRange { get; set; }

    public string Unit { get; set; } = string.Empty;

    public float UnitPrice { get; set; }

    [ForeignKey(nameof(TestType))]
    public Guid ClassId { get; set; }

    public  virtual TestType? TestType { get; set; }

    public virtual ICollection<TestDetail>? TestDetails { get; set; }

    public virtual ICollection<TestCart>? TestCarts { get; set; }
}