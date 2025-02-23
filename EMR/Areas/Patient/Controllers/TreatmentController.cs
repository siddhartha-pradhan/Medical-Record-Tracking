using Microsoft.AspNetCore.Mvc;
using Silverline.Application.Interfaces.Services;
using Silverline.Core.Constants;
using Silverline.Core.ViewModels;
using System.Security.Claims;

namespace Silverline.Areas.Patient.Controllers;

[Area("Patient")]
public class TreatmentController : Controller
{
	private readonly IPatientService _patientService;
	private readonly IAppUserService _appUserService;
	private readonly IAppointmentService _appointmentService;
	private readonly IAppointmentDetailService _appointmentDetailService;
	private readonly ILabDiagnosisService _labDiagnosisService;
	private readonly IMedicalTreatmentService _medicalTreatmentService;
	private readonly ITestService _testService;
	private readonly IMedicineService _medicineService;
	private readonly IDoctorService _doctorService;

	public TreatmentController(IPatientService patientService, 
		IAppUserService appUserService, 
		IAppointmentService appointmentService, 
		IAppointmentDetailService appointmentDetailService, 
		ILabDiagnosisService labDiagnosisService, 
		IMedicalTreatmentService medicalTreatmentService,
		ITestService testService,
		IMedicineService medicineService,
		IDoctorService doctorService)
	{
		_patientService = patientService;
		_appUserService = appUserService;
		_appointmentService = appointmentService;
		_appointmentDetailService = appointmentDetailService;
		_labDiagnosisService = labDiagnosisService;
		_medicalTreatmentService = medicalTreatmentService;
		_testService = testService;
		_medicineService = medicineService;
		_doctorService = doctorService;
	}

	public IActionResult LabDiagnosis()
	{
		var claimsIdentity = (ClaimsIdentity)User.Identity;

		var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

		var patient = _patientService.GetAllPatients().Where(x => x.UserId == claim.Value).FirstOrDefault();

		var appointments = _appointmentService.GetAllFinalizedAppointments().Where(x => x.PatientId == patient.Id).ToList();

		var appointmentDetails = _appointmentDetailService.GetAllAppointments();

		var tests = _testService.GetAllDiagnosticTests();

		var labDiagnosis = _labDiagnosisService.GetAllLabDiagnosis().Where(x => x.ActionStatus == Constants.Completed);

		var result = (from appointment in appointments
					  join appointmentDetail in appointmentDetails
					  on appointment.Id equals appointmentDetail.AppointmentId
					  join diagnosis in labDiagnosis
					  on appointmentDetail.Id equals diagnosis.ReferralId
					  join test in tests
					  on diagnosis.TestId equals test.Id
					  select new LabTreatmentViewModel
					  {
						  TestName = test.Title,
						  DateOfTest = diagnosis.FinalizedDate?.ToString("dddd, dd MMMM yyyy"),
						  Remarks = diagnosis.TechnicianRemarks,
						  Range = $"{test.FinalRange} - {test.FinalRange} {test.Unit}",
						  Value = $"{diagnosis.Value} {test.Unit}",
						  ReferredBy = _appUserService.GetUser(_doctorService.GetDoctor(appointment.DoctorId).UserId).FullName
					  }).ToList();

		return View(result);
	}

	public IActionResult Medications()
	{
		var claimsIdentity = (ClaimsIdentity)User.Identity;

		var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

		var patient = _patientService.GetAllPatients().Where(x => x.UserId == claim.Value).FirstOrDefault();

		var appointments = _appointmentService.GetAllFinalizedAppointments().Where(x => x.PatientId == patient.Id).ToList();

		var appointmentDetails = _appointmentDetailService.GetAllAppointments();

		var medications = _medicineService.GetAllMedicines();

		var medicalTreatments = _medicalTreatmentService.GetMedicationTreatments().Where(x => x.ActionStatus == Constants.Completed).OrderBy(x => x.Status);

		var result = (from appointment in appointments
					  join appointmentDetail in appointmentDetails
					  on appointment.Id equals appointmentDetail.AppointmentId
					  join diagnosis in medicalTreatments
					  on appointmentDetail.Id equals diagnosis.ReferralId
					  join medicine in medications
					  on diagnosis.MedicineId equals medicine.Id
					  select new DiagnosisTreatmentViewModel
					  {
						  TreatmentId = diagnosis.Id,
						  MedicineName = medicine.Name,
						  Remarks = diagnosis.PharmacistRemarks,
						  Dose = diagnosis.Dose,
						  TimeFormat = diagnosis.TimeFormat,
						  TimePeriod = diagnosis.TimePeriod,
						  ReferredBy = _appUserService.GetUser(_doctorService.GetDoctor(appointment.DoctorId).UserId).FullName,
						  Status = diagnosis.Status
					  }).OrderByDescending(x => x.Status).ToList();

		return View(result);
	}

	public IActionResult Complete(Guid treatmentId)
	{
		_medicalTreatmentService.CompleteCourse(treatmentId);
		TempData["Success"] = "Medicational Course Successfully Completed";
		return RedirectToAction("Medications");
	}
}
