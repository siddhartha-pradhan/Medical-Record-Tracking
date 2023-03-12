using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Silverline.Application.Interfaces.Services;
using Silverline.Core.Constants;
using Silverline.Core.ViewModels;
using Silverline.Infrastructure.Implementation.Services;
using System.Security.Claims;

namespace Silverline.Areas.Doctor.Controllers;

[Area("Doctor")]
public class AppointmentController : Controller
{
    private readonly IAppointmentService _appointmentService;
    private readonly IAppUserService _userService;
    private readonly IDoctorService _doctorService;
    private readonly IPatientService _patientService;
    private readonly IMedicineService _medicineService;
    private readonly ITestService _testService;

    public AppointmentController(
        IAppointmentService appointmentService, 
        IAppUserService userService, 
        IDoctorService doctorService, 
        IPatientService patientService, 
        IMedicineService medicineService,
        ITestService testService)
    {
        _userService = userService;
        _appointmentService = appointmentService;
        _doctorService = doctorService;
        _patientService = patientService;
        _medicineService = medicineService;
		_testService = testService;

	}

    #region Razor Pages
    public IActionResult Index()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        var doctor = _doctorService.GetAllDoctors().Where(x => x.UserId == claim.Value).FirstOrDefault();

        var appointments = _appointmentService.GetAllBookedAppointment(doctor.Id);

        var appoint = new List<AppointmentsViewModel>();

        foreach(var appointment in appointments)
        {
            var userId = _patientService.GetPatient(appointment.PatientId).UserId;
            var user = _userService.GetUser(userId);

            appoint.Add(new AppointmentsViewModel()
            {
                AppointmentId = appointment.Id,
                PatientImage = user.ProfileImage,
                PatientName = user.FullName,
                PatientRequest = appointment.AppointmentRequest
            }); ; 
        }

        return View(appoint);
    }

    public IActionResult Start(Guid Id)
    {
        var appointment = _appointmentService.GetAppointment(Id);
        var patient = _patientService.GetPatient(appointment.PatientId);
        var user = _userService.GetUser(patient.UserId);
        var medicines = _medicineService.GetAllMedicines()
            .Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
		var tests = _testService.GetAllDiagnosticTests()
			.Select(x => new SelectListItem()
			{
				Text = x.Title,
				Value = x.Id.ToString()
			});


		var appointmentVM = new AppointmentDetailViewModel()
        {
            Appointment = new Core.Entities.AppointmentDetail()
            {
                AppointmentId = Id,
                MedicalTreatments = new(),
                LaboratoryDiagnosis = new()
            },
            MedicineList = medicines.ToList(),
            LaboratoryTestList = tests.ToList(),
            PatientAge = DateTime.Today.Year - patient.DateOfBirth.Year,
            PatientName = user.FullName,
        };

        appointmentVM.Appointment.MedicalTreatments.Add(new() { });
        appointmentVM.Appointment.LaboratoryDiagnosis.Add(new() { });

		return View(appointmentVM);
    }
    #endregion

    #region API Calls
    [HttpPost]
	public IActionResult Start(AppointmentDetailViewModel appointmentVm)
    {
        return View();
    }
	#endregion
}
