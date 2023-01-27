using Silverline.Core.Entities.Shared;
using System.ComponentModel.DataAnnotations;

namespace Silverline.Core.Entities;

public class Category : BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; }

    public ICollection<Medicine> Medicines { get; set; }

}
