using System.ComponentModel.DataAnnotations;

namespace Silverline.Core.Entities;

public class TestType
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; }
}
