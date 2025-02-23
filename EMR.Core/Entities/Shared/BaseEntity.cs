namespace Silverline.Core.Entities.Shared;

public class BaseEntity
{
    public DateTime CreatedAt { get; set; }

    public DateTime? LastModifiedAt { get; set; }
    
    public bool IsDeleted { get; set; } = false;
}
