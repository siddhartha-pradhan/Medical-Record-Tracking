using System.Text;
using Silverline.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.UI.Services;
using Silverline.Application.Interfaces.Services;
using Silverline.Core.Constants;

namespace Silverline.Areas.Identity.Pages.Account;

public class RegisterModel : PageModel
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUserStore<IdentityUser> _userStore;
    private readonly IUserEmailStore<IdentityUser> _emailStore;
    private readonly IEmailSender _emailSender;
    private readonly ILogger<RegisterModel> _logger;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly ISpecialtyService _specialtyService;
    private readonly IDoctorService _doctorService;
    private readonly ILabTechnicianService _labTechnicianService;
    private readonly IPharmacistService _pharmacistService;

    public RegisterModel(
        UserManager<IdentityUser> userManager,
        IUserStore<IdentityUser> userStore,
        SignInManager<IdentityUser> signInManager,
        ILogger<RegisterModel> logger,
        IEmailSender emailSender,
        RoleManager<IdentityRole> roleManager,
        IWebHostEnvironment webHostEnvironment,
        ISpecialtyService specialtyService,
        IDoctorService doctorService,
        ILabTechnicianService labTechnicianService,
        IPharmacistService pharmacistService)
    {
        _userManager = userManager;
        _userStore = userStore;
        _emailStore = GetEmailStore();
        _signInManager = signInManager;
        _logger = logger;
        _emailSender = emailSender;
        _roleManager = roleManager;
        _webHostEnvironment = webHostEnvironment;
        _specialtyService = specialtyService;
        _doctorService = doctorService;
        _labTechnicianService = labTechnicianService;
        _pharmacistService = pharmacistService;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public string ReturnUrl { get; set; }

    public IList<AuthenticationScheme> ExternalLogins { get; set; }

    public class InputModel
    {
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Enter a valid 10 digit phone number.")]
        public string PhoneNumber { get; set; }
        
        [Required]
        [Display(Name = "Specification Number")]
        public string CertificationNumber { get; set; }

        [Required]
        [Display(Name = "Department")]
        public Guid DepartmentId { get; set; }

        [Required]
        [Display(Name = "Highest Medical Degree")]
        public string HighestMedicalDegree { get; set; }

        [Required]
        [Display(Name = "Resume PDF")]
        public string? ResumeURL { get; set; }

        [Required]
        [Display(Name = "Certification PDF")]
        public string? CertificationURL { get; set; }

        [Required]
        [Display(Name = "Profile Image")]
        public byte[]? ProfileImage { get; set; }

        [Required]
        public string Role { get; set; }
    }

    public async Task OnGetAsync(string? returnUrl = null)
    {
        if (!_roleManager.RoleExistsAsync(Constants.Admin).GetAwaiter().GetResult())
        {
            _roleManager.CreateAsync(new IdentityRole(Constants.Admin)).GetAwaiter().GetResult();
        }
        if (!_roleManager.RoleExistsAsync(Constants.Patient).GetAwaiter().GetResult())
        {
            _roleManager.CreateAsync(new IdentityRole(Constants.Patient)).GetAwaiter().GetResult();
        }
        if (!_roleManager.RoleExistsAsync(Constants.Doctor).GetAwaiter().GetResult())
        {
            _roleManager.CreateAsync(new IdentityRole(Constants.Doctor)).GetAwaiter().GetResult();
        }
        if (!_roleManager.RoleExistsAsync(Constants.LabTechnician).GetAwaiter().GetResult())
        {
            _roleManager.CreateAsync(new IdentityRole(Constants.LabTechnician)).GetAwaiter().GetResult();
        }
        if (!_roleManager.RoleExistsAsync(Constants.Pharmacist).GetAwaiter().GetResult())
        {
            _roleManager.CreateAsync(new IdentityRole(Constants.Pharmacist)).GetAwaiter().GetResult();
        }

        ReturnUrl = returnUrl;
        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        var departments = new SelectList(_specialtyService.GetAllSpecialties(), "Id", "Name");
        ViewData["DepartmentId"] = departments;
    }

    public async Task<IActionResult> OnPostAsync(IFormFile resume, IFormFile certification, string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");
        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        if (!ModelState.IsValid)
        {
            var folder = "staffs";
            var user = CreateUser();
            var role = Input.Role;
            var password = "Silverline@123";
            var wwwRootPath = _webHostEnvironment.WebRootPath;
            var fileCount = Request.Form.Files.Count;
            var doctor = new Doctor();
            var labTechnician = new LabTechnician(); 
            var pharmacist = new Pharmacist();

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            user.FullName = Input.FullName;
            user.PhoneNumber = Input.PhoneNumber;

            switch (role)
            {
                case "Doctor":
                    doctor.DepartmentId = Input.DepartmentId;
                    doctor.CertificationNumber = Input.CertificationNumber;
                    doctor.HighestMedicalDegree = "Bachelor of Medicine, Bachelor of Surgery";
                    folder = "doctors";
                    break;
                
                case "LabTechnician":
                    labTechnician.CertificateNumber = Input.CertificationNumber;
                    labTechnician.HighestMedicalDegree = "B.Sc Medical Laboratory Technology";
					folder = "staffs";
                    break;

                case "Pharmacist":
                    pharmacist.CertificateNumber = Input.CertificationNumber;
                    pharmacist.HighestMedicalDegree = "Bachelor of Pharmacy";
					folder = "staffs";
                    break;
            }

            if (fileCount > 0)
            {
                var file = Request.Form.Files.FirstOrDefault();

                var fileName = $"[{role} - {finalString}] {user.FullName} - Image";

                var uploads = Path.Combine(wwwRootPath, @$"images\users\{folder}\");
                
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

                user.ImageURL = @$"\images\users\{folder}\" + fileName + extension;

                await _userManager.UpdateAsync(user);
            }

            if(resume != null)
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

            await _userManager.AddToRoleAsync(user, Input.Role);

            await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);

            await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
            
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                var userId = await _userManager.GetUserIdAsync(user);

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                switch (role)
                {
                    case "Doctor":
                        doctor.UserId = userId;
                        _doctorService.AddDoctor(doctor);
                        break;
                    case "LabTechnician":
                        labTechnician.UserId = userId;
                        _labTechnicianService.AddLabTechnician(labTechnician);
                        break;
                    case "Pharmacist":
                        pharmacist.UserId = userId;
                        _pharmacistService.AddPharmacist(pharmacist);
                        break;
                }

                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                    protocol: Request.Scheme);

                TempData["Success"] = "Registration requested successfully";

                return RedirectToPage("./Register");

                //await _emailSender.SendEmailAsync(Input.Email, "Email Confirmation",
                //    $"Hi there, You have been registered to our system. To confirm your email address, please continue by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                //if (_userManager.Options.SignIn.RequireConfirmedAccount)
                //{
                //    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                //}
                //else
                //{
                //    await _signInManager.SignInAsync(user, isPersistent: false);
                //    return LocalRedirect(returnUrl);
                //}
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return Page();
    }

    private AppUser CreateUser()
    {
        try
        {
            return Activator.CreateInstance<AppUser>();
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(AppUser)}'. " +
                $"Ensure that '{nameof(AppUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
        }
    }

    private IUserEmailStore<IdentityUser> GetEmailStore()
    {
        if (!_userManager.SupportsUserEmail)
        {
            throw new NotSupportedException("The default UI requires a user store with email support.");
        }
        return (IUserEmailStore<IdentityUser>)_userStore;
    }
}
