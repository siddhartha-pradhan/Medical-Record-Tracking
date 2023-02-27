using System.Data;
using Microsoft.AspNetCore.Mvc;
using Silverline.Core.Constants;
using Silverline.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Silverline.Application.Interfaces.Services;

namespace Silverline.Areas.Admin.Controllers;

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
                          ProfileImage = user.ProfileImage,
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
                          ProfileImage = user.ProfileImage,
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
                          ProfileImage = user.ProfileImage,
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
    public IActionResult ApproveDoctor(string id)
    {
        var user = _appUserService.GetUser(id);
        var doctor = _doctorService.GetUserDoctor(user.Id);

        if (ModelState.IsValid)
        {
            _doctorService.ApproveDoctor(doctor);

            TempData["Success"] = "Doctor Approved Successfully";

            _emailSender.SendEmailAsync(user.Email, "Successful Registration",
                        $"Hi there, You have been registered to our system as a doctor. The password is Silverline@123.");

            return RedirectToAction("Doctor");
        }

        return View(user);
    }

    [HttpPost, ActionName("LabTechnician")]
    public IActionResult ApproveLabTechnician(string id)
    {
        var user = _appUserService.GetUser(id);
        var labTechnician = _labTechnicianService.GetUserLabTechnician(user.Id);

        if (ModelState.IsValid)
        {
            _labTechnicianService.ApproveLabTechnician(labTechnician);
            TempData["Success"] = "Lab Technician Approved Successfully";

            _emailSender.SendEmailAsync(user.Email, "Successful Registration",
                        $"Hi there, You have been registered to our system as a lab technician. The password is Silverline@123.");

            return RedirectToAction("LabTechnician");
        }

        return View(user);
    }

    [HttpPost, ActionName("Pharmacist")]
    public IActionResult ApprovePharmacist(string id)
    {
        var user = _appUserService.GetUser(id);
        var pharmacist = _pharmacistService.GetUserPharmacist(user.Id);

        if (ModelState.IsValid)
        {
            _pharmacistService.ApprovePharmacist(pharmacist);
            TempData["Success"] = "Pharmacist Approved Successfully";

            _emailSender.SendEmailAsync(user.Email, "Successful Registration",
                        $"Hi there, You have been registered to our system as a Pharmacist. The password is Silverline@123.");

            return RedirectToAction("Pharmacist");
        }

        return View(user);
    }
    #endregion
}
