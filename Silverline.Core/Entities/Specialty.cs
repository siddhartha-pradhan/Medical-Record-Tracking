using Silverline.Core.Entities.Shared;
using System.ComponentModel.DataAnnotations;

namespace Silverline.Core.Entities;

public class Specialty : BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; }

    public ICollection<Doctor> Doctors { get; set; }
}
