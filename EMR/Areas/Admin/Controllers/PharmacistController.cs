using Microsoft.AspNetCore.Mvc;
using EMR.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using EMR.Application.Interfaces.Services;
using EMR.Core.ViewModels;
using EMR.Infrastructure.Implementation.Services;

namespace EMR.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Constants.Admin)]
public class PharmacistController : Controller
{
    private readonly IAppUserService _appUserService;
    private readonly IPharmacistService _pharmacistService;

    public PharmacistController(IAppUserService appUserService, 
        IPharmacistService pharmacistService)
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
                      select new PharmacistViewModel
                      {
                          UserId = user.Id,
                          PharmacistId = pharmacist.Id.ToString(),
                          Image = user.ImageURL,
                          ProfileImage = user.ImageURL,
                          Name = user.FullName,
                          PhoneNumber = user.PhoneNumber,
                          Email = user.Email,
                          CertificationNumber = pharmacist.CertificateNumber,
                          HighestDegree = pharmacist.HighestMedicalDegree,
                          Certification = pharmacist.CertificationURL,
                          Resume = pharmacist.ResumeURL,
                          IsLocked = user.LockoutEnd > DateTime.Now ? true : false
					  }).ToList();

        return View(result);
    }

	public IActionResult Detail(string Id)
	{
		var pharmacists = _pharmacistService.GetAllPharmacists().ToList();

		var users = _appUserService.GetAllUsers().Where(x => x.Id == Id).ToList();

		var result = (from user in users
					  join pharmacist in pharmacists
					  on user.Id equals pharmacist.UserId
					  select new PharmacistViewModel
					  {
						  UserId = user.Id,
                          PharmacistId = pharmacist.Id.ToString(),
                          Image = user.ImageURL,
                          ProfileImage = user.ImageURL,
                          Name = user.FullName,
						  PhoneNumber = user.PhoneNumber,
						  Email = user.Email,
						  CertificationNumber = pharmacist.CertificateNumber,
						  HighestDegree = pharmacist.HighestMedicalDegree,
						  Certification = pharmacist.CertificationURL,
						  Resume = pharmacist.ResumeURL,
						  IsLocked = user.LockoutEnd > DateTime.Now ? true : false,
					  }).FirstOrDefault();

		return View(result);
	}

	public IActionResult Lock(string id)
	{
		var user = _appUserService.GetUser(id);

		_appUserService.LockUser(id);

		TempData["Delete"] = $"{user.FullName} has been locked for 5 days.";

		return RedirectToAction("Index");
	}

	public IActionResult Unlock(string id)
	{
		var user = _appUserService.GetUser(id);

		_appUserService.UnlockUser(id);

		TempData["Success"] = $"{user.FullName} has been unlocked and is now accessible to the system.";

		return RedirectToAction("Index");

	}
	#endregion
}
