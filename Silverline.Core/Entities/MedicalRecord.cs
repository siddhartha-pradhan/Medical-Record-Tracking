using System.ComponentModel.DataAnnotations;

namespace Silverline.Core.Entities;

public class MedicalRecord
{
    [Key]
    public Guid Id { get; set; }

    public string Specialty { get; set; }

    public Guid PatientId { get; set; } 

    public Guid DoctorId { get; set; }

    public string DoctorName { get; set; }  

    public string DateOfAppointment { get; set; } = DateTime.Now.ToString("dd/MMM/YYYY");

    public string Title { get; set; }   

    public string Description { get; set; } 

    public string Medicines { get; set; }

    public string LaboratoryTests { get; set; } 
}
