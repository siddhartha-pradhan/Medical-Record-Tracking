using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Silverline.Core.Constants;
using Silverline.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Silverline.Application.Interfaces.Services;
using Silverline.Infrastructure.Persistence;
using Silverline.Application.Interfaces.Repositories;
using Silverline.Core.Entities;

namespace Silverline.Areas.Doctor.Controllers;

[Area("Doctor")]
[Authorize(Roles = Constants.Doctor)]
public class AppointmentController : Controller
{
    private readonly IAppointmentService _appointmentService;
    private readonly IAppointmentDetailService _appointmentDetailService;
    private readonly IAppUserService _userService;
    private readonly IDoctorService _doctorService;
    private readonly ISpecialtyService _specialtyService;
    private readonly IPatientService _patientService;
    private readonly IMedicineService _medicineService;
    private readonly ITestService _testService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMedicalRecordService _medicalRecordService;
    private readonly ILabDiagnosisService _labDiagnosisService;
    private readonly IMedicalTreatmentService _medicalTreatmentService;
    private readonly ApplicationDbContext _dbContext;

    public AppointmentController(
        IAppointmentService appointmentService, 
        IAppointmentDetailService appointmentDetailService,
        IAppUserService userService, 
        IDoctorService doctorService,
        ISpecialtyService specialtyService,
        IPatientService patientService,
		IMedicineService medicineService,
		ITestService testService,
		IUnitOfWork unitOfWork,
		IMedicalRecordService medicalRecordService,
        ILabDiagnosisService labDiagnosisService,
        IMedicalTreatmentService medicalTreatmentService,
        ApplicationDbContext dbContext)
    {
        _userService = userService;
        _appointmentService = appointmentService;
        _appointmentDetailService = appointmentDetailService;
        _doctorService = doctorService;
        _patientService = patientService;
        _specialtyService = specialtyService;
        _medicineService = medicineService;
		_testService = testService;
		_unitOfWork = unitOfWork;
        _medicalRecordService = medicalRecordService;
        _labDiagnosisService = labDiagnosisService;
        _medicalTreatmentService = medicalTreatmentService;
        _dbContext = dbContext;

	}

    #region Razor Pages
    public IActionResult Index()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;

        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        var doctor = _doctorService.GetAllDoctors().Where(x => x.UserId == claim.Value).FirstOrDefault();

        var appointments = _appointmentService.GetAllBookedAppointment(doctor.Id).Where(x => x.DateOfAppointment.Date == DateTime.Today).OrderBy(x => x.BookedDate);

        var appoint = new List<AppointmentsViewModel>();

        foreach(var appointment in appointments)
        {
            var userId = _patientService.GetPatient(appointment.PatientId).UserId;

            var user = _userService.GetUser(userId);

            var patient = _patientService.GetAllPatients().Where(x => x.UserId == userId).FirstOrDefault();

            appoint.Add(new AppointmentsViewModel()
            {
                AppointmentId = appointment.Id,
                PatientId = patient.Id,
                PatientAge = DateTime.Today.Year - patient.DateOfBirth.Year,
                PatientImage = user.ImageURL,
                PatientName = user.FullName,
                PatientRequest = appointment.AppointmentRequest,
                PaymentStatus = appointment.PaymentStatus,
            });
        }

        return View(appoint);
    }

	public IActionResult Tomorrow()
	{
		var claimsIdentity = (ClaimsIdentity)User.Identity;

		var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

		var doctor = _doctorService.GetAllDoctors().Where(x => x.UserId == claim.Value).FirstOrDefault();

		var appointments = _appointmentService.GetAllBookedAppointment(doctor.Id).Where(x => x.DateOfAppointment.Date > DateTime.Today).OrderBy(x => x.BookedDate);

		var appoint = new List<AppointmentsViewModel>();

		foreach (var appointment in appointments)
		{
			var userId = _patientService.GetPatient(appointment.PatientId).UserId;

			var user = _userService.GetUser(userId);

			var patient = _patientService.GetAllPatients().Where(x => x.UserId == userId).FirstOrDefault();

			appoint.Add(new AppointmentsViewModel()
			{
				AppointmentId = appointment.Id,
				PatientId = patient.Id,
				PatientAge = DateTime.Today.Year - patient.DateOfBirth.Year,
				PatientImage = user.ImageURL,
				PatientName = user.FullName,
				PatientRequest = appointment.AppointmentRequest,
				PaymentStatus = appointment.PaymentStatus
			});
		}

		return View(appoint);
	}

	public IActionResult Emergency()
	{
        var patients = _patientService.GetAllPatients();
        var users = _userService.GetAllUsers();

        var result = (from user in users
                      join patient in patients
                      on user.Id equals patient.UserId
                      select new
                      {
                          Id = patient.Id,
                          Name = user.FullName
                      }).ToList();

		var patientResult = new SelectList(result, "Id", "Name");

		ViewData["PatientId"] = patientResult;

		var medicines = _medicineService.GetAllMedicines()
			.Select(x => new SelectListItem()
			{
				Text = x.Name,
				Value = x.Id.ToString()
			}).ToList();

		var tests = _testService.GetAllDiagnosticTests()
			.Select(x => new SelectListItem()
			{
				Text = x.Title,
				Value = x.Id.ToString()
			}).ToList();

        var appointmentVM = new EmergencyAppointmentViewModel()
        {
            MedicineList = medicines,
            LaboratoryTestList = tests,
            Appointment = new Core.Entities.Appointment(),
            AppointmentDetail = new Core.Entities.AppointmentDetail()
            {
                MedicalTreatments = new(),
                LaboratoryDiagnosis = new()
            }
        };

		appointmentVM.AppointmentDetail.MedicalTreatments.Add(new() { Id = Guid.NewGuid() });
		appointmentVM.AppointmentDetail.LaboratoryDiagnosis.Add(new() { Id = Guid.NewGuid() });

		return View(appointmentVM);
	}

	public IActionResult Start(Guid Id)
    {
        var appointment = _appointmentService.GetAppointment(Id);
        var patient = _patientService.GetPatient(appointment.PatientId);
        var user = _userService.GetUser(patient.UserId);

        var medicines = _medicineService.GetAllMedicines()
            .Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
		var tests = _testService.GetAllDiagnosticTests()
			.Select(x => new SelectListItem()
			{
				Text = x.Title,
				Value = x.Id.ToString()
			});

		var appointmentVM = new AppointmentDetailViewModel()
        {
            Appointment = new Core.Entities.AppointmentDetail()
            {
                AppointmentId = Id,
                MedicalTreatments = new(),
                LaboratoryDiagnosis = new()
            },
            MedicineList = medicines.ToList(),
            LaboratoryTestList = tests.ToList(),
            PatientAge = DateTime.Today.Year - patient.DateOfBirth.Year,
            PatientName = user.FullName,
        };
        appointment.StartTime = DateTime.Now;
        appointmentVM.Appointment.MedicalTreatments.Add(new() { Id = Guid.NewGuid() });
        appointmentVM.Appointment.LaboratoryDiagnosis.Add(new() { Id = Guid.NewGuid() });
        _unitOfWork.Save();

		return View(appointmentVM);
    }

	public IActionResult MedicalRecords(Guid patientId)
	{
        var user = _patientService.GetAllPatients().Where(x => x.Id == patientId).FirstOrDefault();

        var appUser = _userService.GetUser(user.UserId);

		var patient = _patientService.GetAllPatients().Where(x => x.UserId == user.UserId).FirstOrDefault();

		var result = (from record in _medicalRecordService.GetAllMedicalRecords(patient.Id).ToList()
					  group record by record.Specialty into specialty
					  select new MedicalRecordViewModel()
					  {
                          UserId = appUser.Id,
                          PatientName = appUser.FullName,
                          Specialty = specialty.Key,
						  MedicalRecords = specialty.ToList(),
					  }).ToList();

        var records = new MedicalRecordsViewModel()
        {
            UserId = appUser.Id,
            Name = appUser.FullName,
            MedicalRecords = result
        };

        return View(records);
	}

    public IActionResult LabDiagnosis(string userId)
    {
        var patient = _patientService.GetAllPatients().Where(x => x.UserId == userId).FirstOrDefault();

        var appointments = _appointmentService.GetAllFinalizedAppointments().Where(x => x.PatientId == patient.Id).ToList();

        var appointmentDetails = _appointmentDetailService.GetAllAppointments();

        var tests = _testService.GetAllDiagnosticTests();

        var labDiagnosis = _labDiagnosisService.GetAllLabDiagnosis().Where(x => x.ActionStatus == Constants.Completed);

        var result = (from appointment in appointments
                      join appointmentDetail in appointmentDetails
                      on appointment.Id equals appointmentDetail.AppointmentId
                      join diagnosis in labDiagnosis
                      on appointmentDetail.Id equals diagnosis.ReferralId
                      join test in tests
                      on diagnosis.TestId equals test.Id
                      select new LabTreatmentViewModel
                      {
                          TestName = test.Title,
                          DateOfTest = diagnosis.FinalizedDate?.ToString("dddd, dd MMMM yyyy"),
                          Remarks = diagnosis.TechnicianRemarks,
                          Range = $"{test.FinalRange} - {test.FinalRange} {test.Unit}",
                          Value = $"{diagnosis.Value} {test.Unit}",
                          ReferredBy = _userService.GetUser(_doctorService.GetDoctor(appointment.DoctorId).UserId).FullName
                      }).ToList();

        return View(result);
    }

    public IActionResult Medications(string userId)
    {
        var patient = _patientService.GetAllPatients().Where(x => x.UserId == userId).FirstOrDefault();

        var appointments = _appointmentService.GetAllFinalizedAppointments().Where(x => x.PatientId == patient.Id).ToList();

        var appointmentDetails = _appointmentDetailService.GetAllAppointments();

        var medications = _medicineService.GetAllMedicines();

        var medicalTreatments = _medicalTreatmentService.GetMedicationTreatments().Where(x => x.ActionStatus == Constants.Completed && x.Status == Constants.Ongoing);

        var result = (from appointment in appointments
                      join appointmentDetail in appointmentDetails
                      on appointment.Id equals appointmentDetail.AppointmentId
                      join diagnosis in medicalTreatments
                      on appointmentDetail.Id equals diagnosis.ReferralId
                      join medicine in medications
                      on diagnosis.MedicineId equals medicine.Id
                      select new DiagnosisTreatmentViewModel
                      {
                          MedicineName = medicine.Name,
                          Remarks = diagnosis.PharmacistRemarks,
                          Dose = diagnosis.Dose,
                          TimeFormat = diagnosis.TimeFormat,
                          TimePeriod = diagnosis.TimePeriod,
                          ReferredBy = _userService.GetUser(_doctorService.GetDoctor(appointment.DoctorId).UserId).FullName
                      }).ToList();

        return View(result);
    }
    #endregion

    #region API Calls
    [HttpPost]
	public IActionResult Start(AppointmentDetailViewModel appointmentVm)
    {
        _appointmentDetailService.FinalizeAppointment(appointmentVm.Appointment);

        var claimsIdentity = (ClaimsIdentity)User.Identity;

        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        var doctor = _doctorService.GetAllDoctors().Where(x => x.UserId == claim.Value).FirstOrDefault();

        var docUser = _userService.GetUser(doctor.UserId);

        var specialty = _specialtyService.GetSpecialty(doctor.DepartmentId);

        var appointment = _appointmentService.GetAppointment(appointmentVm.Appointment.AppointmentId);

        var patient = _patientService.GetPatient(appointment.PatientId);

        var medicines = new List<string>();

		var laboratorytests = new List<string>();

		var medicalTreatments = appointmentVm.Appointment.MedicalTreatments;

		var laboratoryDiagnosis = appointmentVm.Appointment.LaboratoryDiagnosis;

		for (int i = 0; i < medicalTreatments.Count(); i++)
        {
            var medicine = medicalTreatments[i].MedicineId;
            var result = _medicineService.GetMedicine(medicine);
            var meds = $"{result.Name}: {medicalTreatments[i].Dose}";
            medicines.Add(meds);
        }

		for (int i = 0; i < laboratoryDiagnosis.Count(); i++)
		{
			var tests = laboratoryDiagnosis[i].TestId;
			var result = _testService.GetDiagnosticTest(tests);
			var test = $"{result.Title}";
			laboratorytests.Add(test);
		}

		var medicalRecords = new Core.Entities.MedicalRecord
		{
            DateOfAppointment = DateTime.Now.ToString("dddd, dd MMMM yyyy"),
			PatientId = patient.Id,
			Specialty = specialty.Name,
			DoctorName = docUser.FullName,
			Title = appointmentVm.Appointment.AppointmentTitle,
			Description = appointmentVm.Appointment.AppointmentDescription,
			Medicines = string.Join(", ", medicines),
            LaboratoryTests = string.Join(", ", laboratorytests)
		};

        _dbContext.MedicalRecords.Add(medicalRecords);

        _dbContext.SaveChanges();

		TempData["Success"] = "Appointment Finalized Successfully";

		return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Emergency(EmergencyAppointmentViewModel emergencyAppointmentModel)
    {
		var claimsIdentity = (ClaimsIdentity)User.Identity;

		var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

		var doctor = _doctorService.GetAllDoctors().Where(x => x.UserId == claim.Value).FirstOrDefault();

        var id = Guid.NewGuid();

        var appointment = new Appointment()
        {
            Id = id,
            BookedDate = DateTime.Now,
            AppointmentRequest = "Emergency Appointment",
            AppointmentStatus = Constants.Completed,
            DoctorId = doctor.Id,
            FeeAmount = 50,
            PaymentStatus = Constants.Completed,
            StartTime = DateTime.Now,
            FinalizedTime = DateTime.Now,
            DateOfAppointment = DateTime.Now,
            PatientId = emergencyAppointmentModel.PatientId
        };

        _appointmentService.AddEmergencyAppointment(appointment);

        var appointmentDetails = new AppointmentDetail()
        {
            AppointmentId = id,
            AppointmentTitle = emergencyAppointmentModel.AppointmentDetail.AppointmentTitle,
            AppointmentDescription = emergencyAppointmentModel.AppointmentDetail.AppointmentDescription,
            LaboratoryDiagnosis = emergencyAppointmentModel.AppointmentDetail.LaboratoryDiagnosis,
            MedicalTreatments = emergencyAppointmentModel.AppointmentDetail.MedicalTreatments
        };

		_appointmentDetailService.FinalizeAppointment(appointmentDetails);

		var docUser = _userService.GetUser(doctor.UserId);

		var specialty = _specialtyService.GetSpecialty(doctor.DepartmentId);

		var appointmentDetail = _appointmentService.GetAppointment(id);

		var patient = _patientService.GetPatient(appointment.PatientId);

		var medicines = new List<string>();

		var laboratorytests = new List<string>();

		var medicalTreatments = emergencyAppointmentModel.AppointmentDetail.MedicalTreatments;

		var laboratoryDiagnosis = emergencyAppointmentModel.AppointmentDetail.LaboratoryDiagnosis;

		for (int i = 0; i < medicalTreatments.Count(); i++)
		{
			var medicine = medicalTreatments[i].MedicineId;
            if(medicine == Guid.Empty)
            {
                break;
            }
			var result = _medicineService.GetMedicine(medicine);
			var meds = $"{result.Name}: {medicalTreatments[i].Dose}";
			medicines.Add(meds);
		}

		for (int i = 0; i < laboratoryDiagnosis.Count(); i++)
		{
			var tests = laboratoryDiagnosis[i].TestId;
            if(tests == Guid.Empty) 
            { 
                break; 
            }
			var result = _testService.GetDiagnosticTest(tests);
			var test = $"{result.Title}";
			laboratorytests.Add(test);
		}

		var medicalRecords = new MedicalRecord
		{
			DateOfAppointment = DateTime.Now.ToString("dddd, dd MMMM yyyy"),
			PatientId = patient.Id,
			Specialty = specialty.Name,
			DoctorName = docUser.FullName,
			Title = emergencyAppointmentModel.AppointmentDetail.AppointmentTitle,
			Description = emergencyAppointmentModel.AppointmentDetail.AppointmentDescription,
			Medicines = string.Join(", ", medicines),
			LaboratoryTests = string.Join(", ", laboratorytests)
		};

		_dbContext.MedicalRecords.Add(medicalRecords);

		_dbContext.SaveChanges();

		TempData["Success"] = "Appointment Finalized Successfully";

		var patients = _patientService.GetAllPatients();
		var users = _userService.GetAllUsers();

		var patientresult = (from user in users
					  join patientUser in patients
					  on user.Id equals patientUser.UserId
					  select new
					  {
						  Id = patientUser.Id,
						  Name = user.FullName
					  }).ToList();

		var patientResult = new SelectList(patientresult, "Id", "Name");

		ViewData["PatientId"] = patientResult;

		var medicinesList = _medicineService.GetAllMedicines()
			.Select(x => new SelectListItem()
			{
				Text = x.Name,
				Value = x.Id.ToString()
			}).ToList();

		var testsList = _testService.GetAllDiagnosticTests()
			.Select(x => new SelectListItem()
			{
				Text = x.Title,
				Value = x.Id.ToString()
			}).ToList();

		var appointmentVM = new EmergencyAppointmentViewModel()
		{
			MedicineList = medicinesList,
			LaboratoryTestList = testsList,
			Appointment = new Core.Entities.Appointment(),
			AppointmentDetail = new Core.Entities.AppointmentDetail()
			{
				MedicalTreatments = new(),
				LaboratoryDiagnosis = new()
			}
		};

		appointmentVM.AppointmentDetail.MedicalTreatments.Add(new() { Id = Guid.NewGuid() });
		appointmentVM.AppointmentDetail.LaboratoryDiagnosis.Add(new() { Id = Guid.NewGuid() });

		return View(appointmentVM);
    }
    #endregion
}
