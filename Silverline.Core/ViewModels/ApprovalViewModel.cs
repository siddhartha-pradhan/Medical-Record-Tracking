namespace Silverline.Core.ViewModels;

public class ApprovalViewModel
{
    public string UserId { get; set; }  

    public Guid StaffId { get; set; }

    public string FullName { get; set; }

    public string EmailAddress { get; set; }    

    public string PhoneNumber { get; set; }

    public byte[] ProfileImage { get; set; }

    public string? Department { get; set; }

    public string HighestMedicalDegree { get; set; }    

    public string ResumeURL { get; set; }   

    public string CertificationURL { get; set; }

    public string CertificationNumber { get; set; }
}
