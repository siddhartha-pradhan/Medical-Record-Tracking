using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMR.Core.Entities;

public class TestDetail
{
    [Key]
    public Guid Id { get; set; }

    public Guid TestHeaderId { get; set; }

    public Guid TestId { get; set; }

    public float Value { get; set; }

    public string Status { get; set; }

    public float Price { get; set; }

    [ForeignKey("TestHeaderId")]
    public virtual TestHeader TestHeader { get; set; }

    [ForeignKey("TestId")]
    public virtual DiagnosticTest DiagnosticTest { get; set; }
}
