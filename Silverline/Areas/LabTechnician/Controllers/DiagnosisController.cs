﻿using Microsoft.AspNetCore.Mvc;
using Silverline.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Silverline.Application.Interfaces.Services;
using Silverline.Core.ViewModels;
using Silverline.Core.Entities;
using Silverline.Application.Interfaces.Repositories;
using Silverline.Infrastructure.Implementation.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
//using Microsoft.Reporting.WebForms;

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
	private readonly ITestCartService _testCartService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public DiagnosisController(ITestService testService, 
		IPatientService patientService, 
		IDoctorService doctorService, 
		IAppointmentDetailService appointmentDetailService, 
		IAppointmentService appointmentService,
        ILabDiagnosisService labDiagnosisService,
        IAppUserService appUserService,
		ILabTechnicianService technicianService,
        ITestCartService testCartService,
        IWebHostEnvironment webHostEnvironment)
	{
		_testService = testService;
		_patientService = patientService;
		_doctorService = doctorService;
		_appointmentDetailService = appointmentDetailService;
		_appointmentService = appointmentService;
        _appUserService = appUserService;
		_labDiagnosisService = labDiagnosisService;
		_technicianService = technicianService;
		_testCartService = testCartService;
		_webHostEnvironment = webHostEnvironment;
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
							FinalizedDate = x.FinalizedDate,
							DoctorName = AppUser(_doctorService.GetDoctor(Appointment(x.ReferralId).DoctorId).UserId).FullName,
							DoctorRemarks = x.DoctorRemarks,
							PatientImage = AppUser(_patientService.GetPatient(Appointment(x.ReferralId).PatientId).UserId).ImageURL,
                            PatientId = _patientService.GetPatient(Appointment(x.ReferralId).PatientId).Id,
							PatientName = AppUser(_patientService.GetPatient(Appointment(x.ReferralId).PatientId).UserId).FullName,
							AppointmentDate = Appointment(x.ReferralId).FinalizedTime
                        }).OrderBy(x => x.AppointmentDate).ToList();

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
            PatientImage = AppUser(_patientService.GetPatient(Appointment(x.ReferralId).PatientId).UserId).ImageURL,
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
							PatientImage = AppUser(_patientService.GetPatient(Appointment(x.ReferralId).PatientId).UserId).ImageURL,
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
		var diagnosis = _testCartService.GetAllTestCarts().Where(x => x.ActionStatus == Constants.Ongoing).Select(x => new LabDiagnosisViewModel
						{
							Id = x.Id,
							TestId = x.TestId,
							PatientId = x.PatientId,
							PatientName = AppUser(_patientService.GetPatient(x.PatientId).UserId).FullName,
							PatientImage = AppUser(_patientService.GetPatient(x.PatientId).UserId).ImageURL,
							TestName = _testService.GetDiagnosticTest(x.TestId).Title,
							TestRange = $"{_testService.GetDiagnosticTest(x.TestId).InitialRange} - {_testService.GetDiagnosticTest(x.TestId).FinalRange}",
							Unit = _testService.GetDiagnosticTest(x.TestId).Unit,
							PaymentStatus = x.PaymentStatus
						}).OrderByDescending(x => x.FinalizedDate).ToList();

        var detail = (from record in diagnosis
                      group record by new
                      {
                          record.PatientName,
                          record.PatientImage,
						  record.PaymentStatus
                      } into patient
                      select new DiagnosisViewModel()
                      {
                          PatientName = patient.Key.PatientName,
                          PatientImage = patient.Key.PatientImage,
                          LaboratoryDiagnosis = patient.ToList(),
						  PaymentStatus = patient.Key.PaymentStatus
                      }).ToList();

		return View(detail);
    }

	public IActionResult RequestedDetails(Guid cartId)
	{
		var cart = _testCartService.GetTestCart(cartId);

		var diagnosis = _labDiagnosisService.GetAllLabDiagnosis().Where(x => x.Id == cart.TestId).FirstOrDefault();

		var result = new CartTestViewModel()
		{
			CartId = cartId,
			TestId = cart.TestId,
			TestTitle = _testService.GetDiagnosticTest(cart.TestId).Title,
			TestRange = $"{_testService.GetDiagnosticTest(cart.TestId).InitialRange} - {_testService.GetDiagnosticTest(cart.TestId).FinalRange} {_testService.GetDiagnosticTest(cart.TestId).Unit}",
			PatientId = cart.PatientId,
			PatientName = AppUser(_patientService.GetPatient(cart.PatientId).UserId).FullName,
			PatientImage = AppUser(_patientService.GetPatient(cart.PatientId).UserId).ImageURL,
		};

        return View(result);
	}

	public IActionResult Records()
	{
		var diagnosis = _testCartService.GetAllTestCarts().Where(x => x.ActionStatus == Constants.Completed)
						.Select(x => new LabDiagnosisViewModel
						{
							Id = x.Id,
							TestId = x.TestId,
							TestName = _testService.GetDiagnosticTest(x.TestId).Title,
							TestRange = $"{_testService.GetDiagnosticTest(x.TestId).InitialRange} - {_testService.GetDiagnosticTest(x.TestId).FinalRange}",
							Unit = _testService.GetDiagnosticTest(x.TestId).Unit,
							Value = x.Value,
							TechnicianRemarks = x.TechnicianRemarks,
							PatientId = x.PatientId,
							PatientImage = AppUser(_patientService.GetPatient(x.PatientId).UserId).ImageURL,
							PatientName = AppUser(_patientService.GetPatient(x.PatientId).UserId).FullName,
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

	[HttpPost]
	public IActionResult RequestedDetails(CartTestViewModel cartTestViewModel)
	{
		var cart = new TestCart()
		{
			Id = cartTestViewModel.CartId,
			Value = cartTestViewModel.TestValue,
			ActionStatus = Constants.Completed,
			TechnicianRemarks = cartTestViewModel.TechnicianRemarks
		};

		_testCartService.Finalize(cart);

		TempData["Success"] = "Successfully Finalized Test";
		
		return RedirectToAction("Requested");

    }
	#endregion
}
