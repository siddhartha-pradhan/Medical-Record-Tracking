using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Silverline.Application.Interfaces.Services;
using Silverline.Core.Constants;
using Silverline.Core.ViewModels;
using Silverline.Infrastructure.Implementation.Services;
using System.Security.Claims;

namespace Silverline.Areas.Patient.Controllers;

[Area("Patient")]
[Authorize(Roles = Constants.Patient)]
public class DiagnosisController : Controller
{
	private readonly IPatientService _patientService;
	private readonly ITestCartService _testCartService;
	private readonly ITestService _testService;
	private readonly ILabDiagnosisService _labDiagnosisService;
	private readonly ITestTypeService _testTypeService;

	public DiagnosisController(IPatientService patientService,
		ITestCartService testCartService,
		ITestService testService,
		ILabDiagnosisService labDiagnosisService,
		ITestTypeService testTypeService)
	{
		_patientService = patientService;
		_testCartService = testCartService;
		_testService = testService;
		_labDiagnosisService = labDiagnosisService;
		_testTypeService = testTypeService;
	}

	public IActionResult Booked()
	{
		var claimsIdentity = (ClaimsIdentity)User.Identity;
		var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

		var patient = _patientService.GetAllPatients().Where(x => x.UserId == claim.Value).FirstOrDefault();
		var carts = _testCartService.GetAllTestCarts().Where(x => x.ActionStatus == Constants.Ongoing && x.PatientId == patient.Id);
		var tests = _testService.GetAllDiagnosticTests();
		var testTypes = _testTypeService.GetAllTestTypes();

		var result = (from testType in testTypes
					  join test in tests
						 on testType.Id equals test.ClassId
					  join cart in carts
						 on test.Id equals cart.TestId
					  select new DiagnosticTestCartViewModel()
					  {
						  Id = cart.Id,
						  BookedDate = cart.BookedDate.ToString("dddd, dd MMMM yyyy HH:mm"),
						  PaymentStatus = cart.PaymentStatus,
						  Range = $"{test.FinalRange} - {test.InitialRange} {test.Unit}",
						  TestType = testType.Name,
						  TestName = test.Title,
					  }).ToList();

		return View(result);
	}

	public IActionResult Record()
	{
		var claimsIdentity = (ClaimsIdentity)User.Identity;
		var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

		var patient = _patientService.GetAllPatients().Where(x => x.UserId == claim.Value).FirstOrDefault();
		var carts = _testCartService.GetAllTestCarts().Where(x => x.ActionStatus == Constants.Completed && x.PatientId == patient.Id);
		var tests = _testService.GetAllDiagnosticTests();
		var testTypes = _testTypeService.GetAllTestTypes();

		var result = (from testType in testTypes
					  join test in tests
						 on testType.Id equals test.ClassId
					  join cart in carts
						 on test.Id equals cart.TestId
					  select new DiagnosticTestCartViewModel()
					  {
						  BookedDate = cart.BookedDate.ToString("dddd, dd MMMM yyyy HH:mm"),
						  FinalizedTest = cart.FinalizedDate.ToString("dddd, dd MMMM yyyy HH:mm"),
						  Range = $"{test.FinalRange} - {test.InitialRange} {test.Unit}",
						  TestName = test.Title,
						  TechnicianRemarks = cart.TechnicianRemarks,
						  Value = $"{cart.Value} {test.Unit}"
					  }).ToList();

		return View(result);
	}

	public IActionResult Cancel(Guid id)
	{
		_testCartService.Cancel(id);
		TempData["Success"] = "Booked Test Successfully Canceled";
		return RedirectToAction("Booked");
	}
}
