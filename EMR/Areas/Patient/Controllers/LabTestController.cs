using Microsoft.AspNetCore.Mvc;
using Silverline.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Silverline.Application.Interfaces.Services;
using Silverline.Infrastructure.Implementation.Services;
using Silverline.Core.ViewModels;
using System.Security.Claims;
using Silverline.Core.Entities;

namespace Silverline.Areas.Patient.Controllers;

[Area("Patient")]
[Authorize(Roles = Constants.Patient)]
public class LabTestController : Controller
{
    private readonly ITestService _testService;
    private readonly ITestTypeService _testTypeService;
    private readonly ITestCartService _testCartService;
    private readonly IPatientService _patientService;

    public LabTestController(ITestService testService,
        ITestTypeService testTypeService,
        ITestCartService testCartService,
        IPatientService patientService)
    {
        _testService = testService;
        _testTypeService = testTypeService;
        _testCartService = testCartService;
        _patientService = patientService;
    }

    #region Razor Pages
    public IActionResult Index()
	{
        var tests = _testService.GetAllDiagnosticTests();
        var testTypes = _testTypeService.GetAllTestTypes();

        var result = (from testType in testTypes
                      join test in tests
                      on testType.Id equals test.ClassId
                      select new TestTypeViewModel
                      {
                          Id = test.Id.ToString(),
                          Title = test.Title,
                          Unit = test.Unit,
                          InitialRange = test.InitialRange,
                          FinalRange = test.FinalRange,
                          UnitPrice = test.UnitPrice,
                          TestType = testType.Name
                      }).ToList();

        var testCart = new CartViewModel()
        {
            TestTypes = result,
            TestCart = new Core.Entities.TestCart(),
        };

        return View(testCart);
	}
    #endregion

    #region API Calls
    public IActionResult AddToCart(Guid testId)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;

        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        var patient = _patientService.GetAllPatients().Where(x => x.UserId == claim.Value).FirstOrDefault();
        
        var cart = _testCartService.GetAllTestCarts().Where(x => x.PatientId == patient.Id && x.TestId == testId).FirstOrDefault();

        if(cart != null)
        {
            TempData["Delete"] = "The diagnostic test has already been added to your cart, check once again.";

            return RedirectToAction("Index");
        }

        var testCart = new Core.Entities.TestCart()
        {
            TestId = testId,
            PatientId = patient.Id,
        };

        _testCartService.AddTest(testCart);

        TempData["Success"] = "Diagnostic successfully added to your cart.";
        
        return RedirectToAction("Index");
    }
    #endregion

}
