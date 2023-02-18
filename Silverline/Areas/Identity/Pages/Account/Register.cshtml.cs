using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Silverline.Core.Constants;
using Silverline.Core.Entities;

namespace Silverline.Areas.Identity.Pages.Account
{
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

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _webHostEnvironment = webHostEnvironment;
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
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Profile Image")]
            public byte[] ProfileImage { get; set; }

            public string Role { get; set; }

            [ValidateNever]
            public IEnumerable<SelectListItem> RolesList { get; set; }
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

            Input = new InputModel()
            {
                RolesList = _roleManager.Roles.Select(x => x.Name).Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i
                })
            };
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (!ModelState.IsValid)
            {
                var user = CreateUser();
                var role = Input.Role;
                var folder = String.Empty;
                var wwwRootPath = _webHostEnvironment.WebRootPath;
                var fileCount = Request.Form.Files.Count;

                user.FullName = Input.FullName;
                user.PhoneNumber = Input.PhoneNumber;

                switch (role)
                {
                    case "Admin":
                        folder = "admin";
                        break;
                    case "Patient":
                        folder = "patient";
                        break;
                    case "Doctor":
                        folder = "doctor";
                        break;
                    default:
                        folder = "staffs";
                        break;
                }

                if (fileCount > 0)
                {
                    var file = Request.Form.Files.FirstOrDefault();

                    var fileName = $"{Guid.NewGuid().ToString()} - {user.FullName}";

                    var uploads = Path.Combine(wwwRootPath, @$"images\users\{folder}");
                    
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

                    user.ImageURL = @$"\images\users\{folder}" + fileName + extension;

                    await _userManager.UpdateAsync(user);
                }


                user.PhoneNumber = Input.PhoneNumber;

                await _userManager.AddToRoleAsync(user, Input.Role);

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);

                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Email Confirmation",
                        $"Hi there, You have been registered to our system. To confirm your email address, please continue by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
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
}
