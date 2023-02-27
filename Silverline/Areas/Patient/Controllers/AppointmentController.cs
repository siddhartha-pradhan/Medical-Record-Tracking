using Microsoft.AspNetCore.Mvc;
using Silverline.Application.Interfaces.Services;
using Silverline.Core.ViewModels;

namespace Silverline.Areas.Patient.Controllers;

[Area("Patient")]
public class AppointmentController : Controller
{
    private readonly IAppUserService _appUserService;
    private readonly IDoctorService _doctorService;
    private readonly ISpecialtyService _specialtyService;

    public AppointmentController(IAppUserService appUserService, IDoctorService doctorService, ISpecialtyService specialtyService)
    {
        _appUserService = appUserService;
        _doctorService = doctorService;
        _specialtyService = specialtyService;
    }

    public IActionResult Index()
    {
        var doctors = _doctorService.GetAllDoctors().ToList();
        var appUsers = _appUserService.GetAllUsers().Where(x => x.EmailConfirmed).ToList();
        var specialties = _specialtyService.GetAllSpecialties().ToList();


        var result = (from appUser in appUsers
                     join doctor in doctors
                     on appUser.Id equals doctor.UserId
                     join specialty in specialties
                     on doctor.DepartmentId equals specialty.Id
                     select new DoctorViewModel
                     {
                         UserId = appUser.Id,
                         DoctorId = doctor.Id,
                         Name = appUser.FullName,
                         HighestMedicalDegree = doctor.HighestMedicalDegree,
                         Specialty = specialty.Name,
                         ProfileImage = appUser.ProfileImage
                     }).ToList();   

        return View(result);
    }
}
