namespace EMR.Core.ViewModels;

public class MedicalRecordsViewModel
{
    public string UserId { get; set; }

    public string Name { get; set; }

    public List<MedicalRecordViewModel> MedicalRecords { get; set; }    
}
