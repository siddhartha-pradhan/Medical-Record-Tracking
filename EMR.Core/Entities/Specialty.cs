using EMR.Core.Entities.Shared;
using System.ComponentModel.DataAnnotations;

namespace EMR.Core.Entities;

public class Specialty : BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Doctor>? Doctors { get; set; }
}
