using EMR.Core.Entities.Shared;

namespace EMR.Core.Entities;

public class Specialty() : BaseEntity<Guid>(Guid.NewGuid())
{
    public string Name { get; set; } = string.Empty;

    public virtual ICollection<MedicalOfficer>? Doctors { get; set; }
}
