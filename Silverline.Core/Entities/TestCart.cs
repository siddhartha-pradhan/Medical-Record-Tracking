using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Silverline.Core.Entities;

public class TestCart
{
    [Key]
    public Guid Id { get; set; }

    public Guid PatientId { get; set; }

    public Guid TestId { get; set; }


    [ForeignKey("PatientId")]
    public virtual Patient Patient { get; set; }

    [ForeignKey("TestId")]
    public virtual DiagnosticTest DiagnosticTest { get; set; }
}
