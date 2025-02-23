using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Silverline.Application.Interfaces.Services;
using Silverline.Core.Constants;
using Silverline.Core.Entities;
using Silverline.Core.ViewModels;
using System.Data;

namespace Silverline.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Constants.Admin)]
public class LabTechnicianController : Controller
{
    private readonly IAppUserService _appUserService;
    private readonly ILabTechnicianService _labTechnicianService;

    public LabTechnicianController(IAppUserService appUserService, 
        ILabTechnicianService labTechnicianService)
    {
        _appUserService = appUserService;
        _labTechnicianService = labTechnicianService;
    }

    #region Razor Pages
    public IActionResult Index()
    {
        var labTechnicians = _labTechnicianService.GetAllLabTechnicians().ToList();
        var users = _appUserService.GetAllUsers().Where(x => x.EmailConfirmed == true).ToList();

        var result = (from user in users
                      join labTechnician in labTechnicians
                      on user.Id equals labTechnician.UserId
                      select new LabTechnicianViewModel
                      {
						  UserId = user.Id,
						  LabTechnicianId = labTechnician.Id.ToString(),
						  Image = user.ImageURL,
						  ProfileImage = user.ImageURL,
						  Name = user.FullName,
						  PhoneNumber = user.PhoneNumber,
						  Email = user.Email,
						  CertificationNumber = labTechnician.CertificateNumber,
						  HighestDegree = labTechnician.HighestMedicalDegree,
						  Certification = labTechnician.CertificationURL,
						  Resume = labTechnician.ResumeURL,
						  IsLocked = user.LockoutEnd > DateTime.Now ? true : false
					  }).ToList();

        return View(result);
    }

	public IActionResult Detail(string id)
	{
		var labTechnicians = _labTechnicianService.GetAllLabTechnicians().ToList();
		var users = _appUserService.GetAllUsers().Where(x => x.Id == id).ToList();

		var result = (from user in users
					  join labTechnician in labTechnicians
					  on user.Id equals labTechnician.UserId
					  select new LabTechnicianViewModel
					  {
						  UserId = user.Id,
						  LabTechnicianId = labTechnician.Id.ToString(),
						  Image = user.ImageURL,
						  ProfileImage = user.ImageURL,
						  Name = user.FullName,
						  PhoneNumber = user.PhoneNumber,
						  Email = user.Email,
						  CertificationNumber = labTechnician.CertificateNumber,
						  HighestDegree = labTechnician.HighestMedicalDegree,
						  Certification = labTechnician.CertificationURL,
						  Resume = labTechnician.ResumeURL,
						  IsLocked = user.LockoutEnd > DateTime.Now ? true : false
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
