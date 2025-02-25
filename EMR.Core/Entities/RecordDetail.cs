using EMR.Core.Entities.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMR.Core.Entities;

public class RecordDetail() : BaseEntity<Guid>(Guid.NewGuid())
{
    [ForeignKey(nameof(RecordHeader))]
    public Guid RecordId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime DateOfAppointment { get; set; }

    public virtual RecordHeader? RecordHeader { get; set; }
}
