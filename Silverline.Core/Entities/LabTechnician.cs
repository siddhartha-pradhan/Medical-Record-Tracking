using System.ComponentModel.DataAnnotations.Schema;

namespace Silverline.Core.Entities;

public class LabTechnician
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string CertificateNumber { get; set; }

    public string HighestMedicalDegree { get; set; }

    [ForeignKey("UserId")]
    public virtual AppUser AppUser { get; set; }
}
