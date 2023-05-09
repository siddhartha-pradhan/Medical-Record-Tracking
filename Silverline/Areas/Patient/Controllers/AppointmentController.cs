using System.Security.Claims;
using Silverline.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Silverline.Core.Constants;
using Silverline.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Silverline.Application.Interfaces.Services;
using Silverline.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;

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
    private readonly IAppointmentDetailService _appointmentDetailService;
    private readonly IMedicalTreatmentService _medicalTreatmentService;
    private readonly ILabDiagnosisService _labDiagnosisService;
    private readonly IMedicineService _medicineService;
    private readonly ITestService _testService;
    private readonly IMedicalRecordService _medicalRecordService;
    private readonly UserManager<IdentityUser> _userManager;
	public AppointmentController(IAppUserService appUserService, 
        IDoctorService doctorService, 
        ISpecialtyService specialtyService, 
        IPatientService patientService, 
        IAppointmentService appointmentService,
		IMedicalTreatmentService medicalTreatmentService,
		ILabDiagnosisService labDiagnosisService,
		IAppointmentDetailService appointmentDetailService,
		IMedicineService medicineService,
		ITestService testService,
		IMedicalRecordService medicalRecordService)
    {
        _appUserService = appUserService;
        _doctorService = doctorService;
        _specialtyService = specialtyService;
        _patientService = patientService;
        _appointmentService = appointmentService;
        _appointmentDetailService = appointmentDetailService;
        _medicalTreatmentService = medicalTreatmentService;
        _labDiagnosisService = labDiagnosisService;
        _testService = testService;
        _medicineService = medicineService;
        _medicalRecordService = medicalRecordService;
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
                      }).OrderBy(x => x.Name).ToList();

        return View(result);
    }

    public IActionResult Booking()
    {
		var claimsIdentity = (ClaimsIdentity)User.Identity;
		var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
		var patient = _patientService.GetAllPatients().Where(x => x.UserId == claim.Value).FirstOrDefault();

		var bookedAppointments = _appointmentService.GetAllBookedAppointments()
								 .Where(x => x.PatientId == patient.Id).ToList()
								 .Select(x => new BookedAppointmentViewModel()
								 {
									 DoctorId = x.DoctorId,
									 DoctorName = GetUser(x.DoctorId).FullName,
									 DoctorImage = GetUser(x.DoctorId).ProfileImage,
									 Title = x.AppointmentRequest,
									 Specialty = GetSpecialty(x.DoctorId).Name,
									 DateOfAppointment = x.DateOfAppointment.ToString("dddd, dd MMMM yyyy HH:mm:ss"),
									 BookedDate = x.BookedDate.ToString("dddd, dd MMMM yyyy HH:mm:ss"),
									 HighestMedicalDegree = _doctorService.GetDoctor(x.DoctorId).HighestMedicalDegree,
								 }).ToList();

        return View(bookedAppointments);
	}

    public IActionResult History()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        var patient = _patientService.GetAllPatients().Where(x => x.UserId == claim.Value).FirstOrDefault();

        var finalizedAppointments = _appointmentService.GetAllFinalizedAppointments()
                                    .Where(x => x.PatientId == patient.Id).ToList()
                                    .Select(x => new FinalizedAppointmentViewModel()
                                    {
                                        DoctorId = x.DoctorId,
                                        DoctorName = GetUser(x.DoctorId).FullName,
                                        DoctorImage = GetUser(x.DoctorId).ProfileImage,
                                        Specialty = GetSpecialty(x.DoctorId).Name,
                                        HighestMedicalDegree = _doctorService.GetDoctor(x.DoctorId).HighestMedicalDegree,
                                        FinalizedAppointments = GetFinalizedAppointments(x.DoctorId, x.PatientId)
                                    }).DistinctBy(x => x.DoctorId).ToList();

        return View(finalizedAppointments);
    }

    private AppUser GetUser(Guid doctorId)
    {
        var doctor = _doctorService.GetDoctor(doctorId);

        var user = _appUserService.GetUser(doctor.UserId);
        
        return user;
    }

    private List<AppointmentFinalizedViewModel> GetFinalizedAppointments(Guid doctorId, Guid patientId)
    {
        var appointments = _appointmentService.GetAllFinalizedAppointments().Where(x => x.DoctorId == doctorId && x.PatientId == patientId).ToList();  
        var details = new List<AppointmentFinalizedViewModel>();
        foreach(var appointment in appointments)
        {
            var result = _appointmentDetailService.GetAllAppointments().Where(x => x.AppointmentId == appointment.Id).FirstOrDefault();
            details.Add(new AppointmentFinalizedViewModel()
            {
                AppointmentId = appointment.Id,
                DiagnosticTitle = result.AppointmentTitle,
                DiagnosticDescription = result.AppointmentDescription,
                RequestTitle = appointment.AppointmentRequest,
                BookedDate = appointment.BookedDate.ToString("dddd, dd MMMM yyyy HH:mm:ss"),
                AppointedDate = appointment.FinalizedTime.ToString("dddd, dd MMMM yyyy HH:mm:ss"),
            });
        }
        return details;
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

        var appointmentHistory = _appointmentService.GetAllAppointments(doctor.Id)
            .Where(x => x.PatientId == patient.Id && x.DoctorId == doctor.Id && x.AppointmentStatus == Constants.Booked).FirstOrDefault();

        if(appointmentHistory != null)
        {
            TempData["Delete"] = "You already have an appointment booked with the doctor, check your history.";
            return RedirectToAction("Index");
        }

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

	private Specialty GetSpecialty(Guid doctorId)
	{
		var doctor = _doctorService.GetDoctor(doctorId);

		var specialty = _specialtyService.GetSpecialty(doctor.DepartmentId);

		return specialty;
	}

	public IActionResult AppointmentDetails(Guid id)
    {
        var appointment = _appointmentService.GetAppointment(id);

        var appointmentDetails = _appointmentDetailService.GetAllAppointments().Where(x => x.AppointmentId == appointment.Id).FirstOrDefault();

        var doctor = _doctorService.GetDoctor(appointment.DoctorId);

        var user = _appUserService.GetUser(doctor.UserId);

        var specialty = _specialtyService.GetSpecialty(doctor.DepartmentId);

		var medicationTreatments = _medicalTreatmentService.GetMedicationTreatments().Where(x => x.ReferralId == appointmentDetails.Id)
                                   .Select(y => new MedicationTreatmentViewModel()
                                   {
                                       Id = y.Id,
                                       ReferralId = y.ReferralId,
                                       MedicineId = y.MedicineId,
                                       MedicineName = _medicineService.GetMedicine(y.MedicineId).Name,
                                       DoctorRemarks = y.DoctorRemarks,
                                       Dose = y.Dose,
                                       TimeFormat = y.TimeFormat,
                                       TimePeriod = y.TimePeriod,
                                   }).ToList();

        var laboratoryDiagnosis = _labDiagnosisService.GetAllLabDiagnosis().Where(x => x.ReferralId == appointmentDetails.Id)
                                  .Select(y => new LaboratoryDiagnosisViewModel()
                                  {
                                      Id = y.Id,
                                      ReferralId = y.ReferralId,
                                      TestId = y.TestId,
                                      TestName = _testService.GetDiagnosticTest(y.TestId).Title,
                                      DoctorRemarks = y.DoctorRemarks
                                  }).ToList();


		var result = new AppointmentDetailsViewModel()
        {
            AppointmentId = id,
            DoctorImage = user.ProfileImage,
            DoctorName = user.FullName,
            HighestMedicalDegree = doctor.HighestMedicalDegree,
            Specialty = specialty.Name,
			BookedDate = appointment.BookedDate.ToString("dddd, dd MMMM yyyy HH:mm:ss"),
            AppointedDate = appointment.DateOfAppointment.ToString("dddd, dd MMMM yyyy HH:mm:ss"),
            FinalizedDate = appointment.FinalizedTime.ToString("dddd, dd MMMM yyyy HH:mm:ss"),
            Request = appointment.AppointmentRequest,
            Title = appointmentDetails.AppointmentTitle,
            Description = appointmentDetails.AppointmentDescription,
            MedicationTreatments = medicationTreatments,
			LaboratoryDiagnosis = laboratoryDiagnosis,
        };

        return View(result);

    }

    public IActionResult MedicalRecords()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        var patient = _patientService.GetAllPatients().Where(x => x.UserId == claim.Value).FirstOrDefault();

        var result = (from record in _medicalRecordService.GetAllMedicalRecords(patient.Id).ToList()
                     group record by record.Specialty into specialty
                     select new MedicalRecordViewModel()
                     {
                         Specialty = specialty.Key,
                         MedicalRecords = specialty.ToList(),
                     }).ToList();

        return View(result);
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