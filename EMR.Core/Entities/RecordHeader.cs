using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Silverline.Core.Entities;

public class RecordHeader
{
    [Key]
    public Guid Id { get ; set; }   

    public Guid PatientId { get; set; }

    public string Title { get; set; }

    [ForeignKey("PatientId")]
    public virtual Patient Patient { get; set; }
}
