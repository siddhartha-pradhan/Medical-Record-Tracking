using EMR.Core.Entities.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMR.Core.Entities;

public class TestDetail() : BaseEntity<Guid>(Guid.NewGuid())
{
    [ForeignKey(nameof(TestHeader))]
    public Guid TestHeaderId { get; set; }

    [ForeignKey(nameof(DiagnosticTest))]
    public Guid TestId { get; set; }

    public float Value { get; set; }

    public string Status { get; set; } = Constants.Constants.Ongoing;

    public float Price { get; set; }

    public virtual TestHeader? TestHeader { get; set; }

    public virtual DiagnosticTest? DiagnosticTest { get; set; }
}
