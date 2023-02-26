using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Silverline.Application.Interfaces.Services;
using Silverline.Core.Constants;
using Silverline.Core.Entities;
using System.Data;

namespace Silverline.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Constants.Admin)]
public class LabTechnicianController : Controller
{
    private readonly IAppUserService _appUserService;
    private readonly ILabTechnicianService _labTechnicianService;

    public LabTechnicianController(IAppUserService appUserService, ILabTechnicianService labTechnicianService)
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
                      select new
                      {
                          Name = user.FullName,
                          PhoneNumber = user.PhoneNumber,
                          Email = user.Email,
                          CertificationNumber = labTechnician.CertificateNumber,
                          HighestMedicalDegree = labTechnician.HighestMedicalDegree
                      }).ToList();

        return View(result);
    }
    #endregion
}
