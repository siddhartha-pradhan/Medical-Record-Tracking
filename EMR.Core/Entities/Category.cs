using EMR.Core.Entities.Shared;
using System.ComponentModel.DataAnnotations;

namespace EMR.Core.Entities;

public class Category : BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Medicine>? Medicines { get; set; }
}
