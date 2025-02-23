using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Silverline.Application.Interfaces.Services;
using Microsoft.AspNetCore.WebUtilities;
using Silverline.Core.Entities;
using Silverline.Core.ViewModels;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Silverline.Core.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using DocumentFormat.OpenXml.Office2010.Excel;
using Silverline.Infrastructure.Implementation.Services;

namespace Silverline.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Constants.Admin)]
public class StaffController : Controller
{
	private readonly SignInManager<IdentityUser> _signInManager;
	private readonly UserManager<IdentityUser> _userManager;
	private readonly IUserStore<IdentityUser> _userStore;
	private readonly IEmailSender _emailSender;
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly IWebHostEnvironment _webHostEnvironment;
	private readonly ISpecialtyService _specialtyService;
	private readonly IDoctorService _doctorService;
	private readonly ILabTechnicianService _labTechnicianService;
	private readonly IPharmacistService _pharmacistService;
	private readonly IAppUserService _appUserService;

	public StaffController(
		UserManager<IdentityUser> userManager,
		IUserStore<IdentityUser> userStore,
		SignInManager<IdentityUser> signInManager,
		IEmailSender emailSender,
		RoleManager<IdentityRole> roleManager,
		IWebHostEnvironment webHostEnvironment,
		ISpecialtyService specialtyService,
		IDoctorService doctorService,
		ILabTechnicianService labTechnicianService,
		IPharmacistService pharmacistService,
		IAppUserService appUserService)
	{
		_userManager = userManager;
		_userStore = userStore;
		_signInManager = signInManager;
		_emailSender = emailSender;
		_roleManager = roleManager;
		_webHostEnvironment = webHostEnvironment;
		_specialtyService = specialtyService;
		_doctorService = doctorService;
		_labTechnicianService = labTechnicianService;
		_pharmacistService = pharmacistService;
		_appUserService = appUserService;
	}

	public IActionResult Index()
	{
		var doctor = new UserViewModel();

		var departments = new SelectList(_specialtyService.GetAllSpecialties(), "Id", "Name");

		ViewData["DepartmentId"] = departments;

		return View(doctor);
	}

	[HttpPost]
	public async Task<IActionResult> Index(UserViewModel userModel, IFormFile resume, IFormFile certification, IFormFile image)
	{
		var folder = "staffs";
		var user = new AppUser();
		var role = userModel.Role;
		var password = "EMR@123";
		var wwwRootPath = _webHostEnvironment.WebRootPath;
		var fileCount = Request.Form.Files.Count;
		var doctor = new Core.Entities.Doctor();
		var labTechnician = new Core.Entities.LabTechnician();
		var pharmacist = new Core.Entities.Pharmacist();

		var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
		var stringChars = new char[8];
		var random = new Random();

		for (int i = 0; i < stringChars.Length; i++)
		{
			stringChars[i] = chars[random.Next(chars.Length)];
		}

		var finalString = new String(stringChars);

		user.FullName = userModel.Name;
		user.Email = userModel.Email;
		user.Email = userModel.Email.ToUpper();
		user.PhoneNumber = userModel.PhoneNumber;

		switch (role)
		{
			case "Doctor":
				doctor.DepartmentId = userModel.Speciality.Value;
				doctor.CertificationNumber = userModel.CertificationNumber;
				doctor.HighestMedicalDegree = userModel.HighestMedicalDegree;
				folder = "doctors";
				break;

			case "LabTechnician":
				labTechnician.CertificateNumber = userModel.CertificationNumber;
				labTechnician.HighestMedicalDegree = userModel.HighestMedicalDegree;
				folder = "staffs";
				break;

			case "Pharmacist":
				pharmacist.CertificateNumber = userModel.CertificationNumber;
				pharmacist.HighestMedicalDegree = userModel.HighestMedicalDegree;
				folder = "staffs";
				break;
		}

		if (fileCount > 0)
		{
			var file = image;

			var fileName = $"[{role} - {finalString}] {userModel.Name} - Image";

			var uploads = Path.Combine(wwwRootPath, @$"images\users\{folder}\");

			var extension = Path.GetExtension(file.FileName);

			using (var dataStream = new MemoryStream())
			{
				file.CopyToAsync(dataStream);
				user.ProfileImage = dataStream.ToArray();
			}

			using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
			{
				file.CopyTo(fileStreams);
			}

			user.ImageURL = @$"\images\users\{folder}\" + fileName + extension;

			user.EmailConfirmed = true;
			
			await _userManager.UpdateAsync(user);
		}

		if (resume != null)
		{
			var fileName = $"[{role} - {finalString}] {user.FullName} - Resume";
			var uploads = Path.Combine(wwwRootPath, @$"images\resume\{folder}\");
			var extension = Path.GetExtension(resume.FileName);

			using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
			{
				resume.CopyTo(fileStreams);
			}

			switch (role)
			{
				case "Doctor":
					doctor.ResumeURL = @$"\images\resume\{folder}\" + fileName + extension;
					break;

				case "LabTechnician":
					labTechnician.ResumeURL = @$"\images\resume\{folder}\" + fileName + extension;
					break;

				case "Pharmacist":
					pharmacist.ResumeURL = @$"\images\resume\{folder}\" + fileName + extension;
					break;
			}

		}

		if (certification != null)
		{
			var fileName = $"[{role} - {finalString}] {user.FullName} - Certificates";
			var uploads = Path.Combine(wwwRootPath, @$"images\certification\{folder}\");
			var extension = Path.GetExtension(certification.FileName);

			using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
			{
				certification.CopyTo(fileStreams);
			}

			switch (role)
			{
				case "Doctor":
					doctor.CertificationURL = @$"\images\certification\{folder}\" + fileName + extension;
					break;

				case "LabTechnician":
					labTechnician.CertificationURL = @$"\images\certification\{folder}\" + fileName + extension;
					break;

				case "Pharmacist":
					pharmacist.CertificationURL = @$"\images\certification\{folder}\" + fileName + extension;
					break;
			}
		}

		user.EmailConfirmed = true;

		await _userManager.AddToRoleAsync(user, userModel.Role);

		await _userStore.SetUserNameAsync(user, userModel.Email, CancellationToken.None);

		user.EmailConfirmed = true;

		var result = await _userManager.CreateAsync(user, password);

		if (result.Succeeded)
		{
			var userId = await _userManager.GetUserIdAsync(user);

			switch (role)
			{
				case "Doctor":
					doctor.UserId = userId;
					doctor.IsApproved = true;
					_doctorService.AddDoctor(doctor);
					await _emailSender.SendEmailAsync(userModel.Email, "Successful Registration",
					$"Dear {userModel.Name}, <br><br>Your request has been accepted and you have been registered to our system as a doctor. <br>Please use your registered email and password as <b>\"EMR@123\"</b> as the login credential for the system.<br>Regards,<br>EMR Hospital");
		            TempData["Success"] = "Doctor Added Successfully";
					return RedirectToAction("Index", "Doctor");

				case "LabTechnician":
					labTechnician.UserId = userId;
					labTechnician.IsApproved = true;
					_labTechnicianService.AddLabTechnician(labTechnician);
					await _emailSender.SendEmailAsync(userModel.Email, "Successful Registration",
					$"Dear {userModel.Name}, <br><br>Your request has been accepted and you have been registered to our system as a lab technician. <br>Please use your registered email and password as <b>\"EMR@123\"</b> as the login credential for the system.<br>Regards,<br>EMR Hospital");
		            TempData["Success"] = "Lab Technician Approved Successfully";
					return RedirectToAction("Index", "LabTechnician");

				case "Pharmacist":
					pharmacist.UserId = userId;
					pharmacist.IsApproved = true;
					_pharmacistService.AddPharmacist(pharmacist);
					await _emailSender.SendEmailAsync(userModel.Email, "Successful Registration",
					$"Dear {userModel.Name}, <br><br>Your request has been accepted and you have been registered to our system as a pharmacist. <br>Please use your registered email and password as <b>\"EMR@123\"</b> as the login credential for the system.<br>Regards,<br>EMR Hospital");
		            TempData["Success"] = "Pharmacist Approved Successfully";
					return RedirectToAction("Index", "Pharmacist");
			}
		}
		return View();
	}
}
