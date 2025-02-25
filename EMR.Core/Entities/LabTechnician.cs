using System.ComponentModel.DataAnnotations.Schema;

namespace EMR.Core.Entities;

public class LabTechnician
{
    public Guid Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public string CertificateNumber { get; set; } = string.Empty;

    public string HighestMedicalDegree { get; set; } = string.Empty;

    public string ResumeURL { get; set; } = string.Empty;

    public string CertificationURL { get; set; } = string.Empty;

    public bool IsApproved { get; set; } = false;

    [ForeignKey("UserId")]
    public virtual User? User { get; set; }
}
