using Microsoft.AspNetCore.Mvc;
using Silverline.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Silverline.Application.Interfaces.Services;
using System.Security.Claims;
using Silverline.Infrastructure.Implementation.Services;
using Silverline.Core.ViewModels;
using Silverline.Core.Entities;

namespace Silverline.Areas.Patient.Controllers;

[Area("Patient")]
[Authorize(Roles = Constants.Patient)]
public class CartController : Controller
{
    private readonly IPatientService _patientService;
    private readonly ITestCartService _testCartService;
    private readonly ITestService _testService;
    private readonly ILabDiagnosisService _labDiagnosisService;

    public CartController(ITestCartService testCartService,
        IPatientService patientService,
        ITestService testService,
        ILabDiagnosisService labDiagnosisService)
    {
        _testCartService = testCartService;
        _patientService = patientService;
        _testService = testService;
        _labDiagnosisService = labDiagnosisService;
    }

    #region Razor Pages
    public IActionResult Index()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;

        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        var patient = _patientService.GetAllPatients().Where(x => x.UserId == claim.Value).FirstOrDefault();

        var cartViewModel = new TestCartViewModel()
        {
            TestCartList = _testCartService.GetAllTestCarts().Where(x => x.PatientId == patient.Id && x.ActionStatus == Constants.Pending).ToList(),
            TestHeader = new Core.Entities.TestHeader()
        };

        foreach(var item in cartViewModel.TestCartList)
        {
            var credits = _testService.GetDiagnosticTest(item.TestId).UnitPrice;
            cartViewModel.TestHeader.TotalAmount += credits;
        }

        return View(cartViewModel);
    }

	public IActionResult Summary()
	{
		var claimsIdentity = (ClaimsIdentity)User.Identity;

		var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        var patient = _patientService.GetAllPatients().Where(x => x.UserId == claim.Value).FirstOrDefault();

        var cartViewModel = new TestCartViewModel()
        {
            TestHeader = new TestHeader(),
            TestCartList = _testCartService.GetAllTestCarts().Where(x => x.PatientId == patient.Id && x.ActionStatus == Constants.Pending).ToList(),
		};
		
		foreach (var item in cartViewModel.TestCartList)
		{
            var test = _testService.GetDiagnosticTest(item.TestId);
			cartViewModel.TestHeader.TotalAmount += test.UnitPrice;
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

    [HttpPost]
    public IActionResult Summary(TestCartViewModel cartViewModel)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;

        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        var patient = _patientService.GetAllPatients().Where(x => x.UserId == claim.Value).FirstOrDefault();

        var testCart = _testCartService.GetAllTestCarts().Where(x => x.PatientId == patient.Id && x.ActionStatus == Constants.Pending).ToList();

        var totalCredits = 0.0f;

        foreach (var item in testCart)
        {
            var test = _testService.GetDiagnosticTest(item.TestId);
            totalCredits += test.UnitPrice;
        }

        var status = Constants.Pending;

        if (cartViewModel.PaymentStatus == "Yes")
        {
            if(patient.CreditPoints < totalCredits)
            {
                TempData["Delete"] = "You do not have enough credit points";
                return RedirectToAction("Index");
            }

            int creditAmount = (int)totalCredits;

            patient.CreditPoints -= creditAmount;

            status = Constants.Completed;
        }

        foreach(var item in testCart)
        {
            var diagnosis = new TestCart()
            {
                Id = item.Id,
                BookedDate = DateTime.Now,
                ActionStatus = Constants.Ongoing,
                PatientId = patient.Id,
                TestId = item.TestId,
                PaymentStatus = status,
            };

            _testCartService.Update(diagnosis);
        }

        TempData["Success"] = "Your order has been successfully placed.";

        return RedirectToAction("Index");
    }
    #endregion
}
