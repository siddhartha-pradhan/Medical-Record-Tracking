using Silverline.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Silverline.Core.ViewModels;

public class DiagnosisDetailViewModel
{
    [Display(Name = "Test Title")]
    public string TestName { get; set; }

    [Display(Name = "Test Range")]
    public string TestRange { get; set; }

    public string Unit { get; set; }

    public Guid DoctorId { get; set; }

    [Display(Name = "Referred Doctor")]
    public string DoctorName { get; set; }

    public Guid PatientId { get; set; }

    public string PatientImage { get; set; }

    public string PatientName { get; set; }

    public LaboratoryDiagnosis LaboratoryDiagnosis { get; set; }
}
