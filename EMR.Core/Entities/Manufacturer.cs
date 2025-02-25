using EMR.Core.Entities.Shared;
using System.ComponentModel.DataAnnotations;

namespace EMR.Core.Entities;

public class Manufacturer : BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    [Display(Name = "Activation Status")]
    public bool IsActive { get; set; } = true;

    public virtual ICollection<Medicine>? Medicines { get; set; }
}
