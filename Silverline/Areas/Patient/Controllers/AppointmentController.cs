using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Silverline.Application.Interfaces.Services;
using Silverline.Core.Constants;
using Silverline.Core.Entities;
using Silverline.Core.ViewModels;
using System.Security.Claims;

namespace Silverline.Areas.Patient.Controllers;

[Area("Patient")]
[Authorize(Roles = Constants.Patient)]
public class AppointmentController : Controller
{
    private readonly IAppUserService _appUserService;
    private readonly IDoctorService _doctorService;
    private readonly ISpecialtyService _specialtyService;
    private readonly IPatientService _patientService;
    private readonly IAppointmentService _appointmentService;

    public AppointmentController(IAppUserService appUserService, 
        IDoctorService doctorService, 
        ISpecialtyService specialtyService, 
        IPatientService patientService, 
        IAppointmentService appointmentService)
    {
        _appUserService = appUserService;
        _doctorService = doctorService;
        _specialtyService = specialtyService;
        _patientService = patientService;
        _appointmentService = appointmentService;
    }

    #region Razor Pages
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

    public IActionResult Book(Guid Id)
    {
        var doctor = _doctorService.GetDoctor(Id);
        var doctorUser = _appUserService.GetUser(doctor.UserId);
        var department = _specialtyService.GetSpecialty(doctor.DepartmentId);

        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        var patient = _patientService.GetAllPatients().Where(x => x.UserId == claim.Value).FirstOrDefault();
        var patientUser = _appUserService.GetUser(patient.UserId);

        var appointment = new AppointmentViewModel()
        {
            Appointment = new Appointment()
            {
                DoctorId = Id,
                PatientId = patient.Id,
                AppointmentStatus = Constants.Booked
            },
            PatientName = patientUser.FullName,
            Age = DateTime.Today.Year - patient.DateOfBirth.Year,
            DoctorName = doctorUser.FullName,
            DoctorProfileImage = doctorUser.ProfileImage,
            DoctorSpecialty = department.Name,
            HighestMedicalDegree = doctor.HighestMedicalDegree
        };

        return View(appointment);
    }
    #endregion

    #region API Calls
    [HttpPost]
    public IActionResult Book(AppointmentViewModel appointmentViewModel)
    {
        if (ModelState.IsValid)
        {
            _appointmentService.BookAppointment(appointmentViewModel.Appointment);
            TempData["Success"] = "Appointment Successfully Booked";
            return RedirectToAction("Index");
        }

        return View(appointmentViewModel);

    }
    #endregion
}