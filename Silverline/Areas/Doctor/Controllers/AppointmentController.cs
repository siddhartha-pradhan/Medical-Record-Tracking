using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Silverline.Core.Constants;
using Silverline.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Silverline.Application.Interfaces.Services;

namespace Silverline.Areas.Doctor.Controllers;

[Area("Doctor")]
[Authorize(Roles = Constants.Doctor)]
public class AppointmentController : Controller
{
    private readonly IAppointmentService _appointmentService;
    private readonly IAppointmentDetailService _appointmentDetailService;
    private readonly IAppUserService _userService;
    private readonly IDoctorService _doctorService;
    private readonly ISpecialtyService _specialtyService;
    private readonly IPatientService _patientService;
    private readonly IMedicineService _medicineService;
    private readonly IMedicalRecordService _medicalRecordService;
    private readonly ITestService _testService;

    public AppointmentController(
        IAppointmentService appointmentService, 
        IAppointmentDetailService appointmentDetailService,
        IAppUserService userService, 
        IDoctorService doctorService,
        ISpecialtyService specialtyService,
        IPatientService patientService, 
        IMedicineService medicineService,
        ITestService testService)
    {
        _userService = userService;
        _appointmentService = appointmentService;
        _appointmentDetailService = appointmentDetailService;
        _doctorService = doctorService;
        _patientService = patientService;
        _medicineService = medicineService;
        _specialtyService = specialtyService;
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
        _appointmentDetailService.FinalizeAppointment(appointmentVm.Appointment);

        var claimsIdentity = (ClaimsIdentity)User.Identity;

        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        var doctor = _doctorService.GetAllDoctors().Where(x => x.UserId == claim.Value).FirstOrDefault();

        var docUser = _userService.GetUser(doctor.UserId);

        var specialty = _specialtyService.GetSpecialty(doctor.DepartmentId);

        var appointment = _appointmentService.GetAppointment(appointmentVm.Appointment.AppointmentId);

        var patient = _patientService.GetPatient(appointment.PatientId);

        var medicines = new List<string>();

        var medicalTreatments = appointmentVm.Appointment.MedicalTreatments;

        for (int i = 0; i < medicalTreatments.Count(); i++)
        {
            var medicine = medicalTreatments[i].MedicineId;
            var result = _medicineService.GetMedicine(medicine);
            var meds = $"{result.Name}: {medicalTreatments[i].Dose}";
            medicines.Add(meds);
        }

        _medicalRecordService.AddMedicalRecord(new Core.Entities.MedicalRecord
        {
            PatientId = patient.Id,
            DoctorId = doctor.Id,
            Specialty = specialty.Name,
            DoctorName = docUser.FullName,
            Title = appointmentVm.Appointment.AppointmentTitle,
            Description = appointmentVm.Appointment.AppointmentDescription,
            Medicines = string.Join(", ", medicines)
        }); 

        return RedirectToAction("Index");
    }
	#endregion
}
