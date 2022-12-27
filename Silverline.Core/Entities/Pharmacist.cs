namespace Silverline.Core.Entities;

public class Pharmacist
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string CertificateNumber { get; set; }

    public string HighestMedicalDegree { get; set; }
}
