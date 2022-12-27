using System.ComponentModel.DataAnnotations;

namespace Silverline.Core.Entities;

public class Category
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; }
}
