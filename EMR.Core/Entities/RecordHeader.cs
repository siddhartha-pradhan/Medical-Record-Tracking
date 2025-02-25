using EMR.Core.Entities.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMR.Core.Entities;

public class RecordHeader() : BaseEntity<Guid>(Guid.NewGuid())
{
    [ForeignKey(nameof(Patient))]
    public Guid PatientId { get; set; }

    public string Title { get; set; } = string.Empty;

    public virtual Patient? Patient { get; set; }
}
