using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Silverline.Application.Interfaces.Services;
using Silverline.Core.Constants;
using Silverline.Core.Entities;
using Silverline.Infrastructure.Implementation.Services;
using System.Security.Claims;

namespace Silverline.Areas.Patient.Controllers;

[Area("Patient")]
[Authorize(Roles = Constants.Patient)]
public class TestController : Controller
{
    private readonly ITestService _testService;
    private readonly ITestCartService _testCartService;
    private readonly IPatientService _patientService;

    public TestController(ITestService testService, ITestCartService testCartService, IPatientService patientService)
    {
        _testService = testService;
        _testCartService = testCartService;
        _patientService = patientService;
    }

    #region Razor Pages
    public IActionResult Index()
    {
        var result = _testService.GetAllDiagnosticTests();
        return View(result);
    }

    public IActionResult Details(Guid Id)
    {
        var cart = new TestCart()
        {
            TestId = Id,
            DiagnosticTest = _testService.GetDiagnosticTest(Id),
        };

        return View(cart);
    }
    #endregion

    #region API Calls
    public IActionResult Details(TestCart testCart)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        var patientId = Guid.Parse(claim.Value);
        testCart.PatientId = patientId;

        var cart = _testCartService.GetAllTestCarts().Where(x => x.PatientId == patientId && x.TestId == testCart.TestId);

        if(testCart == null)
        {
            _testCartService.AddTest(testCart);
        }

        return RedirectToAction(nameof(Index));
    }
    #endregion
}
