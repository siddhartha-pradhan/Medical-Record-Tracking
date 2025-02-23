using Microsoft.AspNetCore.Mvc;
using Silverline.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Silverline.Application.Interfaces.Services;
using Silverline.Core.ViewModels;
using System.Security.Claims;

namespace Silverline.Areas.Pharmacist.Controllers;

[Area("Pharmacist")]
[Authorize(Roles = Constants.Pharmacist)]
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
    private readonly IPharmacistService _pharmacistService;

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
        IPharmacistService pharmacistService)
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
        _pharmacistService = pharmacistService;
    }

    public IActionResult Index()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;

        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        var pharmacist = _pharmacistService.GetAllPharmacists().Where(x => x.UserId == claim.Value).FirstOrDefault();

        var diagnosis = _medicalTreatmentService.GetMedicationTreatments().Count();

        var completedDiagnosis = _medicalTreatmentService.GetMedicationTreatments().Where(x => x.PharmacistId == pharmacist.Id && x.ActionStatus == Constants.Completed).Count();

        var remainingDiagnosis = _medicalTreatmentService.GetMedicationTreatments().Where(x => x.ActionStatus == Constants.Ongoing).Count();

        var diagnosisCount = _medicineService.GetAllMedicines().Count();

        var dashboard = new StaffDashboardViewModel()
        {
            CompletedDiagnosis = completedDiagnosis,
            DiagnosisCount = diagnosisCount,
            RemainingDiagnosis = remainingDiagnosis,
            TotalDiagnosis = diagnosis
        };

        return View(dashboard);
    }
}
