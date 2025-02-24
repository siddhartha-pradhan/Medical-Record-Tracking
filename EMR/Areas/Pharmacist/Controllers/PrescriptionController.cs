using Microsoft.AspNetCore.Mvc;
using EMR.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using EMR.Application.Interfaces.Services;
using EMR.Core.Entities;
using EMR.Core.ViewModels;
using System.Security.Claims;

namespace EMR.Areas.Pharmacist.Controllers;

[Area("Pharmacist")]
[Authorize(Roles = Constants.Pharmacist)]
public class PrescriptionController : Controller
{
    private readonly IMedicineService _medicineService;
    private readonly IPatientService _patientService;
    private readonly IDoctorService _doctorService;
    private readonly IAppointmentDetailService _appointmentDetailService;
    private readonly IAppointmentService _appointmentService;
    private readonly IMedicalTreatmentService _medicalTreatmentService;
    private readonly IAppUserService _appUserService;
    private readonly IPharmacistService _pharmacistService;

    public PrescriptionController(IMedicineService medicineService,
        IPatientService patientService,
        IDoctorService doctorService,
        IAppointmentDetailService appointmentDetailService,
        IAppointmentService appointmentService,
        IMedicalTreatmentService medicalTreatmentService,
        IAppUserService appUserService,
        IPharmacistService pharmacistService)
    {
        _medicineService = medicineService;
        _patientService = patientService;
        _doctorService = doctorService;
        _appointmentDetailService = appointmentDetailService;
        _appointmentService = appointmentService;
        _appUserService = appUserService;
        _medicalTreatmentService = medicalTreatmentService;
        _pharmacistService = pharmacistService;
    }

    private Appointment Appointment(Guid Id)
    {
        var appointmentDetail = _appointmentDetailService.GetAppointmentDetail(Id);

        var appointment = _appointmentService.GetAppointment(appointmentDetail.AppointmentId);

        return appointment;
    }

    private User AppUser(string Id)
    {
        var user = _appUserService.GetUser(Id);

        return user;
    }

    public IActionResult Diagnosis()
    {
        var prescriptions = _medicalTreatmentService.GetMedicationTreatments().Where(x => x.ActionStatus == Constants.Pending)
                            .Select(x => new PrescriptionDiagnosisViewModel
                            {
                                Id = x.Id,
                                ReferralId = x.ReferralId,
                                MedicineId = x.MedicineId,
                                MedicineName = _medicineService.GetMedicine(x.MedicineId).Name,
                                MedicineType = _medicineService.GetMedicine(x.MedicineId).Type,
                                MedicineURL = _medicineService.GetMedicine(x.MedicineId).ImageURL,
                                DoctorId = (_doctorService.GetDoctor(Appointment(x.ReferralId).DoctorId)).Id,
                                DoctorName = AppUser(_doctorService.GetDoctor(Appointment(x.ReferralId).DoctorId).UserId).FullName,
                                DoctorRemarks = x.DoctorRemarks,
                                PatientImage = AppUser(_patientService.GetPatient(Appointment(x.ReferralId).PatientId).UserId).ImageURL,
                                PatientId = _patientService.GetPatient(Appointment(x.ReferralId).PatientId).Id,
                                PatientName = AppUser(_patientService.GetPatient(Appointment(x.ReferralId).PatientId).UserId).FullName,
                                Dose = x.Dose,
                                TimeFormat = x.TimeFormat,
                                TimePeriod = x.TimePeriod
                            }).ToList();

        var detail = (from record in prescriptions
                      group record by new
                      {
                          record.ReferralId,
                          record.PatientName,
                          record.PatientImage,
                      } into patient
                      select new PrescriptionViewModel()
                      {
                          Referral = patient.Key.ReferralId,
                          PatientName = patient.Key.PatientName,
                          PatientImage = patient.Key.PatientImage,
                          PrescriptionDiagnosis = patient.ToList(),
                      }).ToList();

        return View(detail);
    }

    public IActionResult Details(Guid Id)
    {
        var prescriptions = _medicalTreatmentService.GetMedicationTreatments().Where(x => x.Id == Id).ToList();

        var result = prescriptions.Select(x => new PrescriptionDetailViewModel
        {
            MedicineName = _medicineService.GetMedicine(x.MedicineId).Name,
            MedicineType = _medicineService.GetMedicine(x.MedicineId).Type,
            MedicineURL = _medicineService.GetMedicine(x.MedicineId).ImageURL,
            DoctorId = (_doctorService.GetDoctor(Appointment(x.ReferralId).DoctorId)).Id,
            DoctorName = AppUser(_doctorService.GetDoctor(Appointment(x.ReferralId).DoctorId).UserId).FullName,
            PatientImage = AppUser(_patientService.GetPatient(Appointment(x.ReferralId).PatientId).UserId).ImageURL,
            PatientId = _patientService.GetPatient(Appointment(x.ReferralId).PatientId).Id,
            PatientName = AppUser(_patientService.GetPatient(Appointment(x.ReferralId).PatientId).UserId).FullName,
            MedicationTreatments = new()
            {
                Id = x.Id,
                ReferralId = x.ReferralId,
                MedicineId = x.MedicineId,
                DoctorRemarks = x.DoctorRemarks,
                Dose = x.Dose,
                TimePeriod = x.TimePeriod,
                TimeFormat = x.TimeFormat,
            }
        }).FirstOrDefault();

        return View(result);
    }

    public IActionResult History()
    {
        var prescriptions = _medicalTreatmentService.GetMedicationTreatments().Where(x => x.ActionStatus == Constants.Completed)
                            .Select(x => new PrescriptionDiagnosisViewModel
                            {
                                Id = x.Id,
                                ReferralId = x.ReferralId,
                                MedicineId = x.MedicineId,
                                MedicineName = _medicineService.GetMedicine(x.MedicineId).Name,
                                MedicineType = _medicineService.GetMedicine(x.MedicineId).Type,
                                MedicineURL = _medicineService.GetMedicine(x.MedicineId).ImageURL,
                                DoctorId = (_doctorService.GetDoctor(Appointment(x.ReferralId).DoctorId)).Id,
                                DoctorName = AppUser(_doctorService.GetDoctor(Appointment(x.ReferralId).DoctorId).UserId).FullName,
                                DoctorRemarks = x.DoctorRemarks,
                                PatientImage = AppUser(_patientService.GetPatient(Appointment(x.ReferralId).PatientId).UserId).ImageURL,
                                PatientId = _patientService.GetPatient(Appointment(x.ReferralId).PatientId).Id,
                                PatientName = AppUser(_patientService.GetPatient(Appointment(x.ReferralId).PatientId).UserId).FullName,
                                FinalizedDate = x.FinalizedDate,
                                PharmacistRemarks = x.PharmacistRemarks,
                                Dose = x.Dose,
                                TimeFormat = x.TimeFormat,
                                TimePeriod = x.TimePeriod
                            }).OrderByDescending(x => x.FinalizedDate).ToList();

        var detail = (from record in prescriptions
                      group record by new
                      {
                          record.ReferralId,
                          record.PatientName,
                          record.PatientImage,
                      } into patient
                      select new PrescriptionViewModel()
                      {
                          Referral = patient.Key.ReferralId,
                          PatientName = patient.Key.PatientName,
                          PatientImage = patient.Key.PatientImage,
                          PrescriptionDiagnosis = patient.ToList(),
                      }).ToList();

        return View(detail);
    }

    #region API Calls
    [HttpPost]
    public IActionResult Details(PrescriptionDetailViewModel detailViewModel)
    {
        var result = detailViewModel.MedicationTreatments;

        var claimsIdentity = (ClaimsIdentity)User.Identity;

        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        var pharmacist = _pharmacistService.GetAllPharmacists().Where(x => x.UserId == claim.Value).FirstOrDefault();

        result.PharmacistId = pharmacist.Id;

        _medicalTreatmentService.UpdatePrescriptions(result);

        TempData["Success"] = "Medication Purchase Successfully Completed";

        return RedirectToAction("Diagnosis");
    }

    public IActionResult Export(Guid patientId)
    {
        var medicines = _medicineService.GetAllMedicines();
        var patient = _patientService.GetPatient(patientId);
        var user = _appUserService.GetAllUsers().Where(x => x.Id == patient.UserId).FirstOrDefault();
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        var pharmacist = _pharmacistService.GetAllPharmacists().Where(x => x.UserId == claim.Value).FirstOrDefault();
        var appointments = _appointmentService.GetAllFinalizedAppointments().Where(x => x.PatientId == patientId).ToList();
        var appointmentDetails = _appointmentDetailService.GetAllAppointments();
        var medicalTreatments = _medicalTreatmentService.GetMedicationTreatments().Where(x => x.PharmacistId == pharmacist.Id);

        var result = (from appointment in appointments
                      join appointmentDetail in appointmentDetails
                         on appointment.Id equals appointmentDetail.AppointmentId
                      join diagnosis in medicalTreatments
                         on appointmentDetail.Id equals diagnosis.ReferralId
                      select new ReportViewModel
                      {
                          PatientName = user.FullName,
                          DateOfBirth = patient.DateOfBirth.ToString("dd/MM/yyyy"),
                          Medicine = _medicineService.GetAllMedicines().Where(x => x.Id == diagnosis.MedicineId).FirstOrDefault().Name,
                          TimeFormat = diagnosis.TimeFormat,
                          TimePeriod = diagnosis.TimePeriod,
                          TestRemarks = diagnosis.PharmacistRemarks,
                          FinalizedDate = diagnosis.FinalizedDate?.ToString("dd/MM/yyyy"),
                          StaffName = _appUserService.GetAllUsers().Where(x => x.Id == pharmacist.UserId).FirstOrDefault().FullName
                      }).ToList();

        return View();
    }
    #endregion
}
