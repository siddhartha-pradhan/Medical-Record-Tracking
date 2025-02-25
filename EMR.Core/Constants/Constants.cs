namespace EMR.Core.Constants;

public class Constants
{
    #region Roles
    public const string Admin = "Admin";
    public const string Patient = "Patient";
    public const string Doctor = "Doctor";
    public const string LabTechnician = "Lab Technician";
    public const string Pharmacist = "Pharmacist";
    #endregion

    #region Status
    public const string Ongoing = "Ongoing";
    public const string Booked = "Booked";
    public const string Continued = "Continued";
    public const string Completed = "Completed";

    public const string Pending = "Pending";
    public const string Approved = "Approved";
    #endregion
    
    public abstract class Roles
    {
        public const string Admin = "Admin";
        public const string Doctor = "Doctor";
        public const string Patient = "Patient";
        public const string Pharmacist = "Pharmacist";
        public const string LabTechnician = "Lab Technician";
    }
    
    public abstract class DbProviderKeys
    {
        public const string SqlServer = "mssql";
        public const string Npgsql = "postgresql";
    }
    
    private abstract class FolderPath
    {
        public const string Images = "images";
        public const string Documents = "documents";
        public const string EmailTemplates = "email-templates";
    }

    public abstract class FilePath
    {
        public const string UsersImagesFilePath = $"{FolderPath.Images}/user-images/";
        
        public const string ResumeDocumentsFilePath = $"{FolderPath.Documents}/resumes/";
        public const string CertificationDocumentsFilePath = $"{FolderPath.Documents}/certifications/";
        
        public const string EmailTemplateFilePath = $"{FolderPath.EmailTemplates}/";
    }
    
    public abstract class Cookie
    {
        public const string TokenPayload = "X-EMR-Token-Payload";
        public const string TokenSignature = "X-EMR-Token-Signature";
        public const string TokenExpiration = "X-EMR-Token-Expiration";
    }
}
