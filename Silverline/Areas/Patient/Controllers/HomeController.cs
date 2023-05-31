using Microsoft.AspNetCore.Mvc;
using Silverline.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Silverline.Application.Interfaces.Services;
using System.Security.Claims;
using Silverline.Core.Entities;
using Silverline.Core.ViewModels;
using Silverline.Infrastructure.Implementation.Services;

namespace Silverline.Areas.Patient.Controllers;

[Area("Patient")]
[Authorize(Roles = Constants.Patient)]
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
        var patient = _patientService.GetAllPatients().Where(x => x.UserId == claim.Value).FirstOrDefault();
        
        var appointmentCount = _appointmentService.GetAllListAppointments().Where(x => x.PatientId == patient.Id).Count();

        var bookedAppointmentCount = _appointmentService.GetAllListAppointments().Where(x => x.PatientId == patient.Id).Where(x => x.AppointmentStatus == Constants.Booked).Count();

        var totalDoctorsVisited = _appointmentService.GetAllListAppointments().Where(x => x.PatientId == patient.Id).Where(x => x.AppointmentStatus == Constants.Completed)
            .Where(appointment => appointment.PatientId == patient.Id)
            .Select(appointment => appointment.DoctorId)
            .Distinct()
            .Count();

        var appointments = _appointmentService.GetAllFinalizedAppointments().Where(x => x.PatientId == patient.Id).ToList();

        var appointmentDetails = _appointmentDetailService.GetAllAppointments();
        
        var medications = _medicineService.GetAllMedicines();

        var medicalTreatments = _medicalTreatmentService.GetMedicationTreatments().Where(x => x.ActionStatus == Constants.Completed && x.Status == Constants.Ongoing).OrderBy(x => x.Status);

        var result = (from appointment in appointments
                      join appointmentDetail in appointmentDetails
                      on appointment.Id equals appointmentDetail.AppointmentId
                      join diagnosis in medicalTreatments
                      on appointmentDetail.Id equals diagnosis.ReferralId
                      join medicine in medications
                      on diagnosis.MedicineId equals medicine.Id
                      select new DiagnosisTreatmentViewModel
                      {
                          TreatmentId = diagnosis.Id,
                          MedicineName = medicine.Name,
                          Remarks = diagnosis.PharmacistRemarks,
                          Dose = diagnosis.Dose,
                          TimeFormat = diagnosis.TimeFormat,
                          TimePeriod = diagnosis.TimePeriod,
                          ReferredBy = _appUserService.GetUser(_doctorService.GetDoctor(appointment.DoctorId).UserId).FullName,
                          Status = diagnosis.Status
                      }).ToList().Count();

        var dashboard = new PatientDashboardViewModel()
        {
            Appointment = appointmentCount,
            BookedAppointment = bookedAppointmentCount,
            Medications = result,
            VisitedDoctors = totalDoctorsVisited
        };

        return View(dashboard);
	}
}
