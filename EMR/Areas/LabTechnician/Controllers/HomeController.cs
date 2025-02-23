using Microsoft.AspNetCore.Mvc;
using Silverline.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Silverline.Application.Interfaces.Services;
using Silverline.Core.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Silverline.Areas.LabTechnician.Controllers;

[Area("LabTechnician")]
[Authorize(Roles = Constants.LabTechnician)]
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
    private readonly ILabDiagnosisService _labDiagnosisService;
    private readonly ILabTechnicianService _labTechnicianService;
    private readonly ITestCartService _cartService;

    public HomeController(IAppUserService appUserService,
        IPatientService patientService,
        IDoctorService doctorService,
        IAppointmentService appointmentService,
        ISpecialtyService specialtyService,
        ITestService testService,
        IMedicineService medicineService,
        IAppointmentDetailService appointmentDetailService,
        IMedicalTreatmentService medicalTreatmentService,
        ILabDiagnosisService labDiagnosisService,
        ILabTechnicianService labTechnicianService,
        ITestCartService cartService)
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
        _labDiagnosisService = labDiagnosisService;
        _labTechnicianService = labTechnicianService;
        _cartService = cartService;
    }

    public IActionResult Index()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;

        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        var technician = _labTechnicianService.GetAllLabTechnicians().Where(x => x.UserId == claim.Value).FirstOrDefault();

        var diagnosis = _labDiagnosisService.GetAllLabDiagnosis().Count();

        var completedDiagnosis = _labDiagnosisService.GetAllLabDiagnosis().Where(x => x.TechnicianId == technician.Id && x.ActionStatus == Constants.Completed).Count();

        var remainingDiagnosis = _labDiagnosisService.GetAllLabDiagnosis().Where(x => x.ActionStatus == Constants.Ongoing).Count();

        var diagnosisCount = _testService.GetAllDiagnosticTests().Count();

        var cartCount = _cartService.GetAllTestCarts().Where(x => x.ActionStatus == Constants.Completed || x.ActionStatus == Constants.Ongoing).Count();  

        var dashboard = new StaffDashboardViewModel()
        {
            CompletedDiagnosis = completedDiagnosis,
            DiagnosisCount = diagnosisCount,
            RemainingDiagnosis = remainingDiagnosis,
            TotalDiagnosis = diagnosis + cartCount,
        };

        return View(dashboard);
    }
}
