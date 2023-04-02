using System.Data;
using Microsoft.AspNetCore.Mvc;
using Silverline.Core.Constants;
using Silverline.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Silverline.Application.Interfaces.Services;

namespace Silverline.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Constants.Admin)]
public class PatientController : Controller
{
    private readonly IAppUserService _appUserService;
    private readonly IPatientService _patientService;

    public PatientController(IAppUserService appUserService,IPatientService patientService)
    {
        _appUserService = appUserService;
        _patientService = patientService;
    }

    #region Razor Pages
    public IActionResult Index()
    {
        var patients = _patientService.GetAllPatients().ToList();
        var users = _appUserService.GetAllUsers().Where(x => x.EmailConfirmed == true).ToList();

        var result = (from user in users
                      join patient in patients
                      on user.Id equals patient.UserId
                      select new PatientViewModel
                      {
                          UserId = user.Id,
                          PatientId = patient.Id.ToString(),
                          Name = user.FullName,
                          PhoneNumber = user.PhoneNumber,
                          Email = user.Email,
                          Address = patient.Address,
                          DateOfBirth = patient.DateOfBirth.ToString("dd/MM/yyyy"),
                          CreditPoints = patient.CreditPoints,
                          IsLocked = user.LockoutEnd > DateTime.Now ? true : false,
                      }).ToList();

        return View(result);
    }

	public IActionResult Detail(string Id)
	{
		var patients = _patientService.GetAllPatients().ToList();
		var users = _appUserService.GetAllUsers().Where(x => x.Id == Id).ToList();

		var result = (from user in users
					  join patient in patients
					  on user.Id equals patient.UserId
					  select new PatientViewModel
					  {
						  UserId = user.Id,
						  PatientId = patient.Id.ToString(),
						  ProfileImage = user.ImageURL,
						  Image = user.ProfileImage,
						  Name = user.FullName,
						  PhoneNumber = user.PhoneNumber,
						  Email = user.Email,
						  Address = patient.Address,
						  DateOfBirth = patient.DateOfBirth.ToString("dd/MM/yyyy"),
						  CreditPoints = patient.CreditPoints,
						  IsLocked = user.LockoutEnd > DateTime.Now ? true : false,
					  }).FirstOrDefault();

		return View(result);
	}

    #endregion

    #region API Calls
    [HttpPost]
	public IActionResult Detail(PatientViewModel patientViewModel)
    {

        var patientId = patientViewModel.PatientId;
        var credit = patientViewModel.CreditPoints;

        _patientService.AddCredits(new Guid(patientId), credit);
        TempData["Success"] = $"{credit} Credits has been successfully added to the patient.";
        return RedirectToAction("Index");

    }

    [HttpPost]
    public IActionResult Lock(string id)
    {
        var user = _appUserService.GetUser(id);

        _appUserService.LockUser(id);

        TempData["Danger"] = $"{user.FullName} has been locked for 5 days.";

        return RedirectToAction("Index");

    }

    [HttpPost]
    public IActionResult Unlock(string id)
    {
        var user = _appUserService.GetUser(id);

        _appUserService.UnlockUser(id);

        TempData["Success"] = $"{user.FullName} has been unlocked and is now accessible to the system.";

        return RedirectToAction("Index");

    }

    #endregion
}
