using System.ComponentModel.DataAnnotations;

namespace Silverline.Core.Entities;

public class Patient
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime DateOfBirth { get; set; }   

    public string Street { get; set; }
    
    public string Address { get; set; }

    public string CreditPoints { get; set; }
}
