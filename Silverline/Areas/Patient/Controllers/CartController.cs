using Microsoft.AspNetCore.Mvc;
using Silverline.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Silverline.Application.Interfaces.Services;
using System.Security.Claims;
using Silverline.Infrastructure.Implementation.Services;
using Silverline.Core.ViewModels;

namespace Silverline.Areas.Patient.Controllers;

[Area("Patient")]
[Authorize(Roles = Constants.Patient)]
public class CartController : Controller
{
    private readonly IPatientService _patientService;
    private readonly ITestCartService _testCartService;
    private readonly ITestService _testService;

    public CartController(ITestCartService testCartService,
        IPatientService patientService,
        ITestService testService)
    {
        _testCartService = testCartService;
        _patientService = patientService;
        _testService = testService;
    }

    #region Razor Pages
    public IActionResult Index()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;

        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        var patient = _patientService.GetAllPatients().Where(x => x.UserId == claim.Value).FirstOrDefault();

        var cartViewModel = new TestCartViewModel()
        {
            TestCartList = _testCartService.GetAllTestCarts().Where(x => x.PatientId == patient.Id).ToList(),
            TestHeader = new Core.Entities.TestHeader()
        };

        foreach(var item in cartViewModel.TestCartList)
        {
            var credits = _testService.GetDiagnosticTest(item.TestId).UnitPrice;
            cartViewModel.TestHeader.TotalAmount += credits;
        }

        return View(cartViewModel);
    }
    #endregion

    #region API Calls
    public IActionResult Remove(Guid cartId)
    {
        var cart = _testCartService.GetAllTestCarts().Where(u => u.Id == cartId).FirstOrDefault();

        _testCartService.Remove(cart);
        
        TempData["Success"] = "Diagnostics successfully removed from cart.";

        return RedirectToAction(nameof(Index));
    }
    #endregion
}
