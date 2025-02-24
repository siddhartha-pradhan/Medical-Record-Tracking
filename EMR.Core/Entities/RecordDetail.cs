using System.ComponentModel.DataAnnotations.Schema;

namespace EMR.Core.Entities;

public class RecordDetail
{
    public Guid Id { get; set; }

    public Guid RecordId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime DateOfAppointment { get; set; }

    [ForeignKey("RecordId")]
    public virtual RecordHeader RecordHeader { get; set; }
}
