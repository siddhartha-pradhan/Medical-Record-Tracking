using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMR.Core.Entities;

public class RecordHeader
{
    [Key]
    public Guid Id { get ; set; }   

    public Guid PatientId { get; set; }

    public string Title { get; set; }

    [ForeignKey("PatientId")]
    public virtual Patient Patient { get; set; }
}
