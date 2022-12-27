using System.ComponentModel.DataAnnotations;

namespace Silverline.Core.Entities;

public class Specialty
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; }
}
