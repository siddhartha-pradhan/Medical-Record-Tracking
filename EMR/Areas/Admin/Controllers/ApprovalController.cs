using System.Data;
using Microsoft.AspNetCore.Mvc;
using EMR.Core.Constants;
using EMR.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using EMR.Application.Interfaces.Services;
using EMR.Core.Constants;

namespace EMR.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Constants.Admin)]
public class ApprovalController : Controller
{
    private readonly IAppUserService _appUserService;
    private readonly ILabTechnicianService _labTechnicianService;
    private readonly IPharmacistService _pharmacistService;
    private readonly IDoctorService _doctorService;
    private readonly ISpecialtyService _specialtyService;
    private readonly IEmailSender _emailSender;

    public ApprovalController(IAppUserService appUserService, 
        ILabTechnicianService labTechnicianService, 
        IPharmacistService pharmacistService, 
        IDoctorService doctorService, 
        ISpecialtyService specialtyService,
        IEmailSender emailSender)
    {
        _appUserService = appUserService;
        _labTechnicianService = labTechnicianService;
        _pharmacistService = pharmacistService;
        _doctorService = doctorService;
        _specialtyService = specialtyService;
        _emailSender = emailSender;
    }

    #region Razor Pages
    public IActionResult Doctor()
    {
        var appUsers = _appUserService.GetAllUsers().Where(x => !x.EmailConfirmed).ToList();
        var doctors = _doctorService.GetAllDoctors().ToList();
        var specialties = _specialtyService.GetAllSpecialties().ToList();

        var result = (from user in appUsers
                      join doctor in doctors
                      on user.Id equals doctor.UserId
                      join specialty in specialties
                      on doctor.DepartmentId equals specialty.Id
                      select new ApprovalViewModel
                      {
                          UserId = user.Id,
                          StaffId = doctor.Id,
                          FullName = user.FullName,
                          PhoneNumber = user.PhoneNumber,
                          EmailAddress = user.Email,
                          ProfileImage = user.ImageURL,
                          CertificationNumber = doctor.CertificationNumber,
                          Department = specialty.Name,
                          HighestMedicalDegree = doctor.HighestMedicalDegree,
                          CertificationURL = doctor.CertificationURL,
                          ResumeURL = doctor.ResumeURL,
                      }).ToList();

        return View(result);
    }

    public IActionResult Pharmacist()
    {
        var appUsers = _appUserService.GetAllUsers().Where(x => !x.EmailConfirmed).ToList();
        var pharmacists = _pharmacistService.GetAllPharmacists().ToList();

        var result = (from user in appUsers
                      join pharmacist in pharmacists
                      on user.Id equals pharmacist.UserId
                      select new ApprovalViewModel
                      {
                          UserId = user.Id,
                          StaffId = pharmacist.Id,
                          FullName = user.FullName,
                          PhoneNumber = user.PhoneNumber,
                          EmailAddress = user.Email,
                          ProfileImage = user.ImageURL,
                          CertificationNumber = pharmacist.CertificateNumber,
                          HighestMedicalDegree = pharmacist.HighestMedicalDegree,
                          CertificationURL = pharmacist.CertificationURL,
                          ResumeURL = pharmacist.ResumeURL,
                      }).ToList();

        return View(result);
    }

    public IActionResult LabTechnician()
    {
        var appUsers = _appUserService.GetAllUsers().Where(x => !x.EmailConfirmed).ToList();
        var labTechnicians = _labTechnicianService.GetAllLabTechnicians().ToList();

        var result = (from user in appUsers
                      join labTechnician in labTechnicians
                      on user.Id equals labTechnician.UserId
                      select new ApprovalViewModel
                      {
                          UserId = user.Id,
                          StaffId = labTechnician.Id,
                          FullName = user.FullName,
                          PhoneNumber = user.PhoneNumber,
                          EmailAddress = user.Email,
                          ProfileImage = user.ImageURL,
                          CertificationNumber = labTechnician.CertificateNumber,
                          HighestMedicalDegree = labTechnician.HighestMedicalDegree,
                          CertificationURL = labTechnician.CertificationURL,
                          ResumeURL = labTechnician.ResumeURL,
                      }).ToList();

        return View(result);
    }
    #endregion

    #region API Calls
    [HttpPost, ActionName("Doctor")]
    public IActionResult ApproveDoctor(string id, Guid staffId)
    {
        var user = _appUserService.GetUser(id);
        var doctor = _doctorService.GetDoctor(staffId);

        if (ModelState.IsValid)
        {
            _doctorService.ApproveDoctor(doctor);

            TempData["Success"] = "Doctor Approved Successfully";

            _emailSender.SendEmailAsync(user.Email, "Successful Registration",
                        $"Dear {user.FullName}, <br><br>Your request has been accepted and you have been registered to our system as a doctor. <br>Please use your registered email and password as <b>\"EMR@123\"</b> as the login credential for the system.<br>Regards,<br>EMR Hospital");

            return RedirectToAction("Doctor");
        }

        return View();
    }

    [HttpPost, ActionName("LabTechnician")]
    public IActionResult ApproveLabTechnician(string id, Guid staffId)
    {
        var user = _appUserService.GetUser(id);
        var labTechnician = _labTechnicianService.GetLabTechnician(staffId);

        if (ModelState.IsValid)
        {
            _labTechnicianService.ApproveLabTechnician(labTechnician);
            TempData["Success"] = "Lab Technician Approved Successfully";

			_emailSender.SendEmailAsync(user.Email, "Successful Registration",
						$"Dear {user.FullName}, <br><br>Your request has been accepted and you have been registered to our system as a lab technician. <br>Please use your registered email and password as <b>\"EMR@123\"</b> as the login credential for the system.<br>Regards,<br>EMR Hospital");

			return RedirectToAction("LabTechnician");
        }

        return View();
    }

    [HttpPost, ActionName("Pharmacist")]
    public IActionResult ApprovePharmacist(string id, Guid staffId)
    {
        var user = _appUserService.GetUser(id);
        var pharmacist = _pharmacistService.GetPharmacist(staffId);

        if (ModelState.IsValid)
        {
            _pharmacistService.ApprovePharmacist(pharmacist);
            TempData["Success"] = "Pharmacist Approved Successfully";

			_emailSender.SendEmailAsync(user.Email, "Successful Registration",
						$"Dear {user.FullName}, <br><br>Your request has been accepted and you have been registered to our system as a pharmacist. <br>Please use your registered email and password as <b>\"EMR@123\"</b> as the login credential for the system.<br>Regards,<br>EMR Hospital");

			return RedirectToAction("Pharmacist");
        }

        return View();
    }
    #endregion
}
