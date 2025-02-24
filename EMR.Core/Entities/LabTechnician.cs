using System.ComponentModel.DataAnnotations.Schema;

namespace Silverline.Core.Entities;

public class LabTechnician
{
    public Guid Id { get; set; }

    public string UserId { get; set; }

    public string CertificateNumber { get; set; }

    public string HighestMedicalDegree { get; set; }

    public string ResumeURL { get; set; }

    public string CertificationURL { get; set; }

    public bool IsApproved { get; set; } = false;

    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}
