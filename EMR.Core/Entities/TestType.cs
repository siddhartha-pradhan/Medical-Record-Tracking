using EMR.Core.Entities.Shared;

namespace EMR.Core.Entities;

public class TestType() : BaseEntity<Guid>(Guid.NewGuid())
{
    public string Name { get; set; } = string.Empty;
}