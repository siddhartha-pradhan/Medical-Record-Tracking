using Microsoft.AspNetCore.Mvc;
using Silverline.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Silverline.Application.Interfaces.Services;

namespace Silverline.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Constants.Admin)]
public class PharmacistController : Controller
{
    private readonly IAppUserService _appUserService;
    private readonly IPharmacistService _pharmacistService;

    public PharmacistController(IAppUserService appUserService, IPharmacistService pharmacistService)
    {
        _appUserService = appUserService;
        _pharmacistService = pharmacistService;
    }

    #region Razor Pages
    public IActionResult Index()
    {
        var pharmacists = _pharmacistService.GetAllPharmacists().ToList();
        var users = _appUserService.GetAllUsers().Where(x => x.EmailConfirmed == true).ToList();

        var result = (from user in users
                      join pharmacist in pharmacists
                      on user.Id equals pharmacist.UserId
                      select new
                      {
                          Name = user.FullName,
                          PhoneNumber = user.PhoneNumber,
                          Email = user.Email,
                          CertificationNumber = pharmacist.CertificateNumber,
                          HighestDegree = pharmacist.HighestMedicalDegree
                      }).ToList();

        return View(result);
    }
    #endregion
}
