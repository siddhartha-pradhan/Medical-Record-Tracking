using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Silverline.Application.Interfaces.Repositories;
using Silverline.Application.Interfaces.Services;
using Silverline.Core.Constants;
using Silverline.Core.ViewModels;
using System.Data;

namespace Silverline.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Constants.Admin)]
public class DoctorController : Controller
{
    private readonly IAppUserService _appUserService;
    private readonly IDoctorService _doctorService;
    private readonly ISpecialtyService _specialtyService;

    public DoctorController(IAppUserService appUserService, 
        IDoctorService doctorService, 
        ISpecialtyService specialtyService)
    {
        _appUserService = appUserService;
        _doctorService = doctorService;
        _specialtyService = specialtyService;
    }

    #region Razor Pages
    public IActionResult Index()
    {
        var doctors = _doctorService.GetAllDoctors().ToList();
        var specialties = _specialtyService.GetAllSpecialties().ToList();
        var users = _appUserService.GetAllUsers().Where(x => x.EmailConfirmed == true).ToList();

        var result = (from user in users
                      join doctor in doctors
                      on user.Id equals doctor.UserId
                      join specialty in specialties
                      on doctor.DepartmentId equals specialty.Id
                      select new DoctorViewModels
                      {
                          UserId = user.Id,
                          DoctorId = doctor.Id.ToString(),
                          Image = user.ProfileImage,
                          ProfileImage = user.ImageURL,
                          Name = user.FullName,
                          PhoneNumber = user.PhoneNumber,
                          Email = user.Email,
                          Speciality = specialty.Name,
                          CertificationNumber = doctor.CertificationNumber,
                          HighestDegree = doctor.HighestMedicalDegree,
                          Certification = doctor.CertificationURL,
                          Resume = doctor.ResumeURL,
                          IsLocked = user.LockoutEnd > DateTime.Now ? true : false,
                      }).ToList();

        return View(result);
    }

	public IActionResult Detail(string id)
	{
		var doctors = _doctorService.GetAllDoctors().ToList();
		var specialties = _specialtyService.GetAllSpecialties().ToList();
		var users = _appUserService.GetAllUsers().Where(x => x.Id == id).ToList();

		var result = (from user in users
					  join doctor in doctors
					  on user.Id equals doctor.UserId
					  join specialty in specialties
					  on doctor.DepartmentId equals specialty.Id
					  select new DoctorViewModels
					  {
						  UserId = user.Id,
						  DoctorId = doctor.Id.ToString(),
						  Image = user.ProfileImage,
						  ProfileImage = user.ImageURL,
						  Name = user.FullName,
						  PhoneNumber = user.PhoneNumber,
						  Email = user.Email,
						  Speciality = specialty.Name,
						  CertificationNumber = doctor.CertificationNumber,
						  HighestDegree = doctor.HighestMedicalDegree,
						  Certification = doctor.CertificationURL,
						  Resume = doctor.ResumeURL,
						  IsLocked = user.LockoutEnd > DateTime.Now ? true : false,
					  }).FirstOrDefault();

		return View(result);
	}
	#endregion
}
