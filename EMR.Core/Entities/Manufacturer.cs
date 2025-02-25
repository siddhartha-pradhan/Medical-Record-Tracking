using EMR.Core.Entities.Shared;

namespace EMR.Core.Entities;

public class Manufacturer() : BaseEntity<Guid>(Guid.NewGuid())
{
    public string Name { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public virtual ICollection<Medicine>? Medicines { get; set; }
}