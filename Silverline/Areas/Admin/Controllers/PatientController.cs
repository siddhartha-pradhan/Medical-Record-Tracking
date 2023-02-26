using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Silverline.Application.Interfaces.Repositories;
using Silverline.Application.Interfaces.Services;
using Silverline.Core.Constants;
using Silverline.Infrastructure.Implementation.Services;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

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
                      select new
                      {
                          Name = user.FullName,
                          PhoneNumber = user.PhoneNumber,
                          Email = user.Email,
                          Address = patient.Address,
                          DateOfBirth = patient.DateOfBirth,
                          CreditPoints = patient.CreditPoints
                      }).ToList();

        return View(result);
    }
    #endregion

    #region API Calls
    public IActionResult AddCreditPoints(Guid id)
    {
        if (ModelState.IsValid)
        {
            _patientService.AddCredits(id);
            TempData["Success"] = "10 Credits has been successfully added to the patient.";
            return RedirectToAction("Index");
        }

        return View();
    }
    #endregion
}
