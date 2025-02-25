using EMR.Core.Entities.Shared;

namespace EMR.Core.Entities;

public class Category() : BaseEntity<Guid>(Guid.Empty)
{
    public string Name { get; set; } = string.Empty;

    public virtual ICollection<Medicine>? Medicines { get; set; }
}
