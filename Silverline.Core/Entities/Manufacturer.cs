using Silverline.Core.Entities.Shared;
using System.ComponentModel.DataAnnotations;

namespace Silverline.Core.Entities;

public class Manufacturer : BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Location { get; set; }

    public bool IsActive { get; set; } = true;

    public virtual ICollection<Medicine>? Medicines { get; set; }
}
