using Microsoft.AspNetCore.Mvc;
using Silverline.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Silverline.Application.Interfaces.Repositories;
using Silverline.Application.Interfaces.Services;
using System.Security.Claims;
using Silverline.Core.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Globalization;
using System.Text.Encodings.Web;
using System.Text;
using Silverline.Core.Entities;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Silverline.Areas.Doctor.Controllers;

[Area("Doctor")]
[Authorize(Roles = Constants.Doctor)]
public class PatientController : Controller
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
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUserStore<IdentityUser> _userStore;
    private readonly IEmailSender _emailSender;


    public PatientController(IAppointmentService appointmentService,
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
        IWebHostEnvironment webHostEnvironment,
        UserManager<IdentityUser> userManager,
        IUserStore<IdentityUser> userStore,
        IEmailSender emailSender)
	{
		_appointmentService = appointmentService;
		_appointmentDetailService = appointmentDetailService;
		_userService = userService;
		_doctorService = doctorService;
		_specialtyService = specialtyService;
		_patientService = patientService;
		_medicineService = medicineService;
		_testService = testService;
		_unitOfWork = unitOfWork;
		_medicalRecordService = medicalRecordService;
		_labDiagnosisService = labDiagnosisService;
		_medicalTreatmentService = medicalTreatmentService;
        _webHostEnvironment = webHostEnvironment;
        _userManager = userManager;
        _userStore = userStore;
        _emailSender = emailSender;
    }

	public IActionResult Index()
	{
		var claimsIdentity = (ClaimsIdentity)User.Identity;

		var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

		var doctor = _doctorService.GetAllDoctors().Where(x => x.UserId == claim.Value).FirstOrDefault();

		var appointments = _appointmentService.GetAllFinalizedAppointments().Where(x => x.DoctorId == doctor.Id).ToList();

		var patients = _patientService.GetAllPatients();

		var users = _userService.GetAllUsers();

		var result = (from appointment in appointments
					  join patient in patients
						 on appointment.PatientId equals patient.Id
					  join user in users
						 on patient.UserId equals user.Id
					  select new DoctorPatientViewModel
					  {
						  UserId = user.Id,
						  PatientId = patient.Id,
						  Address = patient.Address,
						  Street = patient.Street,
						  DateOfBirth = patient.DateOfBirth.ToString("dd/MM/yyyy"),
						  PatientName = user.FullName,
						  PhoneNumber = user.PhoneNumber,
					  }).DistinctBy(x => x.UserId).ToList();

		return View(result);
	}

	public IActionResult Add()
    {
        var patient = new AddPatientViewModel();

		return View(patient);
	}

	[HttpPost]
    public async Task<IActionResult> Add(AddPatientViewModel patient, IFormFile image)
    {
        var user = new User();
        var wwwRootPath = _webHostEnvironment.WebRootPath;
        var fileCount = Request.Form.Files.Count;

        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[8];
        var random = new Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        var finalString = new String(stringChars);

        var role = Constants.Patient;

        user.FullName = patient.Name;
        user.Email = patient.EmailAddress;
        user.NormalizedEmail = patient.EmailAddress.ToUpper();
        user.PhoneNumber = patient.PhoneNumber;
        user.EmailConfirmed = true;

        if (fileCount > 0)
        {
            var file = image;

            var fileName = $"[{role} - {finalString}] {user.FullName} - Image";

            var uploads = Path.Combine(wwwRootPath, @$"images\users\patients\");

            var extension = Path.GetExtension(file.FileName);

            using (var dataStream = new MemoryStream())
            {
                await file.CopyToAsync(dataStream);

                user.ProfileImage = dataStream.ToArray();
            }

            using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
            {
                file.CopyTo(fileStreams);
            }

            user.ImageURL = @$"images\users\patient\" + fileName + extension;
        }


        await _userManager.AddToRoleAsync(user, Constants.Patient);

        await _userStore.SetUserNameAsync(user, patient.EmailAddress, CancellationToken.None);

        var result = await _userManager.CreateAsync(user, "EMR@123");

        if (result.Succeeded)
        {
            var userId = await _userManager.GetUserIdAsync(user);

            var test = new Core.Entities.Patient()
            {
                UserId = userId,
                Address = patient.Address,
                Street = patient.Street,
                DateOfBirth = patient.DateBirth
            };

            _patientService.AddPatient(test);

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            await _emailSender.SendEmailAsync(patient.EmailAddress, "Email Confirmation",
                $"Dear {user.FullName},<br><br>You have been registered to our system as a patient. <br>You can log in to the system through the following credentials of your email address and password as <b>\"EMR@123\"</b>.<br><br>Regards,<br>EMR Hospital");

            TempData["Success"] = "Patient Registered Successfully";

            return RedirectToAction("Index");

        }
        return View(patient);

    }
}
