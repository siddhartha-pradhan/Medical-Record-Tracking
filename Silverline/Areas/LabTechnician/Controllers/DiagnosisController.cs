using Microsoft.AspNetCore.Mvc;
using Silverline.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Silverline.Application.Interfaces.Services;
using Silverline.Core.ViewModels;
using Silverline.Core.Entities;
using Silverline.Application.Interfaces.Repositories;
using Silverline.Infrastructure.Implementation.Services;
using System.Security.Claims;

namespace Silverline.Areas.LabTechnician.Controllers;

[Area("LabTechnician")]
[Authorize(Roles = Constants.LabTechnician)]
public class DiagnosisController : Controller
{
	private readonly ITestService _testService;
	private readonly IPatientService _patientService;
	private readonly IDoctorService _doctorService;
	private readonly IAppointmentDetailService _appointmentDetailService;
	private readonly IAppointmentService _appointmentService;
	private readonly ILabDiagnosisService _labDiagnosisService;
	private readonly IAppUserService _appUserService;
	private readonly ILabTechnicianService _technicianService;

	public DiagnosisController(ITestService testService, 
		IPatientService patientService, 
		IDoctorService doctorService, 
		IAppointmentDetailService appointmentDetailService, 
		IAppointmentService appointmentService,
        ILabDiagnosisService labDiagnosisService,
        IAppUserService appUserService,
		ILabTechnicianService technicianService)
	{
		_testService = testService;
		_patientService = patientService;
		_doctorService = doctorService;
		_appointmentDetailService = appointmentDetailService;
		_appointmentService = appointmentService;
        _appUserService = appUserService;
		_labDiagnosisService = labDiagnosisService;
		_technicianService = technicianService;


	}

	private Appointment Appointment(Guid Id)
	{
		var appointmentDetail = _appointmentDetailService.GetAppointmentDetail(Id);

		var appointment = _appointmentService.GetAppointment(appointmentDetail.AppointmentId);

		return appointment;
	}

	private AppUser AppUser(string Id)
	{
		var user = _appUserService.GetUser(Id);

		return user;
	}

	public IActionResult Diagnosis()
	{
        var diagnosis = _labDiagnosisService.GetAllLabDiagnosis().Where(x => x.ActionStatus == Constants.Pending)
						.Select(x => new LabDiagnosisViewModel
						{
							Id = x.Id,
							ReferralId = x.ReferralId,
							TestId = x.TestId,
							TestName = _testService.GetDiagnosticTest(x.TestId).Title,
							TestRange = $"{_testService.GetDiagnosticTest(x.TestId).InitialRange} - {_testService.GetDiagnosticTest(x.TestId).FinalRange}",
							Unit = _testService.GetDiagnosticTest(x.TestId).Unit,
							DoctorId = (_doctorService.GetDoctor(Appointment(x.ReferralId).DoctorId)).Id,
							DoctorName = AppUser(_doctorService.GetDoctor(Appointment(x.ReferralId).DoctorId).UserId).FullName,
							DoctorRemarks = x.DoctorRemarks,
							PatientImage = AppUser(_patientService.GetPatient(Appointment(x.ReferralId).PatientId).UserId).ProfileImage,
                            PatientId = _patientService.GetPatient(Appointment(x.ReferralId).PatientId).Id,
							PatientName = AppUser(_patientService.GetPatient(Appointment(x.ReferralId).PatientId).UserId).FullName
						}).ToList();

		var detail = (from record in diagnosis
                      group record by new
					  {
						  record.ReferralId,
						  record.PatientName,
						  record.PatientImage,
					  } into patient
					  select new DiagnosisViewModel()
					  {
						  Referral = patient.Key.ReferralId,
						  PatientName = patient.Key.PatientName,
						  PatientImage = patient.Key.PatientImage,
						  LaboratoryDiagnosis = patient.ToList(),
					  }).ToList();


		return View(detail);
	}

	public IActionResult Details(Guid Id)
	{
		var diagnosis = _labDiagnosisService.GetAllLabDiagnosis().Where(x => x.Id == Id).ToList();

		var result = diagnosis.Select(x => new DiagnosisDetailViewModel
		{
            TestName = _testService.GetDiagnosticTest(x.TestId).Title,
            TestRange = $"{_testService.GetDiagnosticTest(x.TestId).InitialRange} - {_testService.GetDiagnosticTest(x.TestId).FinalRange}",
            Unit = _testService.GetDiagnosticTest(x.TestId).Unit,
            DoctorId = (_doctorService.GetDoctor(Appointment(x.ReferralId).DoctorId)).Id,
            DoctorName = AppUser(_doctorService.GetDoctor(Appointment(x.ReferralId).DoctorId).UserId).FullName,
            PatientImage = AppUser(_patientService.GetPatient(Appointment(x.ReferralId).PatientId).UserId).ProfileImage,
            PatientId = _patientService.GetPatient(Appointment(x.ReferralId).PatientId).Id,
            PatientName = AppUser(_patientService.GetPatient(Appointment(x.ReferralId).PatientId).UserId).FullName,
            LaboratoryDiagnosis = new()
			{
                Id = x.Id,
                ReferralId = x.ReferralId,
                TestId = x.TestId,
                DoctorRemarks = x.DoctorRemarks,
            }
		}).FirstOrDefault();

		return View(result);
	}

	public IActionResult History()
	{
		var diagnosis = _labDiagnosisService.GetAllLabDiagnosis().Where(x => x.ActionStatus == Constants.Completed)
						.Select(x => new LabDiagnosisViewModel
						{
							Id = x.Id,
							ReferralId = x.ReferralId,
							TestId = x.TestId,
							TestName = _testService.GetDiagnosticTest(x.TestId).Title,
							TestRange = $"{_testService.GetDiagnosticTest(x.TestId).InitialRange} - {_testService.GetDiagnosticTest(x.TestId).FinalRange}",
							Unit = _testService.GetDiagnosticTest(x.TestId).Unit,
							Value = x.Value,
							TechnicianRemarks = x.TechnicianRemarks,
							DoctorId = (_doctorService.GetDoctor(Appointment(x.ReferralId).DoctorId)).Id,
							DoctorName = AppUser(_doctorService.GetDoctor(Appointment(x.ReferralId).DoctorId).UserId).FullName,
							DoctorRemarks = x.DoctorRemarks,
							PatientImage = AppUser(_patientService.GetPatient(Appointment(x.ReferralId).PatientId).UserId).ProfileImage,
							PatientId = _patientService.GetPatient(Appointment(x.ReferralId).PatientId).Id,
							PatientName = AppUser(_patientService.GetPatient(Appointment(x.ReferralId).PatientId).UserId).FullName,
							FinalizedDate = x.FinalizedDate
						}).OrderByDescending(x => x.FinalizedDate).ToList();

		var detail = (from record in diagnosis
					  group record by new
					  {
						  record.ReferralId,
						  record.PatientName,
						  record.PatientImage,
					  } into patient
					  select new DiagnosisViewModel()
					  {
						  Referral = patient.Key.ReferralId,
						  PatientName = patient.Key.PatientName,
						  PatientImage = patient.Key.PatientImage,
						  LaboratoryDiagnosis = patient.ToList(),
					  }).ToList();


		return View(detail);
	}

	public IActionResult Requested()
	{
		return View();
	}

	#region API Calls
	[HttpPost]	
	public IActionResult Details(DiagnosisDetailViewModel detailViewModel)
	{
		var result = detailViewModel.LaboratoryDiagnosis;

		var claimsIdentity = (ClaimsIdentity)User.Identity;

		var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

		var technician = _technicianService.GetAllLabTechnicians().Where(x => x.UserId == claim.Value).FirstOrDefault();

		result.TechnicianId = technician.Id;

		_labDiagnosisService.UpdateLabDiagnosis(result);
        
		TempData["Success"] = "Diagnosis Successfully Completed";

		return RedirectToAction("Diagnosis");
	}
    #endregion
}
