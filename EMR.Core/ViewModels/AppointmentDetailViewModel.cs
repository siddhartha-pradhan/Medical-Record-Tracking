using Microsoft.AspNetCore.Mvc.Rendering;
using Silverline.Core.Entities;

namespace Silverline.Core.ViewModels;

public class AppointmentDetailViewModel
{
    public AppointmentDetail Appointment { get; set; }

    public string PatientName { get; set; }

    public int PatientAge { get; set; }

    public List<SelectListItem> MedicineList { get; set; }

	public List<SelectListItem> LaboratoryTestList { get; set; }

}
