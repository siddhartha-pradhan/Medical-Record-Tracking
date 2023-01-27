using Silverline.Core.Entities.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Silverline.Core.Entities;

public class DiagnosticTest : BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public float InitialRange { get; set; }

    public float FinalRange { get; set; }

    public string Unit { get; set; }

    public float UnitPrice { get; set; }

    public Guid ClassId { get; set; }

    [ForeignKey("ClassId ")]
    public  virtual TestType TestType { get; set; }

    public ICollection<TestDetail> TestDetails { get; set; }

    public ICollection<TestCart> TestCarts { get; set; }


}
