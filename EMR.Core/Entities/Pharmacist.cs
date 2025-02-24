using System.ComponentModel.DataAnnotations.Schema;

namespace EMR.Core.Entities;

public class Pharmacist
{
    public Guid Id { get; set; }

    public string UserId { get; set; }

    public string CertificateNumber { get; set; }

    public string HighestMedicalDegree { get; set; }

    public string ResumeURL { get; set; }

    public string CertificationURL { get; set; }

    public bool IsApproved { get; set; } = false;

    [ForeignKey("UserId")]
    public virtual User? AppUser { get; set; }
}
