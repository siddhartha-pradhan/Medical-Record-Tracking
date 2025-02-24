using EMR.Core.Entities.Shared;
using System.ComponentModel.DataAnnotations;

namespace EMR.Core.Entities;

public class TestType : BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; }
}
