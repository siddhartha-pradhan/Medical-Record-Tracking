using Microsoft.AspNetCore.Mvc.Rendering;
using EMR.Core.Entities;

namespace EMR.Core.ViewModels;

public class EmergencyAppointmentViewModel
{
	public Guid PatientId { get; set; }

	public Appointment Appointment { get; set; }

	public AppointmentDetail AppointmentDetail { get; set; }

	public List<SelectListItem> MedicineList { get; set; }

	public List<SelectListItem> LaboratoryTestList { get; set; }
}
