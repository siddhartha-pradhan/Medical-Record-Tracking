using Microsoft.AspNetCore.Mvc;
using Silverline.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Silverline.Application.Interfaces.Services;
using Silverline.Core.ViewModels;
using System.Security.Claims;
using Silverline.Core.Entities;

namespace Silverline.Areas.Doctor.Controllers;

[Area("Doctor")]
[Authorize(Roles = Constants.Doctor)]
public class HomeController : Controller
{
    private readonly IAppUserService _appUserService;
    private readonly IPatientService _patientService;
    private readonly IDoctorService _doctorService;
    private readonly IAppointmentService _appointmentService;
    private readonly ISpecialtyService _specialtyService;
    private readonly ITestService _testService;
    private readonly IMedicineService _medicineService;
    private readonly IAppointmentDetailService _appointmentDetailService;
    private readonly IMedicalTreatmentService _medicalTreatmentService;

    public HomeController(IAppUserService appUserService,
        IPatientService patientService,
        IDoctorService doctorService,
        IAppointmentService appointmentService,
        ISpecialtyService specialtyService,
        ITestService testService,
        IMedicineService medicineService,
        IAppointmentDetailService appointmentDetailService,
        IMedicalTreatmentService medicalTreatmentService)
    {
        _appUserService = appUserService;
        _patientService = patientService;
        _doctorService = doctorService;
        _appointmentService = appointmentService;
        _specialtyService = specialtyService;
        _testService = testService;
        _medicineService = medicineService;
        _appointmentDetailService = appointmentDetailService;
        _medicalTreatmentService = medicalTreatmentService;
    }

    public IActionResult Index()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        
        var doctor = _doctorService.GetAllDoctors().Where(x => x.UserId == claim.Value).FirstOrDefault();

        var appointmentCount = _appointmentService.GetAllListAppointments().Where(x => x.DoctorId == doctor.Id).Count();

        var completedAppointmentCount = _appointmentService.GetAllListAppointments().Where(x => x.DoctorId == doctor.Id && x.AppointmentStatus == Constants.Completed).Count();

        var todayAppointmentCount = _appointmentService.GetAllListAppointments().Where(x => x.DoctorId == doctor.Id && x.AppointmentStatus == Constants.Booked && x.DateOfAppointment == DateTime.Now).Count();

        var patientCount = _appointmentService.GetAllListAppointments().Where(x => x.DoctorId == doctor.Id && x.AppointmentStatus == Constants.Completed)
            .Where(appointment => appointment.DoctorId == doctor.Id)
            .Select(appointment => appointment.PatientId)
            .Distinct()
            .Count();

        var dashboard = new DoctorDashboardViewModel()
        {
            CompletedAppointments = completedAppointmentCount,
            TotalAppointments = appointmentCount,
            TodaysAppointments = todayAppointmentCount,
            PatientCount = patientCount
        };

        return View(dashboard);
    }
}
