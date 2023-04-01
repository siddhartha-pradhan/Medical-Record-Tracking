using Microsoft.AspNetCore.Mvc;
using Silverline.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Silverline.Application.Interfaces.Services;

namespace Silverline.Areas.Patient.Controllers;

[Area("Admin")]
[Authorize(Roles = Constants.Patient)]
public class CartController : Controller
{
    private readonly ITestCartService _testCartService;

    private readonly IEmailSender _emailSender;

    public CartController(ITestCartService testCartService, 
        IEmailSender emailSender)
    {
        _testCartService = testCartService;
        _emailSender = emailSender;
    }

    #region Razor Pages
    public IActionResult Index()
    {
        return View();
    }
    #endregion

    #region API Calls

    #endregion
}
